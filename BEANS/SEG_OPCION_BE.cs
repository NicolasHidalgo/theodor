using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class SEG_OPCION_BE
    {
        public SEG_OPCION_BE()
        {
            // constructor
        }
        public Int64 COD_MENU { get; set; }
        public int COD_MENU_PADRE { get; set; }
        public string NOM_MENU { get; set; }
        public string NAVEGACION_URL { get; set; }
        public string TARGET_MENU { get; set; }
        public string ACTION { get; set; }
        public string CONTROLLER { get; set; }
        public string PARAMETRO { get; set; }
        public string ICONO { get; set; }
        public string TIP_MENU { get; set; }
        public IEnumerable<SEG_OPCION_BE> OpcionHijo { get; set; } = new HashSet<SEG_OPCION_BE>();


        //Campos del Carousel
        public int ide_carousel { get; set; }
        public string imagen { get; set; }
        public string cod_unidad_negocio { get; set; }
        public string cod_aplicacion { get; set; }
        public int ide_aplicacion { get; set; }
        public string nom_aplicacion { get; set; }
    }
}
