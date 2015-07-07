<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="activarEmpleado.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField_persona" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_CONTRATO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_REQUISICION" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_OCUPACION" runat="server" />
            <asp:HiddenField ID="HiddenField_NUM_DOC_IDENTIDAD" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
            <asp:HiddenField ID="HiddenField_SUELDO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPLEADO" runat="server" />
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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
                text-align: center;">
                <div class="div_botones_internos">
                    <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: center">
                                <asp:Table ID="Table_MENU" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_Informacion_Contrato" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contiene_Cargos_en_Fuente">
                    <asp:Panel ID="Panel_Cargos_Por_Fuente" runat="server">
                        <div class="div_contenedor_formulario">
                            <div class="div_cabeza_groupbox">
                                Elaborar Contrato
                            </div>
                            <div class="div_contenido_groupbox">
                                <div class="div_cabeza_groupbox_gris">
                                    Información del Trabajador
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_nombre" runat="server" Text="Nombres:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="TextBox_Nombres" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_apellidos" runat="server" Text="Apellidos:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="TextBox_Apellidos" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_doc_identidad" runat="server" Text="Documento de identidad:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="TextBox_doc_identidad" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Información del Trabajo
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_empresa" runat="server" Text="Empresa:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="TextBox_empresa" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <asp:HiddenField ID="HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION" runat="server" />
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Ciudad
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_Ciudad" runat="server" ValidationGroup="CREAR"
                                                    OnSelectedIndexChanged="DropDownList_Ciudad_SelectedIndexChanged" AutoPostBack="true"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_CIUDAD_SELECCIONADA" runat="server" Text=" " Height="20px" Width="20px"
                                                    BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Centro de Costo
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_CentroCosto" runat="server" ValidationGroup="CREAR"
                                                    AutoPostBack="true" Width="260px" OnSelectedIndexChanged="DropDownList_CentroCosto_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_CC_SELECCIONADO" runat="server" Text=" " Height="20px" Width="20px"
                                                    BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Sub Centro de Costo
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_sub_cc" runat="server" ValidationGroup="CREAR"
                                                    AutoPostBack="true" Width="260px" OnSelectedIndexChanged="DropDownList_Sub_CC_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_SUBC_SELECCIONADO" runat="server" Text=" " Height="20px" Width="20px"
                                                    BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_Servicio" runat="server" Text="Servicio" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_servicio" runat="server" ValidationGroup="CREAR"
                                                    AutoPostBack="true" Width="260px" Visible="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_SERVICIO_SELECCIONADO" runat="server" Text=" " Height="20px"
                                                    Width="20px" BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center;">
                                                <asp:Label ID="Label_Riesgo" runat="server" Text="Riesgo: Desconocido"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td style="text-align: center;">
                                                Documentos entregados al trabajador<br />
                                                <asp:TextBox ID="TextBox_Doc_Entregar" runat="server" TextMode="MultiLine" Width="820px"
                                                    Height="50px" ValidationGroup="GUARDAR" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                Requerimientos del usuario<br />
                                                <asp:TextBox ID="TextBox_Req_usuario" runat="server" TextMode="MultiLine" Width="820px"
                                                    Height="50px" ValidationGroup="GUARDAR" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_Cargo" runat="server" Text="Cargo:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="TextBox_Cargo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_PERIODO_PAGO" runat="server" Text="Periodo pago:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_PERIODO_PAGO" runat="server" ValidationGroup="CREAR"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:RadioButton ID="RadioButton_PRACTICANTE_UNI" runat="server" Text="Practicante universitario"
                                                    GroupName="tipo" OnCheckedChanged="RadioButton_PRACTICANTE_UNI_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td class="td_der">
                                                <asp:RadioButton ID="RadioButton_SENA_ELECTIVO" runat="server" Text="SENA Electivo"
                                                    GroupName="tipo" OnCheckedChanged="RadioButton_SENA_ELECTIVO_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td class="td_der" colspan="2">
                                                <asp:RadioButton ID="RadioButton_SENA_PRODUCTIVO" runat="server" Text="SENA Productivo"
                                                    GroupName="tipo" OnCheckedChanged="RadioButton_SENA_PRODUCTIVO_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td class="td_der" colspan="2">
                                                <asp:RadioButton ID="RadioButton_ninguno" runat="server" Text="Ninguno" GroupName="tipo"
                                                    OnCheckedChanged="RadioButton_ninguno_CheckedChanged" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_salario" runat="server" Text="Salario:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_Salario" runat="server" ValidationGroup="CREAR" Width="255px"
                                                    CssClass="money"></asp:TextBox>
                                            </td>
                                            <td class="td_der" colspan="2">
                                                <asp:DropDownList ID="DropDownList_DESCRIPCION_SALARIO" runat="server" Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Salario Integral:
                                            </td>
                                            <td class="td_der" colspan="3">
                                                <asp:DropDownList ID="DropDownList_Salario_integral" runat="server" AutoPostBack="true"
                                                    ValidationGroup="CREAR" Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Salario" runat="server"
                                        ControlToValidate="TextBox_Salario" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El SALARIO es requerido."
                                        ValidationGroup="CREAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Salario"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_Salario" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Salario"
                                        runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_Salario"
                                        ValidChars=",." />
                                    <%-- DropDownList_Salario_integral --%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Salario_integral"
                                        runat="server" ControlToValidate="DropDownList_Salario_integral" Display="None"
                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;b/&gt;&lt;br/&gt;EL TIPO DE SALARIO es requerido."
                                        ValidationGroup="CREAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Salario_integral"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Salario_integral" />
                                    <%-- DropDownList_servicio --%>
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_servicio" runat="server"
                                        ControlToValidate="DropDownList_servicio" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;b/&gt;&lt;br/&gt;EL SERVICIO es requerido."
                                        ValidationGroup="CREAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_servicio"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_servicio" />
                                    --%>
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Información del contrato
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros" cellpadding="0">
                                        <tr>
                                            <td class="td_izq">
                                                Clase de Contrato
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_Clase_contrato" runat="server" ValidationGroup="CREAR"
                                                    AutoPostBack="True" Width="260px" 
                                                    OnSelectedIndexChanged="DropDownList_Clase_contrato_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Tipo de Contrato
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_tipo_Contrato" runat="server" ValidationGroup="CREAR"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_fecha_inicio" runat="server" Text="Fecha de inicio del contrato:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_fecha_inicio" runat="server" Width="255px" ValidationGroup="CREAR"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha_inicio" runat="server"
                                                    TargetControlID="TextBox_fecha_inicio" Format="dd/MM/yyyy"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_fecha_terminacion" runat="server" Text="Fecha de terminación del contrato:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_fecha_terminacion" runat="server" Width="255px" ValidationGroup="CREAR"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha_terminacion" runat="server"
                                                    TargetControlID="TextBox_fecha_terminacion" Format="dd/MM/yyyy" />
                                            </td>
                                        </tr>
                                    </table>
                                    <%--DropDownList_Clase_contrato--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Clase_contrato"
                                        runat="server" ControlToValidate="DropDownList_Clase_contrato" Display="None"
                                        ErrorMessage="<b>Campo Requerido faltante<b/><br/>LA CLASE DE CONTRATO es requerido."
                                        ValidationGroup="CREAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Clase_contrato"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Clase_contrato" />
                                    <%--DropDownList_tipo_Contrato--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_tipo_Contrato"
                                        runat="server" ControlToValidate="DropDownList_tipo_Contrato" Display="None"
                                        ErrorMessage="<b>Campo Requerido faltante<b/><br/>EL TIPO DE CONTRATO es requerido."
                                        ValidationGroup="CREAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_tipo_Contrato"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_tipo_Contrato" />

                                    <%--TextBox_fecha_inicio--%>
                                    <asp:RangeValidator ID="RangeValidator_TextBox_fecha_inicio" runat="server"
                                        ErrorMessage="<b>Campo Requerido Errado<b/><br/>La FECHA_INICIO debe ser igual o mayor al día de hoy."
                                        ControlToValidate = "TextBox_fecha_inicio" Display="None" ValidationGroup= "CREAR" Type="Date" >
                                    </asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_inicio"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                        TargetControlID="RangeValidator_TextBox_fecha_inicio" />


                                    <asp:Panel ID="Panel_informacion_adicinal_contrato_indefinido" runat="server">
                                        <div style="margin: 10px auto; width: 500px; background-color: #eeeeee;">
                                            <div class="div_informacion_adicinal_contrato_indefinido" style="text-align: center;
                                                margin: 5px auto;">
                                                Información adicional para la impresión de contratos INDEFINIDOS
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre del Empleador
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_indefinido_nombre_empleador" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Número de identificación del Empleador(incluida la ciudad de expedición)
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_indefinido_identificacion_empleador" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad de domicilio
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_indefinido_ciudad_domicilio_empleador" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--TextBox_contrato_indefinido_ciudad_domicilio_empleador--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_indefinido_nombre_empleador"
                                                runat="server" ControlToValidate="TextBox_contrato_indefinido_nombre_empleador"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NOMBRE DEL EMPLEADOR es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_indefinido_nombre_empleador"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_indefinido_nombre_empleador" />
                                            <%--TextBox_contrato_indefinido_identificacion_empleador--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_indefinido_identificacion_empleador"
                                                runat="server" ControlToValidate="TextBox_contrato_indefinido_identificacion_empleador"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NUMERO DE DOCUMENTO DEL EMPLEADOR es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_indefinido_identificacion_empleador"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_indefinido_identificacion_empleador" />
                                            <%--TextBox_contrato_indefinido_ciudad_domicilio_empleador--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_indefinido_ciudad_domicilio_empleador"
                                                runat="server" ControlToValidate="TextBox_contrato_indefinido_ciudad_domicilio_empleador"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>CIUDAD DOMICILIO DEL EMPLEADOR es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_indefinido_ciudad_domicilio_empleador"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_indefinido_ciudad_domicilio_empleador" />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_informacion_adicional_contrato_integral" runat="server">
                                        <div style="margin: 10px auto; width: 500px; background-color: #eeeeee;">
                                            <div class="div_informacion_adicional_contrato_indefinido" style="text-align: center;
                                                margin: 5px auto;">
                                                Información adicional para la impresión de contratos INTEGRALES
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Porcentaje parafiscales
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_integral_porcentaje_parafiscales" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Porcentaje prestacional
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_integral_porcentaje_prestacional" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--TextBox_contrato_integral_porcentaje_parafiscales--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_integral_porcentaje_parafiscales"
                                                runat="server" ControlToValidate="TextBox_contrato_integral_porcentaje_parafiscales"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>PORCENTAJE PARAFISCALES es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_integral_porcentaje_parafiscales"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_integral_porcentaje_parafiscales" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_contrato_integral_porcentaje_parafiscales"
                                                runat="server" TargetControlID="TextBox_contrato_integral_porcentaje_parafiscales"
                                                FilterType="Numbers">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <%--TextBox_contrato_integral_porcentaje_prestacional--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_integral_porcentaje_prestacional"
                                                runat="server" ControlToValidate="TextBox_contrato_integral_porcentaje_prestacional"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>PORCENTAJE PRESTACIONAL es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_integral_porcentaje_prestacional"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_integral_porcentaje_prestacional" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_contrato_integral_porcentaje_prestacional"
                                                runat="server" TargetControlID="TextBox_contrato_integral_porcentaje_prestacional"
                                                FilterType="Numbers">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_informacion_adicional_aprendiz_universitario" runat="server">
                                        <div style="margin: 10px auto; width: 500px; background-color: #eeeeee;">
                                            <div style="text-align: center; margin: 5px auto;">
                                                Información adicional para la impresión de contratos APRENDICES UNIVERSITARIOS
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre del Representante Legal
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_universitario_representante_legal" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Número de identificación del Representante Legal
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_universitario_numero_identificacion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre de la Institución Educativa
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_universitario_nombre_institucion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Nit de la Institución Educativa
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_universitario_nit_institucion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Especialidad del aprendiz
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_universitario_especialidad_aprendiz" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--TextBox_contrato_aprendiz_universitario_representante_legal--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_representante_legal"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_universitario_representante_legal"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NOMBRE REPRESENTANTE LEGAL es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_universitario_representante_legal"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_representante_legal" />
                                            <%--TextBox_contrato_aprendiz_universitario_numero_identificacion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_numero_identificacion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_universitario_numero_identificacion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NUMERO DE DOCUMENTO DEL REPRESENTANTE LEGAL es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_universitario_numero_identificacion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_numero_identificacion" />
                                            <%--TextBox_contrato_aprendiz_universitario_nombre_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_nombre_institucion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_universitario_nombre_institucion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NOMBRE INSTITUCION EDUCATIVA es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_universitario_nombre_institucion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_nombre_institucion" />
                                            <%--TextBox_contrato_aprendiz_universitario_nit_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_nit_institucion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_universitario_nit_institucion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NIT INSTITUCION EDUCATIVA es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_universitario_nit_institucion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_nit_institucion" />
                                            <%--TextBox_contrato_aprendiz_universitario_nit_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_especialidad_aprendiz"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_universitario_especialidad_aprendiz"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>ESPECIALIDAD DEL APRENDIZ es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_universitario_especialidad_aprendiz"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_universitario_especialidad_aprendiz" />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_informacion_adicinal_contrato_aprendiz_sena" runat="server">
                                        <div style="margin: 10px auto; width: 500px; background-color: #eeeeee;">
                                            <div style="text-align: center; margin: 5px auto;">
                                                Información adicional para la impresión de contratos APRENDICES SENA ETAPA ELECTIVA/PRODUCTIVA
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre del Representante Legal
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_sena_representante_legal" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Número de identificación del Representante Legal
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_sena_numero_documento_r_l" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Cargo Representante Legal
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_sena_cargo_r_l" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Especialidad del aprendiz
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_sena_especialidad_aprendiz" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Curso del aprendiz
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_sena_curso_aprendiz" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Nombre de la Institución
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_nombre_institucion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Nit de la Institución
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_nit_institucion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Centro de formación
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_centro_formacion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Duración meses de la formación
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_meses_duracion" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Fecha de inicio
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_fecha_inicio" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_contrato_aprendiz_fecha_inicio"
                                                            runat="server" TargetControlID="TextBox_contrato_aprendiz_fecha_inicio" Format="dd/MM/yyyy" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Fecha final
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_contrato_aprendiz_fecha_final" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_contrato_aprendiz_fecha_final"
                                                            runat="server" TargetControlID="TextBox_contrato_aprendiz_fecha_final" Format="dd/MM/yyyy" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--TextBox_contrato_aprendiz_sena_representante_legal--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_representante_legal"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_sena_representante_legal"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NOMBRE REPRESENTANTE LEGAL es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_sena_representante_legal"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_representante_legal" />
                                            <%--TextBox_contrato_aprendiz_sena_numero_documento_r_l--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_numero_documento_r_l"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_sena_numero_documento_r_l"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NUMERO DE DOCUMENTO DEL REPRESENTANTE LEGAL es requerido."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_sena_numero_documento_r_l"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_numero_documento_r_l" />
                                            <%--TextBox_contrato_aprendiz_sena_cargo_r_l--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_cargo_r_l"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_sena_cargo_r_l" Display="None"
                                                ErrorMessage="<b>Campo Requerido faltante<b/><br/>CARGO DEL REPRESENTANTE LEGAL es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_sena_cargo_r_l"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_sena_cargo_r_l" />
                                            <%--TextBox_contrato_aprendiz_nombre_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_nombre_institucion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_nombre_institucion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NOMBRE DE LA INSTITUCION es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_nombre_institucion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_nombre_institucion" />
                                            <%--TextBox_contrato_aprendiz_nit_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_nit_institucion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_nit_institucion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>NIT DE LA INSTITUCION es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_nit_institucion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_nit_institucion" />
                                            <%--TextBox_contrato_aprendiz_centro_formacion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_centro_formacion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_centro_formacion"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante<b/><br/>CENTRO DE FORMACION es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_centro_formacion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_centro_formacion" />
                                            <%--TextBox_contrato_aprendiz_meses_duracion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_meses_duracion"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_meses_duracion" Display="None"
                                                ErrorMessage="<b>Campo Requerido faltante<b/><br/>MESES DE DURACION es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_meses_duracion"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_meses_duracion" />
                                            <%--TextBox_contrato_aprendiz_nit_institucion--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_fecha_inicio"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_fecha_inicio" Display="None"
                                                ErrorMessage="<b>Campo Requerido faltante<b/><br/>FECHA INICIO es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_fecha_inicio"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_fecha_inicio" />
                                            <%--TextBox_contrato_aprendiz_fecha_final--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_contrato_aprendiz_fecha_final"
                                                runat="server" ControlToValidate="TextBox_contrato_aprendiz_fecha_final" Display="None"
                                                ErrorMessage="<b>Campo Requerido faltante<b/><br/>FECHA FINAL es requerida."
                                                ValidationGroup="CREAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_contrato_aprendiz_fecha_final"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_contrato_aprendiz_fecha_final" />
                                        </div>
                                    </asp:Panel>
                                    <%--Sección para seleccionar la forma de impresión de contrato--%>
                                    <div style="margin: 10px auto; width: 500px; background-color: #eeeeee;">
                                        <div class="div_avisos_importantes" style="text-align: center; margin: 5px auto;">
                                            Por favor seleccione la forma de impresión del contrato
                                        </div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Forma de impresión de contrato
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_FORMA_IMPRESION_CONTRATO" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="CheckBox_CON_CARNET_APARTE" runat="server" Text="Con Carnet Incluido" />
                                                </td>
                                            </tr>
                                        </table>
                                        <%--DropDownList_Clase_contrato--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_FORMA_IMPRESION_CONTRATO"
                                            runat="server" ControlToValidate="DropDownList_FORMA_IMPRESION_CONTRATO" Display="None"
                                            ErrorMessage="<b>Campo Requerido faltante<b/><br/>LA FORMA DE IMPRESIÓN DE CONTRATO es requerida."
                                            ValidationGroup="CREAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_FORMA_IMPRESION_CONTRATO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_FORMA_IMPRESION_CONTRATO" />
                                    </div>
                                </div>
                                <div class="div_espaciador">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:Button ID="Button_Guardar" runat="server" Text="Guardar" CssClass="margin_botones"
                                    ValidationGroup="CREAR" OnClick="Button_Guardar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_clausulas" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Clausulas que serán aplicadas al contratar al trabajador
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <asp:GridView ID="GridView_clausulas" runat="server" Width="846px" AllowPaging="True"
                                                    AutoGenerateColumns="False" DataKeyNames="ID_CLAUSULA,ID_EMPLEADO">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_CLAUSULA" HeaderText="ID_CLAUSULA" Visible="false">
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="false">
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TIPO_CLAUSULA" HeaderText="Tipo">
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion">
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Archivo">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="FileUpload_ARCHIVO" runat="server" />
                                                                <asp:HyperLink ID="HyperLink_ARCHIVO" runat="server">Ver Clausulas</asp:HyperLink>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FileUpload_archivo" runat="server"
                                                                    ControlToValidate="FileUpload_ARCHIVO" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ARCHIVO es requerido."
                                                                    ValidationGroup="ACTUALIZAR" />
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FileUpload_archivo"
                                                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_FileUpload_archivo" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="Panel_GUARDAR_PROCESO" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_Guardar_CLAUSULAS" runat="server" Text="Guardar Proceso" CssClass="margin_botones"
                                                ValidationGroup="ACTUALIZAR" OnClick="Button_Guardar_CLAUSULAS_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_MENU" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de contratación
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Imprimir" runat="server" Text="Imprimir Contrato" CssClass="margin_botones"
                                                OnClick="Button_Imprimir_Click" ValidationGroup="CONTRATO" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_Imprimir" />
            <asp:PostBackTrigger ControlID="Button_Guardar_CLAUSULAS" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
