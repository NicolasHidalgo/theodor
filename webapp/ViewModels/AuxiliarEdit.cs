using BEANS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace webapp.ViewModels
{
    public class AuxiliarEdit
    {
        public IEnumerable<ExtendedSelectListItem> ddlUsuario { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlAgencia { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlBanca { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlPersoneria { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlMoneda { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipDocumento { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlTipCliente { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlCanalAtencion { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlOperacion { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlProducto { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlClasificacionInterna { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlClasificacionExterna { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlGarantiaReal { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlGarantiaPersonal { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlClasificacionGarantia { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlComisionServicio { get; set; } = new HashSet<ExtendedSelectListItem>();
        public IEnumerable<ExtendedSelectListItem> ddlPeriocidad { get; set; } = new HashSet<ExtendedSelectListItem>();

        public List<REN_PYG_BE> dataPYG { get; set; }
        public List<REN_RESUMEN_ESC_BE> dataResEsc { get; set; }
        public List<REN_RORAC_COBERTURA_BE> dataRoracRes { get; set; }
        public List<REN_RORAC_MODELO_BE> dataRoracTbl { get; set; }
        public REN_RORAC_MODELO_BE dataRorac { get; set; }
        public List<REN_COMPOSICION_BE> dataComposicion { get; set; }
        public List<REN_COMISION_BE> dataComision { get; set; }
        public List<RORACOBJETIVO_BE> mantRorac { get; set; }
        public string amortizacion { get; set; }
        public long IdeClienteProducto { get; set; }
        public long CodSuscriptor { get; set; }
        public string CodUsuario { get; set; }
        public REN_SIM_REQ_BE simData { get; set; }

    }
}