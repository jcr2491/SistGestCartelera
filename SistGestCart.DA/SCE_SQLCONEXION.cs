using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pe.oechsle.Entity;

namespace SistGestCart.DA
{
    public class SCE_SQLCONEXION
    {
        public static string GetCadConexion(Usuario usrLogin)        
        {
            string cadena = usrLogin.App.instancia + ";Persist Security Info=True;User ID=" + usrLogin.App.usuarioBD + ";Password=" + usrLogin.App.claveBD + "";

            return cadena;
        }
    }
}
