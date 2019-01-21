using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;

namespace SistGestCart.BL
{
    public class SCE_CARTEL_BL
    {
        Usuario usrLogin;
        SCE_CARTEL_BE BE;

        public SCE_CARTEL_BL()
        {
            
        }

        public SCE_CARTEL_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;           
        }

        public string IsValid(int Id, string nombre, bool operacion)
        {
            string mensaje = "";

            try
            {
                if ((nombre == null) || (nombre.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MC_CN"];
                }

                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);
                List<SCE_CARTEL_BE> plista = DA.Listar();

                if (operacion == false)
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if ((plista[i].ID_CARTEL != Id) && (plista[i].CARTEL == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MC_ND"];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if (plista[i].CARTEL == nombre)
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MC_ND"];
                        }
                    }
                }

                return mensaje;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidarIntegridad(int IdCartel, int IdModelo)
        {
            try
            {
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);
               
                if (DA.ExisteCartelModeloInTB_CMC(IdCartel, IdModelo))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["MC_VIR"];
                }

                if (DA.ExisteCartelModeloInTB_CMCP(IdCartel, IdModelo))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["MC_VIR"];
                }               
               
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_DIGITOS> loadListDigitos()
        {
            List<SCE_DIGITOS> lstDig = new List<SCE_DIGITOS>();
            SCE_DIGITOS DIGITOS = null;

            DIGITOS = new SCE_DIGITOS();
            DIGITOS.ID = "1D";
            DIGITOS.DESCRIPCION = "1 DIGITO(S)";
            lstDig.Add(DIGITOS);

            DIGITOS = new SCE_DIGITOS();
            DIGITOS.ID = "2D";
            DIGITOS.DESCRIPCION = "2 DIGITO(S)";
            lstDig.Add(DIGITOS);

            DIGITOS = new SCE_DIGITOS();
            DIGITOS.ID = "3D";
            DIGITOS.DESCRIPCION = "3 DIGITO(S)";
            lstDig.Add(DIGITOS);

            DIGITOS = new SCE_DIGITOS();
            DIGITOS.ID = "4D";
            DIGITOS.DESCRIPCION = "4 DIGITO(S)";
            lstDig.Add(DIGITOS);

            return lstDig;
        }      

        public void Insertar(string nombre, 
                             List<SCE_CARTEL_MODELO_BE> lstModelos, 
                             int numMaxDigitos, 
                             bool CeroDigitos)
        {
            string strCeroDigitos = string.Empty;

            try
            {
                BE = new SCE_CARTEL_BE();
                BE.NOM_CARTEL = nombre;
                BE.MODELOS = lstModelos;
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (CeroDigitos == true)
                    {
                        strCeroDigitos = "S";
                    }
                    else
                    {
                        strCeroDigitos = "N";  
                    }

                    DA.Insertar(BE, numMaxDigitos, strCeroDigitos);

                    scope.Complete();                                     
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(int Id, 
                               string nombre, 
                               List<SCE_CARTEL_MODELO_BE> lstModelos,
                               int numMaxDigitos,
                               bool CeroDigitos)
        {
            string strCeroDigitos = string.Empty;

            try
            {
                BE = new SCE_CARTEL_BE();
                BE.ID_CARTEL = Id;
                BE.NOM_CARTEL = nombre;
                BE.MODELOS = lstModelos;
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (CeroDigitos == true)
                    {
                        strCeroDigitos = "S";
                    }
                    else
                    {
                        strCeroDigitos = "N";
                    }

                    DA.Actualizar(BE, numMaxDigitos, strCeroDigitos);

                    scope.Complete();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Eliminar(int Id)
        {
            try
            {               
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);
                return DA.Eliminar(Id);                                      
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_CARTEL_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Listar()
        {
            int numMaxDigitos = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NroMaxDigitos"]);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = null;

            try
            {
                DA.SCE_CARTEL_DA DA = new DA.SCE_CARTEL_DA(usrLogin);
                if (DA.Listar().Count > 0)
                {
                    return PivotDtCartelXModelo(ListToDataTable(DA.Listar()), "NOM_MODELO", numMaxDigitos);
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

        /****************************************************************************************/
        /************************************ FUNCIONES ESPECIALES ******************************/
        /****************************************************************************************/
        private static DataTable ListToDataTable(List<SCE_CARTEL_BE> List)
        {
            DataTable oDataTableReturned = new DataTable();

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

        private static object ValorDefault(object objeto)
        {
            if (objeto == System.DBNull.Value)
                return "";
            else
                return objeto;
        }
        /****************************************************************************************/

        public DataTable PivotDtCartelXModelo(DataTable dt, string columnName, int numMaxDigitos)
        {
            //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
            //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            DataTable dtPivot = new DataTable();

            dtPivot.Columns.Add("ID_CARTEL", typeof(int));
            dtPivot.Columns.Add("CARTEL", typeof(String));

            //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
            //DE LA TABLA PIVOTEADA
            //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (columnName == null || columnName.Length == 0)
            {
                throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
            }

            DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

            for (int i = 0; i < distintos0.Rows.Count; i++)
            {
                dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
            }

            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //'LLENA EL DATATABLE PIVOTEADO
            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_CARTEL", "CARTEL");
            DataRow row = null;

            for (int i = 0; i < distintos1.Rows.Count; i++)
            {
                row = dtPivot.NewRow();

                row["ID_CARTEL"] = distintos1.Rows[i][0];
                row["CARTEL"] = distintos1.Rows[i][1];

                DataTable dtAux = new DataTable();
                string strCrtFiltro = null;

                strCrtFiltro = "ID_CARTEL = " + distintos1.Rows[i][0];

                dtAux = FiltraDataTable(dt, strCrtFiltro);

                for (int cols = 2; cols < dtPivot.Columns.Count; cols++)
                {
                    for (int j = 0; j < dtAux.Rows.Count; j++)
                    {
                        //Se hizo el cambio de dtAux.Rows[j][3].ToString() == "1" a dtAux.Rows[j][3].ToString() == "5"
                        if (((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][2].ToString()) && (dtAux.Rows[j][3].ToString() == Convert.ToString(numMaxDigitos))) || ((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][2].ToString()) && (dtAux.Rows[j][3].ToString() == "1")))
                        {
                            row[cols] = "x";
                        }
                    }
                }

                dtPivot.DefaultView.Sort = "CARTEL";

                dtPivot.Rows.Add(row);
            }

            return dtPivot;
        }

        public DataTable FiltraDataTable(DataTable dtprincipal, string ctrFiltro)
        {
            /* Seleccionamos las filas que se correspondan con el identificador */
            System.Data.DataRow[] rows = dtprincipal.Select(ctrFiltro);

            /* Clonamos la estructura del objeto DataTable principal */
            DataTable dt1 = dtprincipal.Clone();

            // Importamos los registros al nuevo DataTable
            foreach (DataRow row in rows)
            {
                dt1.ImportRow(row);
            }

            return dt1;
        }
    }
}

[Serializable]
public class SCE_DIGITOS
{
    public SCE_DIGITOS()
    {
        
    }

    public string ID { get; set; }
    public string DESCRIPCION { get; set; }
}
