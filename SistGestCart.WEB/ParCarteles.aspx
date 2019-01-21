<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeFile="ParCarteles.aspx.cs" Inherits="ParCarteles" Title="Página sin título" %>

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
	    Mantenimiento de Tipo de Carteles
    </div>
    
    <br />
     
    <ajax:UpdatePanel ID="upnlCarteles" runat="server">

        <ContentTemplate>
        
            <div class="DivBorder">
    
                <table>
                
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNomCartelB" runat="server"></asp:TextBox>
                           
                        </td>    
                        <td>
                            Cartel :
                        </td>
                         <td>
                             <asp:Button ID="btnBuscarT" runat="server" Text="Buscar" 
                                 onclick="btnBuscarT_Click" />
                        </td>            
                         <td>
                             &nbsp;&nbsp;&nbsp;
                             </td>
                    </tr>
                    
                </table>
            
            </div>
            
            <br />         
    
            <div align="center" class="DivGridview" style="height:200px;">
                
                <asp:GridView ID="grvTipoCartel" runat="server" CssClass="Grid" 
                    DataKeyNames="ID_CARTEL"
                    onrowcreated="grvTipoCartel_RowCreated"
                    onrowdatabound="grvTipoCartel_RowDataBound"          
                    onrowcommand="grvTipoCartel_RowCommand" >
                
                    <Columns>
                    
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:ImageButton Id="btnEditar1" runat="server" 
                                       CommandName="Editar" ImageUrl="~/images/edit 16x16.png" 
                                       CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" >
                                    </asp:ImageButton>     
                                </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton Id="btnEliminar1" runat="server" 
                                       CommandName="Eliminar" ImageUrl="~/images/icono_eliminar.gif" 
                                       CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" >
                                    </asp:ImageButton>   
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField> 
                        
                      </Columns>
                      
                      <PagerStyle BackColor="#FF6600" Font-Bold="True" Font-Size="Small" 
                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                      
                      <HeaderStyle CssClass="GridHeader" />
                      <AlternatingRowStyle CssClass="GridAlternateRow" />
                      
                </asp:GridView>         
           
            </div> 
            
            <br />
            
            <div align="left">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="btnAgregar_Click" />
            </div>  
            
            <br />
                
            <asp:Panel ID="pnlDetalle" runat="server" Visible="False" >
            
                <div id="Detalle" class="DivBorder" align="center">
                
                    <div class="DivBoxDetCarteles">
                    
                        <table>
                    
                        <tr>
                            <td colspan="4" style="background-color: #FF6600">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr> 
                         <tr>
                            <td colspan="4">                    
                                 <asp:HiddenField ID="hidId" runat="server" Value="" />
                                 <asp:HiddenField ID="hidModelo" runat="server" Value="" />
                            </td>
                        </tr>                
                        <tr>
                            <td colspan="4">                    
                                &nbsp;
                            </td>
                        </tr>            
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                           <td>
                                Cartel:
                            </td> 
                           <td>
                                <asp:TextBox MaxLength="150" Width="350px" runat="server" ID="txtDescripcion"></asp:TextBox>
                            </td>
                             <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                         <tr>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                0 Digit. :
                            </td> 
                            <td align="left">                            
                                <asp:CheckBox ID="chkCeroDigitos" runat="server" Text="" TextAlign="Left" />
                            </td>                            
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>                          
                        </tr>                       
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Modelos:
                            </td>
                            <td>
                                
                              <div align="center" class="DivGridview" style="height:150px;">
                                
                                 <asp:GridView ID="grvModelos" runat="server"  
                                     AutoGenerateColumns="False" Height="126px" CssClass="Grid"
                                     DataKeyNames="ID_MODELO" >
                                    
                                     <RowStyle CssClass="GridRow" />
                                    
                                    <Columns>
                                        
                                        <asp:BoundField DataField="ID_MODELO" HeaderText="Id"  ReadOnly="true" >
                                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                                            <ControlStyle Font-Size="11px" Width="50px" />
                                            <HeaderStyle Font-Size="12px" Width="50px" BackColor="#FF6600" 
                                                Font-Bold="True" ForeColor="White" 
                                                HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ColumnaOculta" />
                                        </asp:BoundField> 
                                            
                                        <asp:BoundField DataField="NOM_MODELO" HeaderText="Modelo"  ReadOnly="true" >
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                            <ControlStyle Font-Size="11px" Width="250px" />
                                            <HeaderStyle Font-Size="12px" Width="250px" BackColor="#FF6600" 
                                                Font-Bold="True" Font-Overline="False" ForeColor="White" 
                                                HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>                
                                        
                                        <asp:TemplateField HeaderText="Seleccionar">
                                             <ItemTemplate>
                                                 <asp:CheckBox ID="chkSelection" runat="server" 
                                                    Checked='<%# Convert.ToBoolean(Eval("FLAGPERTENECE")) %>' 
                                                     oncheckedchanged="chkSelection_CheckedChanged" 
                                                     EnableViewState="true" AutoPostBack="True">
                                                 </asp:CheckBox>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                             <ControlStyle Font-Size="11px" Width="100px" />
                                             <HeaderStyle Font-Size="12px" Width="100px" BackColor="#FF6600" 
                                                    Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:TemplateField>  
                        
                                    </Columns>
                                    
                                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    
                                 </asp:GridView>
                                 
                                 </div>
                                 
                            </td>
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4"> 
                                &nbsp;
                            </td>
                        </tr>           
                        <tr>
                            <td colspan="3" align="center"> 
                                &nbsp;
                                <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                    onclick="btnGuardar_Click" Text="Guardar" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" CausesValidation="true" 
                                    onclick="btnCancelar_Click" Text="Cancelar" />
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;                                 
                            </td>
                        </tr>
                        
                    </table>
                    
                  </div>
                  
                </div>                      
                
            </asp:Panel>   
    
        </ContentTemplate>
    
     </ajax:UpdatePanel>    
     
</asp:Content>

