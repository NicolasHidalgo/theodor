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
            viewModel.IdeClienteProducto = 0;

            var dataInfo = new REN_INFO_BE();
            if (Session["dataInfo"] == null)
            {
                dataInfo = bl.fn_ren_sel_info("INFO1", user.SUSCRIPTOR);
                Session["dataInfo"] = dataInfo;
            }
            else
            {
                dataInfo = (REN_INFO_BE)Session["dataInfo"];
            }

            viewModel.ddlMoneda = dataInfo.lstMoneda.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected,
            });
            viewModel.ddlCanalAtencion = dataInfo.lstCanal.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected,
            });
            viewModel.ddlPersoneria = dataInfo.lstPersoneria.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected,
            });
            viewModel.ddlOperacion = dataInfo.lstOperacion.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected,
            });

            return View(viewModel);
        }

        public JsonResult JSON_PersoneriaChange(string codPersoneria)
        {
            if (codPersoneria == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var dataInfo = new REN_INFO_BE();
            if (Session["dataInfo"] == null)
            {
                dataInfo = bl.fn_ren_sel_info("INFO1", user.SUSCRIPTOR);
                Session["dataInfo"] = dataInfo;
            }
            else
            {
                dataInfo = (REN_INFO_BE)Session["dataInfo"];
            }

            viewModel.ddlTipDocumento = dataInfo.lstTipDocumento
                                        .Where(x => x.cod_personeria.Equals("@") || x.cod_personeria.Equals(codPersoneria))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlTipCliente = dataInfo.lstTipCliente
                                        .Where(x => x.cod_personeria.Equals("@") || x.cod_personeria.Equals(codPersoneria))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_OperacionChange(long ide_cliente_producto, string codOperacion)
        {
            if (codOperacion == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var dataInfo = new REN_INFO_BE();
            if (Session["dataInfo"] == null)
            {
                dataInfo = bl.fn_ren_sel_info("INFO1", user.SUSCRIPTOR);
                Session["dataInfo"] = dataInfo;
            }
            else
            {
                dataInfo = (REN_INFO_BE)Session["dataInfo"];
            }

            viewModel.ddlProducto = dataInfo.lstProducto
                                    .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                    .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlClasificacionInterna = dataInfo.lstClasificacionInterna
                                                .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                                .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlClasificacionExterna = dataInfo.lstClasificacionExterna
                                                .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                                .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlGarantiaReal = dataInfo.lstGarantiaReal
                                        .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlGarantiaPersonal = dataInfo.lstGarantiaPersonal
                                            .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                            .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlClasificacionGarantia = dataInfo.lstClasificacionGarantia
                                                    .Where(x => x.cod_operacion.Equals("@") || x.cod_operacion.Equals(codOperacion))
                                                    .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_ProductoChange(long ide_cliente_producto, string codProducto)
        {
            if (codProducto == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var viewModel = new AuxiliarEdit();
            var dataInfo = new REN_INFO_BE();
            if (Session["dataInfo"] == null)
            {
                dataInfo = bl.fn_ren_sel_info("INFO1", user.SUSCRIPTOR);
                Session["dataInfo"] = dataInfo;
            }
            else
            {
                dataInfo = (REN_INFO_BE)Session["dataInfo"];
            }
            var comisiones = bl.fn_ren_pro_clienteComision_vista(user.SUSCRIPTOR, ide_cliente_producto);
            viewModel.dataComision = comisiones;

            viewModel.amortizacion = dataInfo.lstProducto.Where(x => x.cod.Equals(codProducto)).FirstOrDefault().nom2;

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
            var simData = bl.fn_ren_pro_get(model);
            viewModel.simData = simData;
            model.ide_cliente_producto = viewModel.simData.ide_cliente_producto;

            var comision = bl.fn_ren_pro_clienteComision_vista(model.cod_suscriptor, model.ide_cliente_producto);
            viewModel.dataComision = comision;

            var dataPYG = bl.fn_ren_pyg(model.ide_cliente_producto);
            viewModel.dataPYG = dataPYG;

            model.accion = "@Resumen_escenarios";
            var dataResEsc = bl.fn_ren_resumenEsc(model);
            viewModel.dataResEsc = dataResEsc;

            var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(model.ide_cliente_producto);
            viewModel.dataRoracRes = dataRoracRes;

            /*
            var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 100);
            viewModel.dataRoracTbl = dataRoracTbl;

            var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(model.ide_cliente_producto, 0.001, 1, 10);
            viewModel.dataRorac = dataRorac.FirstOrDefault();

            var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(model.ide_cliente_producto);
            viewModel.dataComposicion = dataComposicion;
            */

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

        public JsonResult JSON_ReporteComposicion(long ide_cliente_producto)
        {
            var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(ide_cliente_producto);
            return Json(dataComposicion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(GEN_REPLY_BE model)
        {
            if (!this.Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "SEG");
            }
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

                if (model.ACCION.Equals("@GRABAR")) // grabar = simular
                {
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
                }
                

                /*
                var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(_sim.ide_cliente_producto, _sim.incremento_tasa, _sim.incremento_plazo, 100);
                viewModel.dataRoracTbl = dataRoracTbl;

                var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(_sim.ide_cliente_producto, _sim.incremento_tasa, _sim.incremento_plazo, 10);
                viewModel.dataRorac = dataRorac.FirstOrDefault();

                var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(_sim.ide_cliente_producto);
                viewModel.dataComposicion = dataComposicion;
                */

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
            if (!this.Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "SEG");
            }
            if (ModelState.IsValid)
            {
                var viewModel = new AuxiliarEdit();
                var model = new REN_SIM_REQ_BE();
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;

                var reply = bl.fn_ren_pro_clienteProducto_nuevo(user.SUSCRIPTOR, ideUsuario);

                var _sim = (REN_SIM_REQ_BE)reply.DATA;
                var comision = bl.fn_ren_pro_clienteComision_vista(codSuscriptor, _sim.ide_cliente_producto);
                viewModel.simData = _sim;
                viewModel.dataComision = comision;

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

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
        public ActionResult EditComision(GEN_REPLY_BE model)
        {
            if (!this.Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "SEG");
            }
            if (ModelState.IsValid)
            {
                var viewModel = new AuxiliarEdit();
                var _obj = (string[])model.DATA;
                var _com = JsonConvert.DeserializeObject<REN_COMISION_BE>(_obj[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                _com.cod_suscriptor = user.SUSCRIPTOR;
                var reply = bl.fn_ren_pro_clienteComision_grabar(_com);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                var req = new REN_SIM_REQ_BE();

                var dataPYG = bl.fn_ren_pyg(_com.ide_cliente_producto);
                viewModel.dataPYG = dataPYG;

                req.accion = "@Resumen_escenarios";
                req.cod_suscriptor = user.SUSCRIPTOR;
                req.ide_cliente_producto = _com.ide_cliente_producto;
                var dataResEsc = bl.fn_ren_resumenEsc(req);
                viewModel.dataResEsc = dataResEsc;

                var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(_com.ide_cliente_producto);
                viewModel.dataRoracRes = dataRoracRes;

                /*
                var dataRoracTbl = bl.fn_ren_vis_clienteProducto_Tabla(_com.ide_cliente_producto, _com.incremento_tasa, _com.incremento_plazo, 100);
                viewModel.dataRoracTbl = dataRoracTbl;

                var dataRorac = bl.fn_ren_vis_clienteProducto_Tabla(_com.ide_cliente_producto, _com.incremento_tasa, _com.incremento_plazo, 10);
                viewModel.dataRorac = dataRorac.FirstOrDefault();

                var dataComposicion = bl.fn_ren_vis_clienteProducto_Composicion(_com.ide_cliente_producto);
                viewModel.dataComposicion = dataComposicion;
                */

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
    }
}
