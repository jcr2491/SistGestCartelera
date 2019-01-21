using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_CARTEL_MODELO_BE
    {
        public SCE_CARTEL_MODELO_BE()
        {
            this.CAMPOS = new List<SCE_CARTEL_MODELO_CAMPO_BE>();
            this.CATEGS_PROMOS = new List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE>();
            this.CARTEL_MODELO_DIGITOS = new List<SCE_CARTEL_MODELO_DIGITOS_BE>();
        }

        public int ID_CARTEL { get; set; }
        public int ID_MODELO { get; set; }
        public string NOM_CARTEL { get; set; }
        public string NOM_MODELO { get; set; }       
        public int FLAGPERTENECE { get; set; }      
        public string NOM_CAMPO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOM_CATEGORIA { get; set; }
        public string NOM_PROMOCION { get; set; }
        public string NOM_PLANTILLA { get; set; }
        public string POS_FC_X { get; set; }
        public string POS_FC_Y { get; set; }
        public int DIGITOS { get; set; }
        public string NRODIGITOS { get; set; }
        public List<SCE_CARTEL_MODELO_CAMPO_BE> CAMPOS { get; set; }
        public List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> CATEGS_PROMOS { get; set; }
        public List<SCE_CARTEL_MODELO_DIGITOS_BE> CARTEL_MODELO_DIGITOS { get; set; }
      
    }
}
