<%@ page language="C#" masterpagefile="~/Principal.master" autoeventwireup="true" validaterequest="false" enableeventvalidation="false" inherits="ConfReglCartelCamp, App_Web_1j973gow" title="Página sin título" %>

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
	    Reglas de Cartel y Campos
    </div>
    
    <br />
    
    <ajax:UpdatePanel ID="upnlReglasCamp" runat="server">
    
        <ContentTemplate>
        
             <div class="DivBorder">
    
                <table>
                
                    <tr>
                        <td>
                            Nombre Cartel-Modelo :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescCartMod" runat="server"></asp:TextBox>
                           
                        </td>    
                         <td>
                             &nbsp;&nbsp;&nbsp;
                             </td>
                         <td>
                             <asp:Button ID="btnBuscarDCM" runat="server" Text="Buscar" 
                                 onclick="btnBuscarDCM_Click" />
                        </td>            
                    </tr>
                    
                </table>
            
            </div>
            
            <br />
        
            <div id="divGrillaDetalle" class="DivGridview" align="center" style="height:250px;">
        
                 <asp:GridView ID="grvListCartelCampo" runat="server" CssClass="Grid" 
                     DataKeyNames="ID_CARTEL,ID_MODELO"                       
                     onrowcommand="grvListCartelCampo_RowCommand" 
                     onrowcreated="grvListCartelCampo_RowCreated" >
                
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
                        
                      </Columns>
                      
                        <PagerStyle BackColor="#FF6600" Font-Bold="True" Font-Size="Small" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                      
                      <HeaderStyle CssClass="GridHeader" />
                      <AlternatingRowStyle CssClass="GridAlternateRow" />
                      
                </asp:GridView>         
            
            </div>
            
            <br />
            <br />  
            
            <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
            
                <div id="Detalle" class="DivBorder" align="center">
                
                    <div class="DivBoxDetConfReglCamps">
                    
                        <table>
                    
                            <tr>
                                <td colspan="4" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                             <tr>
                                <td colspan="4">                    
                                     <asp:HiddenField ID="hidIdCartel" runat="server" Value="" />
                                     <asp:HiddenField ID="hidIdModelo" runat="server" Value="" />
                                     <asp:HiddenField ID="hidCampo" runat="server" Value="" />
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
                               <td align="left">
                                   <asp:TextBox ID="txtCartelModelo" runat="server" Width="430px"></asp:TextBox>
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
                                    Campos:
                                </td>
                                <td>
                                    <div id="divDetCampos" class="DivGridview" align="center" style="height:250px;">
                                    
                                        <asp:GridView ID="grvCampos" runat="server"  
                                         AutoGenerateColumns="False" Height="126px" CssClass="Grid">
                                        
                                             <RowStyle CssClass="GridRow" />
                                            
                                             <Columns>
                                                
                                                <asp:BoundField DataField="ID_CAMPO" HeaderText="Id"  ReadOnly="true" >
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                                                    <ControlStyle Font-Size="11px" Width="50px" />
                                                    <HeaderStyle Font-Size="12px" Width="50px" BackColor="#FF6600" 
                                                        Font-Bold="True" ForeColor="White" 
                                                        HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ColumnaOculta" />
                                                </asp:BoundField> 
                                                    
                                                <asp:BoundField DataField="NOM_CAMPO" HeaderText="Campo"  ReadOnly="true" >
                                                    <ItemStyle HorizontalAlign="Center" Width="250px" />
                                                    <ControlStyle Font-Size="11px" Width="250px" />
                                                    <HeaderStyle Font-Size="12px" Width="250px" BackColor="#FF6600" 
                                                        Font-Bold="True" Font-Overline="False" ForeColor="White" 
                                                        HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción"  ReadOnly="true" >
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
                                                          AutoPostBack="True" oncheckedchanged="chkSelection_CheckedChanged" />
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
                                <td colspan="2"> 
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                        onclick="btnGuardar_Click" Text="Guardar" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancelar" CausesValidation="true"  
                                        Text="Cancelar" onclick="btnCancelar_Click"
                                         />                                 
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

