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
using pe.oechsle.Entity;
using pe.oechsle.sca;
using pe.oechsle.Ex;
using System.Text;
using System.IO;

public partial class SegCambioClave : MyBasePage
{
    private pe.oechsle.Entity.Usuario usr;

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnGrabar.OnClientClick = "return IsValid();";

        if (!Page.IsPostBack)
        {
            String usuario;

            Session["Formulario"] = "SegCambioClave.aspx";

            if (Request.Form["txtLogin"] == null && Session["login"] == null)
            {
                Server.Transfer("SegLogin.aspx");
            }

            if (Session["login"] == null)
            {
                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                sbScript.AppendFormat("document.getElementById('{0}').checked=true;", chkFlagSeguridad.ClientID);
                Util.RegisterScript(upnlCambioPassw, "__Chekear__", sbScript.ToString());

                Util.RegisterAsyncAlert(upnlCambioPassw, "__Alerta__", "Su clave ha caducado, debe cambiarla");

                usuario = Request.Form["txtLogin"];
            }
            else
            {
                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                sbScript.AppendFormat("document.getElementById('{0}').checked=false;", chkFlagSeguridad.ClientID);
                sbScript.AppendFormat("document.getElementById('{0}').innerHTML='';", lblMsg.ClientID);
                Util.RegisterScript(upnlCambioPassw, "__Limpiar__", sbScript.ToString());

                pe.oechsle.Entity.Usuario usr = (pe.oechsle.Entity.Usuario)Session["login"];
                usuario = usr.login;
            }

            hidUserLogin.Value = usuario;
        }
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        String instancia = ConfigurationManager.AppSettings.Get("instancia");
        String user_bd = ConfigurationManager.AppSettings.Get("user_bd");
        String passw_bd = ConfigurationManager.AppSettings.Get("passw_bd");
        String abrevSis = ConfigurationManager.AppSettings.Get("abrevSis");

        byte[] bInst = Seguridad.hexToByte(instancia, "-");
        byte[] bUsBd = Seguridad.hexToByte(user_bd, "-");
        byte[] bPwBD = Seguridad.hexToByte(passw_bd, "-");

        pe.oechsle.sca.Seguridad objSegur = new pe.oechsle.sca.Seguridad(bInst, bUsBd, bPwBD);

        objSegur.host_ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        objSegur.server_ip = Request.ServerVariables["SERVER_NAME"].ToString();

        if (txtClaveActual.Text.Trim() == txtNewClave.Text.Trim())
        {
            string script = @"<script type='text/javascript'>
                                    alert('La clave actual y la nueva clave no pueden ser iguales');" +
                                "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

            return;
        }

        if (txtNewClave.Text.Trim() != txtConfNewClave.Text.Trim())
        {
            string script = @"<script type='text/javascript'>
                                    alert('La confirmacion de la nueva clave es diferente a la nueva clave');" +
                                "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

            return;
        }

        try
        {
            if (chkFlagSeguridad.Checked)
            {
                pe.oechsle.Entity.Usuario objUser = objSegur.login(abrevSis, hidUserLogin.Value, txtClaveActual.Text, txtNewClave.Text);
                Session["login"] = objUser;
                Response.Redirect("SegLogin.aspx");
            }
            else
            {
                objSegur.changePassword(hidUserLogin.Value, txtClaveActual.Text, txtNewClave.Text);
                Response.Redirect("Default.aspx");
            }
        }
        catch (LoginException ex)
        {
            Util.RegisterAsyncAlert(upnlCambioPassw, "__Alerta__", "Error: " + ex.Message);
        }
        catch (PasswordExpiredException ex)
        {
            Util.RegisterAsyncAlert(upnlCambioPassw, "__Alerta__", "Error: " + ex.Message);
            Response.Redirect("SegCambioClave1ra.aspx", true);
        }
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
            sScript.AppendFormat("  strClaveActual = document.getElementById('{0}').value;", txtClaveActual.ClientID);
            sScript.AppendFormat("  strNewClave = document.getElementById('{0}').value;", txtNewClave.ClientID);
            sScript.AppendFormat("  strConfNewClave = document.getElementById('{0}').value;", txtConfNewClave.ClientID);
            sScript.AppendLine("    if (strClaveActual.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", "No ha colocado la clave actual");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtClaveActual.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNewClave.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", "No ha colocado la nueva clave");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNewClave.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strConfNewClave.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", "No ha confirmado la nueva clave");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtConfNewClave.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return confirm(msg);");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }

}
