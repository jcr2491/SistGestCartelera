using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using SistGestCart.BE;
using SistGestCart.DA;
using pe.oechsle.Entity;
using System.Globalization;
using System.Threading;
/* Office 11 para Office 2003, 
 * Office 12 para Office 2007 de Visual Studio 2008 y 
 * Office 14 para Office 2010 de Visual Studio 2010 */
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
/* OpenXML 2.0 para Office 2007 - 2010 */
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

/* ClosedXML v0.69.1.0 para OpenXML 2.0 */
using ClosedXML.Excel;


namespace SistGestCart.BL
{
    [Serializable]
    public class SCE_GUIA_BL
    {
        Usuario usrLogin;

        SCE_GUIA_BE BE;
        SCE_CARTEL_MODELO_BL CARTEL_MODELO_BL;

        private static readonly string Formato = System.Configuration.ConfigurationManager.AppSettings["RutaFisicaSitioWeb"] + "{0}";
        private static readonly string RutaLogStepsPI = String.Format(Formato, System.Configuration.ConfigurationManager.AppSettings["RutaLogStepsPI"]);
        private static readonly object _syncLockRegistrarError = new Object();

        Hashtable myHashtable;
        int MyExcelProcessId;

        public SCE_GUIA_BL()
        {
            
        }

        public SCE_GUIA_BL(Usuario usrLogin)
        {
            this.usrLogin = usrLogin;           
        }

