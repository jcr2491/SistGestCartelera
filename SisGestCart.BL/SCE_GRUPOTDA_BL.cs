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
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;
using System.Transactions;

namespace SistGestCart.BL
{
    public class SCE_GRUPOTDA_BL
    {
        Usuario usrLogin;
        SCE_GRUPOTDA_BE BE;

        public SCE_GRUPOTDA_BL()
        {
            
        }

        public SCE_GRUPOTDA_BL(Usuario usrLogin)
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
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MGT_CN"];
                }

                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);
                List<SCE_GRUPOTDA_BE> plista = DA.Listar();

                if (operacion == false)
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if ((plista[i].ID_GRUPO != Id) && (plista[i].NOM_GRUPO == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MGT_ND"];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if (plista[i].NOM_GRUPO == nombre)
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MGT_ND"];
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

        public void Insertar(string nombre, List<SCE_GRUPOTDA_TIENDA_BE> lstTiendas)
        {
            try
            {
                BE = new SCE_GRUPOTDA_BE();
                BE.NOM_GRUPO = nombre;
                BE.TIENDAS = lstTiendas;
                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.Insertar(BE);
                    scope.Complete();                    
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(int Id, string nombre, List<SCE_GRUPOTDA_TIENDA_BE> lstTiendas)
        {
            try
            {
                BE = new SCE_GRUPOTDA_BE();
                BE.ID_GRUPO = Id;
                BE.NOM_GRUPO = nombre;
                BE.TIENDAS = lstTiendas;
                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.Actualizar(BE);
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
                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);
                return DA.Eliminar(Id);                                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SCE_GRUPOTDA_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_GRUPOTDA_BE> Listar()
        {
            try
            {
                DA.SCE_GRUPOTDA_DA DA = new DA.SCE_GRUPOTDA_DA(usrLogin);
                return DA.Listar();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /****************************************************************************************/
        /************************************ FUNCIONES ESPECIALES ******************************/
        /****************************************************************************************/

        public DataTable ListToDataTable(List<SCE_GRUPOTDA_TIENDA_BE> List)
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

    }
}
