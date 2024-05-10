using BEANS;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapp.Interfaces;
using webapp.ViewModels;

namespace webapp.Controllers.api
{
    [RoutePrefix("api/REN")]
    public class RENController : ApiController
    {
        private readonly REN_BL db = new REN_BL();

        [HttpGet]
        [Route("GetDataTable_PopUp")]
        public DataTable<REN_POPUP_BE> GetDataTable_PopUp([FromUri] DataTableRequest_<REN_SIM_REQ_BE> model)
        {
            var reply = model.Filter;
            //reply.accion = "@POPUP";
            //reply.cod_suscriptor = user.SUSCRIPTOR;
            IQueryable<REN_POPUP_BE> query = db.fn_ren_pro_listarPopup(reply).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                //if (model.Filter.SEARCH != null)
                //{
                //    var _search = model.Filter.SEARCH.ToUpper();
                //    query = query.Where(x => x.COD_USUARIO.ToUpper().Contains(_search)
                //                    || x.NOM_USUARIO.ToUpper().Contains(_search)
                //                    || x.CORREO_ELECTRONICO.ToUpper().Contains(_search));
                //}
            }

            if (model.OrderBy.Count() > 0)
            {
                //por implementar
            }
            //else
            //{
            //    query = query.OrderBy(x => x.cod_tajo_estructura);
            //}

            var recordsFiltered = query.Count();

            if (model.Start != null)
            {
                query = query.Skip(model.Start.GetValueOrDefault());
            }

            if (model.Length != null)
            {
                query = query.Take(model.Length.GetValueOrDefault());
            }

            var data = query
                .AsEnumerable()
                .Select(x => new REN_POPUP_BE
                {
                    ide_cliente_Producto = x.ide_cliente_Producto,
                    Documento = x.Documento,
                    Cliente = x.Cliente,
                    Operacion = x.Operacion,
                    Producto = x.Producto,
                    Monto = x.Monto,
                    Tea = x.Tea,
                    Plazo = x.Plazo,
                });

            var dataTable = new DataTable<REN_POPUP_BE>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }


    }
}
