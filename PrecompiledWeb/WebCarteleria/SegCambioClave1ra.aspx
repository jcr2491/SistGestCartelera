<%@ page language="C#" autoeventwireup="true" inherits="SegCambioClave1ra, App_Web_uqd17gdx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    
</head>

<body>    
        <div id="linea"></div>
    
        <div id="wrapper">
        
            <div id="header">
            
                <div id="logo">
        
                   <img src="images/Logo.jpg" alt="" width="200" height="70" />
                    
                </div>	       
    		    
	            <div id="titulo">
		            <h1>Sistema de Gestión de Cartelería</h1>
		            <h2><span>Grupo Intercorp Retail</span></h2>
	            </div>
	            <!-- end div#menu -->
	                            
            </div>
            <!-- end div#header -->
            
            <div id="page">
    
                <div id="page-bgtop">
                        
                    <div id="content">
                
                            <form id="form1" runat="server">                                   
                                        
                                <div>

                                    <table>
                                    
                                        <tr align="left">
                                            <td colspan="2">
                                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                <asp:HiddenField ID="hidUserLogin" EnableViewState="true" runat="Server" Value="" /> 
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td colspan="2">
                                                <asp:CheckBox ID="chkFlagSeguridad" runat="server" Style="visibility:hidden;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Ingrese Clave Actual:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtClaveActual" runat="server" TextMode="Password" 
                                                    MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Ingrese la Nueva Clave:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewClave" runat="server" TextMode="Password" 
                                                    MaxLength="150"></asp:TextBox>
                                            </td>            
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Confirme la Nueva Clave:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtConfNewClave" runat="server" TextMode="Password" 
                                                    MaxLength="150"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        
                                    </table>

                                </div>        
                                
                                <div>
                                
                                    <table>
                                    
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnGrabar" runat="server" Text="Aceptar" onclick="btnGrabar_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSalir" runat="server" Text="Salir" onclick="btnSalir_Click" 
                                                    Width="69px" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                    
                                </div>             
                            
                            </form>   
                            
                    </div>
                    <!-- end div#content -->
                    
                    <div id="sidebar">
                    
                    </div>
                    <!-- end div#sidebar -->
                    
		            <div style="clear: both; height: 1px"></div>
                    
                </div>
                
            </div>
        
        </div>
        
        <br />            
        
        <div id="footer">
	        <p id="legal">Copyright &copy; 2012 Grupo Intercorp Retail. All Rights Reserved. Designed by <a href="http://www.freecsstemplates.org">SGH</a>.</p>
	        <p id="links"><a href="#">Privacy Policy</a> | <a href="#">Terms of Use</a></p>
        </div>
        <!-- end div#footer -->
        
     </body>
         
</html>
