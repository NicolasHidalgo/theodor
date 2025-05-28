using BEANS;
using BL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webapp.Models.DTO;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Authorize]
    public class REPController : BaseController
    {
        private REP_BL bl = new REP_BL();
        public ActionResult Dashboard()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var codSuscriptor = user.SUSCRIPTOR;
            //string ip = Request.UserHostAddress;
            viewModel.CodSuscriptor = user.SUSCRIPTOR;

            var dataInfo = new REP_INFO_BE();
            if (Session["dataInfoRep"] == null)
            {
                dataInfo = bl.fn_rep_sel_info("FULL_DDL", user.SUSCRIPTOR);
                //Session["dataInfo"] = dataInfo;
            }
            else
            {
                dataInfo = (REP_INFO_BE)Session["dataInfoRep"];
            }

            viewModel.ddlMoneda = dataInfo.ddlMoneda.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlTipCliente = dataInfo.ddlTipCliente.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlPersoneria = dataInfo.ddlPersoneria.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlOperacion = dataInfo.ddlOperacion.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlProducto = dataInfo.ddlProducto.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlPersoneria = dataInfo.ddlPersoneria.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlAgencia = dataInfo.ddlAgencia.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlFuncionario = dataInfo.ddlFuncionario.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlGarantiaPersonal = dataInfo.ddlGarantia.Select(x => new ExtendedSelectListItem(x));
            viewModel.ddlClasificacionInterna = dataInfo.ddlClasificacion.Select(x => new ExtendedSelectListItem(x));

            REP_DASHBOARD_PARAM param = new REP_DASHBOARD_PARAM();
            param.accion = "DASHBOARD";
            param.codSuscriptor = codSuscriptor;

            var dashboard1 = bl.fn_rep_sel_dashboard1(param);
            return View(viewModel);
        }

        public JsonResult JSON_GetDashboard01(REP_DASHBOARD_PARAM param)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            param.accion = "DASHBOARD";
            param.codSuscriptor = user.SUSCRIPTOR;
            var dash = bl.fn_rep_sel_dashboard1(param);
            return Json(dash, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _Dashboard(DASHBOARD_DTO dto)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true,
                ShowParameterPrompts = false,
                ShowPromptAreaButton = false,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                AsyncRendering = false,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            };

            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var codSuscriptor = user.SUSCRIPTOR;

            if (dto.fechaDesde != null && dto.fechaDesde != string.Empty)
            {
                dto.fechaDesde = dto.fechaDesde + "-01";
            }
            if (dto.fechaHasta != null && dto.fechaHasta != string.Empty)
            {
                dto.fechaHasta = dto.fechaHasta + "-01";
            }

            List<ReportParameter> reportParameters = new List<ReportParameter>();
            reportParameters.Add(new ReportParameter("accion", "DASHBOARD", false));
            reportParameters.Add(new ReportParameter("cod_suscriptor", codSuscriptor.ToString(), false));
            reportParameters.Add(new ReportParameter("fecha_desde", dto.fechaDesde, false));
            reportParameters.Add(new ReportParameter("fecha_hasta", dto.fechaHasta, false));
            reportParameters.Add(new ReportParameter("cod_moneda", dto.codMoneda, false));
            reportParameters.Add(new ReportParameter("cod_personeria", dto.codPersoneria, false));
            reportParameters.Add(new ReportParameter("cod_tip_cliente", dto.codTipCliente, false));
            reportParameters.Add(new ReportParameter("cod_operacion", dto.codOperacion, false));
            reportParameters.Add(new ReportParameter("cod_producto", dto.codProducto, false));
            reportParameters.Add(new ReportParameter("garantia", dto.garantia, false));
            reportParameters.Add(new ReportParameter("cod_clasificacion_interna", dto.codClasificacionInterna, false));
            reportParameters.Add(new ReportParameter("cod_agencia", dto.codAgencia, false));
            reportParameters.Add(new ReportParameter("cod_funcionario", dto.codFuncionario, false));


            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["Report_Path"] + "/rp_ren_dashboard0";

            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Report_Server"]);
            reportViewer.ServerReport.SetParameters(reportParameters);

            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;

            return PartialView("_Dashboard");
        }

    }
}