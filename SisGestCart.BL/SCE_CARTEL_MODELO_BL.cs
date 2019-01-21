using System;
using System.Diagnostics;
using System.Data;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;
/* Office 11 para Office 2003 
 * Office 12 para Office 2007 de Visual Studio 2008 
 * Office 14 para Office 2010 de Visual Studio 2010*/
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
/* OpenXML 2.0 para Office 2007 - 2010 */
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
/* ClosedXML v0.69.1.0 para OpenXML 2.0 */
using ClosedXML.Excel;

namespace SistGestCart.BL
{
    public class SCE_CARTEL_MODELO_BL
    {
        static private List<clsInformeLecturaXLS> lstLecturaXLS = new List<clsInformeLecturaXLS>();
        static private List<SCE_CARTEL_MODELO_CAMPO_BE> lstCartelModeloCampo = new List<SCE_CARTEL_MODELO_CAMPO_BE>();

        Usuario usrLogin;
        SCE_CARTEL_MODELO_BE BE;

        Hashtable myHashtable;
        int MyExcelProcessId;

        public SCE_CARTEL_MODELO_BL()
        {
            
        }

        public SCE_CARTEL_MODELO_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;           
        }

        public string IsValid(string IdCartelModelo, int IdCategoria, int IdPromocion)
        {
            string mensaje = "";

            try
            {
                if (IdCartelModelo == "0")
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["CMCP_CN"];
                }

                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);

                int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
                int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

