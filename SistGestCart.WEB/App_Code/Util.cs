using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Descripción breve de Util
/// </summary>
public class Util
{
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //Funcion que registra errores inesperados
    //-----------------------------------------------------------------------------------------------------------------------------------------
    private static readonly string Formato = System.Configuration.ConfigurationManager.AppSettings["RutaFisicaSitioWeb"] + "{0}";
    private static readonly string RutaErrorLog = String.Format(Formato, System.Configuration.ConfigurationManager.AppSettings["RutaErrorLog"]);
    private static readonly string RutaLogStepsPI = String.Format(Formato, System.Configuration.ConfigurationManager.AppSettings["RutaLogStepsPI"]);
    private static readonly object _syncLockRegistrarError = new Object();

    public static void RegistrarError(Exception ex, string titulo, string Formulario)
    {
        lock  (_syncLockRegistrarError)
        {
            String ruta = String.Format(RutaErrorLog, 
                                        DateTime.Now.Year.ToString("0000") + 
                                        DateTime.Now.Month.ToString("00") + 
                                        DateTime.Now.Day.ToString("00"));

            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ruta, true)) 
                {
                    writer.WriteLine("*******************************");
                    writer.WriteLine();
                    writer.WriteLine(String.Format("Titulo  :  {0}", titulo));
                    writer.WriteLine(String.Format("Modulo  :  {0}", Formulario));
                    writer.WriteLine(String.Format("Error   :  {0}", ex.Message));
                    writer.WriteLine(String.Format("Inner   :  {0}", ex.InnerException));
                    writer.WriteLine(String.Format("Origen  :  {0}", ex.Source));
                    writer.WriteLine(String.Format("Pila    :  {0}", ex.StackTrace));
                    writer.WriteLine(String.Format("Fecha   :  {0}", DateTime.Now));
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

    public static void RegLogStepsPrintProcess(string titulo, 
                                               string usuario,
                                               int local,
                                               string Cartel,
                                               int copias)
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
                    writer.WriteLine("*******************************");
                    writer.WriteLine();
                    writer.WriteLine(String.Format("Titulo  :  {0}", titulo));
                    writer.WriteLine(String.Format("Fecha   :  {0}", DateTime.Now));
                    writer.WriteLine(String.Format("Usuario :  {0}", usuario));
                    writer.WriteLine(String.Format("Local   :  {0}", local));
                    writer.WriteLine(String.Format("Cartel  :  {0}", Cartel));
                    writer.WriteLine(String.Format("Copias  :  {0}", copias));                    
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
                                                int copias)
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
                                String.Format("{0,-3}", copias);

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

    public static void SetEnterButton(TextBox txtBox, Button btn)
    {
        txtBox.Attributes.Add("onkeydown",
                  "if(event.which || event.keyCode) { if ((event.which == 13) || (event.keyCode == 13)) " +
                  "{document.getElementById('" + btn.ClientID + "').click(); return false;}}" +
                  "else {return true}; ");
    }   

    //public static string TheSessionId()
    //{
    //    HttpSessionState ss = HttpContext.Current.Session;
    //    HttpContext.Current.Session["test"] = "test";
    //    HttpContext.Current.Response.Write(ss.SessionID);
    //    return "ok";
    //}

    /*-----------------------------------------------------------------------
    * Funcionalidades de JavaScript con AJAX 
    * ----------------------------------------------------------------------*/
    public static void RegisterAsyncPressButton(UpdatePanel upnObj, string strKey, string btnClientID)
    {
        ScriptManager.RegisterClientScriptBlock(upnObj, upnObj.GetType(), strKey, string.Format("document.getElementById('{0}').click();", btnClientID), true);
    }

    public static void RegisterScript(UpdatePanel upnObj, string strKey, String script)
    {
        ScriptManager.RegisterStartupScript(upnObj, upnObj.GetType(), strKey, script, true);
    }

    public static void RegisterAsyncFocus(Page ppg, Control ctrl)
    {
        ScriptManager.GetCurrent(ppg).SetFocus(ctrl);
    }

