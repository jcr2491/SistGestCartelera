<%@ page language="C#" masterpagefile="~/Principal.master" autoeventwireup="true" inherits="ImprBusqImpresion, App_Web_1j973gow" title="Página sin título" %>

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
    
    <ajax:UpdatePanel id="upnlCabecera" runat="server" UpdateMode="Conditional">
       
        <ContentTemplate>
            
            <div align="center" style="background-color:  #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
	            Búsqueda e Impresión
            </div>
            
            <br />
            
            <asp:Panel id="pnlCabecera" runat="server" Visible="true">
              
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
                                    Nombre Guia:</td>
                                <td>
                                    <asp:TextBox ID="txtNomGuiaB" runat="server" Width="193px"></asp:TextBox>
                                </td>    
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Categoría:&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboCategoriaB" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Promocion:</td>
                                <td align="left">
                                    <asp:DropDownList ID="cboPromocionB" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblLocal" runat="server" Text="Local:" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboTiendaB" runat="server" Height="22px" Visible="False" 
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tipo Guia:</td>
                                <td>
                                    <asp:DropDownList ID="cboTipoGuia" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
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
                                <td>
                                     &nbsp;
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            
                        </table>
                 
                 </div>
                 
                 <div class="DivBorder">
                 
                    <table>
                    
                        <tr>
                            <td>
                                <asp:Button ID="btnBusqueda" runat="server" Text="Busqueda"
                                 onclick="btnBusqueda_Click" />
                            </td>                  
                            
                        </tr>
                        
                    </table>
                    
                </div>
                
                <br /> 
                
                <div id="divGrilla" class="DivBorder" align="center">
                
                    <asp:GridView id="grvGuias" runat="server" AutoGenerateColumns="False"  
                        CssClass="Grid" >
                        
                        <PagerSettings FirstPageImageUrl="~/images/NavFirstPage.gif" 
                            FirstPageText="Primero" LastPageImageUrl="~/images/NavLastPage.gif" 
                            LastPageText="Ultimo" Mode="NextPreviousFirstLast" 
                            NextPageImageUrl="~/images/NavNextPage.gif" NextPageText="Siguiente" 
                            PreviousPageImageUrl="~/images/NavPreviousPage.gif" 
                            PreviousPageText="Anterior" />
                        
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
                            <asp:BoundField DataField="DES_TIPO_GUIA" HeaderText="TipGuia" ReadOnly="True" 
                                SortExpression="DES_TIPO_GUIA" />
                                
                            <asp:BoundField DataField="ID_CATEGORIA" HeaderText="IdCategoria" 
                                ReadOnly="True" SortExpression="ID_CATEGORIA" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NOM_CATEGORIA" HeaderText="NomCategoria" 
                                ReadOnly="True" SortExpression="NOM_CATEGORIA" />
                               
                            <asp:BoundField DataField="FECHA_INI" HeaderText="FecInicio" ReadOnly="True" 
                                SortExpression="FECHA_INI" DataFormatString="{0:d}" HtmlEncode="False" />
                            <asp:BoundField DataField="FECHA_FIN" HeaderText="FecFin" ReadOnly="True" 
                                SortExpression="FECHA_FIN" DataFormatString="{0:d}" HtmlEncode="False" />
                            
                            <asp:BoundField DataField="ID_TIENDA" HeaderText="IdTienda" 
                                ReadOnly="True" SortExpression="ID_TIENDA" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NOM_TIENDA" HeaderText="NomTienda" 
                                ReadOnly="True" SortExpression="NOM_TIENDA" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                                
                            <asp:BoundField DataField="ID_GRUPO" HeaderText="IdGrupo" 
                                ReadOnly="True" SortExpression="ID_GRUPO" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="NOM_GRUPO" HeaderText="NomGrupo" 
                                ReadOnly="True" SortExpression="NOM_GRUPO" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="VIGENCIA" HeaderText="Vigencia" ReadOnly="True" 
                                SortExpression="VIGENCIA" />
                             
                             <asp:BoundField DataField="ID_PROMOCION" HeaderText="IdPromocion" 
                                ReadOnly="True" SortExpression="ID_PROMOCION" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
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
    
    <ajax:UpdatePanel id="upnlDetalle" runat="server" UpdateMode="Conditional">
        
        <ContentTemplate>
        
             <asp:Panel id="pnlDetalleGuia" runat="server" Visible="false">
        
                 <div id="divDetalleGuia" class="DivBorder" align="center">
                 
                    <div>
                    
                        <table>
                    
                            <tr>
                                <td colspan="6" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                             
                            <tr>
                                <td align="right">
                                    Fecha Inicio Vigencia:
                                </td>                        
                                <td align="left">
                                    <asp:Label ID="lblFecIni" runat="server" Font-Bold="True"></asp:Label>
                                </td>                        
                                <td align="right">
                                    Fecha Fin Vigencia:
                                </td>                        
                                <td align="left">
                                    <asp:Label ID="lblFecFin" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Label ID="lblLocalOGrupo" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblNomGrupoOLocal" runat="server" Font-Bold="True"></asp:Label>
                                </td>    
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Categoría:</td>
                                <td align="left">
                                    <asp:Label ID="lblCategoria" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;Nombre Guía:</td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNomGuia" 
                                        runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="right">
                                    Promoción:</td>
                                <td align="left">
                                    <asp:Label ID="lblPromocionDet" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" colspan="6">
                                    <asp:HiddenField ID="hdIdGuia" runat="server" />
                                    <asp:HiddenField ID="hidIDGrupo" runat="server" />
                                    <asp:HiddenField ID="hidIdCategoria" runat="server" />
                                    <asp:HiddenField ID="hidIdTienda" runat="server" />
                                    <asp:HiddenField ID="hidPromocion" runat="server" />
                                </td>
                            </tr>
                            
                        </table>
                        
                    </div>                    
                        
                </div>
                
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
                 
                <div id="divGrillaDetalle" class="DivGridview" align="center" style="height:200px;" >
                
                    <asp:GridView id="grvDetalleGuias" runat="server" CssClass="Grid" 
                         onrowcreated="grvDetalleGuias_RowCreated" 
                         onrowdatabound="grvDetalleGuias_RowDataBound" >
                    
                        <Columns>
                    
                        <asp:TemplateField>
                            
                             <ItemTemplate>
                                    <asp:ImageButton Id="btnEditarDetalle" runat="server" 
                                       CommandName="Editar" ImageUrl="~/images/edit 16x16.png" 
                                       CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                                        onclick="btnEditarDetalle_Click" >
                                    </asp:ImageButton>     
                                </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="CARTEL">
                             <ItemTemplate>
                                 <asp:DropDownList ID="ddlCarteles" runat="server" Height="22px">
                                 </asp:DropDownList>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                             <ControlStyle Font-Size="11px" Width="200px" />
                             <HeaderStyle Font-Size="12px" Width="200px" BackColor="#FF6600" 
                                    Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Center" VerticalAlign="Middle"/>
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="IMPRIMIR">
                             <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" 
                                     runat="server" AutoPostBack="false" />                                
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:CheckBox ID="chkSelection" runat="server" AutoPostBack="false"></asp:CheckBox>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                             <ControlStyle Font-Size="11px" Width="70px" />
                             <HeaderStyle Font-Size="12px" Width="70px" BackColor="#FF6600" 
                                    Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Center" VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="COPIAS">
                             <ItemTemplate>
                                 <asp:TextBox ID="txtNroPag" runat="server" Text="">
                                 </asp:TextBox>
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                             <ControlStyle Font-Size="11px" Width="30px" />
                             <HeaderStyle Font-Size="12px" Width="30px" BackColor="#FF6600" 
                                    Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Center" VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        
                      </Columns>
                      
                      <HeaderStyle CssClass="GridHeader" />
                      <AlternatingRowStyle CssClass="GridAlternateRow" />
                      
                    </asp:GridView>
                    
                </div>
                
                <br />
                    
                    <div>
                        <table>
                    
                            <tr>
                                <td>
                                    <asp:Button ID="btnImprimir" runat="server" 
                                        Text="Imprimir" onclick="btnImprimir_Click" />
                                </td>                   
                                <td>
                                     &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                     <asp:Button ID="btnCancelarImpresion" runat="server" Text="Salir" 
                                         onclick="btnCancelarImpresion_Click" Width="71px" 
                                         />
                                </td>
                                
                            </tr>
                            
                        </table>
                    </div>
            
            </asp:Panel>
            
            <asp:Panel ID="pnlImpresion" runat="server" Visible="false">
                
                <div ID="divGrvImpresion" class="DivGridview" align="center" style="height:200px;" >
                    
                    <asp:GridView ID="GrvImpresion" runat="server" CssClass="Grid" AutoGenerateColumns="False" >
                        
                        <RowStyle CssClass="GridRow" />
                        
                        <Columns>                           
                            
                            <asp:BoundField DataField="ID_CARTEL_MODELO" HeaderText="IdCartel" 
                                ReadOnly="True" SortExpression="ID_CARTEL_MODELO" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NOM_CARTEL_MODELO" HeaderText="CARTEL" 
                                ReadOnly="True" SortExpression="NOM_CARTEL_MODELO" >                               
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NOM_MODELO" HeaderText="MODELO" 
                                ReadOnly="True" SortExpression="NOM_MODELO" />
                            
                            <asp:BoundField DataField="TIPO_HOJA" HeaderText="TIPO HOJA" 
                                ReadOnly="True" SortExpression="TIPO_HOJA" />
                                
                            <asp:BoundField DataField="NRO_COPIAS" HeaderText="CANT. COPIAS" 
                                ReadOnly="True" SortExpression="NRO_COPIAS" />
                                
                            <asp:TemplateField>
                                 <ItemTemplate>
                                     <asp:Button ID="btnImprimirFinal" runat="server" Text="Imprimir" 
                                     CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" onclick="btnImprimirFinal_Click" >
                                     </asp:Button> 
                                 </ItemTemplate>
                                <ControlStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                                
                        </Columns>
                        
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        
                    </asp:GridView>     
                    
                   <%-- <asp:GridView ID="GrvImpresion" runat="server" CssClass="Grid" AutoGenerateColumns="False" >
                        
                        <RowStyle CssClass="GridRow" />
                        
                        <Columns>                           
                            
                            <asp:BoundField DataField="ID_CARTEL_MODELO" HeaderText="IdCartel" 
                                ReadOnly="True" SortExpression="ID_CARTEL_MODELO" >
                                <HeaderStyle CssClass="ColumnaOculta" />
                                <ItemStyle CssClass="ColumnaOculta" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NOM_CARTEL_MODELO" HeaderText="CARTEL" 
                                ReadOnly="True" >                               
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NOM_MODELO" HeaderText="MODELO" 
                                ReadOnly="True" />
                            
                            <asp:BoundField DataField="TIPO_HOJA" HeaderText="TIPO HOJA" 
                                ReadOnly="True" />
                                
                            <asp:BoundField DataField="NRO_COPIAS" HeaderText="CANT. COPIAS" 
                                ReadOnly="True" />
                                
                            <asp:BoundField DataField="DIGITOS" HeaderText="NRO DIGITOS" 
                                ReadOnly="True" />
                                
                            <asp:TemplateField>
                                 <ItemTemplate>
                                     <asp:Button ID="btnImprimirFinal" runat="server" Text="Imprimir" 
                                     CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" onclick="btnImprimirFinal_Click" >
                                     </asp:Button> 
                                 </ItemTemplate>
                                <ControlStyle Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                                
                        </Columns>
                        
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                        
                    </asp:GridView>     --%>
                
                </div>
                
                <br />
                
                <div>
                 
                   <table>
                
                       <tr>
                          
                           <td>
                                 <asp:Button ID="btnCancelImpresion" runat="server" Text="Cancelar" onclick="btnCancelImpresion_Click" />
                           </td>
                            
                       </tr>
                        
                   </table>
                    
               </div>
                
            </asp:Panel>
            
            <br />
            
            <br />
            
            <asp:Panel ID="pnlDetalleImpresion" runat="server" Visible="false">
            
                <div ID="divFormImpresion" class="DivBorder" align="center">
                
                    <div>
                    
                        <table>
                    
                            <tr>
                                <td style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Promoción: 
                                    <asp:Label ID="lblNomPromocion" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                
                                    <asp:GridView ID="grvCamposObligDet" runat="server" AutoGenerateColumns="False">
                                    
                                        <Columns>
                                            <asp:BoundField DataField="ID_LINEA" HeaderText="IdLinea" ReadOnly="True" 
                                                SortExpression="ID_LINEA" >
                                                 <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                                                <ControlStyle Font-Size="11px" Width="50px" />
                                                <HeaderStyle Font-Size="12px" Width="50px" BackColor="#FF6600" 
                                                    Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ColumnaOculta" />
                                            </asp:BoundField> 
                                            
                                            <asp:BoundField DataField="ID_CAMPO" HeaderText="IdCampo" ReadOnly="True" 
                                                SortExpression="ID_CAMPO" >
                                                 <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                                                <ControlStyle Font-Size="11px" Width="50px" />
                                                <HeaderStyle Font-Size="12px" Width="50px" BackColor="#FF6600" 
                                                    Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle" CssClass="ColumnaOculta" />
                                            </asp:BoundField> 
                                            
                                            <asp:BoundField DataField="NOM_CAMPO" HeaderText="NomCampo" ReadOnly="True" 
                                                SortExpression="NOM_CAMPO" >
                                                 <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                <ControlStyle Font-Size="11px" Width="70px" />
                                                <HeaderStyle Font-Size="12px" Width="70px" BackColor="#FF6600" 
                                                    Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>    
                                            
                                             <asp:BoundField DataField="VALOR" HeaderText="Valor" ReadOnly="True" 
                                                SortExpression="VALOR" >
                                                 <ItemStyle HorizontalAlign="Center" Width="600px" />
                                                <ControlStyle Font-Size="11px" Width="600px" />
                                                <HeaderStyle Font-Size="12px" Width="600px" BackColor="#FF6600" 
                                                    Font-Bold="True" ForeColor="White" 
                                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField> 
                                            
                                        </Columns>
                                        
                                    </asp:GridView>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            
                        </table>
                     
                    </div>                    
                    
                </div>
                
                <br />
                
                <div>
                
                    <table>
                
                        <tr>
                          
                            <td>
                                 <asp:Button ID="btnCierraDetImpre" runat="server" Text="Cancelar" onclick="btnCierraDetImpre_Click" 
                                      />
                            </td>
                            
                        </tr>
                        
                    </table>
                    
                </div>
                    
            </asp:Panel>
    
        </ContentTemplate>               
        
     </ajax:UpdatePanel>     
     
</asp:Content>

