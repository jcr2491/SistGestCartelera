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

public partial class ParCategoria : MyBasePage
{
    SCE_CATEGORIA_BL CATEGORIA_BL;

    private void LoadData()
    {
        Session["dtRegCategoriaPV"] = CATEGORIA_BL.Listar();
        this.grvCategorias.DataSource = Session["dtRegCategoriaPV"];
        this.grvCategorias.DataBind();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CATEGORIA_BL = new SCE_CATEGORIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        txtDescripcion.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");

        btnAgregar.OnClientClick = "return Agregar();";
        btnGuardar.OnClientClick = "return Limpiar();";
        btnCancelar.OnClientClick = "return Limpiar();";
        btnGuardar.OnClientClick = "return IsValid();";

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ParCategoria.aspx";

            LoadData();
        }
    }   
    
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;
        hidId.Value = Server.HtmlDecode(row.Cells[1].Text);

        if (CATEGORIA_BL.Eliminar(Convert.ToInt32(hidId.Value)) == false)
        {
            LoadData();
        }
        else
        {
            Util.RegisterAsyncAlert(upnlCategorias, "__Alerta__", Resources.Mensajes.msgCategoriaAlertIR);
        }
    }

    protected void grvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgM = (ImageButton)e.Row.FindControl("btnEditar");
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strMsg = "";

        strMsg = CATEGORIA_BL.IsValid(0, txtDescripcion.Text.Trim().ToUpper());

        if (strMsg.Length > 0)
        {
            Util.RegisterAsyncAlert(upnlCategorias, "__Alerta__", strMsg);
            return;
        }

        CATEGORIA_BL.Insertar(txtDescripcion.Text.Trim().ToUpper());

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
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtDescripcion.ClientID);
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCategoriaAlertNombNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  > 100)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCategoriaLongitudNoPermt);
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
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtDescripcion.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", hidId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("    return false;");
            sScript.AppendLine("}");          

            sScript.AppendLine("function Limpiar()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtDescripcion.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", hidId.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').style.visibility = 'hidden';", pnlDetalle.ClientID);
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }    
}
