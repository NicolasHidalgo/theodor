using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class XLS_PROCESO_PARAMETRO_BE
    {
        public int? COD_CARGA { get; set; }
        public int? ORDEN { get; set; }
        public string NOM_CONTROL { get; set; }

        public string COLUMN_NAME { get; set; }
        public string ETIQUETA { get; set; }

        public IEnumerable<XLS_PROCESO_PARAMETRO_OPTION_BE> lstOption { get; set; }
    }

    public class XLS_PROCESO_PARAMETRO_OPTION_BE
    {
        public string CODIGO { get; set; }
        public string NOM_CONTROL { get; set; }
    }
}
