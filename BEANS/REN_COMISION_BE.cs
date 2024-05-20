using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class REN_COMISION_BE
    {
        public string accion { get; set; }
        public long cod_suscriptor { get; set; }
        public long ide_cliente_producto { get; set; }
        public int ide_comision { get; set; }
        public int cod_comision_servicio { get; set; }
        public string comision_servicio { get; set; }
        public string cPeriodicidad { get; set; }
        public int cod_periodicidad { get; set; }
        public string tip_valor { get; set; }
        public double? Porcentaje { get; set; }
        public double? veces { get; set; }
        public double? Comision { get; set; }
        public string simbolo { get; set; }
        public double? valor { get; set; }
        public double incremento_tasa { get; set; }
        public double incremento_plazo { get; set; }

    }
}
