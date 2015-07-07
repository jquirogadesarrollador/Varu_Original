<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="ExamenesLaboratorio.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
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
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" OnClick="Button_NUEVO_Click"
                                                CssClass="margin_botones" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="ADICIONAR" OnClick="Button_GUARDAR_Click" />
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
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" ValidationGroup="BUSCAR_REFERENCIA"
                                                CssClass="margin_botones" OnClick="Button_BUSCAR_Click" />
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
            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                    AutoGenerateColumns="False" DataKeyNames="ID_PROVEEDOR" AllowPaging="True" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_PROVEEDOR" HeaderText="ID_PROVEEDOR" Visible="False" />
                                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razón social" />
                                        <asp:BoundField DataField="NATURALEZA_JURIDICA" HeaderText="Naturaleza jurídica" />
                                        <asp:BoundField DataField="NUMERO_IDENTIFICACION" HeaderText="Núm identificación">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="NOMBRE_CATEGORIA" HeaderText="Categoría" />
                                        <asp:BoundField DataField="CIUDAD_SECTOR" HeaderText="Ciudad" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_DATOS_PROVEEDOR_SELECCIONADO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información del proveedor selecionado
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Razón social
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NOMBRE_PROVEEDOR" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ID_PROVEEDOR" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Nombre
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NOMBRE_CATEGORIA" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ID_CATEGORIA" runat="server" Font-Bold="True" Visible ="false"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="td_izq">
                                    Regional
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_REGIONAL" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Ciudad
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CIUDAD" runat="server" Font-Bold="True" Visible ="false"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información de examenes asociados al proveedor
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_EXAMENES_ASOCIADOS_ACTUALMENTE" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Examenes asociados actualmente
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <asp:GridView ID="GridView_EXAMENES_POR_PROVEEDOR" runat="server" Width="866px" AutoGenerateColumns="False"
                                            DataKeyNames="ID_PRODUCTO,REGISTRO_P_P" AllowPaging="False" OnRowCommand="GridView_EXAMENES_POR_PROVEEDOR_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                    Text="Eliminar">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" Visible="False" />
                                                <asp:BoundField DataField="REGISTRO_P_P" HeaderText="REGISTRO_P_P" Visible="False" />
                                                <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Examen" HtmlEncode="False"
                                                    HtmlEncodeFormatString="False"></asp:BoundField>
                                                <asp:BoundField DataField="DESCRIPCION_PRODUCTO" HeaderText="Descripción" HtmlEncode="False"
                                                    HtmlEncodeFormatString="False">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BASICO" HeaderText="Examen básico" HtmlEncode="False"
                                                    HtmlEncodeFormatString="False">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="APLICA_A" HeaderText="Aplica a" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                            </Columns>
                                            <headerStyle BackColor="#DDDDDD" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_NUEVO_EXAMEN_PARA_PROVEEDOR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Asociar exámen médico a proveedor
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Seleccionar examen
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_EXAMENES" runat="server" Width="260px" ValidationGroup="ADICIONAR_EXAMEN">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_EXAMENES -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_EXAMENES" runat="server"
                                    ControlToValidate="DropDownList_EXAMENES" Display="None" ErrorMessage="Campo Requerido faltante</br>El EXAMEN es requerido."
                                    ValidationGroup="ADICIONAR_EXAMEN" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_EXAMENES"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_EXAMENES" />
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_INGRESAR_EXAMEN_A_GRILLA" runat="server" Text="Adicionar exámen"
                                                CssClass="margin_botones" ValidationGroup="ADICIONAR_EXAMEN" OnClick="Button_INGRESAR_EXAMEN_A_GRILLA_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_CANCELAR_INGRESO" runat="server" Text="Cancelar" ValidationGroup="CANCELAR_EXAMEN"
                                                CssClass="margin_botones" OnClick="Button_CANCELAR_INGRESO_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                                            <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" OnClick="Button_NUEVO_Click"
                                                CssClass="margin_botones" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
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

