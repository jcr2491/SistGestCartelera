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

public partial class ConfCartelCategPromo : MyBasePage
{
    SCE_CATEGORIA_BL CATEGORIA_BL;
    SCE_PROMOCION_BL PROMOCION_BL;
    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE = new SCE_CARTEL_MODELO_BE();    
    SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE CARTEL_MODELO_CATEGORIA_PROMOCION_BE;
    
    private void LoadData()
    {
        this.cboCategoria.DataSource = CATEGORIA_BL.Listar();
        this.cboCategoria.DataValueField = "ID_CATEGORIA";
        this.cboCategoria.DataTextField = "NOM_CATEGORIA";
        this.cboCategoria.DataBind();
        this.cboCategoria.Items.Insert(0, new ListItem("------------------Seleccionar-----------------", "0"));

        this.cboPromocion.DataSource = PROMOCION_BL.Listar();
        this.cboPromocion.DataValueField = "ID_PROMOCION";
        this.cboPromocion.DataTextField = "NOM_PROMOCION";
        this.cboPromocion.DataBind();
        this.cboPromocion.Items.Insert(0, new ListItem("------------------Seleccionar-----------------", "0"));

        this.cboCartelModelo.DataSource = CARTEL_MODELO_BL.cboCartelModeloCP();
        this.cboCartelModelo.DataValueField = "CODIGO";
        this.cboCartelModelo.DataTextField = "DESCRIPCION";
        this.cboCartelModelo.DataBind();
        this.cboCartelModelo.Items.Insert(0, new ListItem("-----------------------------------Seleccionar----------------------------------", "0"));
    }

    private void GetData(int IdCategoria, int IdPromocion)
    {
        List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> ListaCMCP = new List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE>();

        ListaCMCP = CARTEL_MODELO_BL.ListarCMCP(IdCategoria, IdPromocion);

        if (ListaCMCP.Count > 0)
        {
            grvListCartelCampo.DataSource = ListaCMCP;
            grvListCartelCampo.DataBind();

            pnlListado.Visible = true;
            pnlBtnAgregar.Visible = true;
        }
        else
        {
            grvListCartelCampo.DataSource = null;
            grvListCartelCampo.DataBind();

            Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", Resources.Mensajes.msgBusquedaNegativa);

            pnlListado.Visible = false;
            pnlDetalleGrupo.Visible = false;
            pnlBtnAgregar.Visible = true;            
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CATEGORIA_BL = new SCE_CATEGORIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        PROMOCION_BL = new SCE_PROMOCION_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        btnGuardarGrupo.OnClientClick = "return IsValid();";

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ConfCartelCategPromo.aspx";

            LoadData();
        }       
    }    

    protected void grvListCartelCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> lstCartelModeloCategPromo = new List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE>();

        int index = Convert.ToInt32(e.CommandArgument);

