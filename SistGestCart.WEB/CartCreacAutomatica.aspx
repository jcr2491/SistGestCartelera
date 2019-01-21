<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="CartCreacAutomatica.aspx.cs" Inherits="CartCreacAutomatica" Title="Página sin título" %>


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
    
   <ajax:UpdatePanel id="updnlCabecera" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <div align="center" style="background-color:  #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
	            Creación Automática
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
                                    Grupo:</td>
                                <td>
                                    <asp:DropDownList ID="cboGrupoB" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>    
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    Estado Guía:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboEstadoB" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Nombre Guia:</td>
                                <td align="left">
                                    <asp:TextBox ID="txtNomGuiaB" runat="server" Width="193px"></asp:TextBox>                            
                                </td>
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
                                <asp:Button id="btnBusquedaGuia" runat="server" Text="Busqueda de Guías Masivas" 
                                    onclick="btnBusquedaGuia_Click" />
                            </td>
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                 <asp:Button id="btnCrearGuia" runat="server" Text="Crear Nueva Guía Automatica" 
                                     onclick="btnCrearGuia_Click" />
                            </td>
                        </tr>
                        
                    </table>
                    
                </div>
                
                <br /> 
                
                <div id="divGrilla" class="DivBorder" align="center">
                
                    <asp:GridView ID="grvGuias" runat="server" AutoGenerateColumns="False"  
                        CssClass="Grid" onrowdatabound="grvGuias_RowDataBound">
                        
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
                            
                            <asp:TemplateField>
                                 <ItemTemplate>
                                         <asp:ImageButton ID="btnEliminar" runat="server" 
                                             ImageUrl="~/images/icono_eliminar.gif" onclick="btnEliminar_Click" />
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
    
    <ajax:UpdatePanel ID="updnlImportardor" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <asp:Panel ID="pnlImportarGuia" runat="server" Visible="false">
    
                <div id="divDetalleGuia" class="DivBorder" align="center">
                
                    <div class="DivBoxDetGuiaAutomatica1">
                    
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
                                    <asp:TextBox ID="txtFecIniD" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                        TargetControlID="txtFecIniD" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>                        
                                <td align="right">
                                    Fecha Fin Vigencia:
                                </td>                        
                                <td align="left">
                                    <asp:TextBox ID="txtFecFinD" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                        TargetControlID="txtFecFinD" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Grupo:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cboGrupoD" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>    
                            </tr>
                            
                            <tr>
                                <td align="right">
                                    &nbsp;Nombre Guía:</td>
                                <td>
                                    <asp:TextBox ID="txtNomGuia" runat="server" Width="193px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    &nbsp;Seleccionar el archivo:
                                </td>
                                <td align="left" colspan="3">
                                    <asp:DropDownList ID="fuImporDataMasiva" runat="server" Height="22px" Width="350px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left">
                                   &nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="btnImportar" runat="server" Text="Importar Guia" 
                                        onclick="btnImportar_Click" />
                                </td> 
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnCancImport" runat="server" Text="Salir" 
                                        onclick="btnCancImport_Click" Width="85px" />
                                </td> 
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdIdGuia" runat="server" />
                                </td>                    
                            </tr>
                            
                        </table>                        
                    
                    </div>
                        
                </div>
                
                <br />
                
                <asp:Panel ID="pnlDetImportados" runat="server" Visible="false">
               
                    <div id="divInfRegImportados" class="DivGridview" align="center" style="height:235px;" >                   
                    
                        <asp:GridView ID="grvRegImportados" runat="server" CssClass="Grid"                     
                            onrowcreated="grvRegImportados_RowCreated" 
                            onrowdatabound="grvRegImportados_RowDataBound">              
                              
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAlternateRow" />
                            
                        </asp:GridView>
                    
                    </div>
                    
                    <br />
                    
                    <div id="MsjeNroImportados">
                        <asp:Label ID="lblMsjeImportados" runat="server" Font-Bold="True" 
                            ForeColor="Blue"></asp:Label>
                    </div>
                    
                    <br />
                    
                     <div id="divInfRegNoImportados" class="DivGridview" align="center" style="height:232px;" >
                    
                        <asp:GridView ID="grvRegNoImportados" runat="server" CssClass="Grid" 
                             PageSize="5" onrowcreated="grvRegNoImportados_RowCreated">              
                              
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAlternateRow" />
                            
                        </asp:GridView>
                    
                    </div> 
                    
                    <br />
                    
                    <div id="MsjeNroNoImportados">
                        <asp:Label ID="lblMsjeNoImportados" runat="server" Font-Bold="True" 
                            ForeColor="Red"></asp:Label>
                    </div>
                    
                    <br />
                    
                    <div>
                    
                        <table>
                    
                            <tr>
                                <td>
                                    <asp:Button ID="btnGrabarImportacion" runat="server" 
                                        Text="Grabar" onclick="btnGrabarImportacion_Click" />
                                </td>                   
                                <td>
                                     &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                     <asp:Button ID="btnCancelarImportacion" runat="server" Text="Cancelar" 
                                         onclick="btnCancelarImportacion_Click" />
                                </td>
                                
                            </tr>
                            
                        </table>
                    </div>
                
               </asp:Panel>        
            
            </asp:Panel>    
        
       </ContentTemplate>
       
       <Triggers>
            <ajax:PostBackTrigger ControlID="btnImportar" />
       </Triggers>     
        
   </ajax:UpdatePanel>        
    
   <ajax:UpdatePanel ID="updnlDetalleG" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <asp:Panel ID="pnlDetalleGuia" runat="server" Visible="false">
    
                <div id="div1" class="DivBorder" align="center">
                
                    <div class="DivBoxDetGuiaAutomatica2">
                    
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
                                    <asp:TextBox ID="txtFecIniE" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFecIniE" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>                        
                                <td align="right">
                                    Fecha Fin Vigencia:
                                </td>                        
                                <td align="left">
                                    <asp:TextBox ID="txtFecFinE" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtFecFinE" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    Grupo:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cboGrupoE" runat="server" Height="22px" Width="200px">
                                    </asp:DropDownList>
                                </td>    
                            </tr>
                            <tr>
                                <td align="right">
                                    Nombre Guía:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomGuiaE" runat="server" Width="193px"></asp:TextBox>
                                </td> 
                                <td>
                                    <asp:HiddenField ID="hidIdCategoria" runat="server" />
                                    <asp:HiddenField ID="hidIdPromocion" runat="server" />
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:HiddenField ID="hidIndex" runat="server" />
                                    &nbsp;
                                </td> 
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
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
                
                <div id="divGrillaDetalle" class="DivGridview" align="center" style="height:235px;" >
                
                    <asp:GridView ID="grvDetalleGuias" runat="server" CssClass="Grid" 
                        DataKeyNames="ID_LINEA"
                        onrowcommand="grvDetalleGuias_RowCommand" 
                        onrowcreated="grvDetalleGuias_RowCreated" 
                        onrowdatabound="grvDetalleGuias_RowDataBound" 
                        onselectedindexchanged="grvDetalleGuias_SelectedIndexChanged" >
                    
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
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="ColumnaOculta" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton Id="btnPdf" runat="server" 
                                       CommandName="VerPDF" ImageUrl="~/images/pdf.gif" 
                                       CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" >
                                    </asp:ImageButton>   
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                      </Columns>
                      
                      <HeaderStyle CssClass="GridHeader" />
                      <AlternatingRowStyle CssClass="GridAlternateRow" />
                      
                    </asp:GridView>
                    
                </div> 
                
                <br />
                
                <div class="DivBorder">
                    
                    <table>
                
                        <tr>
                            <td>
                                <asp:Button ID="btnAgregarDet" runat="server" 
                                    Text="Agregar Producto" 
                                    onclick="btnAgregarDet_Click" Visible="False" />
                            </td>
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                 <asp:Button ID="btnCEG" runat="server" Text="Grabar Guia" 
                                     onclick="btnCEG_Click" />
                            </td>
                             <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnCambiarEst" runat="server" 
                                    Text="Pasar a Listo para Impresión" onclick="btnCambiarEst_Click" 
                                    Visible="False" />
                            </td>
                            <td>
                                 &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                 <asp:Button ID="btnCancGDG" runat="server" Text="Salir" 
                                     onclick="btnCancGDG_Click" Width="85px" />
                            </td>
                            
                        </tr>
                    
                    </table>
                
                </div>
            
            </asp:Panel>
            
            <br />
            
            <asp:Panel ID="pnlCatPromo" runat="server" Visible="False">
            
                <div id="divCatPromo" class="DivBorder" align="center">
                
                    <div class="DivBoxDetGuiaAutomatica3">
                    
                        <table>
                
                            <tr>
                                <td colspan="4" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Categoría:
                                </td>
                                <td>
                                     <asp:DropDownList ID="cboCategoriaD" runat="server" Height="22px" Width="200px" 
                                         AutoPostBack="True" onselectedindexchanged="cboCategoriaD_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                  <td>
                                      Promoción:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboPromocionD" runat="server" Height="22px" Width="200px" 
                                        AutoPostBack="True" onselectedindexchanged="cboPromocionD_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                                                       
                        </table>
                    
                    </div>
                
                </div>
                
            </asp:Panel>
            
            <br />
                
            <asp:Panel ID="pnlDetalleGuiaCampo" runat="server" Visible="false">
            
                <div id="divDetGuiaCamp" class="DivBorder" align="center">
                
                    <div class="DivBoxDetGuiaAutomatica4">
                    
                        <table>
                     
                            <tr>
                                <td style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>                               
                            <tr>
                                <td align="center">
                                    
                                    <div id="div2" class="DivGridview" align="center" style="height:160px;" >
                                    
                                        <asp:GridView ID="grvCamposOblig" runat="server" AutoGenerateColumns="False">
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
                                                    SortExpression="ALIAS" >
                                                     <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    <ControlStyle Font-Size="11px" Width="70px" />
                                                    <HeaderStyle Font-Size="12px" Width="70px" BackColor="#FF6600" 
                                                        Font-Bold="True" ForeColor="White" 
                                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>                                     
                                                 <asp:TemplateField HeaderText="Valor">
                                                     <ItemTemplate>
                                                         <asp:TextBox ID="txtValor" runat="server" EnableViewState="true" 
                                                            Text='<%# Eval("VALOR") %>'>
                                                         </asp:TextBox>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                     <ControlStyle Font-Size="11px" Width="350px" />
                                                     <HeaderStyle Font-Size="12px" Width="350px" BackColor="#FF6600" 
                                                            Font-Bold="True" ForeColor="White" 
                                                            HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                </asp:TemplateField>
                                            </Columns>
                                            
                                        </asp:GridView>
                                        
                                    </div>
                                    
                                </td>
                                
                            </tr>
                            
                            <tr>
                                
                                 <td align="center">
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                        onclick="btnGuardar_Click" Text="Grabar Producto" />
                                    &nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="true" 
                                        onclick="btnCancelar_Click" Text="Salir" Width="85px" />
                                </td>
                                
                            </tr>
                            
                         </table>
                     
                    </div>
                     
                 </div>
                 
            </asp:Panel>           
            
            <br />
            
            <asp:Panel ID="pnlDetalleGuiaCampoPDF" runat="server" Visible="false">
            
                <div id="divDetGuiaCampPDF" class="DivBorder" align="center">
                
                    <div class="DivBoxDetGuiaAutomatica5">
                    
                        <table>
                     
                            <tr>
                                <td style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="div3" class="DivGridview" align="center" style="height:140px;">
                                    
                                        <asp:GridView ID="grvCamposObligPDF" runat="server" AutoGenerateColumns="False">
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
                                        
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                     <asp:DropDownList ID="cboCartelesPDF" runat="server" Height="22px" 
                                         Width="240px">
                                     </asp:DropDownList>
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
                                    <asp:Button ID="btnVerPDF" runat="server" CausesValidation="true" 
                                        onclick="btnVerPDF_Click" Text="Ver PDF" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelarPDF" runat="server" CausesValidation="true" 
                                        onclick="btnCancelarPDF_Click" Text="Salir" Width="85px" />
                                </td>
                            </tr>
                            
                        </table>
                     
                    </div>
                     
                 </div>
                 
            </asp:Panel>
    
       </ContentTemplate>      
       
     </ajax:UpdatePanel>      
    
</asp:Content>

