<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="cargos.aspx.cs" Inherits="_Default" %>

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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField_ID_OCUPACION" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

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
                                        <td rowspan="0">
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
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
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </td>
                    </tr>
                </table>
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
                                    DataKeyNames="ID_OCUPACION,ID_EMP,COD_OCUPACION,COD_INTERNO" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="ID_EMP" HeaderText="ID_EMP" Visible="False" />
                                        <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="COD_INTERNO" HeaderText="COD_INTERNO" Visible="False" />
                                        <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo de trabajo" />
                                        <asp:BoundField DataField="TIPO_CARGO" HeaderText="Tipo" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                        <asp:BoundField DataField="DSC_FUNCIONES" HeaderText="Funciones">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO_CARGO" HeaderText="Estado">
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
            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información del Cargo
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
                        <asp:Panel ID="Panel_TIPO_CARGO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Tipo de Cargo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadioButtonList_TIPO_GARGO" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList_TIPO_GARGO_SelectedIndexChanged"
                                                ValidationGroup="GUARDAR">
                                                <asp:ListItem Value="GENERICO">Generico</asp:ListItem>
                                                <asp:ListItem Value="CON EMPRESA">Asociado a Empresa</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- RadioButtonList_TIPO_GARGO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_TIPO_GARGO"
                                    ControlToValidate="RadioButtonList_TIPO_GARGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_TIPO_GARGO"
                                    TargetControlID="RequiredFieldValidator_RadioButtonList_TIPO_GARGO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_EMPRESA" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Empresa Usuaria
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Nombre empresa
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_EMPRESA_USUARIA" runat="server" ValidationGroup="GUARDAR"
                                                Width="500px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_EMPRESA_USUARIA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EMPRESA_USUARIA"
                                    ControlToValidate="DropDownList_EMPRESA_USUARIA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA USUARIA es requerida."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EMPRESA_USUARIA"
                                    TargetControlID="RequiredFieldValidator_DropDownList_EMPRESA_USUARIA" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CARGOS_DANE" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Cargos DANE
                            </div>
                            <div class="div_contenido_groupbox_gris">

                                <asp:Panel ID="Panel_BuscadorCargo" runat="server">
                                    <div class="div_avisos_importantes">
                                        Debe realizar la busqueda del cargo DANE; escriba el nombre del cargo, oprima el
                                        botón BUSCAR, y luego utilice la lista de CARGOS ENCONTRADOS para seleccionar el
                                        cargo.
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div style="background-color: #eeeeee; border: 1px solid #000000; width: 550px; margin: 0 auto;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Buscador
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                        Width="350px" MaxLength="15"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_BUSCADOR_CARGO" runat="server" Text="Buscar" CssClass="margin_botones"
                                                        OnClick="Button_BUSCADOR_CARGO_Click" ValidationGroup="BUSCADORCARGO" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_BUSCADOR_CARGO -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_BUSCADOR_CARGO" runat="server"
                                            ControlToValidate="TextBox_BUSCADOR_CARGO" Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="BUSCADORCARGO" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_BUSCADOR_CARGO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_BUSCADOR_CARGO" />
                                    </div>
                                    <div class="div_espaciador"></div>
                                </asp:Panel>

                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Cargos DANE:</td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_GRUPOS_PRIMARIOS" runat="server" ValidationGroup="GUARDAR"
                                                Width="660px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_GRUPOS_PRIMARIOS -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_GRUPOS_PRIMARIOS"
                                    ControlToValidate="DropDownList_GRUPOS_PRIMARIOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El GRUPO PRIMARIO es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_GRUPOS_PRIMARIOS"
                                    TargetControlID="RequiredFieldValidator_DropDownList_GRUPOS_PRIMARIOS" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_DATOS_BASICOS_CARGO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Datos Básicos del Cargo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Nombre
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NOMBRE_CARGO_NUEVO" runat="server" Width="700px" MaxLength="250"
                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="div_espaciador">
                                            </div>
                                            Funciones<br />
                                            <asp:TextBox ID="TextBox_FUNCIONES_CARGO" runat="server" Width="760px" TextMode="MultiLine"
                                                Height="95px" MaxLength="250" ValidationGroup="GUARDAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="div_espaciador">
                                            </div>
                                            Responsabilidades<br />
                                            <asp:TextBox ID="TextBox_RESPONSABILIDADES" runat="server" Width="760px" TextMode="MultiLine"
                                                Height="95px" MaxLength="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="div_espaciador">
                                            </div>
                                            Obligaciones<br />
                                            <asp:TextBox ID="TextBox_OBLIGACIONES" runat="server" Width="760px" TextMode="MultiLine"
                                                Height="95px" MaxLength="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="CheckBox_COMISIONA" runat="server" Text="Comisiona" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_NOMBRE_CARGO_NUEVO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRE_CARGO_NUEVO"
                                    ControlToValidate="TextBox_NOMBRE_CARGO_NUEVO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DEL CARGO es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRE_CARGO_NUEVO"
                                    TargetControlID="RequiredFieldValidator_TextBox_NOMBRE_CARGO_NUEVO" HighlightCssClass="validatorCalloutHighlight" />
                                <!-- TextBox_FUNCIONES_CARGO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FUNCIONES_CARGO"
                                    ControlToValidate="TextBox_FUNCIONES_CARGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las FUNCIONES DEL CARGO son requeridas."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FUNCIONES_CARGO"
                                    TargetControlID="RequiredFieldValidator_TextBox_FUNCIONES_CARGO" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_ESTADO_CARGO" runat="server">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Estado
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_ESTADO" runat="server" ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_ESTADO -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ESTADO"
                                        ControlToValidate="DropDownList_ESTADO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ESTADO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_ESTADO" HighlightCssClass="validatorCalloutHighlight" />
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_ACCION_1" runat="server">
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
                                            <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button_SALIR_1" onclick="window.close();" type="button"
                                                value="Salir" />
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
