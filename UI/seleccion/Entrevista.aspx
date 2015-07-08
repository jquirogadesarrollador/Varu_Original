<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="Entrevista.aspx.cs" Inherits="_Entrevista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>

    <script language="javascript" type="text/javascript">
        setInterval('MantenSesion()', <%= (int) (0.9 * (Session.Timeout * 60000)) %>);
    </script>

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

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_ENTREVISTA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_REQUISICION" runat="server" />
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

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
                                        <td colspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_IMPRIMIR" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" type="button"
                                                cssclass="margin_botones" value="Salir" />
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
                            </div>
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
                                    DataKeyNames="ID_SOLICITUD" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo documento">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Num. doc identidad">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEXO" HeaderText="Sexo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FCH_NACIMIENTO" HeaderText="Nacimiento" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ARCHIVO" HeaderText="Estado">
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

            <asp:Panel ID="Panel_DATOS_CANDIDATO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información del cargo
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Ocupación
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_OCUPACION_ASPIRANTE" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Cargo al que aspira
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ID_OCUPACION" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Estado Candidato
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ARCHIVO_SOLICITUD" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox">
                        Datos personales
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Solicitud
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ID_SOLICITUD" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Apellidos
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_APPELIDOS" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Nombres
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NOMBRES" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Doc. de identidad
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_DOC_IDENTIDAD" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Ciudad
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CIUDAD" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Dirección
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_DIRECCION" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Sector
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_SECTOR" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Telefono
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_TELEFONO" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Aspiración salarial
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_ASPIRACION_SALARIAL" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_TIPO_ENTREVISTA" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Tipo de entrevista
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_FONDO_MENSAJE_TIPO_ENTREVISTA" runat="server" Visible="false"
                            Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_TIPO_ENTREVISTA" runat="server">
                            <asp:Image ID="Image_MENSAJE_TIPO_ENTREVISTA_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_TIPO_ENTREVISTA_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_TIPO_ENTREVISTA" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_TIPO_ENTREVISTA" runat="server" Text="Cerrar"
                                    CssClass="margin_botones" OnClick="Button_CERRAR_MENSAJE_TIPO_ENTREVISTA_Click" />
                            </div>
                        </asp:Panel>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList_TIPO_ENTREVISTA" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="RadioButtonList_TIPO_ENTREVISTA_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="SIN">Entrevista (Productividad)</asp:ListItem>
                                        <asp:ListItem Value="CON">Entrevista (Asociada a Requisición)</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel_TIPO_SIN_REQUISICION" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Sección para entrevista SIN requisición
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_avisos_importantes">
                                    Al seleccionar este tipo de entrevista, se cargarán los datos básicos y se deberá
                                    seleccionar manualmente las pruebas que se evaluarán.
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <asp:Button ID="Button_SELECCIONAR_SIN_REQUISISCION" runat="server" Text="Seleccionar"
                                    CssClass="margin_botones" OnClick="Button_SELECCIONAR_SIN_REQUISISCION_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_TIPO_CON_REQUISICION" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Sección para entrevista CON requisición
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_avisos_importantes">
                                    Al seleccionar este tipo de entrevista, se cargarán los datos básicos , además las
                                    pruebas se cargan según un perfil asociado a una reuisición seleccionada.
                                </div>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Empresa donde va a laborar
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_EMPRESA_ASPIRANTE" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownList_EMPRESA_ASPIRANTE_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_EMPRESA_ASPIRANTE -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EMPRESA_ASPIRANTE"
                                    ControlToValidate="DropDownList_EMPRESA_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar LA EMPRESA."
                                    ValidationGroup="ENTREVISTA" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EMPRESA_ASPIRANTE"
                                    TargetControlID="RequiredFieldValidator_DropDownList_EMPRESA_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Lista de reguicisiones abiertas
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_REQ" runat="server" Width="850px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_REQUERIMIENTO,ID_EMPRESA,REGISTRO_PERFIL" OnRowCommand="GridView_REQ_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                                        Text="Seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Num. requisición">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                                    <asp:BoundField DataField="TIPO_REQ" HeaderText="Req. tipo" />
                                                    <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                                    <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Apertura" DataFormatString="{0:dd/M/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/M/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                                    <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo Requerido" />
                                                    <asp:BoundField DataField="CONT_NOM" HeaderText="Contácto" />
                                                    <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                                    <asp:BoundField DataField="REGISTRO_PERFIL" HeaderText="REGISTRO_PERFIL" Visible="False" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_ENTREVISTA_BASICA" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos Básicos de Entrevista
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
                                                        <asp:Label ID="Label1" runat="server">(Mostrar detalles...)</asp:Label>
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
                        <asp:Panel ID="Panel_VER_ARCHIVO_DE_WORD" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Archivo de microsoft Word (Entrevista Archivada)
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Archivo de Microsoft Word (Entrevista archivada)
                                        </td>
                                        <td class="td_der">
                                            <asp:HyperLink ID="HyperLink_ARCHIVO_WORD_ENTREVISTA" runat="server">Ver archivo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div class="div_espaciador">
                        </div>
                        <div class="div_cabeza_groupbox_gris">
                            Cuestionario Básico
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        Fecha de la entrevista
                                        <asp:TextBox ID="TextBox_FCH_ENTREVISTA" runat="server" MaxLength="10" ValidationGroup="GUARDAR"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FCH_ENTREVISTA" runat="server"
                                            TargetControlID="TextBox_FCH_ENTREVISTA" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="div_titulos_entrevista">
                                            COMPOSICIÓN FAMILIAR
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel_ComposicionFamiliar" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
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
                                                        <asp:GridView ID="GridView_ComposicionFamiliar" runat="server" Width="845px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_COMPOSICION" OnRowCommand="GridView_ComposicionFamiliar_RowCommand">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                    Text="Eliminar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                                                    Text="Actualizar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:TemplateField HeaderText="Tipo Familiar">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList_TipoFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                            CssClass="TextBox_con_letra_pequena" Width="80px">
                                                                        </asp:DropDownList>
                                                                        <!-- DropDownList_TipoFamiliar -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoFamiliar"
                                                                            ControlToValidate="DropDownList_TipoFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE FAMILIAR es requerido."
                                                                            ValidationGroup="COMPOSICIONFAMILIAR" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TipoFamiliar"
                                                                            TargetControlID="RequiredFieldValidator_DropDownList_TipoFamiliar" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombres y Apellidos">
                                                                    <ItemTemplate>
                                                                        Nombres:<br />
                                                                        <asp:TextBox ID="TextBox_NombresFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                            Width="160px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        <!-- TextBox_NombresFamiliar -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NombresFamiliar"
                                                                            ControlToValidate="TextBox_NombresFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                                                            ValidationGroup="COMPOSICIONFAMILIAR" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NombresFamiliar"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_NombresFamiliar" HighlightCssClass="validatorCalloutHighlight" />
                                                                        <br />
                                                                        Apellidos:<br />
                                                                        <asp:TextBox ID="TextBox_ApellidosFamiliar" runat="server" ValidationGroup="COMPOSICIONFAMILIAR"
                                                                            Width="160px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        <!-- TextBox_ApellidosFamiliar -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ApellidosFamiliar"
                                                                            ControlToValidate="TextBox_ApellidosFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Los APELLIDOS son requeridos."
                                                                            ValidationGroup="COMPOSICIONFAMILIAR" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ApellidosFamiliar"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_ApellidosFamiliar" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="F. Nacimiento">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_FechaNacimientoFamiliar" runat="server" Width="80px" CssClass="TextBox_con_letra_pequena"
                                                                            AutoPostBack="True" OnTextChanged="TextBox_FechaNacimientoFamiliar_TextChanged"></asp:TextBox><br />
                                                                        <asp:Label ID="Label_Edad" runat="server" Text="Edad: 0" CssClass="TextBox_con_letra_pequena"></asp:Label>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaNacimientoFamiliar"
                                                                            runat="server" TargetControlID="TextBox_FechaNacimientoFamiliar" Format="dd/MM/yyyy" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="85px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="¿A qué se dedica?">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_ProfesionFamiliar" runat="server" Width="185px" CssClass="TextBox_con_letra_pequena"
                                                                            TextMode="MultiLine" Height="60px" ValidationGroup="COMPOSICIONFAMILIAR"></asp:TextBox>
                                                                        <!-- TextBox_ProfesionFamiliar -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ProfesionFamiliar"
                                                                            ControlToValidate="TextBox_ProfesionFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La PROFESIÓN es requerida."
                                                                            ValidationGroup="COMPOSICIONFAMILIAR" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ProfesionFamiliar"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_ProfesionFamiliar" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="190px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vive en">
                                                                    <ItemTemplate>
                                                                        <div style="margin: 2px auto; background-color: #eeeeee;">
                                                                            <asp:CheckBox ID="CheckBox_Extranjero" runat="server" Text="En el extranjero" AutoPostBack="True"
                                                                                OnCheckedChanged="CheckBox_Extranjero_CheckedChanged" />
                                                                        </div>
                                                                        <asp:Panel ID="Panel_ViveEn" runat="server">
                                                                            <hr />
                                                                            Departamento:<br />
                                                                            <asp:DropDownList ID="DropDownList_DepartamentoFamiliar" runat="server" Width="135px"
                                                                                CssClass="TextBox_con_letra_pequena" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_DepartamentoFamiliar_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            Ciudad:<br />
                                                                            <asp:DropDownList ID="DropDownList_CiudadFamiliar" runat="server" Width="135px" CssClass="TextBox_con_letra_pequena"
                                                                                ValidationGroup="COMPOSICIONFAMILIAR">
                                                                            </asp:DropDownList>
                                                                            <!-- DropDownList_CiudadFamiliar -->
                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CiudadFamiliar"
                                                                                ControlToValidate="DropDownList_CiudadFamiliar" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD es requerida."
                                                                                ValidationGroup="COMPOSICIONFAMILIAR" />
                                                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CiudadFamiliar"
                                                                                TargetControlID="RequiredFieldValidator_DropDownList_CiudadFamiliar" HighlightCssClass="validatorCalloutHighlight" />
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="145px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vive con el Candidato?">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox_ViveConElFamiliar" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="50px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel_Botones_ComposicionFamiliar" runat="server">
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <div style="text-align: left; margin-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="Button_NUEVA_COMPOSICIONFAMILIAR" runat="server" Text="Nuevo Familiar"
                                                                        CssClass="margin_botones" ValidationGroup="NUEVACOMPOSICIONFAMILIAR" OnClick="Button_NUEVA_COMPOSICIONFAMILIAR_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_GUARDAR_COMPOSICIONFAMILIAR" runat="server" Text="Guardar Familiar"
                                                                        CssClass="margin_botones" ValidationGroup="COMPOSICIONFAMILIAR" OnClick="Button_GUARDAR_COMPOSICIONFAMILIAR_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_CANCELAR_COMPOSICIONFAMILIAR" runat="server" Text="Cancelar"
                                                                        CssClass="margin_botones" ValidationGroup="CANCELARCOMPOSICIONFAMILIAR" OnClick="Button_CANCELAR_COMPOSICIONFAMILIAR_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="div_espaciador">
                                        </div>

                                        <div class="div_tag_entrevista">
                                            <ul>
                                                <li>Observaciones y datos adcionales para <b>COMPOSICIÓN FAMILIAR:</b></li>
                                            </ul>
                                        </div>
                                        <asp:TextBox ID="TextBox_COM_C_FAM" runat="server" Height="100px" TextMode="MultiLine"
                                            Width="850px" ValidationGroup="GUARDAR" onPaste="return false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="div_titulos_entrevista">
                                            INFORMACIÓN ACADÉMICA
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ID_INFO_ACADEMICA_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_NIV_ACADEMICO_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_INSTITUCION_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ANNO_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_OBSERVACIONES_EF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ACTIVO_EF" runat="server" />
                                                <div class="div_tag_entrevista">
                                                    <ul>
                                                        <li><b>EDUCACIÓN FORMAL</b></li>
                                                    </ul>
                                                </div>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_EducacionFormal" runat="server" Width="845px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_INFO_ACADEMICA" OnRowCommand="GridView_EducacionFormal_RowCommand">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                    Text="Eliminar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                                                    Text="Actualizar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:TemplateField HeaderText="Grado de Instrucción Alcanzado">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList_NivAcademico" runat="server" ValidationGroup="EDUFORMAL"
                                                                            Width="180px" CssClass="TextBox_con_letra_pequena">
                                                                        </asp:DropDownList>
                                                                        <!-- DropDownList_NivAcademico -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_NivAcademico"
                                                                            ControlToValidate="DropDownList_NivAcademico" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El GRADO DE INSTRUCCIÓN es requerido."
                                                                            ValidationGroup="EDUFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_NivAcademico"
                                                                            TargetControlID="RequiredFieldValidator_DropDownList_NivAcademico" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Institución">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Institucion" runat="server" ValidationGroup="EDUFORMAL"
                                                                            Width="240px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        <!-- TextBox_Institucion -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Institucion"
                                                                            ControlToValidate="TextBox_Institucion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Institución es requerida."
                                                                            ValidationGroup="EDUFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TTextBox_Institucion"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_Institucion" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Año">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Anno" runat="server" Width="70px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Anno" runat="server"
                                                                            TargetControlID="TextBox_Anno" FilterType="Numbers" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="270px" TextMode="MultiLine"
                                                                            Height="45px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel_EducacionFormal" runat="server">
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <div style="text-align: left; margin-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="Button_NuevoEF" runat="server" Text="Nuevo Registro" CssClass="margin_botones"
                                                                        ValidationGroup="NUEVOEF" OnClick="Button_NuevoEF_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_GuardarEF" runat="server" Text="Guardar Registro" CssClass="margin_botones"
                                                                        ValidationGroup="EDUFORMAL" OnClick="Button_GuardarEF_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_CancelarEF" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                        ValidationGroup="CANCELAREF" OnClick="Button_CancelarEF_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="div_espaciador">
                                        </div>

                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ID_INFO_ACADEMICA_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_CURSO_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_INSTITUCION_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_DURACION_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_UNIDAD_DURACION_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_OBSERVACIONES_ENF" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ACTIVO_ENF" runat="server" />
                                                <div class="div_tag_entrevista">
                                                    <ul>
                                                        <li><b>EDUCACIÓN NO FORMAL</b></li>
                                                    </ul>
                                                </div>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_EducacionNoFormal" runat="server" Width="845px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_INFO_ACADEMICA" OnRowCommand="GridView_EducacionNoFormal_RowCommand">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                                    Text="Eliminar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                                                    Text="Actualizar">
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                                </asp:ButtonField>
                                                                <asp:TemplateField HeaderText="Cursos Libres - Diplomados">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Curso" runat="server" ValidationGroup="EDUNOFORMAL" Width="200px"
                                                                            CssClass="TextBox_con_letra_pequena" TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                                        <!-- TextBox_Curso -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Curso"
                                                                            ControlToValidate="TextBox_Curso" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CURSO es requerido."
                                                                            ValidationGroup="EDUNOFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Curso"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_Curso" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Institución">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Institucion" runat="server" ValidationGroup="EDUNOFORMAL"
                                                                            CssClass="TextBox_con_letra_pequena" Width="220px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                                                                        <!-- TextBox_Institucion -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Institucion"
                                                                            ControlToValidate="TextBox_Institucion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Institución es requerida."
                                                                            ValidationGroup="EDUNOFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Institucion_1"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_Institucion" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Duración">
                                                                    <ItemTemplate>
                                                                        Tiempo:
                                                                        <asp:TextBox ID="TextBox_Duracion" runat="server" Width="70px" ValidationGroup="EDUNOFORMAL"
                                                                            CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Duracion"
                                                                            runat="server" TargetControlID="TextBox_Duracion" FilterType="Numbers,Custom"
                                                                            ValidChars="," />
                                                                        <!-- TextBox_Duracion -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Duracion"
                                                                            ControlToValidate="TextBox_Duracion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DURción es requerida."
                                                                            ValidationGroup="EDUNOFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Duracion"
                                                                            TargetControlID="RequiredFieldValidator_TextBox_Duracion" HighlightCssClass="validatorCalloutHighlight" />
                                                                        Unidad:
                                                                        <asp:DropDownList ID="DropDownList_UnidadDuracion" runat="server" ValidationGroup="EDUNOFORMAL"
                                                                            Width="70px" CssClass="TextBox_con_letra_pequena">
                                                                        </asp:DropDownList>
                                                                        <!-- DropDownList_UnidadDuracion -->
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_UnidadDuracion"
                                                                            ControlToValidate="DropDownList_UnidadDuracion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La UNIDAD es requerida."
                                                                            ValidationGroup="EDUNOFORMAL" />
                                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_UnidadDuracion"
                                                                            TargetControlID="RequiredFieldValidator_DropDownList_UnidadDuracion" HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="250px" TextMode="MultiLine"
                                                                            Height="45px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel_EducacionNoFormal" runat="server">
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <div style="text-align: left; margin-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="Button_NuevoENF" runat="server" Text="Nuevo Registro" CssClass="margin_botones"
                                                                        ValidationGroup="NUEVOENF" OnClick="Button_NuevoENF_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_GuardarENF" runat="server" Text="Guardar Registro" CssClass="margin_botones"
                                                                        ValidationGroup="EDUNOFORMAL" OnClick="Button_GuardarENF_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_CancelarENF" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                        ValidationGroup="CANCELARENF" OnClick="Button_CancelarENF_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="div_espaciador">
                                        </div>
                                        <div class="div_tag_entrevista">
                                            <ul>
                                                <li>Observaciones y datos adcionales para <b>INFORMACIÓN ACADÉMICA:</b></li>
                                            </ul>
                                        </div>
                                        <asp:TextBox ID="TextBox_COM_C_ACA" runat="server" Height="100px" TextMode="MultiLine"
                                            Width="850px" ValidationGroup="GUARDAR" onPaste="return false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="div_titulos_entrevista">
                                            EXPERIENCIA LABORAL
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA_EXPLABORAL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ID_EXPERIENCIA" runat="server" />
                                                <asp:HiddenField ID="HiddenField_EMPRESA_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_CARGO_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FUNCIONES_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FECHA_INGRESO_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_FECHA_RETIRO_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_MOTIVO_RETIRO_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ULTIMO_SALARIO_EL" runat="server" />
                                                <asp:HiddenField ID="HiddenField_ACTIVO_EL" runat="server" />
                                                <asp:GridView ID="GridView_ExperienciaLaboral" runat="server" Width="845px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_EXPERIENCIA" OnRowCommand="GridView_ExperienciaLaboral_RowCommand">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                            Text="Eliminar">
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                                            Text="Actualizar">
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="23px" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Información Laboral">
                                                            <ItemTemplate>
                                                                <table class="table_control_registros" width="95%">
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            EMPRESA:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Empresa" runat="server" ValidationGroup="EXPLAB" Width="600px"
                                                                                CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            CARGO DESEMPEÑADO:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Cargo" runat="server" ValidationGroup="EXPLAB" Width="600px"
                                                                                CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            FUNCIONES REALIZADAS:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FuncionesRealizadas" runat="server" ValidationGroup="EXPLAB"
                                                                                Width="600px" TextMode="MultiLine" Height="70px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            FECHA INGRESO:
                                                                        </td>
                                                                        <td style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FechaIngreso" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="TextBox_con_letra_pequena" AutoPostBack="True" OnTextChanged="TextBox_FechaIngreso_TextChanged"></asp:TextBox>
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaIngreso" runat="server"
                                                                                TargetControlID="TextBox_FechaIngreso" Format="dd/MM/yyyy" />
                                                                        </td>
                                                                        <td rowspan="2" style="width: 30%; text-align: left;">
                                                                            <asp:Label ID="Label_TiempoTrabajado" runat="server" Text="Tiempo Desconocido"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            FECHA RETIRO:
                                                                        </td>
                                                                        <td style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FechaRetiro" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="TextBox_con_letra_pequena" AutoPostBack="True" OnTextChanged="TextBox_FechaIngreso_TextChanged"></asp:TextBox>
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaRetiro" runat="server"
                                                                                TargetControlID="TextBox_FechaRetiro" Format="dd/MM/yyyy" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            MOTIVO RETIRO:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:DropDownList ID="DropDownList_MotivoRetiro" runat="server" ValidationGroup="EXPLAB"
                                                                                CssClass="TextBox_con_letra_pequena" Width="600px">
                                                                                <asp:ListItem Value="">NINGUNO</asp:ListItem>
                                                                                <asp:ListItem Value="VOLUNTARIO">VOLUNTARIO</asp:ListItem>
                                                                                <asp:ListItem Value="TERMINACIÓN CONTRATO">TERMINACIÓN CONTRATO</asp:ListItem>
                                                                                <asp:ListItem Value="JUSTA CAUSA">JUSTA CAUSA</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 300px;">
                                                                            ÚLTIMO SALARIO:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Ultimosalario" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="money"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Ultimosalario"
                                                                                runat="server" TargetControlID="TextBox_Ultimosalario" FilterType="Numbers,Custom"
                                                                                ValidChars=",." />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <!-- TextBox_Empresa -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Empresa"
                                                                    ControlToValidate="TextBox_Empresa" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA es requerida."
                                                                    ValidationGroup="EXPLAB" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Empresa"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_Empresa" HighlightCssClass="validatorCalloutHighlight" />
                                                                <!-- TextBox_Cargo -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Cargo"
                                                                    ControlToValidate="TextBox_Cargo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO DESEMPEÑADO es requerido."
                                                                    ValidationGroup="EXPLAB" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Cargo"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_Cargo" HighlightCssClass="validatorCalloutHighlight" />
                                                                <!-- TextBox_FuncionesRealizadas -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FuncionesRealizadas"
                                                                    ControlToValidate="TextBox_FuncionesRealizadas" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las FUNCIONES REALIZADAS son requeridas."
                                                                    ValidationGroup="EXPLAB" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FuncionesRealizadas"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_FuncionesRealizadas" HighlightCssClass="validatorCalloutHighlight" />
                                                                <!-- TextBox_FechaIngreso -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FechaIngreso"
                                                                    ControlToValidate="TextBox_FechaIngreso" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE INGRESO es requerida."
                                                                    ValidationGroup="EXPLAB" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaIngreso"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_FechaIngreso" HighlightCssClass="validatorCalloutHighlight" />
                                                                <!-- TextBox_Ultimosalario -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Ultimosalario"
                                                                    ControlToValidate="TextBox_Ultimosalario" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ÚLTIMO SALARIO es requerido."
                                                                    ValidationGroup="EXPLAB" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Ultimosalario"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_Ultimosalario" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                                <asp:Panel ID="Panel_ExperienciaLaboral" runat="server">
                                                    <div class="div_espaciador">
                                                    </div>
                                                    <div style="text-align: left; margin-left: 10px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="Button_NuevoEmpleo" runat="server" Text="Nuevo Empleo" CssClass="margin_botones"
                                                                        ValidationGroup="NUEVOEXPLAB" OnClick="Button_NuevoEmpleo_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_GuardarEmpleo" runat="server" Text="Guardar Empleo" CssClass="margin_botones"
                                                                        ValidationGroup="EXPLAB" OnClick="Button_GuardarEmpleo_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="Button_CancelarEmpleo" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                        ValidationGroup="CANCELAREXPLAB" OnClick="Button_CancelarEmpleo_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="div_tag_entrevista">
                                            <ul>
                                                <li>Observaciones y datos adcionales para <b>EXPERIENCIA LABORAL:</b></li>
                                            </ul>
                                        </div>
                                        <asp:TextBox ID="TextBox_COM_F_LAB" runat="server" Height="100px" TextMode="MultiLine"
                                            Width="850px" ValidationGroup="GUARDAR" onPaste="return false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FCH_ENTREVISTA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FCH_ENTREVISTA"
                                ControlToValidate="TextBox_FCH_ENTREVISTA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE ENTREVISTA es requerida."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FCH_ENTREVISTA"
                                TargetControlID="RequiredFieldValidator_TextBox_FCH_ENTREVISTA" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_COM_C_FAM -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_COM_C_FAM"
                                ControlToValidate="TextBox_COM_C_FAM" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La COMPOSICIÓN FAMILIAR es requerida."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_COM_C_FAM"
                                TargetControlID="RequiredFieldValidator_TextBox_COM_C_FAM" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_COM_C_ACA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_COM_C_ACA"
                                ControlToValidate="TextBox_COM_C_ACA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Los ESTUDIOS son requeridos."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_COM_C_ACA"
                                TargetControlID="RequiredFieldValidator_TextBox_COM_C_ACA" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_COM_F_LAB -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_COM_F_LAB"
                                ControlToValidate="TextBox_COM_F_LAB" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EXPERIENCIA LABORALes requerida."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_COM_F_LAB"
                                TargetControlID="RequiredFieldValidator_TextBox_COM_F_LAB" HighlightCssClass="validatorCalloutHighlight" />
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel_PRUEBAS" runat="server">
                        <div class="div_espaciador">
                        </div>
                        <div class="div_contenedor_formulario">
                            <div class="div_cabeza_groupbox">
                                Datos pruebas
                            </div>
                            <div class="div_contenido_groupbox">
                                <asp:Panel ID="Panel_FONDO_MENSAJE_PRUEBAS" runat="server" Visible="false" Style="background-color: #999999;">
                                </asp:Panel>
                                <asp:Panel ID="Panel_MENSAJES_PRUEBAS" runat="server">
                                    <asp:Image ID="Image_MENSAJE_PRUEBAS_POPUP" runat="server" Width="50px" Height="50px"
                                        Style="margin: 5px auto 8px auto; display: block;" />
                                    <asp:Panel ID="Panel_COLOR_FONDO_PRUEBAS_POPUP" runat="server" Style="text-align: center">
                                        <asp:Label ID="Label_MENSAJE_PRUEBAS" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                                    </asp:Panel>
                                    <div style="text-align: center; margin-top: 15px;">
                                        <asp:Button ID="Button_CERRAR_MENSAJE_PRUEBAS" runat="server" Text="Cerrar" CssClass="margin_botones"
                                            OnClick="Button_CERRAR_MENSAJE_PRUEBAS_Click" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel_GRILLA_SELECCION_PRUEBAS" runat="server">
                                    <div class="div_avisos_importantes">
                                        Seleccione de la Siguiente Lista las Pruebas que Desea Registrar al Candidato.
                                    </div>
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_SELECCION_PRUEBAS" runat="server" AutoGenerateColumns="False"
                                                Width="885px" DataKeyNames="ID_PRUEBA,NOM_PRUEBA,ID_CATEGORIA">
                                                <Columns>
                                                    <asp:BoundField DataField="ID_PRUEBA" HeaderText="Id prueba">
                                                        <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOM_PRUEBA" HeaderText="Nombre prueba" />
                                                    <asp:BoundField DataField="NOM_CATEGORIA" HeaderText="Nombre categoría" />
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox_PRUEBA" runat="server" />
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
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_GENERAR_RESULTADOS_PRUEBAS" runat="server" Text="Obtener pruebas"
                                                    CssClass="margin_botones" OnClick="Button_GENERAR_RESULTADOS_PRUEBAS_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_SIN_PRUEBAS" runat="server" Text="Entrevista sin Pruebas"
                                                    CssClass="margin_botones" OnClick="Button_SIN_PRUEBAS_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel_PRUEBAS_SELECCIONADAS" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Pruebas seleccionadas
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_PRUEBAS_SELECCIONADAS" runat="server" AutoGenerateColumns="False"
                                                    Width="865px" DataKeyNames="ID_APLICACION_PRUEBA,ID_PRUEBA,ID_CATEGORIA,TIENE_IMAGEN,NOM_PRUEBA">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Id Prueba" DataField="ID_PRUEBA">
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="45px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOM_PRUEBA" HeaderText="Nombre Prueba">
                                                            <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Fecha">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox_FECHA_PRUEBA" runat="server" ValidationGroup="GUARDAR" Width="110px"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_PRUEBA" runat="server"
                                                                    TargetControlID="TextBox_FECHA_PRUEBA" Format="dd/MM/yyyy" />
                                                                <!-- TextBox_FECHA_PRUEBA -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_PRUEBA"
                                                                    ControlToValidate="TextBox_FECHA_PRUEBA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA es requerida."
                                                                    ValidationGroup="GUARDAR" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_PRUEBA"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_FECHA_PRUEBA" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="120px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Interpretación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox_RESULTADO_PRUEBAS" TextMode="MultiLine" Width="300px" Height="180px"
                                                                    runat="server" onPaste="return false" ValidationGroup="GUARDAR"></asp:TextBox>
                                                                <!-- TextBox_RESULTADO_PRUEBAS -->
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_RESULTADO_PRUEBAS"
                                                                    ControlToValidate="TextBox_RESULTADO_PRUEBAS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Los RESULTADOS son requeridos."
                                                                    ValidationGroup="GUARDAR" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_RESULTADO_PRUEBAS"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_RESULTADO_PRUEBAS" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Archivo">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="FileUpload_ARCHIVO" runat="server" />
                                                                <br />
                                                                <br />
                                                                <asp:HyperLink ID="HyperLink_ARCHIVO" runat="server" Style="margin: 0 auto; text-align: center;">Ver Resultados</asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>



            <%--ESTE UPDATE PANEL ES EL CONTENEDOR DE LAS SECCIONES DE HABILIDADES ASSESMENT CENTER Y COMPETENCIAS.--%>
            <asp:HiddenField ID="HiddenField_TIPO_ENTREVISTA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_ASSESMENT_CENTER" runat="server" />
            <asp:Panel ID="Panel_FONDO_MENSAJE_TIPO" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES_TIPO" runat="server">
                <asp:Image ID="Image_MENSAJE_TIPO_POPUP" runat="server" Width="50px" Height="50px"
                    Style="margin: 5px auto 8px auto; display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_TIPO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE_TIPO" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE_TIPO" runat="server" Text="Cerrar" CssClass="margin_botones" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_ContenedorTipoEntrevista" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Tipo de Entrevista
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_SELECCION_TIPO_ENTREVISTA" runat="server">
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

                        <asp:Panel ID="Panel_AvisoDeSeleccionDeTipoEntrevista" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_contenedor_formulario">
                                <div style="text-align: center; color: Blue; font-size: 12px;">
                                    Seleccione el tipo de entrevista. Si seleccionó entrevista por Competencias / Habilidades debe seleccionar el 
                                    Assesment Center Que se ajuste.
                                </div>
                            </div>
                        </asp:Panel>

                        
                        <asp:Panel ID="Panel_SeleccionCompetencias" runat="server">
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
                                            <asp:DropDownList ID="DropDownList_AssesmentCenter" runat="server" Width="500px"
                                                ValidationGroup="SELECCIONCOMPETENCIAS" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_AssesmentCenter_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <%--DropDownList_AssesmentCenter--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_AssesmentCenter"
                                    ControlToValidate="DropDownList_AssesmentCenter" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ASSESMENT CENTER es requerido."
                                    ValidationGroup="SELECCIONCOMPETENCIAS" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_AssesmentCenter"
                                    TargetControlID="RequiredFieldValidator_DropDownList_AssesmentCenter" HighlightCssClass="validatorCalloutHighlight" />

                                <asp:Panel ID="Panel_InformacionAssesmentSeleccionado" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <table cellpadding="3" cellspacing="0" class="table_control_registros" width="610" style="border:1px solid #cccccc;">
                                        <tr>
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
                                        <tr>
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
                        </asp:Panel>
                        <asp:Panel ID="Panel_BotonesDeSeleciconTipoEntrevista" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_AceptarSeleccionDeTipoEntrevista" runat="server" Text="Aceptar Configuración y Continuar" ValidationGroup="SELECCIONCOMPETENCIAS"
                                            OnClick="Button_AceptarSeleccionDeTipoEntrevista_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        
                        
                        <asp:Panel ID="Panel_CALIFICACIONES_COMPETENCIAS" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox">
                                Competencias / Habilidades Evaluadas
                            </div>
                            <div class="div_contenido_groupbox">
                                
                                <div style="text-align:center; font-weight:bold;">
                                    <asp:Label ID="Label_NombreAssesmentCenter" runat="server" Text="NOMBRE DEL ASSESMENT CENTER SELECCIONADO"></asp:Label>
                                </div>
                                <div class="div_espaciador"></div>
                                <div style="text-align:justify;">
                                    <asp:Label ID="Label_Descripcion2Assesment" runat="server" Text="Descripción de Assesment Center seleccionado"></asp:Label>
                                </div>
                                <div class="div_espaciador"></div>
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <asp:GridView ID="GridView_CompetenciasAssesment" runat="server" Width="865px" AutoGenerateColumns="False"
                                            DataKeyNames="ID_APLICACION_COMPETENCIA,ID_COMPETENCIA_ASSESMENT">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Competencia / Habilidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_Competencia" runat="server" Text="Nombre de la Competencia"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Definición">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_DefinicionCompetencia" runat="server" Text="Definición de la competencia"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cumple">
                                                    <ItemTemplate>
                                                        <table class="table_control_registros">
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="CheckBox_CumpleCompetencia" runat="server" 
                                                                        AutoPostBack="True" 
                                                                        oncheckedchanged="CheckBox_CumpleCompetencia_CheckedChanged" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DropDownList_NivelCumplimiento" runat="server" Width="60px" ValidationGroup="GUARDAR">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <%--DropDownList_NivelCumplimiento--%>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_NivelCumplimiento"
                                                            ControlToValidate="DropDownList_NivelCumplimiento" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NIVEL DE CUMPLIMIENTO es requerido."
                                                            ValidationGroup="GUARDAR" Enabled="false"/>
                                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_NivelCumplimiento"
                                                            TargetControlID="RequiredFieldValidator_DropDownList_NivelCumplimiento" HighlightCssClass="validatorCalloutHighlight" Enabled="false"/>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" Width="100px"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No Cumple">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox_NoCumpeCompetencia" runat="server" 
                                                            AutoPostBack="True" 
                                                            oncheckedchanged="CheckBox_NoCumpeCompetencia_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" Width="60px"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Observaciones">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_ObservacionesCalificacion" runat="server" TextMode="MultiLine"
                                                            Width="250px" Height="40px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" Width="260px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <headerStyle BackColor="#DDDDDD" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="Panel_CONCEPTO_GENERAL" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox">
                                Concepto general
                            </div>
                            <div class="div_contenido_groupbox">
                                <div class="div_titulos_entrevista">
                                    CONCEPTO GENERAL
                                </div>
                                <div class="div_tag_entrevista">
                                    El candidato se considera apto por:<br />
                                    <strong>AJUSTE AL PERFIL:</strong><br />
                                    <ul>
                                        <li>Formación y conocimientos especificos.</li>
                                        <li>Experiencia.</li>
                                    </ul>
                                </div>
                                <asp:TextBox ID="TextBox_CONCEPTO_GENERAL" runat="server" Height="200px" TextMode="MultiLine"
                                    Width="850px" ValidationGroup="GUARDAR" onPaste="return false"></asp:TextBox>
                                <!-- TextBox_CONCEPTO_GENERAL -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONCEPTO_GENERAL"
                                    ControlToValidate="TextBox_CONCEPTO_GENERAL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CONCEPTO GENERAL es requerido."
                                    ValidationGroup="GUARDAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONCEPTO_GENERAL"
                                    TargetControlID="RequiredFieldValidator_TextBox_CONCEPTO_GENERAL" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_BOTONES_ACCION_1" runat="server">
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
                                                OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_DOCUMENTOS_1" runat="server" Text="Validar documentos" CssClass="margin_botones"
                                                OnClick="Button_documentos_Click" ValidationGroup="DOCUMENTOS" />
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

