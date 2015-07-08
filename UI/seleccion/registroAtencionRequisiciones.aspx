<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="registroAtencionRequisiciones.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) 
        {
            $(document).ready(function () 
            {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
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

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                ValidationGroup="NUEVO_CLIENTE" OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" 
                                                Width="75px" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" Width="65px" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" Width="67px" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_VOLVER" runat="server" Text="Hoja trabajo" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_VOLVER_Click" Width="90px" />
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
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" ValidationGroup="BUSCAR_CLIENTE"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="100px" ValidationGroup="BUSCAR_CLIENTE"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_Cumplido" runat="server" Text="Cumplido"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_Cumplido" runat="server" AutoPostBack="true">
                                                <asp:ListItem Value="S"> SI </asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True"> NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_Cancelado" runat="server" Text="Cancelado"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_Cancelado" runat="server">
                                                <asp:ListItem Value="S"> SI </asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True"> NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" ValidationGroup="BUSCAR_CLIENTE"
                                                CssClass="margin_botones" OnClick="Button_BUSCAR_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_avanzadas" runat="server" Text=">>" ValidationGroup="BUSQUEDA_AVANZADA"
                                                CssClass="margin_botones" OnClick="Button_BUSCAR_AVANZADA_Click" ToolTip="Búsqueda Avanzada" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                ControlToValidate="DropDownList_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda."
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                ControlToValidate="TextBox_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar."
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                TargetControlID="RequiredFieldValidator_TextBox_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers"
                                runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Numbers" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Letras"
                                runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <div class="div_contenedor_formulario">
                <asp:Panel ID="Panel_PARAMETROS" runat="server">
                    <div class="div_cabeza_groupbox">
                        Busqueda Avanzada
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Text="Razón Social"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_RAZ_SOCIAL" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_RAZ_SOCIAL_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_CUMPLIDOS" runat="server" Text="Cumplido"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_CUMPLIDO" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_CUMPLIDO_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_CANCELADOS" runat="server" Text="Canceldo"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_CANCELADO" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_CANCELADO_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_REGIONAL" runat="server" Text="Regional"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_REGIONAL" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_REGIONAL_CheckedChanged" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_CIUDAD" runat="server" Text="Ciudad"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_CIUDAD" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_CIUDAD_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_PSICOLOGO" runat="server" Text="Psicólogo"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_PSICOLOGO" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_PSICOLOGO_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_TIPO" runat="server" Text="Tipo"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_TIPO" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_TIPO_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        <asp:Label ID="Label_FECHA_REQ" runat="server" Text="Fecha Requerida"></asp:Label>
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:CheckBox ID="CheckBox_FECHA_REQ" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_FECHA_REQ_CheckedChanged" />
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="Button_Busqueda_Avanzada" runat="server" Text="Buscar" ValidationGroup="BUSQUEDA_AVANZADA"
                                        CssClass="margin_botones" OnClick="Button_BUSQUEDA_AVANZADA_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel_RAZ_SOCIAL" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Razón Social
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_RAZON_SOCIAL" runat="server" Text="Razón Social"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_RAZON_SOCIAL" runat="server" ValidationGroup="REPORTE"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CUMPLIDO" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Cumplido
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CUMPLIDO_" runat="server" Text="Cumplido"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CUMPLIDOS" runat="server" ValidationGroup="REPORTE">
                                                <asp:ListItem Value="S"> SI </asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True"> NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CUMPLIDOS"
                                    ControlToValidate="DropDownList_CUMPLIDOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CUMPLIDOS"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CUMPLIDOS" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CANCELADO" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Cancelado
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CANCELADO_" runat="server" Text="Cancelado"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CANCELADOS" runat="server" ValidationGroup="REPORTE">
                                                <asp:ListItem Value="S"> SI </asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True"> NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CANCELADOS"
                                    ControlToValidate="DropDownList_CANCELADOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CANCELADOS"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CANCELADOS" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_REGIONAL" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Regional
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_REGIONALES" runat="server" Text="Regional"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_REGIONALES" runat="server" ValidationGroup="REPORTE">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_REGIONALES"
                                    ControlToValidate="DropDownList_REGIONALES" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_REGIONALES"
                                    TargetControlID="RequiredFieldValidator_DropDownList_REGIONALES" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CIUDAD" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Ciudad
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CIUDADES" runat="server" Text="Ciudad"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" ValidationGroup="REPORTE">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD"
                                    ControlToValidate="DropDownList_CIUDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_psicologo" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Psicólogo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_PSICOLOGOS" runat="server" Text="Psicólogo"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_PSICOLOGO" runat="server" ValidationGroup="REPORTE">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_PSICOLOGO"
                                    ControlToValidate="DropDownList_PSICOLOGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_PSICOLOGO"
                                    TargetControlID="RequiredFieldValidator_DropDownList_PSICOLOGO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_TIPO" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Tipo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_Tipos" runat="server" Text="Tipo"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_Tipo" runat="server" ValidationGroup="REPORTE">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PAR_USUARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Tipo"
                                    ControlToValidate="DropDownList_Tipo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />"
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Tipo"
                                    TargetControlID="RequiredFieldValidator_DropDownList_Tipo" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_FECHA_REQ" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Fecha Requerida
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA_INICIAL" runat="server" Text="Fecha inicial"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_INICIAL" runat="server" Width="90px" MaxLength="10"
                                                ValidationGroup="REPORTE"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA_FINAL" runat="server" Text="Fecha final"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_FINAL" runat="server" Width="90px" MaxLength="10"
                                                ValidationGroup="REPORTE"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_FECHA_INICIAL -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_INICIAL"
                                    ControlToValidate="TextBox_FECHA_INICIAL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA INICIAL es requerida."
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_INICIAL"
                                    TargetControlID="RequiredFieldValidator_TextBox_FECHA_INICIAL" HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_INICIAL" runat="server"
                                    TargetControlID="TextBox_FECHA_INICIAL" Format="dd/MM/yyyy" />
                                <!-- TextBox_FECHA_FINAL -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_FINAL"
                                    ControlToValidate="TextBox_FECHA_FINAL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA FINAL es requerida."
                                    ValidationGroup="REPORTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_FINAL"
                                    TargetControlID="RequiredFieldValidator_TextBox_FECHA_FINAL" HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_FINAL" runat="server"
                                    TargetControlID="TextBox_FECHA_FINAL" Format="dd/MM/yyyy" />
                                <asp:CompareValidator ID="CompareValidator_FECHAS" runat="server" ErrorMessage="<b>Error en FECHA INICIAL Y FECHA FINAL</b><br />La FECHA INICIAL no puede ser mayor que la FECHA FINAL."
                                    Operator="GreaterThanEqual" Type="Date" ControlToCompare="TextBox_FECHA_INICIAL"
                                    ControlToValidate="TextBox_FECHA_FINAL" Display="None"></asp:CompareValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_FECHAS"
                                    TargetControlID="CompareValidator_FECHAS" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="div_espaciador">
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="Panel_GRID_RESULTADOS" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_REQUERIMIENTO,ID_EMPRESA" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                    AllowPaging="True" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Num. requisición">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                        <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_REQ" HeaderText="Req. tipo" />
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Apertura" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo Requerido" />
                                        <asp:BoundField DataField="FECHA_CIERRE" HeaderText="Fecha Cierre" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONT_NOM" HeaderText="Contácto" />
                                        <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Infomación de la requisición
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                            <asp:Panel ID="Panel_CABEZA_REGISTRO" runat="server" CssClass="div_cabeza_groupbox_gris">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 87%;">
                                            Control de registro
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                <tr>
                                                    <td style="font-size: 80%">
                                                        <asp:Label ID="Label_REGISTRO" runat="server">(Mostrar detalles...)</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image_REGISTRO" runat="server" CssClass="img_cabecera_hoja" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_CONTENIDO_REGISTRO" runat="server" CssClass="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FCH_CRE" runat="server" Text="Fecha de Creación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" ReadOnly="True"
                                                ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_REGISTRO" runat="Server"
                                TargetControlID="Panel_CONTENIDO_REGISTRO" ExpandControlID="Panel_CABEZA_REGISTRO"
                                CollapseControlID="Panel_CABEZA_REGISTRO" Collapsed="True" TextLabelID="Label_REGISTRO"
                                ImageControlID="Image_REGISTRO" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                SuppressPostBack="true">
                            </ajaxToolkit:CollapsiblePanelExtender>
                        </asp:Panel>
                        <asp:Panel ID="Panel_ID_REQUERIMIENTO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Código del Requerimiento
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_ID_REQUERIMIENTO" runat="server" Text="Código de req."></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID_REQUERIMIENTO" runat="server" ReadOnly="True" ValidationGroup="COD_CLIENTE"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_DATOS_FORMULARIO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Datos de Empresa Cliente
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Tipo Requisición
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_TIP_REQ" runat="server" Width="200px" ValidationGroup="GUARDAR">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            Empresa
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_ID_EMPRESA" runat="server" Width="450px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList_ID_EMPRESA_SelectedIndexChanged" ValidationGroup="GUARDAR">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_TIP_REQ -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_TIP_REQ" runat="server"
                                    ControlToValidate="DropDownList_TIP_REQ" Display="None" ErrorMessage="Campo Requerido faltante</br>El TIPO DE REQUISICIÓN es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_TIP_REQ"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_TIP_REQ" />
                                <!-- DropDownList_ID_EMPRESA -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_EMPRESA" runat="server"
                                    ControlToValidate="DropDownList_ID_EMPRESA" Display="None" ErrorMessage="Campo Requerido faltante</br> La EMPRESA es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_EMPRESA"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_EMPRESA" />
                            </div>
                            <asp:Panel ID="Panel_CONTRATOS_Y_SERVICIOS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Datos de Contrato Servicio / Servicio Respectivo
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <asp:HiddenField ID="HiddenField_ID_SERVICIO_RESPECTIVO" runat="server" />
                                                <asp:Panel ID="Panel_GRILLA_CONTRATOS_ACTIVOS" runat="server">
                                                    <div class="div_contenedor_grilla_resultados">
                                                        <div class="grid_seleccionar_registros">
                                                            <asp:GridView ID="GridView_CONTRATOS_ACTIVOS" runat="server" Width="840px" AutoGenerateColumns="False"
                                                                DataKeyNames="REGISTRO_CONTRATO,ID_SERVICIO_RESPECTIVO,ID_EMPRESA,FECHA_INICIO,FECHA_VENCE" OnRowCommand="GridView_CONTRATOS_ACTIVOS_RowCommand">
                                                                <Columns>
                                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                                                        Text="Seleccionar">
                                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="NUMERO_CONTRATO" HeaderText="Núm contrato">
                                                                        <ItemStyle CssClass="columna_grid_der" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FECHA_INICIO" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha Inicio">
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FECHA_VENCE" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha Vence">
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Servicio Respectivo">
                                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <headerStyle BackColor="#DDDDDD" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Cargo requerido y especificaciones
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:HiddenField ID="HiddenField_NivelRequerimiento" runat="server" />
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Cargo
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_PERFILES" runat="server" Width="700px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList_PERFILES_SelectedIndexChanged" ValidationGroup="GUARDAR">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Funciones a Desarrollar<br />
                                            <asp:TextBox ID="TextBox_FUNCIONES" runat="server" Width="750px" Height="60px" TextMode="MultiLine"
                                                Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador">
                                </div>
                                <table cellspacing="3" cellpadding="3" class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Edad Mínima
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_EDAD_MIN" runat="server" Width="85px" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Edad Máxima
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_EDAD_MAX" runat="server" Width="85px" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Sexo
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_SEXO" runat="server" Enabled="False" Width="90px" ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            Personas Solicitadas
                                        </td>
                                        <td class="td_der" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            <asp:TextBox ID="TextBox_CANTIDAD" runat="server" Width="90px" ValidationGroup="GUARDAR"
                                                MaxLength="6" AutoPostBack="True" OnTextChanged="TextBox_CANTIDAD_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PERFILES -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_PERFILES" runat="server"
                                    ControlToValidate="DropDownList_PERFILES" Display="None" ErrorMessage="Campo Requerido faltante</br>El PERFIL es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_PERFILES"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_PERFILES" />
                                <!-- TextBox_CANTIDAD -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_CANTIDAD" runat="server"
                                    ControlToValidate="TextBox_CANTIDAD" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE PERSONAS SOLICITADAS es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_CANTIDAD"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_CANTIDAD" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CANTIDAD"
                                    runat="server" TargetControlID="TextBox_CANTIDAD" FilterType="Numbers" />
                                <asp:RangeValidator ID="RangeValidator_TextBox_CANTIDAD" runat="server" ErrorMessage="<b>Datos erroneos</b><br />El NÚMERO DE PERSONAS SOLICITADAS no esta en el rango correcto."
                                    MaximumValue="99999999" MinimumValue="1" Type="Integer" ControlToValidate="TextBox_CANTIDAD"
                                    Display="None" ValidationGroup="GUARDAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CANTIDAD_1"
                                    TargetControlID="RangeValidator_TextBox_CANTIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Experiencia
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_EXPERIENCIA" runat="server" Width="270px" Enabled="False"
                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Nivel Académico
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_NIV_ACADEMICO" runat="server" Width="270px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Fechas de Requisición
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="div_avisos_importantes">
                                    <tr>
                                        <td>
                                            NOTA: La Fecha <B>REFERENCIA SISTEMA</B> es calculada automáticamente por el sistema dependiendo
                                            del Perfil seleccionado y la cantidad de personal requerido. Para Requisiciones
                                            en las que se requieren más de 20 personas la Fecha <B>REFERENCIA SISTEMA</B> debe ser digitada
                                            manualmente.
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador">
                                </div>
                                <table cellpadding="5" cellspacing="4" class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha Recibo
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_R" runat="server" Enabled="False" ValidationGroup="GUARDAR"
                                                Width="120px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA_R" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="TextBox_FECHA_R" />
                                        </td>
                                        <td class="td_izq" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            Fecha Requiere Cliente
                                        </td>
                                        <td class="td_der" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            <asp:TextBox ID="TextBox_FECHA_REQUERIDA" runat="server" ValidationGroup="GUARDAR"
                                                Width="120px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="TextBox_FECHA_REQUERIDA_CalendarExtender" runat="server"
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_FECHA_REQUERIDA" />
                                        </td>
                                        <td class="td_izq" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            Fecha Referencia Sistema
                                        </td>
                                        <td class="td_der" style="background-color: #dddddd; border: 1px solid #aaaaaa;">
                                            <asp:TextBox ID="TextBox_FechaReferenciaSistema" runat="server" ValidationGroup="GUARDAR"
                                                Width="120px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaReferenciaSistema"
                                                runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_FechaReferenciaSistema" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_FECHA_R -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FECHA_R" runat="server"
                                    ControlToValidate="TextBox_FECHA_R" Display="None" ErrorMessage="Campo Requerido faltante&lt;/br&gt;La FECHA DE RECIBO es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FECHA_R"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FECHA_R" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FECHA_R"
                                    runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_FECHA_R"
                                    ValidChars="/">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <!-- TextBox_FECHA_REQUERIDA -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FECHA_REQUERIDA" runat="server"
                                    ControlToValidate="TextBox_FECHA_REQUERIDA" Display="None" ErrorMessage="Campo Requerido faltante&lt;/br&gt;La FECHA REQUERIDA POR EL CLIENTE es necesaria."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FECHA_REQUERIDA"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FECHA_REQUERIDA" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FECHA_REQUERIDA"
                                    runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_FECHA_REQUERIDA"
                                    ValidChars="/">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RangeValidator ID="RangeValidator_TextBox_FECHA_REQUERIDA" runat="server" ControlToValidate="TextBox_FECHA_REQUERIDA"
                                    Display="None" ErrorMessage="ERROR&lt;/br&gt;La FECHA REQUERIDA POR EL CLIENTE no está en un rango válido. (no puede ser menor que la fecha actual)"
                                    MaximumValue="31/12/2090" MinimumValue="01/01/2011" Type="Date" ValidationGroup="GUARDAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FECHA_REQUERIDA_1"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RangeValidator_TextBox_FECHA_REQUERIDA" />
                                <!-- TextBox_FechaReferenciaSistema -->
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FechaReferenciaSistema"
                                    runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_FechaReferenciaSistema"
                                    ValidChars="/">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <div class="div_cabeza_groupbox_gris">
                                            Datos de envio
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_ENVIO_CANDIDATOS" runat="server" Width="260px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList_ENVIO_CANDIDATOS_SelectedIndexChanged"
                                                            ValidationGroup="GUARDAR">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td rowspan="5" valign="top">
                                                        <div class="div_cabeza_groupbox_gris">
                                                            Condiciones de envio
                                                        </div>
                                                        <div class="div_contenido_groupbox_gris">
                                                            <asp:TextBox ID="TextBox_COND_ENVIO" runat="server" Height="105px" TextMode="MultiLine"
                                                                Width="510px" Enabled="False" MaxLength="250" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Dirección
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DIR_ENVIO" runat="server" Width="255px" Enabled="False"
                                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_CIUDAD_ENVIO" runat="server" Width="255px" Enabled="False"
                                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        E-Mail
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_EMAIL_ENVIO" runat="server" Width="255px" Enabled="False"
                                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Teléfono
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_TEL_ENVIO" runat="server" Enabled="False" ValidationGroup="GUARDAR"
                                                            Width="255px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_ENVIO_CANDIDATOS -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ENVIO_CANDIDATOS"
                                                runat="server" ControlToValidate="DropDownList_ENVIO_CANDIDATOS" Display="None"
                                                ErrorMessage="Campo Requerido faltante</br>El SOLICITANTE es requerido." ValidationGroup="GUARDAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ENVIO_CANDIDATOS"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ENVIO_CANDIDATOS" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros" width="870">
                                <tr>
                                    <td valign="top" style="width: 50%;">
                                        <div class="div_cabeza_groupbox_gris">
                                            Ubicación</div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Regional
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="280px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DropDownList_REGIONAL_SelectedIndexChanged" ValidationGroup="GUARDAR">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Departamento
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="280px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_SelectedIndexChanged" ValidationGroup="GUARDAR">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_CIUDAD_REQ" runat="server" Width="280px" 
                                                            ValidationGroup="GUARDAR" AutoPostBack="True" 
                                                            onselectedindexchanged="DropDownList_CIUDAD_REQ_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_CIUDAD_REQ -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CIUDAD_REQ" runat="server"
                                                ControlToValidate="DropDownList_CIUDAD_REQ" Display="None" ErrorMessage="Campo Requerido faltante</br>La CIUDAD DE UBICACIÓN es necesaria."
                                                ValidationGroup="GUARDAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CIUDAD_REQ"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD_REQ" />
                                        </div>
                                    </td>
                                    <td style="width: 50%;">
                                        <div class="div_cabeza_groupbox_gris">
                                            Datos del Contrato de Trabajo
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Duración
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_DURACION" runat="server" ValidationGroup="GUARDAR"
                                                            Width="280px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Jornada
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_HORARIO" runat="server" ValidationGroup="GUARDAR"
                                                            Width="280px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Salario Ofrecido
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_SALARIO" runat="server" ValidationGroup="GUARDAR" MaxLength="9"
                                                            Width="275px" CssClass="money"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_DURACION -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_DURACION" runat="server"
                                                ControlToValidate="DropDownList_DURACION" Display="None" ErrorMessage="Campo Requerido faltante</br>La DURACIÓN es requerida."
                                                ValidationGroup="GUARDAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_DURACION"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_DURACION" />
                                            <!-- DropDownList_HORARIO -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_HORARIO" runat="server"
                                                ControlToValidate="DropDownList_HORARIO" Display="None" ErrorMessage="Campo Requerido faltante</br>El HORARIO es requerido."
                                                ValidationGroup="GUARDAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_HORARIO"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_HORARIO" />
                                            <!-- TextBox_SALARIO -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_SALARIO" runat="server"
                                                ControlToValidate="TextBox_SALARIO" Display="None" ErrorMessage="Campo Requerido faltante</br>El SALARIO es requerido."
                                                ValidationGroup="GUARDAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_SALARIO"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_SALARIO" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_SALARIO"
                                                runat="server" TargetControlID="TextBox_SALARIO" FilterType="Numbers,Custom"
                                                ValidChars=".," />
                                            <asp:RangeValidator ID="RangeValidator_TextBox_SALARIO" runat="server" ErrorMessage="<b>Datos erroneos</b><br />El SALARIO no esta en el rango correcto."
                                                MaximumValue="999999999" MinimumValue="1" Type="Integer" ControlToValidate="TextBox_CANTIDAD"
                                                Display="None" ValidationGroup="GUARDAR"></asp:RangeValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SALARIO_1"
                                                TargetControlID="RangeValidator_TextBox_SALARIO" HighlightCssClass="validatorCalloutHighlight" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros" width="850">
                                <tr>
                                    <td valign="top" style="width: 50%;">
                                        <div class="div_cabeza_groupbox_gris">
                                            Adicionales (Observaciones)
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            Observaciones<br />
                                            <asp:TextBox ID="TextBox_OBS_REQUERIMIENTO" runat="server" TextMode="MultiLine" Width="700px"
                                                Height="60px" ValidationGroup="GUARDAR" MaxLength="250"></asp:TextBox>
                                        </div>
                                        <!-- TextBox_OBS_REQUERIMIENTO -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_OBS_REQUERIMIENTO"
                                            runat="server" ControlToValidate="TextBox_OBS_REQUERIMIENTO" Display="None" ErrorMessage="Campo Requerido faltante</br>Las OBSERVACIONES son requeridas."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_OBS_REQUERIMIENTO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_OBS_REQUERIMIENTO" />
                                    </td>
                                </tr>
                            </table>

                            <asp:Panel ID="Panel_InformacionEnvioEmail" runat="server">
                                <div class="div_espaciador">
                                </div>

                                <table class="table_control_registros" width="870">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Información Envío E-mail
                                            </div> 
                                            <div class="div_contenido_groupbox_gris">
                                                <asp:Panel ID="Panel_EnvioEmailIndeterminado" runat="server">
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label_EnvioEmailIndeterminado" runat="server" Text="Mensaje..." 
                                                                    Font-Bold="False" ForeColor="#CC0000"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_EnvioCorreos" runat="server" Width="865px" AutoGenerateColumns="False" DataKeyNames="NOMBRE_COLABORADOR,USU_LOG,MAIL_COLABORADOR,ROL_COLABORADOR">
                                                            <Columns>
                                                                <asp:BoundField DataField="NOMBRE_COLABORADOR" HeaderText="Colaborador" />
                                                                <asp:BoundField DataField="USU_LOG" HeaderText="Usuario" />
                                                                <asp:BoundField DataField="MAIL_COLABORADOR" HeaderText="E-Mail" />
                                                                <asp:BoundField DataField="ROL_COLABORADOR" HeaderText="Perfil" />
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                            </div>
                                        </td>
                                    </tr>
                                </table>    
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_ACCION_1" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                ValidationGroup="NUEVO_CLIENTE" OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_VOLVER_1" runat="server" Text="Hoja de trabajo" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_VOLVER_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_ACCIONES_REQUISICION" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox">
                                Botones de Requisición</div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Trazabilidad" runat="server" Text="Trazabilidad"
                                                CssClass="margin_botones" ValidationGroup="NUEVO_CLIENTE" 
                                                onclick="Button_Trazabilidad_Click"/>
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_ENVIAR_CANDIDATOS" runat="server" Text="Aspirantes Disponibles"
                                                CssClass="margin_botones" ValidationGroup="NUEVO_CLIENTE" OnClick="Button_ENVIAR_CANDIDATOS_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANDIDATOS_EN_CLIENTE" runat="server" Text="Candidatos en cliente"
                                                CssClass="margin_botones" OnClick="Button_CANDIDATOS_EN_CLIENTE_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_HISTORIAL" runat="server" Text="Historial" CssClass="margin_botones"
                                                ValidationGroup="NUEVOCLIENTE" OnClick="Button_HISTORIAL_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_SEGUIMIENTO" runat="server" Text="Seguimiento" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_SEGUIMIENTO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_COPIAR_REQ" runat="server" Text="Copiar Requisición" CssClass="margin_botones"
                                                ValidationGroup="COPIAR_MREQ" OnClick="Button_COPIAR_REQ_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelSeguimientoDesdeReclutamiento" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" width="870">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Trazabilidad
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <asp:Panel ID="PanelTrazavilidad" runat="server">
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td>
                                                                <div class="div_contenedor_grilla_resultados">
                                                                    <div class="grid_seleccionar_registros">
                                                                        <asp:GridView ID="GridViewTrazavilidad" runat="server" Width="200px" AutoGenerateColumns="False"
                                                                            DataKeyNames="ID_REQUERIMIENTO">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="REQUERIMIENTO" />
                                                                                <asp:BoundField DataField="RECLUTADOR" HeaderText="RECLUTADOR" />
                                                                                <asp:BoundField DataField="CANTIDAD_REQUERIMIENTO" HeaderText="CANTIDAD SOLICITADA" />
                                                                                <asp:BoundField DataField="CANTIDAD_RECLUTAMIENTO" HeaderText="CANTIDAD REQUERIDA" />
                                                                                <asp:BoundField DataField="CONTACTADOS" HeaderText="CANTIDAD CONTACTADA" />
                                                                                <asp:BoundField DataField="RECHAZAN_OFERTA" HeaderText="RECHAZAN OFERTA" />
                                                                                <asp:BoundField DataField="ACEPTAN_OFERTA" HeaderText="ACEPTAN OFERTA" />
                                                                                <asp:BoundField DataField="ASISTEN_A_ENTREVISTA" HeaderText="ASISTEN A ENTREVISTA" />
                                                                                <asp:BoundField DataField="CANTIDAD_DESCARTADA" HeaderText="CANTIDAD DESCARTADA" />
                                                                                <asp:BoundField DataField="CANTIDAD_ENVIADA_AL_CLIENTE" HeaderText="CANTIDAD ENVIADA AL CLIENTE" />
                                                                            </Columns>
                                                                            <headerStyle BackColor="#DDDDDD" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
            <asp:Panel ID="Panel_GRID_ENVIAR_A_CLIENTE" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Candidatos Disponibles Para Enviar al Cliente
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_cabeza_groupbox_gris">
                            Seleccionar Tipo de Filtrado
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO" runat="server" Text="Filtro por perfil"
                                            AutoPostBack="True" OnCheckedChanged="CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL" runat="server"
                                            Text="Filtro por variación de perfil" AutoPostBack="True" OnCheckedChanged="CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA" runat="server" Text="Filtro por cedula"
                                            AutoPostBack="True" OnCheckedChanged="CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="Panel_INTERNO_FILTRO_VARIACION_PERFIL" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList_FiltrarCargo" runat="server" RepeatDirection="Horizontal"
                                                        ValidationGroup="ENVIARCANDIDATO">
                                                        <asp:ListItem Value="CARGO_EXACTO">Cargo Exacto</asp:ListItem>
                                                        <asp:ListItem Value="CARGOS_ASOCIADOS">Cargos Asociados por Cargo DANE</asp:ListItem>
                                                        <asp:ListItem Value="SIN_CARGO">Sin Cargo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Edad min.
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_EDAD_MINIMA" runat="server" Width="50px" ValidationGroup="ENVIARCANDIDATO"></asp:TextBox>
                                                </td>
                                                <td class="td_izq">
                                                    Edad max.
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_EDAD_MAXIMA" runat="server" Width="50px" ValidationGroup="ENVIARCANDIDATO"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Nivel educación
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_NIV_EDUCACION" runat="server" ValidationGroup="ENVIARCANDIDATO"
                                                        Width="260px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    Sexo
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_SEXO" runat="server" ValidationGroup="ENVIARCANDIDATO"
                                                        Width="260px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    Experiencia
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_EXPERIENCIA" runat="server" ValidationGroup="ENVIARCANDIDATO"
                                                        Width="260px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_TRAER_DATOS_FILTRO_VARIACION" runat="server" Text="Consultar"
                                            CssClass="margin_botones" OnClick="Button_TRAER_DATOS_FILTRO_VARIACION_Click"
                                            ValidationGroup="ENVIARCANDIDATO" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="RadioButtonList_FiltroCiudad" runat="server" RepeatDirection="Horizontal"
                                                        ValidationGroup="ENVIARCANDIDATO">
                                                        <asp:ListItem Value="CON_CIUDAD">Candidatos viviendo en la ciudad de la requisición</asp:ListItem>
                                                        <asp:ListItem Value="SIN_CIUDAD">No Importa la Ciudad</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_EDAD_MINIMA -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EDAD_MINIMA" runat="server"
                                ControlToValidate="TextBox_EDAD_MINIMA" Display="None" ErrorMessage="Campo Requerido faltante</br>La EDAD MINIMA es requerida."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EDAD_MINIMA"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EDAD_MINIMA" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EDAD_MINIMA"
                                runat="server" TargetControlID="TextBox_EDAD_MINIMA" FilterType="Numbers" />
                            <asp:RangeValidator ID="RangeValidator_TextBox_EDAD_MINIMA" runat="server" ErrorMessage="<b>Datos erroneos</b><br />La EDAD MINIMA no esta en el rango correcto."
                                MaximumValue="99999999" MinimumValue="1" Type="Integer" ControlToValidate="TextBox_EDAD_MINIMA"
                                Display="None" ValidationGroup="ENVIARCANDIDATO"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EDAD_MINIMA_1"
                                TargetControlID="RangeValidator_TextBox_EDAD_MINIMA" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_EDAD_MAXIMA -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EDAD_MAXIMA" runat="server"
                                ControlToValidate="TextBox_EDAD_MAXIMA" Display="None" ErrorMessage="Campo Requerido faltante</br>La EDAD MAXIMA es requerida."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EDAD_MAXIMA"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EDAD_MAXIMA" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EDAD_MAXIMA"
                                runat="server" TargetControlID="TextBox_EDAD_MAXIMA" FilterType="Numbers" />
                            <asp:RangeValidator ID="RangeValidator_TextBox_EDAD_MAXIMA" runat="server" ErrorMessage="<b>Datos erroneos</b><br />La EDAD MAXIMA no esta en el rango correcto."
                                MaximumValue="99999999" MinimumValue="1" Type="Integer" ControlToValidate="TextBox_EDAD_MAXIMA"
                                Display="None" ValidationGroup="ENVIARCANDIDATO"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EDAD_MAXIMA_1"
                                TargetControlID="RangeValidator_TextBox_EDAD_MAXIMA" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- DropDownList_NIV_EDUCACION -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_NIV_EDUCACION"
                                runat="server" ControlToValidate="DropDownList_NIV_EDUCACION" Display="None"
                                ErrorMessage="Campo Requerido faltante</br>El NIVEL ACADEMICO es requerido."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_NIV_EDUCACION"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_NIV_EDUCACION" />
                            <!-- DropDownList_SEXO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_SEXO" runat="server"
                                ControlToValidate="DropDownList_SEXO" Display="None" ErrorMessage="Campo Requerido faltante</br>El SEXO es requerido."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_SEXO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_SEXO" />
                            <!-- DropDownList_EXPERIENCIA -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_EXPERIENCIA"
                                runat="server" ControlToValidate="DropDownList_EXPERIENCIA" Display="None" ErrorMessage="Campo Requerido faltante</br>La EXPERIENCIA es requerida."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_EXPERIENCIA"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_EXPERIENCIA" />
                            <!-- RadioButtonList_FiltrarCargo -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_RadioButtonList_FiltrarCargo"
                                runat="server" ControlToValidate="RadioButtonList_FiltrarCargo" Display="None"
                                ErrorMessage="Campo Requerido faltante</br> El Filtro de Cargo es requerido."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_RadioButtonList_FiltrarCargo"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_RadioButtonList_FiltrarCargo" />
                            <!-- RadioButtonList_FiltroCiudad -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_RadioButtonList_FiltroCiudad"
                                runat="server" ControlToValidate="RadioButtonList_FiltroCiudad" Display="None"
                                ErrorMessage="Campo Requerido faltante</br> El Filtro de Ciudad es requerido."
                                ValidationGroup="ENVIARCANDIDATO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_RadioButtonList_FiltroCiudad"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_RadioButtonList_FiltroCiudad" />
                        </asp:Panel>
                        <div class="div_espaciador">
                        </div>
                        <asp:Panel ID="Panel_INTERNO_FILTRO_CEDULA" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Número identificación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CEDULA" runat="server" ValidationGroup="CEDULA" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_TRAER_DATOS_FILTRO_CEDULA" runat="server" Text="Consultar"
                                            OnClick="Button_TRAER_DATOS_FILTRO_CEDULA_Click" ValidationGroup="CEDULA" />
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_CEDULA -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_CEDULA" runat="server"
                                ControlToValidate="TextBox_CEDULA" Display="None" ErrorMessage="Campo Requerido faltante</br>La El número de identificación es requerida."
                                ValidationGroup="CEDULA" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_CEDULA"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_CEDULA" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CEDULA"
                                runat="server" TargetControlID="TextBox_CEDULA" FilterType="Numbers" />
                        </asp:Panel>
                        <div class="div_espaciador">
                        </div>
                        <asp:Panel ID="Panel_GRILLA_ENVIAR_A_CLIENTE" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Resultados consulta
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <asp:GridView ID="GridView_ENVIAR_A_CLIENTE" runat="server" Width="865px" AutoGenerateColumns="False"
                                            DataKeyNames="ID_SOLICITUD">
                                            <Columns>
                                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                <asp:BoundField DataField="NOMBRE_SOLICITUD" HeaderText="Nombre" />
                                                <asp:BoundField DataField="DOC_IDENTIDAD_SOLICITUD" HeaderText="Doc. Identidad">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TEL_ASPIRANTE" HeaderText="Telefono">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EDAD_SOLICITUD" HeaderText="Edad">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NOMBRE_CIU_ASPIRANTE" HeaderText="Ciudad" />
                                                <asp:BoundField DataField="NOMBRE_OCUPACION" HeaderText="Cargo"></asp:BoundField>
                                                <asp:BoundField DataField="RESTRICCIONES_JURIDICAS" HeaderText="Restricciones jurídica"
                                                    HtmlEncode="False" HtmlEncodeFormatString="False">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox_CANDIDATOS_SELECCIONADOS_ENVIAR" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <headerStyle BackColor="#DDDDDD" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <div style="text-align: center;">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_ENVIAR_CANDIDATOS_SELECCIONADOS" runat="server" Text="Enviar candidatos seleccionados"
                                                    OnClick="Button_ENVIAR_CANDIDATOS_SELECCIONADOS_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="div_espaciador">
                        </div>
                        <asp:Button ID="Button_OCULTAR_ENVIAR_A_CLIENTES" runat="server" Text="Ocultar sección"
                            OnClick="Button_OCULTAR_ENVIAR_A_CLIENTES_Click" />
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>




            <asp:Panel ID="Panel_GRID_CANDIDATOS_EN_CLIENTE" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Candidatos en cliente
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_CANDIDATOS_EN_CLIENTE" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ID_EMPRESA,ID_SOLICITUD,ID_REQUERIMIENTO" Width="885px" OnRowCommand="GridView_CANDIDATOS_EN_CLIENTE_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="disponible" HeaderText="A disponible" Text="Disponible"
                                            ButtonType="Image" ImageUrl="~/imagenes/plantilla/undo.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="contratar" HeaderText="A contratar" Text="Contratar"
                                            ButtonType="Image" ImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo documento">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Num. documento">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TEL_ASPIRANTE" HeaderText="Telefono">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEXO" HeaderText="Sexo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E_MAIL" HeaderText="E-Mail" />
                                        <asp:BoundField DataField="ASPIRACION_SALARIAL" HeaderText="Aspiración" DataFormatString="$ {0:N}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <asp:Panel ID="Panel_CONFIRMAR_SULEDO_ENVIAR_A_CONTRATAR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="border: 2px solid #bbbbbb; margin: 5px auto; width: 800px; padding: 10px 0px;
                                background-color: #dddddd;">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label1" runat="server" Text="Id solicitud" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_ID_SOLICITUD" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label2" runat="server" Text="Nombre candidato" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_NOMBRE_CANDIDATO" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha de iniciación
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_INICIACION" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_INICIACION" runat="server"
                                                TargetControlID="TextBox_FECHA_INICIACION" Format="dd/MM/yyyy" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Confirmar salario
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_CONFIRMAR_SUELDO" runat="server" ValidationGroup="CONFIRMAR_SUELDO"
                                                CssClass="money"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_CONFIRMAR_SUELDO" runat="server" Text="Enviar a contratar"
                                                CssClass="margin_botones" ValidationGroup="CONFIRMAR_SUELDO" OnClick="Button_CONFIRMAR_SUELDO_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_CANCELAR_ENVIO" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                OnClick="Button_CANCELAR_ENVIO_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_CONFIRMAR_SUELDO -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_CONFIRMAR_SUELDO"
                                    runat="server" ControlToValidate="TextBox_CONFIRMAR_SUELDO" Display="None" ErrorMessage="Campo Requerido faltante</br>El SALARIO es requerido."
                                    ValidationGroup="CONFIRMAR_SUELDO" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_CONFIRMAR_SUELDO"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_CONFIRMAR_SUELDO" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CONFIRMAR_SUELDO"
                                    runat="server" TargetControlID="TextBox_CONFIRMAR_SUELDO" FilterType="Numbers,Custom"
                                    ValidChars=".," />
                                <asp:RangeValidator ID="RangeValidator_TextBox_CONFIRMAR_SUELDO" runat="server" ErrorMessage="<b>Datos erroneos</b><br />El SALARIO no esta en el rango correcto."
                                    MaximumValue="999999999" MinimumValue="1" Type="Currency" ControlToValidate="TextBox_CONFIRMAR_SUELDO"
                                    Display="None" ValidationGroup="CONFIRMAR_SUELDO"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONFIRMAR_SUELDO_1"
                                    TargetControlID="RangeValidator_TextBox_CONFIRMAR_SUELDO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                            <asp:Panel ID="Panel_REQUISITOS_CUMPLIDOS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <div style="border: 2px solid #bbbbbb; margin: 5px auto; width: 800px; padding: 10px 0px;
                                    background-color: #dddddd;">
                                    <div class="div_cabeza_groupbox_gris">
                                        Requisitos cumplidos
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        Los requisitos para esta requisicón estan completos
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="Panel_REQUISITOS_FALTANTES" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Requisitos faltantes
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_REQUISITOS_FALTANTES" runat="server" AutoGenerateColumns="False"
                                                            Width="790px">
                                                            <Columns>
                                                                <asp:BoundField DataField="TIPO_REQUISITO" HeaderText="Requisito" />
                                                                <asp:BoundField DataField="DOCUMENTO" HeaderText="Nombre" />
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="div_espaciador">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="Panel_BOTONES_INTERNOS_ENVIAR_A_CONTRATAR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_OCULTAR_CANDIDATOS_EN_CLIENTE" runat="server" Text="Ocultar"
                                            OnClick="Button_OCULTAR_CANDIDATOS_EN_CLIENTE_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_GRID_HISTORIAL" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Historial
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_HISTORIAL" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ID_EMPRESA,ID_SOLICITUD,ID_REQUERIMIENTO" Width="885px">
                                    <Columns>
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Envío">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRES_CANDIDATO" HeaderText="Nombre">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOCUMENTO_IDENTIDAD" HeaderText="Doc. Identidad">
                                            <ItemStyle CssClass="columna_Grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TEL_ASPIRANTE" HeaderText="Telefono">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEXO" HeaderText="Sexo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E_MAIL" HeaderText="E-Mail">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ASPIRACION_SALARIAL" HeaderText="Aspiración" DataFormatString="$ {0:N}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="div_espaciador">
                        </div>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button10" runat="server" Text="Ocultar" OnClick="Button10_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_GRID_SEGUIMIENTO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Seguimiento
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_SEGUIMIENTO" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO,ID_REQUERIMIENTO">
                                    <Columns>
                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        <asp:BoundField DataField="FECHA_R" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCION" HeaderText="Acción">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FCH_CRE" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha creación"
                                            Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_CRE" HeaderText="Usuario creación" />
                                        <asp:BoundField DataField="FCH_MOD" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha modificación"
                                            Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_MOD" HeaderText="Usuario modificación" Visible="False" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="div_espaciador">
                        </div>
                        <div style="background-color: #dddddd; margin: 10px 40px; padding: 5px; border: 1px solid #cccccc;">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_SEGUIMIENTO" runat="server" MaxLength="10" ValidationGroup="SEGUIMIENTO"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_SEGUIMIENTO" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="TextBox_FECHA_SEGUIMIENTO" />
                                    </td>
                                    <td class="td_izq">
                                        Acción
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ACCION_SEGUIMIENTO" runat="server" ValidationGroup="SEGUIMIENTO"
                                            Width="270px">
                                            <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                            <asp:ListItem Value="GESTIÓN DE RECLUTAMIENTO">GESTIÓN DE RECLUTAMIENTO</asp:ListItem>
                                            <asp:ListItem Value="RETROALIMENTACIÓN DEL CLIENTE">RETROALIMENTACIÓN DEL CLIENTE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        Comentario<br />
                                        <asp:TextBox ID="TextBox_OBS_SEGUIMIENTO" runat="server" TextMode="MultiLine" Width="500px"
                                            Height="70px" MaxLength="250" ValidationGroup="SEGUIMIENTO"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FECHA_SEGUIMIENTO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FECHA_SEGUIMIENTO"
                                runat="server" ControlToValidate="TextBox_FECHA_SEGUIMIENTO" Display="None" ErrorMessage="Campo Requerido faltante</br>La FECHA DE SEGUIMIENTO es necesaria."
                                ValidationGroup="SEGUIMIENTO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FECHA_SEGUIMIENTO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FECHA_SEGUIMIENTO" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FECHA_SEGUIMIENTO"
                                runat="server" TargetControlID="TextBox_FECHA_SEGUIMIENTO" FilterType="Custom,Numbers"
                                ValidChars="/">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <!-- TextBox_OBS_SEGUIMIENTO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_OBS_SEGUIMIENTO" runat="server"
                                ControlToValidate="TextBox_OBS_SEGUIMIENTO" Display="None" ErrorMessage="Campo Requerido faltante</br>Las OBSERVACIONES son necesarias."
                                ValidationGroup="SEGUIMIENTO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_OBS_SEGUIMIENTO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_OBS_SEGUIMIENTO" />
                            <!-- DropDownList_ACCION_SEGUIMIENTO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ACCION_SEGUIMIENTO"
                                runat="server" ControlToValidate="DropDownList_ACCION_SEGUIMIENTO" Display="None"
                                ErrorMessage="Campo Requerido faltante</br>La ACCION es necesario." ValidationGroup="SEGUIMIENTO" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ACCION_SEGUIMIENTO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ACCION_SEGUIMIENTO" />
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button11" runat="server" Text="Ingresar seguimiento" ValidationGroup="SEGUIMIENTO"
                                            OnClick="Button11_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
