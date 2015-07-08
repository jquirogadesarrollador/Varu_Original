<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="Compromisos.aspx.cs" Inherits="_Compromisos" %>

<%@ Register TagPrefix="ewc" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            
            <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_MAESTRA_COMPROMISO" runat="server" />

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
                                            <asp:Button ID="Button_CrearActividad" runat="server" Text="Generar Actividad"
                                                CssClass="margin_botones" ValidationGroup="GENERARACTIVIDAD" 
                                                onclick="Button_CrearActividad_Click"  />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Finalizar" runat="server" Text="Finalizar"
                                                CssClass="margin_botones" ValidationGroup="FINALIZAR" 
                                                onclick="Button_Finalizar_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Volver" runat="server" Text="Volver al Menú" CssClass="margin_botones"
                                                ValidationGroup="VOLVER" onclick="Button_Volver_Click" />
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


            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Compromisos Pendientes
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_para_convencion_hoja_trabajo">
                            <table class="tabla_alineada_derecha">
                                <tr>
                                    <td>
                                        Faltan 10 o más días
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_verde">
                                        </div>
                                    </td>
                                    <td>
                                        Faltan entre 5 a 9 días
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_amarillo">
                                        </div>
                                    </td>
                                    <td>
                                        Vencidos ó faltan entre 1 a 4 días
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_rojo">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_Compromisos" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_MAESTRA_COMPROMISO,ALERTA" 
                                    onprerender="GridView_Compromisos_PreRender" 
                                    onrowcommand="GridView_Compromisos_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COMPROMISO" HeaderText="Compromiso" >
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_LOG_RESPONSABLE" HeaderText="Responsable" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_P" HeaderText="Fecha" 
                                            DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_CompromioSeleccionado" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Compromiso Seleccionado
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_cabeza_groupbox_gris">
                            Datos Básicos
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq" style="font-weight: bold;">
                                        Origen
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_TipoGenera" runat="server" Text="Tipo Genera"></asp:Label>
                                    </td>
                                    <td class="td_izq" style="font-weight: bold;">
                                        Descripción Origen:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_DescripcionOrigen" runat="server" 
                                            Text="Descripción de Origen"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros" style="width: 95%">
                                <tr>
                                    <td class="td_izq" style="font-weight: bold;">
                                        Fecha:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_FechaP" runat="server" Text="00/00/0000"></asp:Label>
                                    </td>
                                    <td class="td_izq" style="font-weight: bold;">
                                        Compromiso:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_Compromiso" runat="server" Text="Titulo del compromiso"></asp:Label>
                                    </td>
                                    <td class="td_izq" style="font-weight: bold;">
                                        Responsable:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_Responsable" runat="server" Text="Titulo del compromiso"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros" style="width: 90%">
                                <tr>
                                    <td style="text-align: left; font-weight: bold;">
                                        Observaciones:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: justify;">
                                        <asp:Label ID="Label_Observaciones" runat="server" Text="Observaciones del compromiso"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <table style="width: 500px; margin: 0 0 0 auto;">
                                <tr>
                                    <td class="td_izq" style="padding-right: 20px;">
                                        <b>Estado:</b>
                                        <asp:Label ID="Label_Estado" runat="server" Text="Estado del Compromiso"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <asp:Panel ID="Panel_SeguimientoCompromiso" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Seguimiento
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <div class="div_contenedor_grilla_resultados">
                                                        <div class="grid_seleccionar_registros">
                                                            <asp:GridView ID="GridView_AdjuntosInforme" runat="server" Width="862px" AutoGenerateColumns="False"
                                                                DataKeyNames="ID_SEGUIMIENTO,ID_MAESTRA_COMROMISO">
                                                                <Columns>
                                                                    <asp:BoundField DataField="FCH_CRE" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SEGUIMIENTO" HeaderText="Seguimineto" >
                                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                                    </asp:BoundField>
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
                                                Nuevo Seguimiento
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td>
                                                            Fecha:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label_FechaNuevoSeguimiento" runat="server" Text="Fecha"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <div class="div_espaciador"></div>

                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Seguimiento:
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_SeguimientoNuevo" runat="server" Width="550px" ValidationGroup="ADJUNTARSEG"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center">
                                                            Descripción:<br />
                                                            <asp:TextBox ID="TextBox_DescripcionAdjunto" runat="server" Width="650px" ValidationGroup="ADJUNTARSEG"
                                                                Height="100px" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--TextBox_SeguimientoNuevo--%>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SeguimientoNuevo"
                                                    ControlToValidate="TextBox_SeguimientoNuevo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SEGUIMIENTO es requerido."
                                                    ValidationGroup="ADJUNTARSEG" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SeguimientoNuevo"
                                                    TargetControlID="RequiredFieldValidator_TextBox_SeguimientoNuevo" HighlightCssClass="validatorCalloutHighlight" />
                                                <%--TextBox_DescripcionAdjunto--%>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DescripcionAdjunto"
                                                    ControlToValidate="TextBox_DescripcionAdjunto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                                    ValidationGroup="ADJUNTARSEG" />
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
                                                        <asp:FileUpload ID="FileUpload_Adjunto" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                        <div class="div_espaciador">
                                        </div>
                                        
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button_NuevoAdjunto" runat="server" Text="Nuevo Seguimiento" 
                                                        ValidationGroup="NUEVOSEG" onclick="Button_NuevoAdjunto_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_GuardarAdjunto" runat="server" 
                                                        Text="Registrar Seguimiento" ValidationGroup="ADJUNTARSEG" 
                                                        onclick="Button_GuardarAdjunto_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_CancelarAdjunto" runat="server" Text="Cancelar" 
                                                        ValidationGroup="CANCELARSEG" onclick="Button_CancelarAdjunto_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_FinalizarSeguimiento" runat="server" Text="Finalizar Actulizacion" 
                                                        ValidationGroup="FINALIZARSEG" 
                                                        onclick="Button_FinalizarSeguimiento_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Button_GuardarAdjunto" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_FinalizarCompromiso" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Finalizar Compromiso
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha Finalización:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FechaFinalizacion" runat="server" ValidationGroup="GUARDAR"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaFinalizacion" runat="server"
                                                TargetControlID="TextBox_FechaFinalizacion" Format="dd/MM/yyyy" />
                                        </td>
                                        <td class="td_izq" style="padding-left: 20px">
                                            Hora Finalización:
                                        </td>
                                        <td class="td_der">
                                            <ewc:TimePicker runat="server" ID="TimePicker_HoraFinalizacion" ControlDisplay="LabelButton"
                                                Scrollable="True" NumberOfColumns="4" PopupWidth="258px" PopupHeight="160px"
                                                MinuteInterval="FifteenMinutes" SelectedTime="01:00 AM" 
                                                PostedTime="08:00 a.m.">
                                                <TimeStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedTimeStyle BackColor="#cccccc" ForeColor="Black" />
                                            </ewc:TimePicker>
                                        </td>
                                    </tr>
                                </table>

                                <%--TextBox_FechaFinalizacion--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FechaFinalizacion" ControlToValidate="TextBox_FechaFinalizacion"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaFinalizacion"
                                    TargetControlID="RequiredFieldValidator_TextBox_FechaFinalizacion" HighlightCssClass="validatorCalloutHighlight" />

                                <div class="div_espaciador"></div>

                                <table class="table_control_registros">
                                    <tr>
                                        <td style="text-align: center">
                                            Descripción:<br />
                                            <asp:TextBox ID="TextBox_DescripcionFinalizacion" runat="server" Width="650px" ValidationGroup="GUARDAR"
                                                Height="100px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <%--TextBox_DescripcionFinalizacion--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DescripcionFinalizacion" ControlToValidate="TextBox_DescripcionFinalizacion"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DescripcionFinalizacion"
                                    TargetControlID="RequiredFieldValidator_TextBox_DescripcionFinalizacion" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_InfoActividad" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Registro Nueva Actividad
                            </div>
                            <div class="div_contenido_groupbox_grid">
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
                                                <asp:DropDownList ID="DropDownList_SubPrograma" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="500px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SubPrograma_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SubPrograma"
                                        ControlToValidate="DropDownList_SubPrograma" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />eL subprograma es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SubPrograma"
                                        TargetControlID="RequiredFieldValidator_DropDownList_SubPrograma" HighlightCssClass="validatorCalloutHighlight" />
                                </div>

                                <div class="div_espaciador">
                                </div>

                                <div class="div_cabeza_groupbox_gris">
                                    Actividad
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Nombre Actividad:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_IdActividad" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="550px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_IdActividad_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_IdActividad"
                                        ControlToValidate="DropDownList_IdActividad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ACTIVIDAD es requerida."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_IdActividad"
                                        TargetControlID="RequiredFieldValidator_DropDownList_IdActividad" HighlightCssClass="validatorCalloutHighlight" />

                                    <div class="div_espaciador">
                                    </div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_der">
                                                Tipo:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_Tipo" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="180px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_izq">
                                                Sector:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_Sector" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Estado:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_EstadoActividad" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td style="text-align: center;">
                                                Descripción
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:TextBox ID="TextBox_DescripcionActividad" runat="server" TextMode="MultiLine"
                                                    Width="817px" Height="78px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Configuración Actividad
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td style="text-align: center;">
                                                Resumen / Características<br />
                                                <asp:TextBox ID="TextBox_Resumen" runat="server" TextMode="MultiLine" ValidationGroup="ADCIONAR"
                                                    Height="88px" Width="817px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Resumen"
                                        ControlToValidate="TextBox_Resumen" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />RESUMEN / CARACTERISTICAS son requeridas."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Resumen"
                                        TargetControlID="RequiredFieldValidator_TextBox_Resumen" HighlightCssClass="validatorCalloutHighlight" />
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq" style="width: 180px">
                                                Fecha Actividad:
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_FechaActividad" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="300px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaActividad" runat="server"
                                                    TargetControlID="TextBox_FechaActividad" Format="dd/MM/yyyy" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq" style="padding-left: 20px">
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
                                        </tr>
                                        <tr>
                                            <td class="td_izq" style="padding-left: 20px">
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
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorTextBox_FechaActividad"
                                        ControlToValidate="TextBox_FechaActividad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE LA ACTIVIDAD es requerida."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                        TargetControlID="RequiredFieldValidatorTextBox_FechaActividad" HighlightCssClass="validatorCalloutHighlight" />
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq" style="width: 180px;">
                                                Presupuesto Asignado:
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_PresupuestoAsignado" runat="server" ValidationGroup="ADICIONAR"
                                                    CssClass="money" Width="300px"></asp:TextBox>
                                            </td>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_PresupuestoMax" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Personal Citado:
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_PersonalCitado" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="300px" MaxLength="4"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PersonalCitado"
                                                    runat="server" TargetControlID="TextBox_PersonalCitado" FilterType="Numbers" />
                                            </td>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_PersonalCitadoMaximo" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Encargado:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_Encargado" runat="server" ValidationGroup="ADICIONAR"
                                                    Width="295PX">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--TextBox_PresupuestoAsignado--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoAsignado"
                                        ControlToValidate="TextBox_PresupuestoAsignado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAsignado"
                                        TargetControlID="RequiredFieldValidator_TextBox_PresupuestoAsignado" HighlightCssClass="validatorCalloutHighlight" />
                                    <asp:RangeValidator ID="RangeValidator_TextBox_PresupuestoAsignado" runat="server"
                                        ControlToValidate="TextBox_PresupuestoAsignado" ValidationGroup="ADICIONAR" Display="None"
                                        MinimumValue="0" MaximumValue="999999999999,99" Type="Currency" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El VALOR no puede supuerar del presupuesto asignado para el año."></asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAsignado_1"
                                        TargetControlID="RangeValidator_TextBox_PresupuestoAsignado" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--TextBox_PersonalCitado--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PersonalCitado"
                                        ControlToValidate="TextBox_PersonalCitado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PERSONAL CITADO es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalCitado"
                                        TargetControlID="RequiredFieldValidator_TextBox_PersonalCitado" HighlightCssClass="validatorCalloutHighlight" />
                                    <asp:RangeValidator ID="RangeValidator_TextBox_PersonalCitado" runat="server" ControlToValidate="TextBox_PersonalCitado"
                                        ValidationGroup="ADICIONAR" Display="None" MinimumValue="0" MaximumValue="9999999"
                                        Type="Integer" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El NÚMERO DE PERSONAL no puede supuerar el personal activo en la empresa."></asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PersonalCitado_1"
                                        TargetControlID="RangeValidator_TextBox_PersonalCitado" HighlightCssClass="validatorCalloutHighlight" />
                                    <%-- DropDownList_Encargado--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Encargado"
                                        ControlToValidate="DropDownList_Encargado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El Encargado de la actividad es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Encargado"
                                        TargetControlID="RequiredFieldValidator_DropDownList_Encargado" HighlightCssClass="validatorCalloutHighlight" />
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq" style="width: 180px;">
                                                Regional
                                            </td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="295px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownList_REGIONAL_SelectedIndexChanged" ValidationGroup="ADICIONAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Departamento
                                            </td>
                                            <td colspan="5" class="td_der">
                                                <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="295px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_SelectedIndexChanged" ValidationGroup="ADICIONAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Ciudad
                                            </td>
                                            <td colspan="5" class="td_der">
                                                <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" Width="295px" ValidationGroup="ADICIONAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_CIUDAD -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CIUDAD" ControlToValidate="DropDownList_CIUDAD"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD es requerida."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CIUDAD"
                                        TargetControlID="RequiredFieldValidator_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />
                                </div>

                                <asp:Panel ID="Panel_BotonesActividad" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_GAURDAR_ACTIVIDAD" runat="server" Text="Guardar" ValidationGroup="ADICIONAR"
                                                    CssClass="margin_botones" OnClick="Button_GAURDAR_ACTIVIDAD_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_CANCELAR_ACTIVIDAD" runat="server" Text="Cancelar" ValidationGroup="CANCELAR"
                                                    CssClass="margin_botones" OnClick="Button_CANCELAR_ACTIVIDAD_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </asp:Panel>












                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BOTONES_PIE" runat="server">
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
                                            <asp:Button ID="Button_Guardar_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Actualizar_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CrearActividad_1" runat="server" Text="Generar Actividad"
                                                CssClass="margin_botones" ValidationGroup="GENERARACTIVIDAD" 
                                                onclick="Button_CrearActividad_Click"  />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Finalizar_1" runat="server" Text="Finalizar Compromiso"
                                                CssClass="margin_botones" ValidationGroup="FINALIZAR" 
                                                onclick="Button_Finalizar_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Cancelar_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_vOLVER_1" runat="server" Text="Volver al Menú" CssClass="margin_botones"
                                                ValidationGroup="VOLVER" onclick="Button_Volver_Click" />
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
