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
    public class SCE_GRUPOTDA_DA
    {
        Usuario usrLogin;

        public SCE_GRUPOTDA_DA()
        {
           
        }

        public SCE_GRUPOTDA_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }        

        public void Insertar(SCE_GRUPOTDA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGrupo = "SP_SCE_GRUPOTDA_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGrupo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOM_GRUPO", BE.NOM_GRUPO);
                        cmd.Parameters.Add("@ID_GRUPO", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        BE.ID_GRUPO = (int)cmd.Parameters["@ID_GRUPO"].Value;  
                    }

                    string sqlGrupoTienda = "SP_SCE_GRUPOTDA_TIENDA_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGrupoTienda, cn))
                    {

                        foreach (SCE_GRUPOTDA_TIENDA_BE GRUPOTDA_TIENDA_BE in BE.TIENDAS)
                        {
                            cmd.Parameters.Clear();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                            cmd.Parameters.AddWithValue("@ID_TIENDA", GRUPOTDA_TIENDA_BE.ID_TIENDA);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Actualizar(SCE_GRUPOTDA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GRUPOTDA_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                        cmd.Parameters.AddWithValue("@NOM_GRUPO", BE.NOM_GRUPO);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlGrupoTienda1 = "SP_SCE_GRUPOTDA_TIENDA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlGrupoTienda1, cn))
                    {
                        cmd.Parameters.Clear();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlGrupoTienda2 = "SP_SCE_GRUPOTDA_TIENDA_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGrupoTienda2, cn))
                    {
                        foreach (SCE_GRUPOTDA_TIENDA_BE GRUPOTDA_TIENDA_BE in BE.TIENDAS)
                        {
                            cmd.Parameters.Clear();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                            cmd.Parameters.AddWithValue("@ID_TIENDA", GRUPOTDA_TIENDA_BE.ID_TIENDA);
                            cmd.ExecuteNonQuery();
                        }
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

                    string sqlGrupo = "SP_SCE_GRUPOTDA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlGrupo, cn))
                    {
                        cmd.Parameters.Clear();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GRUPO", Id);
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

        public SCE_GRUPOTDA_BE ObtenerPorID(int Id)
        {
            SCE_GRUPOTDA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GRUPOTDA_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GRUPO", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_GRUPOTDA_BE();

                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.NOM_GRUPO = Convert.ToString(reader["NOM_GRUPO"]);

                            /* LLENA LA LISTA DE TIENDAS DEL GRUPO */
                            List<SCE_GRUPOTDA_TIENDA_BE> lstTIENDAS = new List<SCE_GRUPOTDA_TIENDA_BE>();

                            SqlDataReader drTIENDAS = null;
                            drTIENDAS = getTiendas(Id);                            

                            while (drTIENDAS.Read())
                            {
                                SCE_GRUPOTDA_TIENDA_BE TIENDA = new SCE_GRUPOTDA_TIENDA_BE();
                                TIENDA.ID_GRUPO = drTIENDAS.GetInt32(0);
                                TIENDA.ID_TIENDA = drTIENDAS.GetInt32(1);
                                TIENDA.NOM_TIENDA = drTIENDAS.GetString(2);                                
                                lstTIENDAS.Add(TIENDA);
                            }

                            BE.TIENDAS = lstTIENDAS;
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

        public SqlDataReader getTiendas(int IdGrupo)
        {
            string sql = "SP_SCE_GRUPOTDA_TIENDA_GET";
            SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin));
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_GRUPO", IdGrupo);
            cmd.CommandText = sql;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }       

        public List<SCE_GRUPOTDA_BE> Listar()
        {
            List<SCE_GRUPOTDA_BE> lstBE = new List<SCE_GRUPOTDA_BE>();
            SCE_GRUPOTDA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GRUPOTDA_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GRUPOTDA_BE();

                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.NOM_GRUPO = Convert.ToString(reader["NOM_GRUPO"]);

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
