<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="ccf.aspx.cs" Inherits="contratacion_ccf" %>

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


    <asp:UpdatePanel ID="UpdatePanel_procesamiento" runat="server">
        <ContentTemplate>

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



            <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
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
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" />
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

            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                    AutoGenerateColumns="False" DataKeyNames="id">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="nit" HeaderText="Nit">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="digito_verificacion" HeaderText="Digito verificación">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="ACTIVO" HeaderText="Activo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>


            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información Entidad
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Control de Registro
                            </div>
                            <div class="div_contenido_groupbox_gris">
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
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_DATOS" runat="server">
                            <table class="table_align_izq" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div class="div_cabeza_groupbox_gris">
                                            Información Entidad
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                    </td>
                                                    <td class="td_der">
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_ID" runat="server" Text="ID"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_ID" runat="server" Width="250px" MaxLength="255" ValidationGroup="ADICIONAR"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_NOM_ENTIDAD" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_NOM_ENTIDAD" runat="server" Width="250px" MaxLength="30"
                                                            ValidationGroup="ADICIONAR"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_NIT" runat="server" Text="Nit"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_NIT" runat="server" Width="250px" MaxLength="9" ValidationGroup="ADICIONAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_DV" runat="server" Text="DV"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DV" runat="server" Width="250px" MaxLength="1" ValidationGroup="ADICIONAR"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_COD_ENTIDAD" runat="server" Text="Código"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_COD_ENTIDAD" runat="server" Width="250px" MaxLength="6"
                                                            ValidationGroup="ADICIONAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_DIR_ENTIDAD" runat="server" Text="Dirección"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DIR_ENTIDAD" runat="server" ValidationGroup="GUARDAR" Width="250px"
                                                            MaxLength="20"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_TEL_ENTIDAD" runat="server" Text="Teléfono"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_TEL_ENTIDAD" runat="server" ValidationGroup="GUARDAR" Width="250px"
                                                            MaxLength="20"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_CONTACTO" runat="server" Text="Contactar a"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_CONTACTO" runat="server" ValidationGroup="GUARDAR" Width="250px"
                                                            MaxLength="30"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_CARGO" runat="server" Text="Cargo"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_CARGO" runat="server" Width="250px" MaxLength="20" ValidationGroup="ADICIONAR"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:CheckBox ID="CheckBox_ESTADO" runat="server" Text="Activo" TextAlign="Left" />
                                                    </td>
                                                    <td class="td_der">
                                                    </td>
                                                    <td class="td_izq">
                                                    </td>
                                                    <td class="td_der">
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- TextBox_NOM_ENTIDAD -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NOM_ENTIDAD"
                                                ControlToValidate="TextBox_NOM_ENTIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_NOM_ENTIDAD"
                                                TargetControlID="RequiredFieldValidator_NOM_ENTIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_DV -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DV" ControlToValidate="TextBox_DV"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El DIGITO DE VERIFICACION es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DV"
                                                TargetControlID="RequiredFieldValidator_DV" HighlightCssClass="validatorCalloutHighlight" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DV" runat="server"
                                                TargetControlID="TextBox_DV" FilterType="Numbers" />
                                            <!-- TextBox_COD_ENTIDAD -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_COD_ENTIDAD"
                                                ControlToValidate="TextBox_COD_ENTIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CODIGO ENTIDAD es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_COD_ENTIDAD"
                                                TargetControlID="RequiredFieldValidator_COD_ENTIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_NIT -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NIT" ControlToValidate="TextBox_NIT"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NIT es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                                TargetControlID="RequiredFieldValidator_NIT" HighlightCssClass="validatorCalloutHighlight" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NIT" runat="server"
                                                TargetControlID="TextBox_NIT" FilterType="Numbers" />
                                            <!-- TextBox_DIR_ENTIDAD -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DIR_ENTIDAD"
                                                ControlToValidate="TextBox_DIR_ENTIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DIRECCCION es requerida."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DIR_ENTIDAD"
                                                TargetControlID="RequiredFieldValidator_DIR_ENTIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_TEL_ENTIDAD -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TEL_ENTIDAD"
                                                ControlToValidate="TextBox_TEL_ENTIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TEL_ENTIDAD"
                                                TargetControlID="RequiredFieldValidator_TEL_ENTIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_CONTACTO -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CONTACTO" ControlToValidate="TextBox_CONTACTO"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CONTACTO es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CONTACTO"
                                                TargetControlID="RequiredFieldValidator_CONTACTO" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_CARGO -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CARGO" ControlToValidate="TextBox_CARGO"
                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO es requerido."
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CARGO"
                                                TargetControlID="RequiredFieldValidator_CARGO" HighlightCssClass="validatorCalloutHighlight" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="Panel_nivelCiudad" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Selección de Ciudades
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table>
                                    <tr>
                                        <td class="td_izq">
                                            Buscar en Grilla:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_BUscarEnGrillaCiudades" runat="server" Width="200px" ValidationGroup="BUSCAR_CIUDADES"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BuscarEnGrillaCiudades" runat="server" Text="Filtrar" OnClick="Button_BuscarEnGrillaCiudades_Click"
                                                ValidationGroup="BUSCAR_CIUDADES" CssClass="margin_botones" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_SeleccionarTodasLasCiudades" runat="server" Text="Seleccionar Todo"
                                                CssClass="margin_botones" OnClick="Button_SeleccionarTodasLasCiudades_Click"
                                                Width="130px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_LimpiarSeleccionCiudades" runat="server" Text="Limpiar Seleccianado"
                                                CssClass="margin_botones" Width="150px" OnClick="Button_LimpiarSeleccionCiudades_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_MostrarSoloCiudadesSeleccionadas" runat="server" Text="Mostrar Seleccionado"
                                                CssClass="margin_botones" Width="155px" OnClick="Button_MostrarSoloCiudadesSeleccionadas_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_BUscarEnGrillaCiudades -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUscarEnGrillaCiudades"
                                    ControlToValidate="TextBox_BUscarEnGrillaCiudades" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El Valor a Buscar es requerido."
                                    ValidationGroup="BUSCAR_CIUDADES" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUscarEnGrillaCiudades"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUscarEnGrillaCiudades" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView_Ciudades" runat="server" Width="840px" DataKeyNames="ID_REGIONAL,ID_DEPARTAMENTO,ID_CIUDAD"
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID_CIUDAD" HeaderText="Cód. Ciudad">
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_DEPARTAMENTO" HeaderText="Departamenteo">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Seleccionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox_Seleccion" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
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
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" />
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
