using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class GEN_UNIDAD_NEGOCIO_BE
    {
        public string cod_unidad_negocio { get; set; }
        public string nom_unidad_negocio { get; set; }
        public string cod_sociedad { get; set; }
        public string cod_sociedad_costo { get; set; }
        public string cod_centro_beneficio { get; set; }
        public string identificador_RRHH { get; set; }
        public short notas { get; set; }
        public short tip_cierre { get; set; }
        public string nom_operativo { get; set; }
        public int niveles { get; set; }
    }
}
