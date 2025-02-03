using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class PRODUCTO_BE
    {
        public string codOperacion { get; set; }
        public string operacion { get; set; }
        public int codProducto { get; set; }
        public string producto { get; set; }
        public int intCodProductoBase { get; set; }
        public string strCodProductoBase { get; set; }
        public string productoBase { get; set; }
        public string codTipClientes { get; set; }
        public int codTipAmortizacion { get; set; }
        public string tipAmortizacion { get; set; }
        public int plazo { get; set; }
        public double? tea { get; set; }
        public double? factorUsoLinea { get; set; }
        public double? factorConversionIndirectos { get; set; }
        public bool aplicaPricing { get; set; }
        public string tipReg { get; set; }
        public int orden { get; set; }
    }
}
