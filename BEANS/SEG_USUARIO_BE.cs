using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class SEG_USUARIO_BE
    {
        public long? IDE_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string NOM_USUARIO { get; set; }
        public string EST_USUARIO { get; set; }

        public string APELLIDOS { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string DIRECCION_USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public long SUSCRIPTOR { get; set; }
        public string IMG_SUSCRIPTOR { get; set; }

        public string NOM_MENU { get; set; }

        public DateTime? FEC_CREACION { get; set; }
        public DateTime? FEC_CESE { get; set; }

        public string COD_APLICACION { get; set; }
        public IEnumerable<SEG_APLICACION_BE> Aplicaciones { get; set; } = new HashSet<SEG_APLICACION_BE>();
        public IEnumerable<SEG_OPCION_BE> Opciones { get; set; } = new HashSet<SEG_OPCION_BE>();

        public IEnumerable<SEG_OPCION_BE> OpcionesData { get; set; } = new HashSet<SEG_OPCION_BE>();
    }

    public class SEG_USUARIO_PERFIL_BE 
    {
        public int? ID_USUARIO { get; set; }
        public int? ID_PERFIL { get; set; }
        public string NOM_PERFIL { get; set; }
        public string COD_APLICACION { get; set; }

        public IEnumerable<SEG_USUARIO_PERFIL_BE> Perfiles { get; set; } = new HashSet<SEG_USUARIO_PERFIL_BE>();
    }
    public class UsuarioFilter
    {
        public string cod_usuario { get; set; }
        public string nom_usuario { get; set; }
        public string correo_electronico { get; set; }
    }
}
