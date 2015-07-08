<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="acoset.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    
    
    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />       
    <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

    <asp:HiddenField ID="HiddenField_REGISTRO_ACOSET" runat="server" />

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" style="text-align:center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" 
                        onclick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

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
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO_REGISTRO_ACOSET" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_HABILITAR_SUBIDA_MASIVA" runat="server" 
                                        Text="Carga Masiva" CssClass="margin_botones"
                                        ValidationGroup="SUBIDAMASIVA" 
                                        onclick="Button_HABILITAR_SUBIDA_MASIVA_Click" />
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
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_BUSCAR" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label1" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="REGISTRO" AllowPaging="True" 
                            onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                            onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="TIPO DOCUMENTO IDENTIFICACIÓN">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="NÚMERO DE IDENTIFICACIÓN">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="REGISTRO" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="ESTADO_ACOSET" HeaderText="Estado">
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
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                ACOSET
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

                <asp:Panel ID="Panel_DATOS_CONTACTO" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Datos personales
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_TIPO_DOC" runat="server" Text="Tipo Documento Identidad"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_TIP_DOC" runat="server" Width="300px"
                                        ValidationGroup="GUARDAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_NUM_DOC" runat="server" Text="Numero Identidad"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NUM_DOC" runat="server" Width="300px" MaxLength="400" 
                                        ValidationGroup="GUARDAR"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <%--DropDownList_TIP_DOC--%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TIP_DOC"
                            ControlToValidate="DropDownList_TIP_DOC" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE DOCUMENTO es requerido."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TIP_DOC"
                            TargetControlID="RequiredFieldValidator_DropDownList_TIP_DOC" HighlightCssClass="validatorCalloutHighlight" />
                        <%--TextBox_NUM_DOC--%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NUM_DOC"
                            ControlToValidate="TextBox_NUM_DOC" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NÚMERO DE DOCUMENTO es requerido."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUM_DOC"
                            TargetControlID="RequiredFieldValidator_TextBox_NUM_DOC" HighlightCssClass="validatorCalloutHighlight" />
                        <div class="div_espaciador">
                        </div>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_NOMBRES" runat="server" Text="Nombres"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NOMBRES" runat="server" Width="300px" MaxLength="50" ValidationGroup="GUARDAR"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_APELLIDOS" runat="server" Text="Apellidos"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_APELLIDOS" runat="server" Width="300px" MaxLength="400"
                                        ValidationGroup="GUARDAR" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRES"
                            ControlToValidate="TextBox_NOMBRES" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRES"
                            TargetControlID="RequiredFieldValidator_TextBox_NOMBRES" HighlightCssClass="validatorCalloutHighlight" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NOMBRES"
                            runat="server" TargetControlID="TextBox_NOMBRES" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                            ValidChars=" " />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_APELLIDOS"
                            ControlToValidate="TextBox_APELLIDOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El APELLIDO es requerido."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_APELLIDOS"
                            TargetControlID="RequiredFieldValidator_TextBox_APELLIDOS" HighlightCssClass="validatorCalloutHighlight" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_APELLIDOS"
                            runat="server" TargetControlID="TextBox_APELLIDOS" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                            ValidChars=" " />
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_ENTIDAD_REPORTA" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        Entidad que Origina Reporte
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Seleccione Entidad
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_ENTIDAD_REPORTA" runat="server"
                                        Width="400px" ValidationGroup="GUARDAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <%--DropDownList_ENTIDAD_REPORTA--%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ENTIDAD_REPORTA"
                            ControlToValidate="DropDownList_ENTIDAD_REPORTA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD QUE REPORTA es requerida."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_REPORTA"
                            TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_REPORTA" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_MOTIVO_REPORTE" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        Motivo Reporte
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:TextBox ID="TextBox_MOTIVO_REPORTE" runat="server" Height="105px" 
                                        TextMode="MultiLine" Width="762px" ValidationGroup="GUARDAR"></asp:TextBox>
                                </td>


                            </tr>
                        </table>
                        <%--TextBox_MOTIVO_REPORTE--%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_MOTIVO_REPORTE"
                            ControlToValidate="TextBox_MOTIVO_REPORTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO es requerido."
                            ValidationGroup="GUARDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_MOTIVO_REPORTE"
                            TargetControlID="RequiredFieldValidator_TextBox_MOTIVO_REPORTE" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_ESTADO_REGISTRO" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        Estado del Registro
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Estado
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_ESTADO_REGISTRO" runat="server" 
                                                ValidationGroup="GUARDAR" AutoPostBack="True" 
                                                onselectedindexchanged="DropDownList_ESTADO_REGISTRO_SelectedIndexChanged">
                                                <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                <asp:ListItem Value="True">ACTIVO</asp:ListItem>
                                                <asp:ListItem Value="False">INACTIVO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <%--DropDownList_ESTADO_REGISTRO--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ESTADO_REGISTRO"
                                    ControlToValidate="DropDownList_ESTADO_REGISTRO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ESTADO_REGISTRO"
                                    TargetControlID="RequiredFieldValidator_DropDownList_ESTADO_REGISTRO" HighlightCssClass="validatorCalloutHighlight" />

                                <asp:Panel ID="Panel_MOTIVO_ESTADO" runat="server">
                                    <div class="div_espaciador"></div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox_MOTIVO_ESTADO" runat="server" Height="105px" TextMode="MultiLine"
                                                    Width="762px" ValidationGroup="GUARDAR"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                     <%--TextBox_MOTIVO_ESTADO--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_MOTIVO_ESTADO"
                                        ControlToValidate="TextBox_MOTIVO_ESTADO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO DE LA INACTIVACIÓN es requerido."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_MOTIVO_ESTADO"
                                        TargetControlID="RequiredFieldValidator_TextBox_MOTIVO_ESTADO" HighlightCssClass="validatorCalloutHighlight" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_SUBIDA_NASIVA" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Subida Masiva de Reportes Acoset
            </div>
            <div class="div_contenido_groupbox">
                
                <asp:Panel ID="Panel_INFO_DATOS_SUBIDA_MASIVA" runat="server">
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:Label ID="Label_NUM_REGISTROS_ARCHIVO" runat="server" Font-Bold="True"></asp:Label>
                                registros Acoset encontrados en archivo plano.
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="Panel_GRILLA_ERRORES_SUBIDA_MASIVA" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Lista Errores en Archivo
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ERRORES_SUBIDA_MASIVA" runat="server" Width="865px" 
                                    AutoGenerateColumns="False" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging"
                                    OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="TIPO_ERROR" HeaderText="Tipo Error" />
                                        <asp:BoundField DataField="LINEA" HeaderText="Línea">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MENSAJE" HeaderText="Mensaje">
                                        <ItemStyle CssClass="columna_grid_justificada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_FILEUPLOAD_ARCHIVO_PLANO" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload_ARCHIVO_PLANO" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="Button_CONFIRMAR_INFO_aRCHIVP_PLANO" runat="server" Text="Verificar archivo"
                                    CssClass="margin_botones" 
                                    onclick="Button_CONFIRMAR_INFO_aRCHIVP_PLANO_Click" />
                            </td>
                            <td>
                                <asp:Button ID="Button_GUARDAR_REGISTROS_MASIVOS" runat="server" Text="Guardar" 
                                    onclick="Button_GUARDAR_REGISTROS_MASIVOS_Click" />
                            </td>
                            <td>
                                <asp:Button ID="Button_CANCELAR_SUBIDA_MASIVA" runat="server" Text="Cancelar" 
                                    onclick="Button_CANCELAR_SUBIDA_MASIVA_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
        <div class="div_espaciador"></div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo"  
                                        CssClass="margin_botones"
                                        ValidationGroup="NUEVO" onclick="Button_NUEVO_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="GUARDAR"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="Button_HABILITAR_SUBIDA_MASIVA_1" runat="server" Text="Carga Masiva" CssClass="margin_botones"
                                        ValidationGroup="SUBIDAMASIVA" onclick="Button_HABILITAR_SUBIDA_MASIVA_Click"/>
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button1" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
