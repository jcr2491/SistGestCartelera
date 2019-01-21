using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistGestCart.BL;
using SistGestCart.BE;
using System.Text;
using System.IO;

public partial class ParCarteles : MyBasePage
{
    SCE_CARTEL_BL CARTEL_BL;
    SCE_MODELO_BL MODELO_BL;
    SCE_CARTEL_BE CARTEL_BE = new SCE_CARTEL_BE();
    SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE; 

    private void LoadData()
    {
        DataTable dtRegCartelPV = new DataTable();
        Session["dtRegCartelPV"] = CARTEL_BL.Listar();
        this.grvTipoCartel.DataSource = Session["dtRegCartelPV"];
        this.grvTipoCartel.DataBind();
    }    

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        CARTEL_BL = new SCE_CARTEL_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        MODELO_BL = new SCE_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        txtDescripcion.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");
        txtNomCartelB.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        btnGuardar.OnClientClick = "return IsValid();";

        Util.SetEnterButton(txtNomCartelB, btnBuscarT);

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ParCarteles.aspx";

            LoadData();
        }
    }

    protected void grvTipoCartel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].CssClass = "ColumnaOculta";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                e.Row.Cells[2].ControlStyle.CssClass = "ColumnaOculta";
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
   }

    protected void btnBuscarT_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtNomCartelB.ClientID);
        Util.RegisterScript(upnlCarteles, "__EnfocarCtrol__", sbScript.ToString());
    }

    private void FiltrarProducto()
    {
        if (Session["dtRegCartelPV"] == null)
        {
            return;
        }

        ((DataTable)Session["dtRegCartelPV"]).Clear();
        Session["dtRegCartelPV"] = CARTEL_BL.Listar();

        if (((DataTable)Session["dtRegCartelPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegCartelPV"]).DefaultView.RowFilter = "[CARTEL] LIKE '%" + txtNomCartelB.Text.Trim() + "%'";
            this.grvTipoCartel.DataSource = ((DataTable)Session["dtRegCartelPV"]).DefaultView;
            this.grvTipoCartel.DataBind();
        }
    }

    protected void grvTipoCartel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar1");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void grvTipoCartel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Session["operacion"] = false;

        try
        {
            int index = Convert.ToInt32(e.CommandArgument);           
   
            int id = Convert.ToInt32(grvTipoCartel.DataKeys[index].Value);
            hidId.Value = Convert.ToString(id);

            if (e.CommandName == "Editar")
            {
                txtDescripcion.Text = Convert.ToString(grvTipoCartel.Rows[index].Cells[3].Text);
                CARTEL_BE = CARTEL_BL.ObtenerPorID(Convert.ToInt32(hidId.Value));               
                chkCeroDigitos.Checked = Convert.ToBoolean(CARTEL_BE.CERO_DIGITOS);              

                grvModelos.DataSource = CARTEL_BE.MODELOS;
                grvModelos.DataBind();

                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", txtDescripcion.ClientID);

                if (CTieneCM() == 0)
                {
                    sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", chkCeroDigitos.ClientID);
                }
                else if (CTieneCM()> 0)
                {
                    sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", chkCeroDigitos.ClientID);
                }

                pnlDetalle.Visible = true;
                Util.RegisterScript(upnlCarteles, "__DesabilitaCtrol__", sbScript.ToString());

            }
            else if (e.CommandName == "Eliminar")
            {
                if (CARTEL_BL.Eliminar(Convert.ToInt32(hidId.Value)) == false)
                {
                    LoadData();

                    Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", Resources.Mensajes.msgCartelAlertEliminacion);
                }
                else
                {
                    Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", Resources.Mensajes.msgCartelAlertIR);
                }
            }
        }
        catch
        { 
        
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Session["operacion"] = true;

        // Llena la grilla de modelos con todos los CheckBoxes vacios
        grvModelos.DataSource = MODELO_BL.Listar();
        grvModelos.DataBind();

        pnlDetalle.Visible = true;

        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtDescripcion.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').value='';", hidId.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", txtDescripcion.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", chkCeroDigitos.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').checked=false;", chkCeroDigitos.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID);
        Util.RegisterScript(upnlCarteles, "__DesabilitaCtrol__", sbScript.ToString());
   }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        int numMaxDigitos = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NroMaxDigitos"]);

        List<SCE_CARTEL_MODELO_BE> lstModelos = new List<SCE_CARTEL_MODELO_BE>();

        if (hidId.Value == "")
        {
            strMsg = CARTEL_BL.IsValid(0,
                                       txtDescripcion.Text.Trim().ToUpper(),
                                       (bool)Session["operacion"]);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", strMsg);
                return;
            }

            foreach (GridViewRow Row in grvModelos.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                CARTEL_MODELO_BE = new SCE_CARTEL_MODELO_BE();

                CheckBox check = Row.FindControl("chkSelection") as CheckBox;

                if (check.Checked)
                {
                    CARTEL_MODELO_BE.ID_MODELO = Convert.ToInt32(grvModelos.Rows[i].Cells[0].Text);                    
                    lstModelos.Add(CARTEL_MODELO_BE);
                }
            }

            CARTEL_BL.Insertar(txtDescripcion.Text.Trim().ToUpper(),
                               lstModelos,
                               numMaxDigitos,
                               chkCeroDigitos.Checked);

            //Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", Resources.Mensajes.msgCartelAlertEjecución);
        }
        else
        {
            strMsg = CARTEL_BL.IsValid(Convert.ToInt32(hidId.Value), 
                                       txtDescripcion.Text.Trim().ToUpper(),
                                       (bool)Session["operacion"]);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", strMsg);
                return;
            }

            foreach (GridViewRow Row in grvModelos.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);
                CARTEL_MODELO_BE = new SCE_CARTEL_MODELO_BE();
                CARTEL_MODELO_BE.ID_MODELO = Convert.ToInt32(grvModelos.Rows[i].Cells[0].Text);
                CheckBox check = Row.FindControl("chkSelection") as CheckBox;

                if (check.Checked)
                {
                    CARTEL_MODELO_BE.FLAGPERTENECE = 1;
                }
                else
                {
                    CARTEL_MODELO_BE.FLAGPERTENECE = 0;
                }

                lstModelos.Add(CARTEL_MODELO_BE);        
            }

            CARTEL_BL.Actualizar(Convert.ToInt32(hidId.Value),
                                 txtDescripcion.Text.Trim().ToUpper(),
                                 lstModelos,
                                 numMaxDigitos,
                                 chkCeroDigitos.Checked);

            //Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", Resources.Mensajes.msgCartelAlertModificacion);
        }

        pnlDetalle.Visible = false;

        LoadData();

        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtNomCartelB.ClientID);
        Util.RegisterScript(upnlCarteles, "__DesabilitaCtrol__", sbScript.ToString());        
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pnlDetalle.Visible = false;
        Session["operacion"] = true;
    }   

    protected void chkSelection_CheckedChanged(object sender, EventArgs e)
    {
        string strMsg;

        // cogemos el check seleccionado
        CheckBox chkPolSeleccionado = (CheckBox)sender;

        // guardamos la fila seleccionada
        GridViewRow _FilaPolseleccionada = (GridViewRow)chkPolSeleccionado.NamingContainer;

        // pasamos el indice denuevo de la fila seleccionada 
        grvModelos.SelectedIndex = _FilaPolseleccionada.RowIndex;

        hidModelo.Value = Convert.ToString(grvModelos.Rows[grvModelos.SelectedIndex].Cells[0].Text);

        if (hidId.Value == "")
        {
            strMsg = "";
        }
        else
        {
            // Valido la integridad referencial en la tabla relacionada del registro de la grilla que este chekeando 
            strMsg = CARTEL_BL.ValidarIntegridad(Convert.ToInt32(hidId.Value),
                                                 Convert.ToInt32(hidModelo.Value));

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlCarteles, "__Alerta__", strMsg);

            }
        }

        CheckBox myCheckBox;
        myCheckBox = new CheckBox();
        myCheckBox = (CheckBox)_FilaPolseleccionada.FindControl("chkSelection");

        if (strMsg.Length > 0)
        {
            // No permite hacer el cambio del estado del check si es que existe 
            // problemas de integridad referencial
            if (myCheckBox.Checked)
            {
                myCheckBox.Checked = false;
            }
            else
            {
                myCheckBox.Checked = true;
            }
        }
        else
        {
            strMsg = "";
        }
    }

    private int CTieneCM()
    {
        int ATCM = 0;

        foreach (GridViewRow Row in grvModelos.Rows)
        {
            int i = Convert.ToInt32(Row.RowIndex);           

            CheckBox check = Row.FindControl("chkSelection") as CheckBox;

            if (check.Checked)
            {
                ATCM = ATCM + 1;
            }           
        }

        return ATCM;
    }

    private void ScriptCliente()
    {
        if (!ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__"))
        {
            StringBuilder sScript = new StringBuilder();

            sScript.AppendLine("");

            sScript.AppendLine("function IsValid()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtDescripcion.ClientID);
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCartelAlertNombNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCartelLongitudNoPermt);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return confirm(msg);");
            sScript.AppendLine("}");

            sScript.AppendLine("function confirmacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            sScript.AppendLine("function confirmEliminacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }
    
}
