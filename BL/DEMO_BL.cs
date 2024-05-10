using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DEMO_BL
    {
        DEMO_DA da = new DEMO_DA();

        public List<DEMO_BE> fn_sel_mov(DEMO_BE param)
        {
            return da.fn_sel_mov(param); ;
        }
        public GEN_REPLY_BE fn_pro_mov(DEMO_BE param)
        {
            return da.fn_pro_mov(param); ;
        }
    }
}
