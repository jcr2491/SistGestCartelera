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
using pe.oechsle.Entity;
using System.Globalization;
using System.Threading;
using SistGestCart.BE;
using SistGestCart.DA;

namespace SistGestCart.BL
{
    [Serializable]
    public class SCE_FILE_GUIA_MASIVA_BL
    {
        Usuario usrLogin;

        SCE_FILE_GUIA_MASIVA_BE BE;

        public SCE_FILE_GUIA_MASIVA_BL()
        {
            
        }

        public SCE_FILE_GUIA_MASIVA_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;           
        }

        public List<SCE_FILE_GUIA_MASIVA_BE> CboFilesGuia()
        {
            try
            {
                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                return DA.CboFilesGuia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataTable ListarFilesGuia()
        {
            try
            {
                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                return ListToDataTable(DA.ListarFilesGuia());               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_FILE_GUIA_MASIVA_BE ObtenerFileGuia(int Id)
        {
            try
            {
                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                return DA.ObtenerFileGuia(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarFileGuia(string nomFile,
                                   string Usuario)
        {
            try
            {
                BE = new SCE_FILE_GUIA_MASIVA_BE();
                BE.NOM_FILE = nomFile;
                BE.USER_CRE = Usuario;

                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                DA.CargarFileGuia(BE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarFileGuia(int IdFile,
                                       string nomFile,
                                       string Estado,                       
                                       string Usuario,
                                       string nomFileOrig)
        {
            try
            {
                BE = new SCE_FILE_GUIA_MASIVA_BE();
                BE.ID_FILE = IdFile;
                BE.NOM_FILE = nomFile;
                BE.ESTADO = Estado; 
                BE.USER_MOD = Usuario;

                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                //EliminarFileGuia(nomFileOrig);
                DA.ActualizarFileGuia(BE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarFileGuia(int Id, string nomFile)
        {
            try
            {
                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                EliminarFileGuia(nomFile);
                return DA.EliminarFileGuia(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaFileGuia(int IdFile,
                                 string Usuario)
        {
            try
            {
                DA.SCE_FILE_GUIA_MASIVA_DA DA = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                DA.BajaFileGuia(IdFile, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EliminarFileGuia(string nomFile)
        {
            string sPath = System.Configuration.ConfigurationManager.AppSettings["PATH_GMA"];

            if (File.Exists(sPath + nomFile))
            {
                File.Delete(sPath + nomFile);
            }
        }

        private static DataTable ListToDataTable(List<SCE_FILE_GUIA_MASIVA_BE> List)
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
    }
}