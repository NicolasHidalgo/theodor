using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class COMISIONSERVICIO_BE
    {
        public int codProducto { get; set; }
        public string producto { get; set; }
        public int codComisionServicio { get; set; }
        public string comisionServicio { get; set; }
        public int codPeriodicidad { get; set; }
        public string periodicidad { get; set; }
        public string tipValor { get; set; }
        public string nomTipValor { get; set; }
        public double? veces { get; set; }
        public double? valorMn1 { get; set; }
        public double? valorMn2 { get; set; }

    }
}
