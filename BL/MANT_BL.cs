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
        public GEN_REPLY_BE fn_mant_pro_agencia(string accion, long codSuscriptor, string codUsuario, AGENCIA_BE param)
        {
            return dat.fn_mant_pro_agencia(accion, codSuscriptor, codUsuario, param);
        }
        public List<FUNCIONARIO_BE> fn_mant_sel_funcionario(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_funcionario(accion, codSuscriptor, codUsuario);
        }
        public List<SEG_USUARIO_BE> fn_mant_sel_usuario(string accion, long codSuscriptor, string codUsuario)
        {
            return dat.fn_mant_sel_usuario(accion, codSuscriptor, codUsuario);
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

    }
}
