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
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;


public partial class ImprBusqImpresion : MyBasePage
{
    SCE_TIENDA_BL TIENDA_BL;
    SCE_CATEGORIA_BL CATEGORIA_BL;
    SCE_PROMOCION_BL PROMOCION_BL;
    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_GUIA_BL GUIA_BL;    

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegProducto()
    {
        DataTable dtRegProducto = new DataTable();

        dtRegProducto.Columns.Clear();

        DataColumn workCol0 = dtRegProducto.Columns.Add("ID_PROMOCION", typeof(Int32));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegProducto.Columns.Add("NOM_PROMOCION", typeof(String));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegProducto.Columns.Add("ID_LINEA", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegProducto.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRegProducto.Columns.Add("NOM_CAMPO", typeof(String));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;

        DataColumn workCol5 = dtRegProducto.Columns.Add("VALOR", typeof(String));
        workCol5.AllowDBNull = true;
        workCol5.Unique = false;

        return dtRegProducto;
    }
   
    //Se crea la estructura del registro temporal de impresiones
    private DataTable CrearDtRegImpresiones()
    {
        DataTable dtRegImpresiones = new DataTable();

        dtRegImpresiones.Columns.Clear();

        DataColumn workCol0 = dtRegImpresiones.Columns.Add("ID_CARTEL_MODELO", typeof(String));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegImpresiones.Columns.Add("LINEA", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegImpresiones.Columns.Add("NROCOPIAS", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        return dtRegImpresiones;
    }

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegImpresion()
    {
        DataTable dtRegImpresion = new DataTable();

        dtRegImpresion.Columns.Clear();

        DataColumn workCol0 = dtRegImpresion.Columns.Add("ID_CARTEL_MODELO", typeof(String));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegImpresion.Columns.Add("ID_LINEA", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegImpresion.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegImpresion.Columns.Add("NOM_CAMPO", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRegImpresion.Columns.Add("VALOR", typeof(String));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;

        DataColumn workCol5 = dtRegImpresion.Columns.Add("POSX", typeof(String));
        workCol5.AllowDBNull = true;
        workCol5.Unique = false;

        DataColumn workCol6 = dtRegImpresion.Columns.Add("POSY", typeof(String));
        workCol6.AllowDBNull = true;
        workCol6.Unique = false;

        DataColumn workCol7 = dtRegImpresion.Columns.Add("HOJA", typeof(String));
        workCol7.AllowDBNull = true;
        workCol7.Unique = false;

        return dtRegImpresion;
    }

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegImpresion1()
    {
        DataTable dtRegImpresion = new DataTable();

        dtRegImpresion.Columns.Clear();

        DataColumn workCol0 = dtRegImpresion.Columns.Add("ID_CARTEL_MODELO", typeof(String));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegImpresion.Columns.Add("ID_LINEA", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegImpresion.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegImpresion.Columns.Add("NOM_CAMPO", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRegImpresion.Columns.Add("VALOR", typeof(String));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;

        DataColumn workCol5 = dtRegImpresion.Columns.Add("POSX", typeof(String));
        workCol5.AllowDBNull = true;
        workCol5.Unique = false;

        DataColumn workCol6 = dtRegImpresion.Columns.Add("POSY", typeof(String));
        workCol6.AllowDBNull = true;
        workCol6.Unique = false;

        DataColumn workCol7 = dtRegImpresion.Columns.Add("HOJA", typeof(String));
        workCol7.AllowDBNull = true;
        workCol7.Unique = false;

        return dtRegImpresion;
    }

    private DataTable CrearDtRepImpresion()
    {
        //Se crea la variable del registro de informe de impresion de productos
        DataTable dtRepImpresion = new DataTable();

        dtRepImpresion.Clear();

        DataColumn workCol0 = dtRepImpresion.Columns.Add("ID_CARTEL_MODELO", typeof(String));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRepImpresion.Columns.Add("NOM_CARTEL_MODELO", typeof(String));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRepImpresion.Columns.Add("NOM_MODELO", typeof(String));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRepImpresion.Columns.Add("TIPO_HOJA", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRepImpresion.Columns.Add("NRO_COPIAS", typeof(Int32));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;        

        return dtRepImpresion;
    }   

    private void LoadData()
    {
        this.cboCategoriaB.DataSource = CATEGORIA_BL.Listar();
        this.cboCategoriaB.DataValueField = "ID_CATEGORIA";
        this.cboCategoriaB.DataTextField = "NOM_CATEGORIA";
        this.cboCategoriaB.DataBind();
        this.cboCategoriaB.Items.Insert(0, new ListItem("------------Seleccionar-----------", "0"));

        this.cboPromocionB.DataSource = PROMOCION_BL.Listar();
        this.cboPromocionB.DataValueField = "ID_PROMOCION";
        this.cboPromocionB.DataTextField = "NOM_PROMOCION";
        this.cboPromocionB.DataBind();
        this.cboPromocionB.Items.Insert(0, new ListItem("------------Seleccionar-----------", "0"));

        this.cboTiendaB.DataSource = TIENDA_BL.Listar();
        this.cboTiendaB.DataValueField = "ID_TIENDA";
        this.cboTiendaB.DataTextField = "NOM_TIENDA";
        this.cboTiendaB.DataBind();
        this.cboTiendaB.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        TIENDA_BL = new SCE_TIENDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CATEGORIA_BL = new SCE_CATEGORIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        PROMOCION_BL = new SCE_PROMOCION_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        btnBusqueda.OnClientClick = "return IsSelected();";

        txtFecIniB.Attributes.Add("readonly", "readonly");
        txtFecFinB.Attributes.Add("readonly", "readonly");

        txtNomProdBus.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));
       
        Util.SetEnterButton(txtNomProdBus, btnBusProd);

        if ((bool)Session["UsuADMPC"] == true)
        {
            lblLocal.Visible = true;
            cboTiendaB.Visible = true;
        }
        else 
        {
            lblLocal.Visible = false;
            cboTiendaB.Visible = false;
        }

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "ImprBusqImpresion.aspx";

            Session["dtRegProducto"] = CrearDtRegProducto();
            ((DataTable)Session["dtRegProducto"]).Clear();

            Session["dtRegImpresiones"] = CrearDtRegImpresiones();
            ((DataTable)Session["dtRegImpresiones"]).Clear();

            Session["dtRegImpresion"] = CrearDtRegImpresion();
            ((DataTable)Session["dtRegImpresion"]).Clear();

            Session["dtRegImpresion1"] = CrearDtRegImpresion1();
            ((DataTable)Session["dtRegImpresion1"]).Clear();

            LoadData();

            this.cboTipoGuia.Items.Insert(0, new ListItem("MANUAL", "1"));
            this.cboTipoGuia.Items.Insert(0, new ListItem("AUTOMATICA", "2"));
            this.cboTipoGuia.Items.Insert(0, new ListItem("------------Seleccionar-----------", "0"));
        }       
    }

    protected void btnBusqueda_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     0,
                     ref PageCount);
        }
        else
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(Session["Tienda"].ToString()),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     0,
                     ref PageCount);
        }

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

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     0,
                     ref PageCount);
        }
        else
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(Session["Tienda"].ToString()),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     0,
                     ref PageCount);
        }

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

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     PageNumber,
                     ref PageCount);
        }
        else
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(Session["Tienda"].ToString()),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     PageNumber,
                     ref PageCount);
        }

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

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     PageNumber,
                     ref PageCount);
        }
        else
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(Session["Tienda"].ToString()),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     false,
                     PageSize,
                     PageNumber,
                     ref PageCount);
        }

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

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     true,
                     PageSize,
                     0,
                     ref PageCount);
        }
        else
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(Session["Tienda"].ToString()),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     txtNomGuiaB.Text,
                     Convert.ToInt32(cboTipoGuia.SelectedValue),
                     true,
                     PageSize,
                     0,
                     ref PageCount);
        }

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

            if ((bool)Session["UsuADMPC"] == true)
            {
                Busqueda(txtFecIniB.Text,
                         txtFecFinB.Text,
                         Convert.ToInt32(cboTiendaB.SelectedValue),
                         Convert.ToInt32(cboPromocionB.SelectedValue),
                         Convert.ToInt32(cboCategoriaB.SelectedValue),
                         txtNomGuiaB.Text,
                         Convert.ToInt32(cboTipoGuia.SelectedValue),
                         false,
                         PageSize,
                         (Convert.ToInt32(txtNroPagina.Text) - 1),
                         ref PageCount);
            }
            else
            {
                Busqueda(txtFecIniB.Text,
                         txtFecFinB.Text,
                         Convert.ToInt32(Session["Tienda"].ToString()),
                         Convert.ToInt32(cboPromocionB.SelectedValue),
                         Convert.ToInt32(cboCategoriaB.SelectedValue),
                         txtNomGuiaB.Text,
                         Convert.ToInt32(cboTipoGuia.SelectedValue),
                         false,
                         PageSize,
                         (Convert.ToInt32(txtNroPagina.Text) - 1),
                         ref PageCount);
            }

            hidPageNumber.Value = txtNroPagina.Text;
            hidPageCount.Value = Convert.ToString(PageCount);

            /*Muestra el estado de paginación de PageNumber igual a 1 teniendo en cuenta que realmente 
             *PageNumber es 0 */
            lblIndicador.Text = "Pagina" + " " + txtNroPagina.Text + " " + "de" + " " + hidPageCount.Value;
        }
    }

    public void Busqueda(string FecIniB,
                         string FecFinB,
                         int local,
                         int IdPpromocion,
                         int IdCategoria,
                         string NomGuia,
                         int TipGuia,
                         bool lastPage,
                         int PageSize,
                         int PageNumber,
                         ref int PageCount)
    {
        DateTime? FecIniBC; //DateTime? == Nullable<DateTime>
        DateTime? FecFinBC; //DateTime? == Nullable<DateTime>

        List<SCE_LIST_GUIA> lstGuia = new List<SCE_LIST_GUIA>();

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

        lstGuia = GUIA_BL.BuscarImpr(FecIniBC,
                                     FecFinBC,
                                     local,
                                     IdPpromocion,
                                     IdCategoria,
                                     NomGuia,
                                     TipGuia,
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
            upnlDetalle.Update();
            this.grvGuias.DataSource = null;
            this.grvGuias.DataBind();
            Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", Resources.Mensajes.msgBusquedaSinRegistros);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        //System.Text.StringBuilder sbScript = new System.Text.StringBuilder();        

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hdIdGuia.Value = Server.HtmlDecode(row.Cells[1].Text);

        lblFecIni.Text = Server.HtmlDecode(row.Cells[6].Text);
        lblFecFin.Text = Server.HtmlDecode(row.Cells[7].Text);

        hidIdTienda.Value = Server.HtmlDecode(row.Cells[8].Text);
        hidIDGrupo.Value = Server.HtmlDecode(row.Cells[10].Text);

        txtNomProdBus.Text = "";

        if (hidIdTienda.Value != "0")
        {
            lblLocalOGrupo.Text = "Tienda";
            lblNomGrupoOLocal.Text = Server.HtmlDecode(row.Cells[9].Text);

            //sbScript.AppendFormat("document.getElementById('{0}').innerHTML='Tienda';", lblLocalOGrupo.ClientID);
            //Util.RegisterScript(upnlDetalle, "__ConfigurarCtroles__", sbScript.ToString());            
        }
        else
        {
            lblLocalOGrupo.Text = "Grupo";
            lblNomGrupoOLocal.Text = Server.HtmlDecode(row.Cells[11].Text);

            //sbScript.AppendFormat("document.getElementById('{0}').innerHTML='Grupo';", lblLocalOGrupo.ClientID);
            //Util.RegisterScript(upnlDetalle, "__ConfigurarCtroles__", sbScript.ToString());
           
        }

        if (cboPromocionB.SelectedValue == "0")
        {
            hidPromocion.Value = "0";
            lblPromocionDet.Text = "TODOS";

            //sbScript.AppendFormat("document.getElementById('{0}').innerHTML='0';", hidPromocion.ClientID);
            //sbScript.AppendFormat("document.getElementById('{0}').innerHTML='TODOS';", lblPromocionDet.ClientID);
            //Util.RegisterScript(upnlDetalle, "__ConfigurarCtroles__", sbScript.ToString());
        }
        else
        {
            hidPromocion.Value = cboPromocionB.SelectedValue.ToString();
            lblPromocionDet.Text = cboPromocionB.SelectedItem.Text;
        }

        Session["varTipGuia"] = Server.HtmlDecode(row.Cells[3].Text);

        hidIdCategoria.Value = Server.HtmlDecode(row.Cells[4].Text);
        lblCategoria.Text = Server.HtmlDecode(row.Cells[5].Text);

        lblNomGuia.Text = Server.HtmlDecode(row.Cells[2].Text);

        Session["varVigente"] = Server.HtmlDecode(row.Cells[12].Text);

        if ((bool)Session["UsuADMPC"] == true) // Si el usuario es administrador o Publicista Central
        {
            //Llena el registro temporal
            Session["dtRegProducto"] = GUIA_BL.GetDetalleGuiaCampoImpr(Convert.ToInt32(hdIdGuia.Value),
                                                                       Convert.ToInt32(hidIdCategoria.Value),
                                                                       Convert.ToInt32(hidPromocion.Value),
                                                                       Convert.ToInt32(cboTiendaB.SelectedValue),
                                                                       Session["varTipGuia"].ToString().Trim());
        }
        else // Si el usuario es Jefe de Seccion
        {
            //Llena el registro temporal
            Session["dtRegProducto"] = GUIA_BL.GetDetalleGuiaCampoImpr(Convert.ToInt32(hdIdGuia.Value),
                                                                       Convert.ToInt32(hidIdCategoria.Value),
                                                                       Convert.ToInt32(hidPromocion.Value),
                                                                       Convert.ToInt32(Session["Tienda"]),
                                                                       Session["varTipGuia"].ToString().Trim());
        } 

        Session["dtRegProductoPV"] = GUIA_BL.PivotDtDetalleGuiaCamposImpr((DataTable)Session["dtRegProducto"], "NOM_CAMPO");

        //Llenamos la grilla con el registro temporal Pivoteado
        this.grvDetalleGuias.DataSource = (DataTable)Session["dtRegProductoPV"];
        this.grvDetalleGuias.DataBind();

        pnlDetalleGuia.Visible = true;

        pnlBarraPaginadora.Visible = false;

        pnlCabecera.Visible = false;
        pnlImpresion.Visible = false;

        if ((string)Session["varVigente"] == "NO VIGENTE")
        {
            btnImprimir.Enabled = false;
            //sbScript.AppendFormat("document.getElementById('{0}').disabled=true;", btnImprimir.ClientID);
            //Util.RegisterScript(upnlCabecera, "__ConfigurarCtroles__", sbScript.ToString());
        }

        //Util.RegisterScript(upnlCabecera, "__ConfigurarCtroles__", sbScript.ToString());

        upnlDetalle.Update();

        /*****************************************************************************************************************************/
        //Limpia y destruye las variables de session de esta seccion
        /*****************************************************************************************************************************/
        Session["varTipGuia"] = null;
        Session["varVigente"] = null;
        /*****************************************************************************************************************************/
    }

    protected void btnBusProd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder(); 
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtNomProdBus.ClientID);
        Util.RegisterScript(upnlDetalle, "__ConfigurarCtroles__", sbScript.ToString());
    }

    private void FiltrarProducto()
    {
        ((DataTable)Session["dtRegProductoPV"]).Clear();
        Session["dtRegProductoPV"] = GUIA_BL.PivotDtDetalleGuiaCamposImpr((DataTable)Session["dtRegProducto"], "NOM_CAMPO");

        if (((DataTable)Session["dtRegProductoPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegProductoPV"]).DefaultView.RowFilter = "[<PROD1>] LIKE '%" + txtNomProdBus.Text.Trim() + "%'";
            this.grvDetalleGuias.DataSource = ((DataTable)Session["dtRegProductoPV"]).DefaultView;
            this.grvDetalleGuias.DataBind();
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {        
        Session["NroCopias"] = 0;

        ((DataTable)Session["dtRegImpresiones"]).Clear();
        ((DataTable)Session["dtRegImpresion"]).Clear();

        //Se crea la variable del registro de informe de impresion de productos
        Session["dtRepImpresion"] = CrearDtRepImpresion();

        foreach (GridViewRow row in grvDetalleGuias.Rows)
        {
            Session["index"] = Convert.ToInt32(row.RowIndex);

            CheckBox check = row.FindControl("chkSelection") as CheckBox;

            if (check.Checked)
            {
                Session["IdLinea"] = Convert.ToInt32(grvDetalleGuias.Rows[Convert.ToInt32(Session["index"])].Cells[6].Text);

                Session["IdCartelModelo"] = ((DropDownList)row.FindControl("ddlCarteles")).SelectedValue.ToString();

                if (Session["IdCartelModelo"].ToString() != "0")
                {
                    Session["NomCartelModelo"] = ((DropDownList)row.FindControl("ddlCarteles")).SelectedItem.Text;

                    Session["NomModelo"] = CARTEL_MODELO_BL.GetNombreModelo(Convert.ToString(Session["IdCartelModelo"]));
                    Session["TipoHoja"] = CARTEL_MODELO_BL.GetTipoHojaModelo(Convert.ToString(Session["IdCartelModelo"]));
                }
                else
                {
                    Session["IdCartelModelo"] = "";
                    Session["NomCartelModelo"] = "";
                    Session["TipoHoja"] = "";
                    Session["NomModelo"] = "";
                }

                TextBox texto = row.FindControl("txtNroPag") as TextBox;

                if (texto.Text != "")
                {
                    Session["NroCopias"] = Convert.ToInt32(((TextBox)row.FindControl("txtNroPag")).Text);
                }
                else
                {
                    Session["NroCopias"] = 0;
                }

                if ((Convert.ToString(Session["IdCartelModelo"]) == "0" || Convert.ToString(Session["IdCartelModelo"]) == "") || (Convert.ToInt32(Session["NroCopias"]) == 0))
                {
                    Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", "Faltan datos para generar el Pool de Impresion");

                    ((DataTable)Session["dtRepImpresion"]).Clear();

                    return;
                }
                else if ((Convert.ToString(Session["IdCartelModelo"]) != "0" || Convert.ToString(Session["IdCartelModelo"]) != "") && (Convert.ToInt32(Session["NroCopias"]) != 0))
                {
                    //LLENO EL REGISTRO DE PARA REPORTE DE IMPRESIONES
                    ((DataTable)this.Session["dtRepImpresion"]).Rows.Add(
                                                                   Convert.ToString(Session["IdCartelModelo"]),
                                                                   Convert.ToString(Session["NomCartelModelo"]),
                                                                   Convert.ToString(Session["NomModelo"]),
                                                                   Convert.ToString(Session["TipoHoja"]),
                                                                   Convert.ToInt32(Session["NroCopias"]));

                    ((DataTable)this.Session["dtRepImpresion"]).AcceptChanges();

                    //LLENO EL REGISTRO DE IMPRESIONES                    
                    ((DataTable)this.Session["dtRegImpresiones"]).Rows.Add(
                                                                   Convert.ToString(Session["IdCartelModelo"]),
                                                                   Convert.ToInt32(Session["IdLinea"]),
                                                                   Convert.ToInt32(Session["NroCopias"]));

                    ((DataTable)this.Session["dtRegImpresiones"]).AcceptChanges();

                    if (GUIA_BL.EsModeloMultiple(Convert.ToString(Session["IdCartelModelo"])))
                    {
                        //LLENO EL REGISTRO DE DATOS DEL PRODUCTO PARA IMPRESION DE CARTELES MULTIPLES
                        this.Session["strCrtFiltro"] = null;
                        this.Session["strCrtFiltro"] = "ID_LINEA = " + Convert.ToInt32(Session["IdLinea"]);
                        this.Session["dtAux"] = Util.FiltraDataTable((DataTable)Session["dtRegProducto"], Convert.ToString(this.Session["strCrtFiltro"]));

                        for (int nc = 1; nc <= Convert.ToInt32(Session["NroCopias"]); nc++)
                        {
                            for (int w = 0; w < ((DataTable)Session["dtAux"]).Rows.Count; w++)
                            {
                                ((DataTable)this.Session["dtRegImpresion"]).Rows.Add(
                                                                   Convert.ToString(Session["IdCartelModelo"]),
                                                                   Convert.ToInt32(Session["IdLinea"]),
                                                                   ((DataTable)Session["dtAux"]).Rows[w][3],
                                                                   ((DataTable)Session["dtAux"]).Rows[w][4],
                                                                   ((DataTable)Session["dtAux"]).Rows[w][5],
                                                                   "",
                                                                   "",
                                                                   "");

                                ((DataTable)this.Session["dtRegImpresion"]).AcceptChanges();

                            }
                        }
                    }
                    else
                    { 
                    
                    }

                }
            }
        }

        if (((DataTable)Session["dtRepImpresion"]).Rows.Count == 0)
        {
            Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", "No ha seleccionado ningun registro");
        }
        else if (((DataTable)Session["dtRepImpresion"]).Rows.Count > 0)
        {
            //Agrupo el registro temporal
            //Añado las columnas por la que se agrupara el DataTable
            IList<string> _groupByColumnNames = new List<string>();
            _groupByColumnNames.Add("ID_CARTEL_MODELO");
            _groupByColumnNames.Add("NOM_CARTEL_MODELO");
            _groupByColumnNames.Add("NOM_MODELO");
            _groupByColumnNames.Add("TIPO_HOJA");

            //Establezco el campo y la funcion por la que se hara la agregación
            IList<DataTableAggregateFunction> _fieldsForCalculation = new List<DataTableAggregateFunction>();
            _fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = DataTableAggregateFunction.AggregateFunction.Sum, ColumnName = "NRO_COPIAS", OutPutColumnName = "NRO_COPIAS" });

            GrvImpresion.DataSource = Util.GetGroupedBy((DataTable)Session["dtRepImpresion"], _groupByColumnNames, _fieldsForCalculation);
            GrvImpresion.DataBind();

            pnlImpresion.Visible = true;
        }

        pnlDetalleImpresion.Visible = false;

        /*****************************************************************************************************************************/
        //Limpia y destruye las variables de session de esta seccion
        /*****************************************************************************************************************************/
        this.Session["strCrtFiltro"] = null;
        this.Session["dtAux"] = null;
        /*****************************************************************************************************************************/
    }    

    protected void btnCancelarImpresion_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();       

        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtFecIniB.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtFecFinB.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').value='';", txtNomGuiaB.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboCategoriaB.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboPromocionB.ClientID);
        sbScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboTipoGuia.ClientID);

        Util.RegisterScript(upnlCabecera, "__ConfigurarCtrolesc__", sbScript.ToString());

        btnImprimir.Enabled = true;

        pnlDetalleGuia.Visible = false;
        pnlImpresion.Visible = false;
        pnlDetalleImpresion.Visible = false;
        pnlCabecera.Visible = true;

        upnlCabecera.Update();
    }

    protected void btnCancelImpresion_Click(object sender, EventArgs e)
    {       
        pnlImpresion.Visible = false;
        pnlDetalleImpresion.Visible = false;
    }

    protected void btnCierraDetImpre_Click(object sender, EventArgs e)
    {
        pnlDetalleImpresion.Visible = false;
        pnlImpresion.Visible = false;
    }

    protected void grvDetalleGuias_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].CssClass = "ColumnaOculta";
            e.Row.Cells[6].CssClass = "ColumnaOculta";       
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                e.Row.Cells[4].ControlStyle.CssClass = "ColumnaOculta";
                e.Row.Cells[6].ControlStyle.CssClass = "ColumnaOculta";

                //e.Row.Cells[1].ControlStyle.Width = 200;

                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
    }

    protected void grvDetalleGuias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox checkH = (CheckBox)e.Row.FindControl("chkAll");
            checkH.Attributes.Add("onclick", String.Format("SelectAll(this,'{0}')", grvDetalleGuias.ClientID));
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            List<SCE_CARTEL_MODELO_BE> lstCMCP = new List<SCE_CARTEL_MODELO_BE>();

            Session["IdPromocion"] = Int32.Parse(e.Row.Cells[4].Text);

            DropDownList ComboBox = (DropDownList)e.Row.FindControl("ddlCarteles");

            CheckBox check = (CheckBox)e.Row.FindControl("chkSelection");
            TextBox texto = (TextBox)e.Row.FindControl("txtNroPag");

            lstCMCP = CARTEL_MODELO_BL.cboCartelModeloCMCP(Convert.ToInt32(hidIdCategoria.Value),
                                                           Convert.ToInt32(Session["IdPromocion"]),
                                                           Convert.ToInt32(hdIdGuia.Value),
                                                           Int32.Parse(e.Row.Cells[6].Text));
            if (lstCMCP.Count > 0)
            {
                ComboBox.DataSource = lstCMCP;
                ComboBox.DataValueField = "CODIGO";
                ComboBox.DataTextField = "DESCRIPCION";
                ComboBox.DataBind();
                ComboBox.Items.Insert(0, new ListItem("-----Seleccionar-----", "0"));
            }
            else
            {
                ComboBox.DataSource = lstCMCP;
                ComboBox.DataValueField = "CODIGO";
                ComboBox.DataTextField = "DESCRIPCION";
                ComboBox.DataBind();
                ComboBox.Items.Insert(0, new ListItem("-----No hay carteles cargados-----", "0"));
                ComboBox.Enabled = false;
                check.Enabled = false;
                texto.Enabled = false;
            }
            string skuProducto = GUIA_BL.GetSkuProducto_Campo(Convert.ToInt32(hdIdGuia.Value), Int32.Parse(e.Row.Cells[6].Text));
            if (skuProducto != "" && skuProducto != "-")
            {
                if (!VerificarStock(skuProducto))
                {
                    check.Enabled = false;
                    texto.Enabled = false;
                    ComboBox.Enabled = false;
                }
            }            

            check.Attributes.Add("onclick", String.Format("CheckeaCheck(this,'{0}')", texto.ClientID));
            texto.Attributes.Add("onkeypress", String.Format("javascript:return SoloNumeros(event)"));

            /*****************************************************************************************************************************/
            //Limpia y destruye las variables de session de esta seccion
            /*****************************************************************************************************************************/
            Session["IdPromocion"] = null;
            /*****************************************************************************************************************************/
        }
    }

    protected void btnImprimirFinal_Click(object sender, EventArgs e)
    {   
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.Parent.Parent;

        /***********************************************/
        /*Limpia el nuevo registro temporal que se creo
        //para agilizar el proceso en cliente y librar
        //de carga de datos al motor de generacion de
        //carteles*/
        /***********************************************/
        ((DataTable)Session["dtRegImpresion1"]).Clear();
        /***********************************************/

        Session["IdCartelModelo"] = row.Cells[0].Text.Trim();
        Session["NomCartelModelo"] = Server.HtmlDecode(row.Cells[1].Text.Trim());

        /*****************************************************************************************************************************/
        // (INICIO DEL PROCESO DE IMPRESION DEL CARTEL)
        /*****************************************************************************************************************************/
        Util.RegLogStepsPrintProcess2("Inicia el proceso de impresion",
                                      Convert.ToString(Session["loginUser"]),
                                      Convert.ToInt32(Session["Tienda"].ToString()),
                                      Convert.ToString(Session["NomCartelModelo"]),
                                      0);
        /*****************************************************************************************************************************/   
        Session["strPrintCartel"] = string.Empty;

        Session["strPathServer"] = System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"];

        Session["strNomPlantilla"] = CARTEL_MODELO_BL.ObtenerPlantilla(Convert.ToString(Session["IdCartelModelo"]));
        Session["strPathCarteles"] = System.Configuration.ConfigurationManager.AppSettings["RutaCarteleria"];

        Session["IdNameCartel"] = Convert.ToString(Session["loginUser"]) + "_" +
                                  DateTime.Now.Day.ToString("00") +
                                  DateTime.Now.Month.ToString("00") +
                                  DateTime.Now.Hour.ToString("00") +
                                  DateTime.Now.Minute.ToString("00") +
                                  DateTime.Now.Second.ToString("00");

        // Realiza este proceso de calculo de coordenadas y determinacion de hoja a la que
        // pertenece la coordenada calculada, si el modelo es de tipo Multiple TACA CHICA, MEDIANA Y GRANDE
        if (GUIA_BL.EsModeloMultiple(Convert.ToString(Session["IdCartelModelo"])))
        {
            /*****************************************************************************************************************************/
            // INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
            // PARA ESTA PARTE DEL CODIGO(OBTENER MATRIZ TEMPORAL PARA LA GENERACION DEL CARTEL MULTIPLE)
            /*****************************************************************************************************************************/
            Util.RegLogStepsPrintProcess2("Inicia la generacion de la matriz temporal de carteles multiples",
                                          Convert.ToString(Session["loginUser"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          0);
            /*****************************************************************************************************************************/

            // Obtener el registro con las coordenadas calculadas del cartel multiple y el numero de paginas que tendra el cartel
            int NroPaginas = 0;            
            Session["dtRegAuxImpresion"] = GUIA_BL.CalculaCoordenadasCartelesMultiples(Convert.ToString(Session["IdCartelModelo"]),
                                                                                       (DataTable)Session["dtRegImpresion"],
                                                                                       ref NroPaginas);
            Session["nroPaginas"] = NroPaginas;

            // Obtener las coordenadas de fin de cuadrante de la plantilla de la cartel
            Session["xFC"] = 0;
            Session["yFC"] = 0;           

            if (GUIA_BL.GetCoordenadaXFinCuadrante(Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(0, 4).ToString().Trim()),
                                                   Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(4, 2).ToString().Trim()),
                                                   Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(6, 1).ToString().Trim())) != "" &&
                GUIA_BL.GetCoordenadaYFinCuadrante(Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(0, 4).ToString().Trim()),
                                                   Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(4, 2).ToString().Trim()),
                                                   Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(6, 1).ToString().Trim())) != "")
            {
                Session["xFC"] = Convert.ToInt32(GUIA_BL.GetCoordenadaXFinCuadrante(Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(0, 4).ToString().Trim()),
                                                                                    Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(4, 2).ToString().Trim()),
                                                                                    Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(6, 1).ToString().Trim())));
                Session["yFC"] = Convert.ToInt32(GUIA_BL.GetCoordenadaYFinCuadrante(Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(0, 4).ToString().Trim()),
                                                                                    Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(4, 2).ToString().Trim()),
                                                                                    Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(6, 1).ToString().Trim())));
            }
            else
            {
                Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", "Error... No ha esblecido el Tag de final de cuadrante de diseño del cartel");

                /*****************************************************************************************************************************/
                // Limpia y destruye las variables de session de esta seccion
                /*****************************************************************************************************************************/
                Session["strNomPlantilla"] = null;
                Session["strPathCarteles"] = null;                
                Session["strPrintCartel"] = null;
                Session["IdNameCartel"] = null;
                Session["strPathServer"] = null;
                Session["nroPaginas"] = null;
                Session["xFC"] = null;
                Session["yFC"] = null;
                /*****************************************************************************************************************************/

                return;
            }

            /*****************************************************************************************************************************/
            // FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
            // PARA ESTA PARTE DEL CODIGO
            // (OBTENER MATRIZ TEMPORAL PARA LA GENERACION DEL CARTEL MULTIPLE)
            /*****************************************************************************************************************************/
            Util.RegLogStepsPrintProcess2("Finaliza la generacion de la matriz temporal de carteles multiples",
                                          Convert.ToString(Session["loginUser"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          Convert.ToInt32(Session["nroPaginas"]));
            /*****************************************************************************************************************************/

            //if (GUIA_BL.ProcesaCartelInterop(Convert.ToString(Session["strPathServer"]),
            //                                 Convert.ToString(Session["strNomPlantilla"]),
            //                                 Convert.ToString(Session["NomCartelModelo"]),
            //                                 (DataTable)Session["dtRegAuxImpresion"],
            //                                 Convert.ToString(Session["IdNameCartel"]),
            //                                 Convert.ToInt32(Session["Tienda"].ToString()),
            //                                 Convert.ToInt32(Session["nroPaginas"]),
            //                                 Convert.ToInt32(Session["xFC"]),
            //                                 Convert.ToInt32(Session["yFC"]),
            //                                 true) == false)

                if (GUIA_BL.GenerarCartel(Convert.ToString(Session["strPathServer"]),
                                          Convert.ToString(Session["strNomPlantilla"]),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          (DataTable)Session["dtRegAuxImpresion"],
                                          Convert.ToString(Session["IdNameCartel"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToInt32(Session["nroPaginas"]),
                                          Convert.ToInt32(Session["xFC"]),
                                          Convert.ToInt32(Session["yFC"]),
                                          true) == false)
            {
                Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_MIMPR"]);
            }
            else
            {
                // Insertar incidencia de proceso de impresion
                GUIA_BL.InsertarLogGuia(Convert.ToString(Session["loginUser"]),
                                        Convert.ToInt32(hdIdGuia.Value),
                                        Convert.ToInt32(Session["NroCopias"]),
                                        Convert.ToInt32(hidIdTienda.Value),
                                        Convert.ToInt32(hidIdCategoria.Value),
                                        Convert.ToInt32(hidPromocion.Value),
                                        Convert.ToString(Session["IdCartelModelo"]));

                Session["strPrintCartel"] = Convert.ToString(Session["strPathCarteles"]) + Convert.ToString(Session["NomCartelModelo"]) + Convert.ToString(Session["IdNameCartel"]) + ".pdf";

                // Abre el documento PDF en una pestaña del explorer distinta
                Session["script"] = @"<script type='text/javascript'>
                                        window.open('" + Convert.ToString(Session["strPrintCartel"]) + "','_blank'); self.focus();" +
                                     "</script>";

//                // Abre el documento PDF en un ventana de explorer distinta
//                Session["script"] = @"<script type='text/javascript'>
//                                        window.open('" + Convert.ToString(Session["strPrintCartel"]) + "', 'window','HEIGHT=800,WIDTH=1000,top=50,left=50,toolbar=yes,scrollbars=yes,toolbar=no,directories=no,status=yes,resizable=yes,copyhistory=no'); self.focus();" +
//                                    "</script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirPopUp", Convert.ToString(Session["script"]), false);
            }
        }
        else // si el modelo es de tipo Simple
        {
            /*****************************************************************************************************************************/
            // INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
            // PARA ESTA PARTE DEL CODIGO(OBTENER MATRIZ TEMPORAL PARA LA GENERACION DEL CARTEL SIMPLE)
            /*****************************************************************************************************************************/
            Util.RegLogStepsPrintProcess2("Inicia la generacion de la matriz temporal de carteles simples",
                                          Convert.ToString(Session["loginUser"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          0);
            /*****************************************************************************************************************************/

            // Obtener la matriz inicial que servira para formar el registro de productos a imprimir en los carteles
            Session["strCrtFiltro_1"] = null;
            Session["strCrtFiltro_1"] = "ID_CARTEL_MODELO = '" + Convert.ToString(Session["IdCartelModelo"]) + "'";
            Session["dtAux_1"] = Util.FiltraDataTable((DataTable)Session["dtRegImpresiones"], Convert.ToString(Session["strCrtFiltro_1"]));

            /*****************************************************************************************************************************/
            // Obtener el registro de productos a imprimir en los carteles
            /*****************************************************************************************************************************/
            Session["IdCartel"] = Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(0, 4).ToString().Trim());
            Session["IdModelo"] = Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(4, 2).ToString().Trim());
            Session["Digitos"] = Convert.ToInt32(Convert.ToString(Session["IdCartelModelo"]).Substring(6, 1).ToString().Trim());

            Session["nroPaginas"] = 0;

            for (int fila = 0; fila < ((DataTable)Session["dtAux_1"]).Rows.Count; fila++)
            {
                Session["nrolinea"] = Convert.ToInt32(((DataTable)Session["dtAux_1"]).Rows[fila][1].ToString());
                Session["nroCopias"] = Convert.ToInt32(((DataTable)Session["dtAux_1"]).Rows[fila][2].ToString());

                for (int z = 1; z <= Convert.ToInt32(Session["nroCopias"]); z++)
                {
                    Session["nroPaginas"] = Convert.ToInt32(Session["nroPaginas"]) + 1;

                    // Extraigo los datos con los que se generara el cartel
                    Session["dtRegistroImpresion"] = GUIA_BL.GetCartelGuiaImpr(Convert.ToInt32(Session["IdCartel"]),
                                                                               Convert.ToInt32(Session["IdModelo"]),
                                                                               Convert.ToInt32(Session["Digitos"]),
                                                                               Convert.ToInt32(hdIdGuia.Value),
                                                                               Convert.ToInt32(Session["nrolinea"]));

                    for (int zax = 0; zax < ((DataTable)Session["dtRegistroImpresion"]).Rows.Count; zax++)
                    {
                        ((DataTable)this.Session["dtRegImpresion1"]).Rows.Add(
                                                                              "",
                                                                              0,
                                                                              0,
                                                                              "",
                                                                              ((DataTable)Session["dtRegistroImpresion"]).Rows[zax][2], // Valor del Campo
                                                                              ((DataTable)Session["dtRegistroImpresion"]).Rows[zax][0], // Coordenada X
                                                                              ((DataTable)Session["dtRegistroImpresion"]).Rows[zax][1], // Coordenada Y
                                                                              ("Hoja" + Session["nroPaginas"]));

                        ((DataTable)this.Session["dtRegImpresion1"]).AcceptChanges();
                    }
                }
            }
            /*****************************************************************************************************************************/

            /*****************************************************************************************************************************/
            // FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
            // PARA ESTA PARTE DEL CODIGO
            // (OBTENER MATRIZ TEMPORAL PARA LA GENERACION DEL CARTEL SIMPLE)
            /*****************************************************************************************************************************/
            Util.RegLogStepsPrintProcess2("Finaliza la generacion de la matriz temporal de carteles simples",
                                          Convert.ToString(Session["loginUser"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          Convert.ToInt32(Session["nroPaginas"]));
            /*****************************************************************************************************************************/

            //if (GUIA_BL.ProcesaCartelInterop(Convert.ToString(Session["strPathServer"]),
            //                                 Convert.ToString(Session["strNomPlantilla"]),
            //                                 Convert.ToString(Session["NomCartelModelo"]),
            //                                 (DataTable)Session["dtRegImpresion1"],
            //                                 Convert.ToString(Session["IdNameCartel"]),
            //                                 Convert.ToInt32(Session["Tienda"].ToString()),
            //                                 Convert.ToInt32(Session["nroPaginas"]),
            //                                 0,
            //                                 0,
            //                                 false) == false)

                if (GUIA_BL.GenerarCartel(Convert.ToString(Session["strPathServer"]),
                                          Convert.ToString(Session["strNomPlantilla"]),
                                          Convert.ToString(Session["NomCartelModelo"]),
                                          (DataTable)Session["dtRegImpresion1"],
                                          Convert.ToString(Session["IdNameCartel"]),
                                          Convert.ToInt32(Session["Tienda"].ToString()),
                                          Convert.ToInt32(Session["nroPaginas"]),
                                          0,
                                          0,
                                          false) == false)
            {
                Util.RegisterAsyncAlert(upnlDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_MIMPR"]);
            }
            else
            {
                // Insertar incidencia de proceso de impresion
                GUIA_BL.InsertarLogGuia(Convert.ToString(Session["loginUser"]),
                                        Convert.ToInt32(hdIdGuia.Value),
                                        Convert.ToInt32(Session["NroCopias"]),
                                        Convert.ToInt32(hidIdTienda.Value),
                                        Convert.ToInt32(hidIdCategoria.Value),
                                        Convert.ToInt32(hidPromocion.Value),
                                        Convert.ToString(Session["IdCartelModelo"]));

                Session["strPrintCartel"] = Convert.ToString(Session["strPathCarteles"]) + Convert.ToString(Session["NomCartelModelo"]) + Convert.ToString(Session["IdNameCartel"]) + ".pdf";

                // Abre el documento PDF en una pestaña del explorer distinta
                Session["script"] = @"<script type='text/javascript'>
                                        window.open('" + Convert.ToString(Session["strPrintCartel"]) + "','_blank'); self.focus();" +
                                    "</script>";

//                // Abre el documento PDF en un ventana de explorer distinta
//                Session["script"] = @"<script type='text/javascript'>
//                                        window.open('" + Convert.ToString(Session["strPrintCartel"]) + "', 'window','HEIGHT=800,WIDTH=1000,top=50,left=50,toolbar=yes,scrollbars=yes,toolbar=no,directories=no,status=yes,resizable=yes,copyhistory=no'); self.focus();" +
//                                     "</script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirPopUp", Convert.ToString(Session["script"]), false);
            }
        }

        /*****************************************************************************************************************************/
        // (FIN DEL PROCESO DE IMPRESION DEL CARTEL)
        /*****************************************************************************************************************************/
        Util.RegLogStepsPrintProcess2("Finaliza le proceso de impresion",
                                      Convert.ToString(Session["loginUser"]),
                                      Convert.ToInt32(Session["Tienda"].ToString()),
                                      Convert.ToString(Session["NomCartelModelo"]),
                                      Convert.ToInt32(Session["nroPaginas"]));
        /*****************************************************************************************************************************/

        /*****************************************************************************************************************************/
        // Limpia y destruye las variables de session de esta seccion
        /*****************************************************************************************************************************/
        Session["strNomPlantilla"] = null;
        Session["strPathCarteles"] = null;        
        Session["strPrintCartel"] = null;
        Session["strPathServer"] = null;
        Session["IdNameCartel"] = null;
        Session["dtRegAuxImpresion"] = null;
        Session["dtAux_1"] = null;
        Session["strCrtFiltro_1"] = null;
        Session["IdCartel"] = null;
        Session["IdModelo"] = null;
        Session["Digitos"] = null;
        Session["nroPaginas"] = null;
        Session["xFC"] = null;
        Session["yFC"] = null;      
        /*****************************************************************************************************************************/     
    }

    protected void btnEditarDetalle_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        int IdLinea = Convert.ToInt32(row.Cells[6].Text);
        lblNomPromocion.Text = Server.HtmlDecode(row.Cells[5].Text);
        grvCamposObligDet.DataSource = GUIA_BL.GetDetalleGuiaCampo1(Convert.ToInt32(hdIdGuia.Value), IdLinea);
        grvCamposObligDet.DataBind();

        pnlDetalleImpresion.Visible = true;
        pnlImpresion.Visible = false;
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
            sScript.AppendFormat("  strSelCategoria = document.getElementById('{0}').value;", cboCategoriaB.ClientID);
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniB.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinB.ClientID);
            sScript.AppendLine("    if (strSelCategoria  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgCCCPNoSelCategoria);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboCategoriaB.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniB.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function SelectAll(spanChk,grdClientID){");
            sScript.AppendLine("    var IsChecked = spanChk.checked;");
            sScript.AppendLine("    var Chk = spanChk;");
            sScript.AppendLine("    Parent = document.getElementById(grdClientID);");
            sScript.AppendLine("    var items = Parent.getElementsByTagName('input');");
            sScript.AppendLine("    for(i=0;i<items.length;i++){");
            sScript.AppendLine("      if(items[i].type=='checkbox'){");
            sScript.AppendLine("          items[i].click();");
            sScript.AppendLine("      }");
            sScript.AppendLine("    }");
            sScript.AppendLine("}");

            sScript.AppendLine("function CheckeaCheck(chk, txtID){");
            sScript.AppendLine("   if(chk.checked){");
            sScript.AppendLine("      document.getElementById(txtID).value='1';");
            sScript.AppendLine("   }else{");
            sScript.AppendLine("      document.getElementById(txtID).value='';");
            sScript.AppendLine("   }");
            sScript.AppendLine("}");

            sScript.AppendLine("function SoloNumeros(e){");
            sScript.AppendLine("    var key;");
            sScript.AppendLine("    if(window.event){");
            sScript.AppendLine("        key = e.keyCode;");
            sScript.AppendLine("   }else if(e.which){");
            sScript.AppendLine("       key = e.which;");
            sScript.AppendLine("   }");
            sScript.AppendLine("   if (key < 48 || key > 57){");
            sScript.AppendLine("    return false;");
            sScript.AppendLine("   }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValidFechas()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniB.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinB.ClientID);
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniB.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function ComparaFechas(dtf1, dtf2)");
            sScript.AppendLine("{");
            sScript.AppendLine("//la fecha 2 debe ser necesariamente mayor a fecha1");
            sScript.AppendLine("   if(dtf1.length==0 || dtf1.length==0 )");
            sScript.AppendLine("   {");
            sScript.AppendLine("       return true;");
            sScript.AppendLine("   }");
            sScript.AppendLine("	fi = dtf1.split('/');");
            sScript.AppendLine("	ff = dtf2.split('/');");
            sScript.AppendLine("	_fechai = fi[2]*10000 + fi[1]*100 + fi[0];");
            sScript.AppendLine("	_fechaf = ff[2]*10000 + ff[1]*100 + ff[0];");
            sScript.AppendLine("	n = _fechaf - _fechai;");
            sScript.AppendLine("	if(n>=0) return true;");
            sScript.AppendLine("	else return false;");
            sScript.AppendLine("}");
            sScript.AppendLine("");

            sScript.AppendLine("function IsNullTextoBusqueda()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  strDescripcion = document.getElementById('{0}').value;", txtNomProdBus.ClientID);
            sScript.AppendLine("    if (strDescripcion.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('{0}');", Resources.Mensajes.msgImpresionBusqTextBusqNull);
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomProdBus.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return true;");
            sScript.AppendLine("}");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }

    public Boolean VerificarStock(string SkuProducto) 
    {
        string URL = "https://api-spsa.pe:8443/supermercados-peruanos-sa/prd-api-spsa/stock/api/v1/stock/PVEA/" + SkuProducto;
        int stock;
        bool resp = false;
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(URL);
            myReq.Method = "GET";
            myReq.ContentType = "application/json";
            myReq.Accept = "application/json";
            WebResponse myResponse = myReq.GetResponse();
            Stream rebut = myResponse.GetResponseStream();
            StreamReader readStream = new StreamReader(rebut, Encoding.UTF8);
            string info = readStream.ReadToEnd();            
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SCE_CONSULTA_STOCK_RESPONSE objeto = (SCE_CONSULTA_STOCK_RESPONSE)javaScriptSerializer.Deserialize<SCE_CONSULTA_STOCK_RESPONSE>(info);

            int code = Convert.ToInt16(objeto.code);
            if(code==1)
            {                
                resp = (objeto.data.totalStockRecords>0);
            }
            
            myResponse.Close();
            readStream.Close();                     
        }
        catch (Exception e1)
        {
            Console.Out.WriteLine("-----------------");
            Console.Out.WriteLine(e1.Message);
            Response.Write(e1.ToString());
        }
        return resp;
    } 

    protected void grvGuias_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvDetalleGuias_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