        public static void RegLogStepsPrintProcess(string titulo,
                                                   string usuario,
                                                   int local,
                                                   string Cartel,
                                                   int copias,
                                                   string msgExepcion)
        {
            lock (_syncLockRegistrarError)
            {
                String ruta = String.Format(RutaLogStepsPI, DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));

                try
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ruta, true))
                    {
                        writer.WriteLine("*******************************");
                        writer.WriteLine();
                        writer.WriteLine(String.Format("Titulo  :  {0}", titulo));
                        writer.WriteLine(String.Format("Fecha   :  {0}", DateTime.Now));
                        writer.WriteLine(String.Format("Usuario :  {0}", usuario));
                        writer.WriteLine(String.Format("Local   :  {0}", local));
                        writer.WriteLine(String.Format("Cartel  :  {0}", Cartel));
                        writer.WriteLine(String.Format("Copias :  {0}", copias));
                        writer.WriteLine(String.Format("msgError :  {0}", msgExepcion));
                        writer.WriteLine();
                        writer.WriteLine("*******************************");
                        writer.WriteLine();

                        writer.Flush();
                        writer.Close();

                        writer.Close();
                    }
                }
                catch
                {

                }
            }
        }

        public static void RegLogStepsPrintProcess2(string titulo,
                                                string usuario,
                                                int local,
                                                string Cartel,
                                                int copias,
                                                string msgExepcion)
        {
            lock (_syncLockRegistrarError)
            {
                String ruta = String.Format(RutaLogStepsPI,
                                            DateTime.Now.Year.ToString("0000") +
                                            DateTime.Now.Month.ToString("00") +
                                            DateTime.Now.Day.ToString("00"));

                try
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ruta, true))
                    {
                        string sb = String.Format("{0,-70}", titulo) + " " +
                                    String.Format("{0,-24}", DateTime.Now) + " " +
                                    String.Format("{0,-25}", usuario) + " " +
                                    String.Format("{0,-4}", local) + " " +
                                    String.Format("{0,-35}", Cartel) + " " +
                                    String.Format("{0,-3}", copias) + " " +
                                    msgExepcion;

                        writer.WriteLine(sb);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch
                {

                }
            }
        }

        public int GetMaxLineaDetalleGuia(int IdGuia)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetMaxLineaDetalleGuia(IdGuia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*****************************************************************************/
        /**METODOS PARA EL CONTROL DE CONCURRENCIA DE USUARIOS EN LAS GUIAS MANUALES**/
        /*****************************************************************************/
        /************METODOS PARA EL CONTROL DE SESSIONES DE USUARIO AL SISTEMA*******/
        //public int ExisteUsuarioLogueado(string usuario)
        //{
        //    try
        //    {
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        return DA.ExisteUsuarioLogueado(usuario);
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
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        DA.InsertarUsuarioEnTablaCtrol(usuario);
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
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        DA.EliminarUsuarioEnTablaCtrol(usuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /*******************METODOS PARA EL CONTROL DE CORRELATIVOS*******************/
        //public int GetMaxLineaDetalleGuiaUser(int IdGuia, string usuario, int IdTienda)
        //{
        //    try
        //    {
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        return DA.GetMaxLineaDetalleGuiaUser(IdGuia, usuario, IdTienda);
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
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        DA.EliminarMaxLineaDetalleGuiaUser(IdGuia, IdLinea, usuario);
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
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        DA.EliminarMaxLineaDetalleGuiaUserExt(usuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int GetLineaDetalleGuiaUser(int IdGuia, string usuario, int IdTienda)
        //{
        //    try
        //    {
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        return DA.GetLineaDetalleGuiaUser(IdGuia, usuario, IdTienda);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /*****************************************************************************/
        /*****************************************************************************/

        //Recarga del metodo para guias manuales
        public double InsertarGuia(string nomGuia,
                                   int TipGuia,
                                   DateTime FecIni,
                                   DateTime FecFin,
                                   int IdTienda,
                                   int IdGrupo,
                                   string Usuario,                                   
                                   List<SCE_GUIA_DET_BE> lstGuiaDet,
                                   List<SCE_GUIA_DET_CAMPO_BE> lstGuiaDetCampo)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            options.Timeout = new TimeSpan(0, 2, 0);

            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options);

            try
            {
                BE = new SCE_GUIA_BE();

                BE.NOM_GUIA = nomGuia;
                BE.TIPO_GUIA = TipGuia;
                BE.ESTADO_GUIA = 1;
                BE.FECHA_INI = FecIni;
                BE.FECHA_FIN = FecFin;
                BE.ID_TIENDA = IdTienda;
                BE.ID_GRUPO = IdGrupo;
                BE.USER_CRE = Usuario;                
                BE.DETALLE_GUIA = lstGuiaDet;
                BE.DETALLE_GUIA_CAMPOS = lstGuiaDetCampo;

                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                return DA.InsertarGuia(BE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scope.Complete();
                scope.Dispose();
            }
        }

        //Recarga del metodo para guias automaticas
        public double InsertarGuia(string nomGuia,
                                   int TipGuia,                  
                                   DateTime FecIni,
                                   DateTime FecFin,
                                   int IdTienda,
                                   int IdGrupo,
                                   string Usuario,
                                   List<SCE_GUIA_DET_BE> lstGuiaDet,
                                   List<SCE_GUIA_DET_CAMPO_BE> lstGuiaDetCampo,
                                   int idFile)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            options.Timeout = new TimeSpan(0, 2, 0);

            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options);

            try
            {
                //Da de baja al archivo Excel de la Guia
                DA.SCE_FILE_GUIA_MASIVA_DA DA0 = new DA.SCE_FILE_GUIA_MASIVA_DA(usrLogin);
                DA0.BajaFileGuia(idFile, Usuario);

                //Graba los datos y el contenido de la guia de la Guia
                BE = new SCE_GUIA_BE();
                BE.NOM_GUIA = nomGuia;
                BE.TIPO_GUIA = TipGuia;
                BE.ESTADO_GUIA = 1;
                BE.FECHA_INI = FecIni;
                BE.FECHA_FIN = FecFin;
                BE.ID_TIENDA = IdTienda;
                BE.ID_GRUPO = IdGrupo;
                BE.USER_CRE = Usuario;
                BE.USER_MOD = Usuario;
                BE.DETALLE_GUIA = lstGuiaDet;
                BE.DETALLE_GUIA_CAMPOS = lstGuiaDetCampo;

                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.InsertarGuia(BE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scope.Complete();
                scope.Dispose();
            }
        }

        public void ActualizarGuia(int IdGuia,
                                   string nomGuia,                                    
                                   DateTime FecIni,
                                   DateTime FecFin,
                                   int IdTienda,
                                   int IdGrupo,
                                   int EstadoGuia,
                                   string Usuario,
                                   List<SCE_GUIA_DET_BE> lstGuiaDet,
                                   List<SCE_GUIA_DET_CAMPO_BE> lstGuiaDetCampo)
        {
            try
            {
                BE = new SCE_GUIA_BE();

                BE.ID_GUIA = IdGuia;
                BE.NOM_GUIA = nomGuia;               
                BE.ESTADO_GUIA = EstadoGuia;
                BE.FECHA_INI = FecIni;
                BE.FECHA_FIN = FecFin;
                BE.ID_TIENDA = IdTienda;
                BE.ID_GRUPO = IdGrupo;
                BE.USER_MOD = Usuario;
                BE.DETALLE_GUIA = lstGuiaDet;
                BE.DETALLE_GUIA_CAMPOS = lstGuiaDetCampo;

                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = new TimeSpan(0, 2, 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    DA.ActualizarGuia(BE);
                    scope.Complete();
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
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                DA.ActualizarEstadoGuia(IdGuia);                    
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public void EliminarGuiaManual(int IdGuia)
        {
            try
            {
                BE = new SCE_GUIA_BE();
                BE.ID_GUIA = IdGuia;
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = new TimeSpan(0, 2, 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    DA.EliminarGuiaManual(BE);
                    scope.Complete();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EliminarGuiaAutomatica(int IdGuia, int EstGuia)
        {
            string msje = string.Empty;

            try
            {
                BE = new SCE_GUIA_BE();
                BE.ID_GUIA = IdGuia;
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = new TimeSpan(0, 2, 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    if (EstGuia == 1)
                    {
                        if (DA.GuiaTieneExepciones(IdGuia) == false)
                        {
                            DA.EliminarGuiaAutomatica(BE);
                            scope.Complete();
                        }
                        else
                        {
                            msje = System.Configuration.ConfigurationManager.AppSettings["GEX_IR"];
                        }
                    }
                    else if (EstGuia == 2)
                    {
                        DA.EliminarGuiaAutomatica(BE);
                        scope.Complete();
                    }
                }

                return msje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_GUIA_BE> Listar()
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        //BUSQUEDA PARA GUIAS MANUALES
        public List<SCE_GUIA_BE> Buscar(Nullable<DateTime> FecIniVig,
                                        Nullable<DateTime> FecFinVig,
                                        int IdTienda,
                                        int IdPromocion,
                                        int IdCategoria,
                                        int Estado,
                                        string NomGuia,
                                        bool lastPage,
                                        int PageSize,
                                        int PageNumber,
                                        ref int PageCount)
        {

            string FecIniVig1;
            string FecFinVig1;
            long FecIniVig2;
            long FecFinVig2;

            if (FecIniVig != null)
            {
                FecIniVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecIniVig));
                FecIniVig1 = FecIniVig1.ToString().Replace("-", "");
                FecIniVig2 = Convert.ToInt64(FecIniVig1);
            }
            else
            {
                FecIniVig1 = null;
                FecIniVig2 = 0;
            }

            if (FecFinVig != null)
            {
                FecFinVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecFinVig));
                FecFinVig1 = FecFinVig1.ToString().Replace("-", "");
                FecFinVig2 = Convert.ToInt64(FecFinVig1);
            }
            else
            {
                FecFinVig1 = null;
                FecFinVig2 = 0;
            }

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.Buscar(FecIniVig2,
                                 FecFinVig2,
                                 IdTienda,
                                 0, // No Filtra por Grupo
                                 IdPromocion,
                                 IdCategoria,
                                 Estado,
                                 NomGuia,
                                 1, // Guia Manual
                                 lastPage,
                                 PageSize,
                                 PageNumber,
                                 ref PageCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //BUSQUEDA PARA GUIAS AUTOMATICAS
        public List<SCE_GUIA_BE> Buscar(Nullable<DateTime> FecIniVig,
                                        Nullable<DateTime> FecFinVig,
                                        int IdGrupo,
                                        int Estado,
                                        string NomGuia,
                                        bool lastPage,
                                        int PageSize,
                                        int PageNumber,
                                        ref int PageCount)
        {
            string FecIniVig1;
            string FecFinVig1;
            long FecIniVig2;
            long FecFinVig2;

            if (FecIniVig != null)
            {
                FecIniVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecIniVig));
                FecIniVig1 = FecIniVig1.ToString().Replace("-", "");
                FecIniVig2 = Convert.ToInt64(FecIniVig1);
            }
            else
            {
                FecIniVig1 = null;
                FecIniVig2 = 0;
            }

            if (FecFinVig != null)
            {
                FecFinVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecFinVig));
                FecFinVig1 = FecFinVig1.ToString().Replace("-", "");
                FecFinVig2 = Convert.ToInt64(FecFinVig1);
            }
            else
            {
                FecFinVig1 = null;
                FecFinVig2 = 0;
            }

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.Buscar(FecIniVig2,
                                 FecFinVig2,
                                 0, // No filtra por tienda o local
                                 IdGrupo,
                                 Estado,
                                 NomGuia,
                                 2, // Guia automatica
                                 lastPage,
                                 PageSize,
                                 PageNumber,
                                 ref PageCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //BUSQUEDA PARA GUIAS AUTOMATICAS CON EXEPCIONES
        public List<SCE_GUIA_BE> BuscarE(Nullable<DateTime> FecIniVig,
                                         Nullable<DateTime> FecFinVig,
                                         int IdGrupo,
                                         string NomGuia,
                                         bool lastPage,
                                         int PageSize,
                                         int PageNumber,
                                         ref int PageCount)
        {
            string FecIniVig1;
            string FecFinVig1;
            long FecIniVig2;
            long FecFinVig2;

            if (FecIniVig != null)
            {
                FecIniVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecIniVig));
                FecIniVig1 = FecIniVig1.ToString().Replace("-", "");
                FecIniVig2 = Convert.ToInt64(FecIniVig1);
            }
            else
            {
                FecIniVig1 = null;
                FecIniVig2 = 0;
            }

            if (FecFinVig != null)
            {
                FecFinVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(FecFinVig));
                FecFinVig1 = FecFinVig1.ToString().Replace("-", "");
                FecFinVig2 = Convert.ToInt64(FecFinVig1);
            }
            else
            {
                FecFinVig1 = null;
                FecFinVig2 = 0;
            }

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.BuscarE(FecIniVig2,
                                  FecFinVig2,
                                  0, //No Flitra por tienda
                                  IdGrupo,
                                  1, //Guia en Trabajo
                                  NomGuia,
                                  2, //Guia Automatica
                                  lastPage,
                                  PageSize,
                                  PageNumber,
                                  ref PageCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //BUSQUEDA PARA MODULO DE BUSQUEDA DE GUIAS E IMPRESIONES
        public List<SCE_LIST_GUIA> BuscarImpr(Nullable<DateTime> fecIniVig,
                                              Nullable<DateTime> fecFinVig,
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
            string fecIniVig1;
            string fecFinVig1;
            long fecIniVig2;
            long fecFinVig2;

            SCE_LIST_GUIA LIST_GUIA;
            List<SCE_GUIA_BE> lstGuia = new List<SCE_GUIA_BE>();
            List<SCE_LIST_GUIA> lstListGuia = new List<SCE_LIST_GUIA>();

            if (fecIniVig != null)
            {
                fecIniVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fecIniVig));
                fecIniVig1 = fecIniVig1.ToString().Replace("-", "");
                fecIniVig2 = Convert.ToInt64(fecIniVig1);
            }
            else
            {
                fecIniVig1 = null;
                fecIniVig2 = 0;
            }

            if (fecFinVig != null)
            {
                fecFinVig1 = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fecFinVig));
                fecFinVig1 = fecFinVig1.ToString().Replace("-", "");
                fecFinVig2 = Convert.ToInt64(fecFinVig1);
            }
            else
            {
                fecFinVig1 = null;
                fecFinVig2 = 0;
            }

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                lstGuia = DA.BuscarImpr(fecIniVig2,
                                        fecFinVig2,
                                        IdTienda,
                                        IdPromocion,
                                        IdCategoria,
                                        NomGuia,
                                        TipGuia,
                                        lastPage,
                                        PageSize,
                                        PageNumber,
                                        ref PageCount);


                for (int i = 0; i < lstGuia.Count; i++)
                {
                    LIST_GUIA = new SCE_LIST_GUIA();

                    LIST_GUIA.ID_GUIA = lstGuia[i].ID_GUIA.ToString("0000");
                    LIST_GUIA.NOM_GUIA = lstGuia[i].NOM_GUIA.ToString();
                    LIST_GUIA.DES_TIPO_GUIA = lstGuia[i].DES_TIPO_GUIA.ToString();
                    LIST_GUIA.ID_PROMOCION = Convert.ToInt32(lstGuia[i].ID_PROMOCION.ToString());
                    LIST_GUIA.ID_CATEGORIA = Convert.ToInt32(lstGuia[i].ID_CATEGORIA.ToString());
                    LIST_GUIA.NOM_CATEGORIA = lstGuia[i].NOM_CATEGORIA.ToString();
                    LIST_GUIA.FECHA_INI = Convert.ToDateTime(lstGuia[i].FECHA_INI.ToString());
                    LIST_GUIA.FECHA_FIN = Convert.ToDateTime(lstGuia[i].FECHA_FIN.ToString());
                    LIST_GUIA.ID_TIENDA = Convert.ToInt32(lstGuia[i].ID_TIENDA.ToString());
                    LIST_GUIA.NOM_TIENDA = lstGuia[i].NOM_TIENDA.ToString();
                    LIST_GUIA.ID_GRUPO = Convert.ToInt32(lstGuia[i].ID_GRUPO.ToString());
                    LIST_GUIA.NOM_GRUPO = lstGuia[i].NOM_GRUPO.ToString();

                    DateTime FecActual = DateTime.Today;

                    if ((LIST_GUIA.FECHA_INI <= FecActual) && (LIST_GUIA.FECHA_FIN >= FecActual))
                    {
                        LIST_GUIA.VIGENCIA = "VIGENTE";
                    }
                    else
                    {
                        LIST_GUIA.VIGENCIA = "NO VIGENTE";
                    }

                    lstListGuia.Add(LIST_GUIA);
                }

                return lstListGuia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public System.Data.DataTable GetNewRegDetalleGuiaCampo(int IdPromocion, int IdCategoria)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);                        
                return DA.GetNewRegDetalleGuiaCampo(IdPromocion, IdCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetDetalleGuia(int IdGuia)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetDetalleGuia(IdGuia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetDetalleGuiaCampo(int IdGuia)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetDetalleGuiaCampo(IdGuia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetDetalleGuiaCampoAutomatico(int IdGuia)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetDetalleGuiaCampoAutomatico(IdGuia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetDetalleGuiaCampoImpr(int IdGuia, 
                                                             int IdCategoria, 
                                                             int IdPromocion,
                                                             int IdTienda,
                                                             string strTipGuia)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                if (strTipGuia == "MANUAL")
                {
                    dt = DA.GetDetalleGuiaCampoImprManual(IdGuia,
                                                          IdCategoria, 
                                                          IdPromocion);
                }
                else if (strTipGuia == "AUTOMATICA")
                {
                    dt = DA.GetDetalleGuiaCampoImprAutomatica(IdGuia,
                                                              IdCategoria, 
                                                              IdPromocion, 
                                                              IdTienda);
                }

                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetDetalleGuiaCampo1(int IdGuia, int IdLinea)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetDetalleGuiaCampo1(IdGuia, IdLinea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public System.Data.DataTable GetCartelGuiaImpr(int IdCartel,
                                                       int IdModelo,
                                                       int Digitos,
                                                       int IdGuia, 
                                                       int IdLinea)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCartelGuiaImpr(IdCartel,
                                            IdModelo,
                                            Digitos,
                                            IdGuia,
                                            IdLinea);
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
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCoordenadaXFinCuadrante(IdCartel,
                                                     IdModelo,
                                                     Digitos);
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
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCoordenadaYFinCuadrante(IdCartel,
                                                     IdModelo, 
                                                     Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNroTotalColumnasPlantilla(string IdCartelModelo)
        {
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetNroTotalColumnasPlantilla(IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNroTotalFilasPlantilla(string IdCartelModelo)
        {
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetNroTotalFilasPlantilla(IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public System.Data.DataTable GetCoordenadasPlantilla(int IdCartel,
                                                             int IdModelo,
                                                             int Digitos)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCoordenadasPlantilla(IdCartel,
                                                  IdModelo,
                                                  Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNroCamposPlantilla(string IdCartelModelo)
        {
            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                //return DA.GetNroCamposPlantilla(IdCartel, IdModelo);
                return DA.GetNroCamposPlantilla(IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EsModeloMultiple(string IdCartelModelo)
        {
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.EsModeloMultiple(IdModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EsVigente(int IdGuia)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                if (DA.GetGuiaVigencia(IdGuia) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------------------------------------------------------------
        //Maneja los datos de las previsualizaciones de carteles PDF de las guias manuales 
        //y automaticas HA SIDO DESESTIMADA
        //--------------------------------------------------------------------------------
        public System.Data.DataTable GetCartelGuiaImpr(int IdCartel,
                                                       int IdModelo,
                                                       int IdGuia,
                                                       int IdLinea,
                                                       int IdCategoria,
                                                       int IdPromocion)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCartelGuiaImpr(IdCartel,
                                            IdModelo,
                                            IdGuia,
                                            IdLinea,
                                            IdCategoria,
                                            IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        //Maneja los datos de la generacion de Carteles PDF de las impresiones masivas
        //HA SIDO DESESTIMADA
        //--------------------------------------------------------------------------------
        //public System.Data.DataTable GetCartelGuiaImpr(int IdCartel,
        //                                               int IdModelo,
        //                                               int IdGuia,                                                       
        //                                               int IdCategoria,
        //                                               int IdPromocion)
        //{
        //    try
        //    {
        //        DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
        //        return DA.GetCartelGuiaImpr(IdCartel,
        //                                    IdModelo,
        //                                    IdGuia,
        //                                    IdCategoria,
        //                                    IdPromocion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //--------------------------------------------------------------------------------

        public System.Data.DataTable GetCamposObligatoriosCP(int IdCategoria, int IdPromocion)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetCamposObligatoriosCP(IdCategoria, IdPromocion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetAllCampos()
        {
            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                return DA.GetAllCampos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable CalculaCoordenadasCartelesMultiples(string IdCartelModelo, 
                                                                         System.Data.DataTable dtRegImpresion,
                                                                         ref int NroPaginas)
        {
            int cContCdrantes = 1;

            // Filtro por Id de Cartel que se ha seleccionado
            System.Data.DataTable dtAux = new System.Data.DataTable();
            string strCrtFiltro = "ID_CARTEL_MODELO = " + IdCartelModelo;
            dtAux = FiltraDataTable(dtRegImpresion, strCrtFiltro);

            /**********************************************************************************************/
            // Obtenemos los campos de la plantilla de la cartel
            /**********************************************************************************************/
            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());
            System.Data.DataTable dtCoordPlantilla = GetCoordenadasPlantilla(IdCartel, IdModelo, Digitos);

            string strCritFilAx = string.Empty;

            //Armamos la el criterio de filtro dinamicamente
            for (int x = 0; x < dtCoordPlantilla.Rows.Count; x++)
            {
                strCritFilAx = strCritFilAx + "ID_CAMPO <> " + dtCoordPlantilla.Rows[x][2] + " AND ";                
            }

            strCritFilAx = strCritFilAx.Substring(0, strCritFilAx.Length - 5).ToString();

            //Realiza este proceso de calculo de coordenadas y determinacion de hoja a la que
            //pertenece la coordenada calculada, si el modelo es de tipo Multiple CUCARDA
            if (dtAux.Rows.Count > 0)
            {
                /******************************************************/
                //Eliminamos el grupo de registros no validos
                /******************************************************/
                //DataRow[] DrArrDel = dtAux.Select("VALOR = ''");
                DataRow[] DrArrDel = dtAux.Select(strCritFilAx);
                foreach (DataRow DrDel in DrArrDel)
                {
                    dtAux.Rows.Remove(DrDel);
                }
                /*****************************************************/

                int NCOLUMNAS = Convert.ToInt32(GetNroTotalColumnasPlantilla(IdCartelModelo));
                int NFILAS = Convert.ToInt32(GetNroTotalFilasPlantilla(IdCartelModelo));

                int NCAMPOS = Convert.ToInt32(GetNroCamposPlantilla(IdCartelModelo));

                int nFila = 1;
                int nColumna = 1;

                //int cContCdrantes = 1;

                int cContCamposFil = 1;
                int cContCamposCol = 1;

                int cContSheet = 1;

                for (int irp = 0; irp < dtAux.Rows.Count; irp++)
                {
                    if (cContCamposCol > NCAMPOS)
                    {
                        //--------------------------------------------
                        //Esta logica es muy forzada se utiliza
                        //temporalmente has encontrar una mas generica
                        //--------------------------------------------
                        if (NCOLUMNAS == 5)
                        {
                            nColumna = 5;
                        }
                        else if (NCOLUMNAS == 4)
                        {
                            nColumna = 4;
                        }
                        else if (NCOLUMNAS == 3)
                        {
                            nColumna = 3;
                        }
                        else if (NCOLUMNAS == 2)
                        {
                            nColumna = 2;
                        }
                        else if (NCOLUMNAS == 1)
                        {
                            nColumna = 1;
                        }
                        //--------------------------------------------

                        cContCamposCol = 1;

                        cContCdrantes = cContCdrantes + 1;
                    }

                    if (cContCamposFil > nColumna * NCAMPOS)
                    {
                        nFila = nFila + 1;

                        cContCamposFil = 1;

                        nColumna = 1;
                    }

                    if (cContCdrantes <= (NCOLUMNAS * NFILAS))
                    {
                        dtAux.Rows[irp][5] = GetCoordenadaX(Convert.ToString(dtAux.Rows[irp][0].ToString()),
                                                            Convert.ToInt32(dtAux.Rows[irp][2]),
                                                            nFila);

                        dtAux.Rows[irp][6] = GetCoordenadaY(Convert.ToString(dtAux.Rows[irp][0].ToString()),
                                                            Convert.ToInt32(dtAux.Rows[irp][2]),
                                                            nColumna);

                        dtAux.Rows[irp][7] = "Hoja" + cContSheet;

                    }
                    else if (cContCdrantes > (NCOLUMNAS * NFILAS))
                    {
                        cContCdrantes = 1;

                        cContCamposFil = 1;
                        cContCamposCol = 1;

                        nFila = 1;
                        nColumna = 1;

                        dtAux.Rows[irp][5] = GetCoordenadaX(Convert.ToString(dtAux.Rows[irp][0].ToString()),
                                                            Convert.ToInt32(dtAux.Rows[irp][2]),
                                                            nFila);

                        dtAux.Rows[irp][6] = GetCoordenadaY(Convert.ToString(dtAux.Rows[irp][0].ToString()),
                                                            Convert.ToInt32(dtAux.Rows[irp][2]),
                                                            nColumna);

                        cContSheet = cContSheet + 1;

                        dtAux.Rows[irp][7] = "Hoja" + cContSheet;

                    }

                    dtAux.AcceptChanges();

                    if (dtAux.Rows[irp][5].ToString() != "" && dtAux.Rows[irp][6].ToString() != "")
                    {
                        cContCamposFil = cContCamposFil + 1;
                        cContCamposCol = cContCamposCol + 1;
                    }
                }

                // Depura las coordenadas en blanco si es que las tuviera
                // Eliminamos el grupo de registros no validos
                DataRow[] DrArrDelNull = dtAux.Select("POSX = ''");
                foreach (DataRow DrDelNull in DrArrDelNull)
                {
                    dtAux.Rows.Remove(DrDelNull);
                }

                NroPaginas = cContSheet;
            }            

            return dtAux;
        }

        public string GetCoordenadaX(string IdCartelModelo, int IdCampo, int nFila)
        {
            string bReturn = string.Empty;

            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());

            try
            {
                string xFC = GetCoordenadaXFinCuadrante(IdCartel, IdModelo, Digitos);

                System.Data.DataTable dtCoordPlantilla = GetCoordenadasPlantilla(IdCartel, IdModelo, Digitos);

                if (GetCXP(dtCoordPlantilla, IdCampo) == "")
                {
                    bReturn = "";
                }
                else
                {
                    bReturn = Convert.ToString(Convert.ToInt32(GetCXP(dtCoordPlantilla, IdCampo)) + Convert.ToInt32(xFC) * (nFila - 1));
                }

                return bReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCoordenadaY(string IdCartelModelo, int IdCampo, int nColumna)
        {
            string bReturn = string.Empty;

            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());

            try
            {
                string yFC = GetCoordenadaYFinCuadrante(IdCartel, IdModelo, Digitos);

                System.Data.DataTable dtCoordPlantilla = GetCoordenadasPlantilla(IdCartel, IdModelo, Digitos);

                if (GetCYP(dtCoordPlantilla, IdCampo) == "")
                {
                    bReturn = "";
                }
                else
                {
                    bReturn = Convert.ToString(Convert.ToInt32(GetCYP(dtCoordPlantilla, IdCampo)) + Convert.ToInt32(yFC) * (nColumna - 1));
                }

                return bReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetCXP(System.Data.DataTable dtCoordPlantilla, int IdCampo)
        {
            string bReturn = string.Empty;

            for (int i = 0; i < dtCoordPlantilla.Rows.Count; i++)
            {
                if (Convert.ToString(IdCampo) == dtCoordPlantilla.Rows[i][2].ToString())
                {
                    bReturn = dtCoordPlantilla.Rows[i][3].ToString();
                }
            }

            return bReturn;
        }

        private string GetCYP(System.Data.DataTable dtCoordPlantilla, int IdCampo)
        {
            string bReturn = string.Empty;

            for (int i = 0; i < dtCoordPlantilla.Rows.Count; i++)
            {
                if (Convert.ToString(IdCampo) == dtCoordPlantilla.Rows[i][2].ToString())
                {
                    bReturn = dtCoordPlantilla.Rows[i][4].ToString();
                }
            }

            return bReturn;
        }        

        public System.Data.DataTable PivotDtDetalleGuiaCampos(System.Data.DataTable dt, string columnName)
        {
            try
            {
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable dtPivot = new System.Data.DataTable();

                dtPivot.Columns.Add("ID_LINEA", typeof(int));

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
                //DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (columnName == null || columnName.Length == 0)
                {
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                }

                dt.DefaultView.Sort = "ID_CAMPO";

                System.Data.DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

                for (int i = 0; i < distintos0.Rows.Count; i++)
                {
                    dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
                }

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'LLENA EL DATATABLE PIVOTEADO
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_LINEA");
                DataRow row = null;

                for (int i = 0; i < distintos1.Rows.Count; i++)
                {
                    row = dtPivot.NewRow();

                    row["ID_LINEA"] = distintos1.Rows[i][0];

                    System.Data.DataTable dtAux = new System.Data.DataTable();
                    string strCrtFiltro = null;

                    strCrtFiltro = "ID_LINEA = " + distintos1.Rows[i][0];

                    dtAux = FiltraDataTable(dt, strCrtFiltro);

                    for (int cols = 1; cols < dtPivot.Columns.Count; cols++)
                    {
                        for (int j = 0; j < dtAux.Rows.Count; j++)
                        {
                            if ((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][2].ToString()))
                            {
                                row[cols] = dtAux.Rows[j][3];
                            }
                        }
                    }

                    dtPivot.Rows.Add(row);
                }

                return dtPivot;           
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public System.Data.DataTable PivotDtDetalleGuiaCamposAutomatico(System.Data.DataTable dt, string columnName)
        {
            try
            {
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable dtPivot = new System.Data.DataTable();

                dtPivot.Columns.Add("ID_CATEGORIA", typeof(Int32));
                dtPivot.Columns.Add("CATEGORÍA", typeof(string));
                dtPivot.Columns.Add("ID_PROMOCION", typeof(Int32));
                dtPivot.Columns.Add("PROMOCIÓN", typeof(string));
                dtPivot.Columns.Add("ID_LINEA", typeof(int));

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
                //DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (columnName == null || columnName.Length == 0)
                {
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                }

                dt.DefaultView.Sort = "ID_CAMPO";

                System.Data.DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

                for (int i = 0; i < distintos0.Rows.Count; i++)
                {
                    dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
                }

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'LLENA EL DATATABLE PIVOTEADO
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_CATEGORIA", "CATEGORÍA", "ID_PROMOCION", "PROMOCIÓN", "ID_LINEA");
                DataRow row = null;

                for (int i = 0; i < distintos1.Rows.Count; i++)
                {
                    row = dtPivot.NewRow();

                    row["ID_CATEGORIA"] = distintos1.Rows[i][0];
                    row["CATEGORÍA"] = distintos1.Rows[i][1];
                    row["ID_PROMOCION"] = distintos1.Rows[i][2];
                    row["PROMOCIÓN"] = distintos1.Rows[i][3];
                    row["ID_LINEA"] = distintos1.Rows[i][4];

                    System.Data.DataTable dtAux = new System.Data.DataTable();
                    string strCrtFiltro = null;

                    strCrtFiltro = "ID_CATEGORIA = " + distintos1.Rows[i][0] + " AND " + "ID_PROMOCION = " + distintos1.Rows[i][2] + " AND " + "ID_LINEA = " + distintos1.Rows[i][4];

                    dtAux = FiltraDataTable(dt, strCrtFiltro);

                    for (int cols = 1; cols < dtPivot.Columns.Count; cols++)
                    {
                        for (int j = 0; j < dtAux.Rows.Count; j++)
                        {
                            if ((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][6].ToString()))
                            {
                                row[cols] = dtAux.Rows[j][7];
                            }
                        }
                    }

                    dtPivot.Rows.Add(row);
                }

                return dtPivot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable PivotDtDetalleGuiaCamposImpr(System.Data.DataTable dt, string columnName)
        {
            try
            {
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable dtPivot = new System.Data.DataTable();

                dtPivot.Columns.Add("ID_PROMOCION", typeof(Int32));
                dtPivot.Columns.Add("PROMOCIÓN", typeof(string));
                dtPivot.Columns.Add("ID_LINEA", typeof(int));

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
                //DE LA TABLA PIVOTEADA
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (columnName == null || columnName.Length == 0)
                {
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                }

                dt.DefaultView.Sort = "ID_CAMPO";

                System.Data.DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

                for (int i = 0; i < distintos0.Rows.Count; i++)
                {
                    if ((Convert.ToString(distintos0.Rows[i][0].ToString().Trim()) != "<LEGAL>"))
                    {
                        dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
                    }
                }

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'LLENA EL DATATABLE PIVOTEADO
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_PROMOCION", "PROMOCIÓN", "ID_LINEA");
                DataRow row = null;

                for (int i = 0; i < distintos1.Rows.Count; i++)
                {
                    row = dtPivot.NewRow();

                    row["ID_PROMOCION"] = distintos1.Rows[i][0];
                    row["PROMOCIÓN"] = distintos1.Rows[i][1];
                    row["ID_LINEA"] = distintos1.Rows[i][2];

                    System.Data.DataTable dtAux = new System.Data.DataTable();
                    string strCrtFiltro = null;

                    strCrtFiltro = "ID_PROMOCION = " + distintos1.Rows[i][0] + " AND " + "ID_LINEA = " + distintos1.Rows[i][2];

                    dtAux = FiltraDataTable(dt, strCrtFiltro);

                    for (int cols = 1; cols < dtPivot.Columns.Count; cols++)
                    {
                        for (int j = 0; j < dtAux.Rows.Count; j++)
                        {
                            if ((Convert.ToString(dtPivot.Columns[cols].ToString()) == dtAux.Rows[j][4].ToString()))
                            {
                                row[cols] = dtAux.Rows[j][5];
                            }
                        }
                    }

                    dtPivot.Rows.Add(row);
                }

                return dtPivot;            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        //-------------------------------------------------------------------------------
        //Maneja el proceso de generacion de Carteles PDF de guias manuales y automaticas
        //-------------------------------------------------------------------------------
        public bool ProcesaCartel(string strSourcePath,
                                  string strPlantilla,
                                  string IdCartelModelo,
                                  string nomCartel,
                                  int IdGuia,
                                  int IdLinea,
                                  string loginUser)
        {
            CARTEL_MODELO_BL = new SCE_CARTEL_MODELO_BL(usrLogin);

            string processId = string.Empty;

            string strSourcePathCartel = System.Configuration.ConfigurationManager.AppSettings["PATH_CARTEL"];

            CheckForExistingExcellProcesses();

            ApplicationClass xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;
            Range range = null;
            var misValue = Type.Missing;//System.Reflection.Missing.Value;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());

            int xFC = 0;
            int yFC = 0;

            // obtengo la extencion o version del archivo excel con el que se esta trabajando
            string sExt = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FILE_EXTENCION"];

            try
            {
                //Busca las coordenadas de fin de cuadrante si el cartel es multiple
                if (EsModeloMultiple(IdCartelModelo))
                {
                    if (GetCoordenadaXFinCuadrante(IdCartel, IdModelo, Digitos) != "" && GetCoordenadaYFinCuadrante(IdCartel, IdModelo, Digitos) != "")
                    {
                        xFC = Convert.ToInt32(GetCoordenadaXFinCuadrante(IdCartel, IdModelo, Digitos));
                        yFC = Convert.ToInt32(GetCoordenadaYFinCuadrante(IdCartel, IdModelo, Digitos));
                    }
                    else
                    {
                        throw new Exception("Error... No ha esblecido el Tag de final de cuadrante de diseño del cartel");
                    }
                }                

                // Copio y renombro el archivo
                FileInfo file = new FileInfo(strSourcePath + strPlantilla + sExt);
                file.CopyTo(strSourcePathCartel + nomCartel + loginUser + sExt, true);

                // ABRE EL NUEVO ARCHIVO EXCEL GENERADO A PARTIR DE LA PLANTILLA Y ESCRIBIR
                // EN EL LOS DATOS DE REGISTRO
                string xlsFilePath = strSourcePathCartel + nomCartel + loginUser + sExt;
               
            
                // abrir el documento
                xlApp = new ApplicationClass();
                GetTheExcelProcessIdThatUsedByThisInstance();

                xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                processId = GetProcessId();

                // seleccion de la hoja de calculo
                // get_item() devuelve object y numera las hojas a partir de 1
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // seleccion rango activo
                range = xlWorkSheet.UsedRange;

                // Extraigo los datos con los que se generara el cartel
                System.Data.DataTable dtRegistroImpresion = new System.Data.DataTable();
                dtRegistroImpresion = GetCartelGuiaImpr(IdCartel,
                                                        IdModelo,
                                                        Digitos,
                                                        IdGuia,
                                                        IdLinea);

                // Llenar el archivo con los datos del producto seleccionado
                for (int i = 0; i < dtRegistroImpresion.Rows.Count; i++)
                {
                    xlWorkSheet.Cells[Convert.ToInt32(dtRegistroImpresion.Rows[i][0]), Convert.ToInt32(dtRegistroImpresion.Rows[i][1])] = dtRegistroImpresion.Rows[i][2];
                }

                //Si el cartel tiene cordenadas de fin de cuadrante entonces borrar el flag <?>
                //del cartel que es de tipo CUCARDA
                if (xFC != 0 && yFC != 0)
                {
                    xlWorkSheet.Cells[xFC, yFC] = "";
                }

                // cerrar guardando los cambios
                xlWorkBook.Close(true, misValue, Type.Missing);
                xlApp.Quit();

                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                // CONVIERTE EN PDF EL ARCHIVO DE EXCEL (ARCHIVO RENOMBRADO)
               // ConvertExcelToPdf((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));
                ConvertExcel_to_PDF((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));
                return true;
            }
            catch
            {
                // cerrar guardando los cambios
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();

                // Elimina el archivo creado
                File.Delete(strSourcePathCartel + nomCartel + loginUser + sExt);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();

                return false;
            }
            finally
            {
                // Liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                KillExcelProcessThatUsedByThisInstance();
            }
        }

        /*******************************************************************************************************************************************/
        /*******************************************************************************************************************************************/
        /*******************************************************************************************************************************************/
        /*******************************************************************************************************************************************/
        /*******************************************************************************************************************************************/

        /*******************************************************************************************************************************************/
        // He creado un Anillo para controlarlos a todos jeje
        // Maneja el proceso de generacion de Carteles PDF de Impresiones Masivas de carteles   
        /*******************************************************************************************************************************************/      

        public bool GenerarCartel(string strSourcePath,
                                  string strPlantilla,
                                  string nomCartel,
                                  System.Data.DataTable dtRegistroImpresion,
                                  string loginUser,
                                  int local,
                                  int nroPaginas,
                                  int xFC,
                                  int yFC,
                                  bool EsMultiple)
        {
            /****************************************************************************/
            //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
            //PARA ESTA PARTE DEL CODIGO
            //(ABRIR Y ESCRIBIR EL ARCHIVO EXCEL)
            /****************************************************************************/
            RegLogStepsPrintProcess2("Inicia la escritura del archivo Excel del cartel",
                                   loginUser,
                                   local,
                                   nomCartel,
                                   nroPaginas,
                                   "");
            /****************************************************************************/

            string strSourcePathCartel = System.Configuration.ConfigurationManager.AppSettings["PATH_CARTEL"];

            // obtengo la extencion o version del archivo excel con el que se esta trabajando
            string sExt = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FILE_EXTENCION"];            

            try
            {
                // Copio y renombro el archivo de la plantilla
                FileInfo file = new FileInfo(strSourcePath + strPlantilla + sExt);
                file.CopyTo(strSourcePathCartel + nomCartel + loginUser + sExt, true);

                System.Data.DataTable dtDataAux = new System.Data.DataTable();
                string strCrtFiltro = null;

                // ABRE EL NUEVO ARCHIVO EXCEL GENERADO A PARTIR DE LA PLANTILLA Y ESCRIBIR
                // EN EL LOS DATOS DE REGISTRO
                string xlsFilePath = strSourcePathCartel + nomCartel + loginUser + sExt;

                // Abre el documento Excel para su lectura.
                using (XLWorkbook workbook = new XLWorkbook(xlsFilePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    // Si es multiple 
                    // el tipo de cartel 
                    if (EsMultiple)
                    {
                        // Borra el simbolo <?>
                        if (xFC != 0 && yFC != 0)
                        {
                            workbook.Worksheet(1).Cell(xFC, yFC).Value = "";
                        }
                    }

                    /******************************************************************************************************/
                    //Genera y escribe las siguientes paginas del cartel
                    /******************************************************************************************************/
                    for (int zc = 2; zc <= nroPaginas; zc++)
                    {
                        //Genera la nueva hoja de calculo (worksheet)
                        worksheet.CopyTo("Hoja" + zc);

                        //Filtra el datatable por Nombre de hoja
                        strCrtFiltro = "HOJA = '" + "Hoja" + zc + "'";
                        dtDataAux = FiltraDataTable(dtRegistroImpresion, strCrtFiltro);

                        // Llenar el archivo con los datos del producto seleccionado
                        for (int i = 0; i < dtDataAux.Rows.Count; i++)
                        {
                            workbook.Worksheet("Hoja" + zc).Cell(Convert.ToInt32(dtDataAux.Rows[i][5]),
                                                                 Convert.ToInt32(dtDataAux.Rows[i][6])).Value = "'" + Convert.ToString(dtDataAux.Rows[i][4]);

                            //workbook.Worksheet("Hoja" + zc).Range(GetColReference(Convert.ToInt32(dtDataAux.Rows[i][5]),
                            //                                                      Convert.ToInt32(dtDataAux.Rows[i][6]))).Value = "'" + Convert.ToString(dtDataAux.Rows[i][4]);
                        }
                    }
                    /******************************************************************************************************/

                    /******************************************************************************************************/
                    //Escribe la primera pagina del cartel    
                    /******************************************************************************************************/
                    //Filtra el datatable por Nombre de hoja
                    strCrtFiltro = "HOJA = '" + "Hoja" + 1 + "'";
                    dtDataAux = FiltraDataTable(dtRegistroImpresion, strCrtFiltro);

                    // Llenar el archivo con los datos del producto seleccionado
                    for (int i = 0; i < dtDataAux.Rows.Count; i++)
                    {
                        workbook.Worksheet(1).Cell(Convert.ToInt32(dtDataAux.Rows[i][5]),
                                                   Convert.ToInt32(dtDataAux.Rows[i][6])).Value = "'" + Convert.ToString(dtDataAux.Rows[i][4]);

                        //workbook.Worksheet(1).Range(GetColReference(Convert.ToInt32(dtDataAux.Rows[i][5]),
                        //                                            Convert.ToInt32(dtDataAux.Rows[i][6]))).Value = "'" + Convert.ToString(dtDataAux.Rows[i][4]); 
                    }
                    /******************************************************************************************************/

                    //Graba los cambios
                    workbook.Save();
                }

                /****************************************************************************/
                //FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(ABRIR Y ESCRIBIR EL ARCHIVO EXCEL)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Finaliza la escritura del archivo Excel del cartel",
                                         loginUser,
                                         local,
                                         nomCartel,
                                         nroPaginas,
                                         "");
                /****************************************************************************/

                /****************************************************************************/
                //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(CONVIERTE EL ARCHIVO EXCEL A PDF)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Inicia la conversion del archivo Excel del cartel a PDF",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        "");
                /****************************************************************************/

                // CONVIERTE EN PDF EL ARCHIVO DE EXCEL (ARCHIVO RENOMBRADO)
                //ConvertExcelToPdf((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));


                ConvertExcel_to_PDF((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));
                /****************************************************************************/
                //FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(CONVIERTE EL ARCHIVO EXCEL A PDF)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Finaliza la conversion del archivo Excel del cartel a PDF",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        "");
                /****************************************************************************/
                
                return true;
            }
            catch (Exception ex)
            {
                /******************************************************************/
                //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(SE GENERO UNA EXCEPCION)
                /*****************************************************************/
                RegLogStepsPrintProcess2("Se genero una excepcion",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        ex.Message.ToString());
                /******************************************************************/

                return false;

            }
            

        }
        /*******************************************************************************************************************************************/

        //private string ConvertExcel_to_PDF(string strExcelOrigen, string strExportarExcel)// USANDO SAUTTINSOFT.USEOFFICE
        //{
        //    string paramSourceBookPath = strExcelOrigen;
        //    string paramExportFilePath = strExportarExcel;

        //    try
        //    {
        //        SautinSoft.UseOffice u = new SautinSoft.UseOffice();
        //        string inputFilePath = strExcelOrigen;
        //        string outputFilePath = strExportarExcel;
        //        int ret = u.InitExcel();

        //        ret = u.ConvertFile(inputFilePath, outputFilePath, SautinSoft.UseOffice.eDirection.XLSX_to_PDF);
        //        u.CloseExcel();
        //        if (ret == 0)
        //        {
        //            //Show produced file
        //            // System.Diagnostics.Process.Start(outputFilePath);
        //        }

        //        if (System.IO.File.Exists(paramSourceBookPath))
        //        { System.IO.File.Delete(paramSourceBookPath); }

        //    }
        //    catch (Exception exp)
        //    {
        //        // Cerrar
        //        CheckForExistingExcellProcesses();
        //        // Elimina el archivo creado
        //        File.Delete(paramExportFilePath);

        //        KillExcelProcessThatUsedByThisInstance();

        //        throw (exp);
        //    }
        //    finally
        //    {

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        KillExcelProcessThatUsedByThisInstance();
        //    }

        //    return paramExportFilePath;
        //}

        private string ConvertExcel_to_PDF(string strExcelOrigen, string strExportarExcel)// USANDO MICROSOFT.OFFICE.INTEROP.EXCEL
        {
            ApplicationClass excelApplication = new ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;

            string paramSourceBookPath = strExcelOrigen;
            string paramExportFilePath = strExportarExcel;
           
            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;
            XlFixedFormatQuality paramExportQuality = XlFixedFormatQuality.xlQualityStandard;
            bool paramOpenAfterPublish = false;
            bool paramIncludeDocProps = true;
            bool paramIgnorePrintAreas = true;
            object paramFromPage = Type.Missing;
            object paramToPage = Type.Missing;
            var missing = Type.Missing; //System.Reflection.Missing.Value;      
            try
            {

                excelWorkbook = excelApplication.Workbooks.Open(paramSourceBookPath, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing);



                if ((excelWorkbook != null))
                {
                    excelWorkbook.ExportAsFixedFormat(paramExportFormat, paramExportFilePath,
                                                     paramExportQuality, paramIncludeDocProps,
                                                     paramIgnorePrintAreas, paramFromPage,
                                                     paramToPage, paramOpenAfterPublish, Type.Missing);
                }

            }
            catch (Exception exp)
            {
                // Cerrar
                CheckForExistingExcellProcesses();
                ((_Workbook)excelWorkbook).Close(false, missing, missing);
                excelApplication.Quit();

                // Elimina el archivo creado
                File.Delete(paramExportFilePath);

                KillExcelProcessThatUsedByThisInstance();

                throw (exp);
            }
            finally
            {
                if ((excelWorkbook != null))
                {
                    excelWorkbook.Close(false, Type.Missing, Type.Missing);
                    excelWorkbook = null;
                }

                // Quit Excel and release the ApplicationClass object.
                if ((excelApplication != null))
                {
                    excelApplication.Quit();
                    excelApplication = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                KillExcelProcessThatUsedByThisInstance();
            }

            return paramExportFilePath;
        }


        private string GetColReference(int row, int col)
        {
            string str_name = null;

            switch (col)
            {
                case 1:
                    str_name = "A" + row;
                    break;

                case 2:
                    str_name = "B" + row;
                    break;

                case 3:
                    str_name = "C" + row;
                    break;

                case 4:
                    str_name = "D" + row;
                    break;

                case 5:
                    str_name = "E" + row;
                    break;

                case 6:
                    str_name = "F" + row;
                    break;

                case 7:
                    str_name = "G" + row;
                    break;

                case 8:
                    str_name = "H" + row;
                    break;

                case 9:
                    str_name = "I" + row;
                    break;

                case 10:
                    str_name = "J" + row;
                    break;

                case 11:
                    str_name = "K" + row;
                    break;

                case 12:
                    str_name = "L" + row;
                    break;

                case 13:
                    str_name = "M" + row;
                    break;

                case 14:
                    str_name = "N" + row;
                    break;
            }

            return str_name;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        // Maneja el proceso de generacion de Carteles PDF de Impresiones Masivas de carteles        
        //------------------------------------------------------------------------------------------------------------------------------------------
        public bool ProcesaCartelInterop(string strSourcePath,
                                         string strPlantilla,                                         
                                         string nomCartel,
                                         System.Data.DataTable dtRegistroImpresion,
                                         string loginUser,
                                         int local,
                                         int nroPaginas,
                                         int xFC,
                                         int yFC,
                                         bool EsMultiple)
        {
            string processId = string.Empty;

            string strSourcePathCartel = System.Configuration.ConfigurationManager.AppSettings["PATH_CARTEL"];

            CheckForExistingExcellProcesses();

            Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;
            Range range = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);          

            // obtengo la extencion o version del archivo excel con el que se esta trabajando
            string sExt = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FILE_EXTENCION"];           

            try
            {
                // Copio y renombro el archivo
                FileInfo file = new FileInfo(strSourcePath + strPlantilla + sExt);
                file.CopyTo(strSourcePathCartel + nomCartel + loginUser + sExt, true);

                /****************************************************************************/
                //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(ABRIR Y ESCRIBIR EL ARCHIVO EXCEL)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Inicia la escritura del archivo Excel del cartel",
                                       loginUser,
                                       local,
                                       nomCartel,
                                       nroPaginas,
                                       "");
                /****************************************************************************/

                // ABRE EL NUEVO ARCHIVO EXCEL GENERADO A PARTIR DE LA PLANTILLA Y ESCRIBIR
                // EN EL LOS DATOS DE REGISTRO
                string xlsFilePath = strSourcePathCartel + nomCartel + loginUser + sExt;

                var misValue = Type.Missing;// System.Reflection.Missing.Value;

                // Abrir el documento
                xlApp = new ApplicationClass();
                GetTheExcelProcessIdThatUsedByThisInstance();

                xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                processId = GetProcessId();

                // Seleccion de la hoja de calculo get_item() devuelve object y numera las hojas a partir de 1
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); //Se accede a la hoja Nº1

                // Si es multiple 
                // el tipo de cartel 
                if (EsMultiple)
                {
                    // Borra el simbolo <?>
                    if (xFC != 0 && yFC != 0)
                    {
                        xlWorkSheet.Cells[xFC, yFC] = "";
                    }
                }

                // Genera las nuevas hojas de calculo de acuerdo al parametro nroPaginas
                for (int worksheetIndex = 1; worksheetIndex < nroPaginas; worksheetIndex++)
                {
                    // Genera nuevas hojas de calculo a partir de la primera hoja
                    // Estas Hojas vendran a ser la paginas del archivo PDF 
                    xlWorkSheet.Copy(misValue, xlWorkBook.Worksheets[worksheetIndex]);                   
                }                

                // seleccion rango activo
                range = xlWorkSheet.UsedRange;

                /*****************************************************************************************************************************************/
                /*****************************************************************************************************************************************/
                for (int z = 1; z <= nroPaginas; z++)
                {
                    //Filtra el datatable por Nombre de hoja
                    string strCrtFiltro = "HOJA = '" + "Hoja" + z + "'";
                    System.Data.DataTable dtDataAux = FiltraDataTable(dtRegistroImpresion, strCrtFiltro);

                    // Se posiciona en la siguiente hoja
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[z];

                    // Llenar el archivo con los datos del producto seleccionado
                    for (int i = 0; i < dtDataAux.Rows.Count; i++)
                    {
                        xlWorkSheet.Cells[Convert.ToInt32(dtDataAux.Rows[i][5]),
                                          Convert.ToInt32(dtDataAux.Rows[i][6])] = dtDataAux.Rows[i][4];
                    }
                }
                /*****************************************************************************************************************************************/
                /*****************************************************************************************************************************************/              

                // cerrar guardando los cambios
                xlWorkBook.Close(true, misValue, Type.Missing);
                xlApp.Quit();

                /****************************************************************************/
                //FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(ABRIR Y ESCRIBIR EL ARCHIVO EXCEL)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Finaliza la escritura del archivo Excel del cartel",
                                         loginUser,
                                         local,
                                         nomCartel,
                                         nroPaginas,
                                         "");
                /****************************************************************************/

                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                /****************************************************************************/
                //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(CONVIERTE EL ARCHIVO EXCEL A PDF)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Inicia la conversion del archivo Excel del cartel a PDF",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        "");
                /****************************************************************************/

                // CONVIERTE EN PDF EL ARCHIVO DE EXCEL (ARCHIVO RENOMBRADO)
                //ConvertExcelToPdf((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));
                ConvertExcel_to_PDF((strSourcePathCartel + nomCartel + loginUser + sExt), (strSourcePathCartel + nomCartel + loginUser + ".pdf"));
                /****************************************************************************/
                //FINALIZA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(CONVIERTE EL ARCHIVO EXCEL A PDF)
                /****************************************************************************/
                RegLogStepsPrintProcess2("Finaliza la conversion del archivo Excel del cartel a PDF",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        "");
                /****************************************************************************/

                return true;

            }
            catch (Exception ex)
            {
                // cerrar el archivo
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();

                // Elimina el archivo creado
                File.Delete(strSourcePathCartel + nomCartel + loginUser + sExt);

                /******************************************************************/
                //INICIA OPERACION DE ESCRITURA DE ARCHIVO DE PLANO DE INCIDENCIAS
                //PARA ESTA PARTE DEL CODIGO
                //(SE GENERO UNA EXCEPCION)
                /*****************************************************************/
                RegLogStepsPrintProcess2("Se genero una excepcion",
                                        loginUser,
                                        local,
                                        nomCartel,
                                        nroPaginas,
                                        ex.Message.ToString());
                /******************************************************************/

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();

                return false;
            }
            finally
            {
                // Liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        /***********************************************************************************/
        /***********************************************************************************/
        /***********************************************************************************/
        /***********************************************************************************/

        //------------------------------------------------------------------------------------------------------------------------------------------

        public System.Data.DataTable Import_Of_Excel(string FilePath)
        {
            //string processId = string.Empty;

            CheckForExistingExcellProcesses();

            Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;
            Range range = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            bool flgSalida = false;

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                // Abrimos el Excel
                var misValue = Type.Missing;//System.Reflection.Missing.Value;

                //GetTheExcelProcessIdThatUsedByThisInstance();

                xlApp = new ApplicationClass();
                GetTheExcelProcessIdThatUsedByThisInstance();
                xlWorkBook = xlApp.Workbooks.Open(FilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                //processId = GetProcessId();

                // seleccion de la hoja de calculo
                // get_item() devuelve object y numera las hojas a partir de 1
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // seleccion rango activo
                range = xlWorkSheet.UsedRange;

                // leer las celdas
                int rows = range.Rows.Count;
                int cols = range.Columns.Count;

                string str_value = null;
                int coldt = 0;

                // Leer la estructura del archivo Excel y forma la estructura del datatable
                for (int col = 1; col <= cols; col++)
                {
                    string colsName = (string)(range.Cells[1, col] as Range).Value2;
                    if (colsName != null)
                    {
                        dt.Columns.Add(colsName, typeof(String));
                        coldt = coldt + 1;
                    }
                }

                // Llena el datatable con el contenido del archivo excel
                // Recorre Filas del Excel
                for (int row = 2; row <= rows; row++)
                {
                    DataRow rowrp;
                    rowrp = dt.NewRow();

                    flgSalida = false;

                    // Recorre Columnas del Excel limitada por el tamaño de la estructura del datable creado
                    for (int col = 1; col <= coldt; col++)
                    {
                        // lectura de la informacion que contiene la celda
                        str_value = (string)(range.Cells[row, col] as Range).Text;

                        if (col == 1 && str_value == "")
                        {
                            flgSalida = true;
                            break;
                        }
                        else
                        {
                            // Asigna el valor de la celda excel a la celda del datatable
                            rowrp[col - 1] = str_value.Trim();
                        }
                    }

                    // Valida que se registra un valor diferente de nulo
                    if (flgSalida == false)
                    {
                        dt.Rows.Add(rowrp);
                    }
                }

                // cerrar
                xlWorkBook.Close(false, misValue, misValue);
                xlApp.Quit();

                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                return dt;

            }
            catch (Exception ex)
            {
                // cerrar el archivo
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();

                throw ex;
            }
            finally
            {                
                // Liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();
            }
        }

        public void ConvertExcelToPdf(string excelFileIn, string pdfFileOut)
        {
            CheckForExistingExcellProcesses();

            //string processId = string.Empty;

            var missing = Type.Missing; //System.Reflection.Missing.Value;            

            Application excel = new Application();

            Microsoft.Office.Interop.Excel.Workbook wbk = null;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            try
            {
                excel.Visible = false;
                excel.ScreenUpdating = false;
                excel.DisplayAlerts = false;

                FileInfo excelFile = new FileInfo(excelFileIn);

                string filename = excelFile.FullName;

                GetTheExcelProcessIdThatUsedByThisInstance();

                wbk = excel.Workbooks.Open(filename, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing, missing, missing,
                                           missing, missing, missing);

                wbk.Activate();

                //processId = GetProcessId();

                object outputFileName = pdfFileOut;

                //Microsoft.Office.Interop.Excel.XlFixedFormatType fileFormat = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;

                //// Save document into PDF Format
                //wbk.ExportAsFixedFormat(fileFormat, outputFileName,
                //                        missing, missing, missing,
                //                        missing, missing, missing,
                //                        missing);

                Microsoft.Office.Interop.Excel.XlFixedFormatType fileFormat = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
                Microsoft.Office.Interop.Excel.XlFixedFormatQuality paramExportQuality = Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard;
                bool paramIncludeDocProps = true;
                bool paramIgnorePrintAreas = false;

                if (wbk != null)//save as pdf

                    wbk.ExportAsFixedFormat(fileFormat,
                                            outputFileName,
                                            paramExportQuality,
                                            paramIncludeDocProps,
                                            paramIgnorePrintAreas,
                                            missing,
                                            missing,
                                            missing,
                                            missing);

                object saveChanges = XlSaveAction.xlDoNotSaveChanges;
                ((_Workbook)wbk).Close(saveChanges, missing, missing);
                excel.Quit();

                // liberar
                releaseObject(wbk);
                releaseObject(excel);

                wbk = null;
                excel = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

            }
            catch (Exception ex)
            {
                // Cerrar

                ((_Workbook)wbk).Close(false, missing, missing);
                excel.Quit();

                // Elimina el archivo creado
                File.Delete(pdfFileOut);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();

                throw (ex);
            }
            finally
            {
                // Liberar
                releaseObject(wbk);
                releaseObject(excel);

                wbk = null;
                excel = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();
            }
        }

        /*-------------------------------------------------------------------------------------------*/
        /* MANEJO DE LAS EXEPCIONES DE LAS GUIAS AUTOMATICAS*/
        /*-------------------------------------------------------------------------------------------*/
        public void ActualizarExcepciones(List<SCE_GUIA_EXCEPCION_BE> lstGuiaExepciones)
        {
            try
            {
                BE = new SCE_GUIA_BE();                
                BE.DETALLE_GUIA_EXCEPCIONES = lstGuiaExepciones;
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                using (TransactionScope scope = new TransactionScope())
                {
                    DA.ActualizarExcepciones(BE);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable ListarExecepcionesPV(int IdGuia, int IdGrupo)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = null;

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                if (DA.Listar().Count > 0)
                {
                    if (DA.ListarExecepcionesPV(IdGuia, IdGrupo).Count > 0)
                    {
                        dt = PivotDtGuiaExecepciones(ListToDataTable(DA.ListarExecepcionesPV(IdGuia, IdGrupo)), "DESCRIPCION");
                        return dt;
                    }
                    else
                    {
                        return dt;
                    }
                }
                else
                {
                    return dt;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable PivotDtGuiaExecepciones(System.Data.DataTable dt, string columnName)
        {
            try
            {
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable dtPivot = new System.Data.DataTable();

                dtPivot.Columns.Add("ID_GUIA", typeof(int));
                dtPivot.Columns.Add("ID_LINEA", typeof(int));
                dtPivot.Columns.Add("VALOR", typeof(String));

                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
                //DE LA TABLA PIVOTEADA
                //'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (columnName == null || columnName.Length == 0)
                {
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                }

                System.Data.DataTable distintos0 = dt.DefaultView.ToTable(true, columnName);

                for (int i = 0; i < distintos0.Rows.Count; i++)
                {
                    dtPivot.Columns.Add(Convert.ToString(distintos0.Rows[i][columnName].ToString()), typeof(string));
                }

                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //'LLENA EL DATATABLE PIVOTEADO
                //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                System.Data.DataTable distintos1 = dt.DefaultView.ToTable(true, "ID_GUIA", "ID_LINEA", "VALOR");
                DataRow row = null;

                for (int i = 0; i < distintos1.Rows.Count; i++)
                {
                    row = dtPivot.NewRow();

                    row["ID_GUIA"] = distintos1.Rows[i][0];
                    row["ID_LINEA"] = distintos1.Rows[i][1];
                    row["VALOR"] = distintos1.Rows[i][2];

                    System.Data.DataTable dtAux = new System.Data.DataTable();
                    string strCrtFiltro = null;

                    strCrtFiltro = "ID_GUIA = " + distintos1.Rows[i][0] + " AND " + "ID_LINEA = " + distintos1.Rows[i][1];

                    dtAux = FiltraDataTable(dt, strCrtFiltro);

                    for (int cols = 3; cols < dtPivot.Columns.Count; cols++)
                    {
                        for (int j = 0; j < dtAux.Rows.Count; j++)
                        {
                            if ((Convert.ToString(dtPivot.Columns[cols].ToString().Trim()) == dtAux.Rows[j][5].ToString().Trim()) && (dtAux.Rows[j][6].ToString().Trim() == "1"))
                            {
                                row[cols] = "x";
                            }
                        }
                    }

                    dtPivot.Rows.Add(row);
                }

                return dtPivot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCE_GUIA_EXCEPCION_BE> GetDetTiendasExecepcion(int IdGuia, int IdLinea)
        {
            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                return DA.GetDetTiendasExecepcion(IdGuia, IdLinea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*-------------------------------------------------------------------------------------------*/
        /* GENERACION DE LA PLANTILLA DE CARGA MASIVA PARA LAS GUIAS AUTOMATICAS*/
        /*-------------------------------------------------------------------------------------------*/
        private System.Data.DataTable ListarPVCamposPlantilla()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string columnName = "ALIAS";

            DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
            dt = DA.ListarPVCamposPlantilla();

            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //'CREA LA ESTRUCTURA DE LA TABLA PIVOTEADA
            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            System.Data.DataTable dtPivot = new System.Data.DataTable();

            dtPivot.Columns.Add("LINEA", typeof(String));
            dtPivot.Columns.Add("CATEGORIA", typeof(String));
            dtPivot.Columns.Add("PROMOCION", typeof(String));

            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //OBTENER LOS REGISTROS DISTINTOS DEL CAMPO PIVOT QUE PASARAN A SER COLUMNAS 
            //DE LA TABLA PIVOTEADA
            //'++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (columnName == null || columnName.Length == 0)
            {
                throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
            }

            dt.DefaultView.Sort = "ALIAS";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtPivot.Columns.Add(Convert.ToString(dt.Rows[i][columnName].ToString()), typeof(string));
            }

            DataRow row = null;
            row = dtPivot.NewRow();

            row["LINEA"] = "N°LINEA";
            row["CATEGORIA"] = "CATEGORIA";
            row["PROMOCION"] = "PROMOCION";

            int cols = 3;

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                row[cols + j] = dt.Rows[j][0];
            }

            dtPivot.Rows.Add(row);

            return dtPivot;
        }

        public bool GenerarPlantillaGM(string userLogin, ref string xlsFilePath)
        {
            //string processId = string.Empty;

            CheckForExistingExcellProcesses();

            Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;

            Range range1 = null;
            Range range2 = null;
            Range range3 = null;
            Range range4 = null;

            var misValue = Type.Missing;//System.Reflection.Missing.Value;

            GC.GetTotalMemory(false);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.GetTotalMemory(true);

            //obtengo la ruta de trabajo del servidor
            string strSourcePath = System.Configuration.ConfigurationManager.AppSettings["PATH_PCM"];

            //obtengo la extencion o version del archivo excel con el que se esta trabajando
            string sExt = System.Configuration.ConfigurationManager.AppSettings["EXCEL_FILE_EXTENCION"];

            // Copio y renombro el archivo
            FileInfo file = new FileInfo(strSourcePath + "PLANTILLA_CARGA_MASIVA" + sExt);

            string NameFile = "PCM_" + userLogin + "_" +
                              DateTime.Now.Day.ToString("00") +
                              DateTime.Now.Month.ToString("00") +
                              DateTime.Now.Hour.ToString("00") +
                              DateTime.Now.Minute.ToString("00") +
                              DateTime.Now.Second.ToString("00");

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);

                System.Data.DataTable dtHojaGuia = ListarPVCamposPlantilla();
                System.Data.DataTable dtHojaCategorias = DA.ListarCategoriasPlantilla();
                System.Data.DataTable dtHojaPromociones = DA.ListarPromocionesPlantilla();
                System.Data.DataTable dtHojaCampos = DA.ListarCamposPlantilla();

                file.CopyTo(strSourcePath + NameFile + sExt, true);

                xlsFilePath = strSourcePath + NameFile + sExt;

                // abrir el documento
                xlApp = new ApplicationClass();

                GetTheExcelProcessIdThatUsedByThisInstance();

                xlWorkBook = xlApp.Workbooks.Open(xlsFilePath, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue,
                                                  misValue, misValue, misValue);

                //processId = GetProcessId();

                // seleccion de la hoja de calculo
                // get_item() devuelve object y numera las hojas a partir de 1
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // seleccion rango activo
                range1 = xlWorkSheet.UsedRange;

                // leer las celdas
                int cols1 = range1.Columns.Count;

                // Recorre la primera fila columna a columna borra su contenido
                for (int col = 1; col <= cols1; col++)
                {
                    string cellContent1 = (string)(range1.Cells[1, col] as Range).Value2;
                    if (cellContent1 != null)
                    {
                        xlWorkSheet.Cells[1, col] = "";
                    }
                }

                // Llenar la hoja activa con los datos del datatable pivoteado de campos
                for (int i = 0; i < dtHojaGuia.Rows.Count; i++)
                {
                    for (int j = 0; j < dtHojaGuia.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 1, j + 1] = dtHojaGuia.Rows[i][j];
                    }
                }

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);

                // seleccion rango activo
                range2 = xlWorkSheet.UsedRange;

                // leer las celdas
                int rows2 = range2.Rows.Count;
                int cols2 = range2.Columns.Count;

                // Recorre la primera fila columna a columna borra su contenido
                for (int row = 2; row <= rows2; row++)
                {
                    for (int col = 1; col <= cols2; col++)
                    {
                        string cellContent2 = (string)(range2.Cells[row, col] as Range).Text;
                        if (cellContent2 != null)
                        {
                            xlWorkSheet.Cells[row, col] = "";
                        }
                    }
                }

                // Llenar la hoja activa con los datos del datatable de categorias
                for (int i = 0; i < dtHojaCategorias.Rows.Count; i++)
                {
                    for (int j = 0; j < dtHojaCategorias.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dtHojaCategorias.Rows[i][j];
                    }
                }

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);

                // seleccion rango activo
                range3 = xlWorkSheet.UsedRange;

                // leer las celdas
                int rows3 = range3.Rows.Count;
                int cols3 = range3.Columns.Count;

                // Recorre la primera fila columna a columna borra su contenido
                for (int row = 2; row <= rows3; row++)
                {
                    for (int col = 1; col <= cols3; col++)
                    {
                        string cellContent3 = (string)(range3.Cells[row, col] as Range).Text;
                        if (cellContent3 != null)
                        {
                            xlWorkSheet.Cells[row, col] = "";
                        }
                    }
                }

                // Llenar la hoja activa con los datos del datatable de promociones
                for (int i = 0; i < dtHojaPromociones.Rows.Count; i++)
                {
                    for (int j = 0; j < dtHojaPromociones.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dtHojaPromociones.Rows[i][j];
                    }
                }

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(4);

                // seleccion rango activo
                range4 = xlWorkSheet.UsedRange;

                // leer las celdas
                int rows4 = range4.Rows.Count;
                int cols4 = range4.Columns.Count;

                // Recorre la primera fila columna a columna borra su contenido
                for (int row = 2; row <= rows4; row++)
                {
                    for (int col = 1; col <= cols4; col++)
                    {
                        string cellContent4 = (string)(range4.Cells[row, col] as Range).Text;
                        if (cellContent4 != null)
                        {
                            xlWorkSheet.Cells[row, col] = "";
                        }
                    }
                }

                // Llenar la hoja activa con los datos del datatable de promociones
                for (int i = 0; i < dtHojaCampos.Rows.Count; i++)
                {
                    for (int j = 0; j < dtHojaCampos.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dtHojaCampos.Rows[i][j];
                    }
                }

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // cerrar el archivo guardando los cambios
                xlWorkBook.Close(true, misValue, Type.Missing);
                xlApp.Quit();

                // liberar
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                releaseObject(range1);
                releaseObject(range2);
                releaseObject(range3);
                releaseObject(range4);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                range1 = null;
                range2 = null;
                range3 = null;
                range4 = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                return true;
            }
            catch
            {
                // Cerrar el archivo que se quedo abierto sin guardar los cambios
                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();

                // Elimina el archivo creado
                File.Delete(strSourcePath + NameFile + sExt);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();

                return false;
            }
            finally
            {
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                releaseObject(range1);
                releaseObject(range2);
                releaseObject(range3);
                releaseObject(range4);

                xlWorkSheet = null;
                xlWorkBook = null;
                xlApp = null;

                range1 = null;
                range2 = null;
                range3 = null;
                range4 = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);

                //KillById(Convert.ToInt32(processId));

                //foreach (Process p in Process.GetProcessesByName("EXCEL"))
                //{
                //    try
                //    {
                //        p.Kill(); p.WaitForExit();
                //    }
                //    catch { }
                //}

                KillExcelProcessThatUsedByThisInstance();
            }
        }

        public bool RestringeCampo(string CostField)
        {
            List<SCE_CAMPO_BE> lstCampo = new List<SCE_CAMPO_BE>();            
            bool rspta = true;

            try
            {
                DA.SCE_CAMPO_DA DA = new DA.SCE_CAMPO_DA(usrLogin);
                lstCampo = DA.Listar1();

                for (int i = 0; i < lstCampo.Count; i++)
                {
                    if (CostField == lstCampo[i].ALIAS.ToString().Trim())
                    {
                        if (lstCampo[i].RESTINGIR == "S")
                        {
                            rspta = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rspta = false;
                throw ex;
            }            

            return rspta;
        }

        public bool EnabledCostField(string CostField)
        {
            bool rspta = true;

            if (CostField == "<TXT-PROMO>")
            {
                rspta = false;
            }

            if (CostField == "<PRECIO-ANTES>")
            {
                rspta = false;
            }

            if (CostField == "<VALOR-ENTER-1>")
            {
                rspta = false;
            }

            if (CostField == "<PREC-DEC-1>")
            {
                rspta = false;
            }

            if (CostField == "<VALOR-ENTER-2>")
            {
                rspta = false;
            }

            if (CostField == "<PREC-DEC-2>")
            {
                rspta = false;
            }

            if (CostField == "<VALOR-ENTER-3>")
            {
                rspta = false;
            }

            if (CostField == "<PREC-DEC-3>")
            {
                rspta = false;
            }

            if (CostField == "<PRECIO-ANTES-2>")
            {
                rspta = false;
            }

            if (CostField == "<PRECIO-ANTES-3>")
            {
                rspta = false;
            }

            return rspta;
        }

        //Registra incidencias de el proceso de impresion de carteles
        public void InsertarLogGuia(string Usuario, int IdGuia, int NroCopias, int IdTienda,
                                    int IdCategoria, int IdPromocion, string IdCartelModelo)
        {
            int IdCartel = Convert.ToInt32(IdCartelModelo.Substring(0, 4).ToString().Trim());
            int IdModelo = Convert.ToInt32(IdCartelModelo.Substring(4, 2).ToString().Trim());
            int Digitos = Convert.ToInt32(IdCartelModelo.Substring(6, 1).ToString().Trim());

            try
            {
                DA.SCE_GUIA_DA DA = new DA.SCE_GUIA_DA(usrLogin);
                DA.InsertarLogGuia(Usuario, IdGuia, NroCopias, IdTienda,
                                   IdCategoria, IdPromocion, IdCartel, IdModelo, Digitos);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        /****************************************************************************************/
        /************************************ FUNCIONES ESPECIALES ******************************/
        /****************************************************************************************/
        private static System.Data.DataTable ListToDataTable(List<SCE_GUIA_EXCEPCION_BE> List)
        {
            System.Data.DataTable oDataTableReturned = new System.Data.DataTable();

            if (List.Count > 0)
            {
                object _baseObj = List[0];
                Type objectType = _baseObj.GetType();
                PropertyInfo[] properties = objectType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    DataColumn oColumna;
                    oColumna = new DataColumn();
                    oColumna.ColumnName = property.Name;
                    oColumna.DataType = property.PropertyType;
                    oDataTableReturned.Columns.Add(oColumna);
                }

                foreach (object objItem in List)
                {
                    DataRow oFila;
                    oFila = oDataTableReturned.NewRow();

                    foreach (PropertyInfo property in properties)
                    {
                        oFila[property.Name] = property.GetValue(objItem, null);
                    }

                    oDataTableReturned.Rows.Add(oFila);
                }
            }

            return oDataTableReturned;
        }       

        public System.Data.DataTable FiltraDataTable(System.Data.DataTable dtprincipal, string ctrFiltro)
        {
            /* Seleccionamos las filas que se correspondan con el identificador */
            System.Data.DataRow[] rows = dtprincipal.Select(ctrFiltro);

            /* Clonamos la estructura del objeto DataTable principal */
            System.Data.DataTable dt1 = dtprincipal.Clone();

            // Importamos los registros al nuevo DataTable
            foreach (DataRow row in rows)
            {
                dt1.ImportRow(row);
            }

            return dt1;
        }

        public System.Data.DataTable CopiarDataTable(System.Data.DataTable dtprincipal)
        {
            /* Seleccionamos las filas que se correspondan con el identificador */
            System.Data.DataRow[] rows = dtprincipal.Select("");

            /* Clonamos la estructura del objeto DataTable principal */
            System.Data.DataTable dt1 = dtprincipal.Clone();

            // Importamos los registros al nuevo DataTable
            foreach (DataRow row in rows)
            {
                dt1.ImportRow(row);
            }

            return dt1;
        }

        private string CopiarRenombrarArchivo(string pathFilePlantilla, string NomFileCartel)
        {
            //Ruta del archivo del cartel con el que sera renombrada la plantilla
            //string NomFileCartel = "CARTEL01.xlsx";

            //Ruta completa del cartel a generar
            string filePathCartel = System.Configuration.ConfigurationManager.AppSettings["PATH_CARTELES_L"] + NomFileCartel;

            //Copio y renombro el archivo
            FileInfo file = new FileInfo(pathFilePlantilla);
            file.CopyTo(filePathCartel, true);

            return filePathCartel;
        }

        private static void releaseObject(Object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private string GetProcessId()
        {
            string nProcessID = string.Empty;

            foreach (Process p in Process.GetProcessesByName("EXCEL"))
            {
                try
                {
                    nProcessID = p.Id.ToString();
                }
                catch { }
            }
            
            return nProcessID;
        }

        public void KillById(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);

                if (process != null)
                {
                    process.Kill(); ; process.WaitForExit();
                }
            }
            catch { }
        }

        void ReleaseExcelResources(Microsoft.Office.Interop.Excel.Worksheet worksheet1, Microsoft.Office.Interop.Excel.Application excel, Microsoft.Office.Interop.Excel.Workbook wbk)
        {
            object missing = System.Reflection.Missing.Value;

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
            }
            catch
            { }
            finally
            {
                worksheet1 = null;
            }

            try
            {
                if (wbk != null)
                {
                    wbk.Close(false, missing, missing);
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wbk);
            }
            catch
            { }
            finally
            {
                wbk = null;
            }

            try
            {
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
            catch
            { }
            finally
            {
                excel = null;
            }
        }

        void CheckForExistingExcellProcesses()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");
            myHashtable = new Hashtable();
            int iCount = 0;

            foreach (Process ExcelProcess in AllProcesses)
            {
                myHashtable.Add(ExcelProcess.Id, iCount);
                iCount = iCount + 1;
            }
        }

        void GetTheExcelProcessIdThatUsedByThisInstance()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");

            // Search For the Right Excel
            foreach (Process ExcelProcess in AllProcesses)
            {
                if (myHashtable == null)
                    return;

                if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                {
                    // Get the process ID
                    MyExcelProcessId = ExcelProcess.Id;
                }
            }

            AllProcesses = null;
        }

        void KillExcelProcessThatUsedByThisInstance()
        {
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");

            foreach (Process ExcelProcess in AllProcesses)
            {
                if (myHashtable == null)
                    return;

                if (ExcelProcess.Id == MyExcelProcessId)
                    ExcelProcess.Kill();
            }

            AllProcesses = null;
        }
    }
}

[Serializable]
public class SCE_LIST_GUIA
{
    public SCE_LIST_GUIA()
    {
        
    }

    public string ID_GUIA { get; set; }
    public string NOM_GUIA { get; set; }
    public string DES_TIPO_GUIA { get; set; }   
    public int ID_PROMOCION { get; set; }
    public int ID_CATEGORIA { get; set; }
    public string NOM_CATEGORIA { get; set; }
    public DateTime FECHA_INI { get; set; }
    public DateTime FECHA_FIN { get; set; }
    public int ID_TIENDA { get; set; }
    public string NOM_TIENDA { get; set; }
    public int ID_GRUPO { get; set; }
    public string NOM_GRUPO { get; set; }
    public string VIGENCIA { get; set; }

}
