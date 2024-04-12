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
    [RoutePrefix("api/SEG")]
    public class SEGController : ApiController
    {
        private readonly SEG_BL db = new SEG_BL();
        public SEGController()
        {

        }
        
        [HttpGet]
        [Route("GetDataTable_Usuario")]
        public DataTable<SEG_USUARIO_BE> GetDataTable_Usuario([FromUri] DataTableRequest_<GEN_REPLY_BE> model)
        {
            var reply = model.Filter;
            IQueryable<SEG_USUARIO_BE> query = db.fn_seg_sel_usuario(reply).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (model.Filter.SEARCH != null)
                {
                    var _search = model.Filter.SEARCH.ToUpper();
                    query = query.Where(x => x.COD_USUARIO.ToUpper().Contains(_search)
                                    || x.NOM_USUARIO.ToUpper().Contains(_search)
                                    || x.CORREO_ELECTRONICO.ToUpper().Contains(_search));
                }
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
                .Select(x => new SEG_USUARIO_BE
                {
                    IDE_USUARIO = x.IDE_USUARIO,
                    COD_USUARIO = x.COD_USUARIO,
                    NOM_USUARIO = x.NOM_USUARIO,
                    EST_USUARIO = x.EST_USUARIO,
                    CORREO_ELECTRONICO = x.CORREO_ELECTRONICO,
                    PASSWORD = x.PASSWORD,
                    FEC_CESE = x.FEC_CESE,
                });

            var dataTable = new DataTable<SEG_USUARIO_BE>()
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
