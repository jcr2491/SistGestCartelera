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
    public class SCE_CATEGORIA_DA
    {
        Usuario usrLogin;

        public SCE_CATEGORIA_DA()
        {
           
        }

        public SCE_CATEGORIA_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public void Insertar(SCE_CATEGORIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CATEGORIA_INS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_CATEGORIA", BE.NOM_CATEGORIA);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Actualizar(SCE_CATEGORIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CATEGORIA_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", BE.ID_CATEGORIA);
                        cmd.Parameters.AddWithValue("@NOM_CATEGORIA", BE.NOM_CATEGORIA);

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

                    string sql = "SP_SCE_CATEGORIA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", Id);
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

        public SCE_CATEGORIA_BE ObtenerPorID(int Id)
        {
            SCE_CATEGORIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CATEGORIA_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_CATEGORIA_BE();

                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);
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

        public List<SCE_CATEGORIA_BE> Listar()
        {
            List<SCE_CATEGORIA_BE> lstBE = new List<SCE_CATEGORIA_BE>();
            SCE_CATEGORIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CATEGORIA_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CATEGORIA_BE();

                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);

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

        public List<SCE_CATEGORIA_BE> ListarConf()
        {
            List<SCE_CATEGORIA_BE> lstBE = new List<SCE_CATEGORIA_BE>();
            SCE_CATEGORIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CATEGORIA_SEL_CONF";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CATEGORIA_BE();

                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);

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

        public string GetNombreCategoria(int IdCategoria)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_NOMBRE_CATEGORIA_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.Add("@NOM_CATEGORIA", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return Convert.ToString(cmd.Parameters["@NOM_CATEGORIA"].Value);
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
