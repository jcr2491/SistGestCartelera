using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using pe.oechsle.sca;
using pe.oechsle.Entity;
using pe.oechsle.Ex;
using pe.oechsle.ex.Entity;
using System.Text;
using System.IO;
using SistGestCart.BL;

public partial class SegLogin : System.Web.UI.Page
{
    //SCE_GUIA_BL GUIA_BL;

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnAceptar.OnClientClick = "return IsValid();";

        Session["Formulario"] = "SegLogin.aspx";

        ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "scriptLogin", "if (top.location.href != self.location.href) top.location.reload();", true);
    }

    protected List<Local> locales = new List<Local>();

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        String instancia = ConfigurationManager.AppSettings.Get("instancia");
        String user_bd = ConfigurationManager.AppSettings.Get("user_bd");
        String passw_bd = ConfigurationManager.AppSettings.Get("passw_bd");
        String abrevSis = ConfigurationManager.AppSettings.Get("abrevSis");

        byte[] bInst = Seguridad.hexToByte(instancia, "-");
        byte[] bUsBd = Seguridad.hexToByte(user_bd, "-");
        byte[] bPwBD = Seguridad.hexToByte(passw_bd, "-");

        Seguridad objSegur = new Seguridad(bInst, bUsBd, bPwBD);

        objSegur.host_ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        objSegur.server_ip = Request.ServerVariables["SERVER_NAME"].ToString();

        try
        {
            pe.oechsle.Entity.Usuario objUser = objSegur.login(abrevSis, txtLogin.Text, txtClave.Text);

            Session["login"] = objUser;
            Session["loginUser"] = objUser.login;

            //* ***************************************************
            // * Variable de Session que guarda los datos de la 
            // * cadena de conexion del sistema de carteleria
            // * ***************************************************/
            Session["DatosCnSistema"] = (Usuario)Session["login"];
            //* ***************************************************/

            //GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

            //if (GUIA_BL.ExisteUsuarioLogueado(Convert.ToString(Session["loginUser"])) == 0)
            //{
            //    throw new LoginException("El usuario ya esta logueado en sistema. Por favor intente con uno diferente que no este logueado actualmente.");
            //}
            //else
            //{
                if (objUser.TablasTipo.Count == 0)
                {
                    throw new LoginException("Usuario no asignado a ningun Tipo de Tabla.");
                }
                else if (objUser.TablasTipo.Count >= 1)
                {
                    foreach (pe.oechsle.ex.Entity.TablaTipo tabla in objUser.TablasTipo)
                    {
                        if (tabla.abreviatura == "SUCS")
                        {
                            if (tabla.Detalles.Count == 0)
                            {
                                throw new LoginException("Usuario no asignado a ninguna Tienda.");
                            }
                            else if (tabla.Detalles.Count > 1)
                            {
                                throw new LoginException("Usuario asignado a más de una Tienda.");
                            }
                            else if (tabla.Detalles.Count == 1)
                            {
                                //GUIA_BL.InsertarUsuarioEnTablaCtrol(Convert.ToString(Session["loginUser"]));

                                /*************************************
                                 * 38 - PUBLICISTA CENTRAL           *
                                 * 39 - JEFE DE SECCION              *
                                 * 41 - ADMIN CARTELERIA             *
                                **************************************/
                                //Session["perfil"] = objUser.Tipo.codigo;
                                //Session["dperfil"] = objUser.Tipo.descripcion;
                                /*************************************/

                                Server.Transfer("Default.aspx");
                            }
                        }
                    }
                }
            //}
        }
        catch (LoginException ex)
        {
            string script = @"<script type='text/javascript'>
                                alert('Error: " + ex.Message + "');" +
                           "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }
        catch (PasswordExpiredException ex)
        {
            string script = @"<script type='text/javascript'>
                                alert('Error: " + ex.Message + "');" +
                           "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

            Server.Transfer("SegCambioClave1ra.aspx", true);
        }      
        catch
        {
            string script = @"<script type='text/javascript'>
                                alert('Error al intentar establecer conexion con el sistema Comuniquese con el area de sistemas');" +
                           "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
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
            sScript.AppendFormat("  strLogin = document.getElementById('{0}').value;", txtLogin.ClientID);
            sScript.AppendFormat("  strClave = document.getElementById('{0}').value;", txtClave.ClientID);
            sScript.AppendLine("    if (strLogin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", "No ha colocado el login del usuario");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtLogin.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strClave.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", "No ha colocado la clave de acceso");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtClave.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return true;");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }
}