<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="descartePersonal.aspx.cs" Inherits="_Default" %>

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
            <asp:Panel ID="Panel_botones_accion" runat="server">
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
                                            <asp:Button ID="Button3" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button4" onclick="window.close();" type="button"
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
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </td>
                    </tr>
                </table>
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
                                        OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                        AutoGenerateColumns="False" DataKeyNames="ID_SOLICITUD,ID_REQUERIMIENTO,NUM_DOC_IDENTIDAD">
                                        <Columns>
                                            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                                SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                            <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                            <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identificación">
                                                <ItemStyle CssClass="columna_grid_der" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SEXO" HeaderText="Sexo" />
                                            <asp:BoundField DataField="ARCHIVO" HeaderText="Estado">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID" Visible="False">
                                                <ItemStyle CssClass="columna_grid_der" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        </Columns>
                                        <headerStyle BackColor="#DDDDDD" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos de la Solicitud de Ingreso
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

                        <asp:Panel ID="Panel_ID_SOLICITUD" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Código de Identificación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_ID_SOLICITUD" runat="server" Text="Código"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID_SOLICITUD" runat="server" ReadOnly="True" ValidationGroup="CODIGO"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_DATOS_SOLICITUD" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Datos de la Solicitud
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_cabeza_groupbox_gris">
                                    Fecha y Estado
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Fecha de Ingreso
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_FECHA_R" runat="server" ValidationGroup="GUARDAR" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA_R" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="TextBox_FECHA_R" />
                                            </td>
                                            <asp:Panel ID="Panel_ESTADO_ASPIRANTE" runat="server">
                                                <td class="td_izq">
                                                    Estado Aspirante
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_ESTADO_ASPIRANTE" runat="server"></asp:TextBox>
                                                </td>
                                            </asp:Panel>
                                        </tr>
                                    </table>
                                </div>

                                <div class="div_espaciador">
                                </div>

                                <div class="div_cabeza_groupbox_gris">
                                    Datos Básicos del Aspirante
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Apellidos
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_APELLIDOS" runat="server" ValidationGroup="GUARDAR" Width="230px"></asp:TextBox>
                                            </td>
                                            <td class="td_izq">
                                                Nombres
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_NOMBRES" runat="server" ValidationGroup="GUARDAR" Width="230px"></asp:TextBox>
                                            </td>
                                            <td class="td_izq">
                                                F. Nacimiento
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_FCH_NACIMIENTO" runat="server" ValidationGroup="GUARDAR"
                                                    MaxLength="10" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FCH_NACIMIENTO" runat="server"
                                                    Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_NACIMIENTO" />
                                            </td>
                                        </tr>
                                    </table>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Sexo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_SEXO" runat="server" ValidationGroup="GUARDAR"
                                                    AutoPostBack="True" Width="100px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="div_espaciador">
                                </div>

                                <table class="table_control_registros">
                                    <tr>
                                        <td style="width: 50%;" valign="top">
                                            <div class="div_cabeza_groupbox_gris">
                                                Identificación Aspirante
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Doc. Identidad
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_TIP_DOC_IDENTIDAD" runat="server" ValidationGroup="GUARDAR"
                                                                Width="260px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Número
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_NUM_DOC_IDENTIDAD" runat="server" ValidationGroup="GUARDAR"
                                                                Width="255px" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Departamento
                                                        </td>
                                                        <td class="td_der" colspan="3">
                                                            <asp:DropDownList ID="DropDownList_DEPARTAMENTO_CEDULA" runat="server" ValidationGroup="GUARDAR"
                                                                Width="260px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Ciudad
                                                        </td>
                                                        <td class="td_der" colspan="3">
                                                            <asp:DropDownList ID="DropDownList_CIU_CEDULA" runat="server" ValidationGroup="GUARDAR"
                                                                Width="260px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="Panel_LIB_MILITAR" runat="server" Visible="false">
                                                            <td class="td_izq">
                                                                Libreta militar
                                                            </td>
                                                            <td class="td_der" colspan="3">
                                                                <asp:TextBox ID="TextBox_LIB_MILITAR" runat="server" ValidationGroup="GUARDAR" Width="255px"
                                                                    MaxLength="15"></asp:TextBox>
                                                            </td>
                                                            <!-- TextBox_LIB_MILITAR -->
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_LIB_MILITAR" runat="server"
                                                                ControlToValidate="TextBox_LIB_MILITAR" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE LA LIBRETA MILITAR es requerido."
                                                                ValidationGroup="GUARDAR" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_LIB_MILITAR"
                                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_LIB_MILITAR" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LIB_MILITAR"
                                                                runat="server" TargetControlID="TextBox_LIB_MILITAR" FilterType="Numbers" />
                                                        </asp:Panel>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="div_espaciador">
                                            </div>

                                            <div class="div_cabeza_groupbox_gris">
                                                Datos de Contácto y Adicionales
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Teléfono
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_TEL_ASPIRANTE" runat="server" ValidationGroup="GUARDAR"
                                                                Width="255px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            E-Mail
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_E_MAIL" runat="server" ValidationGroup="GUARDAR" Width="255px"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Cat. Conducción
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_CAT_LIC_COND" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="div_espaciador">
                                            </div>

                                            <div class="div_cabeza_groupbox_gris">
                                                Estudios
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td valign="top" class="td_izq">
                                                            Nivel de Estudios
                                                        </td>
                                                        <td valign="top" class="td_der">
                                                            <asp:DropDownList ID="DropDownList_NIV_EDUCACION" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td style="width: 50%;" valign="top">
                                            <div class="div_cabeza_groupbox_gris">
                                                Ubicación Aspirante
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Dirección
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_DIR_ASPIRANTE" runat="server" ValidationGroup="GUARDAR"
                                                                Width="255px" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Departamento
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_DEPARTAMENTO_ASPIRANTE" runat="server" Width="260px"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Ciudad
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_CIU_ASPIRANTE" runat="server" Width="260px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Sector
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_SECTOR" runat="server" ValidationGroup="GUARDAR" Width="255px"
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                            <div class="div_espaciador">
                                            </div>

                                            <div class="div_cabeza_groupbox_gris">
                                                Cargo del Aspirante
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <div style="border: 1px solid #cccccc; background-color: #dddddd; padding: 5px; margin: 5px;">
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td class="td_izq">
                                                                Buscador
                                                            </td>
                                                            <td class="td_der">
                                                                <asp:TextBox ID="TextBox_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                                    Width="220px" MaxLength="15"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="Button_BUSCADOR_CARGO" runat="server" Text="Buscar" CssClass="margin_botones"
                                                                    ValidationGroup="BUSCADORCARGO" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table_control_registros">
                                                        <tr>
                                                            <td>
                                                                Cargos encontrados<br />
                                                                <asp:DropDownList ID="DropDownList_ID_OCUPACION" runat="server" Width="370px" ValidationGroup="GUARDAR">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <div class="div_espaciador">
                                                </div>

                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Área de
                                                            <br />
                                                            Especialización
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_AREAS_ESPECIALIZACION" runat="server" ValidationGroup="GUARDAR"
                                                                Width="260px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Aspiración
                                                            <br />
                                                            Salarial
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_ASPIRACION_SALARIAL" runat="server" ValidationGroup="GUARDAR"
                                                                Width="255px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Fuente
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_ID_FUENTE" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BITACORA_HOJA" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Bitacora de Hoja de Vida
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_BITACORA" runat="server" AutoGenerateColumns="False" DataKeyNames="REGISTRO,ID_SOLICITUD"
                                    Width="885px">
                                    <Columns>
                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="FECHA_R" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha registro">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLASE_REGISTRO" HeaderText="Clase" />
                                        <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios" />
                                        <asp:BoundField DataField="FCH_CRE" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha creación">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_CRE" HeaderText="Usuario creación" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Descarte_Entrevista" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Descarte En Contratación
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros" cellpadding="0">
                            <tr>
                                <td class="td_izq">
                                    Descartado por:
                                </td>
                                <td class="td_der">
                                    <asp:RadioButton ID="Cliente" Text="Cliente" runat="server" GroupName="grupo1" />
                                </td>
                                <td class="td_izq">
                                    <asp:RadioButton ID="Cuenta" Text="Apertura de Cuenta" runat="server" GroupName="grupo1" />
                                </td>
                                <td class="td_izq">
                                    <asp:RadioButton ID="Examenes" Text="Resultado de Examenes" runat="server" GroupName="grupo1" />
                                </td>
                                <td class="td_izq">
                                    <asp:RadioButton ID="firma" Text="No Firmó" runat="server" GroupName="grupo1" />
                                </td>
                                <td class="td_izq">
                                    <asp:RadioButton ID="Otros" Text="Otros" runat="server" GroupName="grupo1" />
                                </td>
                            </tr>
                        </table>

                        <div class="div_espaciador">
                        </div>

                        <table class="table_control_registros" cellpadding="0" cellspacing="0">
                            <tr>
                                 <td>
                                    Tipo Descarte:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList_TipoDescarte" runat="server" ValidationGroup="EFECTIVIDAD" 
                                        RepeatDirection="Horizontal">
                                        
                                        <asp:ListItem Value="+">Positivo (+)</asp:ListItem>
                                        <asp:ListItem Value="-">Negativo (-)</asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>

                        <!--TextBox_comentarios_EntreRadioButtonList_TipoDescartevista -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_TipoDescarte" ControlToValidate="RadioButtonList_TipoDescarte"
                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE DESCARTE es requerido."
                            ValidationGroup="EFECTIVIDAD" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_TipoDescarte"
                            TargetControlID="RequiredFieldValidator_RadioButtonList_TipoDescarte" HighlightCssClass="validatorCalloutHighlight" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros" cellpadding="0">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td style="text-align:justify;">
                                        Comentarios:<br />
                                        <asp:TextBox ID="TextBox_comentarios_Entrevista" runat="server" Width="600px" ValidationGroup="EFECTIVIDAD"
                                            TextMode="MultiLine" Height="80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button5" runat="server" Text="Realizar Descarte" CssClass="margin_botones"
                                            OnClick="Button_ADICIONAR_ENTREVISTA_Click" 
                                            ValidationGroup="EFECTIVIDAD" />
                                    </td>
                                </tr>
                            </table>
                        </table>
                        <!--TextBox_comentarios_Entrevista -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Comentarios" ControlToValidate="TextBox_comentarios_Entrevista"
                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El COMENTARIO es requerido."
                            ValidationGroup="EFECTIVIDAD" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Comentarios"
                            TargetControlID="RequiredFieldValidator_TextBox_Comentarios" HighlightCssClass="validatorCalloutHighlight" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Comentarios" runat="server"
                            TargetControlID="TextBox_comentarios_Entrevista" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                            ValidChars=" ÑñÁÉÍÓÚáéíóú1234567890()[]-_" />
                    </div>
                </div>
            </asp:Panel>
            
            <asp:Panel ID="Panel_FORM_BOTONES_1" runat="server">
                <div class="div_espaciador"></div>
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
                                            <asp:Button ID="Button_ENTREVISTA" runat="server" Text="Descartar" CssClass="margin_botones"
                                                OnClick="Button_Entrevista_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button_CERRAR_1" type="button" value="Salir" onclick="window.close();" />
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
