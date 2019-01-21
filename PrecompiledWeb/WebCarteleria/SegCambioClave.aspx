<%@ page language="C#" masterpagefile="~/Principal.master" autoeventwireup="true" inherits="SegCambioClave, App_Web_1j973gow" title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <link href="css/Pagina.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <br />
    <br />
    
    <div align="center" style="background-color:  #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
	    Cambio de Contraseña
    </div> 
    
    <br />
    
    <ajax:UpdatePanel ID="upnlCambioPassw" runat="server">
    
        <ContentTemplate>
            
            <div align="center" class="DivBorder">

                <table>
                
                    <tr align="left">
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <asp:HiddenField ID="hidUserLogin" EnableViewState="true" runat="Server" Value="" /> 
                        </td>
                    </tr>
                    <tr align="left">
                        <td colspan="2">
                            <asp:CheckBox ID="chkFlagSeguridad" runat="server" Style="visibility:hidden;"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Ingrese Clave Actual:
                        </td>
                        <td>
                            <asp:TextBox ID="txtClaveActual" runat="server" TextMode="Password"></asp:TextBox>
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
                            <asp:TextBox ID="txtNewClave" runat="server" TextMode="Password"></asp:TextBox>
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
                            <asp:TextBox ID="txtConfNewClave" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    
                </table>

            </div>        
            
            <div align="center" class="DivBorder">
            
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
    
        </ContentTemplate>
        
     </ajax:UpdatePanel>
            
</asp:Content>

