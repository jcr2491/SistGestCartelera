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
    public class SCE_CAMPO_BL
    {
        Usuario usrLogin;
        SCE_CAMPO_BE BE;

        public SCE_CAMPO_BL()
        {
               
        }

        public SCE_CAMPO_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;         
        }

        public string IsValid(int Id, string alias, string nombre)
        {           
            string mensaje = "";           

            try
            {
                if ((alias == null) || (alias.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAM_CNNA"];
                }

                if ((nombre == null) || (nombre.Length == 0))
                {
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAM_CNNC"];
                }                

                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                List<SCE_CAMPO_BE> plista = DA.Listar();
                
                for (int i = 0; i < plista.Count; i++)
                {
                    if (plista[i].ALIAS == nombre)
                    {
                        return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAM_NCD"];
                    }

                    if (plista[i].ALIAS == alias)
                    {
                        return mensaje = System.Configuration.ConfigurationManager.AppSettings["MCAM_ACD"];
                    }
                }                

                return mensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EsCampoValidable(int IdCartel, int IdCampo)
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);

                if (DA.EsCampoValidable(IdCartel, IdCampo))
                {
                    return "No puede asociar este campo porque es validable por digitos.";
                }              

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(string alias, string nombre, string restringir, string valDigitos, string tipo)
        {
            try
            {
                BE = new SCE_CAMPO_BE();

                BE.ALIAS = alias;
                BE.NOM_CAMPO = nombre;
                BE.RESTINGIR = restringir;
                BE.VALDIGITOS = valDigitos;
                BE.TIPO = tipo;

                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                DA.Insertar(BE);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(int Id, string alias, string nombre)
        {
            try
            {
                BE = new SCE_CAMPO_BE();

                BE.ID_CAMPO = Id;
                BE.ALIAS = alias;
                BE.NOM_CAMPO = nombre;               

                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                DA.Actualizar(BE);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Eliminar(int Id, string Alias)
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);

                return DA.Eliminar(Id, Alias);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_CAMPO_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CAMPO_BE> Listar()
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                return DA.Listar();               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_CAMPO_BE> Listar1()
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                return DA.Listar1();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIdCampo(string NomCampo)
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                return DA.GetIdCampo(NomCampo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
