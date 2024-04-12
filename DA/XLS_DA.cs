using BEANS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class XLS_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }

        public GEN_REPLY_BE fn_xls_sel_permisos(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var per = (XLS_USUARIO_PROCESO_BE)model.DATA;
            List<XLS_USUARIO_PROCESO_BE> lista = new List<XLS_USUARIO_PROCESO_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@ID_USUARIO", System.Data.SqlDbType.Int).Value = per.ID_USUARIO;
            cmd.Parameters.Add("@ID_PROCESO", System.Data.SqlDbType.Int).Value = per.ID_PROCESO;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    XLS_USUARIO_PROCESO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new XLS_USUARIO_PROCESO_BE();
                        bean.ID_USUARIO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ID_USUARIO"));
                        bean.ID_PROCESO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ID_PROCESO"));
                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
        public GEN_REPLY_BE fn_xls_sel_proceso(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var dat = new XLS_PROCESO_BE();
            if (model.DATA != null)
                dat = (XLS_PROCESO_BE)model.DATA;

            List<XLS_PROCESO_BE> lista = new List<XLS_PROCESO_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_xls_proceso]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_CARGA", System.Data.SqlDbType.Int).Value = dat.COD_CARGA;
            //cmd.Parameters.Add("@NOM_CARGA", System.Data.SqlDbType.VarChar, 128).Value = model.NOM_CARGA;
            //cmd.Parameters.Add("@NOM_HOJA", System.Data.SqlDbType.VarChar, 64).Value = model.NOM_HOJA;
            //cmd.Parameters.Add("@PROCEDIMIENTO", System.Data.SqlDbType.VarChar, 64).Value = model.PROCEDIMIENTO;
            //cmd.Parameters.Add("@BACKGROUND", System.Data.SqlDbType.Bit).Value = model.BACKGROUND;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    XLS_PROCESO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new XLS_PROCESO_BE();
                        bean.COD_CARGA = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_CARGA"));
                        bean.NOM_CARGA = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_CARGA"));
                        bean.NOM_HOJA = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_HOJA"));
                        bean.PROCEDIMIENTO = DataReader.SafeGetString(dr, dr.GetOrdinal("PROCEDIMIENTO"));
                        bean.BACKGROUND = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("BACKGROUND"));
                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
        public GEN_REPLY_BE fn_xls_sel_estructura(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var mod = (XLS_PROCESO_ESTRUCTURA_BE)model.DATA;
            List<XLS_PROCESO_ESTRUCTURA_BE> lista = new List<XLS_PROCESO_ESTRUCTURA_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 32).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_CARGA", System.Data.SqlDbType.Int).Value = mod.COD_CARGA;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    XLS_PROCESO_ESTRUCTURA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new XLS_PROCESO_ESTRUCTURA_BE();
                        bean.COD_CARGA = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_CARGA"));
                        bean.NOM_CAMPO = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_CAMPO"));
                        bean.TIPO = DataReader.SafeGetString(dr, dr.GetOrdinal("TIPO"));
                        //bean.EQUIVALENCIA = DataReader.SafeGetString(dr, dr.GetOrdinal("EQUIVALENCIA"));
                        bean.ORDEN = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ORDEN"));
                        bean.COMENTARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("COMENTARIO"));
                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
        public GEN_REPLY_BE fn_xls_sel_parametro(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var mod = (XLS_PROCESO_PARAMETRO_BE)model.DATA;
            List<XLS_PROCESO_PARAMETRO_BE> lista = new List<XLS_PROCESO_PARAMETRO_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_CARGA", System.Data.SqlDbType.Int).Value = mod.COD_CARGA;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "[up_xls_proceso_control]";
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Parameters.Add("@nom_control", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    XLS_PROCESO_PARAMETRO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new XLS_PROCESO_PARAMETRO_BE();
                        List<XLS_PROCESO_PARAMETRO_OPTION_BE> _lstOpt = null;
                        bean.COD_CARGA = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_CARGA"));
                        bean.NOM_CONTROL = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_CONTROL"));
                        bean.ORDEN = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ORDEN"));
                        bean.COLUMN_NAME = DataReader.SafeGetString(dr, dr.GetOrdinal("COLUMN_NAME"));
                        bean.ETIQUETA = DataReader.SafeGetString(dr, dr.GetOrdinal("ETIQUETA"));

                        if (bean.NOM_CONTROL.StartsWith("#"))
                        {
                            cmd2.Parameters["@nom_control"].Value = bean.NOM_CONTROL;
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            _lstOpt = new List<XLS_PROCESO_PARAMETRO_OPTION_BE>();
                            while (dr2.Read())
                            {
                                XLS_PROCESO_PARAMETRO_OPTION_BE _opt = new XLS_PROCESO_PARAMETRO_OPTION_BE();
                                _opt.CODIGO = DataReader.SafeGetString(dr2, dr2.GetOrdinal("CODIGO"));
                                _opt.NOM_CONTROL = DataReader.SafeGetString(dr2, dr2.GetOrdinal("NOM_CONTROL"));
                                _lstOpt.Add(_opt);
                            }
                            bean.lstOption = _lstOpt;
                        }
                        

                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
        public GEN_REPLY_BE fn_xls_upd_permisos(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var per = (XLS_USUARIO_PROCESO_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = "START_PERMISO";
            cmd.Parameters.Add("@ID_USUARIO", System.Data.SqlDbType.Int).Value = per.ID_USUARIO;
            cmd.Parameters.Add("@ID_PROCESO", System.Data.SqlDbType.Int).Value = 0;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                var iFilasAfectadas = cmd.ExecuteNonQuery();

                foreach (var item in per.Procesos)
                {
                    cmd.Parameters["@ACCION"].Value = "INSERT_PERMISO";
                    cmd.Parameters["@ID_PROCESO"].Value = item.ID_PROCESO;
                    cmd.ExecuteNonQuery();
                }
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

        public GEN_REPLY_BE fn_xls_sel_carga(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var dat = (XLS_CARGA_BE)model.DATA;
            List<XLS_CARGA_BE> lista = new List<XLS_CARGA_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_CARGA", System.Data.SqlDbType.Int).Value = dat.COD_CARGA;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    XLS_CARGA_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new XLS_CARGA_BE();
                        bean.IDE_CARGA = DataReader.SafeGetInt32(dr, dr.GetOrdinal("IDE_CARGA"));
                        bean.COD_CARGA = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_CARGA"));
                        bean.FEC_CARGA = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("FEC_CARGA"));
                        bean.USERNAME = DataReader.SafeGetString(dr, dr.GetOrdinal("USERNAME"));
                        bean.ARCHIVO_FISICO = DataReader.SafeGetString(dr, dr.GetOrdinal("ARCHIVO_FISICO"));
                        bean.ARCHIVO_CARGA = DataReader.SafeGetString(dr, dr.GetOrdinal("ARCHIVO_CARGA"));
                        bean.NOM_HOJA = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_HOJA"));
                        bean.FILA_ENCABEZADO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("FILA_ENCABEZADO"));
                        bean.COLUMNA_ENCABEZADO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COLUMNA_ENCABEZADO"));
                        bean.CONTROL_01 = DataReader.SafeGetString(dr, dr.GetOrdinal("CONTROL_01"));
                        bean.CONTROL_02 = DataReader.SafeGetString(dr, dr.GetOrdinal("CONTROL_02"));
                        bean.COD_ESTADO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_ESTADO"));
                        bean.FEC_PROCESO_INICIO = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("FEC_PROCESO_INICIO"));
                        bean.FEC_PROCESO_FIN = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("FEC_PROCESO_FIN"));
                        bean.ERROR = DataReader.SafeGetString(dr, dr.GetOrdinal("ERROR"));
                        bean.PARAMETROS = DataReader.SafeGetString(dr, dr.GetOrdinal("PARAMETROS"));
                        bean.ESTADO = DataReader.SafeGetString(dr, dr.GetOrdinal("ESTADO"));
                        bean.REG_PROCESADOS = DataReader.SafeGetInt64(dr, dr.GetOrdinal("REG_PROCESADOS"));

                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
        public GEN_REPLY_BE fn_xls_upd_carga(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var dat = (XLS_CARGA_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PROCESO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_CARGA", System.Data.SqlDbType.Int).Value = dat.COD_CARGA;
            cmd.Parameters.Add("@ARCHIVO_FISICO", System.Data.SqlDbType.VarChar, 128).Value = dat.ARCHIVO_FISICO;
            cmd.Parameters.Add("@ARCHIVO_CARGA", System.Data.SqlDbType.VarChar, 128).Value = dat.ARCHIVO_CARGA;
            cmd.Parameters.Add("@NOM_HOJA", System.Data.SqlDbType.VarChar, 256).Value = dat.NOM_HOJA;
            cmd.Parameters.Add("@FILA_ENCABEZADO", System.Data.SqlDbType.VarChar, 64).Value = dat.FILA_ENCABEZADO;
            cmd.Parameters.Add("@COLUMNA_ENCABEZADO", System.Data.SqlDbType.VarChar, 64).Value = dat.COLUMNA_ENCABEZADO;
            cmd.Parameters.Add("@CONTROL_01", System.Data.SqlDbType.VarChar, 64).Value = dat.CONTROL_01;
            cmd.Parameters.Add("@CONTROL_02", System.Data.SqlDbType.VarChar, 64).Value = dat.CONTROL_02;

            cmd.CommandTimeout = 0;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                var iFilasAfectadas = cmd.ExecuteNonQuery();
                //var IDE_CARGA = cmd.ExecuteScalar();
                //dat.IDE_CARGA = Convert.ToInt64(IDE_CARGA);
                //model.DATA = dat;

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

        public GEN_REPLY_BE fn_xls_upd_proceso(XLS_PROCESO_BE param)
        {
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            Mensaje = string.Empty;
            
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = param.PROCEDIMIENTO;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@IDE_CARGA", System.Data.SqlDbType.VarChar, 32).Value = param.IDE_CARGA;

            cmd.CommandTimeout = 0;

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

        public DataTable fn_xls_pro_vistaCarga(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var dat = (XLS_CARGA_BE)model.DATA;
            DataTable dataTable = new DataTable();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_XLS_PRO_VISTACARGA]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            //cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@IDE_CARGA", System.Data.SqlDbType.Int).Value = dat.IDE_CARGA;

            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                //model.DATATABLE = dataTable;
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

            return dataTable;
        }

        public GEN_REPLY_BE fn_xls_get_menu(string parametro)
        {
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            Mensaje = string.Empty;
            List<SEG_OPCION_BE> lista = new List<SEG_OPCION_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TB_SEG_MENU WHERE PARAMETRO_MENU = @parametro";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@parametro", System.Data.SqlDbType.VarChar, 1024).Value = parametro;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_OPCION_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_OPCION_BE();
                        bean.cod_aplicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        bean.COD_MENU = DataReader.SafeGetInt64(dr, dr.GetOrdinal("COD_MENU"));
                        bean.NOM_MENU = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_MENU"));
                        bean.PARAMETRO = DataReader.SafeGetString(dr, dr.GetOrdinal("PARAMETRO_MENU"));
                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }

        public GEN_REPLY_BE fn_xls_get_dashboard(int CodReporte)
        {
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            Mensaje = string.Empty;
            List<PBI_DASHBOARD_BE> lista = new List<PBI_DASHBOARD_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TB_PBI_DASHBOARD WHERE COD_REPORTE = @CodReporte";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@CodReporte", System.Data.SqlDbType.Int).Value = CodReporte;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    PBI_DASHBOARD_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new PBI_DASHBOARD_BE();
                        bean.COD_REPORTE = DataReader.SafeGetInt32(dr, dr.GetOrdinal("COD_REPORTE"));
                        bean.NOM_REPORTE = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_REPORTE"));
                        bean.URL_PRIVADO = DataReader.SafeGetString(dr, dr.GetOrdinal("URL_PRIVADO"));
                        bean.URL_PUBLICO = DataReader.SafeGetString(dr, dr.GetOrdinal("URL_PUBLICO"));
                        lista.Add(bean);
                    }
                }
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

            model.DATA = lista;
            return model;
        }
    }
}
