using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class REN_BL
    {
        REN_DA dat = new REN_DA();
        public List<GEN_DDL_BE> fn_ren_sel_ddl(REN_SIM_REQ_BE model)
        {
            return dat.fn_ren_sel_ddl(model);
        }
        public List<REN_PYG_BE> fn_ren_pyg(long idClienteProducto)
        {
            return dat.fn_ren_pyg(idClienteProducto);
        }
        public List<REN_RESUMEN_ESC_BE> fn_ren_resumenEsc(REN_SIM_REQ_BE model)
        {
            return dat.fn_ren_resumenEsc(model);
        }
        public List<REN_RORAC_COBERTURA_BE> fn_ren_vis_clienteProducto_Resumen(long idClienteProducto)
        {
            return dat.fn_ren_vis_clienteProducto_Resumen(idClienteProducto);
        }
        public List<REN_RORAC_MODELO_BE> fn_ren_vis_clienteProducto_Tabla(long idClienteProducto, double incremento_tasa, double incremento_plazo, int accion)
        {
            return dat.fn_ren_vis_clienteProducto_Tabla(idClienteProducto, incremento_tasa, incremento_plazo, accion);
        }
        public List<REN_COMPOSICION_BE> fn_ren_vis_clienteProducto_Composicion(long idClienteProducto)
        {
            return dat.fn_ren_vis_clienteProducto_Composicion(idClienteProducto);
        }
        public GEN_REPLY_BE fn_ren_pro_clienteProducto(GEN_REPLY_BE model)
        {
            return dat.fn_ren_pro_clienteProducto(model);
        }
        public List<REN_COMISION_BE> fn_ren_pro_clienteComision(REN_COMISION_BE param)
        {
            return dat.fn_ren_pro_clienteComision(param);
        }
        public List<REN_POPUP_BE> fn_ren_pro_listarPopup(REN_SIM_REQ_BE param)
        {
            return dat.fn_ren_pro_listarPopup(param);
        }
        public REN_SIM_REQ_BE fn_ren_pro_get(REN_SIM_REQ_BE param)
        {
            return dat.fn_ren_pro_get(param);
        }
    }
}
