<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="CartConfExepciones.aspx.cs" Inherits="CartConfExepciones" %>

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
	    Configuración Productos Con Excepciones
    </div> 
    
    <br />    
    
    <ajax:UpdatePanel ID="upndlBusqueda" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <asp:Panel id="pnlBusqueda" runat="server" Visible="true">
              
                   <div id="divFiltroBusqueda" class="DivBorder">
                 
                        <table>
                            
                            <tr>
                                <td align="right">
                                    Fecha Inicio Vigencia:
                                </td>
                                
                                <td>
                                    <asp:TextBox ID="txtFecIniB" runat="server"></asp:TextBox>                           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecIniB" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                
                                <td align="right">
                                    Fecha Fin Vigencia:
                                </td>
                                
                                <td>
                                    <asp:TextBox ID="txtFecFinB" runat="server"></asp:TextBox>                           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFecFinB" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Grupo:</td>
                                <td>
                                    <asp:DropDownList ID="cboGrupoB" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>    
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Nombre Guia:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomGuiaB" runat="server" Width="193px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="left">
                                    &nbsp;</td>
                                <td align="right">
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td align="right">
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                     &nbsp;
                                </td>
                                <td>
                                     &nbsp;
                                </td>
                                <td>
                                     &nbsp;
                                </td>
                                <td>
                                     &nbsp;
                                </td>
                            </tr>
                        </table>
                 
                 </div>
                 
                 <div class="DivBorder">
                 
                    <table>
                    
                        <tr>
                            <td>
                                <asp:Button id="btnBusquedaGuia" runat="server" Text="Buscar" 
                                    onclick="btnBusquedaGuia_Click" Width="85px" />
                            </td>
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                 
                            </td>
                        </tr>
                        
                    </table>
                    
                </div>
                
                <br /> 
                
                <div id="divGrilla" class="DivBorder" align="center">
                                
                    <asp:GridView ID="grvGuias" runat="server" AutoGenerateColumns="False"  
                        CssClass="Grid">
                        
                        <RowStyle CssClass="GridRow" />
                        
                        <Columns>
                        
                            <asp:TemplateField>
                                 <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" 
                                            ImageUrl="~/images/edit 16x16.png" onclick="btnEditar_Click" />                            
                                    </ItemTemplate>
                                <ControlStyle Width="20px" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                        
                            <asp:BoundField DataField="ID_GUIA" HeaderText="IdGuia" ReadOnly="True" 
                                SortExpression="ID_GUIA" />                    
                            <asp:BoundField DataField="NOM_GUIA" HeaderText="NomGuia" ReadOnly="True" 
                                SortExpression="NOM_GUIA" /> 
                            <asp:BoundField DataField="FECHA_INI" HeaderText="FecInicio" ReadOnly="True" 
                                SortExpression="FECHA_INI" DataFormatString="{0:d}" HtmlEncode="False" />                    
                            <asp:BoundField DataField="FECHA_FIN" HeaderText="FecFin" ReadOnly="True" 
                                SortExpression="FECHA_FIN" DataFormatString="{0:d}" HtmlEncode="False" />
                             <asp:BoundField DataField="ID_TIENDA" HeaderText="IdTienda" 
                                ReadOnly="True" SortExpression="ID_TIENDA" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ID_GRUPO" HeaderText="IdGrupo" 
                                ReadOnly="True" SortExpression="ID_GRUPO" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>                    
                            <asp:BoundField DataField="ESTADO_GUIA" HeaderText="IdEstado" ReadOnly="True" 
                                SortExpression="ESTADO_GUIA" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESTADO_GUIA" HeaderText="Estado" ReadOnly="True" 
                                SortExpression="DESTADO_GUIA" />
                        </Columns>
                        
                          <PagerStyle BackColor="#FF6600" Font-Bold="True" Font-Size="Small" 
                          ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                      
                    </asp:GridView>
                    
                </div>
                
                <asp:Panel ID="pnlBarraPaginadora" runat="server" Visible="False">
                
                    <div class="DivBorder" style="background-color: #FF6600">
                
                        <table width="100%">
                        
                            <tr>
                            
                                <td>
                                    <asp:Button ID="btnFirstPage" runat="server" Text="|<" Font-Bold="True" 
                                        onclick="btnFirstPage_Click" /> 
                                    <asp:Button ID="btnPreviousPage" runat="server" Text="<" Font-Bold="True" 
                                        onclick="btnPreviousPage_Click" /> 
                                    <asp:Button ID="btnNextPage" runat="server" Text=">" Font-Bold="True" 
                                        onclick="btnNextPage_Click" />
                                    <asp:Button ID="btnLastPage" runat="server" Text=">|" Font-Bold="True" 
                                        onclick="btnLastPage_Click" />
                                </td>
                                
                                <td>
                                    <asp:Label ID="lblEtiquetaPaginador" runat="server" Text="Ir a Pagina :" Font-Bold="True" 
                                        ForeColor="White"> </asp:Label>
                                    <asp:TextBox ID="txtNroPagina" runat="server" Width="36px"></asp:TextBox>                                
                                    <asp:Button ID="btnIrAPagina" runat="server" Text="Ir" 
                                        onclick="btnIrAPagina_Click" Width="24px" />                               
                                </td>
                                
                                <td>
                                    <asp:Label ID="lblIndicador" runat="server" Text="" Font-Bold="True" ForeColor="White"></asp:Label>                               
                                    <asp:HiddenField ID="hidPageNumber" runat="server" />
                                    <asp:HiddenField ID="hidPageCount" runat="server" />
                                </td>
                                
                            </tr>
                            
                        </table>
                        
                    </div>
                
                </asp:Panel>             
              
            </asp:Panel>
        
        </ContentTemplate>
        
    </ajax:UpdatePanel>    
    
    <ajax:UpdatePanel ID="upnlDetalle" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
            
             <asp:Panel ID="pnlDetProdTiendas" runat="server" Visible="false">
             
                <br />
                
                <%--AQUI SE TIENE EL CRITERIO DE BUSQUEDA DE LOS PRODUCTOS--%>                
                <div id="divBusqueda" class="DivBorder" align="center">
                
                    <table>
                        <tr>
                            <td align="right">
                                    Nombre Producto:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNomProdBus" runat="server" Width="193px"></asp:TextBox>
                            </td>
                             <td>
                                <asp:Button ID="btnBusProd" runat="server" Text="Busqueda" 
                                     onclick="btnBusProd_Click" />
                            </td>    
                        </tr>
                    </table>
                
                </div>
                
                <br /> 
        
                <div  id="divGrillaDetalle" class="DivGridview" align="center" style="height:250px;">
                
                    <asp:GridView ID="grvListProdClusterTiendas" runat="server" CssClass="Grid" 
                         DataKeyNames="ID_GUIA" 
                         onrowcreated="grvListProdClusterTiendas_RowCreated" >
                    
                            <Columns>
                        
                                <asp:TemplateField>
                                     <ItemTemplate>
                                            <asp:ImageButton Id="btnEditar1" runat="server" 
                                               CommandName="Editar" ImageUrl="~/images/edit 16x16.png" 
                                               CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                                                onclick="btnEditar1_Click" >
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
            
                <div class="DivBorder">
                                
                    <table>
                
                        <tr>
                            <td>
                                <asp:Button ID="btnCancelarDE" runat="server" onclick="btnCancelarDE_Click" 
                                    Text="Salir" Width="85px" />
                            </td>
                        </tr>
                    
                    </table>    
                
                </div>
            
             </asp:Panel>    
             
             <br />  
             
             <asp:Panel ID="pnlDetTiendas" runat="server" Visible="False">
                
                <div id="Detalle" class="DivBorder" align="center">
                
                    <div class="DivBoxDetExepciones">                        
                        
                        <table>
                        
                             <tr>
                                <td style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                            
                            <tr>
                                <td>
                                     <asp:HiddenField ID="hidIdGuia" runat="server" Value="" />                         
                                     <asp:HiddenField ID="hidIdLinea" runat="server" Value="" />
                                     <asp:HiddenField ID="hidTienda" runat="server" Value="" />
                                     <asp:HiddenField ID="hidGrupo" runat="server" Value="" />
                                </td>
                            </tr>
                            <tr>                   
                                <td>                                     
                                     
                                   <div id="divInfDetTiendas" class="DivGridview" align="center" style="height:200px;">
                                 
                                         <asp:GridView ID="grvTiendas" runat="server" 
                                             AutoGenerateColumns="False" Height="126px" CssClass="Grid"
                                             DataKeyNames="ID_TIENDA" >
                                            
                                            <RowStyle CssClass="GridRow" />
                                            
                                            <Columns>
                                                
                                                <asp:BoundField DataField="ID_TIENDA" HeaderText="Id"  ReadOnly="true" >
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                                                    <ControlStyle Font-Size="11px" Width="50px" />
                                                    <HeaderStyle Font-Size="12px" Width="50px" BackColor="#FF6600" 
                                                        Font-Bold="True" ForeColor="White" 
                                                        HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ColumnaOculta" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="NOM_TIENDA" HeaderText="Tienda"  ReadOnly="true" >
                                                    <ItemStyle HorizontalAlign="Center" Width="250px" />
                                                    <ControlStyle Font-Size="11px" Width="250px" />
                                                    <HeaderStyle Font-Size="12px" Width="250px" BackColor="#FF6600" 
                                                        Font-Bold="True" Font-Overline="False" ForeColor="White" 
                                                        HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                </asp:BoundField>           
                                                
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                     <ItemTemplate>
                                                         <asp:CheckBox ID="chkSelection" runat="server" EnableViewState="true" 
                                                            Checked='<%# Convert.ToBoolean(Eval("FLAGPERTENECE")) %>' >
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
                                
                            </tr>        
                                    
                            <tr>
                                <td> 
                                    &nbsp;
                                </td>
                            </tr>  
                                     
                            <tr>
                                <td align="center"> 
                                    &nbsp;
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                        Text="Guardar" onclick="btnGuardar_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="true" 
                                        Text="Cancelar" onclick="btnCancelar_Click" />
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

