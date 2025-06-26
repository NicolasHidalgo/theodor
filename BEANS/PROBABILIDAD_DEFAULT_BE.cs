using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class PROBABILIDAD_DEFAULT_BE
    {
        public long codSuscriptor { get; set; }
        public string codUsuario { get; set; }
        public string codTipCliente { get; set; }
        public string codClasificacionInterna { get; set; }
        public int? codProducto { get; set; }
        public string producto { get; set; }
        public int? codProductoBase { get; set; }
        public string productoBase { get; set; }
        public decimal? probabilidadDefault { get; set; }
        public decimal? tasaRecuperacion { get; set; }
        public decimal? LGD { get; set; }
        public decimal? perdidaEsperada { get; set; }

    }
}
