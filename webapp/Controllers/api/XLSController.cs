using BEANS;
using BL;
using Newtonsoft.Json;
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
    [RoutePrefix("api/XLS")]
    public class XLSController : ApiController
    {
        private readonly XLS_BL db = new XLS_BL();
        public XLSController()
        {

        }
        [HttpGet]
        [Route("GetDataTable_Carga")]
        public DataTable<XLS_CARGA_BE> GetDataTable_Carga([FromUri] DataTableRequest_<GEN_REPLY_BE> model)
        {
            var reply = model.Filter;
            var _obj = (string)reply.DATA;
            var _mod = JsonConvert.DeserializeObject<XLS_CARGA_BE>(_obj);
            reply.DATA = _mod;

            var dat = db.fn_xls_sel_carga(reply);
            var list = (List<XLS_CARGA_BE>)dat.DATA; 
            var query = list.AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                if (_mod.ESTADO != null)
                {
                    if (!_mod.ESTADO.Equals("TODOS"))
                        query = query.Where(x => x.ESTADO.Equals(_mod.ESTADO));
                }
                if (reply.SEARCH != null)
                {
                    if (!reply.SEARCH.Equals(""))
                    {
                        query = query.Where(x => x.CONTROL_01.Contains(reply.SEARCH) || x.CONTROL_02.Contains(reply.SEARCH));
                    }

                }
            }


            if (model.OrderBy.Count() > 0)
            {
                var ob = model.OrderBy.FirstOrDefault();
                if (ob.Property.Equals("CONTROL_01"))
                {
                    if (ob.Direction.Equals("asc"))
                    {
                        query = query.OrderBy(x => x.CONTROL_01);
                    }else
                    {
                        query = query.OrderByDescending(x => x.CONTROL_01);
                    }
                    
                }
                if (ob.Property.Equals("FEC_CARGA"))
                {
                    if (ob.Direction.Equals("asc"))
                    {
                        query = query.OrderBy(x => x.FEC_CARGA);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.FEC_CARGA);
                    }
                }
            }

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
                .Select(x => new XLS_CARGA_BE
                {
                    IDE_CARGA = x.IDE_CARGA,
                    COD_CARGA = x.COD_CARGA,
                    FEC_CARGA = x.FEC_CARGA,
                    USERNAME = x.USERNAME,
                    ARCHIVO_FISICO = x.ARCHIVO_FISICO,
                    ARCHIVO_CARGA = x.ARCHIVO_CARGA,
                    NOM_HOJA = x.NOM_HOJA,
                    FILA_ENCABEZADO = x.FILA_ENCABEZADO,
                    COLUMNA_ENCABEZADO = x.COLUMNA_ENCABEZADO,
                    CONTROL_01 = x.CONTROL_01,
                    CONTROL_02 = x.CONTROL_02,
                    COD_ESTADO = x.COD_ESTADO,
                    FEC_PROCESO_INICIO = x.FEC_PROCESO_INICIO,
                    FEC_PROCESO_FIN = x.FEC_PROCESO_FIN,
                    ERROR = x.ERROR,
                    PARAMETROS = x.PARAMETROS,
                    ESTADO = x.ESTADO,
                    REG_PROCESADOS = x.REG_PROCESADOS
                });

            var dataTable = new DataTable<XLS_CARGA_BE>()
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