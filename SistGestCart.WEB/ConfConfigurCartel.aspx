<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ConfConfigurCartel.aspx.cs" Inherits="ConfConfigurCartel" Title="Página sin título" %>

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
        Configuración Cartel
    </div>
    
    <br />    
    
    <ajax:UpdatePanel ID="upnlBusqueda" runat="server" UpdateMode="Conditional">
    
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
                             <asp:HiddenField ID="hidCartelModelo" runat="server" />
                             <asp:HiddenField ID="hidNroDigitos" runat="server" />
                             <asp:HiddenField ID="hidNom" runat="server" />
                             <asp:HiddenField ID="hidIdCartel" runat="server" />
                             <asp:HiddenField ID="hidIdModelo" runat="server" />
                             <asp:HiddenField ID="hidDigitos" runat="server" />
                        </td>
                        <td>
                             <asp:Button ID="btnBuscarDCM" runat="server" Text="Buscar" 
                                 onclick="btnBuscarDCM_Click" />
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
                       <td style ="width:2%;text-align:center"></td>                       
                       <td style ="width:32%;text-align:center">Cartel-Modelo</td>
                       <td style ="width:32%;text-align:center">Digitos</td>
                       <td style ="width:34%;text-align:center">Plantilla</td>
                    </tr>
                </table>
                
            </div>
            
            <div align="center" class="DivGridview" style="height:150px;">
                
                <asp:GridView ID="grvCartelModelo" runat="server" AutoGenerateColumns="False" 
                    CssClass="Grid" DataKeyNames="ID_CARTEL,ID_MODELO,DIGITOS" ShowHeader="false">

                    <RowStyle CssClass="GridRow" />
        
                    <Columns>
            
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:ImageButton ID="btnBuscar" 
                                                     runat="server" 
                                                     ImageUrl="~/images/icono_lupa.gif" 
                                                     onclick="btnBuscar_Click" />                          
                                    </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ID_CARTEL" HeaderText="Codigo"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="ID_MODELO" HeaderText="Codigo"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="DIGITOS" HeaderText="Codigo"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        
                         <asp:BoundField DataField="DESCRIPCION" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="NRODIGITOS" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="NOM_PLANTILLA" HeaderText=""  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>                    
                       
                    </Columns>
            
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAlternateRow" />
                    
                </asp:GridView>
            
            </div>
    
       </ContentTemplate>
    
    </ajax:UpdatePanel>
    
    <br />
    
    <ajax:UpdatePanel ID="upnlConfigurar" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
        
            <asp:Panel ID="pnlBtnConfigurar" runat="server" Visible="false">

                <div id="DivBtnConfigurar" class="DivBorder" align="left">
                    <asp:Button ID="btnConfigurarNP" runat="server" Text="Configurar Plantilla" 
                        onclick="btnConfigurarNP_Click" />
                </div>       
                
            </asp:Panel>
    
      </ContentTemplate>
        
    </ajax:UpdatePanel>
    
    <br />
    
    <ajax:UpdatePanel ID="upnlImportador" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
            
            <asp:Panel ID="pnlDialgPlantilla" runat="server" Visible="false">
        
                <div id="Dialog" class="DivBorder" align="center">
                
                    <div class="DivBoxDetConfCartel1">
                    
                        <table>
                    
                            <tr>
                                <td colspan="4" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr> 
                            <tr>
                                <td colspan="4">                    
                                     &nbsp;
                                </td>
                            </tr>                
                            <tr>
                                <td colspan="4" align="center">                    
                                    <asp:Label ID="lblCartelPrint" runat="server" Text=""></asp:Label>
                                </td>
                            </tr> 
                            <tr>
                                <td colspan="4" align="center">                    
                                    &nbsp;
                                </td>
                            </tr>            
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;Importar Plantilla:&nbsp;
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuImportPlantillas" runat="server" Height="22px" 
                                        Width="287px" size="30" />
                                </td>
                                <td>
                                    &nbsp;
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
                                <td align="left">
                                    <asp:Button ID="btnImportar" runat="server" 
                                        onclick="btnImportar_Click" Text="Importar" Width="85px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancelar"   
                                        Text="Salir" onclick="btnCancelar_Click" Width="85px" />                                 
                                </td>
                                <td> 
                                     &nbsp;
                                </td>
                                
                            </tr>
                            
                        </table>
                    
                    </div>                    
                    
                </div>
                
            </asp:Panel>
    
        </ContentTemplate>
        
        <Triggers>
            <ajax:PostBackTrigger ControlID="btnImportar" />
        </Triggers>
        
   </ajax:UpdatePanel>    
    
   <ajax:UpdatePanel ID="upnlDetalleConf" runat="server" UpdateMode="Conditional">
    
        <ContentTemplate>
            
            <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
        
                <div id="DivDetalle" class="DivBorder" align="center">
                
                    <div class="DivBoxDetConfCartel2">
                    
                        <table>
                    
                            <tr>
                                <td colspan="3" style="background-color: #FF6600">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
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
                                    Cartel:&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCartel" runat="server"></asp:Label>
                                    &nbsp;
                                </td>                    
                            </tr>                           
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    Digitos:&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDigitos" runat="server"></asp:Label>
                                    &nbsp; &nbsp;
                                </td>                    
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    Nombre Plantilla:&nbsp;
                                </td>
                                <td align="left">                        
                                    <asp:Label ID="lblPlantilla" runat="server"></asp:Label>
                                    &nbsp; &nbsp;
                                </td>                    
                             </tr>
                             <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    Campos:&nbsp;
                                </td>
                                <td>
                                    <asp:GridView ID="grvCampos" runat="server" AutoGenerateColumns="False" 
                                        CssClass="Grid" Height="126px" Width="253px" >
                                        <RowStyle CssClass="GridRow" />
                                        <Columns>
                                            <asp:BoundField DataField="ID_CAMPO" HeaderText="IdCampo" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" Width="70px" CssClass="ColumnaOculta" />
                                                <ControlStyle Font-Size="11px" Width="70px" />
                                                <HeaderStyle BackColor="#FF6600" Font-Bold="True" Font-Overline="False" 
                                                    Font-Size="12px" ForeColor="White" HorizontalAlign="Center" 
                                                    VerticalAlign="Middle" Width="70px" CssClass="ColumnaOculta" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="POSX" HeaderText="X" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" Width="70px" CssClass="ColumnaOculta" />
                                                <ControlStyle Font-Size="11px" Width="70px" />
                                                <HeaderStyle BackColor="#FF6600" Font-Bold="True" Font-Overline="False" 
                                                    Font-Size="12px" ForeColor="White" HorizontalAlign="Center" 
                                                    VerticalAlign="Middle" Width="70px" CssClass="ColumnaOculta" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="POSY" HeaderText="Y" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" Width="70px" CssClass="ColumnaOculta" />
                                                <ControlStyle Font-Size="11px" Width="70px" />
                                                <HeaderStyle BackColor="#FF6600" Font-Bold="True" Font-Overline="False" 
                                                    Font-Size="12px" ForeColor="White" HorizontalAlign="Center" 
                                                    VerticalAlign="Middle" Width="70px" CssClass="ColumnaOculta" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CAMPO" HeaderText="Campo" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" Width="70px" />
                                                <ControlStyle Font-Size="11px" Width="70px" />
                                                <HeaderStyle BackColor="#FF6600" Font-Bold="True" Font-Overline="False" 
                                                    Font-Size="12px" ForeColor="White" HorizontalAlign="Center" 
                                                    VerticalAlign="Middle" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" Width="90px" />
                                                <ControlStyle Font-Size="11px" Width="90px" />
                                                <HeaderStyle BackColor="#FF6600" Font-Bold="True" Font-Overline="False" 
                                                    Font-Size="12px" ForeColor="White" HorizontalAlign="Center" 
                                                    VerticalAlign="Middle" Width="90px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="GridAlternateRow" />
                                    </asp:GridView>
                                    &nbsp;
                                </td>                    
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:HiddenField ID="hidPosFCX" runat="server" />
                                    &nbsp;
                                    <asp:HiddenField ID="hidPosFCY" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnConfPlantilla" runat="server" 
                                        onclick="btnConfPlantilla_Click" Text="Configurar Plantilla" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnVerPlantilla" runat="server"
                                        onclick="btnVerPlantilla_Click" Text="Ver Plantilla (PDF)" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelarConf" runat="server" 
                                        Text="Salir" onclick="btnCancelarConf_Click" Width="85px" />
                                </td>
                            </tr>
                            
                        </table>
                    
                    </div>
                    
                </div>
                
            </asp:Panel>
    
        </ContentTemplate>
    
    </ajax:UpdatePanel>
     
</asp:Content>