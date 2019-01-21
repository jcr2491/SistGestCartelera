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
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.IO;
/* Office 11 para Office 2003, 
 * Office 12 para Office 2007 de Visual Studio 2008 y 
 * Office 14 para Office 2010 de Visual Studio 2010 */
using Microsoft.Office.Interop.Excel;

/// <summary>
/// Descripción breve de UtilExcel
/// </summary>
public class UtilExcel
{
    public static void ReplaceTextInExcelFile(string filename, 
                                              string replace, 
                                              string replacement)
    {
        object misValue = Type.Missing;

        // open excel.
        Application app = new ApplicationClass();

        // open the workbook. 
        Workbook wb = app.Workbooks.Open(filename, misValue, false, 
                                         misValue, misValue, misValue, 
                                         misValue, misValue, misValue, 
                                         misValue, misValue, misValue, 
                                         misValue, misValue, misValue);

        // get the active worksheet. (Replace this if you need to.) 
        Worksheet ws = (Worksheet)wb.ActiveSheet;

        // get the used range. 
        Range r = (Range)ws.UsedRange;

        // call the replace method to replace instances. 
        bool success = (bool)r.Replace(replace, replacement,
                                       XlLookAt.xlWhole, XlSearchOrder.xlByRows,
                                       true, misValue, 
                                       misValue, misValue);

        // save and close. 
        wb.Save();
        app.Quit();
        app = null;
    }

    public static void FindTextInExcelFile(string filename, 
                                           string findText)
    {
        object misValue = Type.Missing;

        // open excel.
        Application app = new ApplicationClass();

        // open the workbook. 
        Workbook wb = app.Workbooks.Open(filename, misValue, false,
                                         misValue, misValue, misValue,
                                         misValue, misValue, misValue,
                                         misValue, misValue, misValue,
                                         misValue, misValue, misValue);

        // get the active worksheet. (Replace this if you need to.) 
        Worksheet ws = (Worksheet)wb.ActiveSheet;

        Range currentFind = null;
        Range firstFind = null;

        // get the used range. 
        Range Fruits = (Range)ws.UsedRange;

        // You should specify all these parameters every time you call this method,
        // since they can be overridden in the user interface. 
        currentFind = Fruits.Find(findText, misValue,
                                  XlFindLookIn.xlValues, 
                                  XlLookAt.xlPart,
                                  XlSearchOrder.xlByRows, 
                                  XlSearchDirection.xlNext,
                                  false, misValue,
                                  misValue);

        while (currentFind != null)
        {
            // Keep track of the first range you find. 
            if (firstFind == null)
            {
                firstFind = currentFind;
            }

            // If you didn't move to a new range, you are done.
            else if (currentFind.get_Address(misValue, 
                                             misValue, 
                                             XlReferenceStyle.xlA1, 
                                             misValue, 
                                             misValue)
                     == firstFind.get_Address(misValue, 
                                              misValue, 
                                              XlReferenceStyle.xlA1, 
                                              misValue, 
                                              misValue))
            {
                break;
            }

            currentFind.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            currentFind.Font.Bold = true;

            currentFind = Fruits.FindNext(currentFind);
        }
    }
}
