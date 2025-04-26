using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class XLS_PROCESO_ESTRUCTURA_BE
    {
        public int? ID_ESTRUCTURA { get; set; }
        public int? COD_CARGA { get; set; }
        public string NOM_CAMPO { get; set; }
        public string TIPO { get; set; }
        public string EQUIVALENCIA { get; set; }
        public int? ORDEN { get; set; }
        public string NOM_HOJA { get; set; }
        public string COMENTARIO { get; set; }
    }
}
