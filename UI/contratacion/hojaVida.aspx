<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="hojaVida.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 97%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
    </asp:Panel>
    <asp:Panel ID="Panel_MENSAJE_POPUP" runat="server">
        <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
            display: block;" />
        <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
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
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_SALIR" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel_busqueda" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="200px" ValidationGroup="BUSCAR_REFERENCIA"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="230px" ValidationGroup="BUSCAR_REFERENCIA"></asp:TextBox>
                                        </td>
                                        <td class="td_der">
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_BUSCAR" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="div_espaciador">
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_espacio_validacion_campos">
                <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_espaciador">
            </div>
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                </table>
            </div>
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                    AutoGenerateColumns="False" DataKeyNames="ID_SOLICITUD" AllowPaging="True" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo doc.">
                                            <ItemStyle CssClass="columna_grid_cntrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Núm identidad">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E_MAIL" HeaderText="E-mail" />
                                        <asp:BoundField DataField="DIR_ASPIRANTE" HeaderText="Dirección" />
                                        <asp:BoundField DataField="TEL_ASPIRANTE" HeaderText="Teléfono" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de trabajador seleccionado
            </div>
            <div class="div_contenido_groupbox">
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
                <asp:UpdatePanel ID="UpdatePanel_DATOS" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel_DATOS_PERSONA_SELECCIONDA" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Datos de Identificación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Id requerimiento:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_ID_REQUERIMIENTO" runat="server" Text="ID_REQUERIMIENTO" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="td_izq">
                                            Id solicitud:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_ID_SOLICITUD" runat="server" Text="ID_SOLICITUD" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="td_izq">
                                            Núm documento:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_NUM_DOCUMENTO_PERSONA_SELECCIONADA" runat="server" 
                                                Text="NUM_DOC_IDENTIDAD" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Nombre:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_NOMBRE_PERSONA_SELECCIONADA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Direccón:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_DIRECCION_PERSONA_SELECCIONADA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Teléfono:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_TELEFONO_PERSONA_SELECCIONADA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            E-Mail:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_MAIL_PERSONA_SELECCIONADA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_GRILLA_CONTRATOS" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Contratos relacionados
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_para_convencion_hoja_trabajo">
                                    <table class="tabla_alineada_derecha">
                                        <tr>
                                            <td>
                                                Contrato seleccionado
                                            </td>
                                            <td class="td_contenedor_colores_hoja_trabajo">
                                                <div class="div_color_azul">
                                                </div>
                                            </td>
                                            <td>
                                                Contrato Activo
                                            </td>
                                            <td class="td_contenedor_colores_hoja_trabajo">
                                                <div class="div_color_verde">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <asp:GridView ID="GridView_CONTRATOS" runat="server" Width="865px" AutoGenerateColumns="False"
                                            OnSelectedIndexChanged="GridView_CONTRATOS_SelectedIndexChanged" DataKeyNames="ID_CONTRATO,ID_EMPLEADO,ID_REQUERIMIENTO,CONTRATO_ESTADO,ID_PERFIL"
                                            AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView_CONTRATOS_PageIndexChanging">
                                            <Columns>
                                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                                    SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="ID_CONTRATO" HeaderText="Núm contrato" />
                                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="Núm empleado" />
                                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Núm requsición" />
                                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa cliente" />
                                                <asp:BoundField DataField="FECHA_INICIA" HeaderText="Fecha inicio" DataFormatString="{0:dd/M/yyyy}">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FCH_LIQ_CONT" HeaderText="Fecha Liq." DataFormatString="{0:dd/M/yyyy}">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CLASE_CONTRATO" HeaderText="Clase contrato" />
                                                <asp:BoundField DataField="TIPO_CONTRATO" HeaderText="Tipo contrato" />
                                                <asp:BoundField DataField="SALARIO" HeaderText="Salario">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONTRATO_ESTADO" HeaderText="Estado contrato" />
                                            </Columns>
                                            <headerStyle BackColor="#DDDDDD" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_AGRUPADOR_DATOS_INFORMATIVOS" runat="server">
                            <asp:Panel ID="Panel_INFO_IMPRESIONES_BASICAS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_IMPRESIONES_BASICAS" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Impresiones Básicas
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_IMPRESIONES_BASICAS" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_IMPRESIONES_BASICAS" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_IMPRESIONES_BASICAS" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:HiddenField ID="HiddenField_ID_CONTRATO" runat="server" />
                                    <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
                                    <asp:Panel ID="Panel_MENSAJE_IMPRESIONES_BASICAS" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJE_IMPRESIONES_BASICAS" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Cert_Laboral" runat="server" CssClass="margin_botones" Height="26px"
                                                    Text="Certificación Laboral" Width="188px" OnClick="Cert_Laboral_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_AUTOS" runat="server" Text="Autos Recomendación" CssClass="margin_botones"
                                                    OnClick="Button_AUTOS_Click" Height="26px" Width="188px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_solicitud_examenes" runat="server" CssClass="margin_botones"
                                                    Text="Solicitud exámenes" Height="26px" Width="188px" 
                                                    onclick="Button_solicitud_examenes_Click1" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_APERTURA_CUENTA" runat="server" Height="26px" Text="Apertura cuenta"
                                                    Width="188px" CssClass="margin_botones" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_ENTREGAS" runat="server" CssClass="margin_botones" OnClick="Button_ENTREGAS_Click"
                                                    Text="Entregas" Height="26px" Width="188px" />
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="Button_CONTRATO" runat="server" CssClass="margin_botones" OnClick="Button_CONTRATO_Click"
                                                    Text="Imprimir contrato" Height="26px" Width="188px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" CssClass="margin_botones" Height="26px" Text="Clausulas"
                                                    Width="188px" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="2">
                                                <div style="border: solid 1px #eeeeee">
                                                    <asp:Panel ID="Panel_SELECCION_IMPRESION_CONTRATO" runat="server">
                                                        <table class="table_control_registros">
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <b>Contrato:</b><asp:Label ID="Label_ClaseContrato" runat="server" Text="Tipo contrato"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="RadioButtonList_impresion_contrato" runat="server" RepeatDirection="Horizontal"
                                                                        ValidationGroup="IMPRIMIR_CON">
                                                                        <asp:ListItem Value="COMPLETO">Completo</asp:ListItem>
                                                                        <asp:ListItem Value="PREIMPRESO">Preimpreso</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBox_ConCarnet" runat="server" Text="Con Carnet" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Button ID="Button_ImprimrContrato1" runat="server" Text="Imprimir" ValidationGroup="IMPRIMIR_CON"
                                                                        OnClick="Button_ImprimrContrato1_Click" Style="height: 26px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!--RadioButtonList_impresion_contrato-->
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RadioButtonList_impresion_contrato"
                                                            runat="server" ControlToValidate="RadioButtonList_impresion_contrato" Display="None"
                                                            ErrorMessage="Campo Requerido faltante</br>El FORMATO DE IMPRESIÓN es requerido."
                                                            ValidationGroup="GUARDAR" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_RadioButtonList_impresion_contrato"
                                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_RadioButtonList_impresion_contrato" />
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_IMPRESIONES_BASICAS"
                                    runat="Server" TargetControlID="Panel_CONTENIDO_IMPRESIONES_BASICAS" ExpandControlID="Panel_CABEZA_IMPRESIONES_BASICAS"
                                    CollapseControlID="Panel_CABEZA_IMPRESIONES_BASICAS" Collapsed="false" TextLabelID="Label_AIMPRESIONES_BASICAS"
                                    ImageControlID="Image_IMPRESIONES_BASICAS" ExpandedText="(Ocultar detalles...)"
                                    CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                                    CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_AFILIACIONES" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_AFILIACIONES" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Afiliaciones
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_AFILIACIONES" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_AFILIACIONES" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_AFILIACIONES" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <div class="div_cabeza_groupbox_gris">
                                        Afiliación -Riesgos Laborales- (ARL)
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_ARP" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_ARP" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_ARP" runat="server" Width="851px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_ARP,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="ID_ARP" HeaderText="ID_ARP" Visible="False" />
                                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/M/yyyy}"
                                                            ItemStyle-CssClass="columna_grid_centrada">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" ItemStyle-CssClass="columna_grid_jus">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Afiliación -Entidad promorora de salud- (EPS)
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_EPS" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_EPS" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_EPS" runat="server" Width="851px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_EPS,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="ID_EPS" HeaderText="ID_EPS" Visible="False" />
                                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/M/yyyy}"
                                                            ItemStyle-CssClass="columna_grid_centrada">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" ItemStyle-CssClass="columna_grid_jus">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Afiliación -Caja de compensación familiar- (CCF)
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_CCF" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_CCF" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_CCF" runat="server" Width="851px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_CAJA_C,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="ID_CAJA_C" HeaderText="ID_EPS" Visible="False" />
                                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/M/yyyy}"
                                                            ItemStyle-CssClass="columna_grid_centrada">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" ItemStyle-CssClass="columna_grid_jus">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Afiliación -Administradora de fondos y pensiones- (AFP)
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_AFP" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_AFP" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_AFP" runat="server" Width="851px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_F_PENSIONES,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="ID_F_PENSIONES" HeaderText="ID_EPS" Visible="False" />
                                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/M/yyyy}"
                                                            ItemStyle-CssClass="columna_grid_centrada">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" ItemStyle-CssClass="columna_grid_jus">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_AFILIACIONES"
                                    runat="Server" TargetControlID="Panel_CONTENIDO_AFILIACIONES" ExpandControlID="Panel_CABEZA_AFILIACIONES"
                                    CollapseControlID="Panel_CABEZA_AFILIACIONES" Collapsed="True" TextLabelID="Label_AFILIACIONES"
                                    ImageControlID="Image_AFILIACIONES" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_ENTREGAS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_ENTREGAS" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Entregas
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_ENTREGAS" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_ENTREGAS" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_ENTREGAS" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJE_ENTREGAS" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJE_ENTREGAS" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_ENTREGAS" runat="server" Width="867px" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="ID_LOTE" HeaderText="ID_LOTE" Visible="False" />
                                                    <asp:BoundField DataField="ID_BODEGA" HeaderText="ID_BODEGA" Visible="False" />
                                                    <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                                    <asp:BoundField DataField="ID_DOCUMENTO" HeaderText="ID_DOCUMENTO" Visible="False" />
                                                    <asp:BoundField DataField="ID_INVENTARIO" HeaderText="ID_INVENTARIO" Visible="False" />
                                                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" Visible="False" />
                                                    <asp:BoundField DataField="NOMBRE_PRODUCTO" HeaderText="Producto" />
                                                    <asp:BoundField DataField="DESCRIPCION_PRODUCTO" HeaderText="Descripción">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CANTIDAD_ENTREGADA" HeaderText="Cantidad">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                                    <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa cliente" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ENTREGAS" runat="Server"
                                    TargetControlID="Panel_CONTENIDO_ENTREGAS" ExpandControlID="Panel_CABEZA_ENTREGAS"
                                    CollapseControlID="Panel_CABEZA_ENTREGAS" Collapsed="True" TextLabelID="Label_ENTREGAS"
                                    ImageControlID="Image_ENTREGAS" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_JURIDICA" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_JURIDICA" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Situación jurídica
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_JURIDICA" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_JURIDICA" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_JURIDICA" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <div class="div_cabeza_groupbox_gris">
                                        Actas de descargo y procesos disciplinarios
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_ACTAS_DESCARGO" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_ACTAS_DESCARGO" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_ACTAS_DESCARGO" runat="server" Width="853px" AutoGenerateColumns="False"
                                                    DataKeyNames="REGISTRO,ID_SOLICITUD,ID_EMPLEADO">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="Núm Acta" />
                                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                        <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_ACTA" HeaderText="Fecha acta" DataFormatString="{0:dd/M/yyyy}">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_CIERRE" HeaderText="Fecha cierre" DataFormatString="{0:dd/M/yyyy}">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" />
                                                        <asp:BoundField DataField="TIPO_PROCESO" HeaderText="Resultado" />
                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción resultado">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Tutelas
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_TUTELAS" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_TUTELAS" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_TUTELAS" runat="server" Width="853px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_TUTELA">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_TUTELA" HeaderText="Núm tutela" />
                                                        <asp:BoundField DataField="FECHA_NOTIFICACION" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha notificación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_PLAZO_CONTESTA" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Plazo para contestar">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_TERMINA_PROCESO" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Fecha fin proceso">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PETICION" HeaderText="Petición">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FALLO" HeaderText="Fallo">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOMBRES_ABOGADO" HeaderText="Responsable caso" />
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Demandas
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_DEMANDAS" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_DEMANDAS" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_DEMANDAS" runat="server" Width="853px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_DEMANDA">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_DEMANDA" HeaderText="Núm demanda" />
                                                        <asp:BoundField DataField="RADICADO" HeaderText="Radicado" />
                                                        <asp:BoundField DataField="NOMBRE_TIPO_PROCESO" HeaderText="Tipo"></asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_NOTIFICACION" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha notificación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_PLAZO_CONTESTA" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Plazo de contestación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_CONTESTACION" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fech contestación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_TERMINA_PROCESO" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Fecha fin proceso">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DEMANDANTE" HeaderText="Demandante" />
                                                        <asp:BoundField DataField="DEMANDADO" HeaderText="Demandado" />
                                                        <asp:BoundField DataField="PRETENSIONES" HeaderText="Pretenciones" />
                                                        <asp:BoundField DataField="RESULTADO_PROCESO" HeaderText="Resultado" />
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Derechos de petición
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:Panel ID="Panel_MENSAJE_DERECHOS_PETICION" runat="server">
                                            <div class="div_espacio_validacion_campos">
                                                <asp:Label ID="Label_MENSAJE_DERECHOS_PETICION" runat="server" ForeColor="Red" />
                                            </div>
                                        </asp:Panel>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_DERECHOS_PETICION" runat="server" Width="853px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_DERECHO">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_DERECHO" HeaderText="Núm derecho" />
                                                        <asp:BoundField DataField="NOMBRE_MOTIVO" HeaderText="Motivo" />
                                                        <asp:BoundField DataField="FECHA_NOTIFICACION" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha notificación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_PLAZO_CONTESTA" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Plazo de contestación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_CONTESTACION" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha contestación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_TERMINA_PROCESO" DataFormatString="{0:dd/M/yyyy}"
                                                            HeaderText="Fecha fin proceso">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                                        <asp:BoundField DataField="SOLICITANTE" HeaderText="Solicitante" />
                                                        <asp:BoundField DataField="CONSIDERACIONES" HeaderText="Consideraciones" />
                                                        <asp:BoundField DataField="NOMBRES_ABOGADO" HeaderText="Abogado encargado" />
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_JURIDICA" runat="Server"
                                    TargetControlID="Panel_CONTENIDO_JURIDICA" ExpandControlID="Panel_CABEZA_JURIDICA"
                                    CollapseControlID="Panel_CABEZA_JURIDICA" Collapsed="True" TextLabelID="Label_JURIDICA"
                                    ImageControlID="Image_JURIDICA" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_INCAPACIDADES" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_INCAPACIDADES" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Incapacidades
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_INCAPACIDADES" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_INCAPACIDADES" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_INCAPACIDADES" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJE_INCAPACIDADES" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJE_INCAPACIDADES" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_INCAPACIDADES" runat="server" Width="867px" AutoGenerateColumns="False"
                                                DataKeyNames="REGISTRO">
                                                <Columns>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="Núm incapacidad">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE_CLASE_INCA" HeaderText="Clase" />
                                                    <asp:BoundField DataField="NOMBRE_TIPO_INCA" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="FECHA" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DSC_DIAG" HeaderText="Diagnóstico" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_INCAPACIDADES"
                                    runat="Server" TargetControlID="Panel_CONTENIDO_INCAPACIDADES" ExpandControlID="Panel_CABEZA_INCAPACIDADES"
                                    CollapseControlID="Panel_CABEZA_INCAPACIDADES" Collapsed="True" TextLabelID="Label_INCAPACIDADES"
                                    ImageControlID="Image_INCAPACIDADES" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_PAGOS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_PAGOS" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Pagos
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_PAGOS" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_PAGOS" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_PAGOS" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJE_PAGOS" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJE_PAGOS" runat="server" ForeColor="Red" />
                                            <asp:CheckBox ID="CheckBox_Exell" runat="server" Visible="False" />
                                        </div>
                                    </asp:Panel>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_PAGOS" runat="server" Width="867px" AutoGenerateColumns="False"
                                                DataKeyNames="CODIGO" PageSize="6" OnSelectedIndexChanged="GridView_PAGOS_SelectedIndexChanged"
                                                AllowPaging="True" OnPageIndexChanging="GridView_PAGOS_PageIndexChanging" OnRowCommand="GridView_PAGOS_RowCommand">
                                                <Columns>
                                                    <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                                        SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:CommandField>
                                                    <asp:ButtonField ImageUrl="../imagenes/plantilla/icono_pdf.png" CommandName="Desprendible"
                                                        HeaderText="" ButtonType="Image">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="CODIGO" HeaderText="Código">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="INICIA" DataFormatString="{0:dd/M/yyyy}" HeaderText="INICIA" />
                                                    <asp:BoundField DataField="TERMINA" DataFormatString="{0:dd/M/yyyy}" HeaderText="TERMINA" />
                                                    <asp:BoundField DataField="PER_CONT" HeaderText="PERIODO CONTABLE" />
                                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />
                                                    <asp:BoundField DataField="SUELDO" HeaderText="SUELDO" />
                                                    <asp:BoundField DataField="DEVENGADO" HeaderText="TOTAL DEVENGADO" />
                                                    <asp:BoundField DataField="DEDUCCIONES" HeaderText="TOTAL DEDUCCIONES" />
                                                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL GENERAL" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                            <asp:Panel ID="PNL_DETALLE_PAGO" runat="server" Visible="False">
                                                <div class="div_espaciador">
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server" CssClass="div_cabeza_groupbox_gris">
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                        <tr>
                                                            <td class="style1">
                                                                PAGO DETALLADO
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="image_cerrar_detalle" runat="server" ImageUrl="~/imagenes/plantilla/collapse.jpg"
                                                                    OnClick="image_cerrar_detalle_Click1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <div>
                                                    <asp:GridView ID="GridView_DETALLE_PAGOS" runat="server" Width="867px" DataKeyNames="CODIGO"
                                                        PageSize="30">
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtenderPagos" runat="Server"
                                    TargetControlID="Panel_CONTENIDO_PAGOS" ExpandControlID="Panel_CABEZA_PAGOS"
                                    CollapseControlID="Panel_CABEZA_PAGOS" Collapsed="True" TextLabelID="Label_PAGOS"
                                    ImageControlID="Image_PAGOS" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFORMACION_FAMILIAR" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_INFORMACION_FAMILIAR" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Información Familiar
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_INFORMACION_FAMILIAR" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_INFORMACION_FAMILIAR" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_INFORMACION_FAMILIAR" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJE_INFORMACION_FAMILIAR" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJE_INFORMACION_FAMILIAR" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <div class="div_contenedor_grilla_resultados">
                                        <asp:UpdatePanel ID="UpdatePanel_ComposicionFamiliar" runat="server">
                                            <ContentTemplate>
                                                <div class="tab_radicacion">
                                                    <table cellspacing="5" class="table_form_radicacion">
                                                        <tr>
                                                            <td class="td_2">
                                                                Estado Civil<br />
                                                                <asp:DropDownList ID="DropDownList_ESTADO_CIVIL" runat="server" ValidationGroup="GUARDAR"
                                                                    CssClass="textbox_ajustado" Enabled="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="td_2">
                                                                <table cellspacing="5" class="table_ajustada">
                                                                    <tr>
                                                                        <td>
                                                                            Número de Hijos<br />
                                                                            <asp:TextBox Enabled="false" ID="TextBox_NUM_HIJOS" runat="server" ValidationGroup="GUARDAR"
                                                                                CssClass="textbox_ajustado"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUM_HIJOS"
                                                                                runat="server" TargetControlID="TextBox_NUM_HIJOS" FilterType="Numbers">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            Cabeza de Familia<br />
                                                                            <asp:DropDownList ID="DropDownList_CabezaFamilia" runat="server" CssClass="textbox_ajustado"
                                                                                ValidationGroup="GUARDAR" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!--TextBox_NUM_HIJOS-->
                                                    <asp:RequiredFieldValidator ID="RFV_TextBox_NUM_HIJOS" runat="server" ControlToValidate="TextBox_NUM_HIJOS"
                                                        Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE HIJOS es requerido."
                                                        ValidationGroup="GUARDAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_NUM_HIJOS"
                                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_TextBox_NUM_HIJOS" />
                                                    <!--DropDownList_ESTADO_CIVIL-->
                                                    <asp:RequiredFieldValidator ID="RFV_DropDownList_ESTADO_CIVIL" runat="server" ControlToValidate="DropDownList_ESTADO_CIVIL"
                                                        Display="None" ErrorMessage="Campo Requerido faltante</br>LA TALLA DE ZAPATOS es requerido."
                                                        ValidationGroup="GUARDAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ESTADO_CIVIL"
                                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_ESTADO_CIVIL" />
                                                    <!--DropDownList_CabezaFamilia-->
                                                    <asp:RequiredFieldValidator ID="RFV_DropDownList_CabezaFamilia" runat="server" ControlToValidate="DropDownList_ESTADO_CIVIL"
                                                        Display="None" ErrorMessage="Campo Requerido faltante</br>LA TALLA DE ZAPATOS es requerido."
                                                        ValidationGroup="GUARDAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CabezaFamilia"
                                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_CabezaFamilia" />
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <asp:HiddenField ID="HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_ID_COMPOSICION" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_ID_TIPO_FAMILIAR" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_NOMBRES" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_APELLIDOS" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_FECHA_NACIMIENTO" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_PROFESION" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_ID_CIUDAD" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_VIVE_CON_EL" runat="server" />
                                                    <asp:HiddenField ID="HiddenField_ACTIVO" runat="server" />
                                                    <div class="div_contenedor_grilla_resultados">
                                                        <div class="grid_seleccionar_registros">
                                                            <asp:GridView ID="GridView_ComposicionFamiliar" runat="server" Width="865px" AutoGenerateColumns="False"
                                                                DataKeyNames="ID_COMPOSICION" Enabled="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Tipo Familiar">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList_TipoFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                                CssClass="TextBox_con_letra_pequena" Width="80px">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoFamiliar"
                                                                                ControlToValidate="DropDownList_TipoFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE FAMILIAR es requerido."
                                                                                ValidationGroup="COMPOSICIONFAMILIAR" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                    ID="ValidatorCalloutExtender_DropDownList_TipoFamiliar" TargetControlID="RequiredFieldValidator_DropDownList_TipoFamiliar"
                                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nombres y Apellidos">
                                                                        <ItemTemplate>
                                                                            Nombres:<br />
                                                                            <asp:TextBox ID="TextBox_NombresFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                                Width="160px" CssClass="TextBox_con_letra_pequena"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    runat="server" ID="RequiredFieldValidator_TextBox_NombresFamiliar" ControlToValidate="TextBox_NombresFamiliar"
                                                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                                                                    ValidationGroup="COMPOSICIONFAMILIAR" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                        ID="ValidatorCalloutExtender_TextBox_NombresFamiliar" TargetControlID="RequiredFieldValidator_TextBox_NombresFamiliar"
                                                                                        HighlightCssClass="validatorCalloutHighlight" />
                                                                            <br />
                                                                            Apellidos:<br />
                                                                            <asp:TextBox ID="TextBox_ApellidosFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                                Width="160px" CssClass="TextBox_con_letra_pequena"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    runat="server" ID="RequiredFieldValidator_TextBox_ApellidosFamiliar" ControlToValidate="TextBox_ApellidosFamiliar"
                                                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Los APELLIDOS son requeridos."
                                                                                    ValidationGroup="COMPOSICIONFAMILIAR" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                        ID="ValidatorCalloutExtender_TextBox_ApellidosFamiliar" TargetControlID="RequiredFieldValidator_TextBox_ApellidosFamiliar"
                                                                                        HighlightCssClass="validatorCalloutHighlight" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="F. Nacimiento">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox_FechaNacimientoFamiliar" runat="server" Width="80px" CssClass="TextBox_con_letra_pequena"></asp:TextBox><br />
                                                                            <asp:Label ID="Label_Edad" runat="server" Text="Edad: 0" CssClass="TextBox_con_letra_pequena"></asp:Label><ajaxToolkit:CalendarExtender
                                                                                ID="CalendarExtender_TextBox_FechaNacimientoFamiliar" runat="server" TargetControlID="TextBox_FechaNacimientoFamiliar"
                                                                                Format="dd/MM/yyyy" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" Width="85px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="¿A qué se dedica?">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TextBox_ProfesionFamiliar" runat="server" Width="185px" CssClass="TextBox_con_letra_pequena"
                                                                                TextMode="MultiLine" Height="60px" ValidationGroup="COMPOSICIONFAMILIAR"></asp:TextBox><asp:RequiredFieldValidator
                                                                                    runat="server" ID="RequiredFieldValidator_TextBox_ProfesionFamiliar" ControlToValidate="TextBox_ProfesionFamiliar"
                                                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La PROFESIÓN es requerida."
                                                                                    ValidationGroup="COMPOSICIONFAMILIAR" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                        ID="ValidatorCalloutExtender_TextBox_ProfesionFamiliar" TargetControlID="RequiredFieldValidator_TextBox_ProfesionFamiliar"
                                                                                        HighlightCssClass="validatorCalloutHighlight" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" Width="190px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Vive en">
                                                                        <ItemTemplate>
                                                                            <div style="margin: 2px auto; background-color: #eeeeee;">
                                                                                <asp:CheckBox ID="CheckBox_Extranjero" runat="server" Text="En el extranjero" /></div>
                                                                            <asp:Panel ID="Panel_ViveEn" runat="server">
                                                                                <hr />
                                                                                Departamento:<br />
                                                                                <asp:DropDownList ID="DropDownList_DepartamentoFamiliar" runat="server" Width="135px"
                                                                                    CssClass="TextBox_con_letra_pequena">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                Ciudad:<br />
                                                                                <asp:DropDownList ID="DropDownList_CiudadFamiliar" runat="server" Width="135px" CssClass="TextBox_con_letra_pequena"
                                                                                    ValidationGroup="COMPOSICIONFAMILIAR">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CiudadFamiliar"
                                                                                    ControlToValidate="DropDownList_CiudadFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD es requerida."
                                                                                    ValidationGroup="COMPOSICIONFAMILIAR" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                        ID="ValidatorCalloutExtender_DropDownList_CiudadFamiliar" TargetControlID="RequiredFieldValidator_DropDownList_CiudadFamiliar"
                                                                                        HighlightCssClass="validatorCalloutHighlight" />
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" Width="145px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Vive con el Candidato?">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox_ViveConElFamiliar" runat="server" /></ItemTemplate>
                                                                        <ItemStyle CssClass="columna_grid_centrada" Width="50px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <headerStyle BackColor="#DDDDDD" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_INFORMACION_FAMILIAR"
                                    runat="Server" TargetControlID="Panel_CONTENIDO_INFORMACION_FAMILIAR" ExpandControlID="Panel_CABEZA_INFORMACION_FAMILIAR"
                                    CollapseControlID="Panel_CABEZA_INFORMACION_FAMILIAR" Collapsed="True" TextLabelID="Label_INFORMACION_FAMILIAR"
                                    ImageControlID="Image_INFORMACION_FAMILIAR" ExpandedText="(Ocultar detalles...)"
                                    CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                                    CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_CONCEPTOS_FIJOS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_CONCEPTOS_FIJOS" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Conceptos fijos
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_CONCEPTOS_FIJOS" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_CONCEPTOS_FIJOS" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_CONCEPTOS_FIJOS" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJES_CONCEPTOS_FIJOS" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJES_CONCEPTOS_FIJOS" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_CONCEPTOS_FIJOS" runat="server" Width="867px" AutoGenerateColumns="False"
                                                DataKeyNames="REGISTRO,ID_EMPLEADO,ID_CONCEPTO,ID_CLAUSULA,LIQ_Q_1,LIQ_Q_2,LIQ_Q_3,LIQ_Q_4"
                                                OnRowCommand="GridView_CONCEPTOS_FIJOS_RowCommand">
                                                <headerStyle BackColor="#DDDDDD" />
                                                <Columns>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HtmlEncode="False" HtmlEncodeFormatString="False"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                                    <asp:BoundField DataField="ID_CONCEPTO" HeaderText="ID_CONCEPTO" Visible="False" />
                                                    <asp:BoundField DataField="DESC_CONCEPTO" HeaderText="Concepto fijo">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CAN_PRE" HeaderText="Cantidad cuotas">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VAL_PRE" HeaderText="Valor cuota" DataFormatString="{0:N0}">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODOS_LIQUIDACION" HeaderText="Périodos para liquidar">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ESTADO_CONCEPTO_FIJO" HeaderText="Estado concepto" />
                                                    <asp:BoundField DataField="ID_CLAUSULA" HeaderText="Núm clausula">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField CommandName="ver" Text="Ver clausula" HeaderText="Ver clausula">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="LIQ_Q_1" HeaderText="LIQ_Q_1" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_2" HeaderText="LIQ_Q_2" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_3" HeaderText="LIQ_Q_3" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_4" HeaderText="LIQ_Q_4" Visible="False" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_CONCEPTOS_FIJOS"
                                    runat="Server" TargetControlID="Panel_CONTENIDO_CONCEPTOS_FIJOS" ExpandControlID="Panel_CABEZA_CONCEPTOS_FIJOS"
                                    CollapseControlID="Panel_CABEZA_CONCEPTOS_FIJOS" Collapsed="True" TextLabelID="Label_CONCEPTOS_FIJOS"
                                    ImageControlID="Image_CONCEPTOS_FIJOS" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                            <asp:Panel ID="Panel_INFO_ACTIVIDADES_PROGRAMAS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <asp:Panel ID="Panel_CABEZA_ACTIVIDADES" runat="server" CssClass="div_cabeza_groupbox_gris">
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="width: 87%;">
                                                Participación En Actividades
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="font-size: 80%">
                                                            <asp:Label ID="Label_ACTIVIDADES" runat="server">(Mostrar detalles...)</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image_ACTIVIDADES" runat="server" CssClass="img_cabecera_hoja" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_CONTENIDO_ACTIVIDADES" runat="server" CssClass="div_contenido_groupbox_gris">
                                    <asp:Panel ID="Panel_MENSAJES_ACTIVIDADES" runat="server">
                                        <div class="div_espacio_validacion_campos">
                                            <asp:Label ID="Label_MENSAJES_ACTIVIDADES" runat="server" ForeColor="Red" />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_ActividadesBinestar" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Actividades Bienestar Social (Resplandor)
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ActividadesBienestar" runat="server" Width="850px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_ASISTENCIA,ID_DETALLE,ID_EMPLEADO,ID_SOLICITUD">
                                                        <headerStyle BackColor="#DDDDDD" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enlace">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink_VisorActividad" runat="server">Ver Actividad</asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_ACTIVIDAD" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Actividad">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION_ACTIVIDAD" HeaderText="Descripción Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_ActividadesRse" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Actividades RSE (Responsabilidad Social Empresarial)
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ActividadesRse" runat="server" Width="850px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_ASISTENCIA,ID_DETALLE,ID_EMPLEADO,ID_SOLICITUD">
                                                        <headerStyle BackColor="#DDDDDD" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enlace">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink_VisorActividad" runat="server">Ver Actividad</asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_ACTIVIDAD" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Actividad">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION_ACTIVIDAD" HeaderText="Descripción Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_ActividadesGlobalSalud" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Actividades Salud Ocupacional
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ActividadesSaludOcupacional" runat="server" Width="850px"
                                                        AutoGenerateColumns="False" DataKeyNames="ID_ASISTENCIA,ID_DETALLE,ID_EMPLEADO,ID_SOLICITUD">
                                                        <headerStyle BackColor="#DDDDDD" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enlace">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink_VisorActividad" runat="server">Ver Actividad</asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_ACTIVIDAD" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Actividad">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION_ACTIVIDAD" HeaderText="Descripción Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_ActividadOperaciones" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Actividades Operaciones
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ActividadesOperaciones" runat="server" Width="850px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_ASISTENCIA,ID_DETALLE,ID_EMPLEADO,ID_SOLICITUD">
                                                        <headerStyle BackColor="#DDDDDD" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enlace">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink_VisorActividad" runat="server">Ver Actividad</asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_ACTIVIDAD" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Actividad">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION_ACTIVIDAD" HeaderText="Descripción Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelActividadesGestionHumana" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            Actividades Gestión Humana -Capacitación y Entrenamiento-
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ActividadesGestionHumana" runat="server" Width="850px"
                                                        AutoGenerateColumns="False" DataKeyNames="ID_ASISTENCIA,ID_DETALLE,ID_EMPLEADO,ID_SOLICITUD">
                                                        <headerStyle BackColor="#DDDDDD" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Enlace">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink_VisorActividad" runat="server">Ver Actividad</asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FECHA_ACTIVIDAD" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Actividad">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION_ACTIVIDAD" HeaderText="Descripción Actividad">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ACTIVIDADES" runat="Server"
                                    TargetControlID="Panel_CONTENIDO_ACTIVIDADES" ExpandControlID="Panel_CABEZA_ACTIVIDADES"
                                    CollapseControlID="Panel_CABEZA_ACTIVIDADES" Collapsed="True" TextLabelID="Label_ACTIVIDADES"
                                    ImageControlID="Image_ACTIVIDADES" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                    ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                    SuppressPostBack="true">
                                </ajaxToolkit:CollapsiblePanelExtender>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button_ImprimrContrato1" />
                        <asp:PostBackTrigger ControlID="Button_ENTREGAS" />
                        <asp:PostBackTrigger ControlID="Button_AUTOS" />
                        <asp:PostBackTrigger ControlID="Button_APERTURA_CUENTA" />
                        <asp:PostBackTrigger ControlID="Button_solicitud_examenes" />
                        <asp:PostBackTrigger ControlID="GridView_CONCEPTOS_FIJOS" />
                        <asp:PostBackTrigger ControlID="GridView_PAGOS" />
                        <asp:PostBackTrigger ControlID="Cert_Laboral" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
    </asp:Panel>
</asp:Content>
