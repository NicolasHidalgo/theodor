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
using System.Web.Http;
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
            if (this.Request.IsAuthenticated)
            {
                //var isAjaxcall = Request.IsAjaxRequest();
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
            }

        }

    }

}