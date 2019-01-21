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
    public class SCE_FILE_GUIA_MASIVA_DA
    {
        Usuario usrLogin;

        public SCE_FILE_GUIA_MASIVA_DA()
        {
           
        }

        public SCE_FILE_GUIA_MASIVA_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public List<SCE_FILE_GUIA_MASIVA_BE> CboFilesGuia()
        {
            List<SCE_FILE_GUIA_MASIVA_BE> lstBE = new List<SCE_FILE_GUIA_MASIVA_BE>();
            SCE_FILE_GUIA_MASIVA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_FILE_GUIA_MASIVA_CBO";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_FILE_GUIA_MASIVA_BE();

                            BE.ID_FILE = Convert.ToInt32(reader["ID_FILE"]);
                            BE.NOM_FILE = Convert.ToString(reader["NOM_FILE"]);

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

        public List<SCE_FILE_GUIA_MASIVA_BE> ListarFilesGuia()
        {
            List<SCE_FILE_GUIA_MASIVA_BE> lstBE = new List<SCE_FILE_GUIA_MASIVA_BE>();
            SCE_FILE_GUIA_MASIVA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_FILE_GUIA_MASIVA_LISTAR";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_FILE_GUIA_MASIVA_BE();

                            BE.ID_FILE = Convert.ToInt32(reader["ID_FILE"]);
                            BE.NOM_FILE = Convert.ToString(reader["NOM_FILE"]);
                            BE.FECHA_MOD = Convert.ToDateTime(reader["FECHA_MOD"]);
                            BE.USER_MOD = Convert.ToString(reader["USER_MOD"]);
                            BE.ESTADO = Convert.ToString(reader["ESTADO_FILE"]);

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

        public SCE_FILE_GUIA_MASIVA_BE ObtenerFileGuia(int Id)
        {
            SCE_FILE_GUIA_MASIVA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_FILE_GUIA_MASIVA_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_FILE", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_FILE_GUIA_MASIVA_BE();

                            BE.ID_FILE = Convert.ToInt32(reader["ID_FILE"]);
                            BE.NOM_FILE = Convert.ToString(reader["NOM_FILE"]);
                            BE.FECHA_MOD = Convert.ToDateTime(reader["FECHA_MOD"]);
                            BE.USER_MOD = Convert.ToString(reader["USER_MOD"]);
                            BE.ESTADO = Convert.ToString(reader["ESTADO_FILE"]);
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

        public void CargarFileGuia(SCE_FILE_GUIA_MASIVA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_FILE_GUIA_MASIVA_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_FILE", BE.NOM_FILE);
                        cmd.Parameters.AddWithValue("@USER_CRE", BE.USER_CRE);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarFileGuia(SCE_FILE_GUIA_MASIVA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_FILE_GUIA_MASIVA_UPD";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_FILE", BE.ID_FILE);
                        cmd.Parameters.AddWithValue("@NOM_FILE", BE.NOM_FILE);
                        cmd.Parameters.AddWithValue("@ESTADO", BE.ESTADO);
                        cmd.Parameters.AddWithValue("@USER_MOD", BE.USER_MOD);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarFileGuia(int Id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_FILE_GUIA_MASIVA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_FILE", Id);
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

        public void BajaFileGuia(int idFile, string User)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_FILE_GUIA_MASIVA_BAJA";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_FILE", idFile);
                        cmd.Parameters.AddWithValue("@USER_MOD", User);
                        cmd.ExecuteNonQuery();
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
