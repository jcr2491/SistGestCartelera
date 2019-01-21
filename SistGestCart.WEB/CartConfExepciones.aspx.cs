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

public partial class CartConfExepciones : MyBasePage
{
    SCE_GRUPOTDA_BL GRUPOTDA_BL;
    SCE_GUIA_BL GUIA_BL;
    SCE_GUIA_EXCEPCION_BE GUIA_EXCEPCION_BE;    

    private void LoadData()
    {
        this.cboGrupoB.DataSource = GRUPOTDA_BL.Listar();
        this.cboGrupoB.DataValueField = "ID_GRUPO";
        this.cboGrupoB.DataTextField = "NOM_GRUPO";
        this.cboGrupoB.DataBind();
        this.cboGrupoB.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GRUPOTDA_BL = new SCE_GRUPOTDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        btnGuardar.OnClientClick = string.Format("return confirmacion('{0}');", Resources.Mensajes.msgConfirmación);
        
        txtNomProdBus.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        txtFecIniB.Attributes.Add("readonly", "readonly");
        txtFecFinB.Attributes.Add("readonly", "readonly");

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "CartConfExepciones.aspx";

            LoadData();
        }
    }

    protected void btnBusquedaGuia_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;

        txtNroPagina.Text = "";

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 txtNomGuiaB.Text,
                 false,
                 PageSize,
                 0,
                 ref PageCount);

        if (grvGuias.Rows.Count < PageSize)
        {
            pnlBarraPaginadora.Visible = false;
        }
        else
        {
            pnlBarraPaginadora.Visible = true;

            hidPageNumber.Value = "0";
            hidPageCount.Value = Convert.ToString(PageCount);

            /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
             *PageNumber es 0 */
            lblIndicador.Text = "Pagina" + " " + "1" + " " + "de" + " " + hidPageCount.Value;
        }
    }

    protected void btnFirstPage_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 txtNomGuiaB.Text,
                 false,
                 PageSize,
                 0,
                 ref PageCount);

        hidPageNumber.Value = "0";
        hidPageCount.Value = Convert.ToString(PageCount);

        /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
         *PageNumber es 0 */
        lblIndicador.Text = "Pagina" + " " + "1" + " " + "de" + " " + hidPageCount.Value;
    }

    protected void btnPreviousPage_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;
        int PageNumber = 0;

        if (Convert.ToInt32(hidPageNumber.Value) > 1)
        {
            PageNumber = Convert.ToInt32(hidPageNumber.Value) - 1;
        }

        if (PageNumber > 0)
        {
            PageNumber = PageNumber - 1;
        }

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 txtNomGuiaB.Text,
                 false,
                 PageSize,
                 PageNumber,
                 ref PageCount);

        hidPageNumber.Value = Convert.ToString(PageNumber + 1);
        hidPageCount.Value = Convert.ToString(PageCount);

        /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
         *PageNumber es 0 */
        lblIndicador.Text = "Pagina" + " " + hidPageNumber.Value + " " + "de" + " " + hidPageCount.Value;
    }

    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;
        int PageNumber = 0;

        if (Convert.ToInt32(hidPageNumber.Value) > 1)
        {
            PageNumber = Convert.ToInt32(hidPageNumber.Value) - 1;
        }

        if (PageNumber >= 0)
        {
            PageNumber = PageNumber + 1;

            /* se valida cuando llega a la ultima pagina*/
            if (PageNumber >= Convert.ToInt32(hidPageCount.Value))
            {
                PageNumber = PageNumber - 1;
            }
        }

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 txtNomGuiaB.Text,
                 false,
                 PageSize,
                 PageNumber,
                 ref PageCount);

        hidPageNumber.Value = Convert.ToString(PageNumber + 1);
        hidPageCount.Value = Convert.ToString(PageCount);

        /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
         *PageNumber es 0 */
        lblIndicador.Text = "Pagina" + " " + hidPageNumber.Value + " " + "de" + " " + hidPageCount.Value;
    }

    protected void btnLastPage_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 txtNomGuiaB.Text,
                 true,
                 PageSize,
                 0,
                 ref PageCount);

        hidPageNumber.Value = Convert.ToString(PageCount);
        hidPageCount.Value = Convert.ToString(PageCount);

        /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
         *PageNumber es 0 */
        lblIndicador.Text = "Pagina" + " " + PageCount + " " + "de" + " " + hidPageCount.Value;
    }

    protected void btnIrAPagina_Click(object sender, EventArgs e)
    {
        if (txtNroPagina.Text != "" && txtNroPagina.Text != "0")
        {
            int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
            int PageCount = 0;

            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboGrupoB.SelectedValue),
                     txtNomGuiaB.Text,
                     false,
                     PageSize,
                     (Convert.ToInt32(txtNroPagina.Text) - 1),
                     ref PageCount);

            hidPageNumber.Value = txtNroPagina.Text;
            hidPageCount.Value = Convert.ToString(PageCount);

            /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
             *PageNumber es 0 */
            lblIndicador.Text = "Pagina" + " " + txtNroPagina.Text + " " + "de" + " " + hidPageCount.Value;
        }
    }

    public void Busqueda(string FecIniB,
                         string FecFinB,
                         int Grupo,
                         string NomGuia,
                         bool lastPage,
                         int PageSize,
                         int PageNumber,
                         ref int PageCount)
    {
        DateTime? FecIniBC; //DateTime? == Nullable<DateTime>
        DateTime? FecFinBC; //DateTime? == Nullable<DateTime>

        List<SCE_GUIA_BE> lstGuia = new List<SCE_GUIA_BE>();

        if ((FecIniB == null) || (FecIniB.ToString().Length == 0))
        {
            FecIniBC = null;
        }
        else
        {
            FecIniBC = Convert.ToDateTime(FecIniB);
        }

        if ((FecFinB == null) || (FecFinB.ToString().Length == 0))
        {
            FecFinBC = null;
        }
        else
        {
            FecFinBC = Convert.ToDateTime(FecFinB);
        }

        lstGuia = GUIA_BL.BuscarE(FecIniBC,
                                  FecFinBC,
                                  Grupo,
                                  NomGuia,
                                  lastPage,
                                  PageSize,
                                  PageNumber,
                                  ref PageCount);

        if (lstGuia.Count > 0)
        {
            ViewState["listadoGuias"] = lstGuia;
            this.grvGuias.DataSource = ViewState["listadoGuias"];
            this.grvGuias.DataBind();
        }
        else
        {
            upndlBusqueda.Update();
            this.grvGuias.DataSource = null;
            this.grvGuias.DataBind();
            Util.RegisterAsyncAlert(upndlBusqueda, "__Alerta__", Resources.Mensajes.msgBusquedaSinRegistros);
        }
    } 
   
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        hidIdGuia.Value = "0";
        hidGrupo.Value = "0";

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hidIdGuia.Value = Server.HtmlDecode(row.Cells[1].Text);
        hidGrupo.Value = Server.HtmlDecode(row.Cells[6].Text);

        Session["dtRegProductoPV"] = GUIA_BL.ListarExecepcionesPV(Convert.ToInt32(hidIdGuia.Value), Convert.ToInt32(hidGrupo.Value));

        grvListProdClusterTiendas.DataSource = (DataTable)Session["dtRegProductoPV"];
        grvListProdClusterTiendas.DataBind();

        pnlBusqueda.Visible = false;
        pnlDetProdTiendas.Visible = true;

        upnlDetalle.Update();
    }

    protected void btnBusProd_Click(object sender, EventArgs e)
    {
        
        ((DataTable)Session["dtRegProductoPV"]).Clear();
        Session["dtRegProductoPV"] = GUIA_BL.ListarExecepcionesPV(Convert.ToInt32(hidIdGuia.Value), Convert.ToInt32(hidGrupo.Value));

        if (((DataTable)Session["dtRegProductoPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegProductoPV"]).DefaultView.RowFilter = "[VALOR] LIKE '%" + txtNomProdBus.Text.Trim() + "%'";
            this.grvListProdClusterTiendas.DataSource = ((DataTable)Session["dtRegProductoPV"]).DefaultView;
            this.grvListProdClusterTiendas.DataBind();
        }
       
    }

    protected void grvListProdClusterTiendas_RowCreated(object sender, GridViewRowEventArgs e)
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

                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }        
    }

    protected void btnEditar1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hidIdGuia.Value = Server.HtmlDecode(row.Cells[1].Text);
        hidIdLinea.Value = Server.HtmlDecode(row.Cells[2].Text);

        grvTiendas.DataSource = GUIA_BL.GetDetTiendasExecepcion(Convert.ToInt32(hidIdGuia.Value), Convert.ToInt32(hidIdLinea.Value));
        grvTiendas.DataBind();

        pnlDetTiendas.Visible = true;
    }   

    protected void btnCancelarDE_Click(object sender, EventArgs e)
    {
        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();
        pnlDetProdTiendas.Visible = false;
        pnlDetTiendas.Visible = false;
        pnlBusqueda.Visible = true;

        pnlBarraPaginadora.Visible = false;

        upndlBusqueda.Update();
    }

   
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        List<SCE_GUIA_EXCEPCION_BE> lstExcepciones = new List<SCE_GUIA_EXCEPCION_BE>();

        foreach (GridViewRow Row in grvTiendas.Rows)
        {
            int i = Convert.ToInt32(Row.RowIndex);

            GUIA_EXCEPCION_BE = new SCE_GUIA_EXCEPCION_BE();

            CheckBox check = Row.FindControl("chkSelection") as CheckBox;

            GUIA_EXCEPCION_BE.ID_GUIA = Convert.ToInt32(hidIdGuia.Value);
            GUIA_EXCEPCION_BE.ID_LINEA = Convert.ToInt32(hidIdLinea.Value);
            GUIA_EXCEPCION_BE.ID_TIENDA = Convert.ToInt32(grvTiendas.Rows[i].Cells[0].Text);
            GUIA_EXCEPCION_BE.USER_MOD = Convert.ToString(Session["loginUser"]);

            if (check.Checked)
            {               
                GUIA_EXCEPCION_BE.FLAGPERTENECE = 1;
            }
            else
            {
                GUIA_EXCEPCION_BE.FLAGPERTENECE = 0;
            }

            lstExcepciones.Add(GUIA_EXCEPCION_BE);
        }

        GUIA_BL.ActualizarExcepciones(lstExcepciones);

        grvListProdClusterTiendas.DataSource = GUIA_BL.ListarExecepcionesPV(Convert.ToInt32(hidIdGuia.Value), Convert.ToInt32(hidGrupo.Value));
        grvListProdClusterTiendas.DataBind();

        pnlDetTiendas.Visible = false;

        Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", Resources.Mensajes.msgAlertaEjecución);
   
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pnlDetTiendas.Visible = false;
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

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }    
}
