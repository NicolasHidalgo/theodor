using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MANT_BL
    {
        MANT_DA dat = new MANT_DA();
        public List<AGENCIA_BE> fn_mant_sel_agencia(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_agencia(accion, codSuscriptor, codUsuario);
        }
        public List<UBIGEO_BE> fn_mant_sel_ubigeo(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_ubigeo(accion, codSuscriptor, codUsuario)
;        }
        public GEN_REPLY_BE fn_mant_pro_agencia(string accion, long codSuscriptor, string codUsuario, AGENCIA_BE param)
        {
            return dat.fn_mant_pro_agencia(accion, codSuscriptor, codUsuario, param);
        }
        public List<FUNCIONARIO_BE> fn_mant_sel_funcionario(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_funcionario(accion, codSuscriptor, codUsuario);
        }
        public List<SEG_USUARIO_BE> fn_mant_sel_funcionarioUsuario(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_funcionarioUsuario(accion, codSuscriptor, codUsuario);
        }
        public List<AGENCIA_BE> fn_mant_sel_funAgencia(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_funAgencia(accion, codSuscriptor, codUsuario);
        }
        public List<BANCA_BE> fn_mant_sel_banca(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_banca(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_agencia(string accion, long codSuscriptor, string codUsuario, FUNCIONARIO_BE param)
        {
            return dat.fn_mant_pro_funcionario(accion, codSuscriptor, codUsuario, param);
        }
        public GEN_REPLY_BE fn_mant_pro_funcionario(string accion, long codSuscriptor, string codUsuario, FUNCIONARIO_BE param)
        {
            return dat.fn_mant_pro_funcionario(accion, codSuscriptor, codUsuario, param);
        }
        public List<RORACOBJETIVO_BE> fn_mant_sel_roracObjetivo(string accion, long codSuscriptor, string cod_usuario)
        {
            return dat.fn_mant_sel_roracObjetivo(accion, codSuscriptor, cod_usuario);
        }
        public List<GEN_DDL_BE> fn_mant_sel_roracDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_roracDDL(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_roracObjetivo(string accion, long codSuscriptor, string codUsuario, RORACOBJETIVO_BE param)
        {
            return dat.fn_mant_pro_roracObjetivo(accion, codSuscriptor, codUsuario, param);
        }
        public List<PRODUCTO_BE> fn_mant_sel_producto(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_producto(accion, codSuscriptor, codUsuario);
        }
        public List<GEN_DDL_BE> fn_mant_sel_productoDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_productoDDL(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_producto(string accion, long codSuscriptor, string codUsuario, PRODUCTO_BE param)
        {
            return dat.fn_mant_pro_producto(accion, codSuscriptor, codUsuario, param);
        }
        public List<COSTOOPERATIVO_BE> fn_mant_sel_costoOperativo(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_costoOperativo(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_costoOperativo(string accion, long codSuscriptor, string codUsuario, COSTOOPERATIVO_BE param)
        {
            return dat.fn_mant_pro_costoOperativo(accion, codSuscriptor, codUsuario, param);
        }
        public List<GEN_DDL_BE> fn_mant_sel_costoOpeDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_costoOpeDDL(accion, codSuscriptor, codUsuario);
        }
        public List<PROBABILIDAD_DEFAULT_BE> fn_mant_sel_probabilidadDefault(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_probabilidadDefault(accion, codSuscriptor, codUsuario);
        }
        public List<GEN_DDL_BE> fn_mant_sel_probabilidadDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_probabilidadDDL(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_probabilidadDefault(string accion, long codSuscriptor, string codUsuario, PROBABILIDAD_DEFAULT_BE param)
        {
            return dat.fn_mant_pro_probabilidadDefault(accion, codSuscriptor, codUsuario, param);
        }
        public List<CLASIFICACION_INTERNA_BE> fn_mant_sel_clasificacionInterna(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_clasificacionInterna(accion, codSuscriptor, codUsuario);
        }
        public List<GEN_DDL_BE> fn_mant_sel_clasificacionInternaDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_clasificacionInternaDDL(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_clasificacionInterna(string accion, long codSuscriptor, string codUsuario, CLASIFICACION_INTERNA_BE param)
        {
            return dat.fn_mant_pro_clasificacionInterna(accion, codSuscriptor, codUsuario, param);
        }
        public List<COMISIONSERVICIO_BE> fn_mant_sel_comisionServicio(string accion, long codSuscriptor, string codUsuario, int codProducto)
        {
            return dat.fn_mant_sel_comisionServicio(accion, codSuscriptor, codUsuario, codProducto);
        }
        public List<PRODUCTO_BE> fn_mant_sel_comisionProducto(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_comisionProducto(accion, codSuscriptor, codUsuario);
        }
        public List<GEN_DDL_BE> fn_mant_sel_comisionDDL(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_comisionDDL(accion, codSuscriptor, codUsuario);
        }
        public GEN_REPLY_BE fn_mant_pro_comisionServicio(string accion, long codSuscriptor, string codUsuario, COMISIONSERVICIO_BE param)
        {
            return dat.fn_mant_pro_comisionServicio(accion, codSuscriptor, codUsuario, param);
        }

    }
}
