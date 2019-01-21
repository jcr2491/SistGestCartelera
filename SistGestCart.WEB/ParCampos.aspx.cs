using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using SistGestCart.BE;
using SistGestCart.BL;
using pe.oechsle.Entity;

public partial class ParCampos : MyBasePage
{
    SCE_CAMPO_BL CAMPO_BL;   

    private void LoadData()
    {
        grvCampos.DataSource = CAMPO_BL.Listar();
        grvCampos.DataBind();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CAMPO_BL = new SCE_CAMPO_BL((Usuario)Session["DatosCnSistema"]);

        txtAlias.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");
        txtCampo.Attributes.Add("onKeyPress", "doClick('" + btnGuardar.ClientID + "',event)");

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ParCampos.aspx";

            LoadData();

            this.cboTipoCampo.Items.Insert(0, new ListItem("PRECIO ENTERO", "PE"));
            this.cboTipoCampo.Items.Insert(0, new ListItem("PRECIO DECIMAL", "PD"));
            this.cboTipoCampo.Items.Insert(0, new ListItem("SIMBOLO", "SI"));
            this.cboTipoCampo.Items.Insert(0, new ListItem("TEXTO", "TE"));
            this.cboTipoCampo.Items.Insert(0, new ListItem("------------Seleccionar-----------", "0"));
        }
    }    
   
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hidId.Value = Server.HtmlDecode(row.Cells[1].Text);
        string Alias = Server.HtmlDecode(row.Cells[2].Text);

        int Rspta = CAMPO_BL.Eliminar(Convert.ToInt32(hidId.Value), Alias);

        if (Rspta == 0)
        {
            LoadData();
        }
        else if (Rspta == 1)
        {
            Util.RegisterAsyncAlert(upnlCampos, "__Alerta__", Resources.Mensajes.msgCampoAlertIR);
        }
        else if (Rspta == 2)
        {
            Util.RegisterAsyncAlert(upnlCampos, "__Alerta__", Resources.Mensajes.msgCampoAlertCAP);
        }
    }    

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string strMsg = "";

        strMsg = CAMPO_BL.IsValid(0,
                                  txtAlias.Text,
                                  txtCampo.Text.Trim().ToUpper());

        if (strMsg.Length > 0)
        {
            Util.RegisterAsyncAlert(upnlCampos, "__Alerta__", strMsg);
            return;
        }

        if (chkRestringir.Checked == true)
        {
            Session["sRestringir"] = "S";
        }
        else
        {
            Session["sRestringir"] = "N";
        }

        if (chkValDig.Checked == true)
        {
            Session["sValDigitos"] = "S";
        }
        else
        {
            Session["sValDigitos"] = "N";
        }

        CAMPO_BL.Insertar(txtAlias.Text,
                          txtCampo.Text.Trim().ToUpper(),
                          Convert.ToString(Session["sRestringir"]),
                          Convert.ToString(Session["sValDigitos"]),
                          cboTipoCampo.SelectedValue.ToString());
       
        LoadData();       
    }    
}
