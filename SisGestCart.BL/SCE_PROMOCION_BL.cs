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
    public class SCE_PROMOCION_BL
    {
        Usuario usrLogin;
        SCE_PROMOCION_BE BE;

        public SCE_PROMOCION_BL()
        {
            
        }

        public SCE_PROMOCION_BL(Usuario usrLogin)
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
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MP_CN"];
                }

                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
                List<SCE_PROMOCION_BE> plista = DA.Listar();               
               
                for (int i = 0; i < plista.Count; i++)
                {
                    if (plista[i].NOM_PROMOCION == nombre)
                    {
                        return mensaje = System.Configuration.ConfigurationManager.AppSettings["MP_ND"];
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
                BE = new SCE_PROMOCION_BE();

                BE.NOM_PROMOCION = nombre;

                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
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
                BE = new SCE_PROMOCION_BE();

                BE.ID_PROMOCION = Id;
                BE.NOM_PROMOCION = nombre;

                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
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
                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);                
                return DA.Eliminar(Id);                          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_PROMOCION_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_PROMOCION_BE> Listar()
        {
            try
            {
                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
                return DA.Listar();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_PROMOCION_BE> ListarConf()
        {
            try
            {
                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
                return DA.ListarConf();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNombrePromocion(int IdPromocion)
        {
            try
            {
                DA.SCE_PROMOCION_DA DA = new DA.SCE_PROMOCION_DA(usrLogin);
                return DA.GetNombrePromocion(IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
