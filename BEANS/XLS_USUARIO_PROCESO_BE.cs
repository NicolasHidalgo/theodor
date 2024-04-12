using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class XLS_USUARIO_PROCESO_BE
    {
        public int ID_USUARIO { get; set; }
        public int ID_PROCESO { get; set; }

        public IEnumerable<XLS_PROCESO_BE> Procesos { get; set; } = new HashSet<XLS_PROCESO_BE>();
    }
}
