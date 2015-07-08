<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="seguridad_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grupo varu</title>
    <link href="../Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ajaxScriptManager" EnablePartialRendering="true"
        CombineScripts="false" />
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
    <asp:UpdatePanel ID="UpdatePanel_Procesamiento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

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

            <div style="height:10px;"></div>

            <div id="div_contenedor_login">
                <table border="0" cellpadding="0" cellspacing="0" width="338">
                    <tr>
                        <td>
                            <img name="plantilla_login_blue_r1_c1" src="../imagenes/plantilla/plantilla_login_blue_r1_c1.png"
                                width="338" height="17" border="0" id="plantilla_login_blue_r1_c1" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="div_fondo_medio_login">
                                <table border="0" cellpadding="0" cellspacing="0" width="338">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_repeat_superior">
                                            <%--POR EL MOMENTO ESTE CAMPO NO SE UTILIZA ESTA BACIO Y COMPRIMIDO--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_contenedor_datos_login">
                                            <div id="div_contenedor_inicio_sesion">
                                                <asp:Panel ID="Panel_PANEL_CONTENEDOR_INICIO_SESION" runat="server">
                                                    <table class="table_control_registros" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    <asp:Label ID="Label_Usuario" runat="server" Text="Usuario"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_NombreUsuario" runat="server" Width="150px" ValidationGroup="INGRESAR"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    <asp:Label ID="Label_Pasword" runat="server" Text="Pasword"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_Pasword" runat="server" Width="150px" ValidationGroup="INGRESAR"
                                                                        TextMode="Password">12346</asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_link_recordar_password" colspan="2">
                                                                <asp:Button ID="Button_ACEPTAR" runat="server" Text="Ingresar" OnClick="Button_ACEPTAR_Click"
                                                                    CssClass="margin_botones" ValidationGroup="INGRESAR" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkButton_RecordarContraseña" runat="server" OnClick="LinkButton_RecordarContraseña_Click">Recordar ó Desbloquear Cuenta</asp:LinkButton>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel_CONTENEDOR_RECUPERAR_PASSWORD" runat="server">
                                                    <table class="table_control_registros" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    <asp:Label ID="Label_RecordarPSW_Usuario" runat="server" Text="Usuario"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_RecordarPSW_Usuario" runat="server" Width="150px" ValidationGroup="RECUPERAR"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    <asp:Label ID="Label_Cedula" runat="server" Text="Cedula"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_Cedula" runat="server" Width="150px" ValidationGroup="RECUPERAR"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    <asp:Label ID="Label_RecordarPSW_Empresa" runat="server" Text="Empresa"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:DropDownList ID="DropDownList_RecordarPSW_Empresa" runat="server" ValidationGroup="RECUPERAR">
                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                        <asp:ListItem Text="Sertempo" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Eficiencia &amp; Servicios" Value="3" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_link_recordar_password" colspan="2">
                                                                <div>
                                                                    <asp:Button ID="Button_Ingresar" runat="server" Text="Enviar Información" OnClick="Button_Ingresar_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- TextBox_NombreUsuario -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_RecordarPSW_Usuario"
                                                        ControlToValidate="TextBox_RecordarPSW_Usuario" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DE USUARIO es requerido."
                                                        ValidationGroup="RECUPERAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_RecordarPSW_Usuario"
                                                        TargetControlID="RequiredFieldValidator_TextBox_RecordarPSW_Usuario" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- TextBox_Cedula -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Cedula"
                                                        ControlToValidate="TextBox_Cedula" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NÚMERO DE CEDULA es requerido."
                                                        ValidationGroup="RECUPERAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Cedula"
                                                        TargetControlID="RequiredFieldValidator_TextBox_Cedula" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- DropDownList_RecordarPSW_Empresa -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_RecordarPSW_Empresa"
                                                        ControlToValidate="DropDownList_RecordarPSW_Empresa" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA es requerida."
                                                        ValidationGroup="RECUPERAR" InitialValue="0" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_RecordarPSW_Empresa"
                                                        TargetControlID="RequiredFieldValidator_DropDownList_RecordarPSW_Empresa" HighlightCssClass="validatorCalloutHighlight" />
                                                </asp:Panel>
                                                <asp:Panel ID="Panel_CAMBIO_PASSWORD_OBLIGATORIO" runat="server">
                                                    <table class="table_control_registros" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    Usuario
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_USU_LOG_CAMBIO" runat="server" Width="140px" ValidationGroup="CAMBIO"
                                                                        Height="16px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    Cedula
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_CEDULA_CAMBIO" runat="server" Width="140px" ValidationGroup="CAMBIO"
                                                                        Height="16px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    Psw Actual
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_USU_PSW_ANT_CAMBIO" runat="server" Width="140px" TextMode="Password"
                                                                        ValidationGroup="CAMBIO" Height="16px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    Psw Nuevo
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_USU_PSW_NUEVO_CAMBIO" runat="server" Width="140px" TextMode="Password"
                                                                        ValidationGroup="CAMBIO" Height="16px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_izq_login">
                                                                <div>
                                                                    Conf. Nuevo
                                                                </div>
                                                            </td>
                                                            <td class="td_der_login">
                                                                <div>
                                                                    <asp:TextBox ID="TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO" runat="server" Width="140px"
                                                                        TextMode="Password" ValidationGroup="CAMBIO" Height="16px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td_link_recordar_password" colspan="2">
                                                                <div>
                                                                    <asp:Button ID="Button_CAMBIAR_PSW" runat="server" Text="Cambiar" OnClick="Button_CAMBIAR_PSW_Click"
                                                                        ValidationGroup="CAMBIO" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- TextBox_USU_LOG_CAMBIO -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_LOG_CAMBIO"
                                                        ControlToValidate="TextBox_USU_LOG_CAMBIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DE USUARIO es requerido."
                                                        ValidationGroup="CAMBIO" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_LOG_CAMBIO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_USU_LOG_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- TextBox_CEDULA_CAMBIO -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CEDULA_CAMBIO"
                                                        ControlToValidate="TextBox_CEDULA_CAMBIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NÚMERO DE CEDULA es requerido."
                                                        ValidationGroup="CAMBIO" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CEDULA_CAMBIO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_CEDULA_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- TextBox_USU_PSW_ANT_CAMBIO -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_PSW_ANT_CAMBIO"
                                                        ControlToValidate="TextBox_USU_PSW_ANT_CAMBIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PASSWORD ANTERIOR es requerido."
                                                        ValidationGroup="CAMBIO" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_ANT_CAMBIO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_USU_PSW_ANT_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- TextBox_USU_PSW_NUEVO_CAMBIO -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_USU_PSW_NUEVO_CAMBIO"
                                                        ControlToValidate="TextBox_USU_PSW_NUEVO_CAMBIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PASSWORD NUEVO es requerido."
                                                        ValidationGroup="CAMBIO" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NUEVO_CAMBIO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_USU_PSW_NUEVO_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_USU_PSW_NUEVO_CAMBIO"
                                                        Display="None" ControlToValidate="TextBox_USU_PSW_NUEVO_CAMBIO" ValidationExpression="^.*(?=.{10,})(?=.*\d)(?=.*[a-z|A-Z])(?=.*[@#$%^&amp;+=!/()?¡¿*+;:,.-_]).*$"
                                                        runat="server" ErrorMessage="<b>Campo Requerido faltante</b><br />El PASSWORD NUEVO no cumple con los requisitos mínimos.<br>Mínimo 10 caracteres.<br>Un número.<br>Un Caracter especial."
                                                        ValidationGroup="CAMBIO"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_USU_PSW_NUEVO_CAMBIO_1"
                                                        TargetControlID="RegularExpressionValidator_TextBox_USU_PSW_NUEVO_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                    <!-- TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO"
                                                        ControlToValidate="TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CONFIRMACIÓN DE NUEVO PASSWORD es requerida."
                                                        ValidationGroup="CAMBIO" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO"
                                                        HighlightCssClass="validatorCalloutHighlight" />
                                                    <asp:CompareValidator ID="CompareValidator_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO"
                                                        runat="server" Display="None" ControlToCompare="TextBox_USU_PSW_NUEVO_CAMBIO"
                                                        ControlToValidate="TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO" Operator="Equal" ErrorMessage="<b>Campo con formato incorrecto</b><br />La CONFIRMACIÓN DE NUEVO PASSWORD debe ser igual al Password nuevo."
                                                        ValidationGroup="CAMBIO"></asp:CompareValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO_1"
                                                        TargetControlID="CompareValidator_TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO" HighlightCssClass="validatorCalloutHighlight" />
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_repeat_inferior">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img name="plantilla_login_blue_r7_c1" src="../imagenes/plantilla/plantilla_login_blue_r7_c1.png"
                                width="338" height="22" border="0" id="plantilla_login_blue_r7_c1" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="div_texto_pie_login">
    </div>

    </form>
</body>
</html>
