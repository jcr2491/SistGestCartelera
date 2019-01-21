<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ConfCartelCategPromo.aspx.cs" Inherits="ConfCartelCategPromo" Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/Pagina.css" rel="stylesheet" type="text/css" />
    <link href="css/GridView.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .ColumnaOculta {display:none;}        
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <br />
    <br />
    
    <div align="center" style="background-color: #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
	    Configuracion de Carteles (Asignar Carteles a Catgorías y Promociones)
    </div> 
    
    <br />
    
    <ajax:UpdatePanel ID="upnlDetalle" runat="server">
        
        <ContentTemplate>
            
            <div class="DivBorder">
    
                <table>
                    <tr>
                        <td>
                            Promoción :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboPromocion" runat="server" Height="22px" Width="250px" 
                                AutoPostBack="True" onselectedindexchanged="cboPromocion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                         <td>
                             Categoría:
                        </td>
                        <td>
                            <asp:DropDownList ID="cboCategoria" runat="server" Height="22px" Width="250px" 
                                AutoPostBack="True" onselectedindexchanged="cboCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                </table>
            
            </div>       
    
            <br />
            <br />    
        
            <asp:Panel ID="pnlListado" runat="server" Visible="false">
                
                <div style =" background-color:#FF6600;  height:15px;width:100%; margin:0;padding:0">
           
                    <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                     style="font-family:Arial;font-size:12px;width:100%;color:white;
                     border-collapse:collapse;height:100%;">
                        <tr>
                           <td style ="width:15px;text-align:center"></td>
                           <td style ="width:300px;text-align:center">Cartel</td>
                        </tr>
                    </table>
                    
                </div>
            
                <div id="divGrillaDetalle" class="DivGridview" align="center" style="height:200px;">
                    
                     <asp:GridView ID="grvListCartelCampo" runat="server" CssClass="Grid" 
                         DataKeyNames="ID_CARTEL,ID_MODELO,ID_CATEGORIA,ID_PROMOCION"
                         onrowcommand="grvListCartelCampo_RowCommand"                 
                         AutoGenerateColumns="False" ShowHeader="false"
                         onrowdatabound="grvListCartelCampo_RowDataBound" >
                         
                         <RowStyle CssClass="GridRow" />
                    
                            <Columns>                     
                                
                                <asp:TemplateField>
                                     <ItemTemplate>
                                             <asp:ImageButton Id="btnEliminar" runat="server" 
                                               CommandName="Eliminar" ImageUrl="~/images/icono_eliminar.gif" 
                                               CommandArgument="<%# ((GridViewRow)Container).RowIndex %>">
                                            </asp:ImageButton>   
                                     </ItemTemplate>
                                    <ControlStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="ID_CARTEL" HeaderText="IdCartel"  ReadOnly="true" >
                                    <ItemStyle HorizontalAlign="Center" Width="30px" CssClass="ColumnaOculta" />
                                    <ControlStyle Font-Size="11px" Width="30px" />
                                    <HeaderStyle Font-Size="12px" Width="30px" CssClass="ColumnaOculta" />
                                </asp:BoundField> 
                                    
                                <asp:BoundField DataField="ID_MODELO" HeaderText="IdModelo"  ReadOnly="true" >
                                    <ItemStyle HorizontalAlign="Center" Width="30px" CssClass="ColumnaOculta" />
                                    <ControlStyle Font-Size="11px" Width="30px" />
                                    <HeaderStyle Font-Size="12px" Width="30px" CssClass="ColumnaOculta" />
                                </asp:BoundField>
                                
                                 <asp:BoundField DataField="ID_CATEGORIA" HeaderText="IdCategoria"  ReadOnly="true" >
                                    <ItemStyle HorizontalAlign="Center" Width="30px" CssClass="ColumnaOculta" />
                                    <ControlStyle Font-Size="11px" Width="30px" />
                                    <HeaderStyle Font-Size="12px" Width="30px" CssClass="ColumnaOculta" />
                                </asp:BoundField>
                                
                                 <asp:BoundField DataField="ID_PROMOCION" HeaderText="IdPromocion"  ReadOnly="true" >
                                    <ItemStyle HorizontalAlign="Center" Width="30px" CssClass="ColumnaOculta" />
                                    <ControlStyle Font-Size="11px" Width="30px" />
                                    <HeaderStyle Font-Size="12px" Width="30px" CssClass="ColumnaOculta" />
                                </asp:BoundField>
                                
                                 <asp:BoundField DataField="DESCRIPCION" HeaderText="Cartel"  ReadOnly="true" >
                                    <ItemStyle HorizontalAlign="Center" Width="400px" />
                                    <ControlStyle Font-Size="11px" Width="400px" />
                                    <HeaderStyle Font-Size="12px" Width="400px"/>
                                </asp:BoundField>
                            
                          </Columns>
                          
                          <PagerStyle BackColor="#FF6600" Font-Bold="True" Font-Size="Small" 
                             ForeColor="White" HorizontalAlign="Center" />
                          
                          <HeaderStyle CssClass="GridHeader" />
                          <AlternatingRowStyle CssClass="GridAlternateRow" />
                          
                    </asp:GridView>         
                
                </div>
             
            </asp:Panel>   
            
            <br />         
           
            <asp:Panel ID="pnlBtnAgregar" runat="server" Visible="false">
            
                <div class="DivBorder">
                     <asp:Button ID="btnAgregar" runat="server" Text="Asociar Cartel" 
                       onclick="btnAgregar_Click" />
                </div> 
                              
            </asp:Panel>  
            
            <br />
            <br />
            
            <asp:Panel ID="pnlDetalleGrupo" runat="server" Visible="false">
            
                <div id="DetallGrupo" class="DivBorder" align="center">  
                
                    <div class="DivBoxDetConfCartCarProm">
                    
                        <table>
                    
                            <tr>
                                <td colspan="4" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                             <tr>
                                <td colspan="4">                    
                                     &nbsp;</td>
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
                                    
                                </td> 
                                <td>
                                    Seleccione Cartel - Modelo:
                                </td>
                                 <td>
                                     &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                   
                                </td>
                               <td>
                               </td> 
                               <td align="center">
                                    &nbsp;&nbsp;<asp:DropDownList ID="cboCartelModelo" runat="server" Height="22px" 
                                        Width="400px">
                                    </asp:DropDownList>                       
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
                                <td colspan="2"> 
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnGuardarGrupo" runat="server" Text="Guardar" 
                                      onclick="btnGuardarGrupo_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancelarGrupo" 
                                        Text="Cancelar" onclick="btnCancelarGrupo_Click" />                                 
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

