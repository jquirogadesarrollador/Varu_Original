<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="FabricaAssesment.aspx.cs" Inherits="_FabricaAssesment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    

    <%--VENTANA EMERGENTE CON MENSAJE--%>
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

    <asp:HiddenField ID="HiddenField_ID_ASSESMENT_CENTER" runat="server" />

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
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td colspan="0">
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_VOLVER" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                        OnClick="Button_LISTA_CONTRATOS_Click" ValidationGroup="VOLVER" />
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

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Datos de Assesment Center
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_ASSESMENT" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="ID_ASSESMENT_CENTER" OnRowCommand="GridView_ASSESMENT_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                    Text="Seleccionar">
                                    <ItemStyle CssClass="columna_grid_centrada" Width="20px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOMBRE_ASSESMENT" HeaderText="Nombre">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION_ASSESMENT" HeaderText="Descripción">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COMPETENCIAS" 
                                    HeaderText="Competencias / Habilidades" HtmlEncode="False" 
                                    HtmlEncodeFormatString="False">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Archivo">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink_ARCHIVO_ASSESMENT" runat="server">Descargar</asp:HyperLink>
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

    <asp:Panel ID="Panel_DatosAssesmentSeleccionado" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Assesment Center Seleccionado
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <asp:Panel ID="Panel_CABEZA_REGISTRO" runat="server" CssClass="div_cabeza_groupbox_gris">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 87%;">
                                    Control de Registro
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
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Nombre:
                        </td>
                        <td class="td_der">
                            <asp:TextBox ID="TextBox_NombreAssesment" runat="server" Width="785px" ValidationGroup="ADICIONAR"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            Descripción:<br />
                            <asp:TextBox ID="TextBox_DescripcionAssesment" runat="server" Height="97px" TextMode="MultiLine"
                                ValidationGroup="ADICIONAR" Width="850px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <!-- TextBox_NombreAssesment -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NombreAssesment"
                    ControlToValidate="TextBox_NombreAssesment" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La NOMBRE es requerido."
                    ValidationGroup="ADICIONAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NombreAssesment"
                    TargetControlID="RequiredFieldValidator_TextBox_NombreAssesment" HighlightCssClass="validatorCalloutHighlight" />
                <!-- TextBox_DescripcionAssesment -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DescripcionAssesment"
                    ControlToValidate="TextBox_DescripcionAssesment" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                    ValidationGroup="ADICIONAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DescripcionAssesment"
                    TargetControlID="RequiredFieldValidator_TextBox_DescripcionAssesment" HighlightCssClass="validatorCalloutHighlight" />

                <asp:Panel ID="Panel_HabilidadesAssesment" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox_gris">
                                Habildiades / Competencias
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_HABILIDAD_ASSESMENT" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_COMPETENCIA" runat="server" />
                                <asp:HiddenField ID="HiddenField_COMPETENCIA" runat="server" />
                                <asp:HiddenField ID="HiddenField_DEFINICION" runat="server" />
                                <asp:HiddenField ID="HiddenField_AREACOMPETENCIA" runat="server" />
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <asp:GridView ID="GridView_CompetenciasAssesment" runat="server" Width="865px" AutoGenerateColumns="False"
                                            DataKeyNames="ID_COMPETENCIA_ASSESMENT,ID_COMPETENCIA,ID_ASSESMENT" OnRowCommand="GridView_CompetenciasAssesment_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                    Text="Eliminar">
                                                    <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="Competencia ó Habilidad">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DropDownList_CompetenciaAssesment" runat="server" ValidationGroup="GUARDARCOMPETENCIA"
                                                            AutoPostBack="true" Width="300px" OnSelectedIndexChanged="DropDownList_CompetenciaAssesment_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <!-- DropDownList_CompetenciaAssesment -->
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CompetenciaAssesment"
                                                            ControlToValidate="DropDownList_CompetenciaAssesment" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La COMPETENCIA es requerida."
                                                            ValidationGroup="GUARDARCOMPETENCIA" />
                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CompetenciaAssesment"
                                                            TargetControlID="RequiredFieldValidator_DropDownList_CompetenciaAssesment" HighlightCssClass="validatorCalloutHighlight" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" Width="310px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Definición">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_DefinicionCompetencia" runat="server" Text="Definición de la competencia"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Área">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_AreaCompetencia" runat="server" Text="Area de la competencia"></asp:Label>
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
                                <div style="text-align: left; margin-left: 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_NUEVA_COMPETENCIA" runat="server" Text="Nueva Competencia / Habilidad"
                                                    CssClass="margin_botones" ValidationGroup="NUEVACOMPETENCIA" OnClick="Button_NUEVA_COMPETENCIA_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_GUARDAR_COMPETENCIA" runat="server" Text="Guardar Competencia / Habilidad"
                                                    CssClass="margin_botones" ValidationGroup="GUARDARCOMPETENCIA" OnClick="Button_GUARDAR_COMPETENCIA_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_CANCELAR_COMPETENCIA" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    ValidationGroup="CANCELARCOMPETENCIA" OnClick="Button_CANCELAR_COMPETENCIA_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <asp:Panel ID="Panel_LinkAssesment" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        Link Archivo Assesment Center
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:Panel ID="Panel_MostrarLink" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Archivo:
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink_ArchivoAssesment" runat="server">Clic aquí para decargar.</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="Panel_AdjuntarLink" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Seleccione el archivo:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload_Archivo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_EstadoAssesment" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq">
                                Estado:
                            </td>
                            <td class="td_der">
                                <asp:DropDownList ID="DropDownList_EstadoAssesment" runat="server" ValidationGroup="ADICIONAR"
                                    Width="260px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <!-- DropDownList_EstadoAssesment -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EstadoAssesment"
                        ControlToValidate="DropDownList_EstadoAssesment" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                        ValidationGroup="ADICIONAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EstadoAssesment"
                        TargetControlID="RequiredFieldValidator_DropDownList_EstadoAssesment" HighlightCssClass="validatorCalloutHighlight" />
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_PIE" runat="server">
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
                                    <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                        OnClick="Button_LISTA_CONTRATOS_Click" ValidationGroup="VOLVER" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
