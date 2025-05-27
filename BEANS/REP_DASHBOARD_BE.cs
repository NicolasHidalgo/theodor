using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class REP_DASHBOARD_BE
    {
        public int item { get; set; }
        public string rubro { get; set; }
        public DateTime? fecha { get; set; }
        public string codRef { get; set; }
        public string nomRef { get; set; }
        public int noEnProceso { get; set; }
        public double? montoEnProceso { get; set; }
        public int noDesembolsado { get; set; }
        public double? desembolso { get; set; }
        public double? desembolsado { get; set; }
        public double? tea { get; set; }
        public double? perdidaEsperada { get; set; }
        public double? spread { get; set; }
        public double? profit { get; set; }
        public double? RORAC { get; set; }
        public double? utilidad { get; set; }
        public double? desembolsoObjetivo { get; set; }
        public double? TEAObjetivo { get; set; }
        public double? utilidadObjetivo { get; set; }
        public double? RORACObjetivo { get; set; }
    }

    public class REP_DASHBOARD3_BE
    {
        public int item { get; set; }
        public string codCat { get; set; }
        public string categoria { get; set; }
        public string color { get; set; }
        public string codRub { get; set; }
        public string rubro { get; set; }
        public int no { get; set; }
        public double? ratio { get; set; }
        public double? monto { get; set; }
        public double? tea { get; set; }
        public double? esperada { get; set; }
        public double? spread { get; set; }
        public double? profit { get; set; }
        public double? RORAC { get; set; }
        public double? utilidad { get; set; }
    }

    public class REP_DASHBOARD_PARAM
    {
        public string accion { get; set; }
        public long codSuscriptor { get; set;}
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public int codMoneda { get; set; }
        public string codPersoneria { get; set; }
        public string codTipCliente { get; set; }
        public string codOperacion { get; set; }
        public int codProducto { get; set; }
        public string codClasificacionInterna { get; set; }
        public bool garantia { get; set; }
        public string codAgencia { get; set; }
        public int codFuncionario { get; set; }

    }
}
