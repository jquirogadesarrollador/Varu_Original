<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="visorActividades.aspx.cs" Inherits="_VisorActividades" %>

<%@ Register TagPrefix="ewc" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>

    <script language="javascript" type="text/javascript">
        setInterval('MantenSesion()', <%= (int) (0.9 * (Session.Timeout * 60000)) %>);
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:HiddenField ID="HiddenField_ID_ACTIVIDAD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_SUB_PROGRAMA_PADRE" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_DETALLE_GENERAL_PADRE" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_PROGRAMA_GENERAL" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_DETALLE" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_DETALLE_GENERAL" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_PRESUPUESTO" runat="server" />
    <asp:HiddenField ID="HiddenField_PRESUPUESTO" runat="server" />

    <asp:HiddenField ID="HiddenField_ACCION_SOBRE_BOTON" runat="server" />

    <asp:HiddenField ID="HiddenField_PRESUPUESTO_ASIGNADO_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_PRESUPUESTO_EJECUTADO_EMPRESA" runat="server" />

    <asp:HiddenField ID="HiddenField_SECCIONES_HABILITADAS" runat="server" />

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
            <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
        </asp:Panel>
        <div style="text-align: center; margin-top: 15px;">
            <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                Style="height: 26px" />
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
        <div class="div_espaciador">
        </div>
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
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICARFINAL" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICARFINAL" onclick="Button_MODIFICARFINAL_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_REPROGRAMAR" runat="server" Text="Reprogramar" CssClass="margin_botones"
                                        ValidationGroup="REPROGRAMAR" OnClick="Button_REPROGRAMAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_AJUSTARPRESUPUESTO" runat="server" 
                                        Text="Ajustar Presupuesto" CssClass="margin_botones"
                                        ValidationGroup="AJUSTARPRES" onclick="Button_AJUSTARPRESUPUESTO_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_RESULTADOS" runat="server" Text="Resultados" CssClass="margin_botones"
                                        ValidationGroup="RESULTADOS" OnClick="Button_ResultadosActividad_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_ACTIVIDAD" runat="server" Text="Cancelar Actividad"
                                        CssClass="margin_botones" ValidationGroup="CANCELARACTIVIDAD" OnClick="Button_CANCELAR_ACTIVIDAD_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
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
    <asp:Panel ID="Panel_InfoActividad" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de la Actividad
            </div>
            <div class="div_contenido_groupbox">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="div_para_convencion_hoja_trabajo">
                            <table class="tabla_alineada_derecha">
                                <tr>
                                    <td>
                                        Actividad Correctamente Creada
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_verde">
                                        </div>
                                    </td>
                                    <td>
                                        Actividad Cancelada
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_rojo">
                                        </div>
                                    </td>
                                    <td>
                                        Actividad Ejecutada
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_gris">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <asp:Label ID="Label_color_estado_detalle" runat="server" Width="800px" Height="25px"
                            Style="text-align: center; margin: 5px auto;"></asp:Label>
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
                        
                        <div class="div_espaciador">
                        </div>

                        <div class="div_cabeza_groupbox_gris">
                            Subprograma
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Subprograma al que Pertenece la Actividad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_SubPrograma" runat="server"
                                            Width="500px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="div_espaciador"></div>

                        <div class="div_cabeza_groupbox_gris">
                            Actividad
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Nombre:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_IdActividad" runat="server" ValidationGroup="GUARDAR"
                                            Width="700px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_der">
                                        Tipo:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList_Tipo" runat="server" ValidationGroup="GUARDAR"
                                            Width="180px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_izq">
                                        Sector:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_Sector" runat="server" ValidationGroup="GUARDAR"
                                            Width="230px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Estado:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList_EstadoActividad" runat="server" 
                                            ValidationGroup="GUARDAR" Width="230px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                        <div class="div_espaciador">
                        </div>

                        <div class="div_cabeza_groupbox_gris">
                            Configuración de Actividad
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td style="text-align: center;">
                                        Resumen / Características<br />
                                        <asp:TextBox ID="TextBox_Resumen" runat="server" TextMode="MultiLine" ValidationGroup="GUARDAR"
                                            Height="88px" Width="817px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Resumen"
                                ControlToValidate="TextBox_Resumen" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />RESUMEN / CARACTERISTICAS son requeridas."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Resumen"
                                TargetControlID="RequiredFieldValidator_TextBox_Resumen" HighlightCssClass="validatorCalloutHighlight" />

                            <div class="div_espaciador">
                            </div>

                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha Actividad:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FechaActividad" runat="server" ValidationGroup="GUARDAR"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaActividad" runat="server"
                                            TargetControlID="TextBox_FechaActividad" Format="dd/MM/yyyy" />
                                    </td>
                                    <td class="td_izq" style="padding-left:20px">
                                        Hora Inicio:
                                    </td>
                                    <td class="td_der">
                                        <ewc:TimePicker runat="server" ID="TimePicker_HoraInicioActividad" ControlDisplay="LabelButton"
                                            Scrollable="True" NumberOfColumns="4" PopupWidth="258px" PopupHeight="160px"
                                            MinuteInterval="15" SelectedTime="01:00 AM">
                                            <TimeStyle BackColor="White" ForeColor="#333333" />
                                            <SelectedTimeStyle BackColor="#cccccc" ForeColor="Black" />
                                        </ewc:TimePicker>
                                    </td>
                                    <td class="td_izq" style="padding-left:20px">
                                        Hora Fin:
                                    </td>
                                    <td class="td_der">
                                        <ewc:TimePicker runat="server" ID="TimePicker_HoraFinActividad" ControlDisplay="LabelButton"
                                            Scrollable="True" NumberOfColumns="4" PopupWidth="258px" PopupHeight="160px"
                                            MinuteInterval="15" SelectedTime="01:00 AM">
                                            <TimeStyle BackColor="White" ForeColor="#333333" />
                                            <SelectedTimeStyle BackColor="#cccccc" ForeColor="Black" />
                                        </ewc:TimePicker>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel_Reprogramacion" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Tipo Reprogramación:
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_TipoReprogramacion" runat="server" Width="400px" ValidationGroup="GUARDAR">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td style="text-align: center;">
                                            Motivo de la Reprogramación<br />
                                            <asp:TextBox ID="TextBox_MotivoReprogramacion" runat="server" TextMode="MultiLine"
                                                ValidationGroup="GUARDAR" Height="78px" Width="817px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div class="div_espaciador"></div>

                                <table style="width:800px; margin:2px 0px 2px auto;">
                                    <tr>
                                        <td class="td_izq" style="width:100%;">
                                            Archivo (Soporte Reprogramación):
                                        </td>
                                        <td style="text-align:right; padding:2px 30px 2px 2PX;">
                                            <asp:FileUpload ID="FileUpload_ArchivoReprogramacion" runat="server" />
                                        </td>
                                    </tr>
                                </table>


                                <%--DropDownList_TipoReprogramacion--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoReprogramacion"
                                    ControlToValidate="DropDownList_TipoReprogramacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TipoReprogramacion"
                                    TargetControlID="RequiredFieldValidator_DropDownList_TipoReprogramacion" HighlightCssClass="validatorCalloutHighlight" />

                                <%--TextBox_MotivoReprogramacion--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_MotivoReprogramacion"
                                    ControlToValidate="TextBox_MotivoReprogramacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO DE REPROGRAMACIÓN es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_MotivoReprogramacion"
                                    TargetControlID="RequiredFieldValidator_TextBox_MotivoReprogramacion" HighlightCssClass="validatorCalloutHighlight" />
                            </asp:Panel>

                            <%--TextBox_FechaActividad--%>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FechaActividad"
                                ControlToValidate="TextBox_FechaActividad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE LA ACTIVIDAD es requerida."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaActividad"
                                TargetControlID="RequiredFieldValidator_TextBox_FechaActividad" HighlightCssClass="validatorCalloutHighlight" />

                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Presupuesto Asignado:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_PresupuestoAsignado" runat="server" 
                                            ValidationGroup="GUARDAR" CssClass="money" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Personal Citado:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_PersonalCitado" runat="server" ValidationGroup="GUARDAR"
                                            Width="300px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PersonalCitado"
                                            runat="server" TargetControlID="TextBox_PersonalCitado" FilterType="Numbers" />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_PersonalCitadoMaximo" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Encargado:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_Encargado" runat="server" 
                                            ValidationGroup="GUARDAR" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <%--TextBox_PresupuestoAsignado--%>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoAsignado"
                                ControlToValidate="TextBox_PresupuestoAsignado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAsignado"
                                TargetControlID="RequiredFieldValidator_TextBox_PresupuestoAsignado" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RangeValidator ID="RangeValidator_TextBox_PresupuestoAsignado" runat="server" 
                                ControlToValidate="TextBox_PresupuestoAsignado" ValidationGroup="GUARDAR" Display="None" MinimumValue="0" MaximumValue="999999999999" Type="Currency"
                                ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El VALOR no puede supuerar del presupuesto asignado para el año."></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAsignado_1"
                                TargetControlID="RangeValidator_TextBox_PresupuestoAsignado" HighlightCssClass="validatorCalloutHighlight" />

                            <%--TextBox_PersonalCitado--%>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PersonalCitado"
                                ControlToValidate="TextBox_PersonalCitado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PERSONAL CITADO es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalCitado"
                                TargetControlID="RequiredFieldValidator_TextBox_PersonalCitado" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RangeValidator ID="RangeValidator_TextBox_PersonalCitado" runat="server"
                                ControlToValidate="TextBox_PersonalCitado" ValidationGroup="GUARDAR" Display="None"
                                MinimumValue="0" MaximumValue="9999999" Type="Integer" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El NÚMERO DE PERSONAL no puede supuerar el personal activo en la empresa."></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalCitado_1"
                                TargetControlID="RangeValidator_TextBox_PersonalCitado" HighlightCssClass="validatorCalloutHighlight" />

                            <%--DropDownList_Encargado--%>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Encargado"
                                ControlToValidate="DropDownList_Encargado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ENCARGADO DE LA ACTIVIDAD es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Encargado"
                                TargetControlID="RequiredFieldValidator_DropDownList_Encargado" HighlightCssClass="validatorCalloutHighlight" />
                            
                            <div class="div_espaciador">
                            </div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Regional
                                    </td>
                                    <td colspan="5" class="td_der">
                                        <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="300px" AutoPostBack="True"
                                            ValidationGroup="GUARDAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Departamento
                                    </td>
                                    <td colspan="5" class="td_der">
                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="300px" AutoPostBack="True"
                                            ValidationGroup="GUARDAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:200px">
                                        Ciudad
                                    </td>
                                    <td colspan="5" class="td_der">
                                        <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" Width="300px" ValidationGroup="GUARDAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_CIUDAD -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CIUDAD" ControlToValidate="DropDownList_CIUDAD"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD es requerida."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CIUDAD"
                                TargetControlID="RequiredFieldValidator_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_MOTIVO_CANCELACION" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Motivo Cancelación
            </div>
            <div class="div_contenido_groupbox">
                <div style="text-align: center;">
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq">
                                Motivo:
                            </td>
                            <td class="td_der">
                                <asp:DropDownList ID="DropDownList_MotivoCancelacion" runat="server" ValidationGroup="GUARDAR"
                                    Width="400px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <div class="div_espaciador"></div>

                    Descripción:<br />
                    <asp:TextBox ID="TextBox_Motivocancelacion" runat="server" TextMode="MultiLine" ValidationGroup="GUARDAR"
                        Height="70px" Width="771px"></asp:TextBox>

                    <asp:Panel ID="Panel_LinkArchivoCancelacion" runat="server">
                        <div class="div_espaciador"></div>
                        <table style="width: 800px; margin: 2px 0px 2px auto;">
                            <tr>
                                <td class="td_izq">
                                    Archivo (Soporte Cancelación):
                                </td>
                                <td style="text-align: right; padding: 2px 50px 2px 2PX; width:90px">
                                    <asp:HyperLink ID="HyperLink_ArchivoCancelacion" runat="server">Ver archivo.</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel_FileUploadArchivoCancelacion" runat="server">
                        <div class="div_espaciador"></div>
                        <table style="width: 800px; margin: 2px 0px 2px auto;">
                            <tr>
                                <td class="td_izq" style="width: 100%;">
                                    Archivo (Soporte Cancelación):
                                </td>
                                <td style="text-align: right; padding: 2px 50px 2px 2PX;">
                                    <asp:FileUpload ID="FileUpload_ArchivoCancelacion" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <!-- DropDownList_MotivoCancelacion -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_MotivoCancelacion"
                        ControlToValidate="DropDownList_MotivoCancelacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO es requerido."
                        ValidationGroup="GUARDAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_MotivoCancelacion"
                        TargetControlID="RequiredFieldValidator_DropDownList_MotivoCancelacion" HighlightCssClass="validatorCalloutHighlight" />

                    <!-- TextBox_Motivocancelacion -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Motivocancelacion"
                        ControlToValidate="TextBox_Motivocancelacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                        ValidationGroup="GUARDAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Motivocancelacion"
                        TargetControlID="RequiredFieldValidator_TextBox_Motivocancelacion" HighlightCssClass="validatorCalloutHighlight" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_AjustarPresupuesto" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Ajustar Presupuesto
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Nuevo Presupuesto Asignado a la Actividad:
                        </td>
                        <td class="td_der">
                            <asp:TextBox ID="TextBox_PresupuestoAjustado" runat="server" 
                                ValidationGroup="GUARDAR" CssClass="money" Width="200px"></asp:TextBox>
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_MaxPresupuestoAjsutar" runat="server" Text="0" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Documento que Certifica el Ajuste de Presupuesto:
                        </td>
                        <td colspan="2" class="td_der">
                            <asp:FileUpload ID="FileUpload_CertificacionAjuste" runat="server" />
                        </td>
                    </tr>
                </table>
                <%--TextBox_PresupuestoAjustado--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoAjustado" ControlToValidate="TextBox_PresupuestoAjustado"
                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El vloar para el NUEVO PRESUPUESTO es requerido."
                    ValidationGroup="GUARDAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAjustado"
                    TargetControlID="RequiredFieldValidator_TextBox_PresupuestoAjustado" HighlightCssClass="validatorCalloutHighlight" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_HistorialAjustesPresupuesto" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Historial de Ajustes a Presupuesto
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HistorialAjustesPresupuesto" runat="server" Width="885px"
                            AutoGenerateColumns="False" DataKeyNames="ID_HIST_AJUSTE,ID_DETALLE">
                            <Columns>
                                <asp:BoundField DataField="FCH_CRE" HeaderText="Fecha Ajuste" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USU_CRE" HeaderText="Usuario">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRESUPUESTO_APROBADO_ANT" DataFormatString="{0:C2}" 
                                    HeaderText="Presupuesto Anterior">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRESUPUESTO_APROBADO_NEW" DataFormatString="{0:C2}" 
                                    HeaderText="Presupuesto Ajustado">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Documento Adjunto">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_DocumentoAdjunto" runat="server">Ver Archivo</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:TemplateField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>       
            </div>
        </div> 
    </asp:Panel>

    <asp:Panel ID="Panel_HistorialReprogramaciones" runat="server">
        <div class="div_espaciador">
        </div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Historial Reprogramaciones
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HistorialReprogramaciones" runat="server" Width="885px"
                            AutoGenerateColumns="False" DataKeyNames="ID_HISTORIAL,ID_DETALLE">
                            <Columns>
                                <asp:BoundField DataField="FECHA_ACTIVIDAD_ANT" HeaderText="Fecha Anterior" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_INICIO_ANT" HeaderText="H. Inicio Anterior">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_FIN_ANT" HeaderText="H. Fin Anterior">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_ACTIVIDAD_NEW" HeaderText="Fecha Nueva" 
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_INICIO_NEW" HeaderText="H. Inicio Nueva">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_FIN_NEW" HeaderText="H. Fin Nueva">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_REPROGRAMACION" HeaderText="Tipo">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOTIVO_REPROGRAMACION" HeaderText="Motivo">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Documento Adjunto">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_DocumentoAdjunto" runat="server">Ver Archivo</asp:HyperLink>
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
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_ResultadosActividad" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Resultados Actividad
            </div>
            <div class="div_contenido_groupbox">

                <asp:Panel ID="Panel_ResultadosPresupuestoPersonal" runat="server">
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq">
                                Costo Actividad
                            </td>
                            <td class="td_der">
                                <asp:TextBox ID="TextBox_PresupuestoFinal" runat="server" ValidationGroup="GUARDAR" CssClass="money"></asp:TextBox>
                            </td>
                            <td class="td_izq">
                                Personal que Asistió:
                            </td>
                            <td class="td_der">
                                <asp:TextBox ID="TextBox_PersonalFinal" runat="server" ValidationGroup="GUARDAR"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PersonalFinal"
                                    runat="server" TargetControlID="TextBox_PersonalFinal" FilterType="Numbers" />
                            </td>
                            <td class="td_der">
                                <asp:Label ID="Label_PersonalAsistioMaximo" runat="server" Text="0" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <!-- TextBox_PresupuestoFinal -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoFinal"
                        ControlToValidate="TextBox_PresupuestoFinal" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO FINAL es requerido."
                        ValidationGroup="GUARDAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoFinal"
                        TargetControlID="RequiredFieldValidator_TextBox_PresupuestoFinal" HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RangeValidator ID="RangeValidator_TextBox_PresupuestoFinal" runat="server" ControlToValidate="TextBox_PresupuestoFinal"
                        ValidationGroup="GUARDAR" Display="None" MinimumValue="0" MaximumValue="999999999999"
                        Type="Currency" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El VALOR no puede supuerar del presupuesto asignado para el año."></asp:RangeValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoFinal_1"
                        TargetControlID="RangeValidator_TextBox_PresupuestoFinal" HighlightCssClass="validatorCalloutHighlight" />
                    
                    <!-- TextBox_PersonalFinal -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PersonalFinal"
                        ControlToValidate="TextBox_PersonalFinal" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO FINAL es requerido."
                        ValidationGroup="GUARDAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalFinal"
                        TargetControlID="RequiredFieldValidator_TextBox_PersonalFinal" HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RangeValidator ID="RangeValidator_TextBox_PersonalFinal" runat="server" ControlToValidate="TextBox_PersonalFinal"
                        ValidationGroup="GUARDAR" Display="None" MinimumValue="0" MaximumValue="9999999"
                        Type="Integer" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El NÚMERO DE PERSONAL no puede supuerar el personal activo en la empresa."></asp:RangeValidator>
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalFinal_1"
                        TargetControlID="RangeValidator_TextBox_PersonalFinal" HighlightCssClass="validatorCalloutHighlight" />

                </asp:Panel>

                <asp:Panel ID="Panel_ResultadosEncuesta" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        (1). Resultados Encuesta
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>


                                <asp:Panel ID="Panel_FONDO_MENSAJE_ENCUESTA" runat="server" Visible="false" Style="background-color: #999999;">
                                </asp:Panel>
                                <asp:Panel ID="Panel_MENSAJES_ENCUESTA" runat="server">
                                    <asp:Image ID="Image_MENSAJE_ENCUESTA_POPUP" runat="server" Width="50px" Height="50px"
                                        Style="margin: 5px auto 8px auto; display: block;" />
                                    <asp:Panel ID="Panel_COLOR_FONDO_ENCUESTA_POPUP" runat="server" Style="text-align: center">
                                        <asp:Label ID="Label_MENSAJE_ENCUESTA" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                                    </asp:Panel>
                                    <div style="text-align: center; margin-top: 15px;">
                                        <asp:Button ID="Button_CERRAR_MENSAJE_ENCUESTA" runat="server" Text="Cerrar" 
                                            Style="height: 26px" onclick="Button_CERRAR_MENSAJE_ENCUESTA_Click" />
                                    </div>
                                </asp:Panel>


                                <table class="table_control_registros" cellpadding="2" cellspacing="2">
                                    <tr style="background-color: #dddddd; border: 1px solid #cccccc;">
                                        <td>
                                            Área de calificación
                                        </td>
                                        <td>
                                            Buena
                                        </td>
                                        <td>
                                            Regular
                                        </td>
                                        <td>
                                            Mala
                                        </td>
                                        <td>
                                            % Buena
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der" style="width: 250px; background-color: #eeeeee;">
                                            Puntuación Total (Logística):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_LogisticaBuena" runat="server" Width="60px" Style="text-align: right;
                                                height: 22px;" MaxLength="3" OnTextChanged="TextBox_LogisticaBuena_TextChanged"
                                                AutoPostBack="True" ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LogisticaBuena"
                                                runat="server" TargetControlID="TextBox_LogisticaBuena" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_LogisticaRegular" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_LogisticaRegular_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LogisticaRegular"
                                                runat="server" TargetControlID="TextBox_LogisticaRegular" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_LogisticaMala" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_LogisticaMala_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LogisticaMala"
                                                runat="server" TargetControlID="TextBox_LogisticaMala" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_LogisticaPorcentajeBuena" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LogisticaPorcentajeBuena"
                                                runat="server" TargetControlID="TextBox_LogisticaPorcentajeBuena" FilterType="Numbers" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der" style="width: 250px; background-color: #eeeeee;">
                                            Puntuación Total (Instructor):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstructorBuena" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstructorBuena_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstructorBuena"
                                                runat="server" TargetControlID="TextBox_InstructorBuena" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstructorRegular" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstructorRegular_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_IstructorRegular"
                                                runat="server" TargetControlID="TextBox_InstructorRegular" 
                                                FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstructorMala" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstructorMala_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstructorMala"
                                                runat="server" TargetControlID="TextBox_InstructorMala" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstructorPorcentajeBuena" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstructorPorcentajeBuena"
                                                runat="server" TargetControlID="TextBox_InstructorPorcentajeBuena" FilterType="Numbers" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der" style="width: 250px; background-color: #eeeeee;">
                                            Puntuación Total (Instalaciones):
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstalacionesBuena" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstalacionesBuena_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstalacionesBuena"
                                                runat="server" TargetControlID="TextBox_InstalacionesBuena" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstalacionesRegular" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstalacionesRegular_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstalacionesRegular"
                                                runat="server" TargetControlID="TextBox_InstalacionesRegular" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstalacionesMala" runat="server" Width="60px" Style="text-align: right;"
                                                MaxLength="3" AutoPostBack="True" 
                                                OnTextChanged="TextBox_InstalacionesMala_TextChanged" 
                                                ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstalacionesMala"
                                                runat="server" TargetControlID="TextBox_InstalacionesMala" FilterType="Numbers" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_InstalacionesPorcentajeBuena" runat="server" 
                                                Width="60px" Style="text-align: right;"
                                                MaxLength="3" ValidationGroup="GUARDAR">0</asp:TextBox>
                                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_InstalacionesPorcentajeBuena"
                                                runat="server" TargetControlID="TextBox_InstalacionesPorcentajeBuena" FilterType="Numbers" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq" style="width: 250px; background-color: #cccccc;">
                                            Total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalBuena" runat="server" Width="60px" MaxLength="3" 
                                                Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalRegular" runat="server" Width="60px" MaxLength="3"
                                                Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalMala" runat="server" Width="60px" MaxLength="3" 
                                                Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq" style="width: 250px; background-color: #cccccc;">
                                            % Total:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalBenaPorcentaje" runat="server" Width="60px" 
                                                MaxLength="3" Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalRegularPorcentaje" runat="server" Width="60px" MaxLength="3"
                                                Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_TotalMalaPorcentaje" runat="server" Width="60px" 
                                                MaxLength="3" Style="text-align: right;" ValidationGroup="GUARDAR">0</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_LogisticaBuena -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_LogisticaBuena"
                                    ControlToValidate="TextBox_LogisticaBuena" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_LogisticaBuena"
                                    TargetControlID="RequiredFieldValidator_TextBox_LogisticaBuena" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_LogisticaRegular--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_LogisticaRegular"
                                    ControlToValidate="TextBox_LogisticaRegular" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_LogisticaRegular"
                                    TargetControlID="RequiredFieldValidator_TextBox_LogisticaRegular" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_LogisticaMala--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_LogisticaMala"
                                    ControlToValidate="TextBox_LogisticaMala" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_LogisticaMala"
                                    TargetControlID="RequiredFieldValidator_TextBox_LogisticaMala" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_InstructorBuena--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_InstructorBuena"
                                    ControlToValidate="TextBox_InstructorBuena" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_InstructorBuena"
                                    TargetControlID="RequiredFieldValidator_TextBox_InstructorBuena" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_IstructorRegular--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_IstructorRegular"
                                    ControlToValidate="TextBox_InstructorRegular" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_IstructorRegular"
                                    TargetControlID="RequiredFieldValidator_TextBox_IstructorRegular" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_InstructorMala--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_InstructorMala"
                                    ControlToValidate="TextBox_InstructorMala" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_InstructorMala"
                                    TargetControlID="RequiredFieldValidator_TextBox_InstructorMala" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_InstalacionesBuena--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_InstalacionesBuena"
                                    ControlToValidate="TextBox_InstalacionesBuena" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_InstalacionesBuena"
                                    TargetControlID="RequiredFieldValidator_TextBox_InstalacionesBuena" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_InstalacionesRegular--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_InstalacionesRegular"
                                    ControlToValidate="TextBox_InstalacionesRegular" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_InstalacionesRegular"
                                    TargetControlID="RequiredFieldValidator_TextBox_InstalacionesRegular" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_InstalacionesMala--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_InstalacionesMala"
                                    ControlToValidate="TextBox_InstalacionesMala" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El LA PUNTUACION es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_InstalacionesMala"
                                    TargetControlID="RequiredFieldValidator_TextBox_InstalacionesMala" HighlightCssClass="validatorCalloutHighlight" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Panel ID="Panel_ResultadosEncuestaLinkArchivo" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="margin: 0 10px 0 490px; text-align: right;">
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            Link de Descarga (Archivo de Soporte):
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ArchivoEncuesta" runat="server">Clic Aquí</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_ResultadoEncuestaUpload" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="margin: 0 10px 0 390px; text-align: right;">
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            Archivo de Soporte:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload_ArchivoEncuesta" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_ControlAsistencia" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        (1). Control de Asistencia
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div style="width:100%;">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td style="padding-right:10px;">
                                                            <asp:CheckBox ID="CheckBox_TodosEmpleados"  runat="server" Text="Seleccionar Todos" 
                                                                AutoPostBack="True" 
                                                                oncheckedchanged="CheckBox_TodosEmpleados_CheckedChanged"/>
                                                        </td>
                                                        <td>
                                                            Filtro:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox_FiltroEmpleados" runat="server" Width="360px" ValidationGroup="FILTROEMPEADOS"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button_FiltrarEmpleados" runat="server" Text="Filtrar" 
                                                                onclick="Button_FiltrarEmpleados_Click"  ValidationGroup="FILTROEMPLEADOS"/>
                                                        </td>
                                                        <td style="padding-left:10px;">
                                                            <asp:CheckBox ID="CheckBox_MostrarSoloSeleccionados"  runat="server" Text="Mostrar Solo Seleccionados" 
                                                                AutoPostBack="True" 
                                                                oncheckedchanged="CheckBox_MostrarSoloSeleccionados_CheckedChanged"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--TextBox_FiltroEmpleados--%>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FiltroEmpleados" ControlToValidate="TextBox_FiltroEmpleados"
                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Digite un valor para filtrar."
                                                    ValidationGroup="FILTROEMPLEADOS" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FiltroEmpleados"
                                                    TargetControlID="RequiredFieldValidator_TextBox_FiltroEmpleados" HighlightCssClass="validatorCalloutHighlight" />
                                            </div>

                                            <asp:HiddenField ID="HiddenField_LETRA_PAGINACION_LISTA" runat="server" />

                                            <table cellspacing="5" cellpadding="2" class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_a" runat="server" OnClick="LinkButton_a_Click">A</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_b" runat="server" OnClick="LinkButton_b_Click">B</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_c" runat="server" OnClick="LinkButton_c_Click">C</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton3_d" runat="server" OnClick="LinkButton3_d_Click">D</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_e" runat="server" OnClick="LinkButton_e_Click">E</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_f" runat="server" OnClick="LinkButton_f_Click">F</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_g" runat="server" OnClick="LinkButton_g_Click">G</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_h" runat="server" OnClick="LinkButton_h_Click">H</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_i" runat="server" OnClick="LinkButton_i_Click">I</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_j" runat="server" OnClick="LinkButton_j_Click">J</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_k" runat="server" OnClick="LinkButton_k_Click">K</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_l" runat="server" OnClick="LinkButton_l_Click">L</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_m" runat="server" OnClick="LinkButton_m_Click">M</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_n" runat="server" OnClick="LinkButton_n_Click">N</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_o" runat="server" OnClick="LinkButton_o_Click">O</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_p" runat="server" OnClick="LinkButton_p_Click">P</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_q" runat="server" OnClick="LinkButton_q_Click">Q</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_r" runat="server" OnClick="LinkButton_r_Click">R</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_s" runat="server" OnClick="LinkButton_s_Click">S</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_t" runat="server" OnClick="LinkButton_t_Click">T</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_u" runat="server" OnClick="LinkButton_u_Click">U</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_v" runat="server" OnClick="LinkButton_v_Click">V</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_w" runat="server" OnClick="LinkButton_w_Click">W</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_x" runat="server" OnClick="LinkButton_x_Click">X</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_y" runat="server" OnClick="LinkButton_y_Click">Y</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton_z" runat="server" OnClick="LinkButton_z_Click">Z</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>


                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ControlAsistencia" runat="server" Width="800px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_EMPLEADO,ID_SOLICITUD">
                                                        <Columns>
                                                            <asp:BoundField DataField="NOMBRES_EMPLEADO" HeaderText="Nombres Empleado" 
                                                                HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NUMERO_IDENTIFICACION" 
                                                                HeaderText="Doc. Identificación" HtmlEncode="False" 
                                                                HtmlEncodeFormatString="False">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARGO" HeaderText="Cargo" HtmlEncode="False" 
                                                                HtmlEncodeFormatString="False" />
                                                            <asp:TemplateField HeaderText="Asistencia">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox_Asistencia" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_Asistencia_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="50px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; padding-right: 10px">
                                            Trabajadores Seleccionados:
                                            <asp:Label ID="Label_trabajadoresSeleciconados" runat="server" Text="000" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Panel ID="Panel_linkArchivoAsistencia" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="margin: 0 10px 0 490px; text-align: right;">
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            Link de Descarga (Archivo de Soporte):
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ArchivoAsistencia" runat="server">Clic Aquí</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_UploadArchivoAsistencia" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="margin: 0 10px 0 390px; text-align: right;">
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            Archivo de Soporte:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload_ArchivoAsistencia" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_EntidadesColaboradoras" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        (4). Entidades Colaboradoras
                    </div>
                    <div class="div_contenido_groupbox_gris">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="HiddenField_FILA_GRILLA_SELECCIONADA" runat="server" />
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView_EntidadesColaboradoras" runat="server" Width="810px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_ENTIDAD_COLABORA,ID_ENTIDAD" OnRowCommand="GridView_EntidadesColaboradoras_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                Text="Eliminar">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:TemplateField HeaderText="Entidad">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_EntidadesColoboradoras" runat="server" ValidationGroup="ENTIDAD"
                                                                        Width="250px">
                                                                    </asp:DropDownList>
                                                                    <%--DropDownList_EntidadesColoboradoras--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_EntidadesColoboradoras"
                                                                        runat="server" ControlToValidate="DropDownList_EntidadesColoboradoras" Display="None"
                                                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La ENTIDAD es requerida."
                                                                        ValidationGroup="ENTIDAD" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EntidadesColoboradoras"
                                                                        TargetControlID="RequiredFieldValidator_DropDownList_EntidadesColoboradoras" HighlightCssClass="validatorCalloutHighlight" />

                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox_DescripcionEntidad" runat="server" Height="50px" TextMode="MultiLine"
                                                                        ValidationGroup="ENTIDAD" Width="500px"></asp:TextBox>
                                                                    <%--TextBox_DescripcionEntidad--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_DescripcionEntidad"
                                                                        runat="server" ControlToValidate="TextBox_DescripcionEntidad" Display="None"
                                                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La DESCRIPCIÓN es requerida."
                                                                        ValidationGroup="ENTIDAD" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DescripcionEntidad"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_DescripcionEntidad" HighlightCssClass="validatorCalloutHighlight" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel_BotonesAccionEntidades" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_NuevaEntidadColaboradora" runat="server" 
                                                    Text="Nueva Entidad" ValidationGroup="NUEVAENTIDAD"
                                                    CssClass="margin_botones" 
                                                    OnClick="Button_NuevaEntidadColaboradora_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_GuardarEntidad" runat="server" Text="Guardar Entidad" ValidationGroup="ENTIDAD"
                                                    CssClass="margin_botones" OnClick="Button_GuardarEntidad_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_CancelarEntidad" runat="server" Text="Cancear Entidad" ValidationGroup="CANCELARENTIDAD"
                                                    CssClass="margin_botones" OnClick="Button_CancelarEntidad_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_Compromisos" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        (5). Compromisos
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        


                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:HiddenField ID="HiddenField_FILA_GRILLA_SELECCIONADA_COMPROMISO" runat="server" />
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA_COMPROMISO" runat="server" />

                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView_Compromisos" runat="server" Width="855px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_MAESTRA_COMPROMISO" 
                                                        onrowcommand="GridView_Compromisos_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                Text="Eliminar">
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="25px"/>
                                                            </asp:ButtonField>
                                                            <asp:TemplateField HeaderText="Compromiso">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Compromiso" runat="server" ValidationGroup="COMPROMISO" Width="160px"></asp:TextBox>
                                                                    <%--TextBox_Compromiso--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Compromiso"
                                                                        runat="server" ControlToValidate="TextBox_Compromiso" Display="None"
                                                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El COMPROMISO es requerido."
                                                                        ValidationGroup="COMPROMISO" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Compromiso"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_Compromiso" HighlightCssClass="validatorCalloutHighlight" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Responsable">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_ResponsableCompromiso" runat="server" ValidationGroup="COMPROMISO" Width="160px">
                                                                    </asp:DropDownList>
                                                                    <%--DropDownList_ResponsableCompromiso--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ResponsableCompromiso"
                                                                        runat="server" ControlToValidate="DropDownList_ResponsableCompromiso" Display="None"
                                                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El RESPONSABLE es requerido."
                                                                        ValidationGroup="COMPROMISO" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ResponsableCompromiso"
                                                                        TargetControlID="RequiredFieldValidator_DropDownList_ResponsableCompromiso" HighlightCssClass="validatorCalloutHighlight" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fecha">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox_FechaCompromiso" runat="server" ValidationGroup="COMPROMISO" Width="110px"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FechaCompromiso"
                                                                        runat="server" TargetControlID="TextBox_FechaCompromiso" FilterType="Custom,Numbers"
                                                                        ValidChars="/" >
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaCompromiso" runat="server"
                                                                        TargetControlID="TextBox_FechaCompromiso" Format="dd/MM/yyyy" />
                                                                    <%--TextBox_FechaCompromiso--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FechaCompromiso" runat="server"
                                                                        ControlToValidate="TextBox_FechaCompromiso" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La FECHA es requerida."
                                                                        ValidationGroup="COMPROMISO" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaCompromiso"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_FechaCompromiso" HighlightCssClass="validatorCalloutHighlight" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observaciones">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox_ObservacionesCompromiso" runat="server" Height="50px" TextMode="MultiLine"
                                                                        ValidationGroup="COMPROMISO" Width="250px"></asp:TextBox>
                                                                    <%--TextBox_ObservacionesCompromiso--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_ObservacionesCompromiso"
                                                                        runat="server" ControlToValidate="TextBox_ObservacionesCompromiso" Display="None"
                                                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;Las OBSERVACIONES son requeridas."
                                                                        ValidationGroup="COMPROMISO" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ObservacionesCompromiso"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_ObservacionesCompromiso" HighlightCssClass="validatorCalloutHighlight" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estado">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_EstadoCompromiso" runat="server" Text="Estado"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="65px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel_BotonesAccionCompromisos" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_NuevoCompromiso" runat="server" Text="Nuevo Compromiso" ValidationGroup="NUEVOCOMPROMISO"
                                                    CssClass="margin_botones" onclick="Button_NuevoCompromiso_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_GuardarCompromiso" runat="server" 
                                                    Text="Guardar Compromiso" ValidationGroup="COMPROMISO"
                                                    CssClass="margin_botones" onclick="Button_GuardarCompromiso_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_CancelarCompromiso" runat="server" 
                                                    Text="Cancear Compromiso" ValidationGroup="CANCELARCOMPROMISO"
                                                    CssClass="margin_botones" onclick="Button_CancelarCompromiso_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                    </div>
                </asp:Panel>

               
                <asp:Panel ID="Panel_ImagenYConclusiones" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        (6). Imagen Representativa y Conclusiones
                    </div>
                    <div class="div_contenido_groupbox_gris">

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Image ID="Image_Representativa" runat="server" />
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="Panel_SubirArchivoImagenRepresentativa" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Seleccione una imagen que represente los resultados:
                                    </td>
                                    <td class="td_der">
                                        <asp:FileUpload ID="FileUpload_ImagenRepresentativa" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros" width="800">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_InformacionImagenRepresentativa" runat="server" 
                                            Text="En este campo solo se permite adjuntar IMAGENES (JPG, GIF, PNG, BMP), Cualquier otro tipo de archivo como documentos de word, excel, PDFs deben ser adjuntados en la sección de ADJUNTOS al finalizar el proceso de registro de resultados." 
                                            Font-Bold="True" Font-Size="9pt" ForeColor="#CC0000"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td style="text-align:center;">
                                    Conclusiones<br />
                                    <asp:TextBox ID="TextBox_ConclusionesActividad" runat="server" 
                                        ValidationGroup="GUARDAR" TextMode="MultiLine" Height="82px" Width="841px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <%--TextBox_ConclusionesActividad--%>
                        <asp:RequiredFieldValidator runat="server" 
                            ID="RequiredFieldValidator_TextBox_ConclusionesActividad" ControlToValidate="TextBox_ConclusionesActividad"
                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las CONCLUSIONES son requeridas."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ConclusionesActividad"
                            TargetControlID="RequiredFieldValidator_TextBox_ConclusionesActividad" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </asp:Panel>

            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_AdjuntarArchivos" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox_gris">
                Adjuntos
            </div>
            <div class="div_contenido_groupbox_gris">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_AdjuntosInforme" runat="server" Width="776px" AutoGenerateColumns="False"
                                                DataKeyNames="id_adjunto">
                                                <Columns>
                                                    <asp:BoundField DataField="FCH_CRE" HeaderText="Fecha registro" DataFormatString="{0:dd/MM/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TITULO" HeaderText="Título" />
                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Archivo">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink_ARCHIVO_ADJUNTO" runat="server">Ver archivo</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="Panel_NuevoAdjunto" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Nuevo Adjunto
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Titulo:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_TituloAdjunto" runat="server" Width="550px" ValidationGroup="ADJUNTARARCHIVO"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            Descripción:<br />
                                            <asp:TextBox ID="TextBox_DescripcionAdjunto" runat="server" Width="600px" ValidationGroup="ADJUNTARARCHIVO"
                                                Height="100px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <%--TextBox_TituloAdjunto--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_TituloAdjunto"
                                    ControlToValidate="TextBox_TituloAdjunto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TITULO es requerido."
                                    ValidationGroup="ADJUNTARARCHIVO" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_TituloAdjunto"
                                    TargetControlID="RequiredFieldValidator_TextBox_TituloAdjunto" HighlightCssClass="validatorCalloutHighlight" />
                                <%--TextBox_DescripcionAdjunto--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DescripcionAdjunto"
                                    ControlToValidate="TextBox_DescripcionAdjunto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                    ValidationGroup="ADJUNTARARCHIVO" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DescripcionAdjunto"
                                    TargetControlID="RequiredFieldValidator_TextBox_DescripcionAdjunto" HighlightCssClass="validatorCalloutHighlight" />
                            </div>

                            <div class="div_espaciador">
                            </div>

                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Seleccione el Archivo:
                                    </td>
                                    <td class="td_der">
                                        <asp:FileUpload ID="FileUpload_AdjuntosInforme" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <div class="div_espaciador">
                        </div>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NuevoAdjunto" runat="server" Text="Nuevo Adjunto" 
                                        ValidationGroup="NUEVOADJUNTO" onclick="Button_NuevoAdjunto_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_GuardarAdjunto" runat="server" Text="Adjuntar" 
                                        ValidationGroup="ADJUNTARARCHIVO" onclick="Button_GuardarAdjunto_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_CancelarAdjunto" runat="server" Text="Cancelar" 
                                        ValidationGroup="CANCELARADJUNTO" onclick="Button_CancelarAdjunto_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button_GuardarAdjunto" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_BOTONES_ACCION_PIE" runat="server">
        <div class="div_espaciador">
        </div>
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
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                        ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICARFINAL_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICARFINAL" onclick="Button_MODIFICARFINAL_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_REPROGRAMAR_1" runat="server" Text="Reprogramar" CssClass="margin_botones"
                                        ValidationGroup="REPROGRAMAR" OnClick="Button_REPROGRAMAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_AJUSTARPRESUPUESTO_1" runat="server" 
                                        Text="Ajustar Presupuesto" CssClass="margin_botones"
                                        ValidationGroup="AJUSTARPRES" onclick="Button_AJUSTARPRESUPUESTO_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_RESULTADOS_1" runat="server" Text="Resultados" CssClass="margin_botones"
                                        ValidationGroup="RESULTADOS" OnClick="Button_ResultadosActividad_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_ACTIVIDAD_1" runat="server" Text="Cancelar Actividad"
                                        CssClass="margin_botones" ValidationGroup="CANCELARACTIVIDAD" OnClick="Button_CANCELAR_ACTIVIDAD_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR_1" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
