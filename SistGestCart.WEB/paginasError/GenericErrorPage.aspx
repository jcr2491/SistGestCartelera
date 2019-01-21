<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericErrorPage.aspx.cs" Inherits="paginasError_GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Sistema de Carteleria</title>
    <link href="../css/Estilos.css" rel="stylesheet" type="text/css" />        
    <link href="../images/Logo.jpg" rel="SHORTCUT ICON" />
    
    <script type="text/javascript">
        function mostrarMensaje()
        {
            var obj_div = document.getElementById('<%=div_StackTrace.ClientID %>');
            obj_div.style.display = obj_div.style.display == 'none'?'block':'none';
        }
    </script> 
    
</head>

<body>
    <form id="form1" runat="server">
    
        <table cellspacing="0" width="100%" cellpadding="0" id="Table2">
            <tr>
                <td style="background-image: url(images/logo_background.gif); width: 333px; padding-left:20px">
                    <img src="../images/Logo.jpg" />                    
                </td>
            </tr>
        </table>
        
        <div style="padding-left: 40px; margin-top: 10px">
        
            <table id="Table5" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td valign="top" rowspan="3">
                        <img src="../images/Error.jpg" />                        
                    </td>
                    <td style="font-weight: bold; font-size: 18px; border-bottom: gainsboro 1px solid">
                        Ocurrió un error inesperado en la aplicación</td>
                </tr>
                <tr>
                    <td align="left">
                        <a href="../Default.aspx">Ir a la pagina principal</a><br />
                        <a href="../SegLogin.aspx">Volver al Inicio</a>
                    </td>
                </tr>
            </table>
            
            <br />
            
            <table style="width: 100%" id="tbl_Exception" runat="server">
                <tr>
                    <td class="cssLinkButton" style="font-size: small">
                       Información técnica
                    </td>
                </tr>
                <tr>
                    <td style="line-height: 18px">
                        <div id="div_Mensaje">
                            <b>Mensaje:</b>&nbsp;<asp:Label ID="lbl_ErrorMsg" runat="server"></asp:Label><br />
                            <b><a href="#" onclick="javascript:mostrarMensaje();">Stack Trace: </a></b><br />
                            <div id="div_StackTrace" runat="server" style="background-color: #EFEFEF; font-family: Monospace;
                                width: 95%; padding: 5px 5px 5px 5px; border: 1px solid #d0d0d0; margin-top: 3px; display:none">
                                
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            
        </div>
        
    </form>
    
</body>

</html>
