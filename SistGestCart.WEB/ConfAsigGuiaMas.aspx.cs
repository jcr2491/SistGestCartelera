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
using SistGestCart.BL;
using SistGestCart.BE;
using System.Text;
using System.IO;

public partial class ConfAsigGuiaMas : MyBasePage
{
    SCE_GUIA_BL GUIA_BL;
    SCE_FILE_GUIA_MASIVA_BL FILE_GUIA_MASIVA_BL;

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    private void LoadData()
    {
        Session["dtRegFileGuia"] = FILE_GUIA_MASIVA_BL.ListarFilesGuia();
        this.grvFilesGuia.DataSource = Session["dtRegFileGuia"];
        this.grvFilesGuia.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        FILE_GUIA_MASIVA_BL = new SCE_FILE_GUIA_MASIVA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        txtNomFile.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ConfAsigGuiaMas.aspx";

            LoadData();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarGuiasMasivas();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtNomFile.ClientID);
    }

    private void FiltrarGuiasMasivas()
    {
        ((DataTable)Session["dtRegFileGuia"]).Clear();
        Session["dtRegFileGuia"] = FILE_GUIA_MASIVA_BL.ListarFilesGuia();

        if (((DataTable)Session["dtRegFileGuia"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegFileGuia"]).DefaultView.RowFilter = "[NOM_FILE] LIKE '%" + txtNomFile.Text.Trim() + "%'";
            this.grvFilesGuia.DataSource = ((DataTable)Session["dtRegFileGuia"]).DefaultView;
            this.grvFilesGuia.DataBind();
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hidId.Value = Server.HtmlDecode(row.Cells[2].Text);
        lblFile.Text = Server.HtmlDecode(row.Cells[3].Text);

        if (Server.HtmlDecode(row.Cells[6].Text) == "ACTIVO")
        {
            chkEstado.Checked = true;
        }
        else if (Server.HtmlDecode(row.Cells[6].Text) == "INACTIVO")
        {
            chkEstado.Checked = false;
        }

        pnlDetalle.Visible = true;
        NomFile.Visible = true;
        Estado.Visible = true;
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hidId.Value = Server.HtmlDecode(row.Cells[2].Text);
        string strNomFile = Server.HtmlDecode(row.Cells[3].Text);

        bool Rspta = FILE_GUIA_MASIVA_BL.EliminarFileGuia(Convert.ToInt32(hidId.Value), strNomFile);

        if (Rspta == false)
        {
            LoadData();
        }
        else 
        {
            Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "Error. No se pudo eliminar el registro.");
        }       
    }

    protected void grvFilesGuia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        hidId.Value = "";
        pnlDetalle.Visible = true;
        NomFile.Visible = false;
        Estado.Visible = false;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Session["sName"] = System.IO.Path.GetFileName(fuLoadFileCM.FileName);
        Session["sExt"] = System.IO.Path.GetExtension(fuLoadFileCM.FileName);
        Session["sPath"] = System.Configuration.ConfigurationManager.AppSettings["PATH_GMA"] + (string)Session["sName"];
       
        if (hidId.Value == "")
        {
            if (fuLoadFileCM.HasFile)
            {
                if (ValidaExtension((string)Session["sExt"]))
                {
                    if (fuLoadFileCM.PostedFile.ContentLength > 4096)
                    {
                        // Graba el registro de la operación
                        FILE_GUIA_MASIVA_BL.CargarFileGuia(Convert.ToString(Session["sName"]),
                                                           Session["loginUser"].ToString());

                        // Carga el archivo de la guia masiva seleccionada en el servidor
                        fuLoadFileCM.SaveAs((string)Session["sPath"]);
                    }
                    else
                    {
                        Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "El archivo exede el tamaño permitido");
                    }
                }
                else
                {
                    Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "El archivo no es de tipo Excel");
                }
            }
            else
            {
                Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "No ha seleccionado el archivo");
            }

            Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "Se ha cargado correctamente el archivo de la guia automatica.");
        }
        else
        {
            if (chkEstado.Checked == true)
            {
                Session["sEstado"] = "A";
            }
            else 
            {
                Session["sEstado"] = "I";
            }

            // Graba el registro de la operación
            FILE_GUIA_MASIVA_BL.ActualizarFileGuia(Convert.ToInt32(hidId.Value),
                                                   Convert.ToString(Session["sName"]),
                                                   Convert.ToString(Session["sEstado"]),
                                                   Session["loginUser"].ToString(),
                                                   lblFile.Text);

            if (fuLoadFileCM.HasFile)
            {
                if (ValidaExtension((string)Session["sExt"]))
                {
                    if (fuLoadFileCM.PostedFile.ContentLength > 4096)
                    {
                        // Carga el archivo de la guia masiva seleccionada en el servidor
                        fuLoadFileCM.SaveAs((string)Session["sPath"]);
                    }
                    else
                    {
                        Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "El archivo exede el tamaño permitido");
                    }
                }
                else
                {
                    Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "El archivo no es de tipo Excel");
                }
            }

            Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", "Se ha actualizado correctamente el archivo de la guia automatica.");
        }                   

        pnlDetalle.Visible = false;

        LoadData();                
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblFile.Text = "";
        pnlDetalle.Visible = false;
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        string xlsFilePath = string.Empty;

        // Genera la plantilla para la guia masiva
        if (GUIA_BL.GenerarPlantillaGM(Convert.ToString(Session["loginUser"]), ref xlsFilePath) == true)
        {
            // Descarga la plantilla generada para la guia masiva
            Download(xlsFilePath);
        }
        else
        {
            Util.RegisterAsyncAlert(upnlBusqueda, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_NEXPCM"]);
        }        
    }

    private void Download(string filename)
    {
        if (!String.IsNullOrEmpty(filename))
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(filename);

            if (toDownload.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                Response.AddHeader("Content-Length", toDownload.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(filename);
                Response.Flush();
                Response.TransmitFile(filename);
                Response.End();
            }
        }
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

            sScript.AppendLine("function confirmEliminacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");           

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }   
}