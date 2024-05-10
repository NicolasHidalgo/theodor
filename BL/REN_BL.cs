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
        public REN_INFO_BE fn_ren_sel_info(string accion, long cod_suscriptor)
        {
            var info = new REN_INFO_BE();
            var data = dat.fn_ren_sel_info(accion, cod_suscriptor);
            var empty = new GEN_INFO_BE();
            empty.cod = "";
            empty.nom = "";
            empty.cod_personeria = "@";
            empty.cod_operacion = "@";
            empty.selected = true;

            info.lstOperacion = data.Where(x => x.info.Equals("OPERACION")).ToList();
            info.lstOperacion.Add(empty);
            info.lstCanal = data.Where(x => x.info.Equals("CANAL")).ToList();
            info.lstCanal.Add(empty);
            info.lstMoneda = data.Where(x => x.info.Equals("MONEDA")).ToList();
            info.lstMoneda.Add(empty);
            info.lstPersoneria = data.Where(x => x.info.Equals("PERSONERIA")).ToList();
            info.lstPersoneria.Add(empty);
            info.lstTipDocumento = data.Where(x => x.info.Equals("TIP_DOCUMENTO")).ToList();
            info.lstTipDocumento.Add(empty);
            info.lstTipCliente = data.Where(x => x.info.Equals("TIP_CLIENTE")).ToList();
            info.lstTipCliente.Add(empty);
            info.lstProducto = data.Where(x => x.info.Equals("PRODUCTO")).ToList();
            info.lstProducto.Add(empty);
            info.lstClasificacionInterna = data.Where(x => x.info.Equals("CLASIFICACION_INTERNA")).ToList();
            info.lstClasificacionInterna.Add(empty);
            info.lstClasificacionExterna = data.Where(x => x.info.Equals("CLASIFICACION_EXTERNA")).ToList();
            info.lstClasificacionExterna.Add(empty);
            info.lstGarantiaReal = data.Where(x => x.info.Equals("GARANTIA_REAL")).ToList();
            info.lstGarantiaReal.Add(empty);
            info.lstGarantiaPersonal = data.Where(x => x.info.Equals("GARANTIA_PERSONAL")).ToList();
            info.lstGarantiaPersonal.Add(empty);
            info.lstClasificacionGarantia = data.Where(x => x.info.Equals("CLASIFICACION_GARANTIA")).ToList();
            info.lstClasificacionGarantia.Add(empty);

            return info;
        }
        public List<REN_PYG_BE> fn_ren_pyg(long idClienteProducto)
        {
            return dat.fn_ren_pyg(idClienteProducto);
        }
        public List<REN_PYG_BE> fn_ren_pyg(string accion, long idClienteProducto)
        {
            return dat.fn_ren_pyg(accion, idClienteProducto);
        }
        public List<REN_CRONOGRAMA_BE> fn_ren_calendario(string accion, long ideClienteProducto)
        {
            return dat.fn_ren_cronograma(accion, ideClienteProducto);
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
        public List<REN_COMISION_BE> fn_ren_pro_clienteComision_vista(long codSuscriptor, long ideClienteProducto)
        {
            return dat.fn_ren_pro_clienteComision_vista(codSuscriptor, ideClienteProducto);
        }
        public GEN_REPLY_BE fn_ren_pro_clienteComision_grabar(REN_COMISION_BE param)
        {
            return dat.fn_ren_pro_clienteComision_grabar(param);
        }
        public List<REN_POPUP_BE> fn_ren_pro_listarPopup(REN_SIM_REQ_BE param)
        {
            return dat.fn_ren_pro_listarPopup(param);
        }
        public REN_SIM_REQ_BE fn_ren_pro_get(REN_SIM_REQ_BE param)
        {
            return dat.fn_ren_pro_get(param);
        }
        public GEN_REPLY_BE fn_ren_pro_clienteProducto_nuevo(long codSuscriptor, long ideUsuario)
        {
            return dat.fn_ren_pro_clienteProducto_nuevo(codSuscriptor, ideUsuario);
        }
    }
}
