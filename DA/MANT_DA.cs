using BEANS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MANT_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }
        public List<AGENCIA_BE> fn_mant_sel_agencia(string accion, long cod_suscriptor, string cod_usuario)
        {
            Mensaje = string.Empty;
            List<AGENCIA_BE> lista = new List<AGENCIA_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_agencia]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = cod_suscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    AGENCIA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new AGENCIA_BE();
                        bean.cod_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_agencia"));
                        bean.nom_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_agencia"));
                        bean.distrito = DataReader.SafeGetString(dr, dr.GetOrdinal("distrito"));
                        bean.provincia = DataReader.SafeGetString(dr, dr.GetOrdinal("provincia"));
                        bean.departamento = DataReader.SafeGetString(dr, dr.GetOrdinal("departamento"));

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

        public GEN_REPLY_BE fn_mant_pro_agencia(string accion, long codSuscriptor, string codUsuario, AGENCIA_BE param)
        {
            Mensaje = string.Empty;
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_agencia]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = codSuscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = codUsuario;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar,10).Value = param.cod_agencia;
            cmd.Parameters.Add("@nom_agencia", System.Data.SqlDbType.VarChar,50).Value = param.nom_agencia;
            cmd.Parameters.Add("@distrito", System.Data.SqlDbType.VarChar, 50).Value = param.distrito;
            cmd.Parameters.Add("@provincia", System.Data.SqlDbType.VarChar, 50).Value = param.provincia;
            cmd.Parameters.Add("@departamento", System.Data.SqlDbType.VarChar, 50).Value = param.departamento;


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

        public List<FUNCIONARIO_BE> fn_mant_sel_funcionario(string accion, long cod_suscriptor, string cod_usuario)
        {
            Mensaje = string.Empty;
            List<FUNCIONARIO_BE> lista = new List<FUNCIONARIO_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_funcionario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = cod_suscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    FUNCIONARIO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new FUNCIONARIO_BE();
                        bean.cod_funcionario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_funcionario"));
                        bean.nom_funcionario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_funcionario"));
                        bean.ide_usuario = DataReader.SafeGetInt64(dr, dr.GetOrdinal("ide_usuario"));
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.cod_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_agencia"));
                        bean.nom_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_agencia"));
                        bean.cod_personeria = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_personeria"));
                        bean.personeria = DataReader.SafeGetString(dr, dr.GetOrdinal("personeria"));
                        
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
        public List<SEG_USUARIO_BE> fn_mant_sel_usuario(string accion, long cod_suscriptor, string cod_usuario)
        {
            Mensaje = string.Empty;
            List<SEG_USUARIO_BE> lista = new List<SEG_USUARIO_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_funcionario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = cod_suscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_USUARIO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_USUARIO_BE();
                        bean.IDE_USUARIO = DataReader.SafeGetInt64(dr, dr.GetOrdinal("IDE_USUARIO"));
                        bean.COD_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("COD_USUARIO"));
                        bean.NOM_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_USUARIO"));
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
        public List<AGENCIA_BE> fn_mant_sel_funAgencia(string accion, long cod_suscriptor, string cod_usuario)
        {
            Mensaje = string.Empty;
            List<AGENCIA_BE> lista = new List<AGENCIA_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_funcionario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = cod_suscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    AGENCIA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new AGENCIA_BE();
                        bean.cod_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_agencia"));
                        bean.nom_agencia = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_agencia"));
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
        public List<BANCA_BE> fn_mant_sel_banca(string accion, long cod_suscriptor, string cod_usuario)
        {
            Mensaje = string.Empty;
            List<BANCA_BE> lista = new List<BANCA_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_funcionario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = cod_suscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    BANCA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new BANCA_BE();
                        bean.cod_personeria = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_personeria"));
                        bean.personeria = DataReader.SafeGetString(dr, dr.GetOrdinal("personeria"));
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
        public GEN_REPLY_BE fn_mant_pro_funcionario(string accion, long codSuscriptor, string codUsuario, FUNCIONARIO_BE param)
        {
            Mensaje = string.Empty;
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_funcionario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = codSuscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = codUsuario;
            cmd.Parameters.Add("@cod_funcionario", System.Data.SqlDbType.VarChar, 10).Value = param.cod_funcionario;
            cmd.Parameters.Add("@nom_funcionario", System.Data.SqlDbType.VarChar, 50).Value = param.nom_funcionario;
            cmd.Parameters.Add("@ide_usuario_funcionario", System.Data.SqlDbType.BigInt).Value = param.ide_usuario_funcionario;
            cmd.Parameters.Add("@cod_agencia", System.Data.SqlDbType.VarChar, 100).Value = param.cod_agencia;
            cmd.Parameters.Add("@cod_personeria", System.Data.SqlDbType.VarChar, 5).Value = param.cod_personeria;

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

        public List<RORACOBJETIVO_BE> fn_mant_sel_roracObjetivo(string accion, long codSuscriptor, string codUsuario)
        {
            Mensaje = string.Empty;
            List<RORACOBJETIVO_BE> lista = new List<RORACOBJETIVO_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_RORACObjetivo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = codSuscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = codUsuario;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    RORACOBJETIVO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new RORACOBJETIVO_BE();
                        bean.codPersoneria = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_personeria"));
                        bean.personeria = DataReader.SafeGetString(dr, dr.GetOrdinal("personeria"));
                        bean.codTipCliente = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_tip_cliente"));
                        bean.tipCliente = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_cliente"));
                        bean.codProductoBase = DataReader.GetValueOrNull<Int32>(dr, dr.GetOrdinal("cod_producto_base"));
                        bean.productoBase = DataReader.SafeGetString(dr, dr.GetOrdinal("producto_base"));
                        bean.roracObjetivo = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("RORACObjetivo"));
                        bean.autonomiaComercial = DataReader.GetValueOrNull<double>(dr, dr.GetOrdinal("autonomia_comercial"));
                        bean.tipReg = DataReader.SafeGetString(dr, dr.GetOrdinal("tipreg"));

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
        public List<GEN_DDL_BE> fn_mant_sel_roracDDL(string accion, long codSuscriptor, string codUsuario)
        {
            Mensaje = string.Empty;
            List<GEN_DDL_BE> lista = new List<GEN_DDL_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_ren_cud_RORACObjetivo]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_suscriptor", System.Data.SqlDbType.BigInt).Value = codSuscriptor;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = codUsuario;

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
                        if (accion.Equals("@PERSONERIA"))
                        {
                            bean.Value = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_personeria"));
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("personeria"));
                        }
                        if (accion.Equals("@PRODUCTO_BASE"))
                        {
                            bean.Value = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_producto_base")).ToString();
                            bean.Text = DataReader.SafeGetString(dr, dr.GetOrdinal("producto_base"));
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

    }
}
