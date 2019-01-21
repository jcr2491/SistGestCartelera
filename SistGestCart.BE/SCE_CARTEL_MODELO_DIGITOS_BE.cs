using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CARTEL_MODELO_DIGITOS_BE
    {
        public SCE_CARTEL_MODELO_DIGITOS_BE()
        {

        }

        public int ID_CARTEL { get; set; }
        public int ID_MODELO { get; set; }
        public int DIGITOS { get; set; }
    }
}
