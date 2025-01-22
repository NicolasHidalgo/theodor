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

            return View(viewModel);
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

            var dataUsuario = bl.fn_mant_sel_usuario("@USUARIO", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlUsuario = dataUsuario.Select(x => new ExtendedSelectListItem
            {
                Value = x.IDE_USUARIO.ToString(),
                Text = x.NOM_USUARIO,
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
                Value = x.cod_personeria,
                Text = x.personeria,
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
            var dataPersoneria = bl.fn_mant_sel_roracDDL("@PERSONERIA", user.SUSCRIPTOR, user.COD_USUARIO);
            viewModel.ddlPersoneria = dataPersoneria.Select(x => new ExtendedSelectListItem
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

            viewModel.mantRorac = bl.fn_mant_sel_roracObjetivo("SELECT", user.SUSCRIPTOR, user.COD_USUARIO);

            return View(viewModel);
        }
    }
}