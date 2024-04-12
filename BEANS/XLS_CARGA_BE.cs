using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class XLS_CARGA_BE
    {
        public int? IDE_CARGA { get; set; }
        public int? COD_CARGA { get; set; }
        public DateTime? FEC_CARGA { get; set; }
        public string USERNAME { get; set; }
        public string ARCHIVO_FISICO { get; set; }
        public string ARCHIVO_CARGA { get; set; }
        public string NOM_HOJA { get; set; }
        public int? FILA_ENCABEZADO { get; set; }
        public int? COLUMNA_ENCABEZADO { get; set; }
        public string CONTROL_01 { get; set; }
        public string CONTROL_02 { get; set; }
        public int? COD_ESTADO { get; set; }
        public DateTime? FEC_PROCESO_INICIO { get; set; }
        public DateTime? FEC_PROCESO_FIN  { get; set; }
        public string ERROR { get; set; }
        public string PARAMETROS { get; set; }
        public string ESTADO { get; set; }
        public Int64? REG_PROCESADOS { get; set; }
    }
}