                if (DA.ExisteCartelModeloInTB_CMCP(IdCartel, IdModelo, IdCategoria, IdPromocion))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["CMCP_CD"];
                }

                return mensaje;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCMC(int IdCartel, 
                                  int IdModelo, 
                                  List<SCE_CARTEL_MODELO_CAMPO_BE> lstCartelModelosCampos,
                                  int numMaxDigitos)
        {
            string CeroDigitos = string.Empty;

            try
            {
                BE = new SCE_CARTEL_MODELO_BE();
                BE.ID_CARTEL = IdCartel;
                BE.ID_MODELO = IdModelo;
                BE.CAMPOS = lstCartelModelosCampos;
                
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                DA.SCE_CARTEL_DA DA1 = new DA.SCE_CARTEL_DA(usrLogin);

                CeroDigitos = DA1.EsCartelModeloCeroDigitos(IdCartel);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.ActualizarCMC(BE, numMaxDigitos, CeroDigitos);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarCMC1(int IdCartel,
                                     int IdModelo,
                                     int Digitos,
                                     string NomPlantilla,
                                     List<SCE_CARTEL_MODELO_CAMPO_BE> lstCartelModelosCampos,
                                     string PosFCX,
                                     string PosFCY)
        {
            try
            {
                BE = new SCE_CARTEL_MODELO_BE();
                BE.ID_CARTEL = IdCartel;
                BE.ID_MODELO = IdModelo;
                BE.DIGITOS = Digitos;
                BE.NOM_PLANTILLA = NomPlantilla;
                BE.CAMPOS = lstCartelModelosCampos;
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.ActualizarCMC1(BE, PosFCX, PosFCY);
                    scope.Complete();
                    return System.Configuration.ConfigurationManager.AppSettings["CMC_MD"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public SCE_CARTEL_MODELO_BE ObtenerPorID(int IdCartel, int IdModelo)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ObtenerPorID(IdCartel, IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_CARTEL_MODELO_BE ObtenerPorID1(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ObtenerPorID1(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CARTEL_MODELO_BE> Listar()
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable ListarCMConf()
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return ListToDataTable(DA.ListarCMConf());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SCE_CARTEL_MODELO_BE> cboCartelModelo()
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.cboCartelModelo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CARTEL_MODELO_BE> cboCartelModeloCP()
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.cboCartelModeloCP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CARTEL_MODELO_BE> cboCartelModeloCMCP(int IdCategoria, int IdPromocion)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.cboCartelModeloCMCP(IdCategoria, IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CARTEL_MODELO_BE> cboCartelModeloCMCP(int IdCategoria, 
                                                              int IdPromocion, 
                                                              int IdGuia, 
                                                              int IdLinea)
        {
            List<SCE_CARTEL_MODELO_BE> lstCartelesModelo = new List<SCE_CARTEL_MODELO_BE>();
            List<SCE_CARTEL_MODELO_CAMPO_BE> lstCM_MULTICAMPO_VAL = new List<SCE_CARTEL_MODELO_CAMPO_BE>();
            List<SCE_CARTEL_MODELO_BE> lstCM = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE CM;

            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);

                /*OBTENER EL LISTADO TODOS LOS CARTELES ASIGNADOS A LA CATEGORIA PROMOCION
                  QUE TENGAN UNA PLANTILLA ASIGNADA PREVIAMENTE*/
                lstCartelesModelo = DA.cboCartelModeloCMCP(IdCategoria, IdPromocion);

                for (int i = 0; i < lstCartelesModelo.Count; i++)
                {
                    int IdCartel = Convert.ToInt32(lstCartelesModelo[i].ID_CARTEL);
                    int IdModelo = Convert.ToInt32(lstCartelesModelo[i].ID_MODELO);
                    int Digitos = Convert.ToInt32(lstCartelesModelo[i].DIGITOS);

                    /*VALIDA QUE COINCIDAN LOS CAMPOS DEL CARTEL CON LOS CAMPOS LLENOS DE LA LINEA
                     DE DETALLE DE LA GUIA (CAMPOS LLENOS DEL PRODUCTO)*/
                    if (DA.ValidaCartelModeloLineaProducto(IdLinea,
                                                           IdGuia,
                                                           IdCartel,
                                                           IdModelo,
                                                           Digitos) == false)
                    {
                        //De la lista validada anteriormente ahora excluye los carteles que no 
                        //tienen igual numero de digitos
                        if (DA.TieneIgualNroDigitos(IdLinea,
                                                    IdGuia,
                                                    IdCartel,
                                                    IdModelo, 
                                                    Digitos) == true)
                        {                            
                                CM = new SCE_CARTEL_MODELO_BE();
                                CM.CODIGO = IdCartel.ToString("0000") + IdModelo.ToString("00") + Digitos.ToString();
                                CM.DESCRIPCION = lstCartelesModelo[i].DESCRIPCION.ToString().Trim();
                                lstCM.Add(CM);
                        }
                    }
                }

                return lstCM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistePlantilla(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ExistePlantilla(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerPlantilla(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ObtenerPlantilla(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerPlantilla(string Codigo)
        {
            int IdCartel = Convert.ToInt32(Codigo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(Codigo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(Codigo.Substring(6, 1).ToString().Trim());

            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ObtenerPlantilla(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistePlantillaPDF(string path)
        {
            bool rspta = false;

            if (File.Exists(path))
            {
                rspta = true;
            }

            return rspta;
        }

        public System.Data.DataTable ListarPV()
        {
            int numMaxDigitos = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NroMaxDigitos"]);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = null;

            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);          
                if (DA.Listar().Count > 0)
                {
                    if (DA.ListarPV().Count > 0)
                    {
                        dt = PivotDtCartelXModelo(ListToDataTable(DA.ListarPV()), "NOM_CAMPO", numMaxDigitos);
                        return dt;
                    }
                    else
                    {
                        return dt;
                    }
                }
                else
                {
                    return dt;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------------------------------------------------------------------------------------------

        public void ActualizarCMCP(string IdCartelModelo, 
                                   int Digitos, 
                                   List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> lstCartelModelosCategPromo)
        {
            string CeroDigitos = string.Empty;

            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                BE = new SCE_CARTEL_MODELO_BE();
                BE.ID_CARTEL = IdCartel;
                BE.ID_MODELO = IdModelo;
                BE.CATEGS_PROMOS = lstCartelModelosCategPromo;
              
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                DA.SCE_CARTEL_DA DA1 = new DA.SCE_CARTEL_DA(usrLogin);

                CeroDigitos = DA1.EsCartelModeloCeroDigitos(IdCartel);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.ActualizarCMCP(BE, Digitos, CeroDigitos);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCMCP(int IdCartel, int IdModelo, List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> lstCartelModelosCategPromo)
        {
            try
            {
                BE = new SCE_CARTEL_MODELO_BE();
                BE.ID_CARTEL = IdCartel;
                BE.ID_MODELO = IdModelo;
                BE.CATEGS_PROMOS = lstCartelModelosCategPromo;

                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.EliminarCMCP(BE);
                    scope.Complete();                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> ListarCMCP(int IdCategoria, int IdPromocion)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);               
                return DA.ListarCMCP(IdCategoria, IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TieneCPConfiguradoCM(int IdCategoria, int IdPromocion)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.TieneCPConfiguradoCM(IdCategoria, IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNombreModelo(string IdCartelModelo)
        {
            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.GetNombreModelo(IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTipoHojaModelo(string IdCartelModelo)
        {
            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.GetTipoHojaModelo(IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteParCategPromoSeleccionado(int IdCategoria, int IdPromocion)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.ExisteParCategPromoSeleccionado(IdCategoria, IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /****************************************************************************************/
        /************************************ FUNCIONES ESPECIALES ******************************/
        /****************************************************************************************/

        private static System.Data.DataTable ListToDataTable(List<SCE_CARTEL_MODELO_BE> List)
        {
            System.Data.DataTable oDataTableReturned = new System.Data.DataTable();

            if (List.Count > 0)
            {
                object _baseObj = List[0];
                Type objectType = _baseObj.GetType();
                PropertyInfo[] properties = objectType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    DataColumn oColumna;
                    oColumna = new DataColumn();
                    oColumna.ColumnName = property.Name;
                    oColumna.DataType = property.PropertyType;
                    oDataTableReturned.Columns.Add(oColumna);
                }

                foreach (object objItem in List)
                {
                    DataRow oFila;
                    oFila = oDataTableReturned.NewRow();

                    foreach (PropertyInfo property in properties)
                    {
                        oFila[property.Name] = property.GetValue(objItem, null);
                    }

                    oDataTableReturned.Rows.Add(oFila);
                }
            }

            return oDataTableReturned;
        }

        public System.Data.DataTable PivotDtCartelXModelo(System.Data.DataTable dt, string columnName, int numMaxDigitos)
        {
            try
            {
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable dtPivot = new System.Data.DataTable();

                dtPivot.Columns.Add("ID_CARTEL", typeof(int));
                dtPivot.Columns.Add("ID_MODELO", typeof(int));
                dtPivot.Columns.Add("DESCRIPCION", typeof(String));

                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
                //DE LA TABLA PIVOTEADA
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (columnName == null || columnName.Length == 0)
                {
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                }

                System.Data.DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

                for (int i = 0; i < distintos0.Rows.Count; i++)
                {
                    dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
                }

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'LLENA EL DATATABLE PIVOTEADO
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_CARTEL", "ID_MODELO", "DESCRIPCION");
                DataRow row = null;

                for (int i = 0; i < distintos1.Rows.Count; i++)
                {
                    row = dtPivot.NewRow();

                    row["ID_CARTEL"] = distintos1.Rows[i][0];
                    row["ID_MODELO"] = distintos1.Rows[i][1];
                    row["DESCRIPCION"] = distintos1.Rows[i][2];

                    System.Data.DataTable dtAux = new System.Data.DataTable();
                    string strCrtFiltro = null;

                    strCrtFiltro = "ID_CARTEL = " + distintos1.Rows[i][0] + " AND " + "ID_MODELO = " + distintos1.Rows[i][1];

                    dtAux = FiltraDataTable(dt, strCrtFiltro);

                    for (int cols = 2; cols < dtPivot.Columns.Count; cols++)
                    {
                        for (int j = 0; j < dtAux.Rows.Count; j++)
                        {
                            // Se hizo el cambio  a dtAux.Rows[j][4].ToString() == "1" por dtAux.Rows[j][4].ToString() == "5"
                            if (((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][5].ToString()) && (dtAux.Rows[j][4].ToString() == Convert.ToString(numMaxDigitos))) || ((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][5].ToString()) && (dtAux.Rows[j][4].ToString() == "1")))
                            {
                                row[cols] = "x";
                            }
                        }
                    }

                    dtPivot.Rows.Add(row);
                }

                dtPivot.DefaultView.Sort = "DESCRIPCION";

                return dtPivot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable FiltraDataTable(System.Data.DataTable dtprincipal, string ctrFiltro)
        {
            /* Seleccionamos las filas que se correspondan con el identificador */
            System.Data.DataRow[] rows = dtprincipal.Select(ctrFiltro);

            /* Clonamos la estructura del objeto DataTable principal */
            System.Data.DataTable dt1 = dtprincipal.Clone();

            // Importamos los registros al nuevo DataTable
            foreach (DataRow row in rows)
            {
                dt1.ImportRow(row);
            }

            return dt1;
        }

        public bool HaveMoreOneSheetExcel(string PathFile)
        {
            bool rspta = false;

            string xlsFilePath = PathFile;

            CheckForExistingExcellProcesses();

            Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            var misValue = Type.Missing;//System.Reflection.Missing.Value;

            try
            {
                GetTheExcelProcessIdThatUsedByThisInstance();

                // abrir el documento
                xlApp = new ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                // Obtengo el numero de Hojas de Caculo
                int nroPestañas = xlWorkBook.Sheets.Count;                           

                // cerrar
                xlWorkBook.Close(false, misValue, misValue);
                xlApp.Quit();

                // liberar
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                // Valido si tiene mas de una hoja de calulo o no
                if (nroPestañas > 1)
                {
                    rspta = true;
                }
                else
                {
                    rspta = false;
                }

                return rspta;
            }
            catch (Exception ex)
            {
                // cerrar
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();

                KillExcelProcessThatUsedByThisInstance();

                throw ex;
            }
            finally
            {
                // liberar
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                KillExcelProcessThatUsedByThisInstance();
            }            
        }

        public List<clsInformeValidacionXLS> GetInforme(string PathFile,
                                                        int IdCartel,
                                                        int IdModelo,
                                                        int Digitos,
                                                        ref bool Errores, 
                                                        ref string PosFCX, 
                                                        ref string PosFCY)
        {
            clsInformeValidacionXLS InformeValidacionXLS;
            List<clsInformeValidacionXLS> lstInformeValidacionXLS = new List<clsInformeValidacionXLS>();

            Errores = false;

            //Se obtiene los datos del archivo Excel
            lstLecturaXLS = LeerXLS(PathFile);

            //Se obtiene los datos de la BD
            lstCartelModeloCampo = CargarData(IdCartel, IdModelo, Digitos);

            //Se toma como referencia de busqueda la data leida del archivo excel y 
            //busca si el campo existe en la BD
            for (int i = 0; i < lstLecturaXLS.Count; i++)
            {
                //CEZ
                if (lstLecturaXLS[i].VALOR.ToString().Trim() != "<?>")
                {
                    if (FindCampoBD(lstLecturaXLS[i].VALOR.ToString().Trim()))
                    {
                        InformeValidacionXLS = new clsInformeValidacionXLS();
                        InformeValidacionXLS.CAMPO = lstLecturaXLS[i].VALOR.ToString().Trim();
                        InformeValidacionXLS.DESCRIPCION = GetDescField(lstLecturaXLS[i].VALOR.ToString().Trim());
                        InformeValidacionXLS.POSX = lstLecturaXLS[i].POSX.ToString().Trim();
                        InformeValidacionXLS.POSY = lstLecturaXLS[i].POSY.ToString().Trim();
                        lstInformeValidacionXLS.Add(InformeValidacionXLS);
                    }
                    else
                    {
                        InformeValidacionXLS = new clsInformeValidacionXLS();
                        InformeValidacionXLS.CAMPO = lstLecturaXLS[i].VALOR.ToString().Trim();
                        InformeValidacionXLS.DESCRIPCION = "Campo NO RECONOCIDO";
                        lstInformeValidacionXLS.Add(InformeValidacionXLS);
                        Errores = true;
                    }
                }
                else
                {
                    PosFCX = lstLecturaXLS[i].POS_FC_X.ToString().Trim();
                    PosFCY = lstLecturaXLS[i].POS_FC_Y.ToString().Trim();
                }
                
            }

            //Se toma como referencia de busqueda la data leida de la BD y busca si el
            //campo existe en el archivo excel
            for (int i = 0; i < lstCartelModeloCampo.Count; i++)
            {
                if (FindCampoExcel(lstCartelModeloCampo[i].ALIAS.ToString().Trim()) == false)
                {
                    InformeValidacionXLS = new clsInformeValidacionXLS();
                    InformeValidacionXLS.CAMPO = lstCartelModeloCampo[i].ALIAS.ToString().Trim();
                    InformeValidacionXLS.DESCRIPCION = "Campo OBLIGATORIO NO INGRESADO";                   
                    lstInformeValidacionXLS.Add(InformeValidacionXLS);
                    Errores = true;
                }
            }

            return lstInformeValidacionXLS;
        }

        private List<clsInformeLecturaXLS> LeerXLS(string PathFile)
        {
            clsInformeLecturaXLS InformeLecturaXLS;
            List<clsInformeLecturaXLS> lstLecturaXLS = new List<clsInformeLecturaXLS>();

            string xlsFilePath = PathFile;

            CheckForExistingExcellProcesses();

            Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;
            Range range = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            var misValue = Type.Missing;//System.Reflection.Missing.Value;

            try
            {
                GetTheExcelProcessIdThatUsedByThisInstance();

                // abrir el documento
                xlApp = new ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                // seleccion de la hoja de calculo
                // get_item() devuelve object y numera las hojas a partir de 1
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // seleccion rango activo
                range = xlWorkSheet.UsedRange;

                // leer las celdas
                int rows = range.Rows.Count;
                int cols = range.Columns.Count;

                string str_value = null;
                string str_name = null;

                for (int row = 1; row <= rows; row++)
                {
                    for (int col = 1; col <= cols; col++)
                    {
                        //str_value = (string)(range.Cells[row, col] as Range).Text;

                        if ((range.Cells[row, col] as Range).Value2 != null)
                        {
                            // lectura de la informacion que contiene la celda no vacia
                            str_value = (range.Cells[row, col] as Range).Value2.ToString(); //o (string)(range.Cells[row, col] as Range).Text

                            // Identificacion del nombre de la celda no vacia  
                            switch ((string)(range.Cells[row, col] as Range).Column.ToString())
                            {
                                //case txtCodArea.Name:
                                case "1":
                                    str_name = "A" + row;
                                    break;

                                case "2":
                                    str_name = "B" + row;
                                    break;

                                case "3":
                                    str_name = "C" + row;
                                    break;

                                case "4":
                                    str_name = "D" + row;
                                    break;

                                case "5":
                                    str_name = "F" + row;
                                    break;

                                case "6":
                                    str_name = "G" + row;
                                    break;

                                case "7":
                                    str_name = "H" + row;
                                    break;

                                case "8":
                                    str_name = "I" + row;
                                    break;

                                case "9":
                                    str_name = "J" + row;
                                    break;

                                case "10":
                                    str_name = "K" + row;
                                    break;

                                case "11":
                                    str_name = "L" + row;
                                    break;
                            }

                            //if (str_value != "S/." || str_value != "$")
                            if (str_value.IndexOf("<") > 0 || str_value.IndexOf(">") > 0)
                            {
                                InformeLecturaXLS = new clsInformeLecturaXLS();
                                InformeLecturaXLS.ID_CAMPO = 0;
                                InformeLecturaXLS.POSX = Convert.ToString(row);
                                InformeLecturaXLS.POSY = Convert.ToString(col);
                                InformeLecturaXLS.CELDA = str_name;
                                InformeLecturaXLS.COORDENADA = "(" + row + "," + col + ")";
                                InformeLecturaXLS.VALOR = str_value;

                                if (InformeLecturaXLS.VALOR.ToString().Trim() == "<?>")
                                {
                                    InformeLecturaXLS.POS_FC_X = Convert.ToString(row);
                                    InformeLecturaXLS.POS_FC_Y = Convert.ToString(col);
                                }

                                lstLecturaXLS.Add(InformeLecturaXLS);
                            }
                        }
                    }
                }

                // cerrar
                xlWorkBook.Close(false, misValue, misValue);
                xlApp.Quit();

                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                return lstLecturaXLS;

            }
            catch (Exception ex)
            {
                // cerrar
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();                

                KillExcelProcessThatUsedByThisInstance();

                throw ex;
            }
            finally
            {
                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                KillExcelProcessThatUsedByThisInstance();
            }
        }

        private bool FindCampoExcel(string Campo)
        {
            bool bReturn = false;
            List<clsInformeLecturaXLS> lst = lstLecturaXLS;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].VALOR.ToString() == Campo)
                {
                    bReturn = true;
                    break;
                }
                else
                {
                    bReturn = false;
                }
            }

            return bReturn;
        }

        private bool FindCampoBD(string Campo)
        {
            bool bReturn = false;
            List<SCE_CARTEL_MODELO_CAMPO_BE> lst = lstCartelModeloCampo;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].ALIAS.ToString() == Campo)
                {
                    bReturn = true;
                    break;
                }
                else
                {
                    bReturn = false;
                }
            }

            return bReturn;
        }

        private string GetDescField(string Alias)
        {
            string DescField = "";
            List<SCE_CARTEL_MODELO_CAMPO_BE> lst = lstCartelModeloCampo;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].ALIAS.ToString().Trim() == Alias)
                {
                    DescField = lst[i].NOM_CAMPO.ToString().Trim();
                }
            }

            return DescField;
        }

        /* CARGA DATA DE CAMPOS DESDE LA BD */
        private List<SCE_CARTEL_MODELO_CAMPO_BE> CargarData(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                DA.SCE_CARTEL_MODELO_DA DA = new DA.SCE_CARTEL_MODELO_DA(usrLogin);
                return DA.grvCampos(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConvertExcelToPdf(string excelFileIn, string pdfFileOut)
        {
            CheckForExistingExcellProcesses();

            var missing = Type.Missing;//System.Reflection.Missing.Value;            

            Application excel = new Application();

            Microsoft.Office.Interop.Excel.Workbook wbk = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            try
            {
                excel.Visible = false;
                excel.ScreenUpdating = false;
                excel.DisplayAlerts = false;

                FileInfo excelFile = new FileInfo(excelFileIn);

                string filename = excelFile.FullName;

                GetTheExcelProcessIdThatUsedByThisInstance();

                wbk = excel.Workbooks.Open(filename, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing);

                wbk.Activate();

                object outputFileName = pdfFileOut;

                Microsoft.Office.Interop.Excel.XlFixedFormatType fileFormat = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
                Microsoft.Office.Interop.Excel.XlFixedFormatQuality paramExportQuality = Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard;
                bool paramIncludeDocProps = true;
                bool paramIgnorePrintAreas = false;

                if (wbk != null)//save as pdf

                    wbk.ExportAsFixedFormat(fileFormat,
                                            outputFileName,
                                            paramExportQuality,
                                            paramIncludeDocProps,
                                            paramIgnorePrintAreas,
                                            missing,
                                            missing,
                                            missing,
                                            missing);                    

                object saveChanges = XlSaveAction.xlDoNotSaveChanges;
                ((_Workbook)wbk).Close(saveChanges, missing, missing);
                excel.Quit();

                // liberar
                releaseObject(wbk);
                releaseObject(excel);

                wbk = null;
                excel = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

            }
            catch (Exception ex)
            {
                // Cerrar
                ((_Workbook)wbk).Close(false, missing, missing);
                excel.Quit();

                // Elimina el archivo creado
                File.Delete(pdfFileOut);

                KillExcelProcessThatUsedByThisInstance();

                throw (ex);
            }
            finally
            {
                // Liberar
                releaseObject(wbk);
                releaseObject(excel);

                wbk = null;
                excel = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);                

                KillExcelProcessThatUsedByThisInstance();
            }
        }        

        private static void releaseObject(Object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }       

        void CheckForExistingExcellProcesses()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");
            myHashtable = new Hashtable();
            int iCount = 0;

            foreach (Process ExcelProcess in AllProcesses)
            {
                myHashtable.Add(ExcelProcess.Id, iCount);
                iCount = iCount + 1;
            }
        }

        void GetTheExcelProcessIdThatUsedByThisInstance()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");

            // Search For the Right Excel
            foreach (Process ExcelProcess in AllProcesses)
            {
                if (myHashtable == null)
                    return;

                if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                {
                    // Get the process ID
                    MyExcelProcessId = ExcelProcess.Id;
                }
            }

            AllProcesses = null;
        }

        void KillExcelProcessThatUsedByThisInstance()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");

            foreach (Process ExcelProcess in AllProcesses)
            {
                if (myHashtable == null)
                    return;

                if (ExcelProcess.Id == MyExcelProcessId)
                    ExcelProcess.Kill();
            }

            AllProcesses = null;
        }
    }
}

/// <summary>
/// Descripción breve de clsInformeLecturaXLS
/// </summary>
public class clsInformeLecturaXLS
{
    public clsInformeLecturaXLS()
    {

    }

    public int ID_CAMPO { get; set; }
    public string POSX { get; set; }
    public string POSY { get; set; }
    public string CELDA { get; set; }
    public string COORDENADA { get; set; }    
    public string VALOR { get; set; }
    public string POS_FC_X { get; set; }
    public string POS_FC_Y { get; set; }

}

public class clsInformeValidacionXLS
{
    public clsInformeValidacionXLS()
    {

    }

    public int ID_CAMPO { get; set; }
    public string POSX { get; set; }
    public string POSY { get; set; }
    public string CAMPO { get; set; }
    public string DESCRIPCION { get; set; }
}

