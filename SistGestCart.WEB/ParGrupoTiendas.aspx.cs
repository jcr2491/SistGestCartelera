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

public partial class ParGrupoTiendas : MyBasePage
{
    SCE_GRUPOTDA_BL GRUPOTDA_BL;
    SCE_TIENDA_BL TIENDA_BL;
    SCE_TIENDA_BE TIENDA_BE = new SCE_TIENDA_BE();
    SCE_GRUPOTDA_TIENDA_BE GRUPOTDA_TIENDA_BE;  

    //Se crea la variable del registro temporal de tiendas del grupo
    //static private DataTable dtTiendas = new DataTable();

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDataTable()
    {
        DataTable dtTiendas = new DataTable();

        dtTiendas.Columns.Clear();

        DataColumn workCol = dtTiendas.Columns.Add("ID_TIENDA", typeof(Int32));
        workCol.AllowDBNull = true;
        workCol.Unique = false;

        DataColumn workCo2 = dtTiendas.Columns.Add("NOM_TIENDA", typeof(String));
        workCo2.AllowDBNull = true;
        workCo2.Unique = false;

        return dtTiendas;
    }

    private void LoadData()
    {
        this.grvGrupos.DataSource = GRUPOTDA_BL.Listar();
        this.grvGrupos.DataBind();

        this.cboTienda.DataSource = TIENDA_BL.Listar();        
        this.cboTienda.DataValueField = "ID_TIENDA";
        this.cboTienda.DataTextField = "NOM_TIENDA";
        this.cboTienda.DataBind();
        this.cboTienda.Items.Insert(0, new ListItem("-------------------------------Seleccionar-------------------------------", "0"));
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GRUPOTDA_BL = new SCE_GRUPOTDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        TIENDA_BL = new SCE_TIENDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        txtDescripcion.Attributes.Add("onKeyPress", "doClick('" + btnGuardarGrupo.ClientID + "',event)");

        btnAñadirTienda.OnClientClick = "return IsSelected();";
        btnGuardarGrupo.OnClientClick = "return IsValid();";

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ParGrupoTiendas.aspx";

            hidId.Value = "";

            Session["dtTiendas"] = CrearDataTable();
                                     
            LoadData(); 
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {

    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        hidId.Value = "";
        Session["operacion"] = false;

        List<SCE_GRUPOTDA_TIENDA_BE> lstGrupoTienda = new List<SCE_GRUPOTDA_TIENDA_BE>();

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;
        hidId.Value = Server.HtmlDecode(row.Cells[2].Text);
        txtDescripcion.Text = Server.HtmlDecode(row.Cells[3].Text);

        ((DataTable)Session["dtTiendas"]).Clear();

        lstGrupoTienda = GRUPOTDA_BL.ObtenerPorID(Convert.ToInt32(hidId.Value)).TIENDAS;

        for (int i = 0; i < lstGrupoTienda.Count; i++)
        {
            DataRow rowAux;
            rowAux = ((DataTable)Session["dtTiendas"]).NewRow();
            rowAux["ID_TIENDA"] = Convert.ToInt32(lstGrupoTienda[i].ID_TIENDA.ToString());
            rowAux["NOM_TIENDA"] = lstGrupoTienda[i].NOM_TIENDA.ToString();
            ((DataTable)Session["dtTiendas"]).Rows.Add(rowAux);
        }

        this.grvTiendasGrupo.DataSource = Session["dtTiendas"];
        this.grvTiendasGrupo.DataBind();

        txtDescripcion.Focus();

        pnlDetalleGrupo.Visible = true;
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        hidId.Value = "";
        Session["operacion"] = false;

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;
        hidId.Value = Server.HtmlDecode(row.Cells[2].Text);

        if (GRUPOTDA_BL.Eliminar(Convert.ToInt32(hidId.Value)) == false)
        {
            LoadData();

            Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", Resources.Mensajes.msgGrupoTiendaAlertEliminacion);
        }
        else
        {
            Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", Resources.Mensajes.msgGrupoTiendaAlertIR);
        }
    }

    protected void grvTiendasGrupo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                ((DataTable)Session["dtTiendas"]).Rows.RemoveAt(index);
            }

