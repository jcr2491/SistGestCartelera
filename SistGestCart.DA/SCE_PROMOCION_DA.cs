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
    public class SCE_PROMOCION_DA
    {
        Usuario usrLogin;

        public SCE_PROMOCION_DA()
        {
           
        }

        public SCE_PROMOCION_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }        

        public void Insertar(SCE_PROMOCION_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_PROMOCION_INS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_PROMOCION", BE.NOM_PROMOCION);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Actualizar(SCE_PROMOCION_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_PROMOCION_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", BE.ID_PROMOCION);
                        cmd.Parameters.AddWithValue("@NOM_PROMOCION", BE.NOM_PROMOCION);

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

                    string sql = "SP_SCE_PROMOCION_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", Id);
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

        public SCE_PROMOCION_BE ObtenerPorID(int Id)
        {
            SCE_PROMOCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_PROMOCION_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_PROMOCION_BE();

                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.NOM_PROMOCION = Convert.ToString(reader["NOM_PROMOCION"]);
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

        public List<SCE_PROMOCION_BE> Listar()
        {
            List<SCE_PROMOCION_BE> lstBE = new List<SCE_PROMOCION_BE>();
            SCE_PROMOCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_PROMOCION_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_PROMOCION_BE();

                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.NOM_PROMOCION = Convert.ToString(reader["NOM_PROMOCION"]);

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

        public List<SCE_PROMOCION_BE> ListarConf()
        {
            List<SCE_PROMOCION_BE> lstBE = new List<SCE_PROMOCION_BE>();
            SCE_PROMOCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_PROMOCION_SEL_CONF";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_PROMOCION_BE();

                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.NOM_PROMOCION = Convert.ToString(reader["NOM_PROMOCION"]);

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

        public string GetNombrePromocion(int IdPromocion)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_NOMBRE_PROMOCION_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.Add("@NOM_PROMOCION", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return Convert.ToString(cmd.Parameters["@NOM_PROMOCION"].Value);
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
