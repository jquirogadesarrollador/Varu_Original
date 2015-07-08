<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="perfiles.aspx.cs" Inherits="_Default" %>

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

    <asp:UpdatePanel ID="UpdatePanel_Procesamiento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_ENTREVISTA_INICIAL" runat="server" />
            
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
            <asp:HiddenField ID="HiddenField_PAGINA_GRID" runat="server" />
            
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
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
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
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}123456789" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos de Perfiles
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_PERFILES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO,ID_EMPRESA" AllowPaging="True" OnPageIndexChanging="GridView_PERFILES_PageIndexChanging"
                                    OnRowCommand="GridView_PERFILES_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="CARGO" />
                                        <asp:BoundField DataField="DSC_FUNCIONES" HeaderText="FUNCIONES">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EDAD_MAX" HeaderText="EDAD MÁXIMA">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EDAD_MIN" HeaderText="EDAD MÍNIMA">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEXO" HeaderText="SEXO">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXPERIENCIA" HeaderText="EXPERIENCIA" />
                                        <asp:BoundField DataField="NIV_ESTUDIOS" HeaderText="NIVEL ESTUDIOS" />
                                        <asp:BoundField DataField="REGISTRO" HeaderText="ID" Visible="False" />
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

            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Perfil
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

                        <asp:Panel ID="Panel_IDENTIFICADOR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div style="margin: 0 auto; display: block; width: 100%;">
                                <div class="div_cabeza_groupbox_gris">
                                    Perfil
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Ocupación
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_ocupacion" runat="server" ValidationGroup="GUARDAR"
                                                    Width="570px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_ocupacion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                Funciones del cargo seleciconado<br />
                                                <asp:TextBox ID="TextBox_FincionesCargoSeleccionado" runat="server" TextMode="MultiLine"
                                                    Width="650px" Height="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Edad: Mínima
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_EDAD_MINIMA" runat="server" Width="200px" MaxLength="40"
                                                    ValidationGroup="PERFILES"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EDAD_MINIMA"
                                                    runat="server" TargetControlID="TextBox_EDAD_MINIMA" FilterType="Numbers" />
                                            </td>
                                            <td class="td_izq">
                                                Máxima
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_EDAD_MAXIMA" runat="server" Width="200px" MaxLength="40"
                                                    ValidationGroup="PERFILES"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EDAD_MAXIMA"
                                                    runat="server" TargetControlID="TextBox_EDAD_MAXIMA" FilterType="Numbers" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Experiencia:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_Experiencia" runat="server" ValidationGroup="PERFILES"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Sexo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_SEXO" runat="server" ValidationGroup="PERFILES"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Nivel Académico
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_NIV_ACADEMICO" runat="server" ValidationGroup="PERFILES"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--DropDownList_ocupacion--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ocupacion"
                                        ControlToValidate="DropDownList_ocupacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La OCUPACIÓN es requerida."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ocupacion"
                                        TargetControlID="RequiredFieldValidator_DropDownList_ocupacion" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--TextBox_EDAD_MINIMA--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_EDAD_MINIMA"
                                        ControlToValidate="TextBox_EDAD_MINIMA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EDAD MÍNIMA permitida es requerida."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EDAD_MINIMA"
                                        TargetControlID="RequiredFieldValidator_TextBox_EDAD_MINIMA" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--TextBox_EDAD_MAXIMA--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_EDAD_MAXIMA"
                                        ControlToValidate="TextBox_EDAD_MAXIMA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EDAD MÁXIMA permitida es requerida."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EDAD_MAXIMA"
                                        TargetControlID="RequiredFieldValidator_TextBox_EDAD_MAXIMA" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--DropDownList_Experiencia--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Experiencia"
                                        ControlToValidate="DropDownList_Experiencia" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EXPERIENCIA es requerida."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Experiencia"
                                        TargetControlID="RequiredFieldValidator_DropDownList_Experiencia" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--DropDownList_SEXO--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SEXO"
                                        ControlToValidate="DropDownList_SEXO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SEXO es requerido."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SEXO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_SEXO" HighlightCssClass="validatorCalloutHighlight" />
                                    <%--DropDownList_NIV_ACADEMICO--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_NIV_ACADEMICO"
                                        ControlToValidate="DropDownList_NIV_ACADEMICO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NIVEL ACADEMICO es requerido."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_NIV_ACADEMICO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_NIV_ACADEMICO" HighlightCssClass="validatorCalloutHighlight" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_OBSERVACIONES_ESPECIALES" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Observaciones Especiales
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox_OBSERVACIONES_ESPECIALES" runat="server" Height="88px" TextMode="MultiLine"
                                                Width="838px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <%--TextBox_OBSERVACIONES_ESPECIALES--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBSERVACIONES_ESPECIALES"
                                    ControlToValidate="TextBox_OBSERVACIONES_ESPECIALES" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBESERVACIONES ESPECIALES son requeridas."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBSERVACIONES_ESPECIALES"
                                    TargetControlID="RequiredFieldValidator_TextBox_OBSERVACIONES_ESPECIALES" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>

                        <div class="div_espaciador">
                        </div>

                        <table class="table_form_dos_columnas" cellpadding="0" cellspacing="0" width="90%">
                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="Panel_documentos" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Documentos requeridos para este perfil
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <asp:Panel ID="Panel_FONDO_MENSAJE_DOCUMENTOS" runat="server" Visible="false" Style="background-color: #999999;">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel_MENSAJES_DOCUMENTOS" runat="server">
                                                        <asp:Image ID="Image_MENSAJE_POPUP_DOCUMENTOS" runat="server" Width="50px" Height="50px"
                                                            Style="margin: 5px auto 8px auto; display: block;" />
                                                        <asp:Panel ID="Panel_COLOR_FONDO_POPUP_DOCUMENTOS" runat="server" Style="text-align: center">
                                                            <asp:Label ID="Label_MENSAJE_DOCUMENTOS" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                                                        </asp:Panel>
                                                        <div style="text-align: center; margin-top: 15px;">
                                                            <asp:Button ID="Button_CERRAR_MENSAJE_DOCUMENTOS" runat="server" Text="Cerrar" 
                                                                CssClass="margin_botones" onclick="Button_CERRAR_MENSAJE_DOCUMENTOS_Click" />
                                                        </div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel_ADICIONAR_DOCUMENTO" runat="server">
                                                        <table class="table_control_registros">
                                                            <tr>
                                                                <td class="td_izq">
                                                                    <asp:Label ID="Label_NOMBRE_DOCUMENTO" runat="server" Text="Documento"></asp:Label>
                                                                </td>
                                                                <td class="td_der">
                                                                    <asp:DropDownList ID="DropDownList_NOMBRE_DOCUMENTO" runat="server" Width="240px"
                                                                        ValidationGroup="NUEVOCLIENTE">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="td_der">
                                                                    <asp:Button ID="Button_ADICIONAR_DOCUMENTO" runat="server" Text="Adicionar" CssClass="margin_botones"
                                                                        ValidationGroup="SERVICIOCOMP" OnClick="Button_ADICIONAR_DOCUMENTO_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="div_espaciador">
                                                        </div>
                                                    </asp:Panel>
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td>
                                                                <div class="div_contenedor_grilla_resultados">
                                                                    <div class="grid_seleccionar_registros">
                                                                        <asp:GridView ID="GridView_NOMBRE_DOCUMENTO" runat="server" Width="400px" OnRowCommand="GridView_NOMBRE_DOCUMENTO_RowCommand"
                                                                            AutoGenerateColumns="False" DataKeyNames="Código Documento">
                                                                            <Columns>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                                    Text="Eliminar">
                                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                                </asp:ButtonField>
                                                                                <asp:TemplateField HeaderText="Documento">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label_Documento" runat="server" Text="Nombre del Documento"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>
                                    <div class="div_espaciador">
                                    </div>
                                </td>
                                <td valign="top">
                                    <asp:Panel ID="Panel_pruebas" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Pruebas Requeridas Para este Perfil
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel_FONDO_MENSAJE_PRUEBAS" runat="server" Visible="false" Style="background-color: #999999;">
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel_MENSAJES_PRUEBAS" runat="server">
                                                        <asp:Image ID="Image_MENSAJE_POPUP_PRUEBAS" runat="server" Width="50px" Height="50px"
                                                            Style="margin: 5px auto 8px auto; display: block;" />
                                                        <asp:Panel ID="Panel_COLOR_FONDO_POPUP_PRUEBAS" runat="server" Style="text-align: center">
                                                            <asp:Label ID="Label_MENSAJE_PRUEBAS" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                                                        </asp:Panel>
                                                        <div style="text-align: center; margin-top: 15px;">
                                                            <asp:Button ID="Button_CERRAR_MENSAJE_PRUEBAS" runat="server" Text="Cerrar" 
                                                                CssClass="margin_botones" onclick="Button_CERRAR_MENSAJE_PRUEBAS_Click" />
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel_ADICIONAR_PRUEBAS" runat="server">
                                                        <table class="table_control_registros">
                                                            <tr>
                                                                <td class="td_izq">
                                                                    <asp:Label ID="Label_NOMBRE_PRUEBA" runat="server" Text="Prueba"></asp:Label>
                                                                </td>
                                                                <td class="td_der">
                                                                    <asp:DropDownList ID="DropDownList_NOMBRE_PRUEBA" runat="server" Width="262px" ValidationGroup="NUEVOCLIENTE">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="td_der">
                                                                    <asp:Button ID="Button_ADICIONAR_PRUEBA" runat="server" Text="Adicionar" CssClass="margin_botones"
                                                                        ValidationGroup="SERVICIOCOMP" OnClick="Button_ADICIONAR_PRUEBA_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="div_espaciador">
                                                        </div>
                                                    </asp:Panel>
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td>
                                                                <div class="div_contenedor_grilla_resultados">
                                                                    <div class="grid_seleccionar_registros">
                                                                        <asp:GridView ID="GridView_NOMBRE_PRUEBA" runat="server" Width="400px" OnRowCommand="GridView_NOMBRE_PRUEBA_RowCommand"
                                                                            AutoGenerateColumns="False" DataKeyNames="Código Prueba">
                                                                            <Columns>
                                                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                                    Text="Eliminar">
                                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                                </asp:ButtonField>
                                                                                <asp:TemplateField HeaderText="Prueba">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label_Prueba" runat="server" Text="Nombre de la Prueba"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>
                                    <div class="div_espaciador">
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="Panel_CONFIGURACION_REFERENCIA" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Tipo de Confirmación de Referencias
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Tipo de Confirmación de Referencia:
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_TipoConfirmacionReferencia" runat="server" ValidationGroup="GUARDAR" 
                                                Width="400px" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <%--DropDownList_TipoConfirmacionReferencia--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoConfirmacionReferencia" ControlToValidate="DropDownList_TipoConfirmacionReferencia"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CATEGORÍA DE CONFIRMACIÓN DE REFERENCIAS es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TipoConfirmacionReferencia"
                                    TargetControlID="RequiredFieldValidator_DropDownList_TipoConfirmacionReferencia" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_NivelDificultadRequerimientos" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox">
                                Nivel Dificultad para Requerimientos
                            </div>
                            <div class="div_contenido_groupbox">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadioButtonList_NivelDificultadReq" runat="server" 
                                                ValidationGroup="GUARDAR" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="BAJA">Baja</asp:ListItem>
                                                <asp:ListItem Value="MEDIA">Media</asp:ListItem>
                                                <asp:ListItem Value="COMPLEJA">Compleja</asp:ListItem>
                                            </asp:RadioButtonList> 
                                        </td>
                                    </tr>
                                </table>
                                <%--RadioButtonList_NivelDificultadReq--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_NivelDificultadReq" ControlToValidate="RadioButtonList_NivelDificultadReq"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La NIVEL DE DIFICULTAD PARA REQUERIMIENTOS es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_NivelDificultadReq"
                                    TargetControlID="RequiredFieldValidator_RadioButtonList_NivelDificultadReq"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_CONFIGURACION_ENTRVISTA" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Formato Entrevista
                            </div>
                            <div class="div_contenido_groupbox_gris">

                                <asp:Panel ID="Panel_TIPO_ENTREVISTA" runat="server">
                                    <table class="table_control_registros" cellpadding="2">
                                        <tr>
                                            <td style="border: 1px solid #000000; background-color: #eeeeee;">
                                                <asp:CheckBox ID="CheckBox_TipoBasica" runat="server" Text="Básica" AutoPostBack="True"
                                                    OnCheckedChanged="CheckBox_TipoBasica_CheckedChanged" />
                                            </td>
                                            <td style="border: 1px solid #000000; background-color: #eeeeee;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox_TipoCompetencias" runat="server" Text="Competencias / Habilidades" AutoPostBack="True"
                                                                OnCheckedChanged="CheckBox_TipoCompetencias_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel_COMPETENCIAS" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="div_espaciador">
                                            </div>
                                            <div class="div_cabeza_groupbox_gris">
                                                Selección de Assesment Center
                                            </div>
                                            <div class="div_contenido_groupbox_gris">



                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Assesment Center:
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_AssesmentCenter" runat="server" 
                                                                Width="500px" ValidationGroup="GUARDAR" AutoPostBack="True" 
                                                                onselectedindexchanged="DropDownList_AssesmentCenter_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--DropDownList_AssesmentCenter--%>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_AssesmentCenter" ControlToValidate="DropDownList_AssesmentCenter"
                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ASSESMENT CENTER es requerido."
                                                    ValidationGroup="GUARDAR" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_AssesmentCenter"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_AssesmentCenter"
                                                    HighlightCssClass="validatorCalloutHighlight" />

                                                <asp:Panel ID="Panel_InformacionAssesmentSeleccionado" runat="server">
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <table cellpadding="3" cellspacing="0" class="table_control_registros" width="610" style="border:1px solid #cccccc;">
                                                        <tr style="border:1px solid #cccccc;">
                                                            <td class="td_izq">
                                                                <b>Descripción:</b>
                                                            </td>
                                                            <td class="td_der">
                                                                <asp:Label ID="Label_DescripcionAssesment" runat="server" Text="Texto con la descripcion del Assesment Center seleccionado."></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr style="border:1px solid #cccccc;">
                                                            <td class="td_izq">
                                                                <b>Competecias / Habilidades:</b>
                                                            </td>
                                                            <td class="td_der">
                                                                <asp:Label ID="Label_CompetenciasAssesment" runat="server" Text="Lista de Competencias ó Habilidades del Assesment Center seleccionado."></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES_ABAJO" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div style="margin: 0 auto; display: block;">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
