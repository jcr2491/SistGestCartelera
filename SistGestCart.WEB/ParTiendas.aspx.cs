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
using System.Text;
using System.IO;

public partial class ParTiendas : MyBasePage
{
    SCE_TIENDA_BL TIENDA_BL;

    private void LoadData()
    {
        Session["dtRegTiendaPV"] = TIENDA_BL.Listar();
        this.grvTienda.DataSource = Session["dtRegTiendaPV"];
        this.grvTienda.DataBind();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        TIENDA_BL = new SCE_TIENDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        txtId.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");
        txtDescripcion.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");

        txtNomTienda.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        btnAgregar.OnClientClick = "return Agregar();";
        btnGuardar.OnClientClick = "return Limpiar();";
        btnCancelar.OnClientClick = "return Limpiar();";

        btnGuardar.OnClientClick = "return IsValid();";

        Util.SetEnterButton(txtNomTienda, btnBuscarT);

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ParTiendas.aspx";

            LoadData();
        }
    }

    protected void btnBuscarT_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtNomTienda.ClientID);
    }

    private void FiltrarProducto()
    {
        if (Session["dtRegTiendaPV"] == null)
        {
            return;
        }

        ((DataTable)Session["dtRegTiendaPV"]).Clear();
        Session["dtRegTiendaPV"] = TIENDA_BL.Listar();

        if (((DataTable)Session["dtRegTiendaPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegTiendaPV"]).DefaultView.RowFilter = "[NOM_TIENDA] LIKE '%" + txtNomTienda.Text.Trim() + "%'";
            this.grvTienda.DataSource = ((DataTable)Session["dtRegTiendaPV"]).DefaultView;
            this.grvTienda.DataBind();
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton img = (ImageButton)sender;
        //GridViewRow row = (GridViewRow)img.Parent.Parent;


        //        string sbScript = @"<script type='text/javascript'>
        //                                Editar1('" + Server.HtmlDecode(row.Cells[2].Text) + "'" + "," + "'" + Server.HtmlDecode(row.Cells[3].Text) + "');" +
        //                            "</script>";

        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "SelRowGrid", sbScript, false);

        //img.OnClientClick = String.Format("return Editar('{0}','{1}')", Server.HtmlDecode(row.Cells[2].Text), Server.HtmlDecode(row.Cells[3].Text));
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        if (TIENDA_BL.Eliminar(Convert.ToInt32(Server.HtmlDecode(row.Cells[2].Text))) == false)
        {
            LoadData();

            //Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", Resources.Mensajes.msgTiendaAlertEliminacion);
        }
        else
        {
            Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", Resources.Mensajes.msgTiendaAlertIR);
        }
    }

    protected void grvTienda_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgM = (ImageButton)e.Row.FindControl("btnEditar");
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgM.OnClientClick = string.Format("return Editar('{0}','{1}')", Server.HtmlDecode(e.Row.Cells[2].Text), Server.HtmlDecode(e.Row.Cells[3].Text));
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strMsg = "";

        if (hidId.Value == "")
        {
            strMsg = TIENDA_BL.IsValid(Convert.ToInt32(txtId.Text),
                                       txtDescripcion.Text.Trim().ToUpper(),
                                       true);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", strMsg);
                return;
            }

            TIENDA_BL.Insertar(Convert.ToInt32(txtId.Text),
                               txtDescripcion.Text.Trim().ToUpper());

            //Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", Resources.Mensajes.msgTiendaAlertEjecución);
        }
        else
        {
            strMsg = TIENDA_BL.IsValid(Convert.ToInt32(hidId.Value),
                                       txtDescripcion.Text.Trim().ToUpper(),
                                       false);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", strMsg);
                return;
            }

            TIENDA_BL.Actualizar(Convert.ToInt32(hidId.Value),
                                 txtDescripcion.Text.Trim().ToUpper());

            //Util.RegisterAsyncAlert(upnlTiendas, "__Alerta__", Resources.Mensajes.msgTiendaAlertModificacion);
        }

        LoadData();
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
            sScript.AppendFormat("  strId = document.getElementById('{0}').value;", txtId.ClientID);
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtDescripcion.ClientID);
            sScript.AppendLine("    if ((strId.length  == 0) || (strId == '0'))");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgTiendaAlertIdNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtId.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgTiendaAlertNombNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  > 100)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgTiendaLongitudNoPermt);
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

            sScript.AppendLine("function Agregar()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').style.visibility = 'visible';", pnlDetalle.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').disabled = false;", txtId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtDescripcion.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", hidId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').focus();", txtId.ClientID);
            sScript.AppendLine("    return false;");
            sScript.AppendLine("}");

            sScript.AppendLine("function Editar(idTienda,desTienda)");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').style.visibility = 'visible';", pnlDetalle.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value=idTienda;", hidId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value=idTienda;", txtId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').disabled = true;", txtId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value=desTienda;", txtDescripcion.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("    return false;");
            sScript.AppendLine("}");

            sScript.AppendLine("function Limpiar()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtDescripcion.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", hidId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').style.visibility = 'hidden';", pnlDetalle.ClientID);
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsTextBusqNull()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strCartelModelo = document.getElementById('{0}').value;", txtNomTienda.ClientID);
            sScript.AppendLine("    if (strCartelModelo.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCNomTiendaBusNull);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomTienda.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }
}
