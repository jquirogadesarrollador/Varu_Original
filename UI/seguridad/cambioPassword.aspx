<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="cambioPassword.aspx.cs" Inherits="seguridad_cambioPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <%--PANEL CONTENEDOR DE LA VENTANA PROCESAMIENTO--%>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_Cambiar" runat="server" Text="Cambiar" 
                                                CssClass="margin_botones" ValidationGroup="CAMBIAR" 
                                                onclick="Button_Cambiar_Click" />
                                        </td>
                                        <td colspan="0">
                                            <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" class="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_Formulario" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos de Usuario
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Nombre de Usuario:        
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_LOG" runat="server" ValidationGroup="CAMBIAR" 
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    E-Mail:        
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_MAIL" runat="server" ValidationGroup="CAMBIAR" 
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Contraseña Anterior:        
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_PSW_ANT" runat="server" ValidationGroup="CAMBIAR" 
                                        TextMode="Password" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Contraseña Nueva:        
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_PSW_NEW" runat="server" ValidationGroup="CAMBIAR" 
                                        TextMode="Password" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Confirmar Contraseña Nueva:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_PSW_NEW_CONF" runat="server" 
                                        ValidationGroup="CAMBIAR" TextMode="Password" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <!-- TextBox_USU_LOG -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_LOG"
                            ControlToValidate="TextBox_USU_LOG" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DE USUARIO es requerido."
                            ValidationGroup="CAMBIAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_LOG"
                            TargetControlID="RequiredFieldValidator_TextBox_USU_LOG" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- TextBox_USU_MAIL -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_MAIL"
                            ControlToValidate="TextBox_USU_MAIL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El E - MAIL es requerido."
                            ValidationGroup="CAMBIAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_MAIL"
                            TargetControlID="RequiredFieldValidator_TextBox_USU_MAIL" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- TextBox_USU_PSW_ANT -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_PSW_ANT"
                            ControlToValidate="TextBox_USU_PSW_ANT" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CONTRASEÑA ANTERIOR es requerida."
                            ValidationGroup="CAMBIAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_ANT"
                            TargetControlID="RequiredFieldValidator_TextBox_USU_PSW_ANT" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- TextBox_USU_PSW_NEW -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_PSW_NEW"
                            ControlToValidate="TextBox_USU_PSW_NEW" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PASSWORD NUEVO es requerido."
                            ValidationGroup="CAMBIAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NEW"
                            TargetControlID="RequiredFieldValidator_TextBox_USU_PSW_NEW" HighlightCssClass="validatorCalloutHighlight" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_USU_PSW_NEW" Display="None"
                            ControlToValidate="TextBox_USU_PSW_NEW" ValidationExpression="^.*(?=.{10,})(?=.*\d)(?=.*[a-z|A-Z])(?=.*[@#$%^&amp;+=!/()?¡¿*+;:,.-_]).*$"
                            runat="server" 
                            ErrorMessage="<b>Campo Requerido faltante</b><br />El PASSWORD NUEVO no cumple con los requisitos mínimos.<br>Mínimo 10 caracteres.<br>Un número.<br>Un Caracter especial." 
                            ValidationGroup="CAMBIAR"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NEW_1"
                            TargetControlID="RegularExpressionValidator_TextBox_USU_PSW_NEW" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- TextBox_USU_PSW_NEW_CONF -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_PSW_NEW_CONF"
                            ControlToValidate="TextBox_USU_PSW_NEW_CONF" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CONFIRMACIÓN DE NUEVO PASSWORD es requerida."
                            ValidationGroup="CAMBIAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NEW_CONF"
                            TargetControlID="RequiredFieldValidator_TextBox_USU_PSW_NEW_CONF" HighlightCssClass="validatorCalloutHighlight" />
                        <asp:CompareValidator ID="CompareValidator_TextBox_USU_PSW_NEW_CONF" 
                            runat="server" Display="None" ControlToCompare="TextBox_USU_PSW_NEW" ControlToValidate="TextBox_USU_PSW_NEW_CONF" Operator="Equal"
                            ErrorMessage="<b>Campo con formato incorrecto</b><br />La CONFIRMACIÓN DE NUEVO PASSWORD debe ser igual al Password nuevo." ValidationGroup="CAMBIAR" ></asp:CompareValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NEW_CONF_1"
                            TargetControlID="CompareValidator_TextBox_USU_PSW_NEW_CONF" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BOTONES_PIE" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_CAMBIAR_1" runat="server" Text="Cambiar" 
                                                CssClass="margin_botones" ValidationGroup="CAMBIAR" 
                                                onclick="Button_Cambiar_Click" />
                                        </td>
                                        <td colspan="0">
                                            <input id="Button_SALIR_1" type="button" value="Salir" onclick="window.close();" class="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

