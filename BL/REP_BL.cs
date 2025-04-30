using BEANS;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class REP_BL
    {
        REP_DA dat = new REP_DA();
        public REP_INFO_BE fn_rep_sel_info(string accion, long cod_suscriptor)
        {
            var info = new REP_INFO_BE();
            var data = dat.fn_rep_sel_ddl(accion, cod_suscriptor);
            var empty = new REP_INFO_BE();
            //empty.cod = "";
            //empty.nom = "";
            //empty.cod_personeria = "@";
            //empty.cod_operacion = "@";
            //empty.selected = true;

            info.ddlMoneda = data.Where(x => x.Aux1.Equals("MONEDA")).ToList();
            info.ddlTipCliente = data.Where(x => x.Aux1.Equals("TIPOCLIENTE")).ToList();
            info.ddlOperacion = data.Where(x => x.Aux1.Equals("OPERACION")).ToList();
            info.ddlProducto = data.Where(x => x.Aux1.Equals("PRODUCTO")).ToList();
            info.ddlPersoneria = data.Where(x => x.Aux1.Equals("PERSONERIA")).ToList();
            info.ddlAgencia = data.Where(x => x.Aux1.Equals("AGENCIA")).ToList();
            info.ddlFuncionario = data.Where(x => x.Aux1.Equals("FUNCIONARIO")).ToList();
            info.ddlGarantia = data.Where(x => x.Aux1.Equals("GARANTIA")).ToList();
            info.ddlClasificacion = data.Where(x => x.Aux1.Equals("CLASIFICACION")).ToList();

            return info;
        }
    }
}

