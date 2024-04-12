using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class XLS_BL
    {
        XLS_DA dat = new XLS_DA();

        public GEN_REPLY_BE fn_xls_sel_proceso(GEN_REPLY_BE model)
        {
            return dat.fn_xls_sel_proceso(model);
        }
        public GEN_REPLY_BE fn_xls_upd_permisos(GEN_REPLY_BE model)
        {
            return dat.fn_xls_upd_permisos(model);
        }
        public GEN_REPLY_BE fn_xls_sel_permisos(GEN_REPLY_BE model)
        {
            return dat.fn_xls_sel_permisos(model);
        }
        public GEN_REPLY_BE fn_xls_sel_estructura(GEN_REPLY_BE model)
        {
            return dat.fn_xls_sel_estructura(model);
        }
        public GEN_REPLY_BE fn_xls_sel_parametro(GEN_REPLY_BE model)
        {
            return dat.fn_xls_sel_parametro(model);
        }
        public GEN_REPLY_BE fn_xls_upd_carga(GEN_REPLY_BE model, XLS_PROCESO_BE proceso)
        {
            model = dat.fn_xls_upd_carga(model);

            //if (model.MENSAJE.Contains("SUCCESS"))
            //{
            //    proceso.IDE_CARGA = ((XLS_CARGA_BE)model.DATA).IDE_CARGA;

            //    model = dat.fn_xls_upd_proceso(proceso);
            //}
            return model;
        }
        public GEN_REPLY_BE fn_xls_sel_carga(GEN_REPLY_BE model)
        {
            return dat.fn_xls_sel_carga(model);
        }
        public DataTable fn_xls_pro_vistaCarga(GEN_REPLY_BE model)
        {
            return dat.fn_xls_pro_vistaCarga(model);
        }
        public GEN_REPLY_BE fn_xls_get_menu(string parametro)
        {
            return dat.fn_xls_get_menu(parametro);
        }
        public GEN_REPLY_BE fn_xls_get_dashboard(int CodReporte)
        {
            return dat.fn_xls_get_dashboard(CodReporte);
        }

    }
}