    /*-----------------------------------------------------------------------
     * Mensajes de JavaScript con AJAX 
     * ----------------------------------------------------------------------*/
    public static void RegisterAsyncAlert(UpdatePanel upnObj, string strKey, string strMsg)
    {
        ScriptManager.RegisterClientScriptBlock(upnObj, upnObj.GetType(), strKey, string.Format("alert('{0}');", strMsg), true);
    }

    public static DataTable FiltraDataTable(DataTable dtprincipal, string ctrFiltro)
    {
        /* Seleccionamos las filas que se correspondan con el identificador */
        System.Data.DataRow[] rows = dtprincipal.Select(ctrFiltro);

        /* Clonamos la estructura del objeto DataTable principal */
        DataTable dt1 = dtprincipal.Clone();

        // Importamos los registros al nuevo DataTable
        foreach (DataRow row in rows)
        {
            dt1.ImportRow(row);
        }

        return dt1;
    }

    //The way to call the Group By Function
    //Argument 1 = Data table in IMAGE 1
    //Argument 2 = The fields you want for the data table returned
    //Argument 3 = The field you want to group by
    //Argument 4 = The function you want to do It can be either SUM, COUNT, AVG etc.

    //DataTable dtGroupedBy = GetGroupedBy(dt, "CodeName,Quantity,Current", "CodeName", "Sum");

    /// <summary>
    /// The group by clause for Data Table para columnas expecificas de calculo
    /// </summary>
    /// <param name="dt">The main data table to be filtered with the Group By.</param>
    /// <param name="columnNamesInDt">The Fields sent seperated by commas. EG "Field1,Field2,Field3"</param>
    /// <param name="groupByColumnNames">The column name which should be used for group by.</param>
    /// <param name="typeOfCalculation">The calculation type like Sum, Avg Or Count.</param>
    /// <returns></returns>    
    public static DataTable GetGroupedBy(DataTable dt, string columnNamesInDt, string groupByColumnNames, string typeOfCalculation)
    {
        //Return its own if the column names are empty
        if (columnNamesInDt == string.Empty || groupByColumnNames == string.Empty)
        {
            return dt;
        }

        //Once the columns are added find the distinct rows and group it bu the numbet
        DataTable _dt = dt.DefaultView.ToTable(true, groupByColumnNames);

        //The column names in data table
        string[] _columnNamesInDt = columnNamesInDt.Split(',');

        for (int i = 0; i < _columnNamesInDt.Length; i = i + 1)
        {
            if (_columnNamesInDt[i] != groupByColumnNames)
            {
                _dt.Columns.Add(_columnNamesInDt[i]);
            }
        }

        //Gets the collection and send it back
        for (int i = 0; i < _dt.Rows.Count; i = i + 1)
        {
            for (int j = 0; j < _columnNamesInDt.Length; j = j + 1)
            {
                if (_columnNamesInDt[j] != groupByColumnNames)
                {
                    _dt.Rows[i][j] = dt.Compute(typeOfCalculation + "(" + _columnNamesInDt[j] + ")", groupByColumnNames + " = '" + _dt.Rows[i][groupByColumnNames].ToString() + "'");
                }
            }
        }

        return _dt;
    }

    /*ORDENAR UN DATATABLE*/
    //DataTable dtAux = new DataTable();
    //string strCrtFiltro = null;
    //strCrtFiltro = "VALOR <> ''";
    //dtAux = Util.FiltraDataTable(dtRegImpresion, strCrtFiltro);           

    //DataView _dv = new DataView(dtAux);
    //_dv.Sort = "ID_LINEA ASC";


    /// <summary>
    /// Group by DataTable para multiples columnas
    /// </summary>
    /// <param name="_dtSource"></param>
    /// <param name="_groupByColumnNames"></param>
    /// <param name="_fieldsForCalculation"></param>
    /// <returns></returns>
    /// 
    ////Gets the mock data table
    //DataTable _dt = GetDataTable();

    ////Add columns which you want to group by
    //IList<string> _groupByColumnNames = new List<string>();
    //_groupByColumnNames.Add("State");
    //_groupByColumnNames.Add("City");

    ////Functions you want to perform on which fields
    //IList<DataTableAggregateFunction> _fieldsForCalculation = new List<DataTableAggregateFunction>();
    //_fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = AggregateFunction.Avg, ColumnName = "Population", OutPutColumnName = "PopulationAvg" });
    //_fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = AggregateFunction.Sum, ColumnName = "Population", OutPutColumnName = "PopulationSum" });
    //_fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = AggregateFunction.Count, ColumnName = "Population", OutPutColumnName = "PopulationCount" });
    //_fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = AggregateFunction.Max, ColumnName = "Year", OutPutColumnName = "YearMax" });
    //_fieldsForCalculation.Add(new DataTableAggregateFunction() { enmFunction = AggregateFunction.Min, ColumnName = "Year", OutPutColumnName = "YearMin" });

    ////Gets the result after grouping by
    //DataTable dtGroupedBy = GetGroupedBy(_dt, _groupByColumnNames, _fieldsForCalculation);

    public static DataTable GetGroupedBy(DataTable _dtSource,
                                         IList<string> _groupByColumnNames,
                                         IList<DataTableAggregateFunction> _fieldsForCalculation)
    {
        
        //Once the columns are added find the distinct rows and group it bu the numbet
        DataTable _dtReturn = _dtSource.DefaultView.ToTable(true, _groupByColumnNames.ToArray());

        //The column names in data table
        foreach (DataTableAggregateFunction _calculatedField in _fieldsForCalculation)
        {
            _dtReturn.Columns.Add(_calculatedField.OutPutColumnName);
        }

        //Gets the collection and send it back
        for (int i = 0; i < _dtReturn.Rows.Count; i = i + 1)
        {
            // Gets the filter string
            string _filterString = string.Empty;
            for (int j = 0; j < _groupByColumnNames.Count; j = j + 1)
            {
                if (j > 0)
                {
                    _filterString += " AND ";
                }
                if (_dtReturn.Columns[_groupByColumnNames[j]].DataType == typeof(System.Int32))
                {
                    _filterString += _groupByColumnNames[j] + " = " + _dtReturn.Rows[i][_groupByColumnNames[j]].ToString() + "";
                }
                else
                {
                    _filterString += _groupByColumnNames[j] + " = '" + _dtReturn.Rows[i][_groupByColumnNames[j]].ToString() + "'";
                }
            }
           
            // Compute the aggregate command
            foreach (DataTableAggregateFunction _calculatedField in _fieldsForCalculation)
            {
                _dtReturn.Rows[i][_calculatedField.OutPutColumnName] = _dtSource.Compute(_calculatedField.enmFunction.ToString() + "(" + _calculatedField.ColumnName + ")", _filterString);
            }
        }

        return _dtReturn;
    }

    /// <summary>
    /// Metodos genéricos para convertir una Lista Genérica de elementos en
    /// un objeto DataTable o un DataTable en una Lista Genérica
    /// </summary>
    public static DataTable ListToDataTable<T>(List<T> List)
    {
        DataTable oDataTableReturned = new DataTable();

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

    public static string getParametroWebConfig(string pNombre)
    {
        return ConfigurationManager.AppSettings[pNombre];
    }
}

/// <summary>
/// The class which will have properties of function to be performed and on which field
/// </summary>
public class DataTableAggregateFunction
{
    /// <summary>
    /// The functions which can be used for aggreation
    /// </summary>
    public enum AggregateFunction
    {
        Sum,
        Avg,
        Count,
        Max,
        Min
    }   

    /// <summary>
    /// The function to be performed
    /// </summary>
    public AggregateFunction enmFunction { get; set; }

    /// <summary>
    /// Performed for which column
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// What should be the name after output
    /// </summary>
    public string OutPutColumnName { get; set; }

}


