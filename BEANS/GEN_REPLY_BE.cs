using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class GEN_REPLY_BE
    {
        public string COD_MENU { get; set; }
        public string CONTROLLER { get; set; }
        public string HOSTNAME { get; set; }
        public string SESSION { get; set; }
        public string ACCION { get; set; }
        public string MENSAJE { get; set; }
        public string SEARCH { get; set; }
        public int? IDE_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string CORREO { get; set; }
        public string COD_APLICACION { get; set; }
        public int? ID_PROCESO { get; set; }
        public Object DATA { get; set; }
        public string Base64 { get; set; }
    }
}
