using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class GEN_TIEMPO_BE
    {

        public int periodo { get; set; }
        public int mes { get; set; }
        public string nom_mes { get; set; }
        public int cod_rango_fecha { get; set; }
        public string nom_rango_fecha { get; set; }
        public DateTime? fec_desde { get; set; }
        public DateTime? fec_hasta { get; set; }
        public int est_aprobacion { get; set; }
        public string titulo { get; set; }
    }
}
