<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="solicitudIngreso.aspx.cs" Inherits="_SolicitudIngreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
                $('.textbox_ajustado_money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>

    <script language="javascript" type="text/javascript">
        setInterval('MantenSesion()', <%= (int) (0.9 * (Session.Timeout * 60000)) %>);
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
        
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />

            <%--CONTROL DE ESTADO PARA CADA PESTAÑA--%>
            <asp:HiddenField ID="HiddenField_IndexTab" runat="server" />

            <asp:HiddenField ID="HiddenField_EstadoDatosBasicos" runat="server" />
            <asp:HiddenField ID="HiddenField_EstadoUbicacion" runat="server" />
            <asp:HiddenField ID="HiddenField_EstadoEducacion" runat="server" />
            <asp:HiddenField ID="HiddenField_EstadoDatosLaborales" runat="server" />
            <asp:HiddenField ID="HiddenField_EstadoFamilia" runat="server" />
            <asp:HiddenField ID="HiddenField_DatosAdicionales" runat="server" />
            <asp:HiddenField ID="HiddenField_AccionSobreFormulario" runat="server" />

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


            <asp:Panel ID="Panel_BUSQUEDA_CEDULA" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Validación de Aspirante
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Label ID="Label_VALIDAR_CEDULA" runat="server" Text="Número de documento"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox_VALIDAR_CEDULA" runat="server" MaxLength="20" 
                                        ValidationGroup="VALIDAR" Width="260px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button_VALIDAR_CEDULA" runat="server" Text="Validar cédula" OnClick="Button_VALIDAR_CEDULA_Click"
                                        ValidationGroup="VALIDAR" CssClass="margin_botones" />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_VALIDAR_CEDULA -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_VALIDAR_CEDULA" runat="server"
                            ControlToValidate="TextBox_VALIDAR_CEDULA" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE DOCUMENTO es requerido."
                            ValidationGroup="VALIDAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_VALIDAR_CEDULA"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_VALIDAR_CEDULA" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_VALIDAR_CEDULA"
                            runat="server" TargetControlID="TextBox_VALIDAR_CEDULA" FilterType="Numbers">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
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
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <input class="margin_botones" id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>


            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_espaciador"></div>
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
                                CollapseControlID="Panel_CABEZA_REGISTRO" Collapsed="False" TextLabelID="Label_REGISTRO"
                                ImageControlID="Image_REGISTRO" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                                ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                                SuppressPostBack="true">
                            </ajaxToolkit:CollapsiblePanelExtender>
                        </asp:Panel>

                        <asp:Panel ID="Panel_EstadoCandidato" runat="server">
                            <div class="div_espaciador"></div>
                            <table width="100%">
                                <tr>
                                    <td style="width:80%;" class="td_izq">
                                        Estado:
                                    </td>
                                    <td style="width:20%;" class="td_izq">
                                        <asp:Label ID="Label_EstadoAspirante" runat="server" Text="Estado del Candidato" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_DevolverEnCliente" runat="server" Text="A Disponible" 
                                            Width="95px" onclick="Button_DevolverEnCliente_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:80%;" class="td_izq">
                                        Fecha Radicación Hoja de Vida:
                                    </td>
                                    <td style="width:20%;" class="td_izq">
                                        <asp:Label ID="Label_FechaIngreso" runat="server" Text="Fecha Ingreso Candidato" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <asp:Panel ID="Panel_Pestanas" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_para_convencion_hoja_trabajo">
                                <table class="tabla_alineada_derecha">
                                    <tr>
                                        <td>
                                            Pestaña Correcta.
                                        </td>
                                        <td class="td_contenedor_colores_hoja_trabajo">
                                            <div class="div_color_verde_pestana">
                                            </div>
                                        </td>
                                        <td>
                                            Pestaña Seleccionada.
                                        </td>
                                        <td class="td_contenedor_colores_hoja_trabajo">
                                            <div class="div_color_gris_pestana">
                                            </div>
                                        </td>
                                        <td>
                                            Pestaña con Datos Faltantes
                                        </td>
                                        <td class="td_contenedor_colores_hoja_trabajo">
                                            <div class="div_color_rojo_pestana">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button Text="Datos Básicos" BorderStyle="None" ID="Tab_DatosBasicos" 
                                            CssClass="botonActivo" runat="server" onclick="Tab_DatosBasicos_Click"/>        
                                    </td>
                                    <td>
                                        <asp:Button Text="Ubicación" BorderStyle="None" ID="Tab_Ubicacion" 
                                            CssClass="botonInactivoFail" runat="server" onclick="Tab_Ubicacion_Click"/>        
                                    </td>
                                    <td>
                                        <asp:Button Text="Educación" BorderStyle="None" ID="Tab_Educacion" 
                                            CssClass="botonInactivoFail" runat="server" onclick="Tab_Educacion_Click"/>        
                                    </td>
                                    <td>
                                        <asp:Button Text="Datos Laborales" BorderStyle="None" ID="Tab_DatosLaborales" 
                                            CssClass="botonInactivoFail" runat="server" 
                                            onclick="Tab_DatosLaborales_Click"/>        
                                    </td>
                                    <td>
                                        <asp:Button Text="Familia" BorderStyle="None" ID="Tab_Familia" 
                                            CssClass="botonInactivoFail" runat="server" onclick="Tab_Familia_Click"/>        
                                    </td>
                                    <td>
                                        <asp:Button Text="Datos Adicionales" BorderStyle="None" ID="Tab_Adicionales" 
                                            CssClass="botonInactivoFail" runat="server" 
                                            onclick="Tab_Adicionales_Click"/>        
                                    </td>
                                </tr>
                            </table>
                           

                            <asp:MultiView ID="MainView" runat="server">
                                <asp:View ID="View_DatosBasicos" runat="server">
                                    <div class="tab_radicacion">
                                        <table cellspacing="5">
                                            <tr>
                                                <td class="td_der">
                                                    Fecha de Nacimiento<br />
                                                    <asp:TextBox ID="TextBox_FCH_NACIMIENTO" runat="server" ValidationGroup="GUARDAR"
                                                        MaxLength="10" Width="130px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FCH_NACIMIENTO" runat="server"
                                                        Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_NACIMIENTO" />
                                                </td>
                                                <td class="td_der">
                                                    País de Nacimiento:<br />
                                                    <asp:DropDownList ID="DropDownList_PaisNacimiento" runat="server" ValidationGroup="GUARDAR"
                                                        Width="260px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_PaisNacimiento_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_der">
                                                    Sexo<br />
                                                    <asp:DropDownList ID="DropDownList_SEXO" runat="server" ValidationGroup="GUARDAR"
                                                        AutoPostBack="True" CssClass="textbox_ajustado" 
                                                        onselectedindexchanged="DropDownList_SEXO_SelectedIndexChanged" 
                                                        Width="260px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_der">
                                                    Rh<br />
                                                    <asp:DropDownList ID="DropDownList_RH" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>

                                        <!-- DropDownList_PaisNacimiento -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_PaisNacimiento"
                                            runat="server" ControlToValidate="DropDownList_PaisNacimiento" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>El PAIS DE NACIMIENTO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_PaisNacimiento"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_PaisNacimiento" />

                                        <!-- TextBox_FCH_NACIMIENTO -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FCH_NACIMIENTO" runat="server"
                                            ControlToValidate="TextBox_FCH_NACIMIENTO" Display="None" ErrorMessage="Campo Requerido faltante</br>La FECHA DE NACIMIENTO es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FCH_NACIMIENTO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FCH_NACIMIENTO" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FCH_NACIMIENTO"
                                            runat="server" TargetControlID="TextBox_FCH_NACIMIENTO" FilterType="Custom,Numbers"
                                            ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                                        <!-- DropDownList_SEXO -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_SEXO" runat="server" ControlToValidate="DropDownList_SEXO"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>El SEXO es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_SEXO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_SEXO" />

                                        <!-- DropDownList_RH -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_RH" runat="server" ControlToValidate="DropDownList_RH"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>El RH es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_RH"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_RH" />

                                        <div class="div_espaciador"></div>

                                        <table class="table_form_radicacion" cellspacing="5">
                                            <tr>
                                                <td class="td_3">
                                                    <table class="table_ajustada" cellspacing="5">
                                                        <tr>
                                                            <td class="td_2">
                                                                Tipo Documento<br />
                                                                <asp:DropDownList ID="DropDownList_TIP_DOC_IDENTIDAD" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="td_2">
                                                                Número Documento<br />
                                                                <asp:TextBox ID="TextBox_NUM_DOC_IDENTIDAD" runat="server" 
                                                                    ValidationGroup="GUARDAR" MaxLength="16" CssClass="textbox_ajustado"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUM_DOC_IDENTIDAD"
                                                                    runat="server" TargetControlID="TextBox_NUM_DOC_IDENTIDAD" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_3">
                                                    Departamento<br />
                                                    <asp:DropDownList ID="DropDownList_DEPARTAMENTO_CEDULA" runat="server" ValidationGroup="GUARDAR"
                                                        CssClass="textbox_ajustado" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownList_DEPARTAMENTO_CEDULA_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_3">
                                                    Ciudad<br />
                                                    <asp:DropDownList ID="DropDownList_CIU_CEDULA" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <!-- DropDownList_TIP_DOC_IDENTIDAD -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_TIP_DOC_IDENTIDAD"
                                            runat="server" ControlToValidate="DropDownList_TIP_DOC_IDENTIDAD" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>El TIPO DE DOCUMENTO es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_TIP_DOC_IDENTIDAD"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_TIP_DOC_IDENTIDAD" />

                                        <!-- TextBox_NUM_DOC_IDENTIDAD -->
                                        <asp:RequiredFieldValidator ID="RFV_TextBox_NUM_DOC_IDENTIDAD"
                                            runat="server" ControlToValidate="TextBox_NUM_DOC_IDENTIDAD" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE DOCUMENTO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_NUM_DOC_IDENTIDAD"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_TextBox_NUM_DOC_IDENTIDAD" />

                                        <!-- DropDownList_CIU_CEDULA -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_CIU_CEDULA" runat="server"
                                            ControlToValidate="DropDownList_CIU_CEDULA" Display="None" ErrorMessage="Campo Requerido faltante</br>La CIUDAD DE EXPEDICIÓN es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CIU_CEDULA"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_CIU_CEDULA" />

                                        <div class="div_espaciador"></div>

                                        <table cellpadding="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Nombres<br />
                                                    <asp:TextBox ID="TextBox_NOMBRES" runat="server" ValidationGroup="GUARDAR" MaxLength="100" Width="100%"
                                                        CssClass="textbox_ajustado"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NOMBRES"
                                                        runat="server" TargetControlID="TextBox_NOMBRES" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                                        ValidChars=" áéíóúÁÉÍÓÚÑñüÜàèìòùÀÈÌÒÙ" />
                                                </td>
                                                <td class="td_2">
                                                    Apellidos<br />
                                                    <asp:TextBox ID="TextBox_APELLIDOS" runat="server" ValidationGroup="GUARDAR" MaxLength="100" Width="100%"
                                                        CssClass="textbox_ajustado"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_APELLIDOS"
                                                        runat="server" TargetControlID="TextBox_APELLIDOS" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                                        ValidChars=" áéíóúÁÉÍÓÚÑñüÜàèìòùÀÈÌÒÙ" />
                                                </td>
                                            </tr>
                                        </table>

                                        <!-- TextBox_APELLIDOS -->
                                        <asp:RequiredFieldValidator ID="RFV_TextBox_APELLIDOS" runat="server" ControlToValidate="TextBox_APELLIDOS"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>Los APELLIDOS son requeridos."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_APELLIDOS"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_TextBox_APELLIDOS" />
                                        <!-- TextBox_NOMBRES -->
                                        <asp:RequiredFieldValidator ID="RFV_TextBox_NOMBRES" runat="server" ControlToValidate="TextBox_NOMBRES"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>Los NOMBRES son requeridos."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_NOMBRES"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_TextBox_NOMBRES" />
                                        
                                        <div class="div_espaciador"></div>

                                        <table cellpadding="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Libreta Militar<br />
                                                    <asp:TextBox ID="TextBox_LIB_MILITAR" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado"
                                                        MaxLength="15"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LIB_MILITAR"
                                                        runat="server" TargetControlID="TextBox_LIB_MILITAR" FilterType="Numbers" />
                                                </td>
                                                <td class="td_2">
                                                    Categoría Conducción<br />
                                                    <asp:DropDownList ID="DropDownList_CAT_LIC_COND" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>

                                        <!-- TextBox_LIB_MILITAR -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_LIB_MILITAR" runat="server"
                                            ControlToValidate="TextBox_LIB_MILITAR" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE LA LIBRETA MILITAR es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_LIB_MILITAR"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_LIB_MILITAR" />

                                        <div class="div_espaciador"></div>

                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    <table cellspacing="5" class="table_ajustada">
                                                        <tr>
                                                            <td class="td_2">
                                                                Teléfono<br />
                                                                <asp:TextBox ID="TextBox_TEL_ASPIRANTE" runat="server" ValidationGroup="GUARDAR"
                                                                    CssClass="textbox_ajustado" MaxLength="20"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_TextBox_TEL_ASPIRANTE"
                                                                    runat="server" TargetControlID="TextBox_TEL_ASPIRANTE" FilterType="Numbers,Custom"
                                                                    ValidChars="()[]{}- extEXT" />
                                                            </td>
                                                            <td class="td_2">
                                                                Celular<br />
                                                                <asp:TextBox ID="TextBox_CEL_ASPIRANTE" runat="server" ValidationGroup="GUARDAR"
                                                                    CssClass="textbox_ajustado" MaxLength="10"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CEL_ASPIRANTE"
                                                                    runat="server" TargetControlID="TextBox_CEL_ASPIRANTE" FilterType="Numbers" />
                                                            </td>
                                                        </tr>
                                                    </table>    
                                                </td>
                                                <td class="td_2">
                                                    E-Mail<br />
                                                    <asp:TextBox ID="TextBox_E_MAIL" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado"
                                                        MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_TEL_ASPIRANTE -->
                                        <asp:RequiredFieldValidator runat="server" ID="RFV_TextBox_TEL_ASPIRANTE"
                                            ControlToValidate="TextBox_TEL_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO DEL ASPIRANTE es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_TEL_ASPIRANTE"
                                            TargetControlID="RFV_TextBox_TEL_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />

                                        <!-- TextBox_CEL_ASPIRANTE -->
                                        <asp:RequiredFieldValidator runat="server" ID="RFV_TextBox_CEL_ASPIRANTE"
                                            ControlToValidate="TextBox_CEL_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CELULAR DEL ASPIRANTE es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CEL_ASPIRANTE"
                                            TargetControlID="RFV_TextBox_CEL_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />

                                        <!-- TextBox_E_MAIL -->
                                        <asp:RequiredFieldValidator runat="server" ID="RFV_TextBox_E_MAIL"
                                            ControlToValidate="TextBox_E_MAIL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El E-MAIL del aspirante es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_E_MAIL"
                                            TargetControlID="RFV_TextBox_E_MAIL" HighlightCssClass="validatorCalloutHighlight" />
                                        <asp:RegularExpressionValidator ID="REV_TextBox_E_MAIL" Display="None"
                                            runat="server" ErrorMessage="<b>E-Mail incorrecto</b><br />El E-MAIL tiene un formato incorrecto."
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox_E_MAIL"
                                            ValidationGroup="GUARDAR"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_E_MAIL_1"
                                            TargetControlID="REV_TextBox_E_MAIL" HighlightCssClass="validatorCalloutHighlight" />
                                    </div>
                                </asp:View>
                                
                                <asp:View ID="View_Ubicacion" runat="server">
                                    <div class="tab_radicacion">
                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Departamento<br />
                                                    <asp:DropDownList ID="DropDownList_DEPARTAMENTO_ASPIRANTE" runat="server" CssClass="textbox_ajustado"
                                                        AutoPostBack="True"
                                                        ValidationGroup="GUARDAR" 
                                                        onselectedindexchanged="DropDownList_DEPARTAMENTO_ASPIRANTE_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_2">
                                                    Ciudad<br />
                                                    <asp:DropDownList ID="DropDownList_CIU_ASPIRANTE" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_CIU_ASPIRANTE -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CIU_ASPIRANTE"
                                            runat="server" ControlToValidate="DropDownList_CIU_ASPIRANTE" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>La CIUDAD DEL ASPIRANTE es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CIU_ASPIRANTE"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CIU_ASPIRANTE" />

                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Dirección<br />
                                                    <asp:TextBox ID="TextBox_DIR_ASPIRANTE" runat="server" ValidationGroup="GUARDAR"
                                                        CssClass="textbox_ajustado" MaxLength="255"></asp:TextBox>
                                                </td>
                                                <td class="td_2">
                                                    Barrio<br />
                                                    <asp:TextBox ID="TextBox_SECTOR" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado"
                                                        MaxLength="255"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_DIR_ASPIRANTE -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DIR_ASPIRANTE"
                                            ControlToValidate="TextBox_DIR_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD DEL ASPIRANTE es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DIR_ASPIRANTE"
                                            TargetControlID="RequiredFieldValidator_TextBox_DIR_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />

                                        <!-- TextBox_SECTOR -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SECTOR"
                                            ControlToValidate="TextBox_SECTOR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El BARRIO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SECTOR"
                                            TargetControlID="RequiredFieldValidator_TextBox_SECTOR" HighlightCssClass="validatorCalloutHighlight" />

                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Tipo Vivienda<br />
                                                    <asp:DropDownList ID="DropDownList_TipoVivienda" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_2">
                                                    Estrato<br />
                                                    <asp:DropDownList ID="DropDownList_ESTRATO" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                        <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_TipoVivienda -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TipoVivienda"
                                            ControlToValidate="DropDownList_TipoVivienda" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La TIPO DE VIVIENDA es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TipoVivienda"
                                            TargetControlID="RequiredFieldValidator_DropDownList_TipoVivienda" HighlightCssClass="validatorCalloutHighlight" />

                                        <!-- DropDownList_ESTRATO -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ESTRATO"
                                            ControlToValidate="DropDownList_ESTRATO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTRATO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ESTRATO"
                                            TargetControlID="RequiredFieldValidator_DropDownList_ESTRATO" HighlightCssClass="validatorCalloutHighlight" />
                                    </div>
                                </asp:View>

                                <asp:View ID="View_Educacion" runat="server">
                                    <div class="tab_radicacion">
                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Nivel de escolaridad<br />
                                                    <asp:DropDownList ID="DropDownList_NIV_EDUCACION" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_2">
                                                    Profesión<br />
                                                    <asp:DropDownList ID="DropDownList_nucleo_formacion" runat="server" CssClass="textbox_ajustado"
                                                        ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_NIV_EDUCACION -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_NIV_EDUCACION"
                                            runat="server" ControlToValidate="DropDownList_NIV_EDUCACION" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>El NIVEL DE ESTUDIOS es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_NIV_EDUCACION"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_NIV_EDUCACION" />

                                        <!-- DropDownList_NUCLEO_FORMACION -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_nucleo_formacion"
                                            runat="server" ControlToValidate="DropDownList_nucleo_formacion" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>El NUCLEO FORMACIÓN es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_nucleo_formacion"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_nucleo_formacion" />

                                        <div class="div_espaciador"></div>

                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                                        <asp:GridView ID="GridView_EducacionFormal" runat="server" Width="864px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_INFO_ACADEMICA" 
                                                            OnRowCommand="GridView_EducacionFormal_RowCommand">
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
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_NivAcademico"
                                                                            ControlToValidate="DropDownList_NivAcademico" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El GRADO DE INSTRUCCIÓN es requerido."
                                                                            ValidationGroup="EDUFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                ID="ValidatorCalloutExtender_DropDownList_NivAcademico" TargetControlID="RequiredFieldValidator_DropDownList_NivAcademico"
                                                                                HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Institución">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Institucion" runat="server" ValidationGroup="EDUFORMAL"
                                                                            Width="240px" CssClass="TextBox_con_letra_pequena"></asp:TextBox><asp:RequiredFieldValidator
                                                                                runat="server" ID="RequiredFieldValidator_TextBox_Institucion" ControlToValidate="TextBox_Institucion"
                                                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Institución es requerida."
                                                                                ValidationGroup="EDUFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                    ID="ValidatorCalloutExtender_TTextBox_Institucion" TargetControlID="RequiredFieldValidator_TextBox_Institucion"
                                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Año">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Anno" runat="server" Width="70px" CssClass="TextBox_con_letra_pequena"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender
                                                                            ID="FilteredTextBoxExtender_TextBox_Anno" runat="server" TargetControlID="TextBox_Anno"
                                                                            FilterType="Numbers" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="270px" TextMode="MultiLine"
                                                                            Height="45px" CssClass="TextBox_con_letra_pequena"></asp:TextBox></ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel_BotonesEducacionFormal" runat="server">
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

                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
                                                        <asp:GridView ID="GridView_EducacionNoFormal" runat="server" Width="864px" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_INFO_ACADEMICA" 
                                                            OnRowCommand="GridView_EducacionNoFormal_RowCommand">
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
                                                                            CssClass="TextBox_con_letra_pequena" TextMode="MultiLine" Height="60px"></asp:TextBox><asp:RequiredFieldValidator
                                                                                runat="server" ID="RequiredFieldValidator_TextBox_Curso" ControlToValidate="TextBox_Curso"
                                                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CURSO es requerido."
                                                                                ValidationGroup="EDUNOFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                    ID="ValidatorCalloutExtender_TextBox_Curso" TargetControlID="RequiredFieldValidator_TextBox_Curso"
                                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Institución">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Institucion" runat="server" ValidationGroup="EDUNOFORMAL"
                                                                            CssClass="TextBox_con_letra_pequena" Width="220px" Height="60px" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                                                runat="server" ID="RequiredFieldValidator_TextBox_Institucion" ControlToValidate="TextBox_Institucion"
                                                                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Institución es requerida."
                                                                                ValidationGroup="EDUNOFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                    ID="ValidatorCalloutExtender_TextBox_Institucion_1" TargetControlID="RequiredFieldValidator_TextBox_Institucion"
                                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Duración">
                                                                    <ItemTemplate>
                                                                        Tiempo:
                                                                        <asp:TextBox ID="TextBox_Duracion" runat="server" Width="70px" ValidationGroup="EDUNOFORMAL"
                                                                            CssClass="TextBox_con_letra_pequena"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender_TextBox_Duracion" runat="server" TargetControlID="TextBox_Duracion"
                                                                                FilterType="Numbers,Custom" ValidChars="," />
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Duracion"
                                                                            ControlToValidate="TextBox_Duracion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DURción es requerida."
                                                                            ValidationGroup="EDUNOFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                ID="ValidatorCalloutExtender_TextBox_Duracion" TargetControlID="RequiredFieldValidator_TextBox_Duracion"
                                                                                HighlightCssClass="validatorCalloutHighlight" />
                                                                        Unidad:
                                                                        <asp:DropDownList ID="DropDownList_UnidadDuracion" runat="server" ValidationGroup="EDUNOFORMAL"
                                                                            Width="70px" CssClass="TextBox_con_letra_pequena">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_UnidadDuracion"
                                                                            ControlToValidate="DropDownList_UnidadDuracion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La UNIDAD es requerida."
                                                                            ValidationGroup="EDUNOFORMAL" /><ajaxToolkit:ValidatorCalloutExtender runat="Server"
                                                                                ID="ValidatorCalloutExtender_DropDownList_UnidadDuracion" TargetControlID="RequiredFieldValidator_DropDownList_UnidadDuracion"
                                                                                HighlightCssClass="validatorCalloutHighlight" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" Width="80px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="250px" TextMode="MultiLine"
                                                                            Height="45px" CssClass="TextBox_con_letra_pequena"></asp:TextBox></ItemTemplate>
                                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel_BotonesEducacionNoFormal" runat="server">
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
                                    </div>
                                </asp:View>
                                <asp:View ID="View_DatosLaborales" runat="server">
                                    <div class="tab_radicacion">
                                        <table class="div_avisos_importantes">
                                            <tr>
                                                <td>
                                                    Debe realizar la busqueda del cargo interno al que aspira el candidato. escriba
                                                    el nombre del cargo, oprima el botón BUSCAR, y luego utilice la lista de CARGOS
                                                    ENCONTRADOS para seleccionar el cargo.
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_3">   
                                                    (1). Buscar Cargo<br />
                                                    <table class="table_ajustada">
                                                        <tr>
                                                            <td style="width:92%">
                                                                <asp:TextBox ID="TextBox_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                                    CssClass="textbox_ajustado" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td style="width:8%">
                                                                <asp:ImageButton ID="ImageButton_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                                    ImageUrl="~/imagenes/areas/view2.gif" 
                                                                    onclick="ImageButton_BUSCADOR_CARGO_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="td_3">
                                                    (2). Seleccionar Cargo<br />
                                                    <asp:DropDownList ID="DropDownList_ID_OCUPACION" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_3">
                                                    (3). Experiencia<br />
                                                    <asp:DropDownList ID="DropDownList_EXPERIENCIA" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_BUSCADOR_CARGO -->
                                        <asp:RequiredFieldValidator ID="RFV_TextBox_BUSCADOR_CARGO" runat="server"
                                            ControlToValidate="TextBox_BUSCADOR_CARGO" Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="BUSCADORCARGO" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_BUSCADOR_CARGO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_TextBox_BUSCADOR_CARGO" />

                                        <!-- DropDownList_ID_OCUPACION -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_ID_OCUPACION"
                                            runat="server" ControlToValidate="DropDownList_ID_OCUPACION" Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_OCUPACION"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_ID_OCUPACION" />

                                        <!-- DropDownList_EXPERIENCIA -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_EXPERIENCIA"
                                            runat="server" ControlToValidate="DropDownList_EXPERIENCIA" Display="None" ErrorMessage="Campo Requerido faltante</br>La EXPERIENCIA es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_EXPERIENCIA"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_EXPERIENCIA" />

                                        <div class="div_espaciador"></div>

                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Aspiración Salarial<br />
                                                    <asp:TextBox ID="TextBox_ASPIRACION_SALARIAL" runat="server" ValidationGroup="GUARDAR"
                                                        CssClass="textbox_ajustado_money" MaxLength="15"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ASPIRACION_SALARIAL"
                                                        runat="server" TargetControlID="TextBox_ASPIRACION_SALARIAL" FilterType="Numbers,Custom"
                                                        ValidChars=".," />
                                                </td>
                                                <td class="td_2">
                                                    Área Interés Laboral<br />
                                                    <asp:DropDownList ID="DropDownList_AREAS_ESPECIALIZACION" runat="server" ValidationGroup="GUARDAR"
                                                        CssClass="textbox_ajustado">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_ASPIRACION_SALARIAL -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_ASPIRACION_SALARIAL"
                                            runat="server" ControlToValidate="TextBox_ASPIRACION_SALARIAL" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>La ASPIRACIÓN SALARIAL es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_ASPIRACION_SALARIAL"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_ASPIRACION_SALARIAL" />

                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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
                                                <asp:GridView ID="GridView_ExperienciaLaboral" runat="server" Width="864px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_EXPERIENCIA" 
                                                    OnRowCommand="GridView_ExperienciaLaboral_RowCommand">
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
                                                                <table class="table_control_registros" width="100%">
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            EMPRESA:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Empresa" runat="server" ValidationGroup="EXPLAB" Width="600px"
                                                                                CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            CARGO DESEMPEÑADO:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Cargo" runat="server" ValidationGroup="EXPLAB" Width="600px"
                                                                                CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            FUNCIONES REALIZADAS:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FuncionesRealizadas" runat="server" ValidationGroup="EXPLAB"
                                                                                Width="600px" TextMode="MultiLine" Height="70px" CssClass="TextBox_con_letra_pequena"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            FECHA INGRESO:
                                                                        </td>
                                                                        <td style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FechaIngreso" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="TextBox_con_letra_pequena" AutoPostBack="True" OnTextChanged="TextBox_FechaIngreso_TextChanged"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                                    ID="CalendarExtender_TextBox_FechaIngreso" runat="server" TargetControlID="TextBox_FechaIngreso"
                                                                                    Format="dd/MM/yyyy" />
                                                                        </td>
                                                                        <td rowspan="2" style="width: 20%; text-align: left;">
                                                                            <asp:Label ID="Label_TiempoTrabajado" runat="server" Text="Tiempo Desconocido"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            FECHA RETIRO:
                                                                        </td>
                                                                        <td style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_FechaRetiro" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="TextBox_con_letra_pequena" AutoPostBack="True" OnTextChanged="TextBox_FechaIngreso_TextChanged"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                                    ID="CalendarExtender_TextBox_FechaRetiro" runat="server" TargetControlID="TextBox_FechaRetiro"
                                                                                    Format="dd/MM/yyyy" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
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
                                                                        <td style="text-align: left; margin-left: 10px; width: 380px;">
                                                                            ÚLTIMO SALARIO:
                                                                        </td>
                                                                        <td colspan="2" style="text-align: left; margin-left: 10px;">
                                                                            <asp:TextBox ID="TextBox_Ultimosalario" runat="server" ValidationGroup="EXPLAB" Width="120px"
                                                                                CssClass="money"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Ultimosalario"
                                                                                    runat="server" TargetControlID="TextBox_Ultimosalario" FilterType="Numbers,Custom"
                                                                                    ValidChars=",." />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Empresa"
                                                                    ControlToValidate="TextBox_Empresa" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA es requerida."
                                                                    ValidationGroup="EXPLAB" /><ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Empresa"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_Empresa" HighlightCssClass="validatorCalloutHighlight" />
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Cargo"
                                                                    ControlToValidate="TextBox_Cargo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO DESEMPEÑADO es requerido."
                                                                    ValidationGroup="EXPLAB" /><ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Cargo"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_Cargo" HighlightCssClass="validatorCalloutHighlight" />
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FuncionesRealizadas"
                                                                    ControlToValidate="TextBox_FuncionesRealizadas" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las FUNCIONES REALIZADAS son requeridas."
                                                                    ValidationGroup="EXPLAB" /><ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FuncionesRealizadas"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_FuncionesRealizadas" HighlightCssClass="validatorCalloutHighlight" />
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FechaIngreso"
                                                                    ControlToValidate="TextBox_FechaIngreso" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE INGRESO es requerida."
                                                                    ValidationGroup="EXPLAB" /><ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FechaIngreso"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_FechaIngreso" HighlightCssClass="validatorCalloutHighlight" />
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Ultimosalario"
                                                                    ControlToValidate="TextBox_Ultimosalario" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ÚLTIMO SALARIO es requerido."
                                                                    ValidationGroup="EXPLAB" /><ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Ultimosalario"
                                                                        TargetControlID="RequiredFieldValidator_TextBox_Ultimosalario" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                                <asp:Panel ID="Panel_BotonesExperienciaLaboral" runat="server">
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
                                    </div>
                                </asp:View>

                                <asp:View ID="View_Familia" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel_ComposicionFamiliar" runat="server">
                                        <ContentTemplate>
                                            <div class="tab_radicacion">
                                                <table cellspacing="5" class="table_form_radicacion">
                                                    <tr>
                                                        <td class="td_2">
                                                            Estado Civil<br />
                                                            <asp:DropDownList ID="DropDownList_ESTADO_CIVIL" runat="server" ValidationGroup="GUARDAR"
                                                                CssClass="textbox_ajustado">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="td_2">
                                                            <table cellspacing="5" class="table_ajustada">
                                                                <tr>
                                                                    <td>
                                                                        Número de Hijos<br />
                                                                        <asp:TextBox ID="TextBox_NUM_HIJOS" runat="server" ValidationGroup="GUARDAR" CssClass="textbox_ajustado"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUM_HIJOS"
                                                                            runat="server" TargetControlID="TextBox_NUM_HIJOS" FilterType="Numbers"></ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        Cabeza de Familia<br />
                                                                        <asp:DropDownList ID="DropDownList_CabezaFamilia" runat="server" CssClass="textbox_ajustado"
                                                                            ValidationGroup="GUARDAR">
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
                                                            DataKeyNames="ID_COMPOSICION" 
                                                            OnRowCommand="GridView_ComposicionFamiliar_RowCommand">
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
                                                                        <asp:TextBox ID="TextBox_FechaNacimientoFamiliar" runat="server" Width="80px" CssClass="TextBox_con_letra_pequena"
                                                                            AutoPostBack="True" OnTextChanged="TextBox_FechaNacimientoFamiliar_TextChanged"></asp:TextBox><br />
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
                                                                            <asp:CheckBox ID="CheckBox_Extranjero" runat="server" Text="En el extranjero" AutoPostBack="True"
                                                                                OnCheckedChanged="CheckBox_Extranjero_CheckedChanged" /></div>
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
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:View>

                                <asp:View ID="View_DatosAdicionales" runat="server">
                                    <div class="tab_radicacion">
                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_3">
                                                    Talla Camisa<br />
                                                    <asp:DropDownList ID="DropDownList_Talla_Camisa" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_3">
                                                    Talla Pantalón<br />
                                                    <asp:DropDownList ID="DropDownList_Talla_Pantalon" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_3">
                                                    Talla Zapatos<br />
                                                    <asp:DropDownList ID="DropDownList_talla_zapatos" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_Talla_Camisa -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_Talla_Camisa"
                                            runat="server" ControlToValidate="DropDownList_Talla_Camisa" Display="None" ErrorMessage="Campo Requerido faltante</br>LA TALLA DE CAMISA es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Talla_Camisa"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_Talla_Camisa" />
                                        <!-- DropDownList_Talla_Pantalon -->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_Talla_Pantalon"
                                            runat="server" ControlToValidate="DropDownList_Talla_Pantalon" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>LA TALLA DEL PANTALÓN es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Talla_Pantalon"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_Talla_Pantalon" />
                                        <!--DropDownList_talla_zapatos-->
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_talla_zapatos"
                                            runat="server" ControlToValidate="DropDownList_talla_zapatos" Display="None"
                                            ErrorMessage="Campo Requerido faltante</br>LA TALLA DE ZAPATOS es requerido."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_talla_zapatos"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_talla_zapatos" />

                                        <div class="div_espaciador"></div>

                                        <table cellspacing="5" class="table_form_radicacion">
                                            <tr>
                                                <td class="td_2">
                                                    Fuente Reclutamiento<br />
                                                    <asp:DropDownList ID="DropDownList_ID_FUENTE" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td_2">
                                                    Cómo se Enteró de Nosotros<br />
                                                    <asp:DropDownList ID="DropDownList_ComoSeEntero" runat="server" CssClass="textbox_ajustado" ValidationGroup="GUARDAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>

                                        <%--DropDownList_ID_FUENTE--%>
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_ID_FUENTE" runat="server"
                                            ControlToValidate="DropDownList_ID_FUENTE" Display="None" ErrorMessage="Campo Requerido faltante</br>La FUENTE es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_FUENTE"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_ID_FUENTE" />

                                        <%--DropDownList_ComoSeEntero--%>
                                        <asp:RequiredFieldValidator ID="RFV_DropDownList_ComoSeEntero" runat="server"
                                            ControlToValidate="DropDownList_ID_FUENTE" Display="None" ErrorMessage="Campo Requerido faltante</br>La FUENTE es requerida."
                                            ValidationGroup="GUARDAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFV_DropDownList_ComoSeEntero" />

                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>




            <asp:Panel ID="Panel_Botones_Pestanas" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:Button ID="Button_Anterior" runat="server" Text="Anterior" 
                                    ValidationGroup="GUARDAR" CssClass="margin_botones" 
                                    onclick="Button_Anterior_Click"/>
                            </td>
                            <td>
                                <asp:Button ID="Button_Siguiente" runat="server" Text="Siguiente" 
                                    ValidationGroup="GUARDAR" CssClass="margin_botones" 
                                    onclick="Button_Siguiente_Click"/>
                            </td>
                            <td>
                                <asp:Button ID="Button_Guardar" runat="server" Text="Guardar" 
                                    ValidationGroup="GUARDAR" CssClass="margin_botones" 
                                    onclick="Button_Guardar_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>


            <asp:Panel ID="Panel_FORM_BOTONES_1" runat="server">
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
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
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
    <div class="div_espaciador">
    </div>
</asp:Content>
