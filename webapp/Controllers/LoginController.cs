using BEANS;
using BL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webapp.ViewModels;
using static webapp.App_Start.Startup;

namespace webapp.Controllers
{
    public class LoginController : Controller
    {
        SEG_BL seg = new SEG_BL();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    if (Session["Usuario"] == null)
                    {
                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    }
                    else
                    {
                        return this.RedirectToLocal(returnUrl);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            /*
            if (fullUrl.Contains("localhost"))
            {
                model.GoogleSiteKey = siteKey_localhost;
                model.GoogleSecretKey = secretKey_localhost;
            }
            if (fullUrl.Contains("mineriabreca"))
            {
                model.GoogleSiteKey = siteKey_mineriabreca;
                model.GoogleSecretKey = secretKey_mineriabreca;
            }
            if (fullUrl.Contains("svdcxdpd0"))
            {
                model.GoogleSiteKey = siteKey_svdcxdpd0;
                model.GoogleSecretKey = secretKey_svdcxdpd0;
            }
            if (fullUrl.Contains("svlimdpd0"))
            {
                model.GoogleSiteKey = siteKey_svlimdpd0;
                model.GoogleSecretKey = secretKey_svlimdpd0;
            }
            */


            /*
            if (model.GoogleSiteKey == "" || model.GoogleSiteKey == null)
            {
                ModelState.AddModelError("", "Inválido acceso al sistema.");
                ModelState.AddModelError("", "Ingrese usando el dominio correcto.");
            }
            */
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(LoginVM model, string returnUrl)
        {
            var fullUrl = Request.Url.ToString();
            /*
            if (fullUrl.Contains("localhost"))
            {
                model.GoogleSiteKey = siteKey_localhost;
                model.GoogleSecretKey = secretKey_localhost;
            }
            if (fullUrl.Contains("mineriabreca"))
            {
                model.GoogleSiteKey = siteKey_mineriabreca;
                model.GoogleSecretKey = secretKey_mineriabreca;
            }
            if (fullUrl.Contains("svdcxdpd0"))
            {
                model.GoogleSiteKey = siteKey_svdcxdpd0;
                model.GoogleSecretKey = secretKey_svdcxdpd0;
            }
            if (fullUrl.Contains("svlimdpd0"))
            {
                model.GoogleSiteKey = siteKey_svlimdpd0;
                model.GoogleSecretKey = secretKey_svlimdpd0;
            }

            if (model.GoogleSiteKey == "" || model.GoogleSiteKey == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Inválido acceso al sistema");
                ModelState.AddModelError("", "Ingrese usando el dominio correcto.");
                return View(model);
            }
            */
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            /*
            var CodUsuarioWs = model.CodUsuario;
            var Correo = model.CodUsuario;
            var Dominio = "STRACON";
            
            if (model.CodUsuario.Contains("@stracon.com"))
            {
                CodUsuarioWs = model.CodUsuario.Replace("@stracon.com", "");
            }
            
            if (!model.CodUsuario.Contains("@"))
            {
                Dominio = "";
            }
            */
                

            GEN_REPLY_BE req = new GEN_REPLY_BE();
            req.ACCION = "LOGIN";
            SEG_USUARIO_BE user = new SEG_USUARIO_BE();
            user.COD_USUARIO = model.CodUsuario;
            user.PASSWORD = model.Password;
            req.DATA = user;
            var nombreCompleto = string.Empty;

            var res = seg.fn_seg_login(req);

            if (res.MENSAJE.Contains("ERROR"))
            {
                ModelState.Clear();
                ModelState.AddModelError("", res.MENSAJE);
                return View(model);
            }

            req.ACCION = "ACCESO";
            user.COD_USUARIO = model.CodUsuario;
            user.NOM_USUARIO = nombreCompleto;
            req.DATA = user;

            res = seg.fn_seg_acceso(req);
            if (res.MENSAJE != null)
            {
                if (res.MENSAJE.Contains("ERROR"))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "No hay acceso a la base de datos.");
                    ModelState.AddModelError("", "Consulte con su Administrador de Sistemas");
                    return View(model);
                }
            }
            

            var usuarioSistema = (SEG_USUARIO_BE)res.DATA;
            user.IDE_USUARIO = usuarioSistema.IDE_USUARIO;
            user.DIRECCION_USUARIO = usuarioSistema.DIRECCION_USUARIO;
            user.COD_APLICACION = usuarioSistema.COD_APLICACION;
            user.EST_USUARIO = usuarioSistema.EST_USUARIO;
            if (nombreCompleto == string.Empty)
                user.NOM_USUARIO = usuarioSistema.NOM_USUARIO;

            res.DATA = user;

            var isPersistent = true;
            this.SignInUser(user.COD_USUARIO, isPersistent);

            /*
            //CAPCHA
            var response = Request["g-recaptcha-response"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", model.GoogleSecretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            if (!status)
            {
                ModelState.AddModelError("", "Google reCaptcha validation failed");
                return View(model);
            }
            */

            /*
            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            model.Username = model.Username.ToLower();
            if (model.Username.Contains(dominio))
            {
                model.Username = model.Username.Replace(dominio, "");
            }

            if (model.Username.Contains("dg\\"))
            {
                //Auth BD PENDIENTE DE INVESTIGAR
                // we are in!
                Session["LoginName"] = model.Username;
                return RedirectToLocal(returnUrl);
            }
            else
            {
                var authenticationResult = authService.SignIn(model.Username, model.Password, model.IsPersistent);

                if (authenticationResult.IsSuccess)
                {
                    // we are in!
                    Session["LoginName"] = model.Username;
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", authenticationResult.ErrorMessage);
                return View(model);
            }
            */

            UsuarioSession(res);
            return RedirectToLocal(returnUrl);
        }

        private void UsuarioSession(GEN_REPLY_BE res)
        {
            var user = (SEG_USUARIO_BE)res.DATA;
            res.COD_USUARIO = user.COD_USUARIO;
            res.ACCION = "APLICACIONES";
            user.Aplicaciones = seg.fn_seg_sel_usu_app(res);
            user.COD_APLICACION = user.Aplicaciones.FirstOrDefault().COD_APLICACION;
            res.COD_APLICACION = user.COD_APLICACION;

            res.ACCION = "DIRECTO";
            var listOpc = new List<SEG_OPCION_BE>();
            res = seg.fn_seg_menuDinamico(res, ref listOpc);
            user.Opciones = ((List<SEG_OPCION_BE>)res.DATA).AsEnumerable();
            user.OpcionesData = listOpc.AsEnumerable();

            Session["Usuario"] = user;
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SignInUser(string username, bool isPersistent)
        {
            var claims = new List<Claim>();
            try
            {
                // settings
                claims.Add(new Claim(ClaimTypes.Name, username));
                //var claimIdentities = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var claimIdentities = new ClaimsIdentity(claims, MyAuthentication.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;

                //sign in
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdentities);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Logoff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            //authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}