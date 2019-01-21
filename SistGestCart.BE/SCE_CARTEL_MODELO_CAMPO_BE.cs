using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{   
    [Serializable]
    public class SCE_CARTEL_MODELO_CAMPO_BE
    {
        public SCE_CARTEL_MODELO_CAMPO_BE()
        {

        }

        public int ID_CARTEL { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_CAMPO { get; set; }
        public string NOM_CAMPO { get; set; }
        public int FLAGPERTENECE { get; set; }
        public string ALIAS { get; set; }
        public string POSX { get; set; }
        public string POSY { get; set; }
        public string CAMPO { get; set; }
        public string DESCRIPCION { get; set; }
        public int DIGITOS { get; set; }
    }
}
