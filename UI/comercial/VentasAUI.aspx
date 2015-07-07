<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="VentasAUI.aspx.cs" Inherits="comercial_VentasAUI" %>

<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador"></div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_EXPORTAR" runat="server" Text="Exportar" 
                                        CssClass="margin_botones" onclick="Button_EXPORTAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            Digite el período a consultar (AAMM)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_buscar" runat="server" Width="220px" ValidationGroup="Buscar" CssClass="margin_botones"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="Buscar" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_buscar"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="Buscar" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers" runat="server" TargetControlID="TextBox_buscar" FilterType="Numbers" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                
            </div>
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Hoja de trabajo"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" Width="885px" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="ID_EMPRESA">
                            <Columns>
                                <asp:BoundField DataField="Nit" HeaderText="Nit"/>
                                <asp:BoundField DataField="Empresa" HeaderText="Empresa"/>
                                <asp:BoundField DataField="Facturacion_AIU" HeaderText="AIU" 
                                    DataFormatString="{0:N0}" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Porcentaje_aiu" HeaderText="% AIU">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>

