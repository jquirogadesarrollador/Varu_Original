<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="cancelacionyCumplimientoRequisiciones.aspx.cs" Inherits="_Default"
    EnableEventValidation="false" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
            <asp:HiddenField ID="HiddenField_CUMPLIDO" runat="server" />
            <asp:HiddenField ID="HiddenField_CANCELADO" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" Style="height: 26px"
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
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
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
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="CheckBoxList_CANCELADO_CUMPLIDO" runat="server" RepeatDirection="Horizontal"
                                                TextAlign="Left">
                                                <asp:ListItem Value="CUMPLIDO">Cumplido</asp:ListItem>
                                                <asp:ListItem Value="CANCELADO">Cancelado</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td class="td_der">
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" ValidationGroup="BUSCAR_CLIENTE"
                                                CssClass="margin_botones" OnClick="Button_BUSCAR_Click" />
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

            <asp:Panel ID="Panel_GRID_RESULTADOS" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la Busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_REQUERIMIENTO,ID_EMPRESA" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Número Requerimiento">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="TIPO_REQ" HeaderText="Tipo" />
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Apertura" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_CIERRE" HeaderText="Fecha Cierre" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONTACTO" HeaderText="Contácto" />
                                        <asp:BoundField DataField="MAIL_CONTACTO" HeaderText="E-Mail" />
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
                        Infomación de la Requisición
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

                        <asp:Panel ID="Panel_DATOS_FORMULARIO" runat="server">
                            <asp:Panel ID="Panel_EMPRESA_FECHAS" runat="server">
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <div class="div_cabeza_groupbox_gris">
                                                Datos de Empresa</div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Tipo Requisición
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_TIP_REQ" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Empresa
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_ID_EMPRESA" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td valign="top" style="width: 50%;">
                                            <div class="div_cabeza_groupbox_gris">
                                                Fechas de requisición
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Fecha recibo
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_FECHA_R" runat="server" ValidationGroup="GUARDAR" Width="100px"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Fecha req cliente
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_FECHA_REQUERIDA" runat="server" ValidationGroup="GUARDAR"
                                                                Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_DATOS_ENVIO" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Datos de envio
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Nombre
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_ENVIO_CANDIDATOS" runat="server" Width="260px"
                                                                ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td rowspan="5" valign="top">
                                                            <div class="div_cabeza_groupbox_gris">
                                                                Condiciones de envio
                                                            </div>
                                                            <div class="div_contenido_groupbox_gris">
                                                                <asp:TextBox ID="TextBox_COND_ENVIO" runat="server" Height="105px" TextMode="MultiLine"
                                                                    Width="460px" MaxLength="250" ValidationGroup="GUARDAR"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Dirección
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_DIR_ENVIO" runat="server" Width="255px" Enabled="False"
                                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Ciudad
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_CIUDAD_ENVIO" runat="server" Width="255px" Enabled="False"
                                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            E-Mail
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_EMAIL_ENVIO" runat="server" Width="255px" Enabled="False"
                                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Teléfono
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_TEL_ENVIO" runat="server" Enabled="False" ValidationGroup="GUARDAR"
                                                                Width="255px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_CONTRATO_Y_SERVICIOS_RESPECTIVOS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Datos de servicio respectivo
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">
                                                                        Servicio respectivo #:
                                                                        <asp:Label ID="Label_ID_SERVICIO_RESPECTIVO" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <table style="margin: 0 5px 0 auto;">
                                                                            <tr>
                                                                                <td style="text-align: right">
                                                                                    Vence
                                                                                    <asp:TextBox ID="TextBox_FECHA_VENCE_SERVICIO" runat="server" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:TextBox ID="TextBox_DESCRIPCION_SERVICIO" runat="server" TextMode="MultiLine"
                                                                Width="665px" Height="80px" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_ESPECIFICACIONES_UBICACIONES" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <div class="div_cabeza_groupbox_gris">
                                                Cargo requerido y especificaciones
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Cargo
                                                        </td>
                                                        <td class="td_der" colspan="4">
                                                            <asp:DropDownList ID="DropDownList_PERFILES" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            edad mínima
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_EDAD_MIN" runat="server" Width="50px" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                        <td class="td_izq">
                                                            edad máxima
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_EDAD_MAX" runat="server" Width="50px" Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Personas<br />
                                                            solicitadas
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_CANTIDAD" runat="server" Width="50px" ValidationGroup="GUARDAR"
                                                                MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td class="td_izq">
                                                            sexo
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_SEXO" runat="server" Enabled="False" Width="50px" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Experiencia
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_EXPERIENCIA" runat="server" Width="255px" Enabled="False"
                                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Nivel<br />
                                                            académico
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_NIVEL_ACADEMICO" runat="server" Enabled="False" Width="255px"
                                                                ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            funciones a desarrollar<br />
                                                            <asp:TextBox ID="TextBox_FUNCIONES" runat="server" Width="385px" Height="60px" TextMode="MultiLine"
                                                                Enabled="False" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td valign="top" style="width: 50%;">
                                            <div class="div_cabeza_groupbox_gris">
                                                Ubicación
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Regional
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Departamento
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Ciudad
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_CIUDAD_REQ" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="div_espaciador">
                                                </div>
                                            </div>
                                            <div class="div_espaciador">
                                            </div>
                                            <div class="div_cabeza_groupbox_gris">
                                                Datos del contrato
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Duración
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_DURACION" runat="server" Width="255px" MaxLength="20" ValidationGroup="GUARDAR"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Horario de trabajo
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_HORARIO" runat="server" Width="255px" ValidationGroup="GUARDAR"
                                                                MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Salario ofrecido
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_SALARIO" runat="server" ValidationGroup="GUARDAR" MaxLength="9"
                                                                Width="255px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="div_espaciador">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_OBSERVACIONES" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" width="100%">
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <div class="div_cabeza_groupbox_gris">
                                                Adicionales (Observaciones)
                                            </div>
                                            <div class="div_contenido_groupbox_gris">
                                                Observaciones<br />
                                                <asp:TextBox ID="TextBox_OBS_REQUERIMIENTO" runat="server" TextMode="MultiLine" Width="779px"
                                                    Height="60px" ValidationGroup="GUARDAR" MaxLength="250"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_ACCIONES_REQUISICION" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox">
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_REQUISICION" runat="server" Text="Cancelar Requisicion"
                                                CssClass="margin_botones" ValidationGroup="NUEVO_CLIENTE" OnClick="Button_CANCELAR_REQUISICION_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CUMPLIR_REQUISICION" runat="server" Text="Cumplir Requisicion"
                                                CssClass="margin_botones" OnClick="Button_CUMPLIR_REQUISICION_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel_EfectividadOportunidad" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Oportunidad y Efectividad
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_ReqOportuna" runat="server" Text="Oportuna" Width="150px" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_ReqEfectiva" runat="server" Text="Efectiva" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_CANCELAR_REQUISICION" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Cancelar Requisición
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td style="text-align: center;">
                                    <asp:RadioButtonList ID="RadioButtonList_TIPO_CANCELACION" runat="server" RepeatDirection="Horizontal"
                                        ValidationGroup="CANCELARREQ" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList_TIPO_CANCELACION_SelectedIndexChanged">
                                        <asp:ListItem Value="CLIENTE">Por Cliente</asp:ListItem>
                                        <asp:ListItem Value="INTERNA">Cancelación Interna</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <!-- RadioButtonList_TIPO_CANCELACION -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_TIPO_CANCELACION"
                            ControlToValidate="RadioButtonList_TIPO_CANCELACION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CANCLACIÓN es requerido."
                            ValidationGroup="CANCELARREQ" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_TIPO_CANCELACION"
                            TargetControlID="RequiredFieldValidator_RadioButtonList_TIPO_CANCELACION" HighlightCssClass="validatorCalloutHighlight" />

                        <asp:Panel ID="Panel_CANCELACION_POR_CLIENTE" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Motivo
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_MOTIVO_CANCELACION" runat="server" ValidationGroup="CANCELARREQ">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_MOTIVO_CANCELACION -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_MOTIVO_CANCELACION"
                                ControlToValidate="DropDownList_MOTIVO_CANCELACION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO DE LA CANCELACIÓN es requerido."
                                ValidationGroup="CANCELARREQ" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_MOTIVO_CANCELACION"
                                TargetControlID="RequiredFieldValidator_DropDownList_MOTIVO_CANCELACION" HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>

                        <div class="div_espaciador">
                        </div>

                        <div style="text-align: center;">
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_CANCELACION" runat="server" Text="Guardar" ValidationGroup="CANCELARREQ"
                                            OnClick="Button_GUARDAR_CANCELACION_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_CUMPLIR_REQUISICION" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Cumplir Requisición
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Motivo
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_MOTIVO_CUMPLIR" runat="server" ValidationGroup="CUMPLIRREQ">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_MOTIVO_CUMPLIR -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_MOTIVO_CUMPLIR"
                            ControlToValidate="DropDownList_MOTIVO_CUMPLIR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO es requerido."
                            ValidationGroup="CUMPLIRREQ" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_MOTIVO_CUMPLIR"
                            TargetControlID="RequiredFieldValidator_DropDownList_MOTIVO_CUMPLIR" HighlightCssClass="validatorCalloutHighlight" />
                        <div class="div_espaciador">
                        </div>
                        <div style="text-align: center;">
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_CUMPLIR" runat="server" Text="Guardar" ValidationGroup="CUMPLIRREQ"
                                            OnClick="Button_GUARDAR_CUMPLIR_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
