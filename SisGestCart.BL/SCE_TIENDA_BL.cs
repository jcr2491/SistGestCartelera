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
    public class SCE_TIENDA_BL
    {
        Usuario usrLogin;
        SCE_TIENDA_BE BE;

        public SCE_TIENDA_BL()
        {

        }

        public SCE_TIENDA_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public string IsValid(int Id, string nombre, bool operacion)
        {
            string mensaje = "";

            try
            {
                if ((Id == 0) || (nombre.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_CN"];
                }

                if ((nombre == null) || (nombre.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_CN"];
                }

                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                List<SCE_TIENDA_BE> plista = DA.Listar();

                if (operacion == false)
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if ((plista[i].ID_TIENDA != Id) && (plista[i].NOM_TIENDA == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_ND"];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if ((plista[i].ID_TIENDA == Id) && (plista[i].NOM_TIENDA == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_ND"];
                        }

                        if ((plista[i].ID_TIENDA == Id) && (plista[i].NOM_TIENDA != nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_ND"];
                        }

                        if ((plista[i].ID_TIENDA != Id) && (plista[i].NOM_TIENDA == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MT_ND"];
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

        public void Insertar(int Id, string nombre)
        {
            try
            {
                BE = new SCE_TIENDA_BE();

                BE.ID_TIENDA = Id;
                BE.NOM_TIENDA = nombre;

                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                DA.Insertar(BE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(int Id, string nombre)
        {
            try
            {
                BE = new SCE_TIENDA_BE();

                BE.ID_TIENDA = Id;
                BE.NOM_TIENDA = nombre;

                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                DA.Actualizar(BE);
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
                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                return DA.Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_TIENDA_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Listar()
        {
            try
            {
                DA.SCE_TIENDA_DA DA = new DA.SCE_TIENDA_DA(usrLogin);
                return ListToDataTable(DA.Listar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable ListToDataTable(List<SCE_TIENDA_BE> List)
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
