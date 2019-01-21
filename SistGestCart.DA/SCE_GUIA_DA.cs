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
    public class SCE_GUIA_DA
    {
        Usuario usrLogin;

        public SCE_GUIA_DA()
        {
           
        }

        public SCE_GUIA_DA(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;
        }

        //public void Ins_CabDet(string Cab_Nombre, List Detalles)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Nombre", typeof(string));
        //    dt.Columns.Add("OtroDato1", typeof(int));
        //    dt.Columns.Add("OtroDato2", typeof(DateTime));
        //    foreach (Detalle objDet in Detalles)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[0] = objDet.Nombre;
        //        dr[1] = objDet.OtroDato1;
        //        dr[2] = objDet.OtroDato2;
        //        dt.Rows.Add(dr);
        //    }
        //    using (SqlCommand objSCmd = new SqlCommand("Ins_CabDet", new SqlConnection("Data  Source=XXX....")))
        //    {
        //        objSCmd.CommandType = CommandType.StoredProcedure;
        //        objSCmd.Connection.Open();
        //        objSCmd.Parameters.Add("@Nombre_Cab", SqlDbType.NVarChar, 20).Value = Cab_Nombre;
        //        objSCmd.Parameters.AddWithValue("@Detalles", dt);
        //        objSCmd.ExecuteNonQuery();
        //    }
        //}

        public int GetMaxLineaDetalleGuia(int IdGuia)
        {
            int x = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_GET_MAX_LINEA_DETALLE_GUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.Add("@MAXLINEA", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return x = Convert.ToInt32(cmd.Parameters["@MAXLINEA"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*****************************************************************************/
        /**METODOS PARA EL CONTROL DE CONCURRENCIA DE USUARIOS EN LAS GUIAS MANUALES**/
        /*****************************************************************************/
        /*******************METODOS PARA EL CONTROL DE CORRELATIVOS*******************/
        //public int ExisteUsuarioLogueado(string usuario)
        //{
        //    int x = 0;

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlCartel = "SP_SCE_CTRL_SESSIONES_EXISTE_USER";

        //            using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.Parameters.Add("@RSPTA", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                return x = Convert.ToInt32(cmd.Parameters["@RSPTA"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void InsertarUsuarioEnTablaCtrol(string usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sql = "SP_SCE_CTRL_SESSIONES_INS";

        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void EliminarUsuarioEnTablaCtrol(string usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sql = "SP_SCE_CTRL_SESSIONES_DEL";

        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /************METODOS PARA EL CONTROL DE SESSIONES DE USUARIO AL SISTEMA*******/

        //public int GetMaxLineaDetalleGuiaUser(int IdGuia, string usuario, int IdTienda)
        //{
        //    int x = 0;

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlCartel = "SP_GET_MAX_LINEA_DETALLE_GUIA";

        //            using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.Parameters.AddWithValue("@TIENDA", IdTienda);
        //                cmd.Parameters.Add("@MAXLINEA", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                return x = Convert.ToInt32(cmd.Parameters["@MAXLINEA"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void EliminarMaxLineaDetalleGuiaUser(int IdGuia, int IdLinea, string usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sql = "SP_DEL_MAX_LINEA_DETALLE_GUIA";

        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
        //                cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.ExecuteNonQuery();                       
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void EliminarMaxLineaDetalleGuiaUserExt(string usuario)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sql = "SP_DEL_MAX_LINEA_DETALLE_GUIA_EXT";

        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int GetLineaDetalleGuiaUser(int IdGuia, string usuario, int IdTienda)
        //{
        //    int x = 0;

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlCartel = "SP_GET_LINEA_DETALLE_GUIA_USER";

        //            using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
        //                cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //                cmd.Parameters.AddWithValue("@TIENDA", IdTienda);
        //                cmd.Parameters.Add("@LINEAUSER", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                return x = Convert.ToInt32(cmd.Parameters["@LINEAUSER"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /*****************************************************************************/
        /*****************************************************************************/

        /* REGISTRA LA GUIA CON LOS REGISTROS DE LOS PRODUCTOS (REGISTRA EN LA TABLA
         * DE CABECERA (SCE_GUIA) Y LAS DOS TABLAS DE DETALLE (SCE_GUIA_DET y 
         * SCE_GUIA_DET_CAMPO)*/
        //public double InsertarGuia(SCE_GUIA_BE BE)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlGuia = "SP_SCE_GUIA_INS";

        //            using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@NOM_GUIA", BE.NOM_GUIA);
        //                cmd.Parameters.AddWithValue("@TIPO_GUIA", BE.TIPO_GUIA);
        //                cmd.Parameters.AddWithValue("@ESTADO_GUIA", BE.ESTADO_GUIA);
        //                cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
        //                cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
        //                cmd.Parameters.AddWithValue("@FECHA_INI", BE.FECHA_INI);
        //                cmd.Parameters.AddWithValue("@FECHA_FIN", BE.FECHA_FIN);
        //                cmd.Parameters.AddWithValue("@USER_CRE", BE.USER_CRE);
        //                cmd.Parameters.Add("@ID_GUIA", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                BE.ID_GUIA = (int)cmd.Parameters["@ID_GUIA"].Value;
        //            }

        //            string sqlDetalleGuia = "SP_SCE_GUIA_DET_INS";                  

        //            foreach (SCE_GUIA_DET_BE GUIA_DET_BE in BE.DETALLE_GUIA)
        //            {
        //                using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    cmd.Parameters.Clear();
        //                    cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                    //cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_BE.ID_LINEA);
        //                    cmd.Parameters.AddWithValue("@ID_CATEGORIA", GUIA_DET_BE.ID_CATEGORIA);
        //                    cmd.Parameters.AddWithValue("@ID_PROMOCION", GUIA_DET_BE.ID_PROMOCION);
        //                    cmd.Parameters.AddWithValue("@FLG_IMPRESION", GUIA_DET_BE.FLG_IMPRESION);
        //                    cmd.Parameters.Add("@ID_LINEA", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();

        //                    GUIA_DET_BE.ID_LINEA = (int)cmd.Parameters["@ID_LINEA"].Value;
        //                }
        //            }

        //            string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_INS";

        //            using (SqlCommand cmd1 = new SqlCommand(sqlDetalleGuiaCamp, cn))
        //            {
        //                cmd1.CommandType = CommandType.StoredProcedure;
                        
        //                foreach (SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE in BE.DETALLE_GUIA_CAMPOS)
        //                {
        //                    ////ITERAMOS EL NUMERO DE REPETICIONES DE LA LINEA EN BASE A LOS CAMPOS DEL PRODUCTO
        //                    //for (int i = 1; i <= repLineas; i++)
        //                    //{
        //                        cmd1.Parameters.Clear();
        //                        cmd1.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                                
        //                        //cmd1.Parameters.AddWithValue("@ID_LINEA", GetLineaDetalleGuia(BE.ID_GUIA));
                                
        //                        cmd1.Parameters.AddWithValue("@ID_CAMPO", GUIA_DET_CAMPO_BE.ID_CAMPO);
        //                        cmd1.Parameters.AddWithValue("@VALOR", GUIA_DET_CAMPO_BE.VALOR);
        //                        cmd1.ExecuteNonQuery();
        //                    //}
        //                }
        //            }
        //        }

        //        return BE.ID_GUIA;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public double InsertarGuia(SCE_GUIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_GUIA_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NOM_GUIA", BE.NOM_GUIA);
                        cmd.Parameters.AddWithValue("@TIPO_GUIA", BE.TIPO_GUIA);
                        cmd.Parameters.AddWithValue("@ESTADO_GUIA", BE.ESTADO_GUIA);
                        cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                        cmd.Parameters.AddWithValue("@FECHA_INI", BE.FECHA_INI);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", BE.FECHA_FIN);
                        cmd.Parameters.AddWithValue("@USER_CRE", BE.USER_CRE);
                        cmd.Parameters.Add("@ID_GUIA", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        BE.ID_GUIA = (int)cmd.Parameters["@ID_GUIA"].Value;
                    }

                    string sqlDetalleGuia = "SP_SCE_GUIA_DET_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_DET_BE GUIA_DET_BE in BE.DETALLE_GUIA)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                            cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_BE.ID_LINEA);
                            cmd.Parameters.AddWithValue("@ID_CATEGORIA", GUIA_DET_BE.ID_CATEGORIA);
                            cmd.Parameters.AddWithValue("@ID_PROMOCION", GUIA_DET_BE.ID_PROMOCION);
                            cmd.Parameters.AddWithValue("@FLG_IMPRESION", GUIA_DET_BE.FLG_IMPRESION);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCamp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE in BE.DETALLE_GUIA_CAMPOS)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                            cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_CAMPO_BE.ID_LINEA);
                            cmd.Parameters.AddWithValue("@ID_CAMPO", GUIA_DET_CAMPO_BE.ID_CAMPO);
                            cmd.Parameters.AddWithValue("@VALOR", GUIA_DET_CAMPO_BE.VALOR);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return BE.ID_GUIA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        /* ELIMINA TODA LA GUIA. ELIMINA PRIMERO LOS REGISTROS DE LA TABLA 
         * SCE_GUIA_DET_CAMPO DESPUES DE LA TABLA SCE_GUIA_DET Y POR ULTIMO 
         * DE LA TABLA SCE_GUIA */
        public void EliminarGuiaManual(SCE_GUIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCamp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlDetalleGuia = "SP_SCE_GUIA_DET_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlGuia = "SP_SCE_GUIA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* ELIMINA TODA LA GUIA. ELIMINA PRIMERO LOS REGISTROS DE LA TABLA 
         * SCE_GUIA_EXEPCIONES SI ES QUE LA GUIA ESTA EN ESTADO DE IMPRESION
         * SCE_GUIA_DET_CAMPO DESPUES DE LA TABLA SCE_GUIA_DET Y POR ULTIMO 
         * DE LA TABLA SCE_GUIA */
        public void EliminarGuiaAutomatica(SCE_GUIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuiAExecp = "SP_SCE_GUIA_EXEPCIONES_GUIA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlGuiAExecp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }                    

                    string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCamp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlDetalleGuia = "SP_SCE_GUIA_DET_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlGuia = "SP_SCE_GUIA_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarEstadoGuia(int IdGuia)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_ESTADO_UPD";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* ACTUALIZA TODA LA GUIA. ACTUALIZA PRIMERO EL REGISTRO (GUIA) DE LA TABLA 
        * SCE_GUIA DESPUES ELIMINA TODOS LOS REGISTROS DE DICHA GUIA SELECCIONADA
        * DE LA TABLA SCE_GUIA_DET_CAMPO Y DE LA TABLA SCE_GUIA_DET PARA FINALIZAR
        * CON LA INSERCION DE LOS REGISTROS DE LA GUIA EN LA TABLAS SCE_GUIA_DET Y 
        * SCE_GUIA_DET_CAMPO*/
        //public void ActualizarGuia(SCE_GUIA_BE BE)
        //{
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlGuia = "SP_SCE_GUIA_UPD";

        //            using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                cmd.Parameters.AddWithValue("@NOM_GUIA", BE.NOM_GUIA);
        //                cmd.Parameters.AddWithValue("@ESTADO_GUIA", BE.ESTADO_GUIA);
        //                cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
        //                cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
        //                cmd.Parameters.AddWithValue("@FECHA_INI", BE.FECHA_INI);
        //                cmd.Parameters.AddWithValue("@FECHA_FIN", BE.FECHA_FIN);
        //                cmd.Parameters.AddWithValue("@USER_MOD", BE.USER_MOD);
        //                cmd.ExecuteNonQuery();
        //            }

        //            string sqlDetalleGuiaCampD = "SP_SCE_GUIA_DET_CAMPO_DELGUIA";

        //            using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCampD, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                cmd.ExecuteNonQuery();
        //            }

        //            string sqlDetalleGuiaD = "SP_SCE_GUIA_DET_DELGUIA";

        //            using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaD, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.Clear();
        //                cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                cmd.ExecuteNonQuery();
        //            }

        //            string sqlDetalleGuia = "SP_SCE_GUIA_DET_INS";

        //            using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                foreach (SCE_GUIA_DET_BE GUIA_DET_BE in BE.DETALLE_GUIA)
        //                {
        //                    cmd.Parameters.Clear();
        //                    cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                    cmd.Parameters.AddWithValue("@ID_CATEGORIA", GUIA_DET_BE.ID_CATEGORIA);
        //                    cmd.Parameters.AddWithValue("@ID_PROMOCION", GUIA_DET_BE.ID_PROMOCION);
        //                    cmd.Parameters.AddWithValue("@FLG_IMPRESION", GUIA_DET_BE.FLG_IMPRESION);
        //                    cmd.Parameters.Add("@ID_LINEA", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                    cmd.ExecuteNonQuery();

        //                    GUIA_DET_BE.ID_LINEA = (int)cmd.Parameters["@ID_LINEA"].Value;

        //                    string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_INS";

        //                    using (SqlCommand cmd1 = new SqlCommand(sqlDetalleGuiaCamp, cn))
        //                    {
        //                        cmd1.CommandType = CommandType.StoredProcedure;

        //                        foreach (SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE in BE.DETALLE_GUIA_CAMPOS)
        //                        {
        //                            cmd1.Parameters.Clear();
        //                            cmd1.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
        //                            cmd1.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_BE.ID_LINEA);
        //                            cmd1.Parameters.AddWithValue("@ID_CAMPO", GUIA_DET_CAMPO_BE.ID_CAMPO);
        //                            cmd1.Parameters.AddWithValue("@VALOR", GUIA_DET_CAMPO_BE.VALOR);
        //                            cmd1.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}    

        public void ActualizarGuia(SCE_GUIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_GUIA_UPD";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.Parameters.AddWithValue("@NOM_GUIA", BE.NOM_GUIA);
                        cmd.Parameters.AddWithValue("@ESTADO_GUIA", BE.ESTADO_GUIA);
                        cmd.Parameters.AddWithValue("@ID_TIENDA", BE.ID_TIENDA);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", BE.ID_GRUPO);
                        cmd.Parameters.AddWithValue("@FECHA_INI", BE.FECHA_INI);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", BE.FECHA_FIN);
                        cmd.Parameters.AddWithValue("@USER_MOD", BE.USER_MOD);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlDetalleGuiaCampD = "SP_SCE_GUIA_DET_CAMPO_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCampD, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlDetalleGuiaD = "SP_SCE_GUIA_DET_DELGUIA";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaD, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlDetalleGuia = "SP_SCE_GUIA_DET_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_DET_BE GUIA_DET_BE in BE.DETALLE_GUIA)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                            cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_BE.ID_LINEA);

                            cmd.Parameters.AddWithValue("@ID_CATEGORIA", GUIA_DET_BE.ID_CATEGORIA);
                            cmd.Parameters.AddWithValue("@ID_PROMOCION", GUIA_DET_BE.ID_PROMOCION);

                            cmd.Parameters.AddWithValue("@FLG_IMPRESION", GUIA_DET_BE.FLG_IMPRESION);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    string sqlDetalleGuiaCamp = "SP_SCE_GUIA_DET_CAMPO_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlDetalleGuiaCamp, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_DET_CAMPO_BE GUIA_DET_CAMPO_BE in BE.DETALLE_GUIA_CAMPOS)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_GUIA", BE.ID_GUIA);
                            cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_DET_CAMPO_BE.ID_LINEA);
                            cmd.Parameters.AddWithValue("@ID_CAMPO", GUIA_DET_CAMPO_BE.ID_CAMPO);
                            cmd.Parameters.AddWithValue("@VALOR", GUIA_DET_CAMPO_BE.VALOR);
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

        public List<SCE_GUIA_BE> Listar()
        {
            List<SCE_GUIA_BE> lstBE = new List<SCE_GUIA_BE>();
            SCE_GUIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandTimeout = 1200;

                        SqlDataReader reader = cmd.ExecuteReader();                      

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.NOM_GUIA = Convert.ToString(reader["NOM_GUIA"]);
                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.NOM_PROMOCION = Convert.ToString(reader["NOM_PROMOCION"]);
                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);
                            BE.FECHA_INI = Convert.ToDateTime(reader["FECHA_INI"]);
                            BE.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.ESTADO_GUIA = Convert.ToInt32(reader["ESTADO_GUIA"]);
                            BE.DESTADO_GUIA = Convert.ToString(reader["DESTADO_GUIA"]);

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

        //BUSQUEDA PARA MODULO DE GUIAS MANUALES
        public List<SCE_GUIA_BE> Buscar(long FecIniVig,
                                        long FecFinVig,
                                        int IdTienda,
                                        int IdGrupo,
                                        int IdPromocion,
                                        int IdCategoria,
                                        int Estado,
                                        string NomGuia,
                                        int TipGuia,
                                        bool lastPage,
                                        int PageSize,
                                        int PageNumber,
                                        ref int PageCount)
        {
            List<SCE_GUIA_BE> lstBE = new List<SCE_GUIA_BE>();
            SCE_GUIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_SEEK_GUIAS_MANUALES";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (FecIniVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", FecIniVig);
                        }

                        if (FecFinVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", FecFinVig);
                        }

                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", IdGrupo);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ESTADO_GUIA", Estado);
                        cmd.Parameters.AddWithValue("@NOM_GUIA", NomGuia);
                        cmd.Parameters.AddWithValue("@TIPO_GUIA", TipGuia);

                        if (lastPage)
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 0);
                        }

                        cmd.Parameters.AddWithValue("@PageSize", PageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                        cmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int NroTotalPaginas = (int)cmd.Parameters["@PageCount"].Value;
                        PageCount = NroTotalPaginas;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.NOM_GUIA = Convert.ToString(reader["NOM_GUIA"]);
                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.NOM_PROMOCION = Convert.ToString(reader["NOM_PROMOCION"]);
                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);
                            BE.FECHA_INI = Convert.ToDateTime(reader["FECHA_INI"]);
                            BE.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.ESTADO_GUIA = Convert.ToInt32(reader["ESTADO_GUIA"]);
                            BE.DESTADO_GUIA = Convert.ToString(reader["DESTADO_GUIA"]);

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

        //BUSQUEDA PARA MODULO DE GUIAS AUTOMATICAS
        public List<SCE_GUIA_BE> Buscar(long FecIniVig,
                                        long FecFinVig,
                                        int IdTienda,
                                        int IdGrupo,
                                        int Estado,
                                        string NomGuia,
                                        int TipGuia,
                                        bool lastPage,
                                        int PageSize,
                                        int PageNumber,
                                        ref int PageCount)
        {
            List<SCE_GUIA_BE> lstBE = new List<SCE_GUIA_BE>();
            SCE_GUIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_SEEK_GUIAS_AUTOMATICAS";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (FecIniVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", FecIniVig);
                        }

                        if (FecFinVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", FecFinVig);
                        }

                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", IdGrupo);
                        cmd.Parameters.AddWithValue("@ESTADO_GUIA", Estado);
                        cmd.Parameters.AddWithValue("@NOM_GUIA", NomGuia);
                        cmd.Parameters.AddWithValue("@TIPO_GUIA", TipGuia);

                        if (lastPage)
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 0);
                        }

                        cmd.Parameters.AddWithValue("@PageSize", PageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                        cmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int NroTotalPaginas = (int)cmd.Parameters["@PageCount"].Value;
                        PageCount = NroTotalPaginas;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.NOM_GUIA = Convert.ToString(reader["NOM_GUIA"]);
                            BE.FECHA_INI = Convert.ToDateTime(reader["FECHA_INI"]);
                            BE.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.ESTADO_GUIA = Convert.ToInt32(reader["ESTADO_GUIA"]);
                            BE.DESTADO_GUIA = Convert.ToString(reader["DESTADO_GUIA"]);

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

        //BUSQUEDA PARA MODULO DE GUIAS AUTOMATICAS CON EXEPCIONES
        public List<SCE_GUIA_BE> BuscarE(long FecIniVig,
                                         long FecFinVig,
                                         int IdTienda,
                                         int IdGrupo,
                                         int Estado,
                                         string NomGuia,
                                         int TipGuia,
                                         bool lastPage,
                                         int PageSize,
                                         int PageNumber,
                                         ref int PageCount)
        {
            List<SCE_GUIA_BE> lstBE = new List<SCE_GUIA_BE>();
            SCE_GUIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_SEEK_GUIAS_AUTOMATICAS_EXEPCIONES";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (FecIniVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", FecIniVig);
                        }

                        if (FecFinVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", FecFinVig);
                        }

                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", IdGrupo);
                        cmd.Parameters.AddWithValue("@ESTADO_GUIA", Estado);
                        cmd.Parameters.AddWithValue("@NOM_GUIA", NomGuia);
                        cmd.Parameters.AddWithValue("@TIPO_GUIA", TipGuia);

                        if (lastPage)
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 0);
                        }

                        cmd.Parameters.AddWithValue("@PageSize", PageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                        cmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int NroTotalPaginas = (int)cmd.Parameters["@PageCount"].Value;
                        PageCount = NroTotalPaginas;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.NOM_GUIA = Convert.ToString(reader["NOM_GUIA"]);
                            BE.FECHA_INI = Convert.ToDateTime(reader["FECHA_INI"]);
                            BE.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.ESTADO_GUIA = Convert.ToInt32(reader["ESTADO_GUIA"]);
                            BE.DESTADO_GUIA = Convert.ToString(reader["DESTADO_GUIA"]);

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

        //BUSQUEDA PARA MODULO DE BUSQUEDA DE GUIAS E IMPRESIONES
        public List<SCE_GUIA_BE> BuscarImpr(long fecIniVig,
                                            long fecFinVig,
                                            int IdTienda,
                                            int IdPromocion,
                                            int IdCategoria,
                                            string NomGuia,
                                            int TipGuia,
                                            bool lastPage,
                                            int PageSize,
                                            int PageNumber,
                                            ref int PageCount)
        {
            List<SCE_GUIA_BE> lstBE = new List<SCE_GUIA_BE>();
            SCE_GUIA_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_SEEK_GUIAS_IMPRESION";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (fecIniVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INI", fecIniVig);
                        }

                        if (fecFinVig == 0)
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FECHA_FIN", fecFinVig);
                        }

                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@NOM_GUIA", NomGuia);
                        cmd.Parameters.AddWithValue("@TIPO_GUIA", TipGuia);

                        if (lastPage)
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@LastPageSQL", 0);
                        }

                        cmd.Parameters.AddWithValue("@PageSize", PageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                        cmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int NroTotalPaginas = (int)cmd.Parameters["@PageCount"].Value;
                        PageCount = NroTotalPaginas;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.NOM_GUIA = Convert.ToString(reader["NOM_GUIA"]);
                            BE.DES_TIPO_GUIA = Convert.ToString(reader["DES_TIPO_GUIA"]);
                            BE.ID_PROMOCION = Convert.ToInt32(reader["ID_PROMOCION"]);
                            BE.ID_CATEGORIA = Convert.ToInt32(reader["ID_CATEGORIA"]);
                            BE.NOM_CATEGORIA = Convert.ToString(reader["NOM_CATEGORIA"]);
                            BE.FECHA_INI = Convert.ToDateTime(reader["FECHA_INI"]);
                            BE.FECHA_FIN = Convert.ToDateTime(reader["FECHA_FIN"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.NOM_TIENDA = Convert.ToString(reader["NOM_TIENDA"]);
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

        public DataTable GetDetalleGuia(int IdGuia)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);

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

        public DataTable GetDetalleGuiaCampo(int IdGuia)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_CAMPO_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        
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

        public DataTable GetDetalleGuiaCampoAutomatico(int IdGuia)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_CAMPO_GET_A";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);

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

        public DataTable GetDetalleGuiaCampoImprManual(int IdGuia, 
                                                       int IdCategoria, 
                                                       int IdPromocion)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_CAMPO_GET_IMPR";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);                        

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

        public DataTable GetDetalleGuiaCampoImprAutomatica(int IdGuia,
                                                           int IdCategoria,
                                                           int IdPromocion,
                                                           int IdTienda)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_CAMPO_GET_IMPR_AUTOMATICA";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);

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

        public DataTable GetDetalleGuiaCampo1(int IdGuia, int IdLinea)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_DET_CAMPO_GET1";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);

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

        public DataTable GetNewRegDetalleGuiaCampo(int IdPromocion, int IdCategoria)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "GET_NEW_REG_DETALLE_GUIA_CAMPO";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);

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

        //---------------------------------------------------------------------------
        //Maneja los datos de las previsualizaciones de carteles PDF de las guias manuales 
        //y automaticas y masivas
        //---------------------------------------------------------------------------
        public DataTable GetCartelGuiaImpr(int IdCartel,
                                           int IdModelo,
                                           int Digitos,
                                           int IdGuia,
                                           int IdLinea)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GET_CARTEL_GUIA_IMPR";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);                        

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

        public string GetCoordenadaXFinCuadrante(int IdCartel,
                                                 int IdModelo,
                                                 int Digitos)
        {
            string x = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_XFC_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.Add("@XFC", SqlDbType.VarChar,20).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return x = Convert.ToString(cmd.Parameters["@XFC"].Value);                       
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCoordenadaYFinCuadrante(int IdCartel,
                                                 int IdModelo,
                                                 int Digitos)
        {
            string y = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_CARTEL_YFC_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.Add("@YFC", SqlDbType.VarChar,20).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return y = Convert.ToString(cmd.Parameters["@YFC"].Value);
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNroTotalFilasPlantilla(int IdModelo)
        {
            int x = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_NROFILAS_CARTEL_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@NROFILAS", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return x = Convert.ToInt32(cmd.Parameters["@NROFILAS"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNroTotalColumnasPlantilla(int IdModelo)
        {
            int y = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_NROCOLUMNAS_CARTEL_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@NROCOLUMNAS", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return y = Convert.ToInt32(cmd.Parameters["@NROCOLUMNAS"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCoordenadasPlantilla(int IdCartel,
                                                 int IdModelo,
                                                 int Digitos)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GET_COORDENADAS_PLANTILLA";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
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

        public int GetNroCamposPlantilla(int IdCartel, int IdModelo, int Digitos)
        {
            int c = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_NROCAMPOS_PLANTILLA_GET";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.Parameters.Add("@NROCAMPOS", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return c = Convert.ToInt32(cmd.Parameters["@NROCAMPOS"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int GetNroCamposPlantilla(int IdCartel, int IdModelo)
        //{
        //    int c = 0;

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sqlCartel = "SP_SCE_NROCAMPOS_PLANTILLA_GET";

        //            using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
        //                cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
        //                cmd.Parameters.Add("@NROCAMPOS", SqlDbType.Int).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                return c = Convert.ToInt32(cmd.Parameters["@NROCAMPOS"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool EsModeloMultiple(int IdModelo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_ES_MODELO_MULTIPLE";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.Add("@RPTA", SqlDbType.VarChar,20).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        string rpta = Convert.ToString(cmd.Parameters["@RPTA"].Value);

                        if (rpta == "NO")
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

        //---------------------------------------------------------------------------
        //Ha sido desestimada
        //---------------------------------------------------------------------------
        public DataTable GetCartelGuiaImpr(int IdCartel,
                                           int IdModelo,
                                           int IdGuia,
                                           int IdLinea,
                                           int IdCategoria,
                                           int IdPromocion)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GET_CARTEL_GUIA_IMPR_1";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);   

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

        //---------------------------------------------------------------------------
        //Ha sido desestimada
        //---------------------------------------------------------------------------
        //public DataTable GetCartelGuiaImpr(int IdCartel,
        //                                   int IdModelo,
        //                                   int IdGuia,                                           
        //                                   int IdCategoria,
        //                                   int IdPromocion)
        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
        //        {
        //            cn.Open();

        //            string sql = "SP_SCE_GET_CARTEL_GUIA_IMPR_2";

        //            using (SqlCommand cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
        //                cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
        //                cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        
        //                cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
        //                cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);

        //                SqlDataReader reader = cmd.ExecuteReader();

        //                dt.Load(reader);

        //                return dt;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable GetCamposObligatoriosCP(int IdCategoria, int IdPromocion)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "GET_CAMPOS_OBLIGATORIOS_CAT_PROM";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);

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

        public int GetGuiaVigencia(int IdGuia)
        {
            int y = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_GUIA_GET_VIGENCIA";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.Add("@VIGENTE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        return y = Convert.ToInt32(cmd.Parameters["@VIGENTE"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*-------------------------------------------------------------------------------------------*/
        /* MANEJO DE LAS EXEPCIONES DE LAS GUIAS AUTOMATICAS*/
        /*-------------------------------------------------------------------------------------------*/
        public bool GuiaTieneExepciones(int IdGuia)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlCartel = "SP_SCE_GUIA_TIENE_EXEPCIONES";

                    using (SqlCommand cmd = new SqlCommand(sqlCartel, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.Add("@RPTA", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bool rpta = Convert.ToBoolean(cmd.Parameters["@RPTA"].Value);

                        return rpta;                       
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarExcepciones(SCE_GUIA_BE BE)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlExepcion1 = "SP_SCE_GUIA_EXEPCIONES_DEL";

                    using (SqlCommand cmd = new SqlCommand(sqlExepcion1, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_EXCEPCION_BE GUIA_EXCEPCION_BE in BE.DETALLE_GUIA_EXCEPCIONES)
                        {
                            cmd.Parameters.Clear();

                            if (GUIA_EXCEPCION_BE.FLAGPERTENECE == 0)
                            {
                                cmd.Parameters.AddWithValue("@ID_GUIA", GUIA_EXCEPCION_BE.ID_GUIA);
                                cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_EXCEPCION_BE.ID_LINEA);
                                cmd.Parameters.AddWithValue("@ID_TIENDA", GUIA_EXCEPCION_BE.ID_TIENDA);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    string sqlExepcion3 = "SP_SCE_EXISTE_EXEPCION";                    

                    using (SqlCommand cmd = new SqlCommand(sqlExepcion3, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SCE_GUIA_EXCEPCION_BE GUIA_EXCEPCION_BE in BE.DETALLE_GUIA_EXCEPCIONES)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_GUIA", GUIA_EXCEPCION_BE.ID_GUIA);
                            cmd.Parameters.AddWithValue("@ID_LINEA", GUIA_EXCEPCION_BE.ID_LINEA);
                            cmd.Parameters.AddWithValue("@ID_TIENDA", GUIA_EXCEPCION_BE.ID_TIENDA);
                            cmd.Parameters.Add("@EXISTE", SqlDbType.Bit).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            bool Existe = Convert.ToBoolean(cmd.Parameters["@EXISTE"].Value);

                            if (GUIA_EXCEPCION_BE.FLAGPERTENECE == 1)
                            {
                                if (Existe == false)
                                {
                                    string sqlExepcion2 = "SP_SCE_GUIA_EXEPCIONES_INS";

                                    using (SqlCommand cmd1 = new SqlCommand(sqlExepcion2, cn))
                                    {
                                        cmd1.CommandType = CommandType.StoredProcedure;

                                        cmd1.Parameters.Clear();
                                        cmd1.Parameters.AddWithValue("@ID_GUIA", GUIA_EXCEPCION_BE.ID_GUIA);
                                        cmd1.Parameters.AddWithValue("@ID_LINEA", GUIA_EXCEPCION_BE.ID_LINEA);
                                        cmd1.Parameters.AddWithValue("@ID_TIENDA", GUIA_EXCEPCION_BE.ID_TIENDA);
                                        cmd1.Parameters.AddWithValue("@USER_MOD", GUIA_EXCEPCION_BE.USER_MOD);
                                        cmd1.ExecuteNonQuery();
                                    }
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

        public List<SCE_GUIA_EXCEPCION_BE> ListarExecepcionesPV(int IdGuia, int IdGrupo)
        {
            List<SCE_GUIA_EXCEPCION_BE> lstBE = new List<SCE_GUIA_EXCEPCION_BE>();
            SCE_GUIA_EXCEPCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_EXEPCIONES_SEL";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_GRUPO", IdGrupo);

                        cmd.CommandTimeout = 1200;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_EXCEPCION_BE();

                            BE.ID_GUIA = Convert.ToInt32(reader["ID_GUIA"]);
                            BE.ID_LINEA = Convert.ToInt32(reader["ID_LINEA"]);
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.ID_GRUPO = Convert.ToInt32(reader["ID_GRUPO"]);
                            BE.VALOR = Convert.ToString(reader["VALOR"]);
                            BE.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
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

        public List<SCE_GUIA_EXCEPCION_BE> GetDetTiendasExecepcion(int IdGuia, int IdLinea)
        {
            List<SCE_GUIA_EXCEPCION_BE> lstBE = new List<SCE_GUIA_EXCEPCION_BE>();
            SCE_GUIA_EXCEPCION_BE BE = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SP_SCE_GUIA_EXEPCIONES_GET";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            BE = new SCE_GUIA_EXCEPCION_BE();
                           
                            BE.ID_TIENDA = Convert.ToInt32(reader["ID_TIENDA"]);
                            BE.NOM_TIENDA = Convert.ToString(reader["NOM_TIENDA"]);
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


        /*-------------------------------------------------------------------------------------------*/
        /* GENERACION DE LA PLANTILLA DE CARGA MASIVA PARA LAS GUIAS AUTOMATICAS*/
        /*-------------------------------------------------------------------------------------------*/
        public DataTable ListarPVCamposPlantilla()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SCE_CAMPO_PLANTILLA_LISTAR_PV";

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

        public DataTable ListarCategoriasPlantilla()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SCE_CATEGORIA_PLANTILLA_LISTAR";

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

        public DataTable ListarPromocionesPlantilla()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SCE_PROMOCION_PLANTILLA_LISTAR";

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

        public DataTable ListarCamposPlantilla()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sql = "SCE_CAMPOS_PLANTILLA_LISTAR";

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

        public void InsertarLogGuia(string Usuario, int IdGuia, int NroCopias, int IdTienda,
                                      int IdCategoria, int IdPromocion, int IdCartel, int IdModelo, int Digitos)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();

                    string sqlGuia = "SP_SCE_LOG_IMPRESION_INS";

                    using (SqlCommand cmd = new SqlCommand(sqlGuia, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@USUARIO", Usuario);
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@NRO_COPIAS", NroCopias);
                        cmd.Parameters.AddWithValue("@ID_TIENDA", IdTienda);
                        cmd.Parameters.AddWithValue("@ID_CATEGORIA", IdCategoria);
                        cmd.Parameters.AddWithValue("@ID_PROMOCION", IdPromocion);
                        cmd.Parameters.AddWithValue("@ID_CARTEL", IdCartel);
                        cmd.Parameters.AddWithValue("@ID_MODELO", IdModelo);
                        cmd.Parameters.AddWithValue("@DIGITOS", Digitos);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public string GetSkuProducto_Campo(int IdGuia, int IdLinea)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(SCE_SQLCONEXION.GetCadConexion(usrLogin)))
                {
                    cn.Open();
                    string sql = "SP_SCE_GUIA_DET_CAMPO_SKU_GET";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GUIA", IdGuia);
                        cmd.Parameters.AddWithValue("@ID_LINEA", IdLinea);
                        SqlDataReader reader = cmd.ExecuteReader();
                        string result=string.Empty;
                        while (reader.Read())
                        {
                            result = reader["VALOR"].ToString();
                        }

                        return result;
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
