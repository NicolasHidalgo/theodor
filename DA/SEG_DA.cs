using BEANS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class SEG_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        protected List<SEG_OPCION_BE> listaDatos = null;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }

        public GEN_REPLY_BE fn_seg_login(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var user = (SEG_USUARIO_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.ACCION;
            cmd.Parameters.Add("@IDE_USUARIO", System.Data.SqlDbType.Int).Value = user.IDE_USUARIO;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = user.COD_USUARIO;
            cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 64).Value = user.PASSWORD;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 100).Value = user.NOM_USUARIO;
            cmd.Parameters.Add("@CORREO", System.Data.SqlDbType.VarChar, 128).Value = user.CORREO_ELECTRONICO;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        user.IDE_USUARIO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        user.COD_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        user.NOM_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        user.EST_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        user.CORREO_ELECTRONICO = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                    }
                    model.DATA = user;
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

        public GEN_REPLY_BE fn_seg_acceso(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var user = (SEG_USUARIO_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menu]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = user.COD_USUARIO;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 50).Value = model.COD_APLICACION;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = model.SESSION;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = model.HOSTNAME;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.ACCION;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = model.COD_MENU;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = model.CONTROLLER;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 100).Value = user.NOM_USUARIO;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        user.IDE_USUARIO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        user.COD_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        user.NOM_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        user.EST_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        user.CORREO_ELECTRONICO = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        user.DIRECCION_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("direccion_usuario"));
                        user.COD_APLICACION = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        user.SUSCRIPTOR = DataReader.SafeGetInt64(dr, dr.GetOrdinal("suscriptor"));
                        user.IMG_SUSCRIPTOR = DataReader.SafeGetString(dr, dr.GetOrdinal("img_suscriptor"));
                    }
                    model.DATA = user;
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

        public List<SEG_APLICACION_BE> fn_seg_sel_usu_app(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var user = (SEG_USUARIO_BE)model.DATA;
            List<SEG_APLICACION_BE> lista = new List<SEG_APLICACION_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menu]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 50).Value = model.COD_APLICACION;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = model.SESSION;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = model.HOSTNAME;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.ACCION;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = model.COD_MENU;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = model.CONTROLLER;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 100).Value = user.NOM_USUARIO;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_APLICACION_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_APLICACION_BE();
                        bean.COD_APLICACION = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        bean.NOM_APLICACION = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_aplicacion"));
                        bean.ICONO_APLICACION = DataReader.SafeGetString(dr, dr.GetOrdinal("icono_aplicacion"));
                        //bean.APP_DEFAULT = DataReader.SafeGetBoolean(dr, dr.GetOrdinal("APP_DEFAULT"));
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

            return lista;
        }

        public List<SEG_OPCION_BE> fn_seg_sel_menu(GEN_REPLY_BE model)
        {
            List<SEG_OPCION_BE> lista = new List<SEG_OPCION_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menu]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 50).Value = model.COD_APLICACION;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = model.SESSION;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = model.HOSTNAME;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = model.ACCION;
            cmd.Parameters.Add("@cod_menu", System.Data.SqlDbType.Int).Value = model.COD_MENU;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = model.CONTROLLER;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 100).Value = "";

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
                        bean.COD_MENU = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_menu"));
                        bean.COD_MENU_PADRE = DataReader.SafeGetInt32(dr, dr.GetOrdinal("cod_menu_padre"));
                        bean.NOM_MENU = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_menu"));
                        //bean.NAVEGACION_URL = DataReader.SafeGetString(dr, dr.GetOrdinal("navegacion_url"));
                        //bean.TARGET_MENU = DataReader.SafeGetString(dr, dr.GetOrdinal("target_menu"));
                        bean.ACTION = DataReader.SafeGetString(dr, dr.GetOrdinal("action"));
                        bean.CONTROLLER = DataReader.SafeGetString(dr, dr.GetOrdinal("controller"));
                        bean.ICONO = DataReader.SafeGetString(dr, dr.GetOrdinal("icono"));
                        bean.TIP_MENU = DataReader.SafeGetString(dr, dr.GetOrdinal("tip_menu"));
                        bean.PARAMETRO = DataReader.SafeGetString(dr, dr.GetOrdinal("parametro"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        
        public List<SEG_USUARIO_BE> fn_seg_sel_usuario(GEN_REPLY_BE model)
        {
            List<SEG_USUARIO_BE> lista = new List<SEG_USUARIO_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_SEG_USUARIO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;

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
                        bean.IDE_USUARIO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("IDE_USUARIO"));
                        bean.COD_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("COD_USUARIO"));
                        bean.NOM_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_USUARIO"));
                        bean.EST_USUARIO = DataReader.SafeGetString(dr, dr.GetOrdinal("EST_USUARIO"));
                        bean.CORREO_ELECTRONICO = DataReader.SafeGetString(dr, dr.GetOrdinal("CORREO"));
                        bean.FEC_CREACION = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("FEC_CREACION"));
                        bean.FEC_CESE = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("FEC_CESE"));
                        bean.PASSWORD = DataReader.SafeGetString(dr, dr.GetOrdinal("PASSWORD"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        
        
        public GEN_REPLY_BE fn_seg_upd_usuario(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var user = (SEG_USUARIO_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_SEG_USUARIO]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@IDE_USUARIO", System.Data.SqlDbType.Int).Value = user.IDE_USUARIO;
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 64).Value = user.COD_USUARIO;
            cmd.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 64).Value = user.PASSWORD;
            cmd.Parameters.Add("@NOM_USUARIO", System.Data.SqlDbType.VarChar, 128).Value = user.NOM_USUARIO;
            cmd.Parameters.Add("@CORREO", System.Data.SqlDbType.VarChar, 128).Value = user.CORREO_ELECTRONICO;

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
        

        public GEN_REPLY_BE fn_seg_sel_app_perfil(GEN_REPLY_BE model)
        {
            List<SEG_PERFIL_BE> lista = new List<SEG_PERFIL_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_SEG_PERMISOS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@COD_APLICACION", System.Data.SqlDbType.VarChar, 5).Value = model.COD_APLICACION;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_PERFIL_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_PERFIL_BE();
                        bean.ID_PERFIL = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ID_PERFIL"));
                        bean.COD_APLICACION = DataReader.SafeGetString(dr, dr.GetOrdinal("COD_APLICACION"));
                        bean.NOM_PERFIL = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_PERFIL"));
                        bean.NOM_CORTO = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_CORTO"));
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

        public GEN_REPLY_BE fn_seg_upd_permisos(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var per = (SEG_USUARIO_PERFIL_BE)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_SEG_PERMISOS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = "START_PERMISO";
            cmd.Parameters.Add("@ID_USUARIO", System.Data.SqlDbType.Int).Value = per.ID_USUARIO;
            cmd.Parameters.Add("@COD_APLICACION", System.Data.SqlDbType.VarChar, 5).Value = per.COD_APLICACION;
            cmd.Parameters.Add("@ID_PERFIL", System.Data.SqlDbType.Int).Value = 0;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                var iFilasAfectadas = cmd.ExecuteNonQuery();

                foreach (var item in per.Perfiles)
                {
                    cmd.Parameters["@ACCION"].Value = "INSERT_PERMISO";
                    cmd.Parameters["@ID_PERFIL"].Value = item.ID_PERFIL;
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

        public GEN_REPLY_BE fn_seg_upd_session(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var dat = (List<SEG_SESSION_BE>)model.DATA;

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_session]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = "CLEAR_SESSION";
            cmd.Parameters.Add("@COD_USUARIO", System.Data.SqlDbType.VarChar, 64).Value = model.COD_USUARIO;
            cmd.Parameters.Add("@COD_APLICACION", System.Data.SqlDbType.VarChar, 5).Value = model.COD_APLICACION;
            cmd.Parameters.Add("@COD_MENU", System.Data.SqlDbType.BigInt).Value = 0;
            cmd.Parameters.Add("@COD_SESSION", System.Data.SqlDbType.VarChar, 100).Value = "";
            cmd.Parameters.Add("@COD_PERFIL", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@COD_ROL", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@NIV_MENU", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@URL_MENU", System.Data.SqlDbType.VarChar, 300).Value = "";
            cmd.Parameters.Add("@PERMISO", System.Data.SqlDbType.Bit).Value = 0;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                var iFilasAfectadas = cmd.ExecuteNonQuery();

                foreach (var item in dat)
                {
                    cmd.Parameters["@ACCION"].Value = "SESSION_MENU";
                    cmd.Parameters["@COD_MENU"].Value = item.iCodMenu;
                    cmd.Parameters["@COD_SESSION"].Value = item.COD_SESSION;
                    cmd.Parameters["@COD_PERFIL"].Value = item.codPer;
                    cmd.Parameters["@COD_ROL"].Value = item.codRol;
                    cmd.Parameters["@NIV_MENU"].Value = item.siNivMenu;
                    cmd.Parameters["@URL_MENU"].Value = item.vUrlMenu;
                    cmd.Parameters["@PERMISO"].Value = item.bPermiso;
                    iFilasAfectadas = cmd.ExecuteNonQuery();
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

        public GEN_REPLY_BE fn_seg_sel_permisos(GEN_REPLY_BE model)
        {
            Mensaje = string.Empty;
            var mod = (SEG_USUARIO_PERFIL_BE)model.DATA;
            List<SEG_USUARIO_PERFIL_BE> lista = new List<SEG_USUARIO_PERFIL_BE>();

            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[UP_SEG_PERMISOS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 32).Value = model.ACCION;
            cmd.Parameters.Add("@ID_USUARIO", System.Data.SqlDbType.Int).Value = mod.ID_USUARIO;
            cmd.Parameters.Add("@COD_APLICACION", System.Data.SqlDbType.VarChar, 5).Value = mod.COD_APLICACION;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_USUARIO_PERFIL_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new SEG_USUARIO_PERFIL_BE();
                        bean.ID_USUARIO = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ID_USUARIO"));
                        bean.ID_PERFIL  = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ID_PERFIL"));
                        bean.NOM_PERFIL = DataReader.SafeGetString(dr, dr.GetOrdinal("NOM_PERFIL"));
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

        /*
        public List<SEG_MENU_BE> fn_seg_aplicaciones(string cod_usuario, int idOpcion, string accion)
        {
            List<SEG_MENU_BE> lista = new List<SEG_MENU_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_pro_menuMvc]";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = "";
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = null;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@ID_OPCION", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_MENU_BE bean = null;

                    int i_cod_aplicacion = dr.GetOrdinal("cod_aplicacion");
                    int i_nom_aplicacion = dr.GetOrdinal("nom_aplicacion");
                    int i_icono_aplicacion = dr.GetOrdinal("icono_aplicacion");

                    while (dr.Read())
                    {
                        bean = new SEG_MENU_BE();
                        bean.cod_aplicacion = DataReader.SafeGetString(dr, i_cod_aplicacion);
                        bean.nom_aplicacion = DataReader.SafeGetString(dr, i_nom_aplicacion);
                        bean.icono = DataReader.SafeGetString(dr, i_icono_aplicacion);
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        
        public Tuple<SEG_USUARIO_BE, GEN_MENSAJE_BE> up_seg_pro_usuario(string cod_usuario, string cod_aplicacion, string accion, string hostname, string session)
        {
            SEG_USUARIO_BE usuarioBean = null;
            mensajeBean = new GEN_MENSAJE_BE();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "up_seg_pro_menuMvc";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 5).Value = cod_aplicacion;
            cmd.Parameters.Add("@session", System.Data.SqlDbType.VarChar, 30).Value = session;
            cmd.Parameters.Add("@hostname", System.Data.SqlDbType.VarChar, 200).Value = hostname;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 50).Value = accion;
            cmd.Parameters.Add("@ID_OPCION", System.Data.SqlDbType.Int, 0).Value = 0;
            cmd.Parameters.Add("@controller", System.Data.SqlDbType.VarChar, 50).Value = "";

            try
            {

                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        usuarioBean = new SEG_USUARIO_BE();
                        usuarioBean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        usuarioBean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        usuarioBean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        usuarioBean.est_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        usuarioBean.correo_electronico = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        usuarioBean.direccion_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("direccion_usuario"));
                        usuarioBean.cod_aplicacion = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_aplicacion"));
                        usuarioBean.cod_unidad_negocio = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_unidad_negocio"));
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeBean.mensaje += ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            if (mensajeBean.mensaje != null)
            {
                mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
                if (mensajeBean.tipo != "ERROR")
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return Tuple.Create(usuarioBean, mensajeBean);
        }
        
        public List<SEG_APLICACION_BE> fn_seg_listAplicacion(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<SEG_APLICACION_BE> lista = new List<SEG_APLICACION_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_APLICACION_BE bean = null;

                    int i_cod_aplicacion = dr.GetOrdinal("cod_aplicacion");
                    int i_nom_aplicacion = dr.GetOrdinal("nom_aplicacion");

                    while (dr.Read())
                    {
                        bean = new SEG_APLICACION_BE
                        {
                            cod_aplicacion = DataReader.SafeGetString(dr, i_cod_aplicacion),
                            nom_aplicacion = DataReader.SafeGetString(dr, i_nom_aplicacion)
                        };
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<GEN_UNIDAD_NEGOCIO_BE> fn_seg_listUnidad(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<GEN_UNIDAD_NEGOCIO_BE> lista = new List<GEN_UNIDAD_NEGOCIO_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GEN_UNIDAD_NEGOCIO_BE bean = null;

                    int i_cod_unidad_negocio = dr.GetOrdinal("cod_unidad_negocio");
                    int i_nom_unidad_negocio = dr.GetOrdinal("nom_unidad_negocio");

                    while (dr.Read())
                    {
                        bean = new GEN_UNIDAD_NEGOCIO_BE
                        {
                            cod_unidad_negocio = DataReader.SafeGetString(dr, i_cod_unidad_negocio),
                            nom_unidad_negocio = DataReader.SafeGetString(dr, i_nom_unidad_negocio)
                        };
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<SEG_PERFIL_BE> fn_seg_listPerfil(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil)
        {
            List<SEG_PERFIL_BE> lista = new List<SEG_PERFIL_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    SEG_PERFIL_BE bean = null;

                    int i_cod_perfil = dr.GetOrdinal("cod_perfil");
                    int i_nom_perfil = dr.GetOrdinal("nom_perfil");

                    while (dr.Read())
                    {
                        bean = new SEG_PERFIL_BE
                        {
                            cod_perfil = DataReader.SafeGetInt32(dr, i_cod_perfil),
                            nom_perfil = DataReader.SafeGetString(dr, i_nom_perfil)
                        };
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public GEN_MENSAJE_BE updatePermiso(string accion, string cod_usuario, string cod_aplicacion, string cod_unidad_negocio, string cod_perfil, string cod_usuario_accion)
        {
            mensajeBean = new GEN_MENSAJE_BE();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_perfil_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 20).Value = accion;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;
            cmd.Parameters.Add("@cod_unidad_negocio", System.Data.SqlDbType.VarChar, 3).Value = cod_unidad_negocio;
            cmd.Parameters.Add("@cod_perfil", System.Data.SqlDbType.VarChar, 1024).Value = cod_perfil;
            cmd.Parameters.Add("@cod_usuario_accion", System.Data.SqlDbType.VarChar, 50).Value = cod_usuario_accion;

            try
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                con.FireInfoMessageEventOnUserErrors = true;
                con.Open();
                mensajeBean.iFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensajeBean.mensaje += ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            if (mensajeBean.mensaje != null)
            {
                mensajeBean.tipo = Util.GetTypeMessage(mensajeBean.mensaje);
                if (mensajeBean.tipo != "ERROR")
                    mensajeBean.mensaje = mensajeBean.mensaje.Replace("\n", "<br />");
            }

            return mensajeBean;
        }
        public List<SEG_USUARIO_BE> fn_seg_listUsuario(string accion)
        {
            List<SEG_USUARIO_BE> lista = new List<SEG_USUARIO_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_cud_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", System.Data.SqlDbType.VarChar, 10).Value = accion;
            cmd.Parameters.Add("@ide_usuario", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@cod_usuario", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@nom_usuario", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@cod_personal", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            cmd.Parameters.Add("@est_usuario", System.Data.SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@correo_electronico", System.Data.SqlDbType.VarChar, 50).Value = string.Empty;
            //cmd.Parameters.Add("@fec_creacion", System.Data.SqlDbType.DateTime).Value = DateTime.;
            //cmd.Parameters.Add("@fec_cese", System.Data.SqlDbType.DateTime).Value = null;

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
                        bean.ide_usuario = DataReader.SafeGetInt32(dr, dr.GetOrdinal("ide_usuario"));
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.correo_electronico = DataReader.SafeGetString(dr, dr.GetOrdinal("correo_electronico"));
                        bean.fec_creacion = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_creacion"));
                        bean.fec_cese = DataReader.GetValueOrNull<DateTime>(dr, dr.GetOrdinal("fec_cese"));
                        bean.est_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("est_usuario"));
                        lista.Add(bean);

                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        public List<SEG_USUARIO_BE> fn_seg_sel_usuario(string nombre, string cod_aplicacion)
        {
            List<SEG_USUARIO_BE> lista = new List<SEG_USUARIO_BE>();
            String mensaje = "";
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[up_seg_sel_usuario]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 1024).Value = nombre;
            cmd.Parameters.Add("@cod_aplicacion", System.Data.SqlDbType.VarChar, 3).Value = cod_aplicacion;

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
                        bean.cod_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("cod_usuario"));
                        bean.nom_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("nom_usuario"));
                        bean.des_usuario = DataReader.SafeGetString(dr, dr.GetOrdinal("des_usuario"));
                        lista.Add(bean);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return lista;
        }
        */

    }
}
