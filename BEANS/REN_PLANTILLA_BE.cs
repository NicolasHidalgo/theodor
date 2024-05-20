using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class REN_POPUP_BE
    {
        public long ide_cliente_Producto { get; set; }
        public string codigo { get; set; }
        public string tip_documento { get; set; }
        public string num_documento { get; set; }
        public string Cliente { get; set; }
        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Monto { get; set; }
        public string Profit { get; set; }
        public string RORAC { get; set; }

        public string Tea_base { get; set; }
        public string Tea_efectiva { get; set; }
        public double? plazo { get; set; }
        public string cod_usuario { get; set; }
        public string Fecha { get; set; }
    }
}
