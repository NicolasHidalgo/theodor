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

namespace webapp.Controllers
{
    [Authorize]
    public class RENController : BaseController
    {
       
        private REN_BL bl = new REN_BL();
 
        public void regresionLogarimitca()
        {

            // Datos de evaluación
            double[] xData = { 0.08000, 0.25000, 0.50000, 1.00000, 1.50000, 2.00000, 3.00000, 10.0000, 15.00000, 20.00000 };
            double[] yData = { 0.07480, 0.07500, 0.07610, 0.07840, 0.08120, 0.08620, 0.09030, 0.09975, 0.10531, 0.11256 };

            // Llamar al método para ajustar los parámetros
            FitExtendedNelsonSiegelParameters(xData, yData, out double beta0, out double beta1, out double beta2, out double beta3, out double lambda1, out double lambda2);

            // Mostrar los resultados
            Console.WriteLine($"beta0: {beta0}");
            Console.WriteLine($"beta1: {beta1}");
            Console.WriteLine($"beta2: {beta2}");
            Console.WriteLine($"beta3: {beta3}");
            Console.WriteLine($"lambda1: {lambda1}");
            Console.WriteLine($"lambda2: {lambda2}");


            // Calcular y mostrar los valores ajustados y residuales
            Console.WriteLine("\nValores ajustados y residuales:");
            for (int i = 0; i < xData.Length; i++)
            {
                //double logX = logXData[i];
                double logX = Math.Log(xData[i]);
                double fittedValue = beta0 + beta1 * logX + beta3 * Math.Pow(logX, 2) + beta3 * Math.Pow(logX, 3);
                double residual = yData[i] - fittedValue;
                Console.WriteLine($"x: {xData[i]}, y: {yData[i]}, y ajustado: {fittedValue}, residual: {residual}");
            }


        }

        static void FitExtendedNelsonSiegelParameters(double[] xData, double[] yData, out double beta0, out double beta1, out double beta2, out double beta3, out double lambda1, out double lambda2)
        {
            // Valores iniciales para los parámetros
            double[] initialParams = { 0.1, 0.1, 0.1, 0.1, 1, 1 }; // beta0, beta1, beta2, beta3, lambda1, lambda2

            // Configuración del descenso de gradiente
            double learningRate = 0.000005; // Tasa de aprendizaje reducida para mayor precisión
            int maxIterations = 500000; // Aumentar el número máximo de iteraciones
            double tolerance = 1e-15; // Tolerancia más baja para mayor precisión


            // Función del modelo de Nelson-Siegel extendido
            Func<double[], double[], double[]> modelFunction = (paramsVector, x) =>
            {
                double paramBeta0 = paramsVector[0];
                double paramBeta1 = paramsVector[1];
                double paramBeta2 = paramsVector[2];
                double paramBeta3 = paramsVector[3];
                double paramLambda1 = paramsVector[4];
                double paramLambda2 = paramsVector[5];

                var yPred = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    double t = x[i];
                    double term1 = (1 - Math.Exp(-paramLambda1 * t)) / (paramLambda1 * t);
                    double term2 = term1 - Math.Exp(-paramLambda1 * t);
                    double term3 = (1 - Math.Exp(-paramLambda2 * t)) / (paramLambda2 * t) - Math.Exp(-paramLambda2 * t);

                    yPred[i] = paramBeta0 + paramBeta1 * term1 + paramBeta2 * term2 + paramBeta3 * term3;
                }

                return yPred;
            };

            // Función de error
            Func<double[], double> objectiveFunction = paramsVector =>
            {
                var yPred = modelFunction(paramsVector, xData);
                double error = 0;
                for (int i = 0; i < yData.Length; i++)
                {
                    error += Math.Pow(yPred[i] - yData[i], 2);
                }
                return Math.Sqrt(error);
            };

            // Función para calcular el gradiente numéricamente
            Func<double[], double[]> gradientFunction = paramsVector =>
            {
                var epsilon = 1e-8;
                var gradient = new double[paramsVector.Length];
                for (int i = 0; i < paramsVector.Length; i++)
                {
                    var perturbedParams = (double[])paramsVector.Clone();
                    perturbedParams[i] += epsilon;
                    var objectiveValuePlus = objectiveFunction(perturbedParams);
                    perturbedParams[i] -= 2 * epsilon;
                    var objectiveValueMinus = objectiveFunction(perturbedParams);
                    gradient[i] = (objectiveValuePlus - objectiveValueMinus) / (2 * epsilon);
                }
                return gradient;
            };

            // Descenso de gradiente
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[] gradient = gradientFunction(initialParams);

                // Actualizar parámetros
                for (int i = 0; i < initialParams.Length; i++)
                {
                    initialParams[i] -= learningRate * gradient[i];
                }

                // Verificar convergencia
                double error = objectiveFunction(initialParams);
                if (error < tolerance)
                {
                    break;
                }

                // Opción para imprimir el progreso
                if (iteration % 100 == 0)
                {
                    Console.WriteLine($"Iteración {iteration}: Error = {error}");
                }
            }

            // Asignar los valores ajustados a los parámetros de salida
            beta0 = initialParams[0];
            beta1 = initialParams[1];
            beta2 = initialParams[2];
            beta3 = initialParams[3];
            lambda1 = initialParams[4];
            lambda2 = initialParams[5];
        }
    


        public ActionResult Simulador()
        {
            regresionLogarimitca();
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
    }
}
