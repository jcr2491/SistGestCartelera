using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using pe.oechsle.Entity;
using SistGestCart.BL;

public partial class Principal : System.Web.UI.MasterPage
{
    protected pe.oechsle.Entity.Usuario usrLogin;
    SCE_GUIA_BL GUIA_BL;   

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Formulario"] = "Principal.master";

        if (Session["login"] == null)
        {
            Response.Redirect("SegLogin.aspx");
        }        

        pe.oechsle.Entity.Usuario usrLogin = (pe.oechsle.Entity.Usuario)Session["login"];
        lblUsuario.Text = "(" + usrLogin.login + ")";

        foreach (pe.oechsle.ex.Entity.TablaTipo tabla in usrLogin.TablasTipo)
        {
            if (tabla.abreviatura == "SUCS")
            {
                foreach (pe.oechsle.ex.Entity.DetalleTablaTipo detalle in tabla.Detalles)
                {
                    lblTienda.Text = "(" + detalle.codigo.ToString() + ")";
                    Session["Tienda"] = detalle.codigo.ToString();
                    lblNomTienda.Text = "(" + detalle.detalle.ToString() + ")";
                    break;
                }
            }
        }       

        DisableAllOptions();

        ConfigurarMenu();
    }   

    protected void lnkbTipCart_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParCarteles.aspx");
    }

    protected void lnkbTipProm_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParPromociones.aspx");
    }

    protected void lnkbGrupCat_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParCategoria.aspx");
    }

    protected void lnkbCampos_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParCampos.aspx");
    }

    protected void lnkbTienda_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParTiendas.aspx");
    }

    protected void lnkbGrupoTiendas_Click(object sender, EventArgs e)
    {
        Server.Transfer("ParGrupoTiendas.aspx");
    }
   
    protected void lnkbRegCartCamp_Click(object sender, EventArgs e)
    {
        Server.Transfer("ConfReglCartelCamp.aspx");
    }

    protected void lnkbConfCartel_Click(object sender, EventArgs e)
    {
        Server.Transfer("ConfConfigurCartel.aspx");
    }

    protected void lnkConfCartCategProm_Click(object sender, EventArgs e)
    {
        Server.Transfer("ConfCartelCategPromo.aspx");
    }

    protected void lnkCrearGuiaAuto_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConfAsigGuiaMas.aspx");
    }

    protected void lnkbCreaMan_Click(object sender, EventArgs e)
    {
        Response.Redirect("CartCreacManual.aspx");
    }

    protected void lnkbCreaAuto_Click(object sender, EventArgs e)
    {
        Response.Redirect("CartCreacAutomatica.aspx");
    }   
 
     protected void lnkbConfExepciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("CartConfExepciones.aspx");       
    }

    protected void lnkbBusImpr_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImprBusqImpresion.aspx");
    }

    protected void lnkbCambClav_Click(object sender, EventArgs e)
    {
        Server.Transfer("SegCambioClave.aspx");
    }

    protected void lnkSalir_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("SegLogin.aspx");
    }  

    private void DisableAllOptions()
    {
        /* SECCION PARAMETROS */
        lnkbTipProm.Enabled = false; // 2
        lnkbTipProm.ForeColor = System.Drawing.Color.Lavender;
        lnkbGrupCat.Enabled = false; // 3
        lnkbGrupCat.ForeColor = System.Drawing.Color.Lavender;
        lnkbTipCart.Enabled = false; // 4
        lnkbTipCart.ForeColor = System.Drawing.Color.Lavender;       
        lnkbCampos.Enabled = false; // 5
        lnkbCampos.ForeColor = System.Drawing.Color.Lavender;
        lnkbTienda.Enabled = false; // 6
        lnkbTienda.ForeColor = System.Drawing.Color.Lavender;
        lnkbGrupoTiendas.Enabled = false; // 7
        lnkbGrupoTiendas.ForeColor = System.Drawing.Color.Lavender;

        /* SECCION CONFIGURACION */
        lnkbRegCartCamp.Enabled = false; // 9
        lnkbRegCartCamp.ForeColor = System.Drawing.Color.Lavender;
        lnkbConfCartel.Enabled = false; // 10
        lnkbConfCartel.ForeColor = System.Drawing.Color.Lavender;
        lnkConfCartCategProm.Enabled = false; // 11
        lnkConfCartCategProm.ForeColor = System.Drawing.Color.Lavender;
        lnkCrearGuiaAuto.Enabled = false; // 20
        lnkCrearGuiaAuto.ForeColor = System.Drawing.Color.Lavender;

        /* SECCION CARTELERIA */
        lnkbCreaMan.Enabled = false; // 13
        lnkbCreaMan.ForeColor = System.Drawing.Color.Lavender;
        lnkbCreaAuto.Enabled = false; // 14
        lnkbCreaAuto.ForeColor = System.Drawing.Color.Lavender;
        lnkbConfExepciones.Enabled = false; //15
        lnkbConfExepciones.ForeColor = System.Drawing.Color.Lavender;   

        /* SECCION IMPRESION */
        lnkbBusImpr.Enabled = false; // 17
        lnkbBusImpr.ForeColor = System.Drawing.Color.Lavender;
    }

    private void ConfigurarMenu()
    {
        pe.oechsle.Entity.Usuario usrLogin = (pe.oechsle.Entity.Usuario)Session["login"];

        Session["UsuADMPC"] = false;
        Session["UsuMODCA"] = false;

        foreach (pe.oechsle.ex.Entity.Opcion opc in usrLogin.App.Opciones)
        {
            /* SECCION PARAMETROS */

            if (lnkbTipCart.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbTipCart.Enabled = true;
                lnkbTipCart.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbTipProm.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbTipProm.Enabled = true;
                lnkbTipProm.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbGrupCat.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbGrupCat.Enabled = true;
                lnkbGrupCat.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbCampos.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbCampos.Enabled = true;
                lnkbCampos.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbTienda.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbTienda.Enabled = true;
                lnkbTienda.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbGrupoTiendas.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbGrupoTiendas.Enabled = true;
                lnkbGrupoTiendas.ForeColor = System.Drawing.Color.Orange;
            }    

            /* SECCION CONFIGURACION */

            if (lnkbRegCartCamp.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbRegCartCamp.Enabled = true;
                lnkbRegCartCamp.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbConfCartel.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbConfCartel.Enabled = true;
                lnkbConfCartel.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkConfCartCategProm.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkConfCartCategProm.Enabled = true;
                lnkConfCartCategProm.ForeColor = System.Drawing.Color.Orange;
            }

            // INDICE = 20
            if (lnkCrearGuiaAuto.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkCrearGuiaAuto.Enabled = true;
                lnkCrearGuiaAuto.ForeColor = System.Drawing.Color.Orange;
            }

            /* SECCION CARTELERIA */

            if (lnkbCreaMan.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbCreaMan.Enabled = true;
                lnkbCreaMan.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbCreaAuto.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbCreaAuto.Enabled = true;
                lnkbCreaAuto.ForeColor = System.Drawing.Color.Orange;
            }

            if (lnkbConfExepciones.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbConfExepciones.Enabled = true;
                lnkbConfExepciones.ForeColor = System.Drawing.Color.Orange;
            }  

            /* SECCION IMPRESION */
            if (lnkbBusImpr.TabIndex.ToString() == Convert.ToString(opc.id))
            {
                lnkbBusImpr.Enabled = true;
                lnkbBusImpr.ForeColor = System.Drawing.Color.Orange;
            }

            /* ESTABLECE PERMISOS DE CONSULTAS ESPECIALES UN FLAG PARA EL TIPO DE USUARIO
             * ADMIN Y PUBLICISTA CENTRAL*/
            if (Convert.ToString(opc.id) == "18")
            {
                Session["UsuADMPC"] = true;
            }

            /* ESTABLECE PERMISO DE MODIFICACION DE CAMPOS DE PRECIO EN EL MODULO DE GUIA
             * AUTOMATICA PARA PERFILES QUE TENGAN ESTE PERMISO EN EL SISTEMA DE SEGURIDAD
             * DE SP*/
            if (Convert.ToString(opc.id) == "19")
            {
                Session["UsuMODCA"] = true;
            } 
        }       
    }   
}
