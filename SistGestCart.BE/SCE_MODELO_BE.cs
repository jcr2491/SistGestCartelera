using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_MODELO_BE
    {
        public SCE_MODELO_BE()
        {

        }

        public int ID_MODELO { get; set; }        
        public string NOM_MODELO { get; set; }
        public string ALTO { get; set; }
        public string ANCHO { get; set; }
        public string ORIENTACION { get; set; }
        public string TIPO_PAPEL { get; set; }
        public int FLAGPERTENECE { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; }        
    }
}
