using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_PROMOCION_BE
    {
        public SCE_PROMOCION_BE()
        {

        }

        public int ID_PROMOCION { get; set; }
        public string NOM_PROMOCION { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; } 
    }
}
