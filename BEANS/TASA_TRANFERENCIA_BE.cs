using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class TASA_TRANFERENCIA_BE
    {
        public DateTime? fecVigencia { get; set; }
        public int? periodo { get; set; }
        public string ori { get; set; }
        public double? anio { get; set; }
        public double? tasaSol { get; set; }
        public double? poolFondoSol { get; set; }
        public double? tasaUsd { get; set; }
        public double? poolFondoUsd { get; set; }


    }

    public class TASA_TRANF_VALOR_BE
    {
        public DateTime? fecVigencia { get; set; }
        public int codMoneda { get; set; }
        public string moneda { get; set; }
        public double? encaje { get; set; }
        public double? beta0 { get; set; }
        public double? beta1 { get; set; }
        public double? beta2 { get; set; }
        public double? beta3 { get; set; }
        public double? lambda1 { get; set; }
        public double? lambda2 { get; set; }

    }

    public class TASA_TRANFERENCIA_GRABAR
    {
        public List<TASA_TRANFERENCIA_BE> lstTasas { get; set; }
        public List<TASA_TRANF_VALOR_BE> lstConstantes { get; set; }
    }
}
