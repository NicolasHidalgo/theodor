using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEANS
{
    public class REN_SIM_REQ_BE
    {
        public REN_SIM_REQ_BE()
        {
            this.cod_suscriptor = 0;
            this.ide_cliente = 0;
        }
        public string accion { get; set; }
        public Int64 cod_suscriptor { get; set; }
        public Int64 ide_cliente { get; set; }
        public int cod_tip_documento { get; set; }
        public string num_documento { get; set; }
        public string nom_cliente { get; set; }
        public string cod_personeria { get; set; }
        public string cod_tip_cliente { get; set; }
        public int cod_tip_prospecto { get; set; }
        public Int64 ide_cliente_producto { get; set; }
        public int cod_producto { get; set; }
        public string cod_operacion { get; set; }
        public int cod_moneda { get; set; }
        public double monto { get; set; }
        public int cod_canal_atencion { get; set; }
        public double tea { get; set; }
        public double plazo { get; set; }
        public string cod_clasificacion_interna { get; set; }
        public string cod_clasificacion_externa { get; set; }
        public int cod_garantia_real { get; set; }
        public int cod_moneda_garantia_real { get; set; }
        public double monto_garantia_real { get; set; }
        public int cod_garantia_personal { get; set; }
        public int cod_moneda_garantia_personal { get; set; }
        public double monto_garantia_personal { get; set; }
        public string cod_clasificacion_garantia { get; set; }
        public int cod_modelo_rorac { get; set; }
        public Int64 ide_usuario { get; set; }
        public double incremento_tasa { get; set; }
        public double incremento_plazo { get; set; }
    }
}
