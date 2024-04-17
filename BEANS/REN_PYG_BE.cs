using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class REN_PYG_BE
    {
        public int ide { get; set; }
        public string detalle { get; set; }
        public double? operacion { get; set; }
        public double? anual { get; set; }
        public double? trimestral { get; set; }
        public double? mensual { get; set; }
        public double? ratio { get; set; }
        public string color { get; set; }
    }

    public class REN_RESUMEN_ESC_BE
    {
        public int ide { get; set; }
        public string detalle { get; set; }
        public double? operacion { get; set; }
        public double? objetivo { get; set; }
        public double? primaRiesgo { get; set; }
        public string formato { get; set; }
        public string color { get; set; }
    }
    public class REN_RORAC_COBERTURA_BE
    {
        public int ide { get; set; }
        public string estilo { get; set; }
        public string formato { get; set; }
        public string detalle { get; set; }
        public double? res01 { get; set; }
        public double? res02 { get; set; }
        public double? res03 { get; set; }
        public double? res04 { get; set; }
        public double? res05 { get; set; }
    }

    public class REN_RORAC_MODELO_BE
    {
        public double? plazo { get; set; }
        public double? valor1 { get; set; }
        public double? valor2 { get; set; }
        public double? valor3 { get; set; }
        public double? valor4 { get; set; }
        public double? valor5 { get; set; }
        public double? valor6 { get; set; }
        public double? valor7 { get; set; }
        public double? valor8 { get; set; }
        public double? valor9 { get; set; }

        public double? rorac_objetivo { get; set; }
        public double? autonomia_comercial { get; set; }

    }
    public class REN_COMPOSICION_BE
    {
        public int ide { get; set; }
        public int tipo { get; set; }
        public string detalle { get; set; }     
        public double? valor { get; set; }
        public double? invisible { get; set; }
        public double? negativo { get; set; }

    }


}
