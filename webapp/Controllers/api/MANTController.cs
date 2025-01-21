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
    [RoutePrefix("api/MANT")]
    public class MANTController : ApiController
    {
        private readonly MANT_BL db = new MANT_BL();

        [HttpGet]
        [Route("GetDataTable_Agencia")]
        public DataTable<AGENCIA_BE> GetDataTable_Agencia([FromUri] DataTableRequest_<AGENCIA_BE> model)
        {
            var reply = model.Filter;
            //reply.accion = "@POPUP";
            //reply.cod_suscriptor = user.SUSCRIPTOR;
            IQueryable<AGENCIA_BE> query = db.fn_mant_sel_agencia("SELECT", reply.cod_suscriptor, reply.cod_usuario).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                /*
                if (model.Filter.buscar != null)
                {
                    var _search = model.Filter.buscar.ToLower();
                    query = query.Where(
                                       x => x.tip_documento.ToLower().Contains(_search)
                                        || x.num_documento.ToLower().Contains(_search)
                                        || x.Cliente.ToLower().Contains(_search)
                                        || x.Producto.ToLower().Contains(_search)
                                    );
                }
                */
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
                .Select(x => new AGENCIA_BE
                {
                    cod_agencia = x.cod_agencia,
                    nom_agencia = x.nom_agencia,
                    departamento = x.departamento,
                    provincia = x.provincia,
                    distrito = x.distrito,
                });

            var dataTable = new DataTable<AGENCIA_BE>()
            {
                data = data,
                draw = model.Draw.GetValueOrDefault(),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered
            };

            return dataTable;
        }

        [HttpGet]
        [Route("GetDataTable_Funcionario")]
        public DataTable<FUNCIONARIO_BE> GetDataTable_Funcionario([FromUri] DataTableRequest_<FUNCIONARIO_BE> model)
        {
            var reply = model.Filter;
            //reply.accion = "@POPUP";
            //reply.cod_suscriptor = user.SUSCRIPTOR;
            IQueryable<FUNCIONARIO_BE> query = db.fn_mant_sel_funcionario("SELECT", reply.cod_suscriptor, reply.cod_usuario).AsQueryable();

            var recordsTotal = query.Count();

            if (model.Filter != null)
            {
                /*
                if (model.Filter.buscar != null)
                {
                    var _search = model.Filter.buscar.ToLower();
                    query = query.Where(
                                       x => x.tip_documento.ToLower().Contains(_search)
                                        || x.num_documento.ToLower().Contains(_search)
                                        || x.Cliente.ToLower().Contains(_search)
                                        || x.Producto.ToLower().Contains(_search)
                                    );
                }
                */
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
                .Select(x => new FUNCIONARIO_BE
                {
                    cod_funcionario = x.cod_funcionario,
                    nom_funcionario = x.nom_funcionario,
                    ide_usuario = x.ide_usuario,
                    cod_usuario = x.cod_usuario,
                    nom_usuario = x.nom_usuario,
                    cod_agencia = x.cod_agencia,
                    nom_agencia = x.nom_agencia,
                    cod_personeria = x.cod_personeria,
                    personeria = x.personeria,
                });

            var dataTable = new DataTable<FUNCIONARIO_BE>()
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
