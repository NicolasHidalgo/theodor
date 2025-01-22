using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class RORACOBJETIVO_BE
    {
        public string codPersoneria { get; set; }
        public string personeria { get; set; }
        public string codTipCliente { get; set; }
        public string tipCliente { get; set; }
        public int? codProductoBase { get; set; }
        public string productoBase { get; set; }
        public double? roracObjetivo { get; set; }
        public double? autonomiaComercial { get; set; }
        public string tipReg { get; set; }

    }
}
