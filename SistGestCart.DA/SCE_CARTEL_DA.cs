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
    public class SCE_CARTEL_DA
    {
        Usuario usrLogin;

        public SCE_CARTEL_DA()
        {
           
        }

        public SCE_CARTEL_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public void Insertar(SCE_CARTEL_BE BE, int numMaxDigitos, string CeroDigitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NOM_CARTEL", BE.NOM_CARTEL);
                        cmd.Parameters.AddWithValue("@CERO_DIGITOS", CeroDigitos);
                        cmd.Parameters.Add("@ID_CARTEL", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        BE.ID_CARTEL = (int)cmd.Parameters["@ID_CARTEL"].Value;                       
                    }

                    string sqlCartelModeloDigito = "SP_SCE_CARTEL_MODELO_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModeloDigito, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE in BE.MODELOS)
                        {
                            if (CeroDigitos == "S")
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                cmd.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                                cmd.Parameters.AddWithValue("@DIGITOS", 0);
                                cmd.ExecuteNonQuery();
                            }
                            else if (CeroDigitos == "N")
                            {
                                for (int i = 1; i <= numMaxDigitos; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                    cmd.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                                    cmd.Parameters.AddWithValue("@DIGITOS", i);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizar(SCE_CARTEL_BE BE, int numMaxDigitos, string CeroDigitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartelModelo = "SP_SCE_CARTEL_UPD";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                        cmd.Parameters.AddWithValue("@NOM_CARTEL", BE.NOM_CARTEL);
                        cmd.Parameters.AddWithValue("@CERO_DIGITOS", CeroDigitos);
                        cmd.ExecuteNonQuery();
                    }
                    
                    //Evaluamos si existe en registro en la tabla y su flag si es 1 o 0
                    string sqlCartelModelo1 = "SP_SCE_CARTEL_MODELO_EXISTE";               

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo1, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_BE CARTEL_MODELO_BE in BE.MODELOS)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                            cmd.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                            cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            bool Existe = Convert.ToBoolean(cmd.Parameters["@EXISTE"].Value);

                            if (Existe == true && CARTEL_MODELO_BE.FLAGPERTENECE == 1)
                            { 
                                //No pasa nada porque no se ha seleccionado el registro y ya
                                //estaba seleccionado previamente
                            }
                            else if (Existe == true && CARTEL_MODELO_BE.FLAGPERTENECE == 0)
                            {
                                //Elimino el registros que he deseleccionado teniendo 
                                //en cuenta que ya se valido si este registro no tiene 
                                //registros relacionados
                                string sqlCartelModelo2 = "SP_SCE_CARTEL_MODELO_DEL1";

                                using (SqlCommand cmd1 = new SqlCommand(sqlCartelModelo2, cn))
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;

                                    cmd1.Parameters.Clear();
                                    cmd1.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                    cmd1.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                                    cmd1.ExecuteNonQuery();
                                }                               

                            }
                            else if (Existe == false && CARTEL_MODELO_BE.FLAGPERTENECE == 1)
                            {
                                // Inserto los digitos del nuevo cartel-modelo
                                string sqlCartelModeloDigito2 = "SP_SCE_CARTEL_MODELO_INS";

                                using (SqlCommand cmd1 = new SqlCommand(sqlCartelModeloDigito2, cn))
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;

                                    if (CeroDigitos == "S")
                                    {
                                        cmd1.Parameters.Clear();
                                        cmd1.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                        cmd1.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                                        cmd1.Parameters.AddWithValue("@DIGITOS", 0);
                                        cmd1.ExecuteNonQuery();
                                    }
                                    else if (CeroDigitos == "N")
                                    {
                                        for (int i = 1; i <= numMaxDigitos; i++)
                                        {
                                            cmd1.Parameters.Clear();
                                            cmd1.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                            cmd1.Parameters.AddWithValue("@ID_MODELO", CARTEL_MODELO_BE.ID_MODELO);
                                            cmd1.Parameters.AddWithValue("@DIGITOS", i);
                                            cmd1.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else if (Existe == false && CARTEL_MODELO_BE.FLAGPERTENECE == 0)
                            {
                                //No pasa nada ya que el registro de la matriz no esta en
                                //la tabla
                            }
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

                    string sqlCartel = "SP_SCE_CARTEL_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", Id);
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

        public SCE_CARTEL_BE ObtenerPorID(int Id)
        {
            SCE_CARTEL_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        cmd.Parameters.AddWithValue("@ID_CARTEL", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_CARTEL_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.NOM_CARTEL = Convert.ToString(reader["NOM_CARTEL"]);

                            if (Convert.ToString(reader["CERO_DIGITOS"]) == "S")
                            {
                                BE.CERO_DIGITOS = 1;
                            }
                            else if (Convert.ToString(reader["CERO_DIGITOS"]) == "N")
                            {
                                BE.CERO_DIGITOS = 0;
                            }

                            /* LLENA LA LISTA DE MODELOS DEL CARTEL */
                            List<SCE_CARTEL_MODELO_BE> lstMODELOS = new List<SCE_CARTEL_MODELO_BE>();
                            
                            SqlDataReader drMODELOS = null;
                            drMODELOS = getModelos(Id);

                            while (drMODELOS.Read())
                            {
                                SCE_CARTEL_MODELO_BE MODELO = new SCE_CARTEL_MODELO_BE();
                                MODELO.ID_MODELO = drMODELOS.GetInt32(0);
                                MODELO.NOM_MODELO = drMODELOS.GetString(1);
                                MODELO.FLAGPERTENECE = drMODELOS.GetInt32(2);
                                lstMODELOS.Add(MODELO);
                            }

                            BE.MODELOS = lstMODELOS;
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

        public SqlDataReader getModelos(int IdCartel)
        {
            string sql = "SP_SCE_CARTEL_MODELO_GET";
            SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin));
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
            cmd.CommandText = sql; 
         
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            return dr;             
        }

        public List<SCE_CARTEL_BE> Listar()
        {
            List<SCE_CARTEL_BE> lstBE = new List<SCE_CARTEL_BE>();
            SCE_CARTEL_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.CARTEL = Convert.ToString(reader["CARTEL"]);
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

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------

        public bool ExisteCartelModelo(int IdCartel, int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_MODELO_EXISTE";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        byte Existe = Convert.ToByte(cmd.Parameters["@EXISTE"].Value);

                        if (Existe == 0)
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

        public bool ExisteCartelInTB_CM(int IdCartel)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_EXISTE_CARTEL_EN_SCE_CARTEL_MODELO";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Existe = Convert.ToBoolean(cmd.Parameters["@EXISTE"].Value);

                        if (Existe == false)
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

        public bool ExisteCartelModeloInTB_CMC(int IdCartel, int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_EXISTE_CARTEL_MODELO_EN_SCE_CARTEL_MODELO_CAMPO";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Existe = Convert.ToBoolean(cmd.Parameters["@EXISTE"].Value);

                        if (Existe == false)
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

        public bool ExisteCartelModeloInTB_CMCP(int IdCartel, int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_EXISTE_CARTEL_MODELO_EN_SCE_CATEGORIA_PROMOCION";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);                      
                        cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Existe = Convert.ToBoolean(cmd.Parameters["@EXISTE"].Value);

                        if (Existe == false)
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

        public string EsCartelModeloCeroDigitos(int IdCartel)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_CERO_DIGITOS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.Add("@RSPTA", SqlDbType.Char,1).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return Convert.ToString(cmd.Parameters["@RSPTA"].Value);                       
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
