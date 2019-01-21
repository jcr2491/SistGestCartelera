using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE
    {
        public SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE()
        {

        }

        public int ID_CARTEL { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_CATEGORIA { get; set; }
        public int ID_PROMOCION { get; set; }
        public string DESCRIPCION { get; set; }

    }
}
