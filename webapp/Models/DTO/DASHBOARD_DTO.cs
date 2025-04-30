using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models.DTO
{
    public class DASHBOARD_DTO
    {
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public string codMoneda { get; set; }
        public string codPersoneria { get; set; }
        public string codTipCliente { get; set; }
        public string codOperacion { get; set; }
        public string codProducto { get; set; }
        public string codClasificacionInterna { get; set; }
        public string garantia { get; set; }
        public string codAgencia { get; set; }
        public string codFuncionario { get; set; }
    }
}