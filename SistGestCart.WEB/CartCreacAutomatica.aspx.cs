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

public partial class CartCreacAutomatica : MyBasePage
{
    SCE_GRUPOTDA_BL GRUPOTDA_BL;
    SCE_CATEGORIA_BL CATEGORIA_BL;
    SCE_PROMOCION_BL PROMOCION_BL;
    SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;
    SCE_GUIA_BL GUIA_BL;
    SCE_GUIA_DET_BE GUIA_DET_BE;
    SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE;
    SCE_FILE_GUIA_MASIVA_BL FILE_GUIA_MASIVA_BL;

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegErrImport()
    {
        DataTable dtRegErrImport = new DataTable();

        dtRegErrImport.Columns.Clear();

        DataColumn workCol0 = dtRegErrImport.Columns.Add("LINEA", typeof(Int32));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegErrImport.Columns.Add("CATEGORIA", typeof(String));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegErrImport.Columns.Add("PROMOCION", typeof(String));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegErrImport.Columns.Add("CAMPO", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRegErrImport.Columns.Add("ERROR", typeof(String));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;        

        return dtRegErrImport;
    }

    //Se crea la estructura del registro temporal de tiendas del grupo
    private DataTable CrearDtRegProducto()
    {
        DataTable dtRegProducto = new DataTable();
        
        dtRegProducto.Columns.Clear();

        DataColumn workCol0 = dtRegProducto.Columns.Add("ID_CATEGORIA", typeof(Int32));
        workCol0.AllowDBNull = true;
        workCol0.Unique = false;

        DataColumn workCol1 = dtRegProducto.Columns.Add("CATEGORÍA", typeof(String));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtRegProducto.Columns.Add("ID_PROMOCION", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtRegProducto.Columns.Add("PROMOCIÓN", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtRegProducto.Columns.Add("ID_LINEA", typeof(Int32));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;

        DataColumn workCol5 = dtRegProducto.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol5.AllowDBNull = true;
        workCol5.Unique = false;

        DataColumn workCol6 = dtRegProducto.Columns.Add("NOM_CAMPO", typeof(String));
        workCol6.AllowDBNull = true;
        workCol6.Unique = false;

        DataColumn workCol7 = dtRegProducto.Columns.Add("VALOR", typeof(String));
        workCol7.AllowDBNull = true;
        workCol7.Unique = false;

        return dtRegProducto;
    }

    private DataTable CrearNewRegDetalleGuiaCampo()
    {
        DataTable dtNewRegDetalleGuiaCampo = new DataTable();

        dtNewRegDetalleGuiaCampo.Columns.Clear();

        DataColumn workCol1 = dtNewRegDetalleGuiaCampo.Columns.Add("ID_LINEA", typeof(Int32));
        workCol1.AllowDBNull = true;
        workCol1.Unique = false;

        DataColumn workCol2 = dtNewRegDetalleGuiaCampo.Columns.Add("ID_CAMPO", typeof(Int32));
        workCol2.AllowDBNull = true;
        workCol2.Unique = false;

        DataColumn workCol3 = dtNewRegDetalleGuiaCampo.Columns.Add("NOM_CAMPO", typeof(String));
        workCol3.AllowDBNull = true;
        workCol3.Unique = false;

        DataColumn workCol4 = dtNewRegDetalleGuiaCampo.Columns.Add("VALOR", typeof(String));
        workCol4.AllowDBNull = true;
        workCol4.Unique = false;

        return dtNewRegDetalleGuiaCampo;
    }

    private void LoadData()
    {
        this.cboGrupoB.DataSource = GRUPOTDA_BL.Listar();
        this.cboGrupoB.DataValueField = "ID_GRUPO";
        this.cboGrupoB.DataTextField = "NOM_GRUPO";
        this.cboGrupoB.DataBind();
        this.cboGrupoB.Items.Insert(0, new ListItem("------------Seleccionar------------", "0")); 

        this.cboEstadoB.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));
        this.cboEstadoB.Items.Add(new ListItem("EN TRABAJO", "1"));
        this.cboEstadoB.Items.Add(new ListItem("LISTO PARA IMPRESIÓN", "2"));

        this.cboCategoriaD.DataSource = CATEGORIA_BL.Listar();
        this.cboCategoriaD.DataValueField = "ID_CATEGORIA";
        this.cboCategoriaD.DataTextField = "NOM_CATEGORIA";
        this.cboCategoriaD.DataBind();
        this.cboCategoriaD.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));

        this.cboPromocionD.DataSource = PROMOCION_BL.Listar();
        this.cboPromocionD.DataValueField = "ID_PROMOCION";
        this.cboPromocionD.DataTextField = "NOM_PROMOCION";
        this.cboPromocionD.DataBind();
        this.cboPromocionD.Items.Insert(0, new ListItem("-----------Seleccionar----------", "0"));

