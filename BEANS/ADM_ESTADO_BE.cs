using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class ADM_ESTADO_BE
    {
        public int codBandeja { get; set; }
        public string bandeja { get; set; }
        public string usuario { get; set; }
        public string etiqueta { get; set; }
        public string codigo { get; set; }
        public string documento { get; set; }
        public string personeria { get; set; }
        public string cliente { get; set; }
        public string producto { get; set; }
        public string monto { get; set; }
        public string plazo { get; set; }
        public string teaBase { get; set; }
        public string teaEfectiva { get; set; }
        public string profit { get; set;}
        public string rorac { get; set; }
        public string autonomia { get; set; }
        public string fecha { get; set; }
    }
    public class BANDEJA_BE
    {
        public int codBandeja { get; set; }
        public string bandeja { get; set; }
    }
}
