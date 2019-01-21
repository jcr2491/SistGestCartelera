<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SegLogin.aspx.cs" Inherits="SegLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Sistema de Cartelería</title>  
    
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
                                    
                                    <table>
                                    
                                        <tr align="left">
                                            <td colspan="2">
                                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td colspan="2">
                                                 &nbsp;
                                            </td>
                                        
                                        </tr>
                                        
                                        <tr>
                                            
                                            <td>
                                                Usuario:
                                            </td>
                                                
                                            <td>
                                                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2">
                                              
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                Clave:
                                            </td>
                                            
                                            <td>
                                                <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        
                                        <tr>                                        
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                            
                                        </tr>
                                            
                                        <tr>
                                        
                                            <td colspan="2">
                                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                            
                                            </td>
                                        
                                        </tr>
                                    
                                    </table>
                                
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

