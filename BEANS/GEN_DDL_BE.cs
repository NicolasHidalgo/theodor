using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class GEN_DDL_BE
    {
        public string Value { get; set; } = "";
        public string Text { get; set; } = "";
        public string Aux1 { get; set; }
        public string Aux2 { get; set; }
        public string Aux3 { get; set; }
        public bool Selected { get; set; }     
    }
    public class REN_INFO_BE
    {
        public List<GEN_INFO_BE> lstOperacion { get; set; }
        public List<GEN_INFO_BE> lstCanal { get; set; }
        public List<GEN_INFO_BE> lstMoneda { get; set; }
        public List<GEN_INFO_BE> lstPersoneria { get; set; }
        public List<GEN_INFO_BE> lstTipDocumento { get; set; }
        public List<GEN_INFO_BE> lstTipCliente { get; set; }
        public List<GEN_INFO_BE> lstProducto { get; set; }
        public List<GEN_INFO_BE> lstClasificacionInterna { get; set; }
        public List<GEN_INFO_BE> lstClasificacionExterna { get; set; }
        public List<GEN_INFO_BE> lstGarantiaReal { get; set; }
        public List<GEN_INFO_BE> lstGarantiaPersonal { get; set; }
        public List<GEN_INFO_BE> lstClasificacionGarantia { get; set; }

    }
    public class GEN_INFO_BE
    {
        public string info { get; set; } = "";
        public long ide { get; set; }
        public string cod { get; set; } = "";
        public string nom { get; set; }
        public string nom2 { get; set; }
        public string cod_personeria { get; set; }
        public string cod_operacion { get; set; }
        public string cod_tip_cliente { get; set; }
        public bool selected { get; set; }
    }
}