            this.grvTiendasGrupo.DataSource = Session["dtTiendas"];
            this.grvTiendasGrupo.DataBind();
        }
        catch
        { 
        
        }
    }

    protected void grvGrupos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void btnAgrupar_Click(object sender, EventArgs e)
    {
        pnlDetalleGrupo.Visible = true;        
        Limpiar();

        txtDescripcion.Focus();
    }

    protected void btnGuardarGrupo_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        List<SCE_GRUPOTDA_TIENDA_BE> lstTiendas = new List<SCE_GRUPOTDA_TIENDA_BE>();

        if (hidId.Value == "")
        {
            strMsg = GRUPOTDA_BL.IsValid(0, txtDescripcion.Text.Trim().ToUpper(), (bool)Session["operacion"]);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", strMsg);
                return;
            }

            for (int iN = 0; iN < ((DataTable)Session["dtTiendas"]).Rows.Count; iN++)
            {
                GRUPOTDA_TIENDA_BE = new SCE_GRUPOTDA_TIENDA_BE();

                GRUPOTDA_TIENDA_BE.ID_TIENDA = Convert.ToInt32(((DataTable)Session["dtTiendas"]).Rows[iN][0].ToString());
                lstTiendas.Add(GRUPOTDA_TIENDA_BE);
            }

            GRUPOTDA_BL.Insertar(txtDescripcion.Text.Trim().ToUpper(), lstTiendas);

            //Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", Resources.Mensajes.msgGrupoTiendaAlertEjecución);
        }
        else
        {
            strMsg = GRUPOTDA_BL.IsValid(Convert.ToInt32(hidId.Value), txtDescripcion.Text.Trim().ToUpper(), (bool)Session["operacion"]);

            if (strMsg.Length > 0)
            {
                Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", strMsg);
                return;
            }

            for (int iE = 0; iE < ((DataTable)Session["dtTiendas"]).Rows.Count; iE++)
            {
                GRUPOTDA_TIENDA_BE = new SCE_GRUPOTDA_TIENDA_BE();

                GRUPOTDA_TIENDA_BE.ID_TIENDA = Convert.ToInt32(((DataTable)Session["dtTiendas"]).Rows[iE][0].ToString());
                lstTiendas.Add(GRUPOTDA_TIENDA_BE);
            }

            GRUPOTDA_BL.Actualizar(Convert.ToInt32(hidId.Value), txtDescripcion.Text.Trim().ToUpper(), lstTiendas);

            //Util.RegisterAsyncAlert(upnlGrupTdas, "__Alerta__", Resources.Mensajes.msgGrupoTiendaAlertModificacion);
        }

        Limpiar();
        LoadData();       
        pnlDetalleGrupo.Visible = false;
    }

    protected void btnCancelarGrupo_Click(object sender, EventArgs e)
    {
        pnlDetalleGrupo.Visible = false;       
        Limpiar();
    }

    public void Limpiar()
    {
        hidId.Value = "";
        txtDescripcion.Text = "";
        cboTienda.SelectedIndex = 0;
        ((DataTable)Session["dtTiendas"]).Clear();
        grvTiendasGrupo.DataSource = Session["dtTiendas"];
        grvTiendasGrupo.DataBind();
        Session["operacion"] = true;        
    }

    protected void btnAñadirTienda_Click(object sender, EventArgs e)
    {
        DataRow row;
        row = ((DataTable)Session["dtTiendas"]).NewRow();
        row["ID_TIENDA"] = Convert.ToInt32(this.cboTienda.SelectedValue.Trim());
        row["NOM_TIENDA"] = cboTienda.SelectedItem.Text.Trim();

        for (int i = 0; i < ((DataTable)Session["dtTiendas"]).Rows.Count; i++)
        {
            if (((DataTable)Session["dtTiendas"]).Rows[i][1].ToString().Trim() == cboTienda.SelectedItem.Text.Trim())
            {
                return;
            }
        }

        ((DataTable)Session["dtTiendas"]).Rows.Add(row);

        this.grvTiendasGrupo.DataSource = Session["dtTiendas"];
        this.grvTiendasGrupo.DataBind();

        cboTienda.SelectedIndex = 0;        
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
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtDescripcion.ClientID);
            sScript.AppendFormat("  strSelTienda = document.getElementById('{0}').value;", cboTienda.ClientID);
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgGrupoTiendaNombNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgGrupoTiendaLongitudNoPermt);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strSelTienda  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgGrupoTiendaTdaNoseleccionada);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboTienda.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtDescripcion.ClientID);
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgGrupoTiendaNombNoIngresado);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtDescripcion.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strDescripcion.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgGrupoTiendaLongitudNoPermt);
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
