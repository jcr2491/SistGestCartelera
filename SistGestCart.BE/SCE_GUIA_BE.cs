using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistGestCart.BE
{
    [Serializable]
    public class SCE_GUIA_BE
    {
        public SCE_GUIA_BE()
        {
            this.DETALLE_GUIA = new List<SCE_GUIA_DET_BE>();
            this.DETALLE_GUIA_CAMPOS = new List<SCE_GUIA_DET_CAMPO_BE>();
            this.DETALLE_GUIA_EXCEPCIONES = new List<SCE_GUIA_EXCEPCION_BE>();
        }

        public int ID_GUIA { get; set; }        		
	    public string NOM_GUIA { get; set; }   
	    public int TIPO_GUIA { get; set; }     
	    public int ESTADO_GUIA { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }  
        public int ID_TIENDA { get; set; }
	    public int ID_GRUPO { get; set; }            	
	    public DateTime FECHA_CRE { get; set; }          		
	    public DateTime FECHA_MOD { get; set; }
        public int ID_PROMOCION { get; set; }
        public string NOM_PROMOCION { get; set; }
        public int ID_CATEGORIA { get; set; }
        public string NOM_CATEGORIA { get; set; }
        public string DESTADO_GUIA { get; set; }
        public string DES_TIPO_GUIA { get; set; }
        public string NOM_TIENDA { get; set; }
        public string NOM_GRUPO { get; set; }
        public string USER_CRE { get; set; }
        public string USER_MOD { get; set; }       
        public  List<SCE_GUIA_DET_BE> DETALLE_GUIA { get; set; }
        public List<SCE_GUIA_DET_CAMPO_BE> DETALLE_GUIA_CAMPOS { get; set; }
        public List<SCE_GUIA_EXCEPCION_BE> DETALLE_GUIA_EXCEPCIONES { get; set; }
	             		
    }
}
