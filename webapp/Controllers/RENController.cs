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

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearRegression;
using DocumentFormat.OpenXml.Vml;
using Microsoft.Ajax.Utilities;

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
            viewModel.CodSuscriptor = user.SUSCRIPTOR;

            var dataInfo = new REN_INFO_BE();
            if (Session["dataInfo"] == null)
            {
                dataInfo = bl.fn_ren_sel_info("INFO1", user.SUSCRIPTOR);
                //Session["dataInfo"] = dataInfo;
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

        public ActionResult Historico()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];

            //string ip = Request.UserHostAddress;
            viewModel.IdeClienteProducto = 0;
            viewModel.CodSuscriptor = user.SUSCRIPTOR;

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
        public ActionResult Rentabilidad1()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];

            //string ip = Request.UserHostAddress;
            viewModel.IdeClienteProducto = 0;
            viewModel.CodSuscriptor = user.SUSCRIPTOR;

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

        public JsonResult JSON_PersoneriaChange(string codPersoneria, string codOperacion)
        {
            var viewModel = new AuxiliarEdit();
            if (codPersoneria == null || codPersoneria == string.Empty)
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            
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
                                        .Where(x => x.cod_personeria.Equals(codPersoneria))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlTipCliente = dataInfo.lstTipCliente
                                        .Where(x => x.cod_personeria.Equals(codPersoneria))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });

            //cargar producto
            if (codOperacion != null && codOperacion != string.Empty)
            {
                var defTipCliente = viewModel.ddlTipCliente.FirstOrDefault();
                if (defTipCliente.Value != null && defTipCliente.Value != "")
                {
                    viewModel.ddlProducto = dataInfo.lstProducto
                                            .Where(x => x.cod_operacion.Equals(codOperacion) && x.cod_tip_cliente.Equals(defTipCliente.Value))
                                            .Select(x => new ExtendedSelectListItem
                                            {
                                                Value = x.cod,
                                                Text = x.nom,
                                                Aux1 = x.plazo.ToString(),
                                                Aux2 = x.tea.ToString(),
                                                Selected = x.selected
                                            });
                }
            }
            

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_TipoClienteChange(string codOperacion, string codTipCliente)
        {
            var viewModel = new AuxiliarEdit();
            if (codTipCliente == null || codTipCliente == string.Empty || 
                codOperacion == null || codOperacion == string.Empty)
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            
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
                                        .Where(x => x.cod_operacion.Equals(codOperacion) && x.cod_tip_cliente.Equals(codTipCliente))
                                        .Select(x => new ExtendedSelectListItem
                                        {
                                            Value = x.cod,
                                            Text = x.nom,
                                            Aux1 = x.plazo.ToString(),
                                            Aux2 = x.tea.ToString(),
                                            Selected = x.selected
                                        });
            viewModel.ddlClasificacionExterna = dataInfo.lstClasificacionExterna
                                                .Where(x => x.cod_operacion.Equals(codOperacion) && x.cod_tip_cliente.Equals(codTipCliente))
                                                .Select(x => new ExtendedSelectListItem
                                                {
                                                    Value = x.cod,
                                                    Text = x.nom,
                                                    Selected = x.selected
                                                });


            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_OperacionChange(long ide_cliente_producto, string codOperacion, string codTipCliente)
        {
            var viewModel = new AuxiliarEdit();
            if (codOperacion == null || codOperacion == string.Empty)
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            
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

            if (codTipCliente != null && codTipCliente != string.Empty)
            {
                viewModel.ddlProducto = dataInfo.lstProducto
                                    .Where(x => (x.cod_operacion.Equals(codOperacion) && x.cod_tip_cliente.Equals(codTipCliente)))
                                    .Select(x => new ExtendedSelectListItem
                                    {
                                        Value = x.cod,
                                        Text = x.nom,
                                        Aux1 = x.plazo.ToString(),
                                        Aux2 = x.tea.ToString(),
                                        Selected = x.selected
                                    });
                viewModel.ddlClasificacionExterna = dataInfo.lstClasificacionExterna
                                                .Where(x => x.cod_operacion.Equals(codOperacion) && x.cod_tip_cliente.Equals(codTipCliente))
                                                .Select(x => new ExtendedSelectListItem
                                                {
                                                    Value = x.cod,
                                                    Text = x.nom,
                                                    Selected = x.selected
                                                });
            }
            
            viewModel.ddlClasificacionInterna = dataInfo.lstClasificacionInterna
                                                .Where(x => x.cod_operacion.Equals(codOperacion))
                                                .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlGarantiaReal = dataInfo.lstGarantiaReal
                                        .Where(x => x.cod_operacion.Equals(codOperacion))
                                        .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlGarantiaPersonal = dataInfo.lstGarantiaPersonal
                                            .Where(x => x.cod_operacion.Equals(codOperacion))
                                            .Select(x => new ExtendedSelectListItem
            {
                Value = x.cod,
                Text = x.nom,
                Selected = x.selected
            });
            viewModel.ddlClasificacionGarantia = dataInfo.lstClasificacionGarantia
                                                    .Where(x => x.cod_operacion.Equals(codOperacion))
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
            var viewModel = new AuxiliarEdit();
            if (codProducto == null || codProducto == string.Empty)
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            var user = (SEG_USUARIO_BE)Session["Usuario"];
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
            var dataPYG = bl.fn_ren_pyg("@PYG", idClienteProducto);
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

            var dataPYG = bl.fn_ren_pyg("@PYG", model.ide_cliente_producto);
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
            viewModel.amortizacion = dataInfo.lstProducto.Where(x => x.cod.Equals(simData.cod_producto.ToString())).FirstOrDefault().nom2;

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

        public JsonResult JSON_Cronograma(long ide_cliente_producto)
        {
            var data = bl.fn_ren_calendario("@CRONOGRAMA", ide_cliente_producto);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_ReporteComposicion(long ide_cliente_producto)
        {
            var data = bl.fn_ren_vis_clienteProducto_Composicion(ide_cliente_producto);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _sim = JsonConvert.DeserializeObject<REN_SIM_REQ_BE>(_obj[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                var viewModel = new AuxiliarEdit();
                viewModel.simData = new REN_SIM_REQ_BE();
                _sim.ide_usuario = long.Parse(user.IDE_USUARIO.ToString());
                _sim.cod_suscriptor = user.SUSCRIPTOR;
                model.DATA = _sim;

                var reply = new GEN_REPLY_BE();
                if (model.ACCION.Equals("@BORRAR"))
                {
                    reply = bl.fn_ren_pro_clienteProducto_borrar(user.SUSCRIPTOR, _sim.ide_cliente_producto, _sim.ide_usuario);
                    _sim = (REN_SIM_REQ_BE)reply.DATA;
                    viewModel.simData = _sim;
                }
                else
                {
                    reply = bl.fn_ren_pro_clienteProducto(model);
                    if (model.ACCION.Equals("@GRABAR"))
                    {
                        viewModel.simData.tea_base = reply.DATA == null ? 0 : (double)reply.DATA;
                    }
                }
                
                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                if (model.ACCION.Equals("@GRABAR")) // grabar = simular
                {
                    var req = new REN_SIM_REQ_BE();

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

                    var dataPYG = bl.fn_ren_pyg("@PYG",_sim.ide_cliente_producto);
                    viewModel.dataPYG = dataPYG;

                    req.accion = "@Resumen_escenarios";
                    req.cod_suscriptor = user.SUSCRIPTOR;
                    req.ide_cliente_producto = _sim.ide_cliente_producto;

                    var comisiones = bl.fn_ren_pro_clienteComision_vista(user.SUSCRIPTOR, req.ide_cliente_producto);
                    viewModel.dataComision = comisiones;
                    viewModel.amortizacion = dataInfo.lstProducto.Where(x => x.cod.Equals(_sim.cod_producto.ToString())).FirstOrDefault().nom2;

                    var dataResEsc = bl.fn_ren_resumenEsc(req);
                    viewModel.dataResEsc = dataResEsc;

                    var dataRoracRes = bl.fn_ren_vis_clienteProducto_Resumen(_sim.ide_cliente_producto);
                    viewModel.dataRoracRes = dataRoracRes;
                }
                
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
                var viewModel = new AuxiliarEdit();
                var model = new REN_SIM_REQ_BE();
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_ren_pro_clienteProducto_nuevo(user.SUSCRIPTOR, ideUsuario);

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

                var dataPYG = bl.fn_ren_pyg("@PYG", _com.ide_cliente_producto);
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

        public ActionResult TasaTransferencia()
        {

            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];

            var dataVigencia = bl.fn_ren_sel_transferenciaDDL("@fec_vigencia", user.SUSCRIPTOR, user.COD_USUARIO, 1);
            viewModel.ddlVigencia = dataVigencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            var tasaTranf = bl.fn_ren_sel_tasaTranferencia("SELECT", user.SUSCRIPTOR, user.COD_USUARIO, null, 1);
            var tasaTranfValor = bl.fn_ren_sel_tasaTranfValor("SELECT_VALOR", user.SUSCRIPTOR, user.COD_USUARIO, null, 1);

            viewModel.lstTasaTranf = tasaTranf;
            viewModel.lstTasaTranfValor = tasaTranfValor;

            return View(viewModel);

        }

        public JsonResult JSON_CargarTasaTransferencia(string strFecVigencia)
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];

            string formato = "yyyy-MM-dd HH:mm:ss.fff";  // El formato correcto (sin ceros en el día)
            // Intentamos convertir la cadena usando el formato exacto
            DateTime fecVigencia = DateTime.ParseExact(strFecVigencia, formato, System.Globalization.CultureInfo.InvariantCulture);

            var tasaTranf = bl.fn_ren_sel_tasaTranferencia("SELECT", user.SUSCRIPTOR, user.COD_USUARIO, fecVigencia, 1);
            var tasaTranfValor = bl.fn_ren_sel_tasaTranfValor("SELECT_VALOR", user.SUSCRIPTOR, user.COD_USUARIO, fecVigencia, 1);

            viewModel.lstTasaTranf = tasaTranf;
            viewModel.lstTasaTranfValor = tasaTranfValor;

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GrabarTasaTransferencia(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _param = JsonConvert.DeserializeObject<TASA_TRANFERENCIA_GRABAR>(_obj[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_ren_pro_transferencia(codSuscriptor, user.COD_USUARIO, _param);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals(string.Empty) || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                var dataVigencia = bl.fn_ren_sel_transferenciaDDL("@fec_vigencia", user.SUSCRIPTOR, user.COD_USUARIO, 1);
                viewModel.ddlVigencia = dataVigencia.Select(x => new ExtendedSelectListItem
                {
                    Value = x.Value,
                    Text = x.Text,
                    Selected = x.Selected,
                });

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
        public JsonResult JSON_RegresionLogaritmica(double[] xDataPEN, double[] yDataPEN, double[] xDataUSD, double[] yDataUSD)
        {
            // Datos de evaluación
            //double[] xxData = { 0.08000, 0.25000, 1.00000, 1.170000, 1.50000, 2.00000, 3.00000, 3.33000, 12.50000, 20.00000 };
            //double[] yyData = { 0.07480, 0.07500, 0.07840, 0.07610, 0.08120, 0.08620, 0.09030, 0.091668, 0.10300, 0.10400 };

            double[] dataPEN = new double[0];
            double[] dataUSD = new double[0];
            if (xDataPEN != null && yDataPEN != null)
            {
                dataPEN = Util.Formula.RegresionLogaritmica(xDataPEN, yDataPEN);
            }
            if (xDataUSD != null && yDataUSD != null)
            {
                dataUSD = Util.Formula.RegresionLogaritmica(xDataUSD, yDataUSD);
            }

            var data = new
            {
                dataPEN = dataPEN,
                dataUSD = dataUSD
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdmEstado()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            var data = bl.fn_ren_sel_bandeja("BANDEJA", viewModel.CodSuscriptor, viewModel.CodUsuario);

            viewModel.ddlBandeja = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.codBandeja.ToString(),
                Text = x.bandeja,
                //Selected = x.selected,
            });

            viewModel.lstAdmEstado = bl.fn_ren_sel_admEstado("LISTADO", viewModel.CodSuscriptor, viewModel.CodUsuario, 0);
            

            return View(viewModel);
        }

        public JsonResult JSON_GetAdmEstado(int cod)
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var model = new REN_SIM_REQ_BE();
            model.accion = "@EDITAR";
            model.cod_suscriptor = user.SUSCRIPTOR;

            viewModel.lstAdmEstado = bl.fn_ren_sel_admEstado("LISTADO", viewModel.CodSuscriptor, viewModel.CodUsuario, cod);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