        this.cboGrupoD.DataSource = GRUPOTDA_BL.Listar();
        this.cboGrupoD.DataValueField = "ID_GRUPO";
        this.cboGrupoD.DataTextField = "NOM_GRUPO";
        this.cboGrupoD.DataBind();
        this.cboGrupoD.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));

        this.cboGrupoE.DataSource = GRUPOTDA_BL.Listar();
        this.cboGrupoE.DataValueField = "ID_GRUPO";
        this.cboGrupoE.DataTextField = "NOM_GRUPO";
        this.cboGrupoE.DataBind();
        this.cboGrupoE.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));

        this.fuImporDataMasiva.DataSource = FILE_GUIA_MASIVA_BL.CboFilesGuia();
        this.fuImporDataMasiva.DataValueField = "ID_FILE";
        this.fuImporDataMasiva.DataTextField = "NOM_FILE";
        this.fuImporDataMasiva.DataBind();
        this.fuImporDataMasiva.Items.Insert(0, new ListItem("------------Seleccionar------------", "0"));
        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptCliente();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        GRUPOTDA_BL = new SCE_GRUPOTDA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CATEGORIA_BL = new SCE_CATEGORIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        PROMOCION_BL = new SCE_PROMOCION_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        GUIA_BL = new SCE_GUIA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);
        FILE_GUIA_MASIVA_BL = new SCE_FILE_GUIA_MASIVA_BL((pe.oechsle.Entity.Usuario)Session["DatosCnSistema"]);

        btnBusquedaGuia.OnClientClick = "return IsValid5();";

        btnGrabarImportacion.OnClientClick = "return IsValid4();";

        btnCambiarEst.OnClientClick = string.Format("return confirmacion('{0}');", "Desea que la guia pase a impresión?");
        
        txtNomProdBus.Attributes.Add("onkeypress", String.Format("javascript:return SoloEnterosLetrasYEspacios(event)"));

        btnImportar.OnClientClick = "return IsValid();";
        btnAgregarDet.OnClientClick = "return IsValid2();";

        btnCEG.OnClientClick = "return IsValid3();";

        btnVerPDF.OnClientClick = "return IsValid1();";       

        btnCancImport.OnClientClick = "return Limpiar1();";
        btnCancGDG.OnClientClick = "return Limpiar2();";

        txtFecIniB.Attributes.Add("readonly", "readonly");
        txtFecFinB.Attributes.Add("readonly", "readonly");
        txtFecIniD.Attributes.Add("readonly", "readonly");
        txtFecFinD.Attributes.Add("readonly", "readonly");
        txtFecIniE.Attributes.Add("readonly", "readonly");
        txtFecFinE.Attributes.Add("readonly", "readonly");       
        
        Util.SetEnterButton(txtNomProdBus, btnBusProd);

        if (!Page.IsPostBack)
        {
            Session["Formulario"] = "CartCreacAutomatica.aspx";

            this.Session["linea"] = 0;

            this.Session["varEstado"] = 1;

            //********************************************************************
            // Variable de session que toma la estructura del DataTable Global
            // para manejar las operaciones a nivel multiusuario
            //********************************************************************
            DataTable dtImportados = new DataTable();
            Session["dtImportados"] = dtImportados;
            ((DataTable)this.Session["dtImportados"]).Clear();
            //********************************************************************

            //********************************************************************
            // Variable de session que toma la estructura del DataTable Global
            // para manejar las operaciones a nivel multiusuario
            //********************************************************************
            DataTable dtAux1 = new DataTable();
            Session["dtAux1"] = dtAux1;
            ((DataTable)this.Session["dtAux1"]).Clear();
            //********************************************************************             

            //********************************************************************
            // Variable de session que toma la estructura del DataTable Global
            // para manejar las operaciones a nivel multiusuario
            //********************************************************************
            this.Session["dtRegErrImport"] = CrearDtRegErrImport();
            ((DataTable)this.Session["dtRegErrImport"]).Clear();
            //********************************************************************

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

        Busqueda(txtFecIniB.Text,
                 txtFecFinB.Text,
                 Convert.ToInt32(cboGrupoB.SelectedValue),
                 Convert.ToInt32(cboEstadoB.SelectedValue),
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
                 Convert.ToInt32(cboEstadoB.SelectedValue),
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
                 Convert.ToInt32(cboEstadoB.SelectedValue),
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
                 Convert.ToInt32(cboEstadoB.SelectedValue),
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
                 Convert.ToInt32(cboEstadoB.SelectedValue),
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
                     Convert.ToInt32(cboEstadoB.SelectedValue),
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
                                 Grupo,
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
            updnlCabecera.Update();
            this.grvGuias.DataSource = null;
            this.grvGuias.DataBind();
            Util.RegisterAsyncAlert(updnlCabecera, "__Alerta__", Resources.Mensajes.msgBusquedaSinRegistros);
        }
    }

    protected void btnCrearGuia_Click(object sender, EventArgs e)
    {
        pnlImportarGuia.Visible = true;
        pnlCabecera.Visible = false;

        updnlImportardor.Update();
    }  

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        int IdCategoria0 = 0;
        int IdPromocion0 = 0;

        ((DataTable)this.Session["dtRegErrImport"]).Clear();

        if (fuImporDataMasiva.SelectedValue != "0")
        {
            Session["sName"] = fuImporDataMasiva.SelectedItem.ToString();
            Session["sPath"] = System.Configuration.ConfigurationManager.AppSettings["PATH_GMA"] + (string)Session["sName"];

            //Verificamos si existe el archivo
            if (File.Exists(Session["sPath"].ToString()) == false)
            {
                Util.RegisterAsyncAlert(updnlImportardor, "__Alerta__", "El archivo seleccionado no ha sido correctamente cargado.");
                return;
            }

            //--------------------------------------------------------------------------------------
            // Importamos el contenido del Archivo Excel y lo depositamos en un
            // DataTable        
            //--------------------------------------------------------------------------------------
            DataTable dtLeidos = new DataTable();
            Session["dtLeidos"] = dtLeidos;
            Session["dtLeidos"] = GUIA_BL.Import_Of_Excel((string)Session["sPath"]);
            ((DataTable)Session["dtLeidos"]).CaseSensitive = false;
            //--------------------------------------------------------------------------------------                  

            // Clonamos la estructura del objeto DataTable dtLeidos
            this.Session["dtImportados"] = ((DataTable)Session["dtLeidos"]).Clone();                    
            DataTable dtAux0 = ((DataTable)this.Session["dtImportados"]).Clone();                   
            this.Session["dtAux1"] = ((DataTable)this.Session["dtImportados"]).Clone();                   

            // Obtengo todas las categorias y promociones distintas para recorrer este registro
            DataTable distinct = ((DataTable)Session["dtLeidos"]).DefaultView.ToTable(true, "CATEGORIA", "PROMOCION");

            // Obtengo los registros de categoria-promocion validos
            for (int i = 0; i < distinct.Rows.Count; i++)
            {
                if (distinct.Rows[i][0] != "")
                {
                    IdCategoria0 = GetIdOfCadena(distinct.Rows[i][0].ToString().Trim());
                }
                else
                {
                    IdCategoria0 = 0;
                }

                if (distinct.Rows[i][1] != "")
                {
                    IdPromocion0 = GetIdOfCadena(distinct.Rows[i][1].ToString().Trim());
                }
                else
                {
                    IdPromocion0 = 0;
                }

                string strCrtFiltro = null;
                strCrtFiltro = "CATEGORIA LIKE '" + IdCategoria0 + "-%' AND " + "PROMOCION LIKE '" + IdPromocion0 + "-%'";

                DataRow[] DrArrDel = ((DataTable)Session["dtLeidos"]).Select(strCrtFiltro);

                // De los registro leidos del origen de datos se filtrará los registros que cumplan con
                // la categoria y promocion tengan carteles-modelo configurados en el sistema
                if (CARTEL_MODELO_BL.TieneCPConfiguradoCM(IdCategoria0, IdPromocion0) == true)
                {
                    // Importamos los registros correctos al nuevo DataTable
                    foreach (DataRow row in DrArrDel)
                    {
                        ((DataTable)this.Session["dtAux1"]).ImportRow(row);
                    }
                }
                else
                {
                    // Copiamos los registros al DataTable Auxiliar                                                      
                    foreach (DataRow rowAux in DrArrDel)
                    {
                        dtAux0.ImportRow(rowAux);
                    }

                    // Importamos los registros al DataTable de informe de errores
                    for (int j = 0; j < dtAux0.Rows.Count; j++)
                    {
                        ((DataTable)this.Session["dtRegErrImport"]).Rows.Add(
                                                          Convert.ToInt32(dtAux0.Rows[j][0]),
                                                          dtAux0.Rows[j][1].ToString(),
                                                          dtAux0.Rows[j][2].ToString(),
                                                          "",
                                                          "Promoción - Categoría no configurados"
                                                          );

                        ((DataTable)this.Session["dtRegErrImport"]).AcceptChanges();
                      
                    }
                }
            }

            // Recorro las filas y columnas de los registros validos
            for (int z = 0; z < ((DataTable)this.Session["dtAux1"]).Rows.Count; z++)
            {
                // Obtenemos los campos obligatorios de la Categoria - Promocion
                DataTable dtCampOblig = GUIA_BL.GetCamposObligatoriosCP(GetIdOfCadena(((DataTable)this.Session["dtAux1"]).Rows[z][1].ToString().Trim()),
                                                                        GetIdOfCadena(((DataTable)this.Session["dtAux1"]).Rows[z][2].ToString().Trim()));

                // Verificamos si sus campos obligatorios son diferente de vacio
                for (int j = 0; j < dtCampOblig.Rows.Count; j++)
                {
                    // Obtengo el nombre del campo obligatorio
                    string CampoOblig = dtCampOblig.Rows[j][0].ToString() + "-" + dtCampOblig.Rows[j][1].ToString();

                    for (int k = 0; k < ((DataTable)this.Session["dtAux1"]).Columns.Count; k++)
                    {
                        if (CampoOblig.Trim() == ((DataTable)this.Session["dtAux1"]).Columns[k].ColumnName.ToString().Trim())
                        {
                            if (((DataTable)this.Session["dtAux1"]).Rows[z][k].ToString() == "")
                            {
                                ((DataTable)this.Session["dtRegErrImport"]).Rows.Add(
                                                         Convert.ToInt32(((DataTable)this.Session["dtAux1"]).Rows[z][0]),
                                                         ((DataTable)this.Session["dtAux1"]).Rows[z][1].ToString(),
                                                         ((DataTable)this.Session["dtAux1"]).Rows[z][2].ToString(),
                                                         CampoOblig,
                                                         "Campo obligatorio vacio"
                                                         );

                                ((DataTable)this.Session["dtRegErrImport"]).AcceptChanges();
                            }

                            break;
                        }
                    }
                }
            }

            // Obtenidos y registrado los errores, se ingresaran los registros
            // que son validos evaluando el Id de los incorrectos
            DataTable distinct1 = ((DataTable)this.Session["dtRegErrImport"]).DefaultView.ToTable(true, "LINEA");

            List<DataRow> rowsToDelete = new List<DataRow>();
            int x = 0;
            foreach (DataRow row in ((DataTable)this.Session["dtAux1"]).Rows)
            {
                if (ExisteRegistroError(((DataTable)this.Session["dtAux1"]).Rows[x][0].ToString().Trim(), distinct1) == true)
                {
                    rowsToDelete.Add(row);
                }

                x = x + 1;
            }

            foreach (DataRow row in rowsToDelete)
            {
                ((DataTable)this.Session["dtAux1"]).Rows.Remove(row);
            }

            grvRegImportados.DataSource = (DataTable)this.Session["dtAux1"];
            grvRegImportados.DataBind();

            grvRegNoImportados.DataSource = (DataTable)this.Session["dtRegErrImport"];
            grvRegNoImportados.DataBind();

            lblMsjeImportados.Text = "(" + " " + ((DataTable)Session["dtLeidos"]).Rows.Count + " " + ")" + " " + "Registros Leidos y " + "(" + " " + ((DataTable)this.Session["dtAux1"]).Rows.Count + " " + ") Registros Importados";
            lblMsjeNoImportados.Text = "(" + " " + ((DataTable)this.Session["dtRegErrImport"]).Rows.Count + " " + ")" + " " + "Registros no Importados";
            
        }
        else
        {
            Util.RegisterAsyncAlert(updnlImportardor, "__Alerta__", "No ha seleccionado el archivo");
        }



        pnlDetImportados.Visible = true;
    }

    private int GetIdOfCadena(string Cadena)
    {
        int Id = 0;

        char[] delimit = new char[] { '-' };
        string s10 = Cadena;
        string[] ejemploPartido = s10.Split(delimit);

        for (int M = 0; M < ejemploPartido.Length; M++)
        {
            if (M == 0)
            {
                Id = Convert.ToInt32(ejemploPartido[M]);
            }
        }

        return Id;
    }

    private bool ExisteRegistroError(string Id, DataTable dt)
    {
        bool rspta = false;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][0].ToString().Trim() == Id)
            {
                rspta = true;
            }            
        }

        return rspta;
    }

    protected void btnCancImport_Click(object sender, EventArgs e)
    {
        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        ((DataTable)this.Session["dtImportados"]).Clear();
        ((DataTable)this.Session["dtAux1"]).Clear();
        pnlImportarGuia.Visible = false;
        pnlDetImportados.Visible = false;
        pnlCabecera.Visible = true;

        pnlBarraPaginadora.Visible = false;

        updnlCabecera.Update();
    }

    protected void btnGrabarImportacion_Click(object sender, EventArgs e)
    {
        //Captura todos los datos para la grabacion de la Guia
        List<SCE_GUIA_DET_BE> lstDetGuias = new List<SCE_GUIA_DET_BE>();
        List<SCE_GUIA_DET_CAMPO_BE> lstDetGuiasCampo = new List<SCE_GUIA_DET_CAMPO_BE>();

        foreach (GridViewRow Row in grvRegImportados.Rows)
        {
            int i = Convert.ToInt32(Row.RowIndex);

            int IdCategoria = GetIdOfCadena(grvRegImportados.Rows[i].Cells[1].Text.Trim());
            int IdPromocion = GetIdOfCadena(grvRegImportados.Rows[i].Cells[2].Text.Trim());

            GUIA_DET_BE = new SCE_GUIA_DET_BE();
            GUIA_DET_BE.ID_GUIA = 0;
            GUIA_DET_BE.ID_LINEA = Convert.ToInt32(grvRegImportados.Rows[i].Cells[0].Text);
            GUIA_DET_BE.ID_CATEGORIA = IdCategoria;
            GUIA_DET_BE.ID_PROMOCION = IdPromocion;
            lstDetGuias.Add(GUIA_DET_BE);

            //Obtenemos los campos obligatorios de la Categoria - Promocion
            DataTable dtCampOblig = GUIA_BL.GetCamposObligatoriosCP(IdCategoria, IdPromocion);           

            for (int col = 3; col < ((DataTable)this.Session["dtAux1"]).Columns.Count; col++)
            {
                string IdCampo0 = ((DataTable)this.Session["dtAux1"]).Columns[col].ColumnName.ToString().Substring(0, 2).Trim();
                if (IdCampo0.Contains("-"))
                {
                    IdCampo0 = IdCampo0.ToString().Replace("-", "");
                }
                int IdCampo = Convert.ToInt32(IdCampo0);

                if (((DataTable)this.Session["dtAux1"]).Rows[i][col].ToString() != "")
                {
                    GUIA_DET_CAMPO_BE = new SCE_GUIA_DET_CAMPO_BE();
                    GUIA_DET_CAMPO_BE.ID_GUIA = 0;
                    GUIA_DET_CAMPO_BE.ID_LINEA = GUIA_DET_BE.ID_LINEA;
                    GUIA_DET_CAMPO_BE.ID_CAMPO = IdCampo;
                    GUIA_DET_CAMPO_BE.VALOR = ((DataTable)this.Session["dtAux1"]).Rows[i][col].ToString();
                    lstDetGuiasCampo.Add(GUIA_DET_CAMPO_BE);
                }
            }            
        }

        //Graba la cabecera y el detalle de la Guia
        GUIA_BL.InsertarGuia(txtNomGuia.Text.Trim(),
                             2,
                             Convert.ToDateTime(txtFecIniD.Text),
                             Convert.ToDateTime(txtFecFinD.Text),
                             0,
                             Convert.ToInt32(cboGrupoD.SelectedValue),
                             Convert.ToString(Session["loginUser"]),                             
                             lstDetGuias,
                             lstDetGuiasCampo,
                             Convert.ToInt32(fuImporDataMasiva.SelectedValue));        

        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        pnlDetImportados.Visible = false;
        pnlImportarGuia.Visible = false;
        pnlCabecera.Visible = true;

        Util.RegisterAsyncAlert(updnlImportardor, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_AD"]);

        updnlCabecera.Update();
    }

    protected void btnCancelarImportacion_Click(object sender, EventArgs e)
    {
        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        pnlDetImportados.Visible = false;

        pnlBarraPaginadora.Visible = false;

        updnlCabecera.Update();
    }

    //-----------------------------------------------------------------------------
    //-----------------------------------------------------------------------------
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        hdIdGuia.Value = "0";
        Session["operacion"] = false;

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;

        hdIdGuia.Value = Server.HtmlDecode(row.Cells[2].Text);
        txtFecIniE.Text = Server.HtmlDecode(row.Cells[4].Text);
        txtFecFinE.Text = Server.HtmlDecode(row.Cells[5].Text);
        cboGrupoE.SelectedValue = Server.HtmlDecode(row.Cells[7].Text);           
        txtNomGuiaE.Text = Server.HtmlDecode(row.Cells[3].Text);

        Session["varEstado"] = Convert.ToInt32(Server.HtmlDecode(row.Cells[8].Text));
        //Session["varVigente"] = GUIA_BL.EsVigente(Convert.ToInt32(hdIdGuia.Value));

        //Llena el registro temporal
        Session["dtRegProducto"] = GUIA_BL.GetDetalleGuiaCampoAutomatico(Convert.ToInt32(hdIdGuia.Value));

        //Llenamos la grilla con el registro temporal Pivoteado
        Session["dtRegProductoPV"] = GUIA_BL.PivotDtDetalleGuiaCamposAutomatico((DataTable)Session["dtRegProducto"], "NOM_CAMPO");

        this.grvDetalleGuias.DataSource = Session["dtRegProductoPV"];
        this.grvDetalleGuias.DataBind();

        Session["linea"] = 0;     
       
        //Establece el numero maximo de linea           
        foreach (GridViewRow Row in grvDetalleGuias.Rows)
        {
            int i = Convert.ToInt32(Row.RowIndex);

            int AUX = Convert.ToInt32(grvDetalleGuias.Rows[i].Cells[7].Text);

            if (AUX > (int)Session["linea"])
            {
                Session["linea"] = AUX;
            }
        }        
       
        if ((int)Session["varEstado"] == 2)
        {
            btnCambiarEst.Enabled = false;
            btnAgregarDet.Enabled = false;
            btnCEG.Enabled = false;

            txtFecIniE.Enabled = false;
            txtFecFinE.Enabled = false;
            cboGrupoE.Enabled = false;
            txtNomGuiaE.Enabled = false;
        }
        else
        {
            btnCambiarEst.Enabled = true;
            btnAgregarDet.Enabled = true;
            btnCEG.Enabled = true;

            txtFecIniE.Enabled = true;
            txtFecFinE.Enabled = true;
            cboGrupoE.Enabled = true;
            txtNomGuiaE.Enabled = true;
        }

        /**********************************************************************/
        /* EVALUA EL PERMISO DEL USUARIO
        /**********************************************************************/
        if ((bool)Session["UsuMODCA"] == false)
        {
            btnAgregarDet.Enabled = false;
        }
       
        btnCambiarEst.Visible = true;
        pnlDetalleGuia.Visible = true;
        pnlCabecera.Visible = false;

        updnlDetalleG.Update();
    }

    protected void btnBusProd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
        FiltrarProducto();
        sbScript.AppendFormat("document.getElementById('{0}').focus();", txtNomProdBus.ClientID);
    }

    private void FiltrarProducto()
    {
        ((DataTable)Session["dtRegProductoPV"]).Clear();
        Session["dtRegProductoPV"] = GUIA_BL.PivotDtDetalleGuiaCamposAutomatico((DataTable)Session["dtRegProducto"], "NOM_CAMPO");

        if (((DataTable)Session["dtRegProductoPV"]).Rows.Count > 0)
        {
            ((DataTable)Session["dtRegProductoPV"]).DefaultView.RowFilter = "[<PROD1>] LIKE '%" + txtNomProdBus.Text.Trim() + "%'";
            this.grvDetalleGuias.DataSource = ((DataTable)Session["dtRegProductoPV"]).DefaultView;
            this.grvDetalleGuias.DataBind();
        }
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        hdIdGuia.Value = "0";
        Session["operacion"] = false;
       
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.Parent.Parent;
        hdIdGuia.Value = Server.HtmlDecode(row.Cells[2].Text);

        string msge = GUIA_BL.EliminarGuiaAutomatica(Convert.ToInt32(hdIdGuia.Value), Convert.ToInt32(Server.HtmlDecode(row.Cells[8].Text)));

        if (msge.Length > 0)
        {
            Util.RegisterAsyncAlert(updnlCabecera, "__Alerta__", msge);
        }
        else
        {
            btnBusquedaGuia_Click(sender, e);
            Util.RegisterAsyncAlert(updnlCabecera, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_ED"]);
        }
    }

    protected void grvGuias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
        }
    }
    
    protected void btnCancGDG_Click(object sender, EventArgs e)
    {
        //Limpia la grilla de guias
        ViewState["listadoGuias"] = null;
        this.grvGuias.DataSource = ViewState["listadoGuias"];
        this.grvGuias.DataBind();

        Session["linea"] = 0;
        ((DataTable)Session["dtRegProducto"]).Clear();
        ((DataTable)Session["dtNewRegDetalleGuiaCampo"]).Clear();
        cboCategoriaD.Enabled = true;
        cboPromocionD.Enabled = true;
        btnCambiarEst.Visible = false;
        pnlDetalleGuia.Visible = false;        
        pnlDetalleGuiaCampo.Visible = false;
        pnlDetalleGuiaCampoPDF.Visible = false;
        pnlCatPromo.Visible = false;
        pnlCabecera.Visible = true;

        pnlBarraPaginadora.Visible = false;

        updnlCabecera.Update();
    }

    protected void btnAgregarDet_Click(object sender, EventArgs e)
    {
        Session["flgDGG"] = true;
        Session["linea"] = (int)Session["linea"] + 1;
        pnlCatPromo.Visible = true;
        cboCategoriaD.SelectedIndex = 0;
        cboPromocionD.SelectedIndex = 0;
        pnlDetalleGuiaCampo.Visible = false;
    }

    protected void btnCEG_Click(object sender, EventArgs e)
    {
        if (((DataTable)Session["dtRegProducto"]).Rows.Count > 0)
        {
            //Captura todos los datos para la grabacion de la Guia
            List<SCE_GUIA_DET_BE> lstDetGuias = new List<SCE_GUIA_DET_BE>();
            List<SCE_GUIA_DET_CAMPO_BE> lstDetGuiasCampo = new List<SCE_GUIA_DET_CAMPO_BE>();

            //preventivamente elemina los posibles duplicados que se esten generando en registro
            ((DataTable)Session["dtRegProducto"]).DefaultView.Sort = "ID_CAMPO";
            DataTable dt = ((DataTable)Session["dtRegProducto"]).DefaultView.ToTable(true, "ID_LINEA", "ID_CAMPO", "NOM_CAMPO", "VALOR");

            foreach (GridViewRow Row in grvDetalleGuias.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                GUIA_DET_BE = new SCE_GUIA_DET_BE();
                GUIA_DET_BE.ID_GUIA = 0;
                GUIA_DET_BE.ID_LINEA = Convert.ToInt32(grvDetalleGuias.Rows[i].Cells[7].Text);
                //---------------------------------------------------------------------------
                GUIA_DET_BE.ID_CATEGORIA = Convert.ToInt32(grvDetalleGuias.Rows[i].Cells[3].Text);
                GUIA_DET_BE.ID_PROMOCION = Convert.ToInt32(grvDetalleGuias.Rows[i].Cells[5].Text);
                //---------------------------------------------------------------------------
                lstDetGuias.Add(GUIA_DET_BE);
            }

            for (int i = 0; i < ((DataTable)Session["dtRegProducto"]).Rows.Count; i++)
            {
                GUIA_DET_CAMPO_BE = new SCE_GUIA_DET_CAMPO_BE();
                GUIA_DET_CAMPO_BE.ID_GUIA = 0;
                GUIA_DET_CAMPO_BE.ID_LINEA = Convert.ToInt32(((DataTable)Session["dtRegProducto"]).Rows[i][4]);
                GUIA_DET_CAMPO_BE.ID_CAMPO = Convert.ToInt32(((DataTable)Session["dtRegProducto"]).Rows[i][5]);
                GUIA_DET_CAMPO_BE.VALOR = Convert.ToString(((DataTable)Session["dtRegProducto"]).Rows[i][7]);

                lstDetGuiasCampo.Add(GUIA_DET_CAMPO_BE);
            }

            //Graba la cabecera y el detalle de la Guia
            GUIA_BL.ActualizarGuia(Convert.ToInt32(hdIdGuia.Value),
                                   txtNomGuiaE.Text.Trim(),
                                   Convert.ToDateTime(txtFecIniE.Text),
                                   Convert.ToDateTime(txtFecFinE.Text),
                                   0,
                                   Convert.ToInt32(cboGrupoE.SelectedValue),
                                   1,
                                   Convert.ToString(Session["loginUser"]),
                                   lstDetGuias,
                                   lstDetGuiasCampo);

            Limpiar();
            pnlDetalleGuia.Visible = false;
            pnlDetalleGuiaCampo.Visible = false;
            pnlCabecera.Visible = true;

            Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_MD"]);

            updnlCabecera.Update();

        }
        else
        {
            Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_RPV"]);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        cboCategoriaD.SelectedIndex = 0;
        cboPromocionD.SelectedIndex = 0;
        pnlDetalleGuiaCampo.Visible = false;
        pnlCatPromo.Visible = false;
        pnlDetalleGuiaCampoPDF.Visible = false;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //Modo Inserción
        if ((bool)Session["flgDGG"] == true)
        {
            foreach (GridViewRow Row in grvCamposOblig.Rows)
            {
                int i = Convert.ToInt32(Row.RowIndex);

                //VALIDA QUE NINGUN VALOR EN LAS CAJAS DE TEXTO DE LA GRILLA ESTEN VACIAS
                TextBox myTexto;
                myTexto = new TextBox();
                myTexto = Row.FindControl("txtValor") as TextBox;
                if (myTexto.Text == "")
                {
                    Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "Falta llenar de valores los campos del producto");
                    return;
                }
                else
                {
                    ((DataTable)this.Session["dtRegProducto"]).Rows.Add(
                                                                     Convert.ToInt32(cboCategoriaD.SelectedValue),
                                                                     Server.HtmlDecode(cboCategoriaD.SelectedItem.Text),
                                                                     Convert.ToInt32(cboPromocionD.SelectedValue),
                                                                     Server.HtmlDecode(cboPromocionD.SelectedItem.Text),
                                                                     (int)Session["linea"],
                                                                     Convert.ToInt32(grvCamposOblig.Rows[i].Cells[1].Text),
                                                                     Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()),
                                                                     myTexto.Text.Trim()
                                                                    );

                    ((DataTable)this.Session["dtRegProducto"]).AcceptChanges();                    
                }
            }
        }
        else  //Modo Edición
        {
            //Eliminamos el grupo de registros correspondientes a la linea
            //seleccionada
            DataRow[] DrArrDel = ((DataTable)this.Session["dtRegProducto"]).Select("ID_LINEA = " + (int)Session["lineaSeleccionada"]);
            foreach (DataRow DrDel in DrArrDel)
            {
                ((DataTable)this.Session["dtRegProducto"]).Rows.Remove(DrDel);
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
                    Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "Falta llenar de valores en algunos campos del registro del producto");
                    return;
                }
                else
                {
                    ((DataTable)this.Session["dtRegProducto"]).Rows.Add(
                                                                     Convert.ToInt32(grvDetalleGuias.Rows[Convert.ToInt32(hidIndex.Value)].Cells[3].Text),
                                                                     Server.HtmlDecode(grvDetalleGuias.Rows[Convert.ToInt32(hidIndex.Value)].Cells[4].Text.Trim()),
                                                                     Convert.ToInt32(grvDetalleGuias.Rows[Convert.ToInt32(hidIndex.Value)].Cells[5].Text),
                                                                     Server.HtmlDecode(grvDetalleGuias.Rows[Convert.ToInt32(hidIndex.Value)].Cells[6].Text.Trim()),
                                                                     (int)Session["lineaSeleccionada"],
                                                                     Convert.ToInt32(grvCamposOblig.Rows[i].Cells[1].Text),
                                                                     Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()),
                                                                     myTexto.Text.Trim()
                                                                    );

                    ((DataTable)this.Session["dtRegProducto"]).AcceptChanges();                   
                }
            }
        }

        //Ordenamos los registros del DataTable
        ((DataTable)this.Session["dtRegProducto"]).DefaultView.Sort = "ID_LINEA";

        //Llenamos la grilla con el DataTable Pivoteado
        this.grvDetalleGuias.DataSource = GUIA_BL.PivotDtDetalleGuiaCamposAutomatico((DataTable)this.Session["dtRegProducto"], "NOM_CAMPO");
        this.grvDetalleGuias.DataBind();

        pnlDetalleGuiaCampo.Visible = false;
        pnlCatPromo.Visible = false;
        cboCategoriaD.SelectedIndex = 0;
        cboPromocionD.SelectedIndex = 0;
    }

    protected void grvDetalleGuias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            // Se obtiene indice de la row seleccionada
            int index = Convert.ToInt32(e.CommandArgument);

            hidIndex.Value = Convert.ToString(index);

            // Obtengo el id de la entidad que se esta editando
            // en este caso de la entidad registro de productos            
            int id = Convert.ToInt32(grvDetalleGuias.DataKeys[index].Value);

            Session["lineaSeleccionada"] = id;

            hidIdCategoria.Value = grvDetalleGuias.Rows[index].Cells[3].Text;
            hidIdPromocion.Value = grvDetalleGuias.Rows[index].Cells[5].Text;

            if (e.CommandName == "Editar")
            {
                if (hdIdGuia.Value != "0")
                {
                    if (GUIA_BL.EsVigente(Convert.ToInt32(hdIdGuia.Value)) == false)
                    {
                        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "No se puede realizar la operacion por que la guia no esta vigente");
                        return;
                    }

                    if ((int)Session["varEstado"] == 2)
                    {
                        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "No se puede realizar la operacion por que la guia se encuentra en impresión");
                        return;
                    }
                }

                Session["flgDGG"] = false;

                string strCrtFiltro = null;

                strCrtFiltro = "ID_LINEA = " + id;

                this.Session["dtNewRegDetalleGuiaCampo"] = GUIA_BL.FiltraDataTable((DataTable)this.Session["dtRegProducto"], strCrtFiltro);

                this.grvCamposOblig.DataSource = (DataTable)this.Session["dtNewRegDetalleGuiaCampo"];
                this.grvCamposOblig.DataBind();

                /**********************************************************************/
                /* RECORRE LA GRILLA DE DETALLE DEL REGISTRO DEL PRODUCTO SELECCIONADO
                /* Y EVALUA EL PERMISO DEL USUARIO CON RESPECTO A LA EDICION DE PRECIOS*/
                /**********************************************************************/
                if ((bool)Session["UsuMODCA"] == false)
                {
                    foreach (GridViewRow Row in grvCamposOblig.Rows)
                    {
                        int i = Convert.ToInt32(Row.RowIndex);

                        // DETECTA LA CAJA DE TEXTO EN LA GRILLA
                        TextBox myTexto;
                        myTexto = new TextBox();
                        myTexto = Row.FindControl("txtValor") as TextBox;

                        //myTexto.Enabled = GUIA_BL.EnabledCostField(Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()));   
                        myTexto.Enabled = GUIA_BL.RestringeCampo(Server.HtmlDecode(grvCamposOblig.Rows[i].Cells[2].Text.Trim()));                    
                    }
                }
                /**********************************************************************/

                pnlDetalleGuiaCampo.Visible = true;
                pnlDetalleGuiaCampoPDF.Visible = false;
            }
            else if (e.CommandName == "Eliminar")
            {
                if (hdIdGuia.Value != "0")
                {
                    if (GUIA_BL.EsVigente(Convert.ToInt32(hdIdGuia.Value)) == false)
                    {
                        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "No se puede realizar la operacion por que la guia no esta vigente");
                        return;
                    }

                    if ((int)Session["varEstado"] == 2)
                    {
                        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "No se puede realizar la operacion por que la guia se encuentra en impresión");
                        return;
                    }

                    /**********************************************************************/
                    /* EVALUA EL PERMISO DEL USUARIO
                    /**********************************************************************/
                    if ((bool)Session["UsuMODCA"] == false)
                    {
                        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "No se puede realizar la operacion por que el usuario no tiene permisos");
                        return;
                    }
                }

                //Eliminamos el grupo de registros correspondientes a la linea
                //seleccionada
                DataRow[] DrArrDel = ((DataTable)this.Session["dtRegProducto"]).Select("ID_LINEA = " + id);
                foreach (DataRow DrDel in DrArrDel)
                {
                    ((DataTable)this.Session["dtRegProducto"]).Rows.Remove(DrDel);
                }

                this.grvDetalleGuias.DataSource = GUIA_BL.PivotDtDetalleGuiaCamposAutomatico((DataTable)this.Session["dtRegProducto"], "NOM_CAMPO");
                this.grvDetalleGuias.DataBind();
                this.grvDetalleGuias.PageIndex = 0;

                pnlDetalleGuiaCampo.Visible = false;
                pnlDetalleGuiaCampoPDF.Visible = false;
                pnlCatPromo.Visible = false;
            }
            else if (e.CommandName == "VerPDF")
            {
                grvCamposObligPDF.DataSource = GUIA_BL.GetDetalleGuiaCampo1(Convert.ToInt32(hdIdGuia.Value), id);
                grvCamposObligPDF.DataBind();

                this.cboCartelesPDF.DataSource = CARTEL_MODELO_BL.cboCartelModeloCMCP(Convert.ToInt32(grvDetalleGuias.Rows[index].Cells[3].Text),
                                                                                      Convert.ToInt32(grvDetalleGuias.Rows[index].Cells[5].Text),
                                                                                      Convert.ToInt32(hdIdGuia.Value),
                                                                                      (int)Session["lineaSeleccionada"]);

                this.cboCartelesPDF.DataValueField = "CODIGO";
                this.cboCartelesPDF.DataTextField = "DESCRIPCION";
                this.cboCartelesPDF.DataBind();
                this.cboCartelesPDF.Items.Insert(0, new ListItem("----------------Seleccionar----------------", "0"));

                pnlDetalleGuiaCampoPDF.Visible = true;
                pnlDetalleGuiaCampo.Visible = false;
                pnlCatPromo.Visible = false;
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
            e.Row.Cells[5].CssClass = "ColumnaOculta";
            e.Row.Cells[7].CssClass = "ColumnaOculta";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                e.Row.Cells[3].ControlStyle.CssClass = "ColumnaOculta";
                e.Row.Cells[5].ControlStyle.CssClass = "ColumnaOculta";
                e.Row.Cells[7].ControlStyle.CssClass = "ColumnaOculta";     
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
    }

    protected void grvDetalleGuias_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grvDetalleGuias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgE = (ImageButton)e.Row.FindControl("btnEliminar1");
            imgE.OnClientClick = string.Format("return confirmEliminacion('{0}');", Resources.Mensajes.msgConfirmEliminacion);
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
                                  (int)Session["lineaSeleccionada"],
                                  IdNameCartel) == false)
        {
            Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["MCP_NEXF"]);
        }
        else
        {
            strCartel = strPath + cboCartelesPDF.SelectedItem.ToString() + IdNameCartel + ".pdf";

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
        pnlCatPromo.Visible = false;
    }

    private void Limpiar()
    {
        txtFecIniD.Text = "";
        txtFecFinD.Text = "";
        cboCategoriaD.SelectedIndex = 0;
        cboPromocionD.SelectedIndex = 0;
        cboGrupoD.SelectedIndex = 0;
        txtNomGuia.Text = "";
        Session["operacion"] = true;
    }

    protected void btnCambiarEst_Click(object sender, EventArgs e)
    {
        GUIA_BL.ActualizarEstadoGuia(Convert.ToInt32(hdIdGuia.Value));
        Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", System.Configuration.ConfigurationManager.AppSettings["CIMA_MEG"]);

        btnCancGDG_Click(sender, e);
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
   
    protected void grvRegImportados_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
    }

    protected void grvRegImportados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void grvRegNoImportados_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ControlStyle.Font.Size = 8;
                cell.ControlStyle.Font.Bold = false;
            }
        }
    }
    
    protected void cboPromocionD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCategoriaD.SelectedValue != "0" && cboPromocionD.SelectedValue != "0")
        {
            this.grvCamposOblig.DataSource = GUIA_BL.GetNewRegDetalleGuiaCampo(Convert.ToInt32(cboPromocionD.SelectedValue),
                                                                               Convert.ToInt32(cboCategoriaD.SelectedValue));
            this.grvCamposOblig.DataBind();

            if (grvCamposOblig.Rows.Count > 0)
            {
                pnlDetalleGuiaCampo.Visible = true;
            }
            else
            {
                Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "La categoria y promocion que ha seleccionado no esta asociada a un cartel");

                pnlCatPromo.Visible = false;
                pnlDetalleGuiaCampo.Visible = false;
            }                
        }
    }
    protected void cboCategoriaD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCategoriaD.SelectedValue != "0" && cboPromocionD.SelectedValue != "0")
        {
            this.grvCamposOblig.DataSource = GUIA_BL.GetNewRegDetalleGuiaCampo(Convert.ToInt32(cboPromocionD.SelectedValue),
                                                                               Convert.ToInt32(cboCategoriaD.SelectedValue));
            this.grvCamposOblig.DataBind();

            if (grvCamposOblig.Rows.Count > 0)
            {
                pnlDetalleGuiaCampo.Visible = true;
            }
            else
            {
                Util.RegisterAsyncAlert(updnlDetalleG, "__Alerta__", "La categoria y promocion que ha seleccionado no esta asociada a un cartel");

                pnlCatPromo.Visible = false;
                pnlDetalleGuiaCampo.Visible = false;
            } 
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
            sScript.AppendFormat("  strGrupo = document.getElementById('{0}').value;", cboGrupoD.ClientID);
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
            sScript.AppendLine("    if (strGrupo  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha seleccionado el grupo');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboGrupoD.ClientID);
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
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniE.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinE.ClientID);
            sScript.AppendFormat("  strGrupo = document.getElementById('{0}').value;", cboGrupoE.ClientID);
            sScript.AppendFormat("  strNomGuia = document.getElementById('{0}').value;", txtNomGuiaE.ClientID);
            sScript.AppendLine("    if (strFecIni.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de inicio de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha colocado el nombre de la guia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuiaE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('Lo longitud del nombre de la guia sobrepasa el establecido por el sistema');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuiaE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid3()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniE.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinE.ClientID);
            sScript.AppendFormat("  strGrupo = document.getElementById('{0}').value;", cboGrupoE.ClientID);
            sScript.AppendFormat("  strNomGuia = document.getElementById('{0}').value;", txtNomGuiaE.ClientID);
            sScript.AppendLine("    if (strFecIni.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de inicio de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strFecFin.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No colocado la fecha de fin de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecFinE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (ComparaFechas(strFecIni, strFecFin)==false)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('La fecha de inicio no puede ser mayo a la fecha de final de vigencia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtFecIniE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha colocado el nombre de la guia');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuiaE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendLine("    if (strNomGuia.length  > 50)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('Lo longitud del nombre de la guia sobrepasa el establecido por el sistema');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", txtNomGuiaE.ClientID);
            sScript.AppendLine("            return false;");
            sScript.AppendLine("    }");
            sScript.AppendFormat("  return confirm(msg);");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid4()");
            sScript.AppendLine("{");
            sScript.AppendFormat("  msg = '{0}';", Resources.Mensajes.msgConfirmación);
            sScript.AppendFormat("  strFecIni = document.getElementById('{0}').value;", txtFecIniD.ClientID);
            sScript.AppendFormat("  strFecFin = document.getElementById('{0}').value;", txtFecFinD.ClientID);
            sScript.AppendFormat("  strGrupo = document.getElementById('{0}').value;", cboGrupoD.ClientID);
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
            sScript.AppendLine("    if (strGrupo  == 0)");
            sScript.AppendLine("    {");
            sScript.AppendFormat("          alert('No ha seleccionado el grupo');");
            sScript.AppendFormat("          document.getElementById('{0}').focus();", cboGrupoD.ClientID);
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
            sScript.AppendLine("    return confirm(msg);");
            sScript.AppendLine("}");

            sScript.AppendLine("function IsValid5()");
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

            sScript.AppendLine("function Limpiar1()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecIniD.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecFinD.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboGrupoD.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtNomGuia.ClientID);
            sScript.AppendLine("    return true;");
            sScript.AppendLine("}");

            sScript.AppendLine("function Limpiar2()");
            sScript.AppendLine("{");
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecIniE.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtFecFinE.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').selectedIndex=0;", cboGrupoE.ClientID);
            sScript.AppendFormat("document.getElementById('{0}').value='';", txtNomGuiaE.ClientID);
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

            ClientScript.RegisterStartupScript(Page.GetType(), "__ScriptCliente__", sScript.ToString(), true);
        }
    }    
}
