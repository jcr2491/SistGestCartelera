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

public partial class CartCreacManual : MyBasePage
{
    SCE_TIENDA_BL TIENDA_BL;
    SCE_CATEGORIA_BL CATEGORIA_BL;
    SCE_PROMOCION_BL PROMOCION_BL;
    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_GUIA_BL GUIA_BL;
    SCE_GUIA_DET_BE GUIA_DET_BE;
    SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE;    

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegProducto()
    {
        DataTable dtRegProducto = new DataTable();

        dtRegProducto.Columns.Clear();

        DataColumn workCol0 = dtRegProducto.Columns.Add("ID_LINEA", typeof(Int32));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegProducto.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegProducto.Columns.Add("NOM_CAMPO", typeof(String));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegProducto.Columns.Add("VALOR", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        return dtRegProducto;
    }

    private DataTable CrearNewRegDetalleGuiaCampo()
    {
        DataTable dtNewRegDetalleGuiaCampo = new DataTable();

        dtNewRegDetalleGuiaCampo.Columns.Clear();

        DataColumn workCol0 = dtNewRegDetalleGuiaCampo.Columns.Add("ID_LINEA", typeof(Int32));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtNewRegDetalleGuiaCampo.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtNewRegDetalleGuiaCampo.Columns.Add("NOM_CAMPO", typeof(String));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtNewRegDetalleGuiaCampo.Columns.Add("VALOR", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        return dtNewRegDetalleGuiaCampo;
    }

    private void LoadData()
    {
        this.cboTiendaB.DataSource = TIENDA_BL.Listar();
        this.cboTiendaB.DataValueField = "ID_TIENDA";
        this.cboTiendaB.DataTextField = "NOM_TIENDA";
        this.cboTiendaB.DataBind();
        this.cboTiendaB.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));

        this.cboTiendaD.DataSource = TIENDA_BL.Listar();
        this.cboTiendaD.DataValueField = "ID_TIENDA";
        this.cboTiendaD.DataTextField = "NOM_TIENDA";
        this.cboTiendaD.DataBind();
        this.cboTiendaD.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));

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

        this.cboCategoriaD.DataSource = CATEGORIA_BL.ListarConf();
        this.cboCategoriaD.DataValueField = "ID_CATEGORIA";
        this.cboCategoriaD.DataTextField = "NOM_CATEGORIA";
        this.cboCategoriaD.DataBind();
        this.cboCategoriaD.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));

        this.cboPromocionD.DataSource = PROMOCION_BL.ListarConf();
        this.cboPromocionD.DataValueField = "ID_PROMOCION";
        this.cboPromocionD.DataTextField = "NOM_PROMOCION";
        this.cboPromocionD.DataBind();
        this.cboPromocionD.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));

        this.cboEstadoB.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));
        this.cboEstadoB.Items.Add(new ListItem("EN TRABAJO", "1"));
        this.cboEstadoB.Items.Add(new ListItem("LISTO PARA IMPRESIÓN", "2"));      
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ScriptCliente();           
        }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        TIENDA_BL = new SCE_TIENDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CATEGORIA_BL = new SCE_CATEGORIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        PROMOCION_BL = new SCE_PROMOCION_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        btnBusquedaGuia.OnClientClick = "return IsValid3();";

        btnCambiarEst.OnClientClick = string.Format("return confirmacion('{0}');", "Desea que la guia pase a impresión?");
        btnCancGDG.OnClientClick = "return Limpiar();";

        btnAgregarDet.OnClientClick = "return IsValid();";
        btnCEG.OnClientClick = "return IsValid2();";
        btnVerPDF.OnClientClick = "return IsValid1();";

        txtFecIniB.Attributes.Add("readonly", "readonly");
        txtFecFinB.Attributes.Add("readonly", "readonly");
        txtFecIniD.Attributes.Add("readonly", "readonly");
        txtFecFinD.Attributes.Add("readonly", "readonly");        

        /*Si el usuario es administrador podra
        buscar por cualquier tienda*/
        if ((bool)Session["UsuADMPC"] == true)
        {
            lblLocalB.Visible = true;
            cboTiendaB.Visible = true;
        }
        else
        {
            lblLocalB.Visible = false;
            cboTiendaB.Visible = false;
        }

        if (!Page.IsPostBack)
        {
            this.Session["linea"] = 0;
            this.Session["varEstado"] = 1;
            
            //********************************************************************
            // Variable de session que toma la estructura del DataTable Global
            // para manejar las operaciones a nivel multiusuario
            //********************************************************************
            this.Session["dtRegProducto"] = CrearDtRegProducto();
            ((DataTable)this.Session["dtRegProducto"]).Clear();
            //********************************************************************
            
            //********************************************************************
            // Variable de session que toma la estructura del DataTable Global
            // para manejar las operaciones a nivel multiusuario
            //********************************************************************
            this.Session["dtNewRegDetalleGuiaCampo"] = CrearNewRegDetalleGuiaCampo();
            ((DataTable)this.Session["dtNewRegDetalleGuiaCampo"]).Clear();
            //********************************************************************

            LoadData();
        }        
    }

    protected void btnBusquedaGuia_Click(object sender, EventArgs e)
    {
        int PageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PageSize"]);
        int PageCount = 0;

        txtNroPagina.Text = "";

        if ((bool)Session["UsuADMPC"] == true)
        {
            Busqueda(txtFecIniB.Text,
                     txtFecFinB.Text,
                     Convert.ToInt32(cboTiendaB.SelectedValue),
                     Convert.ToInt32(cboPromocionB.SelectedValue),
                     Convert.ToInt32(cboCategoriaB.SelectedValue),
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
                     txtNomGuiaB.Text,
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
                         Convert.ToInt32(cboEstadoB.SelectedValue),
                         txtNomGuiaB.Text,
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
                         Convert.ToInt32(cboEstadoB.SelectedValue),
                         txtNomGuiaB.Text,
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
                         int Local,
                         int IdPpromocion,
                         int IdCategoria,
                         int Estado,
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

        lstGuia = GUIA_BL.Buscar(FecIniBC,
                                 FecFinBC,
                                 Local,
                                 IdPpromocion,
                                 IdCategoria,
                                 Estado,
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
            upnlCabecera.Update();
            this.grvGuias.DataSource = null;
            this.grvGuias.DataBind();
            Util.RegisterAsyncAlert(upnlCabecera, "__Alerta__", Resources.Mensajes.msgBusquedaSinRegistros);
        }
    }

    protected void btnCrearGuia_Click(object sender, EventArgs e)
    {
        this.Session["operacion"] = true;
        this.Session["linea"] = 0;
        hdIdGuia.Value = "0";

        ((DataTable)this.Session["dtRegProducto"]).Clear();
 
        //this.grvDetalleGuias.DataSource = dtRegProducto;
        this.grvDetalleGuias.DataSource = null;
        this.grvDetalleGuias.DataBind();

        uplDetalle.Update();

        pnlDetalleGuia.Visible = true;
        pnlCabecera.Visible = false;

        /*Si el usuario es administrador podra
        registrar guias en cualquier tienda*/
        if ((bool)Session["UsuADMPC"] == true)
        {
            lblLocalD.Visible = true;
            cboTiendaD.Visible = true;
        }
        else
        {
            lblLocalD.Visible = false;
            cboTiendaD.Visible = false;
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        hdIdGuia.Value = "0";
        this.Session["operacion"] = false;

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hdIdGuia.Value = Server.HtmlDecode(row.Cells[2].Text);

        this.Session["hdIdGuia"] = hdIdGuia.Value;

        txtFecIniD.Text = Server.HtmlDecode(row.Cells[8].Text);
        txtFecFinD.Text = Server.HtmlDecode(row.Cells[9].Text);
        cboTiendaD.SelectedValue = Server.HtmlDecode(row.Cells[10].Text);

        hdIdPromocion.Value = Server.HtmlDecode(row.Cells[4].Text);
        txtPromocionD.Text = PROMOCION_BL.GetNombrePromocion(Convert.ToInt32(hdIdPromocion.Value));
        hdIdCategoria.Value = Server.HtmlDecode(row.Cells[6].Text);
        txtCategoriaD.Text = CATEGORIA_BL.GetNombreCategoria(Convert.ToInt32(hdIdCategoria.Value));

        cboPromocionD.SelectedValue = Server.HtmlDecode(row.Cells[4].Text);
        cboCategoriaD.SelectedValue = Server.HtmlDecode(row.Cells[6].Text);

        txtNomGuia.Text = Server.HtmlDecode(row.Cells[3].Text);

        //----------------------------------------------------------------------------------------------

        this.Session["varEstado"] = Convert.ToInt32(Server.HtmlDecode(row.Cells[12].Text));

        //Llena el registro temporal
        this.Session["dtRegProducto"] = GUIA_BL.GetDetalleGuiaCampo(Convert.ToInt32(hdIdGuia.Value));

        //Llenamos la grilla con el registro temporal Pivoteado
        this.grvDetalleGuias.DataSource = GUIA_BL.PivotDtDetalleGuiaCampos((DataTable)this.Session["dtRegProducto"], "NOM_CAMPO");
        this.grvDetalleGuias.DataBind();

        /****************************************************************/
        /* AQUI EXISTE UN PROBLEMA DE CONCURRENCIA A NIVEL DE REGISTRO
         * YA QUE EL USUARIO HA PODIDO ELIMINAR UN REGISTRO DEL DETALLE Y
         * SE PERDIO LA CORRELATIVIDAD PUDIENDO GENERAR ERROR DE CLAVE 
         * PRIMARIA A LA HORA DE REALIZAR UNA MODIFICACION DEL DETALLE DE
         * LA GUIA ADEMAS LA LOGICA DE GENERACION DEL CORRELATIVO DEBE 
         * GESTINARSE DESDE LA BD O EL SERVIDOR*/
        /****************************************************************/
        //Establece el numero de lineas
        //this.Session["linea"] = grvDetalleGuias.Rows.Count + 1;

        this.Session["linea"] = GUIA_BL.GetMaxLineaDetalleGuia(Convert.ToInt32(hdIdGuia.Value));
        /****************************************************************/

        cboCategoriaD.Enabled = false;
        cboPromocionD.Enabled = false;

        cboCategoriaD.Visible = false;
        cboPromocionD.Visible = false;

        txtPromocionD.Visible = true;
        txtCategoriaD.Visible = true;

        btnCambiarEst.Visible = true;

        pnlDetalleGuia.Visible = true;
        pnlCabecera.Visible = false;

        pnlBarraPaginadora.Visible = false;

        uplDetalle.Update();
       
        if ((int)this.Session["varEstado"] == 2) //Guia en impresion
        {
            btnCambiarEst.Enabled = false;
            btnAgregarDet.Enabled = false;
            btnCEG.Enabled = false;

            txtFecIniD.Enabled = false;
            txtFecFinD.Enabled = false;

            /*Si el usuario es administrador podra
            registrar guias en cualquier tienda*/
            if ((bool)Session["UsuADMPC"] == true)
            {
                lblLocalD.Visible = true;
                cboTiendaD.Visible = true;
                cboTiendaD.Enabled = false;
            }
            else
            {
                lblLocalD.Visible = false;
                cboTiendaD.Visible = false;
            }            

            txtNomGuia.Enabled = false;
        }
        else //Guia en trabajo
        {            
            btnCambiarEst.Enabled = true;
            btnAgregarDet.Enabled = true;
            btnCEG.Enabled = true;

            txtFecIniD.Enabled = true;
            txtFecFinD.Enabled = true;

            /*Si el usuario es administrador podra
            registrar guias en cualquier tienda*/
            if ((bool)Session["UsuADMPC"] == true)
            {
                lblLocalD.Visible = true;
                cboTiendaD.Visible = true;
                cboTiendaD.Enabled = true;
            }
            else
            {
                lblLocalD.Visible = false;
                cboTiendaD.Visible = false;
            } 

            txtNomGuia.Enabled = true;           
        }
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        hdIdGuia.Value = "0";
        this.Session["operacion"] = false;

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;
        hdIdGuia.Value = Server.HtmlDecode(row.Cells[2].Text);

        GUIA_BL.EliminarGuiaManual(Convert.ToInt32(hdIdGuia.Value));

        btnBusquedaGuia_Click(sender, e);

        Util.RegisterAsyncAlert(upnlCabecera, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_ED"]);
    }    

    protected void btnCancGDG_Click(object sender, EventArgs e)
    {
        this.Session["linea"] = 0;

        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        ////Limpia los registros temporales
        ((DataTable)this.Session["dtNewRegDetalleGuiaCampo"]).Clear();        

        cboCategoriaD.Visible = true;
        cboPromocionD.Visible = true;

        txtPromocionD.Visible = false;
        txtCategoriaD.Visible = false;

        cboCategoriaD.Enabled = true;
        cboPromocionD.Enabled = true; 

        btnCambiarEst.Visible = false;

        btnCambiarEst.Enabled = true;
        btnAgregarDet.Enabled = true;
        btnCEG.Enabled = true;

        txtFecIniD.Enabled = true;
        txtFecFinD.Enabled = true;
        cboTiendaD.Enabled = true;
        txtNomGuia.Enabled = true;
      
        pnlDetalleGuia.Visible = false;
        pnlDetalleGuiaCampo.Visible = false;
        pnlDetalleGuiaCampoPDF.Visible = false;
        pnlCabecera.Visible = true;

        upnlCabecera.Update();
    }   

    protected void btnAgregarDet_Click(object sender, EventArgs e)
    {
        if ((bool)Session["UsuADMPC"] == true)
        {
            if (cboTiendaD.SelectedValue == "0")
            {
                Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No ha selecionado la tienda.");
                System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
                sbScript.AppendFormat("document.getElementById('{0}').focus();", cboTiendaD.ClientID);
                Util.RegisterScript(upnlCabecera, "__ConfigurarCtroles__", sbScript.ToString());
                return;
            }
        }

        if (hdIdGuia.Value == "0")
        {
            if (cboCategoriaD.SelectedValue == "0")
            {
                Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No ha seleccionado la categoría");
            }

            if (cboPromocionD.SelectedValue == "0")
            {
                Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No ha seleccionado la promoción");
            }
        }

        if (CARTEL_MODELO_BL.ExisteParCategPromoSeleccionado(Convert.ToInt32(cboCategoriaD.SelectedValue), 
                                                             Convert.ToInt32(cboPromocionD.SelectedValue)) == true)
        {
            this.Session["flgDGG"] = true; //flgDGG == true Modo Insercion del registro del producto

            this.grvCamposOblig.DataSource = GUIA_BL.GetNewRegDetalleGuiaCampo(Convert.ToInt32(cboPromocionD.SelectedValue),
                                                                               Convert.ToInt32(cboCategoriaD.SelectedValue));
            this.grvCamposOblig.DataBind();


            this.Session["linea"] = (int)this.Session["linea"] + 1;


            cboCategoriaD.Enabled = false;
            cboPromocionD.Enabled = false;

            pnlDetalleGuiaCampo.Visible = true;
            pnlDetalleGuiaCampoPDF.Visible = false;
        }
        else
        {
            Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No existe Carteles-Modelo Configurados Para esta selección de Categoría-Promoción");
        }
    }

    protected void btnCEG_Click(object sender, EventArgs e)
    {
        if (((DataTable)Session["dtRegProducto"]).Rows.Count > 0)
        {
            //Captura todos los datos para la grabacion de la Guia
            List<SCE_GUIA_DET_BE> lstDetGuias = new List<SCE_GUIA_DET_BE>();
            List<SCE_GUIA_DET_CAMPO_BE> lstDetGuiasCampo = new List<SCE_GUIA_DET_CAMPO_BE>();

            //preventivamente elimina los posibles duplicados que se esten generando en registro
            ((DataTable)this.Session["dtRegProducto"]).DefaultView.Sort = "ID_CAMPO";

            DataTable dt = ((DataTable)this.Session["dtRegProducto"]).DefaultView.ToTable(true, "ID_LINEA", "ID_CAMPO", "NOM_CAMPO", "VALOR");

            foreach (GridViewRow Row in grvDetalleGuias.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                GUIA_DET_BE = new SCE_GUIA_DET_BE();
                GUIA_DET_BE.ID_GUIA = Convert.ToInt32(hdIdGuia.Value);
                GUIA_DET_BE.ID_LINEA = Convert.ToInt32(grvDetalleGuias.Rows[i].Cells[3].Text);

                if (hdIdGuia.Value == "0")
                {
                    GUIA_DET_BE.ID_CATEGORIA = Convert.ToInt32(cboCategoriaD.SelectedValue);
                    GUIA_DET_BE.ID_PROMOCION = Convert.ToInt32(cboPromocionD.SelectedValue);
                }
                else
                {
                    GUIA_DET_BE.ID_CATEGORIA = Convert.ToInt32(hdIdCategoria.Value);
                    GUIA_DET_BE.ID_PROMOCION = Convert.ToInt32(hdIdPromocion.Value);
                }

                lstDetGuias.Add(GUIA_DET_BE);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GUIA_DET_CAMPO_BE = new SCE_GUIA_DET_CAMPO_BE();
                GUIA_DET_CAMPO_BE.ID_GUIA = Convert.ToInt32(hdIdGuia.Value);
                GUIA_DET_CAMPO_BE.ID_LINEA = Convert.ToInt32(dt.Rows[i][0]);
                GUIA_DET_CAMPO_BE.ID_CAMPO = Convert.ToInt32(dt.Rows[i][1]);
                GUIA_DET_CAMPO_BE.VALOR = Convert.ToString(dt.Rows[i][3]);
                lstDetGuiasCampo.Add(GUIA_DET_CAMPO_BE);
            }

            if (hdIdGuia.Value == "0")
            {
                /*Si el usuario es administrador podra
                registrar guias en cualquier tienda*/
                if ((bool)Session["UsuADMPC"] == true)
                {
                    //Graba la cabecera y el detalle de la Guia
                    hdIdGuia.Value = Convert.ToString(GUIA_BL.InsertarGuia(txtNomGuia.Text.Trim(),
                                                                           1,
                                                                           Convert.ToDateTime(txtFecIniD.Text),
                                                                           Convert.ToDateTime(txtFecFinD.Text),
                                                                           Convert.ToInt32(cboTiendaD.SelectedValue),
                                                                           0,
                                                                           Convert.ToString(Session["loginUser"]),
                                                                           lstDetGuias,
                                                                           lstDetGuiasCampo)
                                                      );
                }
                else
                {
                    //Graba la cabecera y el detalle de la Guia
                    hdIdGuia.Value = Convert.ToString(GUIA_BL.InsertarGuia(txtNomGuia.Text.Trim(),
                                                      1,
                                                      Convert.ToDateTime(txtFecIniD.Text),
                                                      Convert.ToDateTime(txtFecFinD.Text),
                                                      Convert.ToInt32(Session["Tienda"].ToString()),
                                                      0,
                                                      Convert.ToString(Session["loginUser"]),
                                                      lstDetGuias,
                                                      lstDetGuiasCampo)
                                                      );
                }

                Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_AD"]);
            }
            else
            {
                /*Si el usuario es administrador podra
                registrar guias en cualquier tienda*/
                if ((bool)Session["UsuADMPC"] == true)
                {
                    //Graba la cabecera y el detalle de la Guia
                    GUIA_BL.ActualizarGuia(Convert.ToInt32(hdIdGuia.Value),
                                           txtNomGuia.Text.Trim(),
                                           Convert.ToDateTime(txtFecIniD.Text),
                                           Convert.ToDateTime(txtFecFinD.Text),
                                           Convert.ToInt32(cboTiendaD.SelectedValue),
                                           0,
                                           1,
                                           Convert.ToString(Session["loginUser"]),
                                           lstDetGuias,
                                           lstDetGuiasCampo);
                }
                else
                {
                    //Graba la cabecera y el detalle de la Guia
                    GUIA_BL.ActualizarGuia(Convert.ToInt32(hdIdGuia.Value),
                                           txtNomGuia.Text.Trim(),
                                           Convert.ToDateTime(txtFecIniD.Text),
                                           Convert.ToDateTime(txtFecFinD.Text),
                                           Convert.ToInt32(Session["Tienda"].ToString()),
                                           0,
                                           1,
                                           Convert.ToString(Session["loginUser"]),
                                           lstDetGuias,
                                           lstDetGuiasCampo);                
                }                

                Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_MD"]);
            }            

            this.Session["varEstado"] = 1;
            
            //Limpia la grilla de guias
            ViewState["listadoGuias"] = null;
            this.grvGuias.DataSource = ViewState["listadoGuias"];
            this.grvGuias.DataBind();

            hdIdPromocion.Value = cboPromocionD.SelectedValue;            
            txtPromocionD.Text = PROMOCION_BL.GetNombrePromocion(Convert.ToInt32(hdIdPromocion.Value));
            hdIdCategoria.Value = cboCategoriaD.SelectedValue;            
            txtCategoriaD.Text = CATEGORIA_BL.GetNombreCategoria(Convert.ToInt32(hdIdCategoria.Value));

            cboPromocionD.Visible = false;
            cboCategoriaD.Visible = false;

            txtPromocionD.Visible = true;
            txtCategoriaD.Visible = true;
                       
            //pnlDetalleGuia.Visible = false;
            pnlDetalleGuiaCampo.Visible = false;
            pnlDetalleGuiaCampoPDF.Visible = false;
            //pnlCabecera.Visible = true;

            this.Session["operacion"] = false;
        }
        else
        {
            Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_RPV"]);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pnlDetalleGuiaCampo.Visible = false;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //Modo Inserción del registro productos
        if ((bool)this.Session["flgDGG"] == true)
        {
            foreach (GridViewRow Row in grvCamposOblig.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                //Valida que ningun valor en las cajas de texto de la grilla esten vacias
                TextBox myTexto;
                myTexto = new TextBox();
                myTexto = Row.FindControl("txtValor") as TextBox;
                if (myTexto.Text == "")
                {
                    Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_VCO"]);
                    return;
                }
                else
                {

                    ((DataTable)this.Session["dtRegProducto"]).Rows.Add((int)this.Session["linea"],
                                                                        Convert.ToInt32(grvCamposOblig.Rows[i].Cells[1].Text),
                                                                        Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()),
                                                                        myTexto.Text.Trim());

                    ((DataTable)this.Session["dtRegProducto"]).AcceptChanges();
                }
            }
        }
        else  // flgDGG == false Modo Edición del registro de productos
        {
            //Eliminamos el grupo de registros correspondientes a la linea seleccionada
            DataRow[] DrArrDel = ((DataTable)this.Session["dtRegProducto"]).Select("ID_LINEA = " + (int)this.Session["lineaSeleccionada"]);
            foreach (DataRow DrDel in DrArrDel)
            {
                ((DataTable)Session["dtRegProducto"]).Rows.Remove(DrDel);
            }

            //Insertamos el registro modificado
            foreach (GridViewRow Row in grvCamposOblig.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                //VALIDA QUE NINGUN VALOR EN LAS CAJAS DE TEXTO DE LA GRILLA ESTEN VACIAS
                TextBox myTexto;
                myTexto = new TextBox();
                myTexto = Row.FindControl("txtValor") as TextBox;
                if (myTexto.Text.Length == 0 || myTexto.Text == "")
                {
                    Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "Falta llenar de valores en algunos campos del registro del producto");
                    return;
                }
                else
                {
                    ((DataTable)this.Session["dtRegProducto"]).Rows.Add((int)this.Session["lineaSeleccionada"],
                                                                   Convert.ToInt32(grvCamposOblig.Rows[i].Cells[1].Text),
                                                                   Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()),
                                                                   myTexto.Text.Trim());

                    ((DataTable)this.Session["dtRegProducto"]).AcceptChanges();
                }
            }
        }

        //Ordenamos los registros del DataTable
        ((DataTable)this.Session["dtRegProducto"]).DefaultView.Sort = "ID_LINEA";

        //Llenamos la grilla con el DataTable Pivoteado
        this.grvDetalleGuias.DataSource = GUIA_BL.PivotDtDetalleGuiaCampos((DataTable)this.Session["dtRegProducto"], "NOM_CAMPO");
        this.grvDetalleGuias.DataBind();

        pnlDetalleGuiaCampo.Visible = false;
    }

    protected void grvDetalleGuias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            // Se obtiene indice de la row seleccionada
            int index = Convert.ToInt32(e.CommandArgument);
         
            // Obtengo el id de la entidad que se esta editando
            // en este caso de la entidad registro de productos            
            int id = Convert.ToInt32(grvDetalleGuias.DataKeys[index].Value);

            this.Session["lineaSeleccionada"] = id;

            if (e.CommandName == "Editar")
            {
                if (hdIdGuia.Value != "0")
                {
                    if (GUIA_BL.EsVigente(Convert.ToInt32(hdIdGuia.Value)) == false)
                    {
                        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No se puede realizar la operacion por que la guia no esta vigente");
                        return;
                    }

                    if ((int)this.Session["varEstado"] == 2)
                    {
                        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No se puede realizar la operacion por que la guia se encuentra en impresión");
                        return;
                    }
                }

                this.Session["flgDGG"] = false; //flgDGG == false Modo Edición del registro de productos              

                string strCrtFiltro = null;
                strCrtFiltro = "ID_LINEA = " + id;
                Session["dtNewRegDetalleGuiaCampo"] = GUIA_BL.FiltraDataTable((DataTable)this.Session["dtRegProducto"], strCrtFiltro);

                this.grvCamposOblig.DataSource = (DataTable)this.Session["dtNewRegDetalleGuiaCampo"];
                this.grvCamposOblig.DataBind();

                pnlDetalleGuiaCampoPDF.Visible = false;
                pnlDetalleGuiaCampo.Visible = true;

            }
            else if (e.CommandName == "Eliminar")
            {
                if (hdIdGuia.Value != "0")
                {
                    if (GUIA_BL.EsVigente(Convert.ToInt32(hdIdGuia.Value)) == false)
                    {
                        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No se puede realizar la operacion por que la guia no esta vigente");
                        return;
                    }

                    if ((int)this.Session["varEstado"] == 2)
                    {
                        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", "No se puede realizar la operacion por que la guia se encuentra en impresión");
                        return;
                    }
                }

                //Eliminamos el grupo de registros correspondientes a la linea seleccionada
                DataRow[] DrArrDel = ((DataTable)this.Session["dtRegProducto"]).Select("ID_LINEA = " + id);
                foreach (DataRow DrDel in DrArrDel)
                {
                    ((DataTable)this.Session["dtRegProducto"]).Rows.Remove(DrDel);
                }

                if (((DataTable)this.Session["dtRegProducto"]).Rows.Count <= 0)
                {
                    this.Session["linea"] = 0;
                    cboCategoriaD.Enabled = true;
                    cboPromocionD.Enabled = true;
                }

                this.grvDetalleGuias.DataSource = GUIA_BL.PivotDtDetalleGuiaCampos((DataTable)this.Session["dtRegProducto"], "NOM_CAMPO");
                this.grvDetalleGuias.DataBind();              

                pnlDetalleGuiaCampo.Visible = false;
                pnlDetalleGuiaCampoPDF.Visible = false;
            }
            else if (e.CommandName == "VerPDF")
            {
                this.cboCartelesPDF.DataSource = CARTEL_MODELO_BL.cboCartelModeloCMCP(Convert.ToInt32(hdIdCategoria.Value),
                                                                                      Convert.ToInt32(hdIdPromocion.Value),
                                                                                      Convert.ToInt32(hdIdGuia.Value),
                                                                                      (int)this.Session["lineaSeleccionada"]);
                if (hdIdGuia.Value != "0")
                {
                    grvCamposObligPDF.DataSource = GUIA_BL.GetDetalleGuiaCampo1(Convert.ToInt32(hdIdGuia.Value), id);
                    grvCamposObligPDF.DataBind();

                    if (grvCamposObligPDF.Rows.Count > 0)
                    {
                        this.cboCartelesPDF.DataValueField = "CODIGO";
                        this.cboCartelesPDF.DataTextField = "DESCRIPCION";
                        this.cboCartelesPDF.DataBind();
                        this.cboCartelesPDF.Items.Insert(0, new ListItem("----------------Seleccionar----------------", "0"));

                        pnlDetalleGuiaCampoPDF.Visible = true;
                        pnlDetalleGuiaCampo.Visible = false;
                    }
                    else
                    {
                        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_GNG"]);
                    }
                }
                else
                {
                    Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_GNG"]);
                }                
            }
        }
        catch 
        {

        }
    }

    protected void grvDetalleGuias_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].CssClass = "ColumnaOculta";           
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                e.Row.Cells[3].ControlStyle.CssClass = "ColumnaOculta";               
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        } 
    }    

    protected void btnVerPDF_Click(object sender, EventArgs e)
    {
        string strCartel = string.Empty;
        string strSourcePath = System.Configuration.ConfigurationManager.AppSettings["PATH_SERVER"];
        string strNomPlantilla = CARTEL_MODELO_BL.ObtenerPlantilla(cboCartelesPDF.SelectedValue);
        string strPath = System.Configuration.ConfigurationManager.AppSettings["RutaCarteleria"];

        string IdNameCartel = Convert.ToString(Session["loginUser"]) + "_" +
                              DateTime.Now.Day.ToString("00") +
                              DateTime.Now.Month.ToString("00") +
                              DateTime.Now.Hour.ToString("00") +
                              DateTime.Now.Minute.ToString("00") +
                              DateTime.Now.Second.ToString("00");

        if (GUIA_BL.ProcesaCartel(strSourcePath,
                                  strNomPlantilla,
                                  cboCartelesPDF.SelectedValue,
                                  cboCartelesPDF.SelectedItem.ToString(),
                                  Convert.ToInt32(hdIdGuia.Value),
                                  (int)this.Session["lineaSeleccionada"],
                                  IdNameCartel) == false)
        {
            Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_NEXF"]);
        }
        else
        {
            strCartel = strPath + cboCartelesPDF.SelectedItem.ToString().Trim() + IdNameCartel + ".pdf";

            //Abre el documento PDF en un ventana de explorer distinta
            string script = @"<script type='text/javascript'>
                                window.open('" + strCartel + "', 'window','HEIGHT=800,WIDTH=1000,top=50,left=50,toolbar=yes,scrollbars=yes,toolbar=no,directories=no,status=yes,resizable=yes,copyhistory=no');" +
                             "</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirPopUp", script, false);
        }           
    }

    protected void btnCancelarPDF_Click(object sender, EventArgs e)
    {
        pnlDetalleGuiaCampoPDF.Visible = false;
    }    

    protected void btnCambiarEst_Click(object sender, EventArgs e)
    {
        GUIA_BL.ActualizarEstadoGuia(Convert.ToInt32(hdIdGuia.Value));
        Util.RegisterAsyncAlert(uplDetalle, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_MEG"]);
    }  

    protected void grvGuias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }

    protected void grvDetalleGuias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar1");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
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
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniD.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinD.ClientID);           
            sScript.AppendFormat("  strNomGuia = document.getElementById('{0}').value;", txtNomGuia.ClientID);
            sScript.AppendLine("    if (strFecIni.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de inicio de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");            
            sScript.AppendLine("    if (strNomGuia.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha colocado el nombre de la guia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuia.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('Lo longitud del nombre de la guia sobrepasa el establecido por el sistema');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuia.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid1()");
            sScript.AppendLine("{");            
            sScript.AppendFormat("  strCartelPDF = document.getElementById('{0}').value;", cboCartelesPDF.ClientID);
            sScript.AppendLine("    if (strCartelPDF  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No selecionado el cartel');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboCartelesPDF.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid2()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniD.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinD.ClientID);           
            sScript.AppendFormat("  strNomGuia = document.getElementById('{0}').value;", txtNomGuia.ClientID);
            sScript.AppendLine("    if (strFecIni.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de inicio de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniD.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");            
            sScript.AppendLine("    if (strNomGuia.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha colocado el nombre de la guia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuia.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('Lo longitud del nombre de la guia sobrepasa el establecido por el sistema');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuia.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return confirm(msg);");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid3()");
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

            sScript.AppendLine("function confirmacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            sScript.AppendLine("function confirmEliminacion(strMsje)");
            sScript.AppendLine("{");
            sScript.AppendFormat("  return confirm(strMsje);");
            sScript.AppendLine("}");

            sScript.AppendLine("function Limpiar()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecIniD.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecFinD.ClientID);          
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtNomGuia.ClientID);            
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function Habilitar()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboPromocionD.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').disabled=false;", cboCategoriaD.ClientID);
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function ComparaFechas(dtf1, dtf2)");
            sScript.AppendLine("{");
            sScript.AppendLine("//  la fecha 2 debe ser necesariamente mayor a fecha1");
            sScript.AppendLine("    if(dtf1.length==0 || dtf1.length==0 )");
            sScript.AppendLine("    {");
            sScript.AppendLine("        return true;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    fi = dtf1.split('/');");
            sScript.AppendLine("	ff = dtf2.split('/');");
            sScript.AppendLine("	_fechai = fi[2]*10000 + fi[1]*100 + fi[0];");
            sScript.AppendLine("	_fechaf = ff[2]*10000 + ff[1]*100 + ff[0];");
            sScript.AppendLine("	n = _fechaf - _fechai;");
            sScript.AppendLine("	if(n>=0) return true;");
            sScript.AppendLine("	else return false;");
            sScript.AppendLine("}");
            sScript.AppendLine("");

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }
   
}
