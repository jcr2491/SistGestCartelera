<%@ page language="C#" enableeventvalidation="false" masterpagefile="~/Principal.master" autoeventwireup="true" inherits="ParGrupoTiendas, App_Web_1j973gow" title="Página sin título" %>

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
	    Agrupacion Tiendas
    </div>
    
    <br />
    
    <ajax:UpdatePanel ID="upnlGrupTdas" runat="server">

        <ContentTemplate>
                    
            <div style =" background-color:#FF6600;  height:15px;width:100%; margin:0;padding:0">
           
                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                 style="font-family:Arial;font-size:12px;width:100%;color:white;
                 border-collapse:collapse;height:100%;">
                    <tr>
                       <td style ="width:20px;text-align:center"></td>                       
                       <td style ="width:200px;text-align:center">Grupo (Clúster)</td>
                    </tr>
                </table>
                
            </div>
            
            <div align="center" class="DivGridview" style="height:200px;">
                
                <asp:GridView ID="grvGrupos" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                    CssClass="Grid" DataKeyNames="ID_GRUPO" onrowdatabound="grvGrupos_RowDataBound">
                
                    <RowStyle CssClass="GridRow" />
                
                    <Columns>
                    
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/images/edit 16x16.png" onclick="btnEditar_Click" />                            
                                </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/images/icono_eliminar.gif" onclick="btnEliminar_Click" />
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ID_GRUPO" HeaderText="Id"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Font-Size="12px" Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField> 
                            
                        <asp:BoundField DataField="NOM_GRUPO" HeaderText="Grupo de Tiendas"  ReadOnly="true" >
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
                <asp:Button ID="btnAgrupar" runat="server" Text="Agrupar Tiendas" 
                    onclick="btnAgrupar_Click" />   
            </div>
           
            <br />
            
            <asp:Panel ID="pnlDetalleGrupo" runat="server" Visible="False">
            
                <div id="DetallGrupo" class="DivBorder" align="center"> 
                
                    <div class="DivBoxDetGrupTiendas">
                    
                        <table>
                    
                            <tr>
                                <div>
                                </div>
                                <td style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                             <tr>
                                <td>                    
                                     <asp:HiddenField ID="hidId" runat="server" Value="" />
                                </td>
                            </tr>                
                            <tr>
                                <td>                    
                                    Ponga el nombre del grupo:
                                </td>
                            </tr>            
                            <tr>
                                <td>
                                    &nbsp;
                                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="150" Width="365px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    &nbsp; Seleccione la tienda: &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                               <td align="center">
                                   
                                   <asp:DropDownList ID="cboTienda" runat="server" Height="22px" Width="370px">
                                   </asp:DropDownList>
                                   &nbsp;&nbsp;&nbsp;
                                   
                               </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAñadirTienda" runat="server" onclick="btnAñadirTienda_Click" 
                                        Text="Añadir Tiendas" />
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>                        
                            <tr>
                                <td>
                                
                                   <div id="divGrupo" class="DivGridview" align="center" style="height:200px;">
                        
                                        <asp:GridView ID="grvTiendasGrupo" runat="server"
                                            AutoGenerateColumns="False" CssClass="Grid" DataKeyNames="ID_TIENDA" 
                                            Height="126px" Width = "350px" onrowcommand="grvTiendasGrupo_RowCommand">
                                            
                                            <RowStyle Height="5px" CssClass="GridRow" />
                                            
                                            <Columns>
                                                <asp:BoundField DataField="ID_TIENDA" HeaderText="IdTienda" ReadOnly="True" 
                                                    SortExpression="ID_TIENDA" >
                                                    <ControlStyle Font-Size="8px" />
                                                    <HeaderStyle CssClass="ColumnaOculta" Height="15px" />
                                                    <ItemStyle CssClass="ColumnaOculta" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="NOM_TIENDA" HeaderText="Tienda" ReadOnly="True" 
                                                    SortExpression="DESTADO_GUIA" >
                                                    <ControlStyle Font-Size="8px" />
                                                    <HeaderStyle Height="15px" />
                                                </asp:BoundField>
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEliminarTda" runat="server" 
                                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                                                            CommandName="Eliminar" ImageUrl="~/images/icono_eliminar.gif" />
                                                    </ItemTemplate>
                                                    <ControlStyle Width="20px" />
                                                    <HeaderStyle Height="15px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="9px" />                                           
                                                </asp:TemplateField>
                                            </Columns>
                                            
                                            <HeaderStyle CssClass="GridHeader" /> 
                                                
                                        </asp:GridView>
                                    
                                    </div>     
                                                  
                                </td>
                            </tr>
                            
                        </table>
                        
                        <br />
                                            
                        <div>
                            <table>                              
                                <tr>
                                    <td colspan="4" align="center"> 
                                        &nbsp;
                                        <asp:Button ID="btnGuardarGrupo" runat="server" CausesValidation="true" 
                                            onclick="btnGuardarGrupo_Click" Text="Guardar" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelarGrupo" runat="server" CausesValidation="true" 
                                            onclick="btnCancelarGrupo_Click" Text="Cancelar" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                
                            </table>
                            
                        </div>
                    
                    </div>                    
                    
                </div>
                
            </asp:Panel>
    
        </ContentTemplate>
    
     </ajax:UpdatePanel> 
     
</asp:Content>

