using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GUIA_EXCEPCION_BE
    {
        public SCE_GUIA_EXCEPCION_BE()
        {
           
        }

        public int ID_GUIA { get; set; }
        public int ID_LINEA { get; set; }
        public int ID_TIENDA { get; set; }
        public int ID_GRUPO { get; set; }        
        public string VALOR { get; set; }
        public string DESCRIPCION { get; set; }        
        public int FLAGPERTENECE { get; set; }
        public string NOM_TIENDA { get; set; }
        public string USER_MOD { get; set; }
        
    }
}
