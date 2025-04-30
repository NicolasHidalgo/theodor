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

    }
}
