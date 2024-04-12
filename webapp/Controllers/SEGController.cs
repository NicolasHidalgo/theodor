using BEANS;
using BL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Authorize]
    public class SEGController : BaseController
    {
        private SEG_BL bl = new SEG_BL();

        public ActionResult Usuario()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit_Usuario(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _user = JsonConvert.DeserializeObject<SEG_USUARIO_BE>(_obj[0]);
                model.DATA = _user;

                var reply = bl.fn_seg_upd_usuario(model);
                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Contains("SUCCESS"))
                    res.Status = HttpStatusCode.OK;

                return Json(res);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     });
        }
        

        public ActionResult Permisos()
        {
            return View();
        }


        public JsonResult JSON_GetPerfil(GEN_REPLY_BE model)
        {
            model = bl.fn_seg_sel_app_perfil(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetPermisos(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _user = JsonConvert.DeserializeObject<SEG_USUARIO_PERFIL_BE>(_obj[0]);
            model.DATA = _user;
            model = bl.fn_seg_sel_permisos(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult JSON_BuscarUsuario(string term)
        {
            List<SEG_USUARIO_BE> usuarios = null;
            if (Session["listaUsuario"] == null)
            {
                GEN_REPLY_BE _reply = new GEN_REPLY_BE();
                _reply.ACCION = "SELECT";
                usuarios = bl.fn_seg_sel_usuario(_reply);
                Session["listaUsuario"] = usuarios;
            }

            usuarios = (List<SEG_USUARIO_BE>)Session["listaUsuario"];
            term = term.ToUpper();
            var lista = usuarios.Where(x => x.COD_USUARIO.ToUpper().Contains(term)
                                        || x.NOM_USUARIO.ToUpper().Contains(term));

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        
       
        public ActionResult CambiarAplicacion(string CodAplicacion)
        {
            var user = new SEG_USUARIO_BE();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login","Login");
            }

            user = (SEG_USUARIO_BE)Session["Usuario"];

            GEN_REPLY_BE req = new GEN_REPLY_BE();
            req.COD_USUARIO = user.COD_USUARIO;
            req.ACCION = "DIRECTO";
            req.COD_APLICACION = CodAplicacion;

            var listaOpc = new List<SEG_OPCION_BE>();
            req = bl.fn_seg_menuDinamico(req, ref listaOpc);

            user.Opciones = ((List<SEG_OPCION_BE>)req.DATA).AsEnumerable();
            user.OpcionesData = listaOpc;
            Session["Usuario"] = user;

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult Permisos(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _aux = _obj[0];
                var _per = JsonConvert.DeserializeObject<SEG_USUARIO_PERFIL_BE>(_aux);
                model.DATA = _per;

                var reply = bl.fn_seg_upd_permisos(model);
                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Contains("SUCCESS"))
                    res.Status = HttpStatusCode.OK;

                return Json(res);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     });
        }
    }
}
