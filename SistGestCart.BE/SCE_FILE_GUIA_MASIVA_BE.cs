using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_FILE_GUIA_MASIVA_BE
    {
        public int ID_FILE { get; set; }
        public string NOM_FILE { get; set; }
        public string ESTADO { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public string USER_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; }
        public string USER_MOD { get; set; }  
    }
}
