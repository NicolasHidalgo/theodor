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
    [RoutePrefix("api/DEMO")]
    public class DEMOController : ApiController
    {
        private readonly DEMO_BL dbMov = new DEMO_BL();


        [HttpGet]
        [Route("GetDataTable_Movimiento")]
        public DataTable<DEMO_BE> GetDataTable_Movimiento([FromUri] DataTableRequest_<DEMO_BE> model)
        {
            var reply = model.Filter;
            IQueryable<DEMO_BE> query = dbMov.fn_sel_mov(reply).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {

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
                .Select(x => new DEMO_BE
                {
                    COD_CIA = x.COD_CIA,
                    COMPANIA_VENTA_3 = x.COMPANIA_VENTA_3,
                    ALMACEN_VENTA = x.ALMACEN_VENTA,
                    TIPO_MOVIMIENTO = x.TIPO_MOVIMIENTO,
                    NRO_DOCUMENTO = x.NRO_DOCUMENTO,
                    COD_ITEM_2 = x.COD_ITEM_2,
                    FECHA_TRANSACCION = x.FECHA_TRANSACCION,
                    TIPO_DOCUMENTO = x.TIPO_DOCUMENTO,
                    CANTIDAD = x.CANTIDAD,
                    PROVEEDOR = x.PROVEEDOR,
                    MONEDA = x.MONEDA,
                });

            var dataTable = new DataTable<DEMO_BE>()
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
