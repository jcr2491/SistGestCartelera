using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CAMPO_BE
    {
        public SCE_CAMPO_BE()
        {

        }

        public int ID_CAMPO { get; set; }
        public string ALIAS { get; set; }
        public string NOM_CAMPO { get; set; }
        public string TIPO { get; set; }
        public int LONGITUD { get; set; }
        public int DECIMALES { get; set; }
        public int FLAGPERTENECE { get; set; }
        public DateTime FECHA_CRE { get; set; }
        public DateTime FECHA_MOD { get; set; }
        public string RESTINGIR { get; set; }
        public string VALDIGITOS { get; set; }
    }
}
