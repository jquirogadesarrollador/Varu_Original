<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="VisitasFidelizacion.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    
    <asp:Panel ID="Panel_ObjetivosArea" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_objetivos_area">
                CONSECUCIÓN DE NEGOCIOS RENTABLES QUE CONTRIBUYAN AL CUMPLIMIENTO DEL PRESUPUESTO ENMARCADO EN LA NORMATIVIDAD LEGAL
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
        text-align: center;">
        <div class="div_botones_internos">
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: center;">
                        <asp:Table ID="Table_MENU" runat="server" BorderStyle="None" CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:Table ID="Table_MENU_1" runat="server" BorderStyle="None" CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                                <div class="div_cabeza_groupbox">
                                    Sección de busqueda
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox_buscar" runat="server" ValidationGroup="buscar" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                    ValidationGroup="buscar" CssClass="margin_botones" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <!--TextBox_BUSCAR-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_buscar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar el período contable. Ejm 1201"
                                    ValidationGroup="buscar" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_buscar"
                                    TargetControlID="RequiredFieldValidator_TextBox_buscar" HighlightCssClass="validatorCalloutHighlight" />
                                
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_buscar"
                                    runat="server" TargetControlID="TextBox_buscar" FilterType="Numbers" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td>
                            <asp:Label ID="Label_ALERTA_VERDE" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_verde">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_AMARILLA" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ROJA" runat="server" Text="0"></asp:Label>
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_rojo">
                            </div>
                        </td>
                    </tr>
                </table>
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
                                <asp:BoundField DataField="Trabajadores" HeaderText="Trabajadores"/>
                                <asp:BoundField DataField="Comercial" HeaderText="Comercial"/>
                                <asp:BoundField DataField="Ventas_Mes_Anterior" HeaderText="Ventas mes anterior" 
                                    DataFormatString="$ {0:C}" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ventas_Mes_Actual" HeaderText="Ventas mes actual" 
                                    DataFormatString="$ {0:C}" >
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
