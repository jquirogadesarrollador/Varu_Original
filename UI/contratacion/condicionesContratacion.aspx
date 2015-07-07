<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="condicionesContratacion.aspx.cs" Inherits="_CondicionesContratacion" %>

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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />

            <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
            <asp:HiddenField ID="HiddenField_REGISTRO_VEN_P_CONTRATACION" runat="server" />

            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

            <asp:HiddenField ID="HiddenField_GridPagina" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un&nbsp; texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_MOMDIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MOMDIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_VOLVER" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                                OnClick="Button_VOLVER_Click" ValidationGroup="VOLVER" />
                                        </td>
                                        <td colspan="0">
                                            <input class="margin_botones" id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" />
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
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Perfiles Asociados
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_PERFILES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO,ID_EMPRESA" AllowPaging="True" OnPageIndexChanging="GridView_PERFILES_PageIndexChanging"
                                    OnRowCommand="GridView_PERFILES_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="CARGO" />
                                        <asp:BoundField DataField="DSC_FUNCIONES" HeaderText="FUNCIONES">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                        <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DATOS_PERFIL_SELECCIONADO" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos perfil seleccionado
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Ocupación
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NOM_OCUPACION" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Edad Mínima
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_EDAD_MIN" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Edad Máxima
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_EDAD_MAX" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Experiencia:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_EXPERIENCIA" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Sexo
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_SEXO" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Nivel Académico
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NIVEL_ACADEMICO" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_SELECCION_CIUDAD_CC_SUBC" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        configuración actual para el perfil seleccionado
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_LISTA_CONFIGURACION_ACTUAL" runat="server" Width="885px"
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO,ID_SUB_C,ID_CENTRO_C,ID_CIUDAD,ID_SERVICIO"
                                    OnRowCommand="GridView_LISTA_CONFIGURACION_ACTUAL_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" ButtonType="Image"
                                            ImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="PORCENTAJE_RIESGO" HeaderText="Riesgo %" />
                                        <asp:BoundField DataField="DOC_TRAB" HeaderText="Doc. Trabajador" />
                                        <asp:BoundField DataField="OBS_CTE" HeaderText="Requerimientos" />
                                        <asp:BoundField DataField="NOM_SUB_C" HeaderText="Sub centro" />
                                        <asp:BoundField DataField="NOM_CC" HeaderText="Centro costo" />
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="NOMBRE_SERVICIO" HeaderText="Servicio" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BOTON_NUEVA_CONDICION" runat="server"> 
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        botones para nueva configuración
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVA_CONDICION_CONTRATACION" runat="server" Text="Crear condición de contratación"
                                        OnClick="Button_NUEVA_CONDICION_CONTRATACION_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_INFO_CONDICION_COTRATACION" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Condición de Contratación
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_DROP_PARA_SELECCIONAR_CIUDAD_CC_SUBC" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Selección de Ubicación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Ciudad
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_CIUDAD_SelectedIndexChanged"
                                                Width="400px" ValidationGroup="ENTIDAD">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Centro costo
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CENTRO_COSTO" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList_CENTRO_COSTO_SelectedIndexChanged" 
                                                Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Sub centro
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_SUB_CENTRO" runat="server" Width="400px" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="DropDownList_SUB_CENTRO_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_CIUDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD"
                                    ControlToValidate="DropDownList_CIUDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Por lo menos se debe seleccionar CIUDAD."
                                    ValidationGroup="ENTIDAD" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />

                                <asp:Panel ID="Panel_SeleccionServicio" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Selección de Servicio
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Servicio
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_SERVICIO" runat="server" Width="400px"
                                                        ValidationGroup="ENTIDAD">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_SERVICIO -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SERVICIO"
                                            ControlToValidate="DropDownList_SERVICIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SERVICIO es requerido."
                                            ValidationGroup="ENTIDAD" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SERVICIO"
                                            TargetControlID="RequiredFieldValidator_DropDownList_SERVICIO" HighlightCssClass="validatorCalloutHighlight" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="Panel_BOTON_EMPEZAR_CONFIGURACION" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_EMPEZAR_CONFIGURACION" runat="server" Text="Empezar configuración"
                                                    OnClick="Button_EMPEZAR_CONFIGURACION_Click" ValidationGroup="ENTIDAD" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="Panel_RIESGO_DOCUMENTOS_REQUISITOS" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Configuración de Condiciones de Contratación para la Ubicación Seleccionada
                            </div>
                            <div class="div_contenido_groupbox_gris">

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

                                <asp:Panel ID="Panel_CAMPOS_RIESGO_DOCUMENTOS_REQUISITOS" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Datos de Riesgo
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Valor riesgo
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_RIESGOS" runat="server" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                    Documentos entregados al trabajador
                                                    <br />
                                                    <table class="table_control_registros" style="text-align: left; font-size: 80%;">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:CheckBoxList ID="CheckBoxList_Documentos1" runat="server">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:CheckBoxList ID="CheckBoxList_Documentos2" runat="server">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:CheckBoxList ID="CheckBoxList_Documentos3" runat="server">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:CheckBoxList ID="CheckBoxList_Documentos4" runat="server">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:CheckBoxList ID="CheckBoxList_Documentos5" runat="server">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                    Recomendaciones<br />
                                                    <asp:TextBox ID="TextBox_REQUERIMIENTOS_USUARIO" runat="server" TextMode="MultiLine"
                                                        Width="700px" Height="73px" ValidationGroup="GUARDAR"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_RIESGOS -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_RIESGOS"
                                            ControlToValidate="DropDownList_RIESGOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El RIESGO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_RIESGOS"
                                            TargetControlID="RequiredFieldValidator_DropDownList_RIESGOS" HighlightCssClass="validatorCalloutHighlight" />
                                        <!-- TextBox_REQUERIMIENTOS_USUARIO -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_REQUERIMIENTOS_USUARIO"
                                            ControlToValidate="TextBox_REQUERIMIENTOS_USUARIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe escribir una breve descripción de las recomendaciones."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_REQUERIMIENTOS_USUARIO"
                                            TargetControlID="RequiredFieldValidator_TextBox_REQUERIMIENTOS_USUARIO" HighlightCssClass="validatorCalloutHighlight" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="Panel_SERVICIOS_COMPLEMENTARIOS" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Configuración de Servicios Complementarios y Exámenes Médicos</div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_InfoNoConfImplementos" runat="server">
                                            <div style="text-align: justify; padding: 5px 20px 5px 15px; font-weight: bold; font-size: 11px;
                                                color:Orange;">
                                                No se puede configurar entrega de Dotación, Epp. Porque no se encuentran activos
                                                estos Servicios Complementarios.
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelInfoNoConfExamenesMedicos" runat="server">
                                            <div style="text-align: justify; padding: 5px 20px 5px 15px; font-weight: bold; font-size: 11px;
                                                color:Orange;">
                                                No se puede configurar entrega de Examenes Medicos Adicionales. Porque no se encuentran
                                                activo este Servicio Complementario.
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel_LISTA_IMPLEMENTOS_SELECCIONADOS" runat="server">
                                            <div class="div_cabeza_groupbox_gris">
                                                lista de Implementos Parametrizados
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA_IMPLEMENTOS" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS" runat="server" />

                                                        <asp:HiddenField ID="HiddenField_REGISTRO_ELEMENTO_INICIAL" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_ID_PRODUCTO" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_CANTIDAD_INICIAL" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FACTURAR_A_INICIAL" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_VALOR_INICIAL" runat="server" />

                                                        <asp:HiddenField ID="HiddenField_REGISTRO_ELEMENTO_PROGRAMADO" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_PROGRAMADA" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_PERIODICIDAD" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_AJUSTE_A" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FECHA_INICIO_PROGRAMADA" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_CANTIDAD_PROGRAMADA" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FACTURAR_A_PROGRAMADA" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_VALOR_PROGRAMADA" runat="server" />
                                                        
                                                        <asp:HiddenField ID="HiddenField_ID_SERVICIO_COMPLEMENTARIO" runat="server" />

                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_ImplementosParametrizados" runat="server" Width="836px"
                                                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL,REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO" 
                                                                    onrowcommand="GridView_ImplementosParametrizados_RowCommand">
                                                                    <Columns>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/areas/view2.gif"
                                                                            Text="Modificar">
                                                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                            Text="Eliminar">
                                                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                                        </asp:ButtonField>
                                                                        <asp:TemplateField HeaderText="Servicio Complementario">
                                                                            <ItemTemplate>
                                                                                <table cellspacing="2" cellpadding="2" class="table_control_registros" style="text-align:left; border:solid 1px #cccccc; margin: 5px auto;">
                                                                                    <tr> 
                                                                                        <td>
                                                                                            Entrega Inicial
                                                                                        </td>
                                                                                        <td>
                                                                                            Servicio Complementario<br />
                                                                                            <asp:DropDownList ID="DropDownList_SERVICIOS_COMPLEMENTARIOS" runat="server" AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="DropDownList_SERVICIOS_COMPLEMENTARIOS_SelectedIndexChanged"
                                                                                                ValidationGroup="SC" Width="155px">
                                                                                            </asp:DropDownList>            
                                                                                        </td>
                                                                                        <td>
                                                                                            Implemento<br />
                                                                                            <asp:DropDownList ID="DropDownList_OBJETOS_SERVICIO" runat="server" ValidationGroup="SC"
                                                                                                Width="155px">
                                                                                            </asp:DropDownList>            
                                                                                        </td>
                                                                                        
                                                                                        <td>
                                                                                            Cantidad<br />
                                                                                            <asp:TextBox ID="TextBox_CANTIDAD_ENTREGA" runat="server" Width="50px" ValidationGroup="SC"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CANTIDAD_ENTREGA"
                                                                                                runat="server" TargetControlID="TextBox_CANTIDAD_ENTREGA" FilterType="Numbers" />
                                                                                        </td>
                                                                                        <td>
                                                                                            Facturar a<br />
                                                                                            <asp:DropDownList ID="DropDownList_FACTURAR_A" runat="server" ValidationGroup="SC"
                                                                                                Width="115px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            Valor<br />
                                                                                            <asp:TextBox ID="TextBox_VALOR_PRODDUCTO" runat="server" Width="105px" ValidationGroup="SC"
                                                                                                class="money"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="6">
                                                                                            <hr />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align:right;">
                                                                                            <asp:CheckBox ID="CheckBox_ProgramarEntregas" runat="server" 
                                                                                                Text="Entregas Periódicas" TextAlign="Left" AutoPostBack="True" ValidationGroup="SC"
                                                                                                oncheckedchanged="CheckBox_ProgramarEntregas_CheckedChanged" />
                                                                                        </td>
                                                                                        <td>
                                                                                            Periodicidad<br />
                                                                                            <asp:DropDownList ID="DropDownList_PERIODO_ENTREGA" runat="server" ValidationGroup="SC"
                                                                                                Width="155px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            Ajuste a<br />
                                                                                            <asp:DropDownList ID="DropDownList_AjusteEntrega" runat="server" 
                                                                                                ValidationGroup="SC" Width="155px" AutoPostBack="True"
                                                                                                onselectedindexchanged="DropDownList_AjusteEntrega_SelectedIndexChanged">
                                                                                                <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                                                                <asp:ListItem Value="CONTRATO">Contrato</asp:ListItem>
                                                                                                <asp:ListItem Value="FECHA">Fecha</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <div class="div_espaciador"></div>
                                                                                            <asp:TextBox ID="TextBox_FechaInicialProgramado" runat="server" Width="150px" ValidationGroup="SC"></asp:TextBox>
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaInicialProgramado" runat="server"
                                                                                                TargetControlID="TextBox_FechaInicialProgramado" Format="dd/MM/yyyy" />
                                                                                        </td>
                                                                                        <td>
                                                                                            Cantidad<br />
                                                                                            <asp:TextBox ID="TextBox_CantidadPeriodica" runat="server" Width="50px" ValidationGroup="SC"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CantidadPeriodica"
                                                                                                runat="server" TargetControlID="TextBox_CantidadPeriodica" FilterType="Numbers" />
                                                                                        </td>
                                                                                        <td>
                                                                                            Facturar a<br />
                                                                                            <asp:DropDownList ID="DropDownList_FacturarAPeriodica" runat="server" ValidationGroup="SC"
                                                                                                Width="115px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            Valor<br />
                                                                                            <asp:TextBox ID="TextBox_ValorPeriodica" runat="server" Width="105px" ValidationGroup="SC"
                                                                                                class="money"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <!-- DropDownList_SERVICIOS_COMPLEMENTARIOS -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                                                                    ControlToValidate="DropDownList_SERVICIOS_COMPLEMENTARIOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar un servicio complementario."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                                                                    HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- DropDownList_OBJETOS_SERVICIO -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_OBJETOS_SERVICIO"
                                                                                    ControlToValidate="DropDownList_OBJETOS_SERVICIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar un servicio complementario."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_OBJETOS_SERVICIO"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_OBJETOS_SERVICIO" HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- TextBox_CANTIDAD_ENTREGA -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CANTIDAD_ENTREGA"
                                                                                    ControlToValidate="TextBox_CANTIDAD_ENTREGA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CANTIDAD es requerida."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CANTIDAD_ENTREGA"
                                                                                    TargetControlID="RequiredFieldValidator_TextBox_CANTIDAD_ENTREGA" HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- DropDownList_FACTURAR_A -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_FACTURAR_A"
                                                                                    ControlToValidate="DropDownList_FACTURAR_A" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />FACTURAR A es requerido."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_FACTURAR_A"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_FACTURAR_A" HighlightCssClass="validatorCalloutHighlight" />




                                                                                <!-- DropDownList_PERIODO_ENTREGA -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_PERIODO_ENTREGA"
                                                                                    ControlToValidate="DropDownList_PERIODO_ENTREGA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La PERIODICIDAD es requerida."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_PERIODO_ENTREGA"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_PERIODO_ENTREGA" HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- DropDownList_AjusteEntrega -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_AjusteEntrega"
                                                                                    ControlToValidate="DropDownList_AjusteEntrega" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El AJUSTE A es requerido."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_AjusteEntrega"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_AjusteEntrega" HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- TextBox_FechaInicialProgramado -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FechaInicialProgramado"
                                                                                    ControlToValidate="TextBox_FechaInicialProgramado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />FECHA DE INICIO es requerida."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaInicialProgramado"
                                                                                    TargetControlID="RequiredFieldValidator_TextBox_FechaInicialProgramado" HighlightCssClass="validatorCalloutHighlight" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FechaInicialProgramado"
                                                                                    runat="server" TargetControlID="TextBox_FechaInicialProgramado" FilterType="Numbers,Custom"
                                                                                    ValidChars="/" />

                                                                                <!-- TextBox_CantidadPeriodica -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CantidadPeriodica"
                                                                                    ControlToValidate="TextBox_CantidadPeriodica" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CANTIDAD es requerida."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CantidadPeriodica"
                                                                                    TargetControlID="RequiredFieldValidator_TextBox_CantidadPeriodica" HighlightCssClass="validatorCalloutHighlight" />

                                                                                <!-- DropDownList_FacturarAPeriodica -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_FacturarAPeriodica"
                                                                                    ControlToValidate="DropDownList_FacturarAPeriodica" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />FACTURAR A es requerido."
                                                                                    ValidationGroup="SC" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_FacturarAPeriodica"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_FacturarAPeriodica" HighlightCssClass="validatorCalloutHighlight" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <asp:Panel ID="Panel_BotonesImplementos" runat="server">
                                                            <div class="div_espaciador">
                                                            </div>
                                                            <table class="table_control_registros">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="Button_NuevoImplemento" runat="server" Text="Nuevo Implemento" CssClass="margin_botones"
                                                                            ValidationGroup="NUEVOIMPLEMENTO" OnClick="Button_NuevoImplemento_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="Button_GuardarImplemento" runat="server" Text="Guardar Implemento"
                                                                            CssClass="margin_botones" ValidationGroup="SC" OnClick="Button_GuardarImplemento_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="Button_CancelarImplemento" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                            ValidationGroup="CANCELARIMPLEMENTO" OnClick="Button_CancelarImplemento_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel_EXAMENES_SELECCIONADOS" runat="server">
                                            <div class="div_espaciador">
                                            </div>
                                            <div class="div_cabeza_groupbox_gris">
                                                lista de examenes parametrizados
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <asp:HiddenField ID="HiddenField_ServicioExamenesMedicos" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_REGISTRO_CON_REG_ELEMENTO_TRABAJO_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_REGISTRO_VEN_P_CONTRATACION_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_ID_PRODUCTO_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_CANTIDAD_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_CODIGO_PERIODO_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_CODIGO_FACTURAR_A_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_VALOR_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_PRIMERA_ENTREGA_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_AJUSTE_A_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_FECHA_INICIO_EXAMENES" runat="server" />
                                                        <asp:HiddenField ID="HiddenField_ID_SERVICIO_COMPLEMENTARIO_EXAMENES" runat="server" />
                                                        
                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_ExamenesParametrizados" runat="server" Width="836px" AutoGenerateColumns="False"
                                                                    DataKeyNames="REGISTRO_CON_REG_ELEMENTO_TRABAJO,REGISTRO_VEN_P_CONTRATACION,CANTIDAD,VALOR,PRIMERA_ENTREGA,AJUSTE_A,FECHA_INICIO,ID_SERVICIO_COMPLEMENTARIO" OnRowCommand="GridView_ExamenesParametrizados_RowCommand">
                                                                    <Columns>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/areas/view2.gif"
                                                                            Text="Actualizar">
                                                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                            Text="Eliminar">
                                                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                                        </asp:ButtonField>
                                                                        <asp:TemplateField HeaderText="Referencia">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="DropDownList_OBJETOS_SERVICIO" runat="server" ValidationGroup="EM"
                                                                                    Width="175px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_OBJETOS_SERVICIO_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <!-- DropDownList_OBJETOS_SERVICIO -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_OBJETOS_SERVICIO"
                                                                                    ControlToValidate="DropDownList_OBJETOS_SERVICIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar un Exámen Médico."
                                                                                    ValidationGroup="EM" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_OBJETOS_SERVICIO"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_OBJETOS_SERVICIO" HighlightCssClass="validatorCalloutHighlight" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Descripción">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label_Descripcion" runat="server" Text="Descripción del Examen"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Aplica a">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label_AplicaA" runat="server" Text="Hombre o Mujer"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Periodicidad">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="DropDownList_PERIODO_ENTREGA" runat="server" ValidationGroup="EM"
                                                                                    Width="125px">
                                                                                </asp:DropDownList>
                                                                                <!-- DropDownList_PERIODO_ENTREGA -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_PERIODO_ENTREGA"
                                                                                    ControlToValidate="DropDownList_PERIODO_ENTREGA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La PERIODICIDAD es requerida."
                                                                                    ValidationGroup="EM" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_PERIODO_ENTREGA"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_PERIODO_ENTREGA" HighlightCssClass="validatorCalloutHighlight" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Facturar a">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="DropDownList_FACTURAR_A" runat="server" ValidationGroup="EM"
                                                                                    Width="125px">
                                                                                </asp:DropDownList>
                                                                                <!-- DropDownList_FACTURAR_A -->
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_FACTURAR_A"
                                                                                    ControlToValidate="DropDownList_FACTURAR_A" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />FACTURAR A es requerido."
                                                                                    ValidationGroup="EM" />
                                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_FACTURAR_A"
                                                                                    TargetControlID="RequiredFieldValidator_DropDownList_FACTURAR_A" HighlightCssClass="validatorCalloutHighlight" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>

                                                                <asp:Panel ID="Panel_BotonesExamenesMedicos" runat="server">
                                                                    <div class="div_espaciador">
                                                                    </div>
                                                                    <table class="table_control_registros">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="Button_NuevoExamen" runat="server" Text="Nuevo Exámen" CssClass="margin_botones"
                                                                                    ValidationGroup="NUEVOEXAMEN" OnClick="Button_NuevoExamen_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="Button_GuardarExamen" runat="server" Text="Guardar Exámen" CssClass="margin_botones"
                                                                                    ValidationGroup="EM" OnClick="Button_GuardarExamen_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="Button_CancelarExamen" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                                    ValidationGroup="CANCELAREXAMEN" OnClick="Button_CancelarExamen_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <%--Desde--%>
                                <asp:Panel ID="Panel_clausulas" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Clausulas que Serán Aplicadas al Contratar al Trabajador</div>
                                    <div class="div_contenido_groupbox_gris">
                                                <asp:UpdatePanel ID="UpdatePanel_clausulas" runat="server">
                                                    <ContentTemplate>
                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_clausulas" runat="server"  Width="846px" AllowPaging="True" 
                                                                    AutoGenerateColumns="False" DataKeyNames="ID_CLAUSULA">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Aplicar">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="CheckBox_aplicar" runat="server"/>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="ID_CLAUSULA" HeaderText="ID" Visible="false">
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TIPO_CLAUSULA" HeaderText="Tipo" >
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion" >
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </asp:Panel>
                                <%--Hasta--%>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BOTONES_INFERIORES" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div style="margin: 0 auto; display: block;">
                                    <div class="div_cabeza_groupbox">
                                        Botones de Acción
                                    </div>
                                    <div class="div_contenido_groupbox">
                                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                            <tr>
                                                <td colspan="0">
                                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                        OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                                </td>
                                                <td colspan="0">
                                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                        OnClick="Button_MOMDIFICAR_Click" ValidationGroup="MODIFICAR" />
                                                </td>
                                                <td colspan="0">
                                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                        OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                                </td>
                                                <td colspan="0">
                                                    <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                                        OnClick="Button_VOLVER_Click" ValidationGroup="VOLVER" />
                                                </td>
                                                <td colspan="0">
                                                    <input class="margin_botones" id="Button6" type="button" value="Salir" onclick="window.close();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="div_espaciador"></div>
</asp:Content>
