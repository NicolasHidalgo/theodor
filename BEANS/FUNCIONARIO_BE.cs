using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class FUNCIONARIO_BE
    {
        public long cod_suscriptor { get; set; }
        public string cod_funcionario { get; set; }
        public string nom_funcionario { get; set; }
        public string cod_usuario { get; set; }
        public string nom_usuario { get; set; }
        public string cod_agencia { get; set; }
        public string nom_agencia { get; set; }
        public string cod_personeria { get; set; }
        public string personeria { get; set; }
        public long ide_usuario_funcionario { get; set; }
        public long ide_usuario { get; set; }
    }
}
