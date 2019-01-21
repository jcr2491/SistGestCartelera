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
    public class SCE_CARTEL_MODELO_DA
    {
        Usuario usrLogin;

        public SCE_CARTEL_MODELO_DA()
        {
           
        }

        public SCE_CARTEL_MODELO_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        public void ActualizarCMC(SCE_CARTEL_MODELO_BE BE, int numMaxDigitos, string CeroDigitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartelModelo1 = "SP_SCE_CARTEL_MODELO_CAMPO_DEL0";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo1, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                        cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlCartelModelo2 = "SP_SCE_CARTEL_MODELO_CAMPO_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo2, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO_BE in BE.CAMPOS)
                        {
                            if (CeroDigitos == "S")
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                                cmd.Parameters.AddWithValue("@DIGITOS", 0);
                                cmd.Parameters.AddWithValue("@ID_CAMPO", CARTEL_MODELO_CAMPO_BE.ID_CAMPO);
                                cmd.ExecuteNonQuery();
                            }
                            else if (CeroDigitos == "N")
                            {
                                for (int i = 1; i <= numMaxDigitos; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                    cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                                    cmd.Parameters.AddWithValue("@DIGITOS", i);
                                    cmd.Parameters.AddWithValue("@ID_CAMPO", CARTEL_MODELO_CAMPO_BE.ID_CAMPO);
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

        public void ActualizarCMC1(SCE_CARTEL_MODELO_BE BE, string PosFCX, string PosFCY)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartelModelo = "SP_SCE_CARTEL_MODELO_UPD";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                        cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                        cmd.Parameters.AddWithValue("@DIGITOS", BE.DIGITOS);
                        cmd.Parameters.AddWithValue("@NOM_PLANTILLA", BE.NOM_PLANTILLA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlCartelModelo3 = "SP_SCE_CARTEL_MODELO_UPD1";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo3, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                        cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                        cmd.Parameters.AddWithValue("@DIGITOS", BE.DIGITOS);
                        cmd.Parameters.AddWithValue("@POS_FC_X", PosFCX);
                        cmd.Parameters.AddWithValue("@POS_FC_Y", PosFCY);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlCartelModelo1 = "SP_SCE_CARTEL_MODELO_CAMPO_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo1, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                        cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                        cmd.Parameters.AddWithValue("@DIGITOS", BE.DIGITOS);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlCartelModelo2 = "SP_SCE_CARTEL_MODELO_CAMPO_UPD";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo2, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO_BE in BE.CAMPOS)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                            cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                            cmd.Parameters.AddWithValue("@DIGITOS", BE.DIGITOS);
                            cmd.Parameters.AddWithValue("@ID_CAMPO", CARTEL_MODELO_CAMPO_BE.ID_CAMPO);
                            cmd.Parameters.AddWithValue("@POS_X", CARTEL_MODELO_CAMPO_BE.POSX);
                            cmd.Parameters.AddWithValue("@POS_Y", CARTEL_MODELO_CAMPO_BE.POSY);
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

        public SCE_CARTEL_MODELO_BE ObtenerPorID(int IdCartel, int IdModelo)
        {
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_GET1";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);                        

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.CODIGO = Convert.ToString(reader["CODIGO"]).Trim();
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();
                            
                            /* LLENA LA LISTA DE CAMPOS DEL CARTEL-MODELO */
                            List<SCE_CARTEL_MODELO_CAMPO_BE> lstCARTELMODELOCAMPO = new List<SCE_CARTEL_MODELO_CAMPO_BE>();

                            SqlDataReader drCARTELMODELOCAMPO = null;
                            drCARTELMODELOCAMPO = getCampos(IdCartel, IdModelo);

                            while (drCARTELMODELOCAMPO.Read())
                            {
                                SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO = new SCE_CARTEL_MODELO_CAMPO_BE();
                                CARTEL_MODELO_CAMPO.ID_CAMPO = drCARTELMODELOCAMPO.GetInt32(0);
                                CARTEL_MODELO_CAMPO.NOM_CAMPO = drCARTELMODELOCAMPO.GetString(1);
                                CARTEL_MODELO_CAMPO.DESCRIPCION = drCARTELMODELOCAMPO.GetString(2);
                                CARTEL_MODELO_CAMPO.FLAGPERTENECE = drCARTELMODELOCAMPO.GetInt32(3);

                                lstCARTELMODELOCAMPO.Add(CARTEL_MODELO_CAMPO);
                            }

                            BE.CAMPOS = lstCARTELMODELOCAMPO;
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

        public SqlDataReader getCampos(int IdCartel, int IdModelo)
        {
            string sql = "SP_SCE_CARTEL_MODELO_CAMPO_GET";
            SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin));
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
            cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
            cmd.CommandText = sql;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }
        
        public List<SCE_CARTEL_MODELO_BE> ListarPV()
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_SEL1";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                     

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
                            BE.NOM_CAMPO = Convert.ToString(reader["ALIAS"]);
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

        public List<SCE_CARTEL_MODELO_BE> Listar()
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                       

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.CODIGO = Convert.ToString(reader["CODIGO"]).Trim();                            
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();                                                    

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


        public List<SCE_CARTEL_MODELO_BE> cboCartelModelo()
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_SEL2";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.CODIGO = Convert.ToString(reader["CODIGO"]).Trim();
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();

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

        public List<SCE_CARTEL_MODELO_BE> ListarCMConf()
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_SEL4";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.DIGITOS = Convert.ToInt32(reader["DIGITOS"]);                            
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();
                            BE.NRODIGITOS = Convert.ToString(reader["NRODIGITOS"]).Trim();
                            BE.NOM_PLANTILLA = Convert.ToString(reader["NOM_PLANTILLA"]).Trim();

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

        public bool ExistePlantilla(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_EXISTE_PLANTILLA";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
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

        public string ObtenerPlantilla(int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_GET_PLANTILLA_CARTEL_IMPR";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.Add("@PLANTILLA", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        string Plantilla = Convert.ToString(cmd.Parameters["@PLANTILLA"].Value);

                        return Plantilla;                 
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCE_CARTEL_MODELO_BE ObtenerPorID1(int IdCartel, int IdModelo, int Digitos)
        {
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_GET2";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();
                          
                            BE.NOM_CARTEL = Convert.ToString(reader["NOM_CARTEL"]).Trim();
                            BE.NOM_MODELO = Convert.ToString(reader["NOM_MODELO"]).Trim();
                            BE.DESCRIPCION = Convert.ToString(reader["NOM_CARTEL"]).Trim() + '-' + Convert.ToString(reader["NOM_MODELO"]).Trim();
                            BE.NRODIGITOS = Convert.ToString(reader["NRODIGITOS"]).Trim();
                            BE.NOM_PLANTILLA = Convert.ToString(reader["NOM_PLANTILLA"]).Trim();                         

                            /* LLENA LA LISTA DE CAMPOS DEL CARTEL-MODELO */
                            List<SCE_CARTEL_MODELO_CAMPO_BE> lstCARTELMODELOCAMPO = new List<SCE_CARTEL_MODELO_CAMPO_BE>();

                            SqlDataReader drCARTELMODELOCAMPO = null;
                            drCARTELMODELOCAMPO = getCampos1(IdCartel, IdModelo, Digitos);

                            while (drCARTELMODELOCAMPO.Read())
                            {
                                SCE_CARTEL_MODELO_CAMPO_BE CARTEL_MODELO_CAMPO = new SCE_CARTEL_MODELO_CAMPO_BE();
                                CARTEL_MODELO_CAMPO.ID_CAMPO = drCARTELMODELOCAMPO.GetInt32(0);
                                CARTEL_MODELO_CAMPO.CAMPO = drCARTELMODELOCAMPO.GetString(1).Trim();
                                CARTEL_MODELO_CAMPO.DESCRIPCION = drCARTELMODELOCAMPO.GetString(2).Trim();
                                if (!drCARTELMODELOCAMPO.IsDBNull(drCARTELMODELOCAMPO.GetOrdinal("POS_X")))
                                {
                                    CARTEL_MODELO_CAMPO.POSX = drCARTELMODELOCAMPO.GetString(3).Trim();
                                }

                                if (!drCARTELMODELOCAMPO.IsDBNull(drCARTELMODELOCAMPO.GetOrdinal("POS_Y")))
                                {
                                    CARTEL_MODELO_CAMPO.POSY = drCARTELMODELOCAMPO.GetString(4).Trim();
                                }
                                
                                lstCARTELMODELOCAMPO.Add(CARTEL_MODELO_CAMPO);
                            }

                            BE.CAMPOS = lstCARTELMODELOCAMPO;
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

        public SqlDataReader getCampos1(int IdCartel, int IdModelo, int Digitos)
        {
            string sql = "SP_SCE_CARTEL_MODELO_CAMPO_GET1";
            SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin));
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
            cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
            cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
            cmd.CommandText = sql;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public List<SCE_CARTEL_MODELO_CAMPO_BE> grvCampos(int IdCartel, int IdModelo, int Digitos)
        {
            List<SCE_CARTEL_MODELO_CAMPO_BE> lstBE = new List<SCE_CARTEL_MODELO_CAMPO_BE>();
            SCE_CARTEL_MODELO_CAMPO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_CAMPO_GET2";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_CAMPO_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.DIGITOS = Convert.ToInt32(reader["DIGITOS"]);
                            BE.ID_CAMPO = Convert.ToInt32(reader["ID_CAMPO"]);
                            BE.POSX = Convert.ToString(reader["POS_X"]).Trim();
                            BE.POSY = Convert.ToString(reader["POS_Y"]).Trim();
                            BE.ALIAS = Convert.ToString(reader["CAMPO"]).Trim();
                            BE.NOM_CAMPO = Convert.ToString(reader["DESCRIPCION"]).Trim();                         

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

        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------        
        public void ActualizarCMCP(SCE_CARTEL_MODELO_BE BE, int Digitos, string CeroDigitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartelModelo = "SP_SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE CARTEL_MODELO_CATEGORIA_PROMOCION_BE in BE.CATEGS_PROMOS)
                        {
                            if (CeroDigitos == "S")
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                                cmd.Parameters.AddWithValue("@DIGITOS", 0);
                                cmd.Parameters.AddWithValue("@ID_CATEGORIA", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_CATEGORIA);
                                cmd.Parameters.AddWithValue("@ID_PROMOCION", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_PROMOCION);
                                cmd.ExecuteNonQuery();
                            }
                            else if (CeroDigitos == "N")
                            {
                                for (int i = 1; i <= Digitos; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                                    cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                                    cmd.Parameters.AddWithValue("@DIGITOS", i);
                                    cmd.Parameters.AddWithValue("@ID_CATEGORIA", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_CATEGORIA);
                                    cmd.Parameters.AddWithValue("@ID_PROMOCION", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_PROMOCION);
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

        public void EliminarCMCP(SCE_CARTEL_MODELO_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartelModelo = "SP_SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlCartelModelo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE CARTEL_MODELO_CATEGORIA_PROMOCION_BE in BE.CATEGS_PROMOS)
                        {
                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@ID_CARTEL", BE.ID_CARTEL);
                            cmd.Parameters.AddWithValue("@ID_MODELO", BE.ID_MODELO);
                            cmd.Parameters.AddWithValue("@ID_CATEGORIA", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_CATEGORIA);
                            cmd.Parameters.AddWithValue("@ID_PROMOCION", CARTEL_MODELO_CATEGORIA_PROMOCION_BE.ID_PROMOCION);
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
        
        public List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> ListarCMCP(int IdCategoria, int IdPromocion)
        {
            List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE> lstBE = new List<SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE>();
            SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);

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

        public bool ExisteCartelModeloInTB_CMCP(int IdCartel, int IdModelo, int IdCategoria, int IdPromocion)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_EXISTE";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
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

        public List<SCE_CARTEL_MODELO_BE> cboCartelModeloCP()
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_SEL3";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.CODIGO = Convert.ToString(reader["CODIGO"]).Trim();
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();

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

        public List<SCE_CARTEL_MODELO_BE> cboCartelModeloCMCP(int IdCategoria, int IdPromocion)
        {
            List<SCE_CARTEL_MODELO_BE> lstBE = new List<SCE_CARTEL_MODELO_BE>();
            SCE_CARTEL_MODELO_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_CARTEL_MODELO_CATEGORIA_PROMOCION_CBO";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_CARTEL_MODELO_BE();

                            BE.ID_CARTEL = Convert.ToInt32(reader["ID_CARTEL"]);
                            BE.ID_MODELO = Convert.ToInt32(reader["ID_MODELO"]);
                            BE.DIGITOS = Convert.ToInt32(reader["DIGITOS"]);
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]).Trim();

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

        public bool TieneCPConfiguradoCM(int IdCategoria, int IdPromocion)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_HAVE_CATEG_PROM_CARTEL_MODELO_CONFIG";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
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

        public bool ValidaCartelModeloLineaProducto(int IdLinea, 
                                                    int IdGuia,
                                                    int IdCartel, 
                                                    int IdModelo,
                                                    int Digitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "VALIDA_CARTEL_LINEA_PRODUCTO";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);

                        cmd.Parameters.Add("@RPTA", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Rpta = Convert.ToBoolean(cmd.Parameters["@RPTA"].Value);

                        if (Rpta == false)
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

        public bool TieneIgualNroDigitos(int IdLinea,
                                         int IdGuia, 
                                         int IdCartel,
                                         int IdModelo,    
                                         int Digitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "CARTEL_MODELO_TIENE_IGUAL_NUMERO_DIGITOS";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);  
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.Add("@RPTA", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool Rpta = Convert.ToBoolean(cmd.Parameters["@RPTA"].Value);

                        if (Rpta == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetNombreModelo(int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_GET_NOMBRE_MODELO_IMPR";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        string Nombre = Convert.ToString(cmd.Parameters["@NOMBRE"].Value);

                        return Nombre;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTipoHojaModelo(int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_GET_TIPO_PAPEL_IMPR";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        string Tipo = Convert.ToString(cmd.Parameters["@TIPO"].Value);

                        return Tipo;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteParCategPromoSeleccionado(int IdCategoria, int IdPromocion)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_EXISTE_PAR_CATEG_PROMO_SELECC";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
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
    }
}
