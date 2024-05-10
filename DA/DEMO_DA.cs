using BEANS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class DEMO_DA
    {
        GEN_Conexion cn = new GEN_Conexion();
        static string Mensaje = string.Empty;
        static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            Mensaje += e.Message + "\n";
        }
        public List<DEMO_BE> fn_sel_mov(DEMO_BE param)
        {
            List<DEMO_BE> lista = new List<DEMO_BE>();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[SP_MOVIMIENTOS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 50).Value = param.ACCION;
            cmd.Parameters.Add("@FEC_INI", System.Data.SqlDbType.VarChar, 50).Value = param.FEC_INI;
            cmd.Parameters.Add("@FEC_FIN", System.Data.SqlDbType.VarChar, 50).Value = param.FEC_FIN;
            cmd.Parameters.Add("@COD_CIA", System.Data.SqlDbType.VarChar, 50).Value = param.COD_CIA;
            cmd.Parameters.Add("@COMPANIA_VENTA_3", System.Data.SqlDbType.VarChar, 50).Value = param.COMPANIA_VENTA_3;
            cmd.Parameters.Add("@ALMACEN_VENTA", System.Data.SqlDbType.VarChar, 50).Value = param.ALMACEN_VENTA;
            cmd.Parameters.Add("@TIPO_MOVIMIENTO", System.Data.SqlDbType.VarChar, 50).Value = param.TIPO_MOVIMIENTO;
            cmd.Parameters.Add("@TIPO_DOCUMENTO", System.Data.SqlDbType.VarChar, 50).Value = param.TIPO_DOCUMENTO;
            cmd.Parameters.Add("@NRO_DOCUMENTO", System.Data.SqlDbType.VarChar, 50).Value = param.NRO_DOCUMENTO;
            cmd.Parameters.Add("@COD_ITEM_2", System.Data.SqlDbType.VarChar, 50).Value = param.COD_ITEM_2;
            cmd.Parameters.Add("@CANTIDAD", System.Data.SqlDbType.VarChar, 50).Value = param.CANTIDAD;
            cmd.Parameters.Add("@PROVEEDOR", System.Data.SqlDbType.VarChar, 50).Value = param.PROVEEDOR;
            cmd.Parameters.Add("@MONEDA", System.Data.SqlDbType.VarChar, 50).Value = param.MONEDA;


            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    DEMO_BE bean = null;
                    while (dr.Read())
                    {
                        bean = new DEMO_BE();
                        bean.COD_CIA = DataReader.SafeGetString(dr, dr.GetOrdinal("COD_CIA"));
                        bean.COMPANIA_VENTA_3 = DataReader.SafeGetString(dr, dr.GetOrdinal("COMPANIA_VENTA_3"));
                        bean.ALMACEN_VENTA = DataReader.SafeGetString(dr, dr.GetOrdinal("ALMACEN_VENTA"));
                        bean.TIPO_MOVIMIENTO = DataReader.SafeGetString(dr, dr.GetOrdinal("TIPO_MOVIMIENTO"));
                        bean.NRO_DOCUMENTO = DataReader.SafeGetString(dr, dr.GetOrdinal("NRO_DOCUMENTO"));
                        bean.COD_ITEM_2 = DataReader.SafeGetString(dr, dr.GetOrdinal("COD_ITEM_2"));
                        bean.FECHA_TRANSACCION = DataReader.SafeGetString(dr, dr.GetOrdinal("FECHA_TRANSACCION"));
                        bean.TIPO_DOCUMENTO = DataReader.SafeGetString(dr, dr.GetOrdinal("TIPO_DOCUMENTO"));
                        bean.CANTIDAD = DataReader.SafeGetString(dr, dr.GetOrdinal("CANTIDAD"));
                        bean.PROVEEDOR = DataReader.SafeGetString(dr, dr.GetOrdinal("PROVEEDOR"));
                        bean.MONEDA = DataReader.SafeGetString(dr, dr.GetOrdinal("MONEDA"));

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

        public GEN_REPLY_BE fn_pro_mov(DEMO_BE param)
        {
            Mensaje = string.Empty;
            GEN_REPLY_BE model = new GEN_REPLY_BE();
            SqlConnection con = cn.getConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[SP_MOVIMIENTOS]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 50).Value = param.ACCION;
            cmd.Parameters.Add("@FEC_INI", System.Data.SqlDbType.VarChar, 50).Value = param.FEC_INI;
            cmd.Parameters.Add("@FEC_FIN", System.Data.SqlDbType.VarChar, 50).Value = param.FEC_FIN;
            cmd.Parameters.Add("@COD_CIA", System.Data.SqlDbType.VarChar, 50).Value = param.COD_CIA;
            cmd.Parameters.Add("@COMPANIA_VENTA_3", System.Data.SqlDbType.VarChar, 50).Value = param.COMPANIA_VENTA_3;
            cmd.Parameters.Add("@ALMACEN_VENTA", System.Data.SqlDbType.VarChar, 50).Value = param.ALMACEN_VENTA;
            cmd.Parameters.Add("@TIPO_MOVIMIENTO", System.Data.SqlDbType.VarChar, 50).Value = param.TIPO_MOVIMIENTO;
            cmd.Parameters.Add("@TIPO_DOCUMENTO", System.Data.SqlDbType.VarChar, 50).Value = param.TIPO_DOCUMENTO;
            cmd.Parameters.Add("@NRO_DOCUMENTO", System.Data.SqlDbType.VarChar, 50).Value = param.NRO_DOCUMENTO;
            cmd.Parameters.Add("@COD_ITEM_2", System.Data.SqlDbType.VarChar, 50).Value = param.COD_ITEM_2;
            cmd.Parameters.Add("@CANTIDAD", System.Data.SqlDbType.VarChar, 50).Value = param.CANTIDAD;
            cmd.Parameters.Add("@PROVEEDOR", System.Data.SqlDbType.VarChar, 50).Value = param.PROVEEDOR;
            cmd.Parameters.Add("@MONEDA", System.Data.SqlDbType.VarChar, 50).Value = param.MONEDA;

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
