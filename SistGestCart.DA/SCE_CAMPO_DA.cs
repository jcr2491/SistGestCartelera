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
    public class SCE_CAMPO_DA
    {
        Usuario usrLogin;

        public SCE_CAMPO_DA()
        {
           
        }

        public SCE_CAMPO_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public bool EsCampoValidable(int IdCartel, int IdCampo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_ES_CAMPO_VALIDABLE";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_CAMPO", IdCampo);                        
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

        public void Insertar(SCE_CAMPO_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_INS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_CAMPO", BE.NOM_CAMPO);
                        cmd.Parameters.AddWithValue("@ALIAS", BE.ALIAS);
                        cmd.Parameters.AddWithValue("@RESTRINGIR", BE.RESTINGIR);
                        cmd.Parameters.AddWithValue("@VALDIGITOS", BE.VALDIGITOS);
                        cmd.Parameters.AddWithValue("@TIPO", BE.TIPO);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }        
        }

        public void Actualizar(SCE_CAMPO_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CAMPO", BE.ID_CAMPO);
                        cmd.Parameters.AddWithValue("@NOM_CAMPO", BE.NOM_CAMPO);
                        cmd.Parameters.AddWithValue("@ALIAS", BE.ALIAS);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Eliminar(int Id, string Alias)
        {
            int Rspta;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CAMPO", Id);
                        cmd.Parameters.AddWithValue("@ALIAS", Alias);
                        cmd.Parameters.Add("@RSPTA", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        Rspta = Convert.ToInt32(cmd.Parameters["@RSPTA"].Value);
                    }
                }

                return Rspta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_CAMPO_BE ObtenerPorID(int Id)
        {
            SCE_CAMPO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CAMPO", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_CAMPO_BE();

                            BE.ID_CAMPO = Convert.ToInt32(reader["ID_CAMPO"]);
                            BE.ALIAS = Convert.ToString(reader["ALIAS"]);
                            BE.NOM_CAMPO = Convert.ToString(reader["NOM_CAMPO"]);                           
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

        public List<SCE_CAMPO_BE> Listar()
        {
            List<SCE_CAMPO_BE> lstBE = new List<SCE_CAMPO_BE>();
            SCE_CAMPO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CAMPO_BE();

                            BE.ID_CAMPO = Convert.ToInt32(reader["ID_CAMPO"]);
                            BE.ALIAS = Convert.ToString(reader["ALIAS"]);
                            BE.NOM_CAMPO = Convert.ToString(reader["NOM_CAMPO"]);                           
                            BE.FLAGPERTENECE = Convert.ToInt32(reader["FLAGPERTENECE"]);
                            BE.TIPO = Convert.ToString(reader["TIPO"]);
                            BE.RESTINGIR = Convert.ToString(reader["RESTINGIR"]);
                            BE.VALDIGITOS = Convert.ToString(reader["VALIDAR"]);
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

        public List<SCE_CAMPO_BE> Listar1()
        {
            List<SCE_CAMPO_BE> lstBE = new List<SCE_CAMPO_BE>();
            SCE_CAMPO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_SEL2";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CAMPO_BE();

                            BE.ID_CAMPO = Convert.ToInt32(reader["ID_CAMPO"]);
                            BE.ALIAS = Convert.ToString(reader["ALIAS"]);
                            BE.NOM_CAMPO = Convert.ToString(reader["NOM_CAMPO"]);
                            BE.RESTINGIR = Convert.ToString(reader["RESTINGIR"]);
                            BE.VALDIGITOS = Convert.ToString(reader["VALIDAR"]);

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

        public int GetIdCampo(string NomCampo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_OBTENER_IDCAMPO";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ALIAS", NomCampo);
                        cmd.Parameters.Add("@IDCAMPO", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return Convert.ToInt32(cmd.Parameters["@IDCAMPO"].Value);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAllCampos()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CAMPO_SEL1";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        dt.Load(reader);

                        return dt;
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
