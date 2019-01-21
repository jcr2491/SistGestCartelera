using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GUIA_DET_BE
    {
        public SCE_GUIA_DET_BE()
        {
            
        }

        public int ID_GUIA { get; set; }
        public int ID_LINEA { get; set; }
        public int ID_CATEGORIA { get; set; }
        public int ID_PROMOCION { get; set; }
        public int FLG_IMPRESION { get; set; }        
    }
}
