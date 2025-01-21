using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class AGENCIA_BE
    {
        public long cod_suscriptor { get; set; }
        public string cod_usuario { get; set; }
        public string cod_agencia { get; set; }
        public string nom_agencia { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }

    }
    public class BANCA_BE
    {
        public long cod_suscriptor { get; set; }
        public string cod_usuario { get; set; }
        public string cod_personeria { get; set; }
        public string personeria { get; set; }

    }
}
