<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="baseDatos.aspx.cs" Inherits="_BaseDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
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

            <asp:Panel ID="Panel_SeleccionEstadoAspirante" runat="server">  
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Tipo de Reporte
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="2" cellspacing="4" style="font-size:85%">
                            <tr>
                                <td style="border:solid 1px #999999; background-color:#dddddd; text-align:left;">
                                    Aspirantes
                                </td>
                                <td style="border:solid 1px #aaaaaa; text-align:justify;">
                                    Personal con solicitud de ingreso registrada y aún no se les ha 
                                    realizado proceso de selección. (entrevista, confirmación de referencias)
                                </td>
                            </tr>
                            <tr>
                                <td style="border:solid 1px #999999; background-color:#dddddd; text-align:left;">
                                    Disponibles
                                </td>
                                <td style="border:solid 1px #aaaaaa; text-align:justify;">
                                    Personal con solicitud de ingreso registrada, ya se les realizó proceso de selección
                                    (entrevista, confirmación de referencias), pero no han sido contratados en ningún momento.
                                </td>
                            </tr>
                            <tr>
                                <td style="border:solid 1px #999999; background-color:#dddddd; text-align:left;">
                                    No Laboran
                                </td>
                                <td style="border:solid 1px #aaaaaa; text-align:justify;">
                                    Personal con solicitud de ingreso registrada, ya se les realizó proceso de selección
                                    (entrevista, confirmación de referencias), han tenido contratos que ya terminaron y en este momento se encuentran
                                    disponibles para ser contratados nuevamente.
                                </td>
                            </tr>
                        </table>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList_TipoReporte" runat="server" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="ASPIRANTES"><span title="Personal con solicitud de ingreso registrada y aún no se les ha realizado proceso de selección. (entrevista, confirmación de referencias)">Aspirantes</span></asp:ListItem>
                                        <asp:ListItem Value="DISPONIBLES"><span title="Personal con solicitud de ingreso registrada, ya se les realizó proceso de selección (entrevista, confirmación de referencias), pero no han sido contratados en ningún momento.">Disponibles</span></asp:ListItem>
                                        <asp:ListItem Value="NO LABORAN"><span title="Personal con solicitud de ingreso registrada, ya se les realizó proceso de selección (entrevista, confirmación de referencias), han tenido contratos que ya terminaron y en este momento se encuentran disponibles para ser contratados nuevamente.">No Laboran</span></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <!-- RadioButtonList_TipoReporte -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RadioButtonList_TipoReporte" runat="server" ControlToValidate="RadioButtonList_TipoReporte"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El TIPO DE REPORTE es requerido."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_RadioButtonList_TipoReporte" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_RadioButtonList_TipoReporte" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_SeleccionChecks" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Selección de Filtros
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="5" cellspacing="5" class="table_control_registros" style="text-align:left;">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_CargoAspira" runat="server" 
                                        Text="Cargo al que Aspira" 
                                        oncheckedchanged="CheckBox_CargoAspira_CheckedChanged" AutoPostBack="true"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_AreaInteres" runat="server" Text="Área de Interés" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_AreaInteres_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_Experiencia" runat="server" Text="Experiencia" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_Experiencia_CheckedChanged"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_AspiracionSalarial" runat="server" 
                                        Text="Aspiración Salarial" AutoPostBack="true" 
                                        oncheckedchanged="CheckBox_AspiracionSalarial_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_NivelEscolaridad" runat="server" 
                                        Text="Nivel de Escolaridad" AutoPostBack="true" 
                                        oncheckedchanged="CheckBox_NivelEscolaridad_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_Profesión" runat="server" Text="Profesión" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_Profesión_CheckedChanged"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_Ciudad" runat="server" Text="Ciudad donde Vive" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_Ciudad_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_Barrio" runat="server" Text="Barrio" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_Barrio_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_FechaActualizacion" runat="server" 
                                        Text="Fecha de Ultima Actualización" AutoPostBack="true" 
                                        oncheckedchanged="CheckBox_FechaActualizacion_CheckedChanged"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox_NombresApellidos" runat="server" 
                                        Text="Nombres y apellidos" AutoPostBack="true" 
                                        oncheckedchanged="CheckBox_NombresApellidos_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_Edad" runat="server" Text="Edad" AutoPostBack="true" 
                                        oncheckedchanged="CheckBox_Edad_CheckedChanged"/>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox_PalabraClave" runat="server" Text="Palabra Clave" 
                                        AutoPostBack="true" oncheckedchanged="CheckBox_PalabraClave_CheckedChanged"/>
                                </td>
                            </tr>
                        </table>
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
                                        <td colspan="0">
                                            <asp:Button ID="Button_Buscar" runat="server" Text="Buscar" 
                                                CssClass="margin_botones" ValidationGroup="BUSCAR" 
                                                onclick="Button_Buscar_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_ExportarExcel" runat="server" Text="Exportar Excel" 
                                                CssClass="margin_botones" ValidationGroup="EXPORTAR" 
                                                onclick="Button_ExportarExcel_Click" />
                                        </td>
                                        <td colspan="0">
                                            <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" class="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_CargoAspira" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Cargo al que Aspira
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    <div class="div_cabeza_groupbox_gris">
                                        (Primer Paso) -Buscar Cargo-
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                        Width="220px" MaxLength="15"></asp:TextBox>
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
                                </td>
                                <td valign="top">
                                    <div class="div_cabeza_groupbox_gris">
                                        (Segundo Paso) -Seleccionar Cargo-
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Cargo:
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_CargoAspira" runat="server" Width="400px" ValidationGroup="BUSCAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_CargoAspira -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CargoAspira" runat="server" ControlToValidate="DropDownList_CargoAspira"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="BUSCAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CargoAspira" runat="Server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CargoAspira" />
                                    </div>
                                </td>
                            </tr>
                        </table> 
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_AreaInteres" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Área de Interés
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Área de interés:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_AreaInteres" runat="server" Width="700px" 
                                        ValidationGroup="BUSCAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_AreaInteres -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_AreaInteres" runat="server" ControlToValidate="DropDownList_AreaInteres"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El ÁREA DE INTERES es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_AreaInteres" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_AreaInteres" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Experiencia" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Experiencia
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Experiencia desde:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_Experiencia" runat="server" Width="670px" 
                                        ValidationGroup="BUSCAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_Experiencia -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Experiencia" runat="server" ControlToValidate="DropDownList_Experiencia"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El EXPERIENCIA es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Experiencia" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Experiencia" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_AspiracionSalarial" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Aspiración Salarial
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Aspiración Salarial (Desde):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_AspiracionSalarialDesde" runat="server" Width="220px" ValidationGroup="BUSCAR" CssClass="money"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_AspiracionSalarialDesde"
                                        runat="server" TargetControlID="TextBox_AspiracionSalarialDesde" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td valign="top">
                                    Aspiración Salarial (Hasta):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_AspiracionSalarialHasta" runat="server" Width="220px" 
                                        ValidationGroup="BUSCAR" CssClass="money"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_AspiracionSalarialHasta"
                                        runat="server" TargetControlID="TextBox_AspiracionSalarialHasta" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_AspiracionSalarialDesde -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_AspiracionSalarialDesde" runat="server" ControlToValidate="TextBox_AspiracionSalarialDesde"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La ASPIRACIÓN SALARIAL (DESDE) es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_AspiracionSalarialDesde" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_AspiracionSalarialDesde" />
                            <!-- TextBox_AspiracionSalarialHasta -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_AspiracionSalarialHasta" runat="server" ControlToValidate="TextBox_AspiracionSalarialHasta"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La ASPIRACION SALARIAL HASTA() es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_AspiracionSalarialHasta" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_AspiracionSalarialHasta" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_NivelEscolaridad" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Nivel de Escolaridad
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Nivel de Escolaridad Desde:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_NivelEsolaridad" runat="server" Width="590px" 
                                        ValidationGroup="BUSCAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_NivelEsolaridad -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_NivelEsolaridad" runat="server" ControlToValidate="DropDownList_NivelEsolaridad"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El NIVEL DE ESCOLARIDAD es requerido."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_NivelEsolaridad" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_NivelEsolaridad" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Profesion" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Profesión
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Profesión:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_Profesion" runat="server" Width="680px" 
                                        ValidationGroup="BUSCAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_Profesion -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Profesion" runat="server" ControlToValidate="DropDownList_Profesion"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La PROFESIÓN es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Profesion" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Profesion" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Ciudad" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Ciudad donde Vive
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    <div class="div_cabeza_groupbox_gris">
                                        (Primer Paso) -Selección Departamento-
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Departamento:
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_Departamento" runat="server" Width="280px" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownList_Departamento_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td valign="top">
                                    <div class="div_cabeza_groupbox_gris">
                                        (Segundo Paso) -Seleccionar Ciudad-
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Ciudad:
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_Ciudad" runat="server" Width="280px" 
                                                        ValidationGroup="BUSCAR">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_Ciudad -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_Ciudad" runat="server" ControlToValidate="DropDownList_Ciudad"
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>La CIUDAD es requerida."
                                            ValidationGroup="BUSCAR" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_Ciudad" runat="Server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_Ciudad" />
                                    </div>
                                </td>
                            </tr>
                        </table> 
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Barrio" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Barrio
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Barrio:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_Barrio" runat="server" ValidationGroup="BUSCAR" 
                                        Width="700px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_Barrio -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Barrio" runat="server" ControlToValidate="TextBox_Barrio"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El BARRIO es requerido."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Barrio" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_Barrio" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FechaActualziacion" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Fecha de Actualización
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Fecha Actualización (Desde):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FechaActualizacionDesde" runat="server" Width="220px" ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaActualizacionDesde" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="TextBox_FechaActualizacionDesde" />
                                </td>
                                <td valign="top">
                                    Fecha Actualización (Hasta):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FechaActualizacionHasta" runat="server" Width="220px" 
                                        ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaActualizacionHasta" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="TextBox_FechaActualizacionHasta" />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_FechaActualizacionDesde -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FechaActualizacionDesde" runat="server" ControlToValidate="TextBox_FechaActualizacionDesde"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La FECHA DE ACTUALIZACION (DESDE) es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FechaActualizacionDesde" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FechaActualizacionDesde" />
                            <!-- TextBox_FechaActualizacionHasta -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FechaActualizacionHasta" runat="server" ControlToValidate="TextBox_FechaActualizacionHasta"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La fecha actualizacion (HASTA) es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FechaActualizacionHasta" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FechaActualizacionHasta" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_NombresApellidos" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Nombres y Apellidos
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Nombres:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_Nombres" runat="server" Width="220px" ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_" runat="server"
                                        TargetControlID="TextBox_Nombres" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                        ValidChars=".,[]{}-_ ñÑáéíóúÁÉÍÓÚ()" />
                                </td>
                                <td valign="top">
                                    Apellidos:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_Apellidos" runat="server" Width="220px" 
                                        ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Apellidos"
                                        runat="server" TargetControlID="TextBox_Apellidos" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                        ValidChars=".,[]{}-_ ñÑáéíóúÁÉÍÓÚ()" />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_Nombres -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Nombres" runat="server" ControlToValidate="TextBox_Nombres"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El NOMBRE es requerido."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Nombres" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_Nombres" />
                            <!-- TextBox_Apellidos -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Apellidos" runat="server" ControlToValidate="TextBox_Apellidos"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>El APELLIDO es requerido."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Apellidos" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_Apellidos" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Edad" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Edad
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Edad (Desde):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_EdadDesde" runat="server" Width="220px" ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EdadDesde"
                                        runat="server" TargetControlID="TextBox_EdadDesde" FilterType="Numbers" />
                                </td>
                                <td valign="top">
                                    Edad (Hasta):
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_EdadHasta" runat="server" Width="220px" 
                                        ValidationGroup="BUSCAR"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EdadHasta"
                                        runat="server" TargetControlID="TextBox_EdadHasta" FilterType="Numbers" />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_EdadDesde -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EdadDesde" runat="server" ControlToValidate="TextBox_EdadDesde"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La EDAD (DESDE) es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EdadDesde" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EdadDesde" />
                            <!-- TextBox_EdadHasta -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EdadHasta" runat="server" ControlToValidate="TextBox_EdadHasta"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La EDAD (HASTA) es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EdadHasta" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EdadHasta" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_PalabraClave" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Palabra Clave
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td valign="top">
                                    Palabra Clave:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_PalabraClave" runat="server" ValidationGroup="BUSCAR" 
                                        Width="700px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_PalabraClave -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_PalabraClave" runat="server" ControlToValidate="TextBox_PalabraClave"
                            Display="None" ErrorMessage="Campo Requerido faltante</br>La PALABRA CLAVE es requerida."
                            ValidationGroup="BUSCAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_PalabraClave" runat="Server"
                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_PalabraClave" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_GrillaReporte" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Resultado de la Busqueda
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_Reporte" runat="server" Width="880px" AutoGenerateColumns="False"
                                                AllowPaging="True" OnPageIndexChanging="GridView_Reporte_PageIndexChanging"
                                                OnRowCommand="GridView_Reporte_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="H.V.">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink_HV" runat="server">H.V.</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Doc. Identificación">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRES_ASPIRANTE" HeaderText="Nombres">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TELEFONO_ASPIRANTE" HeaderText="Telefonos" 
                                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NIVEL_ESCOLARIDAD" HeaderText="Nivel Escolaridad">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PROFESION" HeaderText="Profesión">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AREA_INTERES_LABORAL" HeaderText="Área Interés">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EXPERIENCIA" HeaderText="Experiencia">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EDAD" HeaderText="Edad">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CIUDAD_ASPIRANTE" HeaderText="Ciudad">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BARRIO" HeaderText="Barrio">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ASPIRACION_SALARIAL" HeaderText="Asp. Salarial">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FCH_MOD" HeaderText="Fch. Actualización" 
                                                        DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
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
                                        <td colspan="0">
                                            <asp:Button ID="Button_Buscar_1" runat="server" Text="Buscar" CssClass="margin_botones" ValidationGroup="BUSCAR" onclick="Button_Buscar_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_ExportarExcel_1" runat="server" Text="Exportar Excel" CssClass="margin_botones" ValidationGroup="EXPORTAR" onclick="Button_ExportarExcel_Click" />
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
            <asp:PostBackTrigger ControlID="Button_ExportarExcel_1" />
            <asp:PostBackTrigger ControlID="Button_ExportarExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


