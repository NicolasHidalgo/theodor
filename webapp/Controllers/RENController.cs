using BEANS;
using BL;
using DocumentFormat.OpenXml.EMMA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Authorize]
    public class RENController : BaseController
    {
        private REN_BL bl = new REN_BL();
        public ActionResult Simulador()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];

            //string ip = Request.UserHostAddress;

            var model = new REN_SIM_REQ_BE();
            model.cod_suscriptor = user.SUSCRIPTOR;
            model.ide_cliente_producto = 0;
            viewModel.IdeClienteProducto = model.ide_cliente_producto;

            // combos independientes
            model.accion = "moneda";
            var dataMoneda = bl.fn_ren_sel_ddl(model);
            viewModel.ddlMoneda = dataMoneda.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });
            model.accion = "canal_atencion";
            var dataCanalAtencion = bl.fn_ren_sel_ddl(model);
            viewModel.ddlCanalAtencion = dataCanalAtencion.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });
            model.accion = "periodicidad";
            var dataPeriocidad = bl.fn_ren_sel_ddl(model);
            viewModel.ddlPeriocidad = dataPeriocidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });
            model.accion = "amortizacion";
            var dataAmortizacion = bl.fn_ren_sel_ddl(model);
            var amortizacion = new GEN_DDL_BE();
            if (dataAmortizacion != null && dataAmortizacion.Count > 0)
            {
                amortizacion.Value = dataAmortizacion.FirstOrDefault().Value;
                amortizacion.Text = dataAmortizacion.FirstOrDefault().Text;
            }
            viewModel.amortizacion = amortizacion;
            // fin combos independientes

            model.accion = "personeria";
            var dataPersoneria = bl.fn_ren_sel_ddl(model);
            var personeriaSelected = dataPersoneria.FirstOrDefault();
            viewModel.ddlPersoneria = dataPersoneria.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Value == personeriaSelected.Value,
            });

            // ddl dependientes de personeria
            /*
            model.accion = "tip_documento";
            model.cod_personeria = personeriaSelected.Value;
            var dataTipDoc = bl.fn_ren_sel_ddl(model);
            viewModel.ddlTipDocumento = dataTipDoc.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false
            });

            model.accion = "tip_cliente";
            var dataTipCliente = bl.fn_ren_sel_ddl(model);
            viewModel.ddlTipCliente = dataTipCliente.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false
            });
            // fin ddl dependientes de personeria
            */


            model.accion = "operacion";
            var dataOperacion = bl.fn_ren_sel_ddl(model);
            var operacionSelected = dataOperacion.FirstOrDefault();
            viewModel.ddlOperacion = dataOperacion.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Value == operacionSelected.Value,
            });

            // ddl dependientes de operacion
            /*
            model.accion = "producto";
            model.cod_operacion = operacionSelected.Value;
            var dataProducto = bl.fn_ren_sel_ddl(model);
            var productoSelected = dataProducto.FirstOrDefault();
            viewModel.ddlProducto = dataProducto.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Value == productoSelected.Value,
            });

            model.accion = "clasificacion_interna";
            var dataClasificacionInt = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionInterna = dataClasificacionInt.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "clasificacion_externa";
            var dataClasificacionExt = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionExterna = dataClasificacionExt.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });


            model.accion = "comision_servicio";
            model.cod_producto = int.Parse(productoSelected.Value);
            var dataComisionServicio = bl.fn_ren_sel_ddl(model);
            viewModel.ddlComisionServicio = dataComisionServicio.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "garantia_real";
            var dataGarantiaReal = bl.fn_ren_sel_ddl(model);
            viewModel.ddlGarantiaReal = dataGarantiaReal.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "garantia_personal";
            var dataGarantiaPersonal = bl.fn_ren_sel_ddl(model);
            viewModel.ddlGarantiaPersonal = dataGarantiaPersonal.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "clasificacion_garantia";
            var dataClasificacionGarantia = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionGarantia = dataClasificacionGarantia.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });
            // fin ddl dependientes de operacion
            */

            /*
            var dataPYG = bl.fn_ren_pyg(model.ide_cliente_producto);
            viewModel.dataPYG = dataPYG;

            model.accion = "@Resumen_escenarios";
            var dataResEsc = bl.fn_ren_resumenEsc(model);
            viewModel.dataResEsc = dataResEsc;

            var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(model.ide_cliente_producto);
            viewModel.dataRoracRes = dataRoracRes;

            var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 100);
            viewModel.dataRoracTbl = dataRoracTbl;

            var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 10);
            viewModel.dataRorac = dataRorac.FirstOrDefault();

            var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(model.ide_cliente_producto);
            viewModel.dataComposicion = dataComposicion;
            */

            return View(viewModel);
        }

        public JsonResult JSON_PersoneriaChange(string codPersoneria)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var model = new REN_SIM_REQ_BE();

            model.cod_suscriptor = user.SUSCRIPTOR;
            model.ide_cliente_producto = 0;
            model.accion = "tip_documento";
            model.cod_personeria = codPersoneria;
            var dataTipDoc = bl.fn_ren_sel_ddl(model);
            viewModel.ddlTipDocumento = dataTipDoc.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false
            });

            model.accion = "tip_cliente";
            var dataTipCliente = bl.fn_ren_sel_ddl(model);
            viewModel.ddlTipCliente = dataTipCliente.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_OperacionChange(long ide_cliente_producto, string codOperacion)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var model = new REN_SIM_REQ_BE();
            model.cod_suscriptor = user.SUSCRIPTOR;

            model.accion = "producto";
            model.cod_operacion = codOperacion;
            var dataProducto = bl.fn_ren_sel_ddl(model);
            var productoSelected = dataProducto.FirstOrDefault();
            viewModel.ddlProducto = dataProducto.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Value == productoSelected.Value,
            });

            model.accion = "amortizacion";
            model.ide_cliente_producto = ide_cliente_producto;
            var dataAmortizacion = bl.fn_ren_sel_ddl(model);
            var amortizacion = new GEN_DDL_BE();
            if (dataAmortizacion != null && dataAmortizacion.Count > 0)
            {
                amortizacion.Value = dataAmortizacion.FirstOrDefault().Value;
                amortizacion.Text = dataAmortizacion.FirstOrDefault().Text;
            }
            viewModel.amortizacion = amortizacion;
            model.ide_cliente_producto = 0;

            model.accion = "clasificacion_interna";
            var dataClasificacionInt = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionInterna = dataClasificacionInt.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });
            model.accion = "clasificacion_externa";
            var dataClasificacionExt = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionExterna = dataClasificacionExt.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "garantia_real";
            var dataGarantiaReal = bl.fn_ren_sel_ddl(model);
            viewModel.ddlGarantiaReal = dataGarantiaReal.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "garantia_personal";
            var dataGarantiaPersonal = bl.fn_ren_sel_ddl(model);
            viewModel.ddlGarantiaPersonal = dataGarantiaPersonal.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            model.accion = "clasificacion_garantia";
            var dataClasificacionGarantia = bl.fn_ren_sel_ddl(model);
            viewModel.ddlClasificacionGarantia = dataClasificacionGarantia.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_ComisionServicioChange(string codProducto)
        {
            if (codProducto == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var model = new REN_SIM_REQ_BE();
            model.cod_suscriptor = user.SUSCRIPTOR;
            model.accion = "comision_servicio";
            model.cod_producto = int.Parse(codProducto);
            var dataComisionServicio = bl.fn_ren_sel_ddl(model);
            viewModel.ddlComisionServicio = dataComisionServicio.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = false,
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_ReportePYG(long idClienteProducto)
        {
            var viewModel = new AuxiliarEdit();
            var dataPYG = bl.fn_ren_pyg(idClienteProducto);
            viewModel.dataPYG = dataPYG;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_PopUp()
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var param = new REN_SIM_REQ_BE();
            param.accion = "@POPUP";
            param.cod_suscriptor = user.SUSCRIPTOR;
            var data = bl.fn_ren_pro_listarPopup(param);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_Get(long IdeClienteProducto)
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var model = new REN_SIM_REQ_BE();
            model.accion = "@EDITAR";
            model.cod_suscriptor = user.SUSCRIPTOR;
            model.ide_cliente_producto = IdeClienteProducto;
            viewModel.simData = bl.fn_ren_pro_get(model);
            model.ide_cliente_producto = viewModel.simData.ide_cliente_producto;

            model.accion = "amortizacion";
            var dataAmortizacion = bl.fn_ren_sel_ddl(model);
            var amortizacion = new GEN_DDL_BE();
            if (dataAmortizacion != null && dataAmortizacion.Count > 0)
            {
                amortizacion.Value = dataAmortizacion.FirstOrDefault().Value;
                amortizacion.Text = dataAmortizacion.FirstOrDefault().Text;
            }
            viewModel.amortizacion = amortizacion;

            var dataPYG = bl.fn_ren_pyg(model.ide_cliente_producto);
            viewModel.dataPYG = dataPYG;

            model.accion = "@Resumen_escenarios";
            var dataResEsc = bl.fn_ren_resumenEsc(model);
            viewModel.dataResEsc = dataResEsc;

            var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(model.ide_cliente_producto);
            viewModel.dataRoracRes = dataRoracRes;

            var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 100);
            viewModel.dataRoracTbl = dataRoracTbl;

            var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 10);
            viewModel.dataRorac = dataRorac.FirstOrDefault();

            var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(model.ide_cliente_producto);
            viewModel.dataComposicion = dataComposicion;

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_ReporteRORACModelo(long ide_cliente_producto, double incremento_tasa, double incremento_plazo)
        {
            var viewModel = new AuxiliarEdit();
            var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(ide_cliente_producto, incremento_tasa, incremento_plazo, 100);
            viewModel.dataRoracTbl = dataRoracTbl;

            var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(ide_cliente_producto, incremento_tasa, incremento_plazo, 10);
            viewModel.dataRorac = dataRorac.FirstOrDefault();

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _sim = JsonConvert.DeserializeObject<REN_SIM_REQ_BE>(_obj[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                _sim.ide_usuario = long.Parse(user.IDE_USUARIO.ToString());
                _sim.cod_suscriptor = user.SUSCRIPTOR;
                model.DATA = _sim;

                var reply = bl.fn_ren_pro_clienteProducto(model);
                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;


                var viewModel = new AuxiliarEdit();
                var req = new REN_SIM_REQ_BE();
                var dataPYG = bl.fn_ren_pyg(_sim.ide_cliente_producto);
                viewModel.dataPYG = dataPYG;

                req.accion = "@Resumen_escenarios";
                req.cod_suscriptor = user.SUSCRIPTOR;
                req.ide_cliente_producto = _sim.ide_cliente_producto;
                var dataResEsc = bl.fn_ren_resumenEsc(req);
                viewModel.dataResEsc = dataResEsc;

                var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(_sim.ide_cliente_producto);
                viewModel.dataRoracRes = dataRoracRes;

                var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(_sim.ide_cliente_producto, _sim.incremento_tasa, _sim.incremento_plazo, 100);
                viewModel.dataRoracTbl = dataRoracTbl;

                var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(_sim.ide_cliente_producto, _sim.incremento_tasa, _sim.incremento_plazo, 10);
                viewModel.dataRorac = dataRorac.FirstOrDefault();

                var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(_sim.ide_cliente_producto);
                viewModel.dataComposicion = dataComposicion;

                req.accion = "amortizacion";
                var dataAmortizacion = bl.fn_ren_sel_ddl(req);
                var amortizacion = new GEN_DDL_BE();
                if (dataAmortizacion != null && dataAmortizacion.Count > 0)
                {
                    amortizacion.Value = dataAmortizacion.FirstOrDefault().Value;
                    amortizacion.Text = dataAmortizacion.FirstOrDefault().Text;
                }
                viewModel.amortizacion = amortizacion;

                res.Data = viewModel;

                return Json(res);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage),
                     }); ;
        }

        [HttpPost]
        public ActionResult New()
        {
            if (ModelState.IsValid)
            {
                var model = new REN_SIM_REQ_BE();
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;

                var reply = bl.fn_ren_pro_clienteProducto_nuevo(ideUsuario, user.SUSCRIPTOR);
                var res = new Response();
                res.Message = reply.MENSAJE;
                res.Data = reply.DATA;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                return Json(res);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage),
                     }); ;
        }
    }
}
