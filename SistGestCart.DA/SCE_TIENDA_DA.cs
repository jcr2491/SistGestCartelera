using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistGestCart.BE;
using pe.oechsle.Entity;

namespace SistGestCart.DA
{
    public class SCE_TIENDA_DA
    {
        Usuario usrLogin;

        public SCE_TIENDA_DA()
        {
           
        }

        public SCE_TIENDA_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public void Insertar(SCE_TIENDA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_TIENDA_INS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
                        cmd.Parameters.AddWithValue("@NOM_TIENDA", BE.NOM_TIENDA);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(SCE_TIENDA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_TIENDA_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
                        cmd.Parameters.AddWithValue("@NOM_TIENDA", BE.NOM_TIENDA);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(int Id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_TIENDA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_TIENDA", Id);
                        cmd.Parameters.Add("@RSPTA", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Rspta = Convert.ToBoolean(cmd.Parameters["@RSPTA"].Value);

                        if (Rspta == false)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_TIENDA_BE ObtenerPorID(int Id)
        {
            SCE_TIENDA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_TIENDA_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_TIENDA", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_TIENDA_BE();

                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.NOM_TIENDA = Convert.ToString(reader["NOM_TIENDA"]);
                        }

                        return BE;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_TIENDA_BE> Listar()
        {
            List<SCE_TIENDA_BE> lstBE = new List<SCE_TIENDA_BE>();
            SCE_TIENDA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_TIENDA_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_TIENDA_BE();

                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.NOM_TIENDA = Convert.ToString(reader["NOM_TIENDA"]);

                            lstBE.Add(BE);
                        }

                        return lstBE;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        
    }
}
