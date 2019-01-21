using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class paginasError_GenericErrorPage : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Exception exc_Exception = (Exception)Session["_LastException"];

        if (exc_Exception != null)
        {
            lbl_ErrorMsg.Text = exc_Exception.Message;
            div_StackTrace.InnerHtml = exc_Exception.StackTrace.Replace(Environment.NewLine, "<br />");
            Util.RegistrarError(exc_Exception, "Se ha registrado un error inesperado", Convert.ToString(Session["Formulario"]));
        }
        else
        {
            tbl_Exception.Visible = false;
        }
    }    
}
