<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ParTiendas.aspx.cs" Inherits="ParTiendas" Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/Pagina.css" rel="stylesheet" type="text/css" />
    <link href="css/GridView.css" rel="stylesheet" type="text/css" />
    
    <script src="js/General.js" type="text/javascript"></script>
    
    <style type="text/css">
        .ColumnaOculta {display:none;}        
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    
    <div align="center" style="background-color:  #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
	    Mantenimiento de Tiendas
    </div> 
    
    <br />
    
    <ajax:UpdatePanel ID="upnlTiendas" runat="server">

        <ContentTemplate>
        
            <div class="DivBorder">
    
                <table>
                
                    <tr>
                        <td>
                            Tienda :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNomTienda" runat="server"></asp:TextBox>
                           
                        </td>    
                         <td>
                             &nbsp;&nbsp;&nbsp;
                             </td>
                         <td>
                             <asp:Button ID="btnBuscarT" runat="server" Text="Buscar" 
                                 onclick="btnBuscarT_Click" />
                        </td>            
                    </tr>
                    
                </table>
            
            </div>
            
            <br />
            
            <div style =" background-color:#FF6600;  height:15px;width:100%; margin:0;padding:0">
           
                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                 style="font-family:Arial;font-size:12px;width:100%;color:white;
                 border-collapse:collapse;height:100%;">
                    <tr>
                       <td style ="width:20px;text-align:center"></td>
                       <td style ="width:14px;text-align:center">Id</td>
                       <td style ="width:200px;text-align:center">Tienda</td>
                    </tr>
                </table>
                
            </div>
            
            <div class="DivGridview" align="center" style="height:200px;">
                
                <asp:GridView ID="grvTienda" runat="server" AutoGenerateColumns="False" 
                    CssClass="Grid" DataKeyNames="ID_TIENDA" ShowHeader="false"
                    onrowdatabound="grvTienda_RowDataBound" >
                
                    <RowStyle CssClass="GridRow" />
                
                    <Columns>
                    
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" 
                                        ImageUrl="~/images/edit 16x16.png" 
                                        onclick="btnEditar_Click" />                            
                                </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton ID="btnEliminar" runat="server" 
                                         ImageUrl="~/images/icono_eliminar.gif" 
                                         onclick="btnEliminar_Click" />
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ID_TIENDA" HeaderText="Id"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ControlStyle Font-Size="11px" Width="30px" />
                            <HeaderStyle Font-Size="12px" Width="30px" />
                        </asp:BoundField> 
                            
                        <asp:BoundField DataField="NOM_TIENDA" HeaderText="Tienda"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Font-Size="12px" Width="400px"/>
                        </asp:BoundField>                
                       
                    </Columns>
                    
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    
                </asp:GridView>
                
            </div> 
            
            <br />
            
            <div align="left">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />    
            </div>
            
            <br />
            
             <asp:Panel ID="pnlDetalle" runat="server" Style="visibility:hidden;">
            
                <div id="Detalle" class="DivBorder" align="center"> 
                
                    <div class="DivBoxDetTiendas">
                    
                        <table>
                
                            <tr>
                                <td colspan="3" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                             <tr>
                                <td colspan="3">                    
                                     <asp:HiddenField ID="hidId" runat="server"/>
                                </td>
                            </tr>                
                            <tr>
                                <td colspan="3">                    
                                    &nbsp;
                                </td>
                            </tr>  
                             <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    Id:
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtId" MaxLength="150" Width="100px"></asp:TextBox>
                                </td>
                            </tr>          
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Descripción:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDescripcion" MaxLength="150" Width="350px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"> 
                                    &nbsp;
                                </td>
                            </tr>           
                            <tr>
                                <td colspan="3" align="center"> 
                                    &nbsp;
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                        onclick="btnGuardar_Click" Text="Guardar" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="true" Text="Cancelar" />
                                </td>
                            </tr>
                        
                        </table>
                    
                    </div>
                    
                </div>
                
            </asp:Panel>
            
        </ContentTemplate>
    
     </ajax:UpdatePanel>
     
</asp:Content>

