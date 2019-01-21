using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CATEGORIA_BE
    {
        public SCE_CATEGORIA_BE()
        {

        }

        public int ID_CATEGORIA { get; set; }
        public string NOM_CATEGORIA { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; } 
    }
}
