<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="GenerarAutoliquidacion.aspx.cs" Inherits="contratacion_GenerarAutoliquidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--FONDO DE LA VENTANA DE PROCESAMIENTO--%>
    <asp:UpdateProgress ID="UpdateProgress_Procesamiento" runat="server" style="display: none">
        <ProgressTemplate>
            <%--CONTENEDOR DE LA VENTANA PROCESAMIENTO--%>
            <asp:Panel ID="Panel_ContenedorProcesamiento" runat="server">
                <%--FONDO DE LA VENTANA DE PROCESAMIENTO--%>
                <asp:Panel ID="Panel_FondoProcesamiento" runat="server" CssClass="conf_panel_fondo_ventana_emergente">
                </asp:Panel>
                <%--VENTANA EMERGENTE CON MENSAJE--%>
                <asp:Panel ID="Panel_VentanaProcesamiento" runat="server" CssClass="conf_panel_ventana_emergente">
                    <div style="border: 2px solid #006600; height: 210px; margin: 2px;">
                        <div style="border: 1px solid #006600; height: 204px; margin: 2px;">
                            <div style="height: 75px;">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_label_ventana_procesando" style="text-align: center; color: #BBBBBB;
                                        font-weight: bold; font-size: 130%;">
                                        <asp:Label ID="Label_Procesamiento" runat="server" CssClass="label_ventana_procesando">Procesando...</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image_Procesamiento" runat="server" CssClass="img_ventana_emergente"
                                            Height="19px" ImageUrl="~/imagenes/loading11.gif" Width="220px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel_Procesamiento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_LIQUIDAR" runat="server" Text="Liquidar" CssClass="margin_botones"
                                                OnClick="Button_LIQUIDAR_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_inconsistencias" runat="server" Text="Inconsistencias" CssClass="margin_botones"
                                                OnClick="Button_inconsistencias_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_novedades" runat="server" Text="Novedades" CssClass="margin_botones"
                                                OnClick="Button_novedades_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_reliquidaciones" runat="server" Text="Re LPS" CssClass="margin_botones"
                                                OnClick="Button_reliquidaciones_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_nomina" runat="server" Text="Nomina" CssClass="margin_botones"
                                                OnClick="Button_nomina_Click" Width="140px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_vacaciones" runat="server" Text="Vacaciones" CssClass="margin_botones"
                                                OnClick="Button_vacaciones_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_liquidacion" runat="server" Text="Liquidacion" CssClass="margin_botones"
                                                OnClick="Button_liquidacion_Click" Width="140px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_retiros_lps" runat="server" Text="Retiros(LPS Mes A)" CssClass="margin_botones"
                                                Width="140px" OnClick="Button_retiros_lps_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_retiros_nomina_meses_anteriores" runat="server" Text="Retiros(Nom Mes A)"
                                                CssClass="margin_botones" Width="140px" OnClick="Button_retiros_nomina_meses_anteriores_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_plano" runat="server" Text="Plano" CssClass="margin_botones"
                                                OnClick="Button_plano_Click" Width="140px" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_diferencias_seguridad_social" runat="server" 
                                                Text="Diferencias Seg Social" CssClass="margin_botones" Width="140px" 
                                                onclick="Button_diferencias_seguridad_social_Click" />
                                        </td>
                                        <td>
                                            <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" type="button"
                                                value="Salir" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePane_MENSAJE" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
                    </asp:Panel>
                    <asp:Panel ID="Panel_MENSAJES" runat="server">
                        <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                            display: block;" />
                        <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                            <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el 
                    mensaje, tiene un&nbsp; texto de acuerdo 
                    a la acción correspondiente.</asp:Label>
                        </asp:Panel>
                        <div style="text-align: center; margin-top: 15px;">
                            <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                                Style="height: 26px" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="div_espaciador">
            </div>
            <asp:Panel ID="Panel_DATOS_RETIROS" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información período de Autoliquidación
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Año
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_años" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_izq">
                                        Mes
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_meses" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_der">
                                        <asp:Button ID="Button_identificar" runat="server" Text="Identificar" OnClick="Button_identificar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div_espaciador">
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_para_convencion_hoja_trabajo">
                    </div>
                    <div class="div_cabeza_groupbox">
                        Estado de Autoliquidación</div>
                    <div class="div_contenido_groupbox">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_marcar" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_marcar_CheckedChanged"
                                        Text="Marcar" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_pagar" runat="server" Text="Pagado" OnClick="Button_pagar_Click" />
                                </td>
                            </tr>
                        </table>
                        <div>
                            <div class="div_contenido_groupbox">
                                <div>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_empresas" runat="server" Width="885px" AutoGenerateColumns="False"
                                                DataKeyNames="id_empresa">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Liquidar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox_liquidar" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pagar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox_pagar" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id_empresa" HeaderText="Id" />
                                                    <asp:BoundField DataField="codigo_empresa" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" />
                                                    <asp:BoundField DataField="periodo" HeaderText="Periodo" />
                                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                </div>
                            </div>
                        </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_LIQUIDAR" />
            <asp:PostBackTrigger ControlID="Button_inconsistencias" />
            <asp:PostBackTrigger ControlID="Button_novedades" />
            <asp:PostBackTrigger ControlID="Button_reliquidaciones" />
            <asp:PostBackTrigger ControlID="Button_nomina" />
            <asp:PostBackTrigger ControlID="Button_vacaciones" />
            <asp:PostBackTrigger ControlID="Button_liquidacion" />
            <asp:PostBackTrigger ControlID="Button_retiros_lps" />
            <asp:PostBackTrigger ControlID="Button_retiros_nomina_meses_anteriores" />
            <asp:PostBackTrigger ControlID="Button_plano" />
            <asp:PostBackTrigger ControlID="Button_diferencias_seguridad_social" />
            <asp:PostBackTrigger ControlID="CheckBox_marcar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
