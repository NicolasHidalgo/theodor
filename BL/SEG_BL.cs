using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SEG_BL
    {
        SEG_DA dat = new SEG_DA();
        public GEN_REPLY_BE fn_seg_login(GEN_REPLY_BE model)
        {
            return dat.fn_seg_login(model);
        }
        public GEN_REPLY_BE fn_seg_acceso(GEN_REPLY_BE model)
        {
            return dat.fn_seg_acceso(model);
        }
        public List<SEG_APLICACION_BE> fn_seg_sel_usu_app(GEN_REPLY_BE model)
        {
            return dat.fn_seg_sel_usu_app(model);
        }
        public GEN_REPLY_BE fn_seg_menuDinamico(GEN_REPLY_BE model, ref List<SEG_OPCION_BE> lista)
        {
            List<SEG_OPCION_BE> listaObj = new List<SEG_OPCION_BE>();
            lista = dat.fn_seg_sel_menu(model);

            SEG_OPCION_BE bean = null;
            foreach (var item in lista.Where(x => x.COD_MENU_PADRE == 0))
            {
                bean = new SEG_OPCION_BE();
                bean.COD_MENU = item.COD_MENU;
                bean.COD_MENU_PADRE = item.COD_MENU_PADRE;
                bean.NOM_MENU = item.NOM_MENU;
                bean.NAVEGACION_URL = item.NAVEGACION_URL;
                bean.TARGET_MENU = item.TARGET_MENU;
                bean.ACTION = item.ACTION;
                bean.CONTROLLER = item.CONTROLLER;
                bean.ICONO = item.ICONO;
                bean.TIP_MENU = item.TIP_MENU;
                bean.PARAMETRO = item.PARAMETRO;
                bean.OpcionHijo = this.fn_seg_subMenu(item.COD_MENU, lista);
                listaObj.Add(bean);
            }

            model.DATA = listaObj;
            return model;
        }

        public List<SEG_OPCION_BE> fn_seg_subMenu(Int64 idOpcion, List<SEG_OPCION_BE> lista)
        {
            List<SEG_OPCION_BE> listaObj = new List<SEG_OPCION_BE>();
            SEG_OPCION_BE bean = null;
            foreach (var item in lista.Where(x => x.COD_MENU_PADRE == idOpcion))
            {
                bean = new SEG_OPCION_BE();
                bean.COD_MENU = item.COD_MENU;
                bean.COD_MENU_PADRE = item.COD_MENU_PADRE;
                bean.NOM_MENU = item.NOM_MENU;
                bean.NAVEGACION_URL = item.NAVEGACION_URL;
                bean.TARGET_MENU = item.TARGET_MENU;
                bean.ACTION = item.ACTION;
                bean.CONTROLLER = item.CONTROLLER;
                bean.ICONO = item.ICONO;
                bean.TIP_MENU = item.TIP_MENU;
                bean.PARAMETRO = item.PARAMETRO;
                bean.OpcionHijo = this.fn_seg_subMenu(item.COD_MENU, lista);
                listaObj.Add(bean);
            }

            return listaObj;
        }

        public List<SEG_USUARIO_BE> fn_seg_sel_usuario(GEN_REPLY_BE model)
        {
            return dat.fn_seg_sel_usuario(model);
        }
        public GEN_REPLY_BE fn_seg_upd_usuario(GEN_REPLY_BE model)
        {
            return dat.fn_seg_upd_usuario(model);
        }
        public GEN_REPLY_BE fn_seg_sel_app_perfil(GEN_REPLY_BE model)
        {
            return dat.fn_seg_sel_app_perfil(model);
        }
        public GEN_REPLY_BE fn_seg_upd_permisos(GEN_REPLY_BE model)
        {
            return dat.fn_seg_upd_permisos(model);
        }
        public GEN_REPLY_BE fn_seg_sel_permisos(GEN_REPLY_BE model)
        {
            return dat.fn_seg_sel_permisos(model);
        }
        public GEN_REPLY_BE fn_seg_upd_session(GEN_REPLY_BE model)
        {
            return dat.fn_seg_upd_session(model);
        }
    }
}
