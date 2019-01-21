using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GRUPOTDA_TIENDA_BE
    {
        public SCE_GRUPOTDA_TIENDA_BE()
        {

        }

        public int ID_GRUPO { get; set; }
        public int ID_TIENDA { get; set; }
        public string NOM_TIENDA { get; set; }        
    }
}
