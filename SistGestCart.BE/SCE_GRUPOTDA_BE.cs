using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GRUPOTDA_BE
    {
        public SCE_GRUPOTDA_BE()
        {
            this.TIENDAS = new List<SCE_GRUPOTDA_TIENDA_BE>();
        }

        public int ID_GRUPO { get; set; }
        public String NOM_GRUPO { get; set; }        
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; }
        public List<SCE_GRUPOTDA_TIENDA_BE> TIENDAS { get; set; }
    }

    
}
