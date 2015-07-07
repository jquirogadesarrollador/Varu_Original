<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="hojaTrabajoComercial.aspx.cs" Inherits="_Default" %>

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
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td>
                            <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="Falta más de un mes para Vencimiento"></asp:Label>
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_verde">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="Falta un mes o menos para Vencimiento"></asp:Label>
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="Vencido"></asp:Label>
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
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa">
                                <ItemStyle CssClass="column_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Inicia" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FCH_VENCE" HeaderText="Fecha Vencimiento" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FIRMADO_TEXTO" HeaderText="Firmado">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ENVIO_TEXTO" HeaderText="Enviado">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OBJ_CONTRATO" 
                                    HeaderText="Contrato Comercial / Servicio Respectivo">
                                <ItemStyle CssClass="columna_grid_jus" />
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
