using BEANS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class REP_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }

        public List<GEN_DDL_BE> fn_rep_sel_ddl(string accion, long codSuscriptor)
        {
            Mensaje = string.Empty;
            List<GEN_DDL_BE> lista = new List<GEN_DDL_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_rep_dashboard1]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = codSuscriptor;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_DDL_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new GEN_DDL_BE();
                        if (accion.Equals("FULL_DDL"))
                        {
                            bean.Aux1 = DataReader.SafeGetString(dr, dr.GetOrdinal("tipo"));
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("value"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("text"));
                            bean.Aux2 = DataReader.SafeGetString(dr, dr.GetOrdinal("referencia"));
                        }

                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje += " ERROR: " + ex.Message;
            }
            finally
            {
                Mensaje += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }

        public List<REP_DASHBOARD_BE> fn_rep_sel_dashboard0(REP_DASHBOARD_PARAM param)
        {
            Mensaje = string.Empty;
            List<REP_DASHBOARD_BE> lista = new List<REP_DASHBOARD_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_rep_dashboard0]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = param.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = param.codSuscriptor;

            cmd.Parameters.Add("@fecha_desde", System.Data.SqlDbType.DateTime).Value = param.fechaDesde;
            cmd.Parameters.Add("@fecha_hasta", System.Data.SqlDbType.DateTime).Value = param.fechaHasta;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = param.codMoneda;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = param.codPersoneria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = param.codTipCliente;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = param.codOperacion;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = param.codProducto;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = param.codClasificacionInterna;
            cmd.Parameters.Add("@garantia", System.Data.SqlDbType.Bit).Value = param.garantia;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar, 10).Value = param.codAgencia;
            cmd.Parameters.Add("@cod_funcionario", System.Data.SqlDbType.Int).Value = param.codFuncionario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REP_DASHBOARD_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REP_DASHBOARD_BE();
                        bean.item = DataReader.SafeGetInt32(dr, dr.GetOrdinal("item"));
                        bean.rubro = DataReader.SafeGetString(dr, dr.GetOrdinal("rubro"));
                        bean.codRef = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_ref"));
                        bean.nomRef = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_ref"));
                        bean.noEnProceso = DataReader.SafeGetInt32(dr, dr.GetOrdinal("No_enProceso"));
                        bean.montoEnProceso = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Monto_enProceso"));
                        bean.noDesembolsado = DataReader.SafeGetInt32(dr, dr.GetOrdinal("No_Desembolsado"));
                        bean.desembolso = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Desembolso"));
                        bean.tea = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Tea"));
                        bean.perdidaEsperada = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Perdida_Esperada"));
                        bean.spread = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Spread"));
                        bean.profit = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Profit"));
                        bean.rorac = DataReader.GetValueOrNull<double>  (dr, dr.GetOrdinal("RORAC"));
                        bean.utilidad = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad"));
                        bean.desembolsoObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Desembolso_objetivo"));
                        bean.teaObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("TEA_objetivo"));
                        bean.utilidadObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad_objetivo"));
                        bean.roracObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORAC_objetivo"));

                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje += " ERROR: " + ex.Message;
            }
            finally
            {
                Mensaje += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }

        public List<REP_DASHBOARD_BE> fn_rep_sel_dashboard1(REP_DASHBOARD_PARAM param)
        {
            Mensaje = string.Empty;
            List<REP_DASHBOARD_BE> lista = new List<REP_DASHBOARD_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_rep_dashboard1]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = param.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = param.codSuscriptor;

            cmd.Parameters.Add("@fecha_desde", System.Data.SqlDbType.DateTime).Value = param.fechaDesde;
            cmd.Parameters.Add("@fecha_hasta", System.Data.SqlDbType.DateTime).Value = param.fechaHasta;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = param.codMoneda;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = param.codPersoneria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = param.codTipCliente;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = param.codOperacion;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = param.codProducto;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = param.codClasificacionInterna;
            cmd.Parameters.Add("@garantia", System.Data.SqlDbType.Bit).Value = param.garantia;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar, 10).Value = param.codAgencia;
            cmd.Parameters.Add("@cod_funcionario", System.Data.SqlDbType.Int).Value = param.codFuncionario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REP_DASHBOARD_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REP_DASHBOARD_BE();
                        bean.item = DataReader.SafeGetInt32(dr, dr.GetOrdinal("item"));
                        bean.rubro = DataReader.SafeGetString(dr, dr.GetOrdinal("rubro"));
                        bean.codRef = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_ref"));
                        bean.nomRef = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_ref"));
                        bean.noEnProceso = DataReader.SafeGetInt32(dr, dr.GetOrdinal("No_enProceso"));
                        bean.montoEnProceso = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Monto_enProceso"));
                        bean.noDesembolsado = DataReader.SafeGetInt32(dr, dr.GetOrdinal("No_Desembolsado"));
                        bean.desembolso = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Desembolso"));
                        bean.tea = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Tea"));
                        bean.perdidaEsperada = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Perdida_Esperada"));
                        bean.spread = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Spread"));
                        bean.profit = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Profit"));
                        bean.rorac = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORAC"));
                        bean.utilidad = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad"));
                        bean.desembolsoObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Desembolso_objetivo"));
                        bean.teaObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("TEA_objetivo"));
                        bean.utilidadObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad_objetivo"));
                        bean.roracObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORAC_objetivo"));
                        bean.rankingDesembolso = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Ranking_Desembolso"));
                        bean.rankingcDesembolso = DataReader.SafeGetString(dr, dr.GetOrdinal("Ranking_cDesembolso"));
                        bean.rankingRorac = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Ranking_RORAC"));
                        bean.rankingcRorac = DataReader.SafeGetString(dr, dr.GetOrdinal("Ranking_cRORAC"));

                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje += " ERROR: " + ex.Message;
            }
            finally
            {
                Mensaje += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }

        public List<REP_DASHBOARD_BE> fn_rep_sel_dashboard2(REP_DASHBOARD_PARAM param)
        {
            Mensaje = string.Empty;
            List<REP_DASHBOARD_BE> lista = new List<REP_DASHBOARD_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_rep_dashboard2]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = param.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = param.codSuscriptor;

            cmd.Parameters.Add("@fecha_desde", System.Data.SqlDbType.DateTime).Value = param.fechaDesde;
            cmd.Parameters.Add("@fecha_hasta", System.Data.SqlDbType.DateTime).Value = param.fechaHasta;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = param.codMoneda;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = param.codPersoneria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = param.codTipCliente;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = param.codOperacion;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = param.codProducto;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = param.codClasificacionInterna;
            cmd.Parameters.Add("@garantia", System.Data.SqlDbType.Bit).Value = param.garantia;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar, 10).Value = param.codAgencia;
            cmd.Parameters.Add("@cod_funcionario", System.Data.SqlDbType.Int).Value = param.codFuncionario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REP_DASHBOARD_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REP_DASHBOARD_BE();
                        bean.item = DataReader.SafeGetInt32(dr, dr.GetOrdinal("item"));
                        bean.rubro = DataReader.SafeGetString(dr, dr.GetOrdinal("rubro"));
                        bean.fecha = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("Fecha"));
                        bean.desembolsado = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Desembolsado"));
                        bean.tea = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Tea"));
                        bean.utilidad = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad"));
                        bean.profit = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Profit"));
                        bean.rorac = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORAC"));
                        
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje += " ERROR: " + ex.Message;
            }
            finally
            {
                Mensaje += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }

        public List<REP_DASHBOARD3_BE> fn_rep_sel_dashboard3(REP_DASHBOARD_PARAM param)
        {
            Mensaje = string.Empty;
            List<REP_DASHBOARD3_BE> lista = new List<REP_DASHBOARD3_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_rep_dashboard3]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = param.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = param.codSuscriptor;

            cmd.Parameters.Add("@fecha_desde", System.Data.SqlDbType.DateTime).Value = param.fechaDesde;
            cmd.Parameters.Add("@fecha_hasta", System.Data.SqlDbType.DateTime).Value = param.fechaHasta;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = param.codMoneda;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = param.codPersoneria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = param.codTipCliente;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = param.codOperacion;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = param.codProducto;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = param.codClasificacionInterna;
            cmd.Parameters.Add("@garantia", System.Data.SqlDbType.Bit).Value = param.garantia;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar, 10).Value = param.codAgencia;
            cmd.Parameters.Add("@cod_funcionario", System.Data.SqlDbType.Int).Value = param.codFuncionario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REP_DASHBOARD3_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REP_DASHBOARD3_BE();
                        bean.item = DataReader.SafeGetInt32(dr, dr.GetOrdinal("item"));
                        bean.codCat = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_cat"));
                        bean.categoria = DataReader.SafeGetString(dr, dr.GetOrdinal("categoria"));
                        bean.color = DataReader.SafeGetString(dr, dr.GetOrdinal("color"));
                        bean.codRub = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_rub"));
                        bean.rubro = DataReader.SafeGetString(dr, dr.GetOrdinal("rubro"));
                        bean.no = DataReader.SafeGetInt32(dr, dr.GetOrdinal("No"));
                        bean.ratio = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Ratio"));
                        bean.monto = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Monto"));
                        bean.tea = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Tea"));
                        bean.esperada = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Esperada"));
                        bean.spread = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Spread"));
                        bean.profit = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Profit"));
                        bean.rorac = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORAC"));
                        bean.utilidad = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Utilidad"));

                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje += " ERROR: " + ex.Message;
            }
            finally
            {
                Mensaje += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }

    }
}
