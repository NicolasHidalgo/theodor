using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class COSTOOPERATIVO_BE
    {
        public long codSuscriptor { get; set; }
        public string codUsuario { get; set; }
        public string codOperacion { get; set; }
        public string operacion { get; set; }
        public int codCanalAtencion { get; set; }
        public string canalAtencion { get; set; }
        public double? costoOperativo { get; set; }
        public string tipReg { get; set; }
    }
}
