using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class SEG_SESSION_BE
    {
        public string COD_USUARIO { get; set; }
        public string COD_APLICACION { get; set; }
        public Int64 COD_MENU { get; set; }
        public string COD_SESSION { get; set; }
        public int COD_PERFIL { get; set; }
        public int COD_ROL { get; set; }
        public int NIV_MENU { get; set; }
        public string URL_MENU { get; set; }
        public bool PERMISO { get; set; }


        public int codRol { get; set; }
        public string nomRol { get; set; }
        public int codPer { get; set; }
        public string nomPer { get; set; }
        public int iCodMenu { get; set; }
        public string vNomMenu { get; set; }
        public Int16 siNivMenu { get; set; }
        public string vUrlMenu { get; set; }
        public bool bPermiso { get; set; }
        public Int16 siAcPermiso { get; set; }
    }
}
