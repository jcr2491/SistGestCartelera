using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;

namespace SistGestCart.BL
{
    public class SCE_CATEGORIA_BL
    {
        Usuario usrLogin;
        SCE_CATEGORIA_BE BE;

        public SCE_CATEGORIA_BL()
        {
            
        }

        public SCE_CATEGORIA_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;           
        }

        public string IsValid(int Id, string nombre)
        {
            string mensaje = "";

            try
            {
                if ((nombre == null) || (nombre.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAT_CN"];
                }

                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
                List<SCE_CATEGORIA_BE> plista = DA.Listar();

                for (int i = 0; i < plista.Count; i++)
                {
                    if (plista[i].NOM_CATEGORIA == nombre)
                    {
                        return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAT_ND"];
                    }
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(string nombre)
        {
            try
            {
                BE = new SCE_CATEGORIA_BE();

                BE.NOM_CATEGORIA = nombre;

                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
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
                BE = new SCE_CATEGORIA_BE();

                BE.ID_CATEGORIA = Id;
                BE.NOM_CATEGORIA = nombre;

                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
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
                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
                return DA.Eliminar(Id);                     
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SCE_CATEGORIA_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
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
                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
                return ListToDataTable(DA.Listar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CATEGORIA_BE> ListarConf()
        {
            try
            {
                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
                return DA.ListarConf();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNombreCategoria(int IdCategoria)
        {
            try
            {
                DA.SCE_CATEGORIA_DA DA = new DA.SCE_CATEGORIA_DA(usrLogin);
                return DA.GetNombreCategoria(IdCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataTable ListToDataTable(List<SCE_CATEGORIA_BE> List)
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