        int IdCartel = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[0].ToString());
        int IdModelo = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[1].ToString());
        int IdCategoria = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[2].ToString());
        int IdProMocion = Convert.ToInt32(grvListCartelCampo.DataKeys[index].Values[3].ToString());        

        CARTEL_MODELO_CATEGORIA_PROMOCION_BE = new SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE();
        CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_CATEGORIA = IdCategoria;
        CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_PROMOCION = IdProMocion;
        lstCartelModeloCategPromo.Add(CARTEL_MODELO_CATEGORIA_PROMOCION_BE);

        CARTEL_MODELO_BL.EliminarCMCP(IdCartel, IdModelo, lstCartelModeloCategPromo);

        Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", Resources.Mensajes.msgCCCPAlertEliminacion);          

        GetData(Convert.ToInt32(cboCategoria.SelectedValue), Convert.ToInt32(cboPromocion.SelectedValue));
    }

    protected void grvListCartelCampo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void btnGuardarGrupo_Click(object sender, EventArgs e)
    {
        string strMsg = null;
        int numMaxDigitos = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NroMaxDigitos"]);

        List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> lstCartelModeloCategPromo = new List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE>();

        strMsg = CARTEL_MODELO_BL.IsValid(cboCartelModelo.SelectedValue, 
                                          Convert.ToInt32(cboCategoria.SelectedValue),
                                          Convert.ToInt32(cboPromocion.SelectedValue));

        if (strMsg.Length > 0)
        {
            Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", strMsg);

            pnlListado.Visible = true;
            pnlDetalleGrupo.Visible = true;

            return;
        }

        CARTEL_MODELO_CATEGORIA_PROMOCION_BE = new SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE();
        CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_CATEGORIA = Convert.ToInt32(cboCategoria.SelectedValue);
        CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_PROMOCION = Convert.ToInt32(cboPromocion.SelectedValue);
        lstCartelModeloCategPromo.Add(CARTEL_MODELO_CATEGORIA_PROMOCION_BE);

        CARTEL_MODELO_BL.ActualizarCMCP(cboCartelModelo.SelectedValue, numMaxDigitos, lstCartelModeloCategPromo);        

        pnlDetalleGrupo.Visible = false;
        pnlListado.Visible = true;
        pnlBtnAgregar.Visible = true;      

        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboCategoria.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboPromocion.ClientID);
        Util.RegisterScript(upnlDetalle, "__DesabilitaCtrol__", sbScript.ToString()); 

        GetData(Convert.ToInt32(cboCategoria.SelectedValue), Convert.ToInt32(cboPromocion.SelectedValue));            
    }   

    protected void btnCancelarGrupo_Click(object sender, EventArgs e)
    {
        pnlListado.Visible = false;
        Limpiar();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", cboCategoria.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", cboPromocion.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboCartelModelo.ClientID);
        Util.RegisterScript(upnlDetalle, "__DesabilitaCtrol__", sbScript.ToString());

        if (grvListCartelCampo.Rows.Count > 0)
        {
            pnlListado.Visible = true;
        }

        pnlDetalleGrupo.Visible = true;        
    }

    public void Limpiar()
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder(); 
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboCategoria.ClientID);       
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboCategoria.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboPromocion.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboPromocion.ClientID);
        Util.RegisterScript(upnlDetalle, "__DesabilitaCtrol__", sbScript.ToString());

        pnlDetalleGrupo.Visible = false;
        pnlBtnAgregar.Visible = false;        
    }

    protected void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCategoria.SelectedValue != "0" && cboPromocion.SelectedValue != "0")
        {
            pnlListado.Visible = true;
            GetData(Convert.ToInt32(cboCategoria.SelectedValue), Convert.ToInt32(cboPromocion.SelectedValue));
        }
        else if (cboCategoria.SelectedValue == "0" || cboPromocion.SelectedValue == "0")
        {
            pnlBtnAgregar.Visible = false;
        }
    }

    protected void cboPromocion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCategoria.SelectedValue != "0" && cboPromocion.SelectedValue != "0")
        {
            pnlListado.Visible = true;
            GetData(Convert.ToInt32(cboCategoria.SelectedValue), Convert.ToInt32(cboPromocion.SelectedValue));
        }
        else if (cboCategoria.SelectedValue == "0" || cboPromocion.SelectedValue == "0")
        {
            pnlBtnAgregar.Visible = false;
        }        
    }

    private void ScriptCliente()
    {
        if (!ClientScript.IsClientScriptBlockRegistered("__ScriptCliente__"))
        {
            StringBuilder sScript = new StringBuilder();

            sScript.AppendLine("");

            sScript.AppendLine("function IsSelected()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strSelCategoria = document.getElementById('{0}').value;", cboCategoria.ClientID);
            sScript.AppendFormat("  strSelPromocion = document.getElementById('{0}').value;", cboPromocion.ClientID);
            sScript.AppendLine("    if (strSelCategoria  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCCPNoSelCategoria);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboCategoria.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strSelPromocion  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCCPNoSelPromocion);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboPromocion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strCartelModelo = document.getElementById('{0}').value;", cboCartelModelo.ClientID);
            sScript.AppendLine("    if (strCartelModelo  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCCPNoSelCarModel);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboCartelModelo.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return confirm(msg);");
            sScript.AppendLine("}");

            sScript.AppendLine("function confirmEliminacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }   
}
