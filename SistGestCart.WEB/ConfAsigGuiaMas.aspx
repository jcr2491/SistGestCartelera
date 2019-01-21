<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ConfAsigGuiaMas.aspx.cs" Inherits="ConfAsigGuiaMas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/Pagina.css" rel="stylesheet" type="text/css" />
    <link href="css/GridView.css" rel="stylesheet" type="text/css" />
    
    <script src="js/General.js" type="text/javascript"></script>
    
    <style type="text/css">
        .ColumnaOculta {display:none;}     
        .style1
        {
            height: 34px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
        
    <div align="center" style="background-color:  #FF6600; color: #FFFFFF; font-weight: bold; font-size: medium;">			    
        Carga de Guias Masivas (Guias Automáticas)
    </div>
    
    <br />
    
    <ajax:UpdatePanel ID="upnlBusqueda" runat="server">
    
        <ContentTemplate>
            
            <div class="DivBorder">
    
                <table>
                
                    <tr>
                        <td>
                            Nombre Archivo :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNomFile" runat="server"></asp:TextBox>
                           
                        </td>    
                         <td>
                             &nbsp;&nbsp;&nbsp;
                             <asp:HiddenField ID="hidId" runat="server" />
                        </td>
                         <td>
                             <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                 onclick="btnBuscar_Click" />
                        </td> 
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td> 
                        <td>
                             <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="images/excel.gif" 
                                        onclick="imgExcel_Click" ToolTip="Generar Plantilla Carga Masiva" 
                                        Width="16px" />
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
                       <td style ="width:2.8%;text-align:center"></td>                       
                       <td style ="width:20%;text-align:center">Archivo</td>
                       <td style ="width:18.9%;text-align:center">Ult.Mod.</td>
                       <td style ="width:19%;text-align:center">Modificado</td>
                       <td style ="width:19%;text-align:center">Estado</td>
                    </tr>
                    
                </table>
                
            </div>
            
            <div align="center" class="DivGridview" style="height:150px;">
                
                <asp:GridView ID="grvFilesGuia" runat="server" AutoGenerateColumns="False" 
                    CssClass="Grid" DataKeyNames="ID_FILE" ShowHeader="false" 
                    onrowdatabound="grvFilesGuia_RowDataBound">

                    <RowStyle CssClass="GridRow" />
        
                    <Columns>
            
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" 
                                                     runat="server" 
                                                     ImageUrl="~/images/edit 16x16.png"
                                        onclick="btnEditar_Click" />                          
                                    </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" CssClass="ColumnaOculta" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton   ID="btnEliminar" 
                                                        runat="server" 
                                                        ImageUrl="~/images/icono_eliminar.gif" 
                                                        CausesValidation="false" onclick="btnEliminar_Click"  />
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ID_FILE" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField>                       
                        
                        <asp:BoundField DataField="NOM_FILE" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="FECHA_MOD" HeaderText="" ReadOnly="true" DataFormatString="{0:d}" HtmlEncode="False" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="USER_MOD" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ESTADO" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                       
                    </Columns>
            
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
            
            <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
            
                <div id="Detalle" class="DivBorder" align="center">
                
                    <div class="DivBoxDetCargaGuia">
                        
                        <table>
                    
                            <tr>
                                <td colspan="3" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                             <tr>
                                <td colspan="3">                    
                                     <asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                            </tr>  
                                          
                            <tr>
                                <td colspan="3">  
                                    &nbsp;                                        
                                </td>
                            </tr> 
                            
                            <tr id = "NomFile" runat="server">
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Archivo:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblFile" runat="server" Text=""></asp:Label>
                                </td>
                            </tr> 
                            
                            
                            
                             <tr id = "Estado" runat="server">
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Estado:
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkEstado" runat="server" />
                                </td>
                            </tr>                                     
                            
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Selecc.Archivo:
                                </td>
                                <td>
                                   <asp:FileUpload ID="fuLoadFileCM" runat="server" Height="22px" 
                                        Width="414px" size="53"/>
                                </td>
                            </tr>
                            
                              <tr>
                                <td colspan="3"> 
                                    &nbsp;
                                </td>
                            </tr>      
                                       
                            <tr>
                                 <td align="left">
                                   
                                 </td>
                                 <td colspan="2" align="center"> 
                                    &nbsp;
                                    <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                        onclick="btnGuardar_Click" Text="Cargar" Width="79px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="true" 
                                        Text="Cancelar" Width="79px" onclick="btnCancelar_Click" />
                                 </td>
                            </tr>
                        
                        </table>
                    
                    </div>                    
                
                </div>
                
            </asp:Panel> 
    
       </ContentTemplate>
       
       <Triggers>
            <ajax:PostBackTrigger ControlID="btnGuardar" />
            <ajax:PostBackTrigger ControlID="imgExcel" />
       </Triggers>   
    
    </ajax:UpdatePanel>    
        
</asp:Content>

