using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistGestCart.BL;
using SistGestCart.BE;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Diagnostics;


public partial class ConfConfigurCartel : MyBasePage
{
    //public const int LOGON32_LOGON_INTERACTIVE = 2;
    //public const int LOGON32_PROVIDER_DEFAULT = 0;

    //WindowsImpersonationContext impersonationContext;

    //[DllImport("advapi32.dll")]
    //public static extern int LogonUserA(String lpszUserName,
    //    String lpszDomain,
    //    String lpszPassword,
    //    int dwLogonType,
    //    int dwLogonProvider,
    //    ref IntPtr phToken);

    //[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    //public static extern int DuplicateToken(IntPtr hToken,
    //    int impersonationLevel,
    //    ref IntPtr hNewToken);

    //[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    //public static extern bool RevertToSelf();

    //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    //public static extern bool CloseHandle(IntPtr handle);


    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE = new SCE_CARTEL_MODELO_BE();
    SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO_BE;
    SCE_CAMPO_BL CAMPO_BL;

    string sExt = String.Empty;

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    //private bool impersonateValidUser(String userName, String domain, String password)
    //{
    //    WindowsIdentity tempWindowsIdentity;
    //    IntPtr token = IntPtr.Zero;
    //    IntPtr tokenDuplicate = IntPtr.Zero;

    //    if (RevertToSelf())
    //    {
    //        if (LogonUserA(userName, 
    //                       domain, 
    //                       password, 
    //                       LOGON32_LOGON_INTERACTIVE,
    //                       LOGON32_PROVIDER_DEFAULT, 
    //                       ref token) != 0)
    //        {
    //            if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
    //            {
    //                tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
    //                impersonationContext = tempWindowsIdentity.Impersonate();
    //                if (impersonationContext != null)
    //                {
    //                    CloseHandle(token);
    //                    CloseHandle(tokenDuplicate);
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    if (token != IntPtr.Zero)
    //        CloseHandle(token);
    //    if (tokenDuplicate != IntPtr.Zero)
    //        CloseHandle(tokenDuplicate);
    //    return false;
    //}

    //private void undoImpersonation()
    //{
    //    impersonationContext.Undo();
    //}

