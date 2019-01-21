using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistGestCart.BE;
using SistGestCart.BL;
using System.Text;
using System.IO;

public partial class ConfReglCartelCamp : MyBasePage
{
    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE = new SCE_CARTEL_MODELO_BE();
    SCE_CAMPO_BL CAMPO_BL;
    SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO_BE;

    private void GetData()
    {
        Session["dtRegCMPV"] = CARTEL_MODELO_BL.ListarPV();
        grvListCartelCampo.DataSource = Session["dtRegCMPV"];
        grvListCartelCampo.DataBind();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CAMPO_BL = new SCE_CAMPO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        txtDescCartMod.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        btnGuardar.OnClientClick = string.Format("return confirmacion('{0}');", Resources.Mensajes.msgConfirmación);

        Util.SetEnterButton(txtDescCartMod, btnBuscarDCM);

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ConfReglCartelCamp.aspx";
            GetData();
        }

        Session["operacion"] = true;
    }

    protected void btnBuscarDCM_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtDescCartMod.ClientID);
        Util.RegisterScript(upnlReglasCamp, "__EnfocarCtrol__", sbScript.ToString());
    }

    private void FiltrarProducto()
    {
        ((DataTable)Session["dtRegCMPV"]).Clear();
        Session["dtRegCMPV"] = CARTEL_MODELO_BL.ListarPV();

        if (((DataTable)Session["dtRegCMPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegCMPV"]).DefaultView.RowFilter = "[DESCRIPCION] LIKE '%" + txtDescCartMod.Text.Trim() + "%'";
            this.grvListCartelCampo.DataSource = ((DataTable)Session["dtRegCMPV"]).DefaultView;
            this.grvListCartelCampo.DataBind();
        }
    }

    protected void grvListCartelCampo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].CssClass = "ColumnaOculta";
            e.Row.Cells[2].CssClass = "ColumnaOculta";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                e.Row.Cells[1].ControlStyle.CssClass = "ColumnaOculta";
                e.Row.Cells[2].ControlStyle.CssClass = "ColumnaOculta";

                e.Row.Cells[3].ControlStyle.Width = 200;

                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
    }

    protected void grvListCartelCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
      
        int IdCartel = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[0].ToString());
        hidIdCartel.Value = Convert.ToString(IdCartel);

        int IdModelo = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[1].ToString());
        hidIdModelo.Value = Convert.ToString(IdModelo);

        if (e.CommandName == "Editar")
        {
            Session["operacion"] = false;

            pnlDetalle.Visible = true;

            System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
            sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", txtCartelModelo.ClientID);
            Util.RegisterScript(upnlReglasCamp, "__DesabilitaCtrol__", sbScript.ToString());

            CARTEL_MODELO_BE = CARTEL_MODELO_BL.ObtenerPorID(Convert.ToInt32(hidIdCartel.Value), Convert.ToInt32(hidIdModelo.Value));
            txtCartelModelo.Text = CARTEL_MODELO_BE.DESCRIPCION;

            grvCampos.DataSource = CARTEL_MODELO_BE.CAMPOS;
            grvCampos.DataBind();
        }
    }    

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int numMaxDigitos = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NroMaxDigitos"]);
        List<SCE_CARTEL_MODELO_CAMPO_BE> lstCartelModeloCampo = new List<SCE_CARTEL_MODELO_CAMPO_BE>();

        foreach (GridViewRow Row in grvCampos.Rows)
        {
            int i = Convert.ToInt32(Row.RowIndex);

            CARTEL_MODELO_CAMPO_BE = new SCE_CARTEL_MODELO_CAMPO_BE();

            CheckBox check = Row.FindControl("chkSelection") as CheckBox;

            if (check.Checked)
            {
                CARTEL_MODELO_CAMPO_BE.ID_CARTEL = Convert.ToInt32(hidIdCartel.Value);
                CARTEL_MODELO_CAMPO_BE.ID_MODELO = Convert.ToInt32(hidIdModelo.Value);
                CARTEL_MODELO_CAMPO_BE.ID_CAMPO = Convert.ToInt32(grvCampos.Rows[i].Cells[0].Text);
                lstCartelModeloCampo.Add(CARTEL_MODELO_CAMPO_BE);
            }
        }

        CARTEL_MODELO_BL.ActualizarCMC(Convert.ToInt32(hidIdCartel.Value),
                                       Convert.ToInt32(hidIdModelo.Value),
                                       lstCartelModeloCampo,
                                       numMaxDigitos);

        //Util.RegisterAsyncAlert(upnlReglasCamp, "__Alerta__", Resources.Mensajes.msgCRCCAlertEjecución);    

        pnlDetalle.Visible = false;

        GetData();

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        
        //System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        //sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", txtCartelModelo.ClientID);
        //sbScript.AppendFormat("document.getElementById('{0}').value='';", hidIdCartel.ClientID);
        //sbScript.AppendFormat("document.getElementById('{0}').value='';", hidIdModelo.ClientID);
        //sbScript.AppendFormat("document.getElementById('{0}').value='';", txtDescCartMod.ClientID);
        //Util.RegisterScript(upnlReglasCamp, "__DesHabilitaCtrol__", sbScript.ToString());

        txtCartelModelo.Enabled = false;
        hidIdCartel.Value = "";
        hidIdModelo.Value = "";
        txtDescCartMod.Text = "";
        pnlDetalle.Visible = false;
    }

    protected void chkSelection_CheckedChanged(object sender, EventArgs e)
    {
        string strMsg;

        // cogemos el check seleccionado
        CheckBox chkPolSeleccionado = (CheckBox)sender;

        // guardamos la fila seleccionada
        GridViewRow _FilaPolseleccionada = (GridViewRow)chkPolSeleccionado.NamingContainer;

        // pasamos el indice denuevo de la fila seleccionada 
        grvCampos.SelectedIndex = _FilaPolseleccionada.RowIndex;

        hidCampo.Value = Convert.ToString(grvCampos.Rows[grvCampos.SelectedIndex].Cells[0].Text);

        if (hidCampo.Value == "")
        {
            strMsg = "";
        }
        else
        {
            // Valido la integridad referencial en la tabla relacionada del registro de la grilla que este chekeando 
            strMsg = CAMPO_BL.EsCampoValidable(Convert.ToInt32(hidIdCartel.Value),
                                               Convert.ToInt32(hidCampo.Value));

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlReglasCamp, "__Alerta__", strMsg);

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

        txtCartelModelo.Enabled = false;
    }   

    private void ScriptCliente()
    {
        if (!ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__"))
        {
            StringBuilder sScript = new StringBuilder();

            sScript.AppendLine("");

            sScript.AppendLine("function confirmacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsTextBusqNull()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strCartelModelo = document.getElementById('{0}').value;", txtDescCartMod.ClientID);
            sScript.AppendLine("    if (strCartelModelo.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCNoSelCartModelo);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescCartMod.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }

   
}
