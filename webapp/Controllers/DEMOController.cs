using BEANS;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class DEMOController : Controller
    {
        private DEMO_BL MOV_BL = new DEMO_BL();
        public ActionResult Demo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditMov(DEMO_BE model)
        {
            if (ModelState.IsValid)
            {
                var reply = MOV_BL.fn_pro_mov(model);
                var res = new Response();
                res.Message = reply.MENSAJE;

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
