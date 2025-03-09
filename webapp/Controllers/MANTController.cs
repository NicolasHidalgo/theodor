using BEANS;
using BL;
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
    public class MANTController : BaseController
    {
        private MANT_BL bl = new MANT_BL();
        public ActionResult Agencias()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            var ubigeo = Session["ubigeo"] as List<UBIGEO_BE>;
            if (Session["ubigeo"] == null)
            {
                ubigeo = bl.fn_mant_sel_ubigeo("@UBIGEO", user.SUSCRIPTOR, user.COD_USUARIO);
                Session["ubigeo"] = ubigeo;
            }

            var data = ubigeo
                .Select(x => new {
                    departamento = x.departamento,
                    codUbigeo = x.codUbigeo.Substring(0, 2),
                    selected = false
                })
                .Distinct()
                .OrderBy(x => x.departamento)
                .ToList();
            data = data.Prepend(new { departamento = "--Selecciona--", codUbigeo = "", selected = true }).ToList();

            viewModel.ddlDepartamento = data.Select(x => new ExtendedSelectListItem
            {
                Value = x.codUbigeo,
                Text = x.departamento,
                Selected = x.selected,
            });

            return View(viewModel);
        }

        public JsonResult JSON_DepartamentoChange(string codDepartamento)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var ubigeo = Session["ubigeo"] as List<UBIGEO_BE>;
            if (Session["ubigeo"] == null)
            {
                ubigeo = bl.fn_mant_sel_ubigeo("@UBIGEO", user.SUSCRIPTOR, user.COD_USUARIO);
                Session["ubigeo"] = ubigeo;
            }
            var data = ubigeo
                .Where(x => x.codUbigeo.StartsWith(codDepartamento)) 
                .Select(x => new {
                    provincia = x.provincia,
                    codUbigeo = x.codUbigeo.Length >= 4 ? x.codUbigeo.Substring(0, 4) : x.codUbigeo, //x.codUbigeo.Substring(0, 4),
                    //selected = false
                })
                .Distinct()
                .OrderBy(x => x.provincia)
                .ToList();
            //data = data.Append(new { provincia = "", codUbigeo = "", selected = true }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_ProvinciaChange(string codProvincia)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var ubigeo = Session["ubigeo"] as List<UBIGEO_BE>;
            if (Session["ubigeo"] == null)
            {
                ubigeo = bl.fn_mant_sel_ubigeo("@UBIGEO", user.SUSCRIPTOR, user.COD_USUARIO);
                Session["ubigeo"] = ubigeo;
            }
            var data = ubigeo
                .Where(x => x.codUbigeo.StartsWith(codProvincia)) // Filtra por departamento
                .Select(x => new {
                    distrito = x.distrito,
                    codUbigeo = x.codUbigeo.Length >= 6 ? x.codUbigeo.Substring(0, 6) : x.codUbigeo, //x.codUbigeo.Substring(0, 6),
                    //selected = false
                })
                .Distinct()
                .OrderBy(x => x.distrito)
                .ToList();
            //data = data.Append(new { distrito = "", codUbigeo = "", selected = true }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditAgencia(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<AGENCIA_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_agencia(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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
        public ActionResult Funcionarios()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            var dataUsuario = bl.fn_mant_sel_funcionarioUsuario("@USUARIO", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlUsuario = dataUsuario.Select(x => new ExtendedSelectListItem
            {
                Value = x.IDE_USUARIO.ToString(),
                Text = x.COD_USUARIO,
                Selected = false, //x.selected,
            });
            var dataAgencia = bl.fn_mant_sel_funAgencia("@AGENCIA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlAgencia = dataAgencia.Select(x => new ExtendedSelectListItem
            {
                Value = x.cod_agencia,
                Text = x.nom_agencia,
                Selected = false, //x.selected,
            });
            var dataBanca = bl.fn_mant_sel_banca("@BANCA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlBanca = dataBanca.Select(x => new ExtendedSelectListItem
            {
                Value = x.codTipCliente,
                Text = x.tipCliente,
                Selected = false, //x.selected,
            });


            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditFuncionario(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<FUNCIONARIO_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_funcionario(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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

        public ActionResult RoracObjetivo()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            viewModel.mantRorac = bl.fn_mant_sel_roracObjetivo("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            /*
            var dataPersoneria = bl.fn_mant_sel_roracDDL("@PERSONERIA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlPersoneria = dataPersoneria.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });*/
            var dataTipCliente = bl.fn_mant_sel_roracDDL("@TIP_CLIENTE", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlTipCliente = dataTipCliente.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var dataProductoBase = bl.fn_mant_sel_roracDDL("@PRODUCTO_BASE", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlProducto = dataProductoBase.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }

        public JsonResult JSON_RORACObjectivo_Refresh()
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var data = bl.fn_mant_sel_roracObjetivo("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditRORACObjetivo(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<RORACOBJETIVO_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_roracObjetivo(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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


        public ActionResult Productos()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            viewModel.producto = bl.fn_mant_sel_producto("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            var dataProducto = bl.fn_mant_sel_productoDDL("@PRODUCTO_BASE", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlProducto = dataProducto.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Aux1 = x.Aux1,
                Aux2 = x.Aux2,
                Selected = false, //x.Selected,
            });
            var dataTipCliente = bl.fn_mant_sel_productoDDL("@BANCA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlTipCliente = dataTipCliente.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProducto(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<PRODUCTO_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_producto(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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


        public JsonResult JSON_Producto_Refresh()
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var data = bl.fn_mant_sel_producto("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CostosOperativos()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;
            viewModel.costoOperativo = bl.fn_mant_sel_costoOperativo("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);

            var dataOperacion = bl.fn_mant_sel_costoOpeDDL("@OPERACION", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlOperacion = dataOperacion.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var dataCanalAtencion = bl.fn_mant_sel_costoOpeDDL("@CANAL_ATENCION", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlCanalAtencion = dataCanalAtencion.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditCostoOperativo(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<COSTOOPERATIVO_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_costoOperativo(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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

        public JsonResult JSON_CostoOperativo_Refresh()
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var data = bl.fn_mant_sel_costoOperativo("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProbabilidadDefault()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            var dataTipCliente = bl.fn_mant_sel_probabilidadDDL("@TIP_CLIENTE", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlTipCliente = dataTipCliente.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var dataProductoBase = bl.fn_mant_sel_probabilidadDDL("@PRODUCTO_BASE", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlProducto = dataProductoBase.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var dataClasificacion = bl.fn_mant_sel_probabilidadDDL("@CLASIFICACION_INTERNA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlClasificacionInterna = dataClasificacion.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProbabilidadDefault(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<PROBABILIDAD_DEFAULT_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_probabilidadDefault(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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

        public ActionResult ClasificacionInterna()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;
            viewModel.dataCI = bl.fn_mant_sel_clasificacionInterna("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);

            var dataOpe = bl.fn_mant_sel_clasificacionInternaDDL("@OPERACION", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlOperacion = dataOpe.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }
        public JsonResult JSON_ClasificacionInterna_Refresh()
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var data = bl.fn_mant_sel_clasificacionInterna("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditClasificacionInterna(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<CLASIFICACION_INTERNA_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_clasificacionInterna(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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

        public ActionResult ComisionServicio()
        {
            var viewModel = new AuxiliarEdit();
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            viewModel.CodSuscriptor = user.SUSCRIPTOR;
            viewModel.CodUsuario = user.COD_USUARIO;

            var dataProducto = bl.fn_mant_sel_comisionProducto("SELECT_PRODUCTO", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.producto = dataProducto;

            //var dataComision = bl.fn_mant_sel_comisionServicio("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);
            //viewModel.comisionServicio = dataComision;

            var ddlProducto = bl.fn_mant_sel_comisionDDL("@PRODUCTO", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlProducto = ddlProducto.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var ddlComision = bl.fn_mant_sel_comisionDDL("@COMISION_SERVICIO", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlComisionServicio = ddlComision.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var ddlPeriodicidad = bl.fn_mant_sel_comisionDDL("@PERIODICIDAD", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlPeriocidad = ddlPeriodicidad.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });
            var ddlTipValor = bl.fn_mant_sel_comisionDDL("@TIP_VALOR", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlTipValor = ddlTipValor.Select(x => new ExtendedSelectListItem
            {
                Value = x.Value,
                Text = x.Text,
                Selected = x.Selected,
            });

            return View(viewModel);
        }

        public JsonResult JSON_ComisionServicio_Refresh(int codProducto)
        {
            var user = (SEG_USUARIO_BE)Session["Usuario"];
            var data = bl.fn_mant_sel_comisionServicio("SELECT", user.SUSCRIPTOR, user.COD_USUARIO, codProducto);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditComisionServicio(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _dat = (string[])model.DATA;
                var _obj = JsonConvert.DeserializeObject<COMISIONSERVICIO_BE>(_dat[0]);
                var user = (SEG_USUARIO_BE)Session["Usuario"];
                //var viewModel = new AuxiliarEdit();
                var ideUsuario = long.Parse(user.IDE_USUARIO.ToString());
                var codSuscriptor = user.SUSCRIPTOR;
                model.DATA = _obj;

                var reply = new GEN_REPLY_BE();
                reply = bl.fn_mant_pro_comisionServicio(model.ACCION, codSuscriptor, user.COD_USUARIO, _obj);

                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Equals("") || reply.MENSAJE.Contains(Constantes.SUCCESS))
                    res.Status = HttpStatusCode.OK;

                //res.Data = viewModel;

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