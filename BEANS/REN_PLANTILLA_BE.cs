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
        public string Documento { get; set; }
        public string Cliente { get; set; }
        public string Operacion { get; set; }
        public string Producto { get; set; }
        public string Monto { get; set; }
        public string Esc { get; set; }

        public string Tea { get; set; }
        public double? Plazo { get; set; }
        public string cod_usuario { get; set; }
    }
}
