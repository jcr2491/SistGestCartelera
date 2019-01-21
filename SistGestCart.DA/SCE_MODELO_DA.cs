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
    public class SCE_MODELO_DA
    {
        Usuario usrLogin;

        public SCE_MODELO_DA()
        {
           
        }

        public SCE_MODELO_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }       

        public void Insertar(SCE_MODELO_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_MODELO_INS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_MODELO", BE.NOM_MODELO);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Actualizar(SCE_MODELO_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_MODELO_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                        cmd.Parameters.AddWithValue("@NOM_MODELO", BE.NOM_MODELO);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Eliminar(int Id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_MODELO_DEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_MODELO", Id);                        

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SCE_MODELO_BE ObtenerPorID(int Id)
        {
            SCE_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_MODELO_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_MODELO", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_MODELO_BE();

                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.NOM_MODELO = Convert.ToString(reader["NOM_MODELO"]);
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

        public List<SCE_MODELO_BE> Listar()
        {
            List<SCE_MODELO_BE> lstBE = new List<SCE_MODELO_BE>();
            SCE_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_MODELO_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_MODELO_BE();

                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.NOM_MODELO = Convert.ToString(reader["NOM_MODELO"]);
                            BE.FLAGPERTENECE = Convert.ToInt32(reader["FLAGPERTENECE"]);

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
