using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;

namespace SistGestCart.BL
{
    public class SCE_MODELO_BL
    {
        Usuario usrLogin;
        SCE_MODELO_BE BE;

        public SCE_MODELO_BL()
        {
            
        }

        public SCE_MODELO_BL(Usuario usrLogin)
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
                    return mensaje = System.Configuration.ConfigurationManager.AppSettings["MM_CN"];
                }

                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                List<SCE_MODELO_BE> plista = DA.Listar();

                if (operacion == false)
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if ((plista[i].ID_MODELO != Id) && (plista[i].NOM_MODELO == nombre))
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MM_CN"];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < plista.Count; i++)
                    {
                        if (plista[i].NOM_MODELO == nombre)
                        {
                            return mensaje = System.Configuration.ConfigurationManager.AppSettings["MGT_CN"];
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

        public string Insertar(string nombre)
        {
            try
            {
                BE = new SCE_MODELO_BE();

                BE.NOM_MODELO = nombre;

                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                DA.Insertar(BE);

                return System.Configuration.ConfigurationManager.AppSettings["MM_AD"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Actualizar(int Id, string nombre)
        {
            try
            {
                BE = new SCE_MODELO_BE();

                BE.ID_MODELO = Id;
                BE.NOM_MODELO = nombre;

                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                DA.Actualizar(BE);

                return System.Configuration.ConfigurationManager.AppSettings["MM_MD"];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string Eliminar(int Id)
        {
            try
            {
                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                DA.Eliminar(Id);

                return System.Configuration.ConfigurationManager.AppSettings["MM_ED"];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SCE_MODELO_BE ObtenerPorID(int Id)
        {
            try
            {
                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                return DA.ObtenerPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_MODELO_BE> Listar()
        {
            try
            {
                DA.SCE_MODELO_DA DA = new DA.SCE_MODELO_DA(usrLogin);
                return DA.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
