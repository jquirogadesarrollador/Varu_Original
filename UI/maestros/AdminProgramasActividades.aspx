<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="AdminProgramasActividades.aspx.cs" Inherits="_AdminProgramasActividades" %>

<%@ Register TagPrefix="ewc" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>
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
            <asp:HiddenField ID="HiddenField_ID_PROGRAMA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PROGRAMA_GENERAL" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PRESUPUESTO" runat="server" />
            <asp:HiddenField ID="HiddenField_ANNO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
            <asp:HiddenField ID="HiddenField_FORM_SOLO_LECTURA" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_NODO_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_NODO_SELECCIONADO" runat="server" />

            <asp:HiddenField ID="HiddenField_PRESUPUESTO_TOTAL" runat="server" />
            <asp:HiddenField ID="HiddenField_ASIGNADO" runat="server" />
            <asp:HiddenField ID="HiddenField_EJECUTADO" runat="server" />

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

            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_NUEVAACTIVIDAD" runat="server" Text="Nueva Actividad" CssClass="margin_botones"
                                                ValidationGroup="NUEVAACTIVIDAD" OnClick="Button_NUEVAACTIVIDAD_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_Imprimir" runat="server" Text="Imprimir Programa" 
                                                ValidationGroup="IMPRIMIR" onclick="Button_Imprimir_Click" CssClass="margin_botones"/>
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" type="button"
                                                value="Salir" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Sección de Busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                ValidationGroup="BUSCAR_CLIENTE" CssClass="margin_botones" />
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
                                ValidChars=" -_.,:;ÑñáéíóúÁÉÍÓÚ" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel_ResultadosBusquedaEmpresas" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Registros Encontrados
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_EmpresasEncontradas" runat="server" Width="880px" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="ID" 
                                    OnPageIndexChanging="GridView_EmpresasEncontradas_PageIndexChanging" 
                                    onrowcommand="GridView_EmpresasEncontradas_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                            ImageUrl="~/imagenes/areas/view2.gif" Text="Seleccionar">
                                        <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Raz. social" HeaderText="Raz. social" />
                                        <asp:BoundField DataField="NIT" HeaderText="NIT">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tel 1" HeaderText="Tel 1">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Celular" HeaderText="Celular">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="Activo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Código" HeaderText="Código">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_PRESUPUESTO,ANNO,ID_PROGRAMA,ID_PROGRAMA_GENERAL,PRESUPUESTO,ASIGNADO,EJECUTADO" AllowPaging="True"
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                            Text="seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ANNO" HeaderText="Año">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRESUPUESTO" HeaderText="Presupuesto Total" 
                                            DataFormatString="{0:C2}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ASIGNADO" DataFormatString="{0:C2}" 
                                            HeaderText="Asignado">
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EJECUTADO" DataFormatString="{0:C2}" 
                                            HeaderText="Ejecutado">
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CON_PROGRAMA" HeaderText="Estado">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_ArbolPrograma" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Esquema del Programa
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <div class="div_para_convencion_hoja_trabajo">
                                        <table class="tabla_alineada_derecha">
                                            <tr>
                                                <td>
                                                    Actividad Aprobada
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
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_EsquemaProgramaEspecifico" runat="server" Width="875px"
                                                AutoGenerateColumns="False" DataKeyNames="ID_DETALLE,ID_ACTIVIDAD,ID_ESTADO">
                                                <Columns>
                                                    <asp:BoundField DataField="REGIONAL" HeaderText="Regional" />
                                                    <asp:BoundField DataField="CIUDAD" HeaderText="Ciudad" />
                                                    <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                                                    <asp:BoundField DataField="ACTIVIDAD" HeaderText="Actividad" />
                                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PRESUPUESTO" HeaderText="Presupuesto" DataFormatString="{0:C2}">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>






            <asp:Panel ID="Panel_InfoActividad" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información de la Actividad
                    </div>
                    <div class="div_contenido_groupbox">
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
                                    <td class="td_izq" style="width:180px">
                                        Fecha Actividad:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FechaActividad" runat="server" 
                                            ValidationGroup="ADICIONAR" Width="300px"></asp:TextBox>
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
                                    <td class="td_izq" style="width:180px;">
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
                                        <asp:DropDownList ID="DropDownList_Encargado" runat="server" ValidationGroup="ADICIONAR" Width="295PX">
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
                            <asp:RangeValidator ID="RangeValidator_TextBox_PersonalCitado" runat="server"
                                ControlToValidate="TextBox_PersonalCitado" ValidationGroup="ADICIONAR" Display="None"
                                MinimumValue="0" MaximumValue="9999999" Type="Integer" ErrorMessage="<b>Campo Requerido valor desbordado</b><br />El NÚMERO DE PERSONAL no puede supuerar el personal activo en la empresa."></asp:RangeValidator>
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
                                    <td class="td_izq" style="width:180px;">
                                        Regional
                                    </td>
                                    <td colspan="5">
                                        <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="295px" AutoPostBack="True"
                                            OnSelectedIndexChanged="DropDownList_REGIONAL_SelectedIndexChanged" 
                                            ValidationGroup="ADICIONAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Departamento
                                    </td>
                                    <td colspan="5" class="td_der">
                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="295px" AutoPostBack="True"
                                            OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_SelectedIndexChanged" 
                                            ValidationGroup="ADICIONAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Ciudad
                                    </td>
                                    <td colspan="5" class="td_der">
                                        <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" Width="295px" 
                                            ValidationGroup="ADICIONAR">
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
                                            <asp:Button ID="Button_NUEVAACTIVIDAD_1" runat="server" Text="Nueva Actividad" CssClass="margin_botones"
                                                ValidationGroup="NUEVAACTIVIDAD" OnClick="Button_NUEVAACTIVIDAD_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_Imprimir_1" runat="server" Text="Imprimir Programa" ValidationGroup="IMPRIMIR"
                                                OnClick="Button_Imprimir_Click" CssClass="margin_botones" />
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
            <asp:PostBackTrigger ControlID="Button_Imprimir_1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