    private void CargaCartelesModelo()
    {
        Session["dtRegCM"] = CARTEL_MODELO_BL.ListarCMConf();
        this.grvCartelModelo.DataSource = Session["dtRegCM"];
        this.grvCartelModelo.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CAMPO_BL = new SCE_CAMPO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        btnImportar.OnClientClick = string.Format("return confirmacion('{0}');", Resources.Mensajes.msgConfirmación);
        
        txtDescCartMod.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));
        
        Util.SetEnterButton(txtDescCartMod, btnBuscarDCM);

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ConfConfigurCartel.aspx";

            CargaCartelesModelo();
        }
    }

    protected void btnBuscarDCM_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtDescCartMod.ClientID);
        Util.RegisterScript(upnlBusqueda, "__EnfocarCtrol__", sbScript.ToString());
    }

    private void FiltrarProducto()
    {
        ((DataTable)Session["dtRegCM"]).Clear();
        Session["dtRegCM"] = CARTEL_MODELO_BL.ListarCMConf();

        if (((DataTable)Session["dtRegCM"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegCM"]).DefaultView.RowFilter = "[DESCRIPCION] LIKE '%" + txtDescCartMod.Text.Trim() + "%'";
            this.grvCartelModelo.DataSource = Session["dtRegCM"];
            this.grvCartelModelo.DataBind();
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();

        hidIdCartel.Value = Server.HtmlDecode(row.Cells[1].Text);
        hidIdModelo.Value = Server.HtmlDecode(row.Cells[2].Text);
        hidDigitos.Value = Server.HtmlDecode(row.Cells[3].Text);
        hidNom.Value = Server.HtmlDecode(row.Cells[4].Text) + " DE " + Server.HtmlDecode(row.Cells[5].Text);
        hidCartelModelo.Value = Server.HtmlDecode(row.Cells[4].Text);
        hidNroDigitos.Value = Server.HtmlDecode(row.Cells[5].Text);

        if (CARTEL_MODELO_BL.ExistePlantilla(Convert.ToInt32(hidIdCartel.Value),
                                             Convert.ToInt32(hidIdModelo.Value),
                                             Convert.ToInt32(hidDigitos.Value)) == true)
        {
            CARTEL_MODELO_BE = CARTEL_MODELO_BL.ObtenerPorID1(Convert.ToInt32(hidIdCartel.Value),
                                                              Convert.ToInt32(hidIdModelo.Value),
                                                              Convert.ToInt32(hidDigitos.Value));

            if (CARTEL_MODELO_BE != null)
            {
                lblCartel.Text = CARTEL_MODELO_BE.DESCRIPCION.ToString().Trim();
                lblDigitos.Text = CARTEL_MODELO_BE.NRODIGITOS.ToString().Trim();
                lblPlantilla.Text = CARTEL_MODELO_BE.NOM_PLANTILLA.ToString().Trim();

                grvCampos.DataSource = CARTEL_MODELO_BE.CAMPOS;
                grvCampos.DataBind();

                sbScript.AppendFormat("document.getElementById('{0}').style.visibility='visible';", btnVerPlantilla.ClientID);

                pnlBtnConfigurar.Visible = false;
                pnlDialgPlantilla.Visible = false;
                pnlDetalle.Visible = true;
            }
            else
            {
                Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_NEP"] + " " + Convert.ToString(hidNom.Value));

                sbScript.AppendFormat("document.getElementById('{0}').style.visibility='hidden';", btnVerPlantilla.ClientID);

                pnlBtnConfigurar.Visible = false;
                pnlDialgPlantilla.Visible = false;
                pnlDetalle.Visible = false;
            }

            upnlDetalleConf.Update();
            upnlConfigurar.Update();
        }
        else
        {        
            upnlDetalleConf.Update();
            upnlConfigurar.Update();
            upnlImportador.Update();

            Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_NEP"] + " " + Convert.ToString(hidNom.Value));

            pnlBtnConfigurar.Visible = true;
            pnlDialgPlantilla.Visible = false;
            pnlDetalle.Visible = false;           
        }

        lblCartelPrint.Text = hidNom.Value;

        Util.RegisterScript(upnlConfigurar, "__DesabilitaCtrol__", sbScript.ToString());
    }

    protected void btnConfigurarNP_Click(object sender, EventArgs e)
    {
        pnlBtnConfigurar.Visible = false;
        pnlDialgPlantilla.Visible = true;       

        upnlImportador.Update();
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();

        List<SCE_CARTEL_MODELO_CAMPO_BE> lstcmc = new List<SCE_CARTEL_MODELO_CAMPO_BE>();

        if (fuImportPlantillas.HasFile)
        {
            Session["sName"] = System.IO.Path.GetFileNameWithoutExtension(fuImportPlantillas.FileName);
            sExt = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FILE_EXTENCION"];
            Session["sPath"] = System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"] + Session["sName"] + sExt;

            if (ValidaExtension(sExt))
            {
                if (fuImportPlantillas.PostedFile.ContentLength > 4096)
                {
                    // Carga el archivo de la plantilla selecionada en el servidor
                    fuImportPlantillas.SaveAs((string)Session["sPath"]);

                    // Verificar si el archivo de la plantilla Excel tiene mas de una Hoja de Calculo
                    if (CARTEL_MODELO_BL.HaveMoreOneSheetExcel((string)Session["sPath"]) == true)
                    {
                        Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", "La plantilla ha sido mal configurada porque tiene mas de una pestaña en esta. Por favor eliminar las pestañas adicionales y quede solo la que contiene el diseño de la plantilla y volver a reconfigurarla en el sistema.");

                        return;
                    }

                    // Obtiene los resultados de comparar los campos del Excel con los campos de la BD
                    bool errores = false;
                    string PosFCX = string.Empty;
                    string PosFCY = string.Empty;

                    // Llena la grilla con el informe de la comparación del Excel y la BD
                    grvCampos.DataSource = CARTEL_MODELO_BL.GetInforme((string)Session["sPath"],
                                                                       Convert.ToInt32(hidIdCartel.Value),
                                                                       Convert.ToInt32(hidIdModelo.Value),
                                                                       Convert.ToInt32(hidDigitos.Value),
                                                                       ref errores,
                                                                       ref PosFCX,
                                                                       ref PosFCY);

                    grvCampos.DataBind();

                    if (errores == true) // En caso de que haiga diferencias entre el Excel y la BD
                    {
                        lblCartel.Text = hidCartelModelo.Value.ToString();
                        lblDigitos.Text = hidNroDigitos.Value.ToString();
                        lblPlantilla.Text = Session["sName"].ToString();                        

                        Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_PNI"]);

                        upnlDetalleConf.Update();

                        sbScript.AppendFormat("document.getElementById('{0}').style.visibility='hidden';", btnVerPlantilla.ClientID);
                        Util.RegisterScript(upnlImportador, "__DesabilitaCtrol__", sbScript.ToString());

                        pnlBtnConfigurar.Visible = false;
                        pnlDialgPlantilla.Visible = false;
                        pnlDetalle.Visible = true;
                    }
                    else // En caso de que no haiga diferencias entre el Excel y la BD
                    {
                        // Convierte en PDF la plantilla Excel
                        CARTEL_MODELO_BL.ConvertExcelToPdf((string)Session["sPath"],
                                                           (System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"] + Session["sName"] + ".pdf"));

                        // Actualiza las tablas SCE_CARTEL_MODELO (NOM_PLANTILLA) y SCE_CARTEL_MODELO_CAMPO (Campos POS X y POS Y)
                        foreach (GridViewRow Row in grvCampos.Rows)
                        {
                            int i = Convert.ToInt32(Row.RowIndex);

                            CARTEL_MODELO_CAMPO_BE = new SCE_CARTEL_MODELO_CAMPO_BE();

                            CARTEL_MODELO_CAMPO_BE.ID_CAMPO = Convert.ToInt32(CAMPO_BL.GetIdCampo(Server.HtmlDecode(grvCampos.Rows[i].Cells[3].Text)));
                            CARTEL_MODELO_CAMPO_BE.POSX = Server.HtmlDecode(grvCampos.Rows[i].Cells[1].Text);
                            CARTEL_MODELO_CAMPO_BE.POSY = Server.HtmlDecode(grvCampos.Rows[i].Cells[2].Text);
                            lstcmc.Add(CARTEL_MODELO_CAMPO_BE);
                        }

                        // Actualiza la tabla SCE_CARTEL_MODELO (Campo NOM_PLANTILLA)
                        CARTEL_MODELO_BL.ActualizarCMC1(Convert.ToInt32(hidIdCartel.Value),
                                                        Convert.ToInt32(hidIdModelo.Value),
                                                        Convert.ToInt32(hidDigitos.Value),
                                                        Session["sName"].ToString(),
                                                        lstcmc,
                                                        PosFCX,
                                                        PosFCY);

                        // Llena el objeto con los resultados de la importacion realizada correctamente
                        CARTEL_MODELO_BE = CARTEL_MODELO_BL.ObtenerPorID1(Convert.ToInt32(hidIdCartel.Value),
                                                                          Convert.ToInt32(hidIdModelo.Value),
                                                                          Convert.ToInt32(hidDigitos.Value));                        
                        
                        // Llena los labeles y la grilla con los campos que estan en la BD o NO
                        lblCartel.Text = CARTEL_MODELO_BE.DESCRIPCION.ToString().Trim();
                        lblDigitos.Text = CARTEL_MODELO_BE.NRODIGITOS.ToString().Trim();
                        lblPlantilla.Text = CARTEL_MODELO_BE.NOM_PLANTILLA.ToString().Trim();

                        /***********************************************************************************/
                        /*COPIA LA PLANTILLA EXCEL AL FILE SERVER POR SUPLANTACION DE WINDOWS*/
                        /***********************************************************************************/
                        //string strPathServer = System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"];
                        //string strPathFs = System.Configuration.ConfigurationManager.AppSettings["PATH_PLANTILLAS_FS"];
                        
                        //string username = System.Configuration.ConfigurationManager.AppSettings["LOGIN_FS"];
                        //string password = System.Configuration.ConfigurationManager.AppSettings["PASSWORD_FS"];
                        //string domain = System.Configuration.ConfigurationManager.AppSettings["DOMINIO_FS"];

                        /***********************************************************************************/
                        /***********************************************************************************/
                        
                        //// Create Impersonation Object
                        //WI impersonation = new WI(domain, username, password);

                        //// Start Impersonation
                        //impersonation.Impersonate();

                        ///*Copiar la plantilla Excel Generada en la servidor.*/
                        //FileInfo file = new FileInfo(Convert.ToString(Session["sPath"]));
                        //file.CopyTo(strPathFs + Session["sName"].ToString() + sExt, true);

                        //// Stop Impersonation
                        //impersonation.Revert();
                        
                        /***********************************************************************************/
                        /***********************************************************************************/

                        //System.Security.Principal.WindowsImpersonationContext impersonationContext;
                        //impersonationContext = ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();

                        ///*Copiar la plantilla Excel Generada en la servidor.*/
                        //FileInfo file = new FileInfo(Session["sPath"].ToString());
                        //file.CopyTo(strPathFs + Session["sName"].ToString() + sExt, true);

                        //impersonationContext.Undo();

                        /***********************************************************************************/
                        /***********************************************************************************/

                        //if (impersonateValidUser(username, domain, password))
                        //{
                        //    //Insert your code that runs under the security context of the authenticating user here.
                        //    /*Copiar la plantilla Excel Generada en la servidor.*/
                        //    FileInfo file = new FileInfo(Convert.ToString(Session["sPath"]));
                        //    file.CopyTo(strPathFs + Session["sName"].ToString() + sExt, true);

                        //    undoImpersonation();
                        //}
                        //else
                        //{
                        //    //Your impersonation failed. Therefore, include a fail-safe mechanism here.
                        //}

                        /***********************************************************************************/
                        /***********************************************************************************/




                        /***********************************************************************************/
                        /***********************************************************************************/

                        // Llena la grilla de informe de importación
                        grvCampos.DataSource = CARTEL_MODELO_BE.CAMPOS;
                        grvCampos.DataBind();           
                        
                        Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_PIC"]);

                        upnlDetalleConf.Update();

                        sbScript.AppendFormat("document.getElementById('{0}').style.visibility='visible';", btnVerPlantilla.ClientID);
                        Util.RegisterScript(upnlImportador, "__DesabilitaCtrol__", sbScript.ToString());

                        /************************/
                        CargaCartelesModelo();
                        upnlBusqueda.Update();
                        /************************/

                        pnlBtnConfigurar.Visible = false;
                        pnlDialgPlantilla.Visible = false;
                        pnlDetalle.Visible = true;                         
                    }
                }
                else
                {
                    Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", "El archivo exede el tamaño permitido");

                    pnlBtnConfigurar.Visible = false;
                    pnlDialgPlantilla.Visible = true;

                    upnlImportador.Update();
                }
            }
            else
            {
                Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", "El archivo no es de tipo Excel");

                pnlBtnConfigurar.Visible = false;
                pnlDialgPlantilla.Visible = true;

                upnlImportador.Update();
            }
        }
        else
        {
            Util.RegisterAsyncAlert(upnlImportador, "__Alerta__", "No ha seleccionado el archivo");

            pnlBtnConfigurar.Visible = false;
            pnlDialgPlantilla.Visible = true;

            upnlImportador.Update();
        }       
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pnlDialgPlantilla.Visible = false;

        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtDescCartMod.ClientID);
        Util.RegisterScript(upnlImportador, "__DesabilitaCtrol__", sbScript.ToString());
        
        upnlBusqueda.Update();
        upnlConfigurar.Update();
    }

    protected void btnConfPlantilla_Click(object sender, EventArgs e)
    {
        pnlDetalle.Visible = false;
        pnlDialgPlantilla.Visible = true;        

        upnlImportador.Update();
    }

    protected void btnCancelRecf_Click(object sender, EventArgs e)
    {
        pnlDialgPlantilla.Visible = false;        
    }

    protected void btnVerPlantilla_Click(object sender, EventArgs e)
    {
        // ruta donde se hallan los PDFs (tal vez quieras incluir:
        string sPath = System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"];
        string sPathPlantillas = System.Configuration.ConfigurationManager.AppSettings["RutaPlantillas"];

        string strPlantilla = string.Empty;
        string strPlantilla1 = string.Empty;

        strPlantilla = sPath + CARTEL_MODELO_BL.ObtenerPlantilla(Convert.ToInt32(hidIdCartel.Value),
                                                                 Convert.ToInt32(hidIdModelo.Value),
                                                                 Convert.ToInt32(hidDigitos.Value)).ToString().Trim() + ".pdf";

        strPlantilla1 = sPathPlantillas + CARTEL_MODELO_BL.ObtenerPlantilla(Convert.ToInt32(hidIdCartel.Value),
                                                                            Convert.ToInt32(hidIdModelo.Value),
                                                                            Convert.ToInt32(hidDigitos.Value)).ToString().Trim() + ".pdf";

        if (CARTEL_MODELO_BL.ExistePlantillaPDF(strPlantilla) == false)
        {
            Util.RegisterAsyncAlert(upnlDetalleConf, "__Alerta__", "No ha generado correctamente la plantilla. Vuelva a configurarla.");
        }
        else
        {
            // Abre el documento PDF en un ventana de explorer distinta
            string script = @"<script type='text/javascript'>
                                window.open('" + strPlantilla1 + "', 'window','HEIGHT=800,WIDTH=1000,top=50,left=50,toolbar=yes,scrollbars=yes,toolbar=no,directories=no,status=yes,resizable=yes,copyhistory=no');" +
                             "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirPopUp", script, false);

            pnlDetalle.Visible = true;
        }
    }

    protected void btnCancelarConf_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtDescCartMod.ClientID);
        Util.RegisterScript(upnlImportador, "__DesabilitaCtrol__", sbScript.ToString());

        pnlDialgPlantilla.Visible = false;
        pnlDetalle.Visible = false;       

        CargaCartelesModelo();        

        upnlBusqueda.Update();
    }

    private bool ValidaExtension(string sExtension)
    {
        if (sExtension == ".xls" || sExtension == ".xlsx")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ScriptCliente()
    {
        if (!ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__"))
        {
            StringBuilder sScript = new StringBuilder();

            sScript.AppendLine("");

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

            sScript.AppendLine("function confirmacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }
}
