using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GUIA_DET_CAMPO_BE
    {
        public SCE_GUIA_DET_CAMPO_BE()
        {
            
        }

        public int ID_GUIA { get; set; }
        public int ID_LINEA { get; set; }
        public int ID_CAMPO { get; set; }
        public string VALOR { get; set; }
    }
}
