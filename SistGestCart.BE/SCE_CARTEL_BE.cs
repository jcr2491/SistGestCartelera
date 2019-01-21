using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CARTEL_BE
    {
        public SCE_CARTEL_BE()
        {
            this.MODELOS = new List<SCE_CARTEL_MODELO_BE>();
        }

        public int ID_CARTEL { get; set; }
        public string NOM_CARTEL { get; set; }
        public string NOM_MODELO { get; set; }
        public int FLAGPERTENECE { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; }
        public string CARTEL { get; set; }
        public byte CERO_DIGITOS { get; set; }
        public List<SCE_CARTEL_MODELO_BE> MODELOS { get; set; }
    }
}
