using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class XLS_PROCESO_BE
    {
        public int? ID_PROCESO { get; set; }
        public int? COD_CARGA { get; set; }
        public string NOM_CARGA { get; set; }
        public string NOM_HOJA { get; set; }
        public string PROCEDIMIENTO { get; set; }
        public bool BACKGROUND { get; set; }
        public Int64? IDE_CARGA { get; set; }
    }
}
