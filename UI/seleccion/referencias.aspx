<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="referencias.aspx.cs" Inherits="seleccion_referencias" %>

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

            <asp:HiddenField ID="HiddenField_ID_REFERENCIA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
            <asp:HiddenField ID="HiddenField_ARCHIVO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_ACTUALIZACION_REFERENCIA" runat="server" />

            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
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
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nueva" CssClass="margin_botones"
                                                ValidationGroup="NUEVO" OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_IMPRIMIR" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
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
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                ValidationGroup="BUSCAR_REFERENCIA" CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                ControlToValidate="DropDownList_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda."
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                ControlToValidate="TextBox_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar."
                                ValidationGroup="BUSCAR_REFERENCIA" />
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

            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, 
                    tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" Style="height: 26px"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
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
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" AutoGenerateColumns="False"
                                    DataKeyNames="ID_SOLICITUD,ID_REFERENCIA,ARCHIVO,ID_REQUERIMIENTO" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="Número de documento" HeaderText="Número de documento">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="ID_REFERENCIA" HeaderText="ID_REFERENCIA" Visible="False" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DATOS_CANDIDATO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información del Candidato
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Número Documento
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_DOCUMENTO_IDENTIDAD" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Nombre
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NOMBRES" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Cargo
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CARGO" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_SeleccionTipoReferencia" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Selección de Tipo de Confirmación de Referencias Laborales
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Tipo de Confirmación de Referencia:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_TipoConfirmacionReferencia" runat="server" ValidationGroup="GUARDAR"
                                        Width="400px" AutoPostBack="True" 
                                        onselectedindexchanged="DropDownList_TipoConfirmacionReferencia_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <%--DropDownList_TipoConfirmacionReferencia--%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoConfirmacionReferencia"
                            ControlToValidate="DropDownList_TipoConfirmacionReferencia" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CATEGORÍA DE CONFIRMACIÓN DE REFERENCIAS es requerida."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TipoConfirmacionReferencia"
                            TargetControlID="RequiredFieldValidator_DropDownList_TipoConfirmacionReferencia"
                            HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Confirmación Referencia Laboral</div>
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

                        <asp:Panel ID="Panel_INFORMAION_REFERENCIA" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Información de la Referencia
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:Panel ID="Panel_TIPO_REFERENCIA" runat="server">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Tipo de referencia
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_TIPO_REFERENCIA" runat="server" Width="250px"
                                                    ValidationGroup="ADICIONAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_TIPO_REFERENCIA -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TIPO_REFERENCIA"
                                        ControlToValidate="DropDownList_TIPO_REFERENCIA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE REFERENCIA es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TIPO_REFERENCIA"
                                        TargetControlID="RequiredFieldValidator_TIPO_REFERENCIA" HighlightCssClass="validatorCalloutHighlight" />
                                    <div class="div_espaciador">
                                    </div>
                                </asp:Panel>

                                <div style="text-align: center; font-weight: bold;">
                                    Datos Informante
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Nombre
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NOMBRE_INFORMANTE" runat="server" ValidationGroup="ADICIONAR"
                                                Width="320px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Cargo del informante
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_CARGO_INFORMANTE" runat="server" ValidationGroup="ADICIONAR"
                                                Width="320px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_NOMBRE_INFORMANTE -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRE_INFORMANTE"
                                    ControlToValidate="TextBox_NOMBRE_INFORMANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El  NOMBRE DEL INFORMANTE es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRE_INFORMANTE"
                                    TargetControlID="RequiredFieldValidator_TextBox_NOMBRE_INFORMANTE" HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_CARGO_INFORMANTE -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CARGO_INFORMANTE"
                                    ControlToValidate="TextBox_CARGO_INFORMANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El  CARGO DEL INFORMANTE es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CARGO_INFORMANTE"
                                    TargetControlID="RequiredFieldValidator_TextBox_CARGO_INFORMANTE" HighlightCssClass="validatorCalloutHighlight" />

                                <div class="div_espaciador">
                                </div>

                                <div style="text-align: center; font-weight: bold;">
                                    Datos Jefe Inmediato
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Nombre
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_Nombrejefe" runat="server" Width="320px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Cargo del Jefe
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_CargoJefe" runat="server" Width="320px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div class="div_espaciador">
                                </div>

                                <div style="text-align: center; font-weight: bold;">
                                    Datos Laborales del Candidato
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_EMPRESA_TRABAJO" runat="server" Text="Empresa Donde Trabajó"></asp:Label>
                                        </td>
                                        <td colspan="4" class="td_der">
                                            <asp:TextBox ID="TextBox_EMPRESA_TRABAJO" runat="server" Width="435px" MaxLength="255"
                                                ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Teléfono:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_Numero_Telefono" runat="server" Width="115px" MaxLength="255"
                                                ValidationGroup="ADICIONAR"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Numero_Telefono"
                                                runat="server" TargetControlID="TextBox_Numero_Telefono" FilterType="Numbers,Custom"
                                                ValidChars="()[]{}- extEXT" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label1" runat="server" Text="Empresa Temporal"></asp:Label>
                                        </td>
                                        <td colspan="4" class="td_der">
                                            <asp:TextBox ID="TextBox_EmpresaTemporal" runat="server" Width="435px" MaxLength="255"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Teléfono:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_TelefonoEmpresaTemporal" runat="server" Width="115px" MaxLength="255"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_TelefonoEmpresaTemporal"
                                                runat="server" TargetControlID="TextBox_TelefonoEmpresaTemporal" FilterType="Numbers,Custom"
                                                ValidChars="()[]{}- extEXT" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="td_izq">
                                            <asp:Label ID="Label2" runat="server" Text="Tipo Contrato"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_TipoContrato" runat="server" Width="165px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA_INGRESO" runat="server" Text="Fecha de ingreso"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_INGRESO" runat="server" Width="130px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA_INGRESO" runat="server"
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_FECHA_INGRESO" />
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA_RETIRO" runat="server" Text="Fecha de retiro"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_RETIRO" runat="server" Width="115px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA_RETIRO" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="TextBox_FECHA_RETIRO" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_ULTIMO_CARGO" runat="server" Text="Ultimo cargo desempeñado"></asp:Label>
                                        </td>
                                        <td colspan="6" class="td_der">
                                            <asp:TextBox ID="TextBox_ULTIMO_CARGO" runat="server" MaxLength="255" CssClass="textbox_ajustado"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="td_izq">
                                            Ultimo salario
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ULTIMO_SALARIO" runat="server" Width="165px" MaxLength="255"
                                                CssClass="money"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Comisiones
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_Comisiones" runat="server" Width="130px" MaxLength="255"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Bonos
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_Bonos" runat="server" Width="115px" MaxLength="255"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td style="text-align: center;">
                                            Motivo del Retiro<br />
                                            <asp:TextBox ID="TextBox_MotivoRetiro" runat="server" ValidationGroup="ADICIONAR"
                                                TextMode="MultiLine" Height="100px" Width="830px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_EMPRESA_TRABAJO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_EMPRESA_TRABAJO"
                                    ControlToValidate="TextBox_EMPRESA_TRABAJO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA DONDE TRABAJÓ es requerida."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_EMPRESA_TRABAJO"
                                    TargetControlID="RequiredFieldValidator_EMPRESA_TRABAJO" HighlightCssClass="validatorCalloutHighlight" />

                                <!--Textbox_numero_telefono-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Numero_Telefono"
                                    ControlToValidate="TextBox_Numero_Telefono" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El Número de Teléfono es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Numero_Telefono"
                                    TargetControlID="RequiredFieldValidator_TextBox_Numero_Telefono" HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_ULTIMO_SALARIO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ULTIMO_SALARIO"
                                    ControlToValidate="TextBox_ULTIMO_SALARIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ULTIMO SALARIO es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_ULTIMO_SALARIO"
                                    TargetControlID="RequiredFieldValidator_ULTIMO_SALARIO" HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ULTIMO_SALARIO"
                                    runat="server" TargetControlID="TextBox_ULTIMO_SALARIO" FilterType="Numbers,Custom"
                                    ValidChars=".," />
                                <!-- TextBox_FECHA_INGRESO -->
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FECHA_INGRESO" runat="server"
                                    TargetControlID="TextBox_FECHA_INGRESO" FilterType="Custom,Numbers" ValidChars="/">
                                </ajaxToolkit:FilteredTextBoxExtender>

                                <!-- TextBox_FECHA_RETIRO -->
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FECHA_RETIRO" runat="server"
                                    TargetControlID="TextBox_FECHA_RETIRO" FilterType="Custom,Numbers" ValidChars="/">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                
                                <!-- TextBox_ULTIMO_CARGO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ULTIMO_CARGO"
                                    ControlToValidate="TextBox_ULTIMO_CARGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El  ÚLTIMO CARGO DESEMPEÑADO es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_ULTIMO_CARGO"
                                    TargetControlID="RequiredFieldValidator_ULTIMO_CARGO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>

                            <asp:Panel ID="Panel_PREGUNTAS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Cuestionario
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_PREGUNTAS" runat="server" Width="866px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_PREGUNTA,ID_RESPUESTA">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cuestionario">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_PREGUNTA" runat="server" Text="Pregunta"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Respuestas">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_RESPUESTA" runat="server" ValidationGroup="ADICIONAR" TextMode="MultiLine"
                                                                Width="440px" Height="50px"></asp:TextBox>
                                                            <!-- TextBox_FECHA_RETIRO -->
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_RESPUESTA" runat="server"
                                                                ControlToValidate="TextBox_RESPUESTA" Display="None" ErrorMessage="Campo Requerido faltante</br>La RESPUESTA es requerida."
                                                                ValidationGroup="ADICIONAR" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_RESPUESTA"
                                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_RESPUESTA" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="150px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Calificaciones" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Califique al Candidato
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_CalificacionesReferencia" runat="server" Width="700px"
                                                            AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Cualidad">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label_Cualidad" runat="server" Text="Cualidad"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Calificación">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList_Calificacion" runat="server" ValidationGroup="ADICIONAR"
                                                                            Width="300PX">
                                                                        </asp:DropDownList>
                                                                        <!-- DropDownList_Calificacion -->
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Calificacion"
                                                                            runat="server" ControlToValidate="DropDownList_Calificacion" Display="None" ErrorMessage="Campo Requerido faltante</br>La CALIFICACIÓN es requerida."
                                                                            ValidationGroup="ADICIONAR" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Calificacion"
                                                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Calificacion" />
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
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
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
                                            <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nueva" CssClass="margin_botones"
                                                ValidationGroup="NUEVO" OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
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
            <asp:PostBackTrigger ControlID="Button_IMPRIMIR_1" />
            <asp:PostBackTrigger ControlID="Button_IMPRIMIR" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
