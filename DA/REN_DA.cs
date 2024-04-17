using BEANS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class REN_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }
        public List<GEN_DDL_BE> fn_ren_sel_ddl(REN_SIM_REQ_BE model)
        {
            Mensaje = string.Empty;
            List<GEN_DDL_BE> lista = new List<GEN_DDL_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_pro_clienteProducto]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = model.cod_suscriptor;
            cmd.Parameters.Add("@ide_cliente", System.Data.SqlDbType.BigInt).Value = model.ide_cliente;
            cmd.Parameters.Add("@cod_tip_documento", System.Data.SqlDbType.Int).Value = model.cod_tip_documento;
            cmd.Parameters.Add("@num_documento", System.Data.SqlDbType.VarChar, 20).Value = model.num_documento;
            cmd.Parameters.Add("@nom_cliente", System.Data.SqlDbType.VarChar, 250).Value = model.nom_cliente;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = model.cod_personeria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = model.cod_tip_cliente;
            cmd.Parameters.Add("@cod_tip_prospecto", System.Data.SqlDbType.Int).Value = model.cod_tip_prospecto;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = model.ide_cliente_producto;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = model.cod_producto;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = model.cod_operacion;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = model.cod_moneda;
            cmd.Parameters.Add("@monto", System.Data.SqlDbType.Float).Value = model.monto;
            cmd.Parameters.Add("@cod_canal_atencion", System.Data.SqlDbType.Int).Value = model.cod_canal_atencion;
            cmd.Parameters.Add("@tea", System.Data.SqlDbType.Float).Value = model.tea;
            cmd.Parameters.Add("@plazo", System.Data.SqlDbType.Float).Value = model.plazo;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_interna;
            cmd.Parameters.Add("@cod_clasificacion_externa", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_externa;
            cmd.Parameters.Add("@cod_garantia_real", System.Data.SqlDbType.Int).Value = model.cod_garantia_real;
            cmd.Parameters.Add("@cod_moneda_garantia_real", System.Data.SqlDbType.Int).Value = model.cod_moneda_garantia_real;
            cmd.Parameters.Add("@monto_garantia_real", System.Data.SqlDbType.Float).Value = model.monto_garantia_real;
            cmd.Parameters.Add("@cod_garantia_personal", System.Data.SqlDbType.Int).Value = model.cod_garantia_personal;
            cmd.Parameters.Add("@cod_moneda_garantia_personal", System.Data.SqlDbType.Int).Value = model.cod_moneda_garantia_personal;
            cmd.Parameters.Add("@monto_garantia_personal", System.Data.SqlDbType.Float).Value = model.monto_garantia_personal;
            cmd.Parameters.Add("@cod_clasificacion_garantia", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_garantia;
            cmd.Parameters.Add("@cod_modelo_rorac", System.Data.SqlDbType.Int).Value = model.cod_modelo_rorac;
            cmd.Parameters.Add("@ide_usuario", System.Data.SqlDbType.BigInt).Value = model.ide_usuario;

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
                        if (model.accion.Equals("personeria"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_personeria"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("personeria"));
                        }
                        if (model.accion.Equals("amortizacion"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_tip_amortizacion")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_amortizacion"));
                        }
                        if (model.accion.Equals("moneda"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_moneda")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("moneda"));
                        }
                        if (model.accion.Equals("tip_documento"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_tip_documento")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_documento"));
                        }
                        if (model.accion.Equals("tip_cliente"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tip_cliente"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_cliente"));
                        }
                        if (model.accion.Equals("canal_atencion"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_canal_atencion")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("canal_atencion"));
                        }
                        if (model.accion.Equals("operacion"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_operacion"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("operacion"));
                        }
                        if (model.accion.Equals("producto"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_producto")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("producto"));
                        }
                        if (model.accion.Equals("clasificacion_interna"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_clasificacion_interna"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("clasificacion_interna"));
                        }
                        if (model.accion.Equals("clasificacion_externa"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_clasificacion_externa"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("clasificacion_externa"));
                        }
                        if (model.accion.Equals("garantia_real"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_garantia_real")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("garantia_real"));
                        }
                        if (model.accion.Equals("garantia_personal"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_garantia_personal")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("garantia_personal"));
                        }
                        if (model.accion.Equals("clasificacion_garantia"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_clasificacion_garantia"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("clasificacion_garantia"));
                        }
                        if (model.accion.Equals("comision_servicio"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_comision_servicio")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("comision_servicio"));
                            bean.Aux1 = DataReader.SafeGetString(dr, dr.GetOrdinal("periodicidad"));
                            bean.Aux2 = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_valor"));
                            bean.Aux3 = DataReader.SafeGetString(dr, dr.GetOrdinal("valor"));
                        }
                        if (model.accion.Equals("periodicidad"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_periodicidad")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("periodicidad"));
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

        public List<REN_PYG_BE> fn_ren_pyg(long idClienteProducto)
        {
            Mensaje = string.Empty;
            List<REN_PYG_BE> lista = new List<REN_PYG_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * from dbo.fn_ren_pyg(@idClienteProducto)";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@idClienteProducto", idClienteProducto);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_PYG_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_PYG_BE();
                        bean.ide = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide"));
                        bean.detalle = DataReader.SafeGetString(dr, dr.GetOrdinal("detalle"));
                        bean.operacion = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("operacion"));
                        bean.anual = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("anual"));
                        bean.trimestral = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("trimestral"));
                        bean.mensual = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("mensual"));
                        bean.ratio = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("ratio"));
                        bean.color = DataReader.SafeGetString(dr, dr.GetOrdinal("color"));
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

        public List<REN_RESUMEN_ESC_BE> fn_ren_resumenEsc(REN_SIM_REQ_BE model)
        {
            Mensaje = string.Empty;
            List<REN_RESUMEN_ESC_BE> lista = new List<REN_RESUMEN_ESC_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_pro_clienteProducto]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = model.cod_suscriptor;
            cmd.Parameters.Add("@ide_cliente", System.Data.SqlDbType.BigInt).Value = model.ide_cliente;
            cmd.Parameters.Add("@cod_tip_documento", System.Data.SqlDbType.Int).Value = model.cod_tip_documento;
            cmd.Parameters.Add("@num_documento", System.Data.SqlDbType.VarChar, 20).Value = model.num_documento;
            cmd.Parameters.Add("@nom_cliente", System.Data.SqlDbType.VarChar, 250).Value = model.nom_cliente;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = model.cod_personeria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = model.cod_tip_cliente;
            cmd.Parameters.Add("@cod_tip_prospecto", System.Data.SqlDbType.Int).Value = model.cod_tip_prospecto;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = model.ide_cliente_producto;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = model.cod_producto;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = model.cod_operacion;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = model.cod_moneda;
            cmd.Parameters.Add("@monto", System.Data.SqlDbType.Float).Value = model.monto;
            cmd.Parameters.Add("@cod_canal_atencion", System.Data.SqlDbType.Int).Value = model.cod_canal_atencion;
            cmd.Parameters.Add("@tea", System.Data.SqlDbType.Float).Value = model.tea;
            cmd.Parameters.Add("@plazo", System.Data.SqlDbType.Float).Value = model.plazo;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_interna;
            cmd.Parameters.Add("@cod_clasificacion_externa", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_externa;
            cmd.Parameters.Add("@cod_garantia_real", System.Data.SqlDbType.Int).Value = model.cod_garantia_real;
            cmd.Parameters.Add("@cod_moneda_garantia_real", System.Data.SqlDbType.Int).Value = model.cod_moneda_garantia_real;
            cmd.Parameters.Add("@monto_garantia_real", System.Data.SqlDbType.Float).Value = model.monto_garantia_real;
            cmd.Parameters.Add("@cod_garantia_personal", System.Data.SqlDbType.Int).Value = model.cod_garantia_personal;
            cmd.Parameters.Add("@cod_moneda_garantia_personal", System.Data.SqlDbType.Int).Value = model.cod_moneda_garantia_personal;
            cmd.Parameters.Add("@monto_garantia_personal", System.Data.SqlDbType.Float).Value = model.monto_garantia_personal;
            cmd.Parameters.Add("@cod_clasificacion_garantia", System.Data.SqlDbType.VarChar, 5).Value = model.cod_clasificacion_garantia;
            cmd.Parameters.Add("@cod_modelo_rorac", System.Data.SqlDbType.Int).Value = model.cod_modelo_rorac;
            cmd.Parameters.Add("@ide_usuario", System.Data.SqlDbType.BigInt).Value = model.ide_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_RESUMEN_ESC_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_RESUMEN_ESC_BE();
                        bean.ide = DataReader.SafeGetInt32(dr, dr.GetOrdinal("Ide"));
                        bean.detalle = DataReader.SafeGetString(dr, dr.GetOrdinal("Detalle"));
                        bean.operacion = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Operacion"));
                        bean.objetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Objetivo"));
                        bean.primaRiesgo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("PrimaRiesgo"));
                        bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                        bean.color = DataReader.SafeGetString(dr, dr.GetOrdinal("color"));
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

        public List<REN_RORAC_COBERTURA_BE> fn_ren_vis_clienteProducto_Resumen(long idClienteProducto)
        {
            Mensaje = string.Empty;
            List<REN_RORAC_COBERTURA_BE> lista = new List<REN_RORAC_COBERTURA_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_vis_clienteProducto_Resumen]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = idClienteProducto;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_RORAC_COBERTURA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_RORAC_COBERTURA_BE();
                        bean.ide = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide"));
                        bean.estilo = DataReader.SafeGetString(dr, dr.GetOrdinal("estilo"));
                        bean.formato = DataReader.SafeGetString(dr, dr.GetOrdinal("formato"));
                        bean.detalle = DataReader.SafeGetString(dr, dr.GetOrdinal("detalle"));
                        bean.res01 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("res01"));
                        bean.res02 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("res02"));
                        bean.res03 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("res03"));
                        bean.res04 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("res04"));
                        bean.res05 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("res05"));

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

        public List<REN_RORAC_MODELO_BE> fn_ren_vis_clienteProducto_Tabla(long idClienteProducto, double incremento_tasa, double incremento_plazo, int accion)
        {
            Mensaje = string.Empty;
            List<REN_RORAC_MODELO_BE> lista = new List<REN_RORAC_MODELO_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_vis_clienteProducto_tabla]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = idClienteProducto;
            cmd.Parameters.Add("@incremento_tasa", System.Data.SqlDbType.Decimal).Value = incremento_tasa;
            cmd.Parameters.Add("@incremento_plazo", System.Data.SqlDbType.Decimal).Value = incremento_plazo;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.Int).Value = accion;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_RORAC_MODELO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_RORAC_MODELO_BE();
                        if (accion == 10)
                        {
                            bean.rorac_objetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("rorac_objetivo"));
                            bean.autonomia_comercial = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("autonomia_comercial"));
                        }
                        else
                        {
                            bean.plazo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("plazo"));
                            bean.valor1 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor1"));
                            bean.valor2 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor2"));
                            bean.valor3 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor3"));
                            bean.valor4 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor4"));
                            bean.valor5 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor5"));
                            bean.valor6 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor6"));
                            bean.valor7 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor7"));
                            bean.valor8 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor8"));
                            bean.valor9 = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor9"));
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

        public List<REN_COMPOSICION_BE> fn_ren_vis_clienteProducto_Composicion(long idClienteProducto)
        {
            Mensaje = string.Empty;
            List<REN_COMPOSICION_BE> lista = new List<REN_COMPOSICION_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_vis_clienteProducto_Composicion]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = idClienteProducto;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_COMPOSICION_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_COMPOSICION_BE();
                        bean.ide = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide"));
                        bean.tipo = DataReader.SafeGetInt32(dr, dr.GetOrdinal("Tipo"));
                        bean.detalle = DataReader.SafeGetString(dr, dr.GetOrdinal("detalle"));
                        bean.valor = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("valor"));
                        bean.invisible = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("invisible"));
                        bean.negativo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("negativo"));

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

        public List<REN_COMISION_BE> fn_ren_pro_clienteComision(REN_COMISION_BE param)
        {
            Mensaje = string.Empty;
            List<REN_COMISION_BE> lista = new List<REN_COMISION_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_pro_clienteComision]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = param.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = param.cod_suscriptor;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = param.ide_cliente_producto;
            cmd.Parameters.Add("@ide_comision", System.Data.SqlDbType.Int).Value = param.ide_comision;
            cmd.Parameters.Add("@cod_periodicidad", System.Data.SqlDbType.Int).Value = param.cod_periodicidad;
            cmd.Parameters.Add("@porcentaje", System.Data.SqlDbType.Float).Value = param.Porcentaje;
            cmd.Parameters.Add("@comision", System.Data.SqlDbType.Float).Value = param.Comision;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    REN_COMISION_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new REN_COMISION_BE();
                        bean.ide_comision = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_comision"));
                        bean.cod_comision_servicio = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_comision_servicio"));
                        bean.comision_servicio = DataReader.SafeGetString(dr, dr.GetOrdinal("comision_servicio"));
                        bean.cPeriodicidad = DataReader.SafeGetString(dr, dr.GetOrdinal("cPeriodicidad"));
                        bean.cod_periodicidad = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_periodicidad"));
                        bean.tip_valor = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_valor"));
                        bean.Porcentaje = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Porcentaje"));
                        bean.Comision = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("Comision"));

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


        public GEN_REPLY_BE fn_ren_pro_clienteProducto(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var obj = (REN_SIM_REQ_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_pro_clienteProducto]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = obj.accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = obj.cod_suscriptor;
            cmd.Parameters.Add("@ide_cliente", System.Data.SqlDbType.BigInt).Value = obj.ide_cliente;
            cmd.Parameters.Add("@cod_tip_documento", System.Data.SqlDbType.Int).Value = obj.cod_tip_documento;
            cmd.Parameters.Add("@num_documento", System.Data.SqlDbType.VarChar, 20).Value = obj.num_documento;
            cmd.Parameters.Add("@nom_cliente", System.Data.SqlDbType.VarChar, 250).Value = obj.nom_cliente;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_personeria;
            cmd.Parameters.Add("@cod_tip_cliente", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_tip_cliente;
            cmd.Parameters.Add("@cod_tip_prospecto", System.Data.SqlDbType.Int).Value = obj.cod_tip_prospecto;
            cmd.Parameters.Add("@ide_cliente_producto", System.Data.SqlDbType.BigInt).Value = obj.ide_cliente_producto;
            cmd.Parameters.Add("@cod_producto", System.Data.SqlDbType.Int).Value = obj.cod_producto;
            cmd.Parameters.Add("@cod_operacion", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_operacion;
            cmd.Parameters.Add("@cod_moneda", System.Data.SqlDbType.Int).Value = obj.cod_moneda;
            cmd.Parameters.Add("@monto", System.Data.SqlDbType.Float).Value = obj.monto;
            cmd.Parameters.Add("@cod_canal_atencion", System.Data.SqlDbType.Int).Value = obj.cod_canal_atencion;
            cmd.Parameters.Add("@tea", System.Data.SqlDbType.Float).Value = obj.tea;
            cmd.Parameters.Add("@plazo", System.Data.SqlDbType.Float).Value = obj.plazo;
            cmd.Parameters.Add("@cod_clasificacion_interna", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_clasificacion_interna;
            cmd.Parameters.Add("@cod_clasificacion_externa", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_clasificacion_externa;
            cmd.Parameters.Add("@cod_garantia_real", System.Data.SqlDbType.Int).Value = obj.cod_garantia_real;
            cmd.Parameters.Add("@cod_moneda_garantia_real", System.Data.SqlDbType.Int).Value = obj.cod_moneda_garantia_real;
            cmd.Parameters.Add("@monto_garantia_real", System.Data.SqlDbType.Float).Value = obj.monto_garantia_real;
            cmd.Parameters.Add("@cod_garantia_personal", System.Data.SqlDbType.Int).Value = obj.cod_garantia_personal;
            cmd.Parameters.Add("@cod_moneda_garantia_personal", System.Data.SqlDbType.Int).Value = obj.cod_moneda_garantia_personal;
            cmd.Parameters.Add("@monto_garantia_personal", System.Data.SqlDbType.Float).Value = obj.monto_garantia_personal;
            cmd.Parameters.Add("@cod_clasificacion_garantia", System.Data.SqlDbType.VarChar, 5).Value = obj.cod_clasificacion_garantia;
            cmd.Parameters.Add("@cod_modelo_rorac", System.Data.SqlDbType.Int).Value = obj.cod_modelo_rorac;
            cmd.Parameters.Add("@ide_usuario", System.Data.SqlDbType.BigInt).Value = obj.ide_usuario;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                var iFilasAfectadas = cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                model.MENSAJE += " ERROR: " + ex.Message;
            }
            finally
            {
                model.MENSAJE += Mensaje;
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return model;
        }
    }
}
