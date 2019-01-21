using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Descripción breve de MyBasePage
/// ciclo de vida de una página ASP.NET
/// Eventos del ciclo de vida
/// </summary>
public class MyBasePage : System.Web.UI.Page
{
    //SCE_GUIA_BL GUIA_BL;  

    /// <summary>
    /// Se genera cuando la fase de inicio se ha completado y antes de que comience la fase de inicialización.
    /// Utilice este evento para lo siguiente:
    /// --Examine la propiedad IsPostBack para determinar si es la primera vez que se procesa la página. 
    ///     En este momento también se han establecido las propiedades IsCallback e IsCrossPagePostBack.
    /// --Crear o volver a crear controles dinámicos.
    ///    Normalmente es usado para añadir dinámicamente controles a la pagina, porque añadiéndolos aquí 
    ///    garantizamos que a dichos controles se les apliquen adecuadamente los Skins del Theme definido 
    ///    (si no hemos definido ningún Theme daría un poco igual añadirlos aquí o en Init, por ejemplo).
    /// --Establecer una página maestra de forma dinámica.
    /// --Establecer la propiedad Theme de forma dinámica.
    /// --Leer o establecer los valores de las propiedades de perfil.
    /// Nota	
    ///     Si la solicitud es una devolución de datos, los valores de los controles todavía no se han restaurado 
    ///     del estado de vista.Si establece una propiedad de un control en esta fase, es posible que su valor se
    ///     sobrescriba en el evento siguiente.    
    /// </summary>   
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        Response.Cache.SetNoStore();
    }

    /// <summary>
    /// Se provoca cuanto todos los controles se han inicializado y se aplicado la configuración de máscara. 
    /// El evento Init de controles individuales se produce antes del evento Init de la página.
    /// Este evento ocurre después de que todos los controles de la pagina ya tienen definidos sus Theme . 
    /// Utilice este evento para leer o inicializar las propiedades del control.
    /// </summary>    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);        

        if (Session["login"] == null)
        {
            Response.Redirect("SegLogin.aspx");
        }       
    }

    /// <summary>
    /// Se genera al final de la fase de inicialización de la página. Sólo tiene lugar una operación 
    /// entre los eventos InitComplete y Init: el seguimiento de los cambios de estado de vista se activa. 
    /// El seguimiento del estado de vista permite a los controles conservar los valores agregados mediante 
    /// programación a la colección ViewState. Hasta que se activa el seguimiento del estado de vista, los valores 
    /// agregados al estado de vista se pierden de un postback a otro. 
    /// Normalmente, los controles activan el seguimiento del estado de vista inmediatamente después de generar 
    /// su evento Init.
    /// Use este evento para realizar cambios al estado de vista que desea para asegurarse de que se conservan 
    /// después del siguiente postback.
    /// </summary>    
    //protected override void OnInitComplete(EventArgs e)
    //{
    //    base.OnInitComplete(e);

    //    // your code
    //}

    /// <summary>
    /// Se genera después de que la página carga el estado de vista para ella misma y para todos los controles, 
    /// y después de procesar los datos de postback incluidos en la instancia de Request.
    /// </summary>   
    //protected override void OnPreLoad(EventArgs e)
    //{
    //    base.OnPreLoad(e);

    //    // your code
    //}    

    /// <summary>
    /// El objeto Page llama al método OnLoad en el objeto Page y, a continuación, vuelve a hacer lo mismo para 
    /// cada control secundario hasta que se cargan la página y todos los controles. 
    /// El evento Load de controles individuales se produce después del evento Load de la página.
    /// Utilice el método del evento OnLoad para establecer las propiedades de los controles y establecer 
    /// las conexiones a bases de datos. 
    /// </summary>
    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);

    //    // your code
    //}

    //************************************************************************************************************
    //Eventos de control
    //************************************************************************************************************
    //Utilice estos eventos para controlar eventos de control específicos, como un evento Click del control Button 
    //o un evento TextChanged del control TextBox.
    //En una solicitud de devolución de datos, si la página contiene controles validadores, compruebe la 
    //propiedad IsValid de Page y de cada uno de los controles de validación antes de realizar cualquier
    //procesamiento.
    //************************************************************************************************************

    /// <summary>
    /// Se genera al final de la fase de control de eventos.
    /// Utilice este evento para las tareas que requieran que se carguen todos los demás controles en la página.
    /// </summary>   
    //protected override void OnLoadComplete(EventArgs e)
    //{
    //    base.OnLoadComplete(e);

    //    // your code
    //}
    
    /// <summary>
    /// Se genera después de que el objeto Page haya creado todos los controles necesarios para representar 
    /// la página, incluidos los controles secundarios de controles compuestos. 
    /// (Para ello, el objeto Page llama a EnsureChildControls para cada control y para la página).
    /// El objeto Page genera el evento PreRender en el objeto Page y, a continuación, vuelve a hacer lo mismo 
    /// para cada control secundario. El evento PreRender de controles individuales se produce después del evento 
    /// PreRender de la página.
    /// Use el evento para realizar los cambios finales en el contenido de la página o en sus controles antes de 
    /// que comience la fase de representación.Primero ocurre el PreRender de la pagina y después el de cada uno de controles
    /// </summary>
    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);

    //    // your code
    //}

    /// <summary>
    /// Se genera después de que cada control enlazado a datos cuya propiedad DataSourceID esté establecida 
    /// llame a su método DataBind. Para obtener más información, vea Eventos de enlace de datos de controles 
    /// enlazados a datos más adelante en este tema.
    /// </summary>   
    //protected override void OnPreRenderComplete(EventArgs e)
    //{
    //    base.OnPreRenderComplete(e);

    //    // your code
    //}

    /// <summary>
    /// Se genera después de guardar el estado de las vistas y los controles para la página y para todos los 
    /// controles. Los cambios realizados a la página o a los controles en este punto afectan a la presentación
    /// pero los cambios no se recuperarán en el postback siguiente.
    /// </summary>   
    //protected override void OnSaveStateComplete(EventArgs e)
    //{
    //    base.OnSaveStateComplete(e);

    //    // your code
    //}

    /// <summary>
    /// Éste no es un evento; en esta fase del procesamiento, el objeto Page llama a este método en cada control. 
    /// Todos los controles de servidor web ASP.NET tienen un método Render que escribe el marcado del control que
    /// se envía al explorador.
    /// Si crea un control personalizado, normalmente reemplazará este método para generar el marcado del control. 
    /// Sin embargo, si el control personalizado sólo incorpora controles de servidor Web de ASP.NET estándar y 
    /// ningún marcado personalizado, no necesita reemplazar el método Render. Para obtener más información, 
    /// consulte Desarrollar controles de servidor ASP.NET personalizados.
    /// Un control de usuario (un archivo .ascx) incorpora automáticamente la representación, por lo que no 
    /// necesita representar explícitamente el control en el código.
    /// </summary>   
    //protected override void OnRender(EventArgs e)
    //{
    //    base.OnRender(e);

    //    // your code
    //}    

    /// <summary>
    /// Se genera para cada control y, a continuación, para la página.
    /// En los controles, utilice este evento para realizar tareas finales de limpieza en controles específicos, 
    /// como cerrar las conexiones a bases de datos específicas del control.
    /// Para la propia página, utilice este evento para hacer un último trabajo de limpieza, como cerrar archivos 
    /// abiertos y conexiones a bases de datos, finalizar el registro u otras tareas específicas de la solicitud.
    /// Nota	
    ///     Durante la fase de descarga, la página y sus controles ya se han representado, por lo que no se podrán
    ///     realizar más cambios en la secuencia de respuesta.Si intenta llamar a un método, como Response.Write, 
    ///     la página producirá una excepción.
    /// </summary>   
    //protected override void OnUnload(EventArgs e)
    //{
    //    base.OnUnload(e);       
    //}    
    
    /// <summary>
    /// si se produce una excepción no controlada durante el proceso de la página, se desencadena el evento
    /// este evento que no es determinista
     /// </summary>   
    protected override void OnError(System.EventArgs e)
    {
        base.OnError(e);       

        Exception exc_Exception = (Exception)Server.GetLastError();
        Session["_LastException"] = exc_Exception;
        Response.Redirect("paginasError/GenericErrorPage.aspx");        
    }    
}
