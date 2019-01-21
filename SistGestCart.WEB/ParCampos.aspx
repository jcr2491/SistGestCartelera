<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ParCampos.aspx.cs" Inherits="ParCampos" Title="Página sin título" %>

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
	    Mantenimiento de Campos de Cartel
    </div>
      
    <br />
    
    <ajax:UpdatePanel ID="upnlCampos" runat="server">

        <ContentTemplate>
        
            <div style =" background-color:#FF6600;  height:15px;width:100%; margin:0;padding:0">
           
                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                 style="font-family:Arial;font-size:12px;width:100%;color:white;
                 border-collapse:collapse;height:100%;">
                    <tr>
                       <td style ="width:14px;text-align:center"></td>
                       <td style ="width:212px;text-align:center">Alias</td>
                       <td style ="width:204px;text-align:center">Campo</td>
                       <td style ="width:204px;text-align:center">Tipo</td>
                       <td style ="width:195px;text-align:center">Restringir</td>
                       <td style ="width:195px;text-align:center">Validar Dig.</td>
                    </tr>
                </table>
                
            </div>
        
            <div id="divGrillaDetalle" class="DivGridview" align="center" style="height:250px;">
            
                <asp:GridView 
                    ID="grvCampos" runat="server" AutoGenerateColumns="False" ShowHeader="false" 
                    CssClass="Grid" DataKeyNames="ID_CAMPO">
                
                    <RowStyle CssClass="GridRow" />
                
                    <Columns>   
                                         
                        <asp:TemplateField>
                             <ItemTemplate>
                                     <asp:ImageButton 
                                        ID="btnEliminar" 
                                        runat="server" 
                                        ImageUrl="~/images/icono_eliminar.gif" 
                                        onclick="btnEliminar_Click" 
                                        OnClientClick="javascript:return confirmEliminacion()" />
                             </ItemTemplate>
                            <ControlStyle Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="ID_CAMPO" HeaderText="Id"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ColumnaOculta" />
                            <ControlStyle Font-Size="11px" Width="50px" />
                            <HeaderStyle Width="50px" CssClass="ColumnaOculta" />
                        </asp:BoundField> 
                            
                        <asp:BoundField DataField="ALIAS" HeaderText="Alias"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>  
                        
                        <asp:BoundField DataField="NOM_CAMPO" HeaderText="Campo"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="TIPO" HeaderText="Campo"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="RESTINGIR" HeaderText="Restringir"  ReadOnly="true" >
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                            <ControlStyle Font-Size="11px" Width="400px" />
                            <HeaderStyle Width="400px"/>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="VALDIGITOS" HeaderText="Validar Dig."  ReadOnly="true" >
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
                <input id="btnAgregar" type="button" value="Agregar" onclick="javascript:return Agregar()" />
            </div>  
            
            <br /> 
            
            <div id="divDetalle" class="DivBorder" align="center" style="display:none;">
            
                <div id="pnlDetalle" class="DivBoxDetCampos" style="display:none;">                    

                    <table>
                        
                        <tr>
                            <td colspan="2" style="background-color: #FF6600">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr> 
                         <tr>
                            <td colspan="2">                    
                                 <asp:HiddenField ID="hidId" runat="server" />
                            </td>
                        </tr>                
                        <tr>
                            <td colspan="2">  
                                &nbsp;                                        
                            </td>
                        </tr>            
                        <tr>
                            <td>
                                Alias:
                            </td>
                            <td>
                                <asp:TextBox MaxLength="150" Width="350px" runat="server" ID="txtAlias"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Campo:
                            </td>
                            <td>
                                <asp:TextBox MaxLength="150" Width="350px" runat="server" ID="txtCampo"></asp:TextBox>
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                Tipo:
                            </td>
                            <td align="left">
                                 <asp:DropDownList ID="cboTipoCampo" runat="server" Height="22px" Width="200px">
                                 </asp:DropDownList>
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                Restringir:
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkRestringir" runat="server" />
                            </td>
                        </tr>                    
                        <tr>
                            <td>
                                Val. Digito:
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkValDig" runat="server" />
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>           
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnGuardar" runat="server" CausesValidation="true" 
                                    onclick="btnGuardar_Click" Text="Guardar" OnClientClick="javascript:return IsValid()" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input id="btnCancelar" type="button" value="Cancelar" onclick="javascript:return Cancelar()" />
                            </td>
                        </tr>
                    
                    </table>
                
                </div>                    
            
            </div>            
    
        </ContentTemplate>        
    
    </ajax:UpdatePanel> 
    
    <script language="javascript" type="text/javascript">
    
        function Agregar()
        {
            var pnlDetalle = $get("pnlDetalle");
            pnlDetalle.style.display = '';
            
            var divDetalle = $get("divDetalle");            
            divDetalle.style.display = '';
            
            $get('<%= txtAlias.ClientID %>').value = "";
            $get('<%= txtCampo.ClientID %>').value = "";
            $get('<%= hidId.ClientID %>').value = "";
            $get('<%= chkRestringir.ClientID %>').checked = false;
            $get('<%= chkValDig.ClientID %>').checked = false;
            $get('<%= cboTipoCampo.ClientID %>').selectedIndex = 0;
            $get('<%= txtAlias.ClientID %>').focus();
            
            return false;
        }

        function Cancelar() 
        {
            var pnlDetalle = $get("pnlDetalle");
            pnlDetalle.style.display = 'none';
            
            var divDetalle = $get("divDetalle");
            divDetalle.style.display = 'none';
                      
            return false;
        }

        function IsValid() 
        {
            var msg = '<%= Resources.Mensajes.msgConfirmación %>';
            var txtAlias = $get('<%= txtAlias.ClientID %>');
            var txtCampo = $get('<%= txtCampo.ClientID %>');
            var cboTipoCampo = $get('<%= cboTipoCampo.ClientID %>');

            if (txtAlias.value == '') {
                alert('<%= Resources.Mensajes.msgCampoAliasNoIngresado %>');
                txtAlias.focus();
                return false;
            }
            if (txtAlias.length > 20) {
                alert('<%= Resources.Mensajes.msgCampoAliasLongitudNoPermt %>');
                txtAlias.focus();
                return false;
            }
            if (txtCampo.value == '') {
                alert('<%= Resources.Mensajes.msgCCampoNoIngresado %>');
                txtCampo.focus();
                return false;
            }
            if (txtCampo.length > 100) {
                alert('<%= Resources.Mensajes.msgCampoNombreLongitudNoPermt %>');
                txtCampo.focus();
                return false;
            }
            if (cboTipoCampo.selectedIndex == 0) {
                alert('<%= Resources.Mensajes.msgCCampoNoIngresado %>');
                cboTipoCampo.focus();
                return false;
            }

            return confirm(msg);
        }

        function confirmacion(msg) {
            return confirm(msg);
        }

        function confirmEliminacion() 
        {
            var msg = '<%= Resources.Mensajes.msgConfirmEliminacion %>';            
            return confirm(msg);
        }
        
    </script>
    
</asp:Content>

