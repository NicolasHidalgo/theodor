using BEANS;
using BL;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class BaseController : Controller
    {
        /*.EDMX Entities*/
        //protected SEG_BL neg_seguridad;

        /* DATOS DEL SISTEMA */
        //protected tb_sistema Sistema;

        /* DATOS DEL USUARIO */
        //protected Tuple<SEG_USUARIO_BE, GEN_REPLY_BE> Usuario;
        //protected string mensaje = "";
        //static string dominio = ConfigurationManager.AppSettings["dominio"];

        //[DllImport("user32.dll")]
        //static public extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        //Process GetExcelProcess(Microsoft.Office.Interop.Excel.Application excelApp)
        //{
        //    int id;
        //    GetWindowThreadProcessId(excelApp.Hwnd, out id);
        //    return Process.GetProcessById(id);
        //}


        public BaseController()
        {
            //neg_seguridad = new SEG_BL();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var sessionUsuario = Session["Usuario"];
            if (!this.Request.IsAuthenticated)
            //if (!this.Request.IsAuthenticated || sessionUsuario == null)
            {
                //requestContext.HttpContext.Response.Clear();
                //var routeData = new RouteData();
                //routeData.Values.Add("controller", "Error");
                //routeData.Values.Add("action", "Error");
                //routeData.Values.Add("exception", "Sesión Expirada");
                //routeData.Values.Add("statusCode", 999);

                //Response.TrySkipIisCustomErrors = true;
                //IController controller = new ErrorController();
                //controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                //Response.End();

                if (requestContext.HttpContext.Request.HttpMethod == "POST")
                {              
                    /*
                    var jsonString = "{\"Status\":400,\"Message\": \"666: FIN SESION\" }";
                    requestContext.HttpContext.Response.Clear();
                    requestContext.HttpContext.Response.Write(jsonString);
                    requestContext.HttpContext.Response.ContentType = "application/json";
                    */
                }
                else
                {
                    requestContext.HttpContext.Response.Clear();
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Login");
                    routeData.Values.Add("action", "Login");
                    Response.TrySkipIisCustomErrors = true;
                    IController controller = new LoginController();
                    controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                    Response.End();
                }

                
            }
            else
            {
                if (sessionUsuario == null)
                {
                    GEN_REPLY_BE req = new GEN_REPLY_BE();
                    SEG_USUARIO_BE user = new SEG_USUARIO_BE();
                    var userOwin = HttpContext.GetOwinContext().Authentication.User;
                    user.COD_USUARIO = userOwin.Identity.Name;
                    req.DATA = user;
                    req.ACCION = "ACCESO";
                    SEG_BL seg = new SEG_BL();

                    var res = seg.fn_seg_acceso(req);
                    if (res.MENSAJE != null)
                    {
                        if (res.MENSAJE.Contains("ERROR"))
                        {
                            requestContext.HttpContext.Response.Clear();
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Login");
                            routeData.Values.Add("action", "Login");
                            Response.TrySkipIisCustomErrors = true;
                            IController controller = new LoginController();
                            controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                            Response.End();
                        }
                    }
                    var usuarioSistema = (SEG_USUARIO_BE)res.DATA;
                    user.IDE_USUARIO = usuarioSistema.IDE_USUARIO;
                    user.NOM_USUARIO = usuarioSistema.NOM_USUARIO;
                    user.DIRECCION_USUARIO = usuarioSistema.DIRECCION_USUARIO;
                    user.COD_APLICACION = usuarioSistema.COD_APLICACION;
                    user.EST_USUARIO = usuarioSistema.EST_USUARIO;
                    user.SUSCRIPTOR = usuarioSistema.SUSCRIPTOR;
                    user.IMG_SUSCRIPTOR = usuarioSistema.IMG_SUSCRIPTOR;
                    res.DATA = user;

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
                if (requestContext.HttpContext.Request.HttpMethod != "POST")
                {
                    var user = (SEG_USUARIO_BE)Session["Usuario"];
                    var _lstOpc = user.OpcionesData;
                    var _AbsoluteUri = requestContext.HttpContext.Request.Url.AbsoluteUri;
                    var _AbsolutePath = requestContext.HttpContext.Request.Url.AbsolutePath;
                    var _pos = _AbsoluteUri.IndexOf(_AbsolutePath);
                    var _url = _AbsoluteUri.Substring(_pos, _AbsoluteUri.Length - _pos).ToUpper();

                    var _split = _AbsolutePath.Split('/');
                    var _controllers = _lstOpc.Select(x => x.CONTROLLER.ToUpper()).Where(x => !x.Equals("")).Distinct();

                    var _controller = _split[1].ToUpper();
                    var _action = _split[2].ToUpper();

                    var _cc = _controllers.Where(x => x.Equals(_controller)).FirstOrDefault();
                    if (_cc != null)
                    {
                        if (!_action.StartsWith("JSON"))
                        {
                            var _aux = _lstOpc.Where(x => !x.ACTION.Equals("")).Select(x => "/" + x.CONTROLLER.ToUpper() + "/" + x.ACTION.ToUpper() + x.PARAMETRO.ToUpper());
                            var ii = _aux.Where(x => x.Equals(_url)).FirstOrDefault();
                            if (ii == null)
                            {
                                requestContext.HttpContext.Response.Clear();
                                var routeData = new RouteData();
                                routeData.Values.Add("controller", "Error");
                                routeData.Values.Add("action", "Error");
                                routeData.Values.Add("exception", "Página no autorizada");
                                routeData.Values.Add("statusCode", 403);

                                Response.TrySkipIisCustomErrors = true;
                                IController controller = new ErrorController();
                                controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                                Response.End();
                            }
                        }

                    }
                }
                        

            }

            /*
            var cod_aplicacion = "";
            int number = 0;

            var LoginName = string.Empty;

            if (Session["LoginName"] != null)
            {
                LoginName = Session["LoginName"].ToString();
            }
            else
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    LoginName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                else
                    LoginName = HttpContext.User.Identity.Name.ToString();
            }

            var usuario = HttpContext.User.Identity.Name.ToString();
            var cod_usuario = string.Empty;

            if (LoginName.Contains("\\"))
                cod_usuario = LoginName;
            else if (!(usuario.Contains(dominio)))
                cod_usuario = dominio + usuario;
            else
                cod_usuario = usuario;

            Session["clientId"] = cod_usuario;
            var hostname = Dns.GetHostName();
            var session = Session.SessionID;
            Session["session"] = session;
            cod_aplicacion = "";
            if (requestContext.RouteData.Values["id"] != null)
            {
                cod_aplicacion = requestContext.RouteData.Values["id"].ToString();
                if (int.TryParse(cod_aplicacion, out number))
                    cod_aplicacion = Session["cod_aplicacion"].ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                    cod_aplicacion = Session["cod_aplicacion"].ToString();
            }

            Usuario = neg_seguridad.up_seg_pro_usuario(cod_usuario, cod_aplicacion, "ACCESO", hostname, session);

            GEN_REPLY_BE log = null;
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();

            //VALIDANDO ERRORES DE USUARIO
            var errorUsuario = false;
            if (Usuario.Item2.tipo == "ERROR")
            {
                requestContext.HttpContext.Response.Clear();
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "Error");
                routeData.Values.Add("exception", Usuario.Item2.mensaje);
                routeData.Values.Add("statusCode", 999);

                Response.TrySkipIisCustomErrors = true;
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                Response.End();
                errorUsuario = true;
            }

            if (errorUsuario == false)
            {
                //VALIDANDO ERRORES DE SESSION
                var errorSession = false;
                var idx = requestContext.HttpContext.Request["idx"];
                var isAjax = requestContext.HttpContext.Request.IsAjaxRequest();
                if (controllerName != "Home" && isAjax == false)
                {
                    if (idx == null || idx != Session.SessionID)
                    {
                        GEN_REPLY_BE execute = null;
                        if (cod_aplicacion == string.Empty)
                        {
                            if (requestContext.RouteData.Values["controller"] != null)
                            {
                                cod_aplicacion = requestContext.RouteData.Values["controller"].ToString();
                                if (int.TryParse(cod_aplicacion, out number))
                                    cod_aplicacion = Session["cod_aplicacion"].ToString();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                                    cod_aplicacion = Session["cod_aplicacion"].ToString();
                            }
                        }


                        if (idx.StartsWith("qwe"))
                        {
                            execute = neg_seguridad.Update(Usuario.Item1.cod_usuario, cod_aplicacion, "SESSION", actionName, controllerName, idx);
                            if (!(execute.mensaje.StartsWith("OK")))
                            {
                                requestContext.HttpContext.Response.Clear();
                                var routeData = new RouteData();
                                routeData.Values.Add("controller", "Error");
                                routeData.Values.Add("action", "Error");
                                routeData.Values.Add("exception", execute.mensaje);
                                routeData.Values.Add("statusCode", 999);

                                Response.TrySkipIisCustomErrors = true;
                                IController controller = new ErrorController();
                                controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                                Response.End();
                                errorSession = true;
                            }
                        }
                        else
                        {
                            requestContext.HttpContext.Response.Clear();
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Error");
                            routeData.Values.Add("action", "Error");
                            routeData.Values.Add("exception", "Error: Sesión expirada.");
                            routeData.Values.Add("statusCode", 999);

                            Response.TrySkipIisCustomErrors = true;
                            IController controller = new ErrorController();
                            controller.Execute(new RequestContext(requestContext.HttpContext, routeData));
                            Response.End();
                            errorSession = true;
                        }
                    }
                }

                if (errorSession == false)
                {
                    if (cod_aplicacion == null || cod_aplicacion == string.Empty)
                        cod_aplicacion = Usuario.Item1.cod_aplicacion;

                    if (Usuario.Item1 != null)
                    {
                        Session["cod_unidad_negocio"] = Usuario.Item1.cod_unidad_negocio;

                        //Verificando cambios de aplicación
                        if (!string.IsNullOrEmpty(Session["cod_aplicacion"] as string))
                        {
                            if (Session["cod_aplicacion"].ToString() == cod_aplicacion)
                            {
                                if (Session["aplicaciones"] == null || (Session["aplicaciones"] as IEnumerable<SEG_MENU_BE>)?.Count() == 0) { }
                                Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MENU_BE>();

                                if (Session["Opciones"] == null || (Session["Opciones"] as IEnumerable<SEG_MENU_BE>)?.Count() == 0)
                                    Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MENU_BE>();

                                if (Session["Carousel"] == null || (Session["Carousel"] as IEnumerable<SEG_MENU_BE>)?.Count() == 0)
                                    Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion, string.Empty).AsEnumerable<SEG_MENU_BE>();
                            }
                            else
                            {
                                Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MENU_BE>();
                                Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MENU_BE>();
                                Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion, string.Empty).AsEnumerable<SEG_MENU_BE>();
                            }
                        }
                        else
                        {
                            Session["aplicaciones"] = neg_seguridad.fn_seg_aplicaciones(Usuario.Item1.cod_usuario, 0, "APLICACIONES").AsEnumerable<SEG_MENU_BE>();
                            Session["Opciones"] = neg_seguridad.fn_seg_menu(Usuario.Item1.cod_usuario, 0, "", cod_aplicacion).AsEnumerable<SEG_MENU_BE>();
                            Session["Carousel"] = neg_seguridad.fn_seg_sel_carousel(Usuario.Item1.cod_usuario, "Carousel", cod_aplicacion, string.Empty).AsEnumerable<SEG_MENU_BE>();
                        }

                        Session["cod_aplicacion"] = cod_aplicacion;
                        Session["Usuario"] = Usuario.Item1;

                        log = neg_seguridad.Update(Usuario.Item1.cod_usuario, cod_aplicacion, "LOG", hostname, controllerName, session);
                    }
                }

            }
            */
        }

    }

}