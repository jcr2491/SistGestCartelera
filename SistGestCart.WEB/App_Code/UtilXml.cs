using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Descripción breve de UtilXml
/// </summary>
public class UtilXml
{
    public static DataRow[] TraerSysRow(string str_pAplicacion, string str_Tabla, string str_Filtro)
    {
        DataSet ads_XML = new DataSet();
        DataTable adt_XML = new DataTable();

        ads_XML.ReadXml(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format("{0}/xml/SysTables.xml", str_pAplicacion)));
        adt_XML = ads_XML.Tables[str_Tabla];

        return adt_XML.Select(str_Filtro);
    }

    public static DataTable TraerSysTable(string str_pAplicacion, string str_Tabla)
    {
        DataSet ads_XML = new DataSet();
        DataTable adt_XML = new DataTable();

        ads_XML.ReadXml(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format("{0}/xml/SysTables.xml", str_pAplicacion)));
        adt_XML = ads_XML.Tables[str_Tabla];

        return adt_XML;
    }
}
