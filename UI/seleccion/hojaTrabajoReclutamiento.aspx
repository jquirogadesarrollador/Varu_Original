<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="hojaTrabajoReclutamiento.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            font-weight: bold;
        }
        .style3
        {
            text-align: right;
            padding-right: 4px;
            height: 21px;
        }
        .style4
        {
            text-align: left;
            padding-left: 4px;
            margin-left: 40px;
            height: 21px;
        }
        .style37
        {
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress_Procesamiento" runat="server" style="display: none">
        <ProgressTemplate>
            <asp:Panel ID="Panel_ContenedorProcesamiento" runat="server">
                <asp:Panel ID="Panel_FondoProcesamiento" runat="server" CssClass="conf_panel_fondo_ventana_emergente">
                </asp:Panel>
                <asp:Panel ID="Panel_VentanaProcesamiento" runat="server" CssClass="conf_panel_ventana_emergente">
                    <div style="border: 2px solid #006600; height: 210px; margin: 2px;">
                        <div style="border: 1px solid #006600; height: 204px; margin: 2px;">
                            <div style="height: 75px;">
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
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_ObjetivosArea" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_objetivos_area">
                        CONTROLAR LA GESTIÓN DE RECLUTAMIENTO
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_botones_internos">
                    <asp:Panel ID="BotonesPrincipales" runat="server">
                        <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Button ID="ButtonCoordinador" runat="server" CssClass="margin_botones" Text="Coordinador"
                                        OnClick="ButtonCoordinador_Click" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="ButtonReclutador" CssClass="margin_botones" runat="server" Text="Reclutador"
                                        OnClick="ButtonReclutador_Click" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button ID="ButtonRecepcion" runat="server" CssClass="margin_botones" Text="Recepción"
                                        OnClick="Button1_Click" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="ButtonPsicologo" CssClass="margin_botones" runat="server" 
                                        Text="Psicologo" onclick="ButtonPsicologo_Click" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="ButtonReportes" CssClass="margin_botones" runat="server" 
                                        Text="Reportes" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
            <asp:HiddenField ID="HiddenField_PAGINA_GRID" runat="server" />
            <asp:HiddenField ID="HiddenField_SEGUIMIENTO_RECEPCION" runat="server" />
            <asp:HiddenField ID="HiddenField_Reclutador_Temporal" runat="server" />
            <asp:HiddenField ID="Seguimiento_Recepcion_Candidato" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
            <asp:HiddenField ID="HiddenFieldSolicitudReclutamiento" runat="server" />
            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <div class="div_contenedor_formulario">
                <asp:Panel ID="Panel_MENSAJES" runat="server">
                    <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                        display: block;" />
                    <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center"
                        Width="656px">
                        <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.">
                        </asp:Label>
                        <div style="text-align: center; margin-top: 15px;">
                            <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                                OnClick="Button_CERRAR_MENSAJE_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="AsignacionReclutador" runat="server">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label8" runat="server" Text="Cod. Requerimiento:" Style="font-size: x-small;
                                        font-weight: 700"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="TextBoxIdRequerimiento" CssClass="margin_botones" runat="server"
                                        Font-Size="8pt" Width="390px">
                                    </asp:TextBox>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="Label6" runat="server" Text="Descripción" Style="font-size: x-small;
                                        font-weight: 700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label1" runat="server" Text="Empresa:" Style="font-size: x-small;
                                        font-weight: 700"></asp:Label>
                                </td>
                                <td class="style9">
                                    <asp:DropDownList ID="DropDownListEmpresa" runat="server" AutoPostBack="true" CssClass="margin_botones"
                                        ValidationGroup="BUSCAR_CLIENTE" Width="390px" OnSelectedIndexChanged="DropDownListEmpresa_SelectedIndexChanged"
                                        Height="16px" Font-Size="8pt">
                                    </asp:DropDownList>
                                </td>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label2" runat="server" Text="Cargo:" Style="font-size: x-small; font-weight: 700"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:DropDownList ID="DropDownListCargo" runat="server" AutoPostBack="true" CssClass="margin_botones"
                                            ValidationGroup="BUSCAR_CLIENTE" Width="390px" Font-Size="8pt">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style13" rowspan="6">
                                        <asp:TextBox ID="TextBoxDescripcion" CssClass="margin_botones" runat="server" Height="111px"
                                            Width="140px" TextMode="MultiLine" Font-Size="8pt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="LabelRegionalReclutamiento" runat="server" Text="Regional:" Style="font-size: x-small;
                                            font-weight: 700; text-align: center;"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="DropDownList1regional" CssClass="margin_botones" runat="server"
                                            Style="margin-left: 0px" Width="390px" Font-Size="8pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label7" runat="server" Style="font-size: x-small; font-weight: 700;
                                            text-align: center;" Text="Ciudad"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="DropDownListCiudad" runat="server" CssClass="margin_botones"
                                            Font-Size="8pt" Style="margin-left: 0px" Width="390px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label3" runat="server" Text="Cantidad:" Style="font-size: x-small;
                                            font-weight: 700; text-align: center;"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="TextBoxCantidadSolicitada" runat="server" Width="48px" CssClass="margin_botones"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                            TargetControlID="TextBoxCantidadSolicitada" FilterType="Numbers" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label4" runat="server" Text="Fecha requerida:" Style="font-size: x-small;
                                            font-weight: 700; text-align: center;"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <asp:TextBox ID="TextBoxFechaRequerida" runat="server" CssClass="margin_botones"
                                            Style="margin-left: 0px" Width="390px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_REQUERIDA" runat="server"
                                            TargetControlID="TextBoxFechaRequerida" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label5" runat="server" Text="Reclutador:" Style="font-size: x-small;
                                            font-weight: 700; text-align: center;"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="DropDownListReclutador" CssClass="margin_botones" runat="server"
                                            Style="margin-left: 0px" Width="390px" Font-Size="8pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="GrabarSolicitudPorProductividad" runat="server" CssClass="margin_botones"
                                            Text="Grabar" Height="31px" Width="61px" OnClick="GrabarSolicitudPorProductividad_Click" />
                                        <asp:Button ID="BtnEditar" runat="server" CssClass="margin_botones"
                                            Text="Modificar" Height="31px" Width="104px" onclick="BtnEditar_Click" 
                                            Visible="False"/>
                                        <asp:Button ID="BtnBorrarRequicicion" runat="server" CssClass="margin_botones"
                                            Text="Borrar" Height="31px" Width="104px" 
                                            Visible="False" onclick="BtnBorrarRequicicion_Click"/>
                                    </td>
                                </tr>
                            </tr>
                        </table>
                        <div class="div_contenedor_formulario">
                            <asp:Panel ID="PanelSeguimientoAsistencia" runat="server">
                                <div style="width: 632px; background-color: #003300; color: #FFFFFF; font-family: 'Times New Roman', Times, serif;
                                    font-weight: bold;">
                                    Asistencia de candidato
                                </div>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="LabelREGISTRO" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="div_titulo_actividad_grilla">
                                            <asp:Label ID="Label33" runat="server" Text="Fecha de citacion" CssClass="style37"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label34" runat="server" Text="Hora de Citacion" CssClass="style37"
                                                Width="120"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" Text="Candidato" CssClass="style37" Width="120"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            <asp:TextBox ID="txtFechaCitada" runat="server" CssClass="margin_botones" Style="margin-left: 0px"
                                                Width="120px"></asp:TextBox>
                                        </td>
                                        <td class="style36">
                                            <asp:TextBox ID="TextBoxHora" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="style36">
                                            <asp:TextBox ID="TextBoxCandidatoSitado" runat="server" Width="230pt"></asp:TextBox>
                                        </td>
                                        <td class="style39">
                                            &nbsp;
                                        </td>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Button ID="ButtonAcisteCita" CssClass="margin_botones" runat="server" Text="Asiste a la cita"
                                                    OnClick="ButtonAcisteCita_Click" />
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </asp:Panel>

                <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <div class="div_cabeza_groupbox">
                                    Sección de Busqueda
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged"
                                                    CssClass="margin_botones">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE"
                                                    CssClass="margin_botones" ontextchanged="TextBox_BUSCAR_TextChanged"></asp:TextBox>
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
                                <!--TextBox_BUSCAR-->
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
                </div>
            </asp:Panel>
                <asp:Panel ID="AspPanelPorProductividad" runat="server">
                                        <div class="div_cabeza_groupbox_gris">
                                            <span class="style32"><strong>Solicitud por Productividad</strong></span>
                                        </div>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label18" runat="server" Text="Empresa:"></asp:Label>
                                                </td>
                                                <td class="style9">
                                                    <asp:DropDownList ID="ProductividadEmpresa" runat="server" AutoPostBack="true" CssClass="margin_botones"
                                                        ValidationGroup="BUSCAR_CLIENTE" Width="390px" Height="16px" Font-Size="8pt">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" Style="font-size: x-small; font-weight: 700"
                                                            Text="Descripción">
                                                    </asp:Label>
                                                </td>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label23" runat="server" Text="Cargo:" Style="font-size: x-small; font-weight: 700"></asp:Label>
                                                    </td>
                                                    <td class="style2">
                                                        <asp:DropDownList ID="ProductividadCargo" runat="server" AutoPostBack="true" CssClass="margin_botones"
                                                            ValidationGroup="BUSCAR_CLIENTE" Width="390px" Font-Size="8pt">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style13" rowspan="6">
                                                        
                                                        <asp:TextBox ID="ProductividadDescripcion" CssClass="margin_botones" runat="server"
                                                            Height="111px" Width="350px" TextMode="MultiLine" Font-Size="8pt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label25" runat="server" Text="Regional:" Style="font-size: x-small;
                                                            font-weight: 700; text-align: center;"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ProductividadRegional" CssClass="margin_botones" runat="server"
                                                            Style="margin-left: 0px" Width="390px" Font-Size="8pt" OnSelectedIndexChanged="ProductividadRegional_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label40" runat="server" Style="font-size: x-small; font-weight: 700;
                                                            text-align: center;" Text="Ciudad"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ProductividadCiudad" runat="server" CssClass="margin_botones"
                                                            Font-Size="8pt" Style="margin-left: 0px" Width="390px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label41" runat="server" Text="Cantidad:" Style="font-size: x-small;
                                                            font-weight: 700; text-align: center;"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="ProductividadCantidad" runat="server" Width="48px" CssClass="margin_botones"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                            TargetControlID="ProductividadCantidad" FilterType="Numbers" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label42" runat="server" Text="Fecha requerida:" Style="font-size: x-small;
                                                            font-weight: 700; text-align: center;"></asp:Label>
                                                    </td>
                                                    <td class="style9">
                                                        <asp:TextBox ID="ProductividadFechaRequerida" runat="server" CssClass="margin_botones"
                                                            Style="margin-left: 0px" Width="390px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="ProductividadFechaRequerida"
                                                            Format="dd/MM/yyyy" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label43" runat="server" Text="Reclutador:" Style="font-size: x-small;
                                                            font-weight: 700; text-align: center;"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ProductividadReclutador" CssClass="margin_botones" runat="server"
                                                            Style="margin-left: 0px" Width="390px" Font-Size="8pt">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Button ID="ButtonGrabarProductividad" runat="server" CssClass="margin_botones"
                                                            Text="Grabar" Height="31px" Width="61px" OnClick="ButtonGrabarProductividad_Click" />
                                                    </td>
                                                </tr>
                                        </table>
                                        <div class=" div_contenedor_formulario">
                                            <div class="table_control_registros">
                                                <div class="div_contenedor_grilla_resultados">
                                                    <asp:GridView ID="GridViewSeguimiento_Productividad" runat="server" 
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="NUMERACION" HeaderText="NUM" />
                                                            <asp:BoundField DataField="FECHA_CREACION" HeaderText="FECHA DE CREACION"></asp:BoundField>
                                                            <asp:BoundField DataField="ID_USUARIO_ASIGNADO" HeaderText="RECLUTADOR">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_OCUPACION" HeaderText="NOM_OCUPACION" Visible="False" />
                                                            <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="EMPRESA">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CANTIDAD_SOLICITADA" HeaderText="CANTIDAD SOLICITADA" />
                                                            <asp:BoundField DataField="CONTACTADOS" HeaderText="CANTIDAD CONTACTADA" />
                                                            <asp:BoundField DataField="RECHAZAN_OFERTA" HeaderText="RECHAZAN OFERTA" />
                                                            <asp:BoundField DataField="ACEPTAN_OFERTA" HeaderText="ACEPTAN OFERTA" />
                                                            <asp:BoundField DataField="ASISTEN_ENTREVISTA" HeaderText="ASISTEN A ENTREVISTA" />
                                                            <asp:ButtonField ImageUrl="~/imagenes/plantilla/edit.gif" CommandName="Asignar" HeaderText=""
                                                                ButtonType="Image" Visible = "false">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                        </div>
                                    </asp:Panel>

                <asp:Panel ID="Hoja_de_Trabajo_Coordinador" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class=" div_contenedor_formulario">
                        <div class="div_cabeza_groupbox">
                            <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Hoja de trabajo"></asp:Label>
                        </div>
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        <span><strong>Solicitud por Requerimiento</strong></span>
                    </div>
                    <div class=" div_contenedor_formulario">
                                            <div class="table_control_registros">
                                                <div class="div_contenedor_grilla_resultados">
                                                    <asp:GridView ID="GridViewSeguimientoReclutamiento" runat="server" 
                                                        AutoGenerateColumns="False" AllowPaging="True"
                                                        OnSelectedIndexChanged="GridView_HOJA_DE_TRABAJO_SelectedIndexChanged"
                                                         OnRowCommand="GridView_HOJA_DE_TRABAJO_RowCommand" TabIndex="15" 
                                                        onpageindexchanging="GridViewSeguimientoReclutamiento_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="NUMERACION" HeaderText="NUM" />
                                                            <asp:BoundField DataField="FECHA_CREACION" HeaderText="FECHA DE CREACION"></asp:BoundField>
                                                            <asp:BoundField DataField="ID_USUARIO_ASIGNADO" HeaderText="RECLUTADOR">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Id_Requerimiento" HeaderText="REQUERIMIENTO">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_OCUPACION" HeaderText="NOM_OCUPACION" Visible="False" />
                                                            <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="EMPRESA">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CANTIDAD_SOLICITADA" HeaderText="CANTIDAD SOLICITADA" />
                                                            <asp:BoundField DataField="CONTACTADOS" HeaderText="CANTIDAD CONTACTADA" />
                                                            <asp:BoundField DataField="RECHAZAN_OFERTA" HeaderText="RECHAZAN OFERTA" />
                                                            <asp:BoundField DataField="ACEPTAN_OFERTA" HeaderText="ACEPTAN OFERTA" />
                                                            <asp:BoundField DataField="ASISTEN_ENTREVISTA" HeaderText="ASISTEN A ENTREVISTA" />
                                                            <asp:ButtonField ImageUrl="~/imagenes/plantilla/edit.gif" CommandName="Asignar" HeaderText=""
                                                                ButtonType="Image" Visible = "false">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                    <div class="div_para_convencion_hoja_trabajo">
                    <div class="div_espaciador" ></div>
                    <div class="div_espaciador" ></div>
                        <table>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label>
                                    &nbsp;Requisiciones creadas hoy.
                                </td>
                                <td class="td_der">
                                    <div class="div_color_verde">
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label>
                                    &nbsp;Requisiciones en plazo de entrega.
                                </td>
                                <td class="td_contenedor_colores_hoja_trabajo">
                                    <div class="div_color_amarillo">
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label>
                                    Requisiciones con plazo de entrega vencido.
                                </td>
                                <td class="td_contenedor_colores_hoja_trabajo">
                                    <div class="div_color_rojo">
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" CssClass="grid_seleccionar_registros"
                            AutoGenerateColumns="False" 
                            DataKeyNames="ID_REQUERIMIENTO,COD_EMPRESA,COD_OCUPACION" OnSelectedIndexChanged="GridView_HOJA_DE_TRABAJO_SelectedIndexChanged"
                            OnRowCommand="GridView_HOJA_DE_TRABAJO_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="NUMERACION" HeaderText="Num"></asp:BoundField>
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Num. Req.">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COD_EMPRESA" HeaderText="COD_EMPRESA" Visible="False" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_REQ" HeaderText="Req. Tipo" />
                                <asp:BoundField DataField="FECHA_R" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Apertura">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_REQUERIDA" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Vence">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo Req." />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="Req." />
                                <asp:BoundField DataField="ENVIADOS" HeaderText="Env." />
                                <asp:BoundField DataField="CONTRATAR" HeaderText="Por Cont." />
                                <asp:BoundField DataField="DESCARTADOS" HeaderText="Desc." />
                                <asp:BoundField DataField="FALTAN" HeaderText="Faltan" />
                                <asp:BoundField DataField="Id_usuario_Asignado" 
                                    HeaderText="Asignado al Reclutador" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:ButtonField ImageUrl="~/imagenes/plantilla/Asignar.png"  CommandName="Asignar"
                                    HeaderText="Asignar reclutador" ButtonType="Image">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:ButtonField ImageUrl="../imagenes/plantilla/edit.gif" CommandName="Modificar"
                                    HeaderText="Modificar" ButtonType="Image">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                </asp:Panel>
                <div class=" div_contenedor_formulario">
                    <asp:Panel ID="Hoja_De_Trabajo_Reclutador" runat="server">
                        <div class="div_cabeza_groupbox_gris">
                            Hoja de trabajo reclutador
                        </div>
                        <table>
                            <tr>
                                <td class="td_izq">
                                    <div class="div_color_verde">
                                    </div>
                                </td>
                                <td class="td_der">
                                     <asp:Label ID="Label32" runat="server" Text="0"></asp:Label>
                                    Requerimiento cumplido.
                                </td>
                                <td class="td_contenedor_colores_hoja_trabajo">
                                    <div class="div_color_rojo">
                                    </div>
                                </td>
                                <td >
                                    <asp:Label ID="Label39" runat="server" Text="0"></asp:Label>
                                    Requisiciones con plazo de entrega vencido.
                                </td>
                            </tr>
                        </table>
                        <div class="div_contenedor_formulario">
                            <div class=" div_contenedor_formulario">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridViewHojaDeTrabajoReclutador" runat="server" CssClass="grid_seleccionar_registros"
                                        AutoGenerateColumns="False" DataKeyNames="id_solicitud" AllowPaging="True"
                                        Width="885px" OnRowCommand="GridViewHojaDeTrabajoReclutador_RowCommand1" 
                                        onpageindexchanging="GridViewHojaDeTrabajoReclutador_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="NUMERACION" HeaderText="NUM" />
                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Num. Req.">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="id_solicitud" HeaderText="id_solicitud" />
                                            <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="CLIENTE" />
                                            <asp:BoundField DataField="NOM_OCUPACION" HeaderText="CARGO" />
                                            <asp:BoundField DataField="cantidad" HeaderText="CANTIDAD" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="REGIONAL" />
                                            <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="CIUDAD" />
                                            <asp:BoundField DataField="Fecha_Requerida" HeaderText="FECHA REQUERIDA" />
                                            <asp:BoundField DataField="CITADOS" HeaderText="CITADOS" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASISTEN_CITA" HeaderText="ASISTEN A ENTREVISTA" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:ButtonField ImageUrl="~/imagenes/plantilla/user_add.png" CommandName="Agregar_Contacto"
                                                HeaderText="" ButtonType="Image">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class=" div_contenedor_formulario">
                    <asp:Panel ID="AspPanleResumenRequerimiento" runat="server" Width="888px">
                        <div>
                            <div class="div_cabeza_groupbox_gris">
                                Resumen Requerimiento
                            </div>
                        </div>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label13" runat="server" Text="Solicitud:" CssClass="style2"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="TextBoxResumenSolicitud" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label14" runat="server" Text="Requerimiento:" CssClass="style2"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="TextBoxResumenRequerimiento" runat="server" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label15" runat="server" Text="Cantidad:" CssClass="style2"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:Label ID="TextBoxResumenCantidad" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="Label17" runat="server" Text="Cargo:" CssClass="style2"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:Label ID="TextBoxResumenCargo" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label16" runat="server" Text="Cliente:" CssClass="style2"></asp:Label>
                                </td>
                                <td colspan="1" class="td_der">
                                    <asp:Label ID="TextBoxRequerimientoCliente" runat="server" Text="">
                                    </asp:Label>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label19" runat="server" Text="Regional:" CssClass="style2"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="LabelRegional" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label22" runat="server" CssClass="style2" Text="Descripción:"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    &nbsp;</td>
                                <td class="td_izq">
                                    <asp:Label ID="Label21" runat="server" Text="ciudad:" class="td_izq" 
                                        CssClass="style2"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="LabelCiudad" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_DescripcionResumenRequerimiento" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="table_control_registros" style="border-style: groove">
                            <tr>
                                <td colspan="5" class="label_nombre_modulo">
                                    <asp:Label ID="Label9" runat="server" Text="Agregar contacto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label26" runat="server" Text="Documento del candidato:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Text="Apellido"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label28" runat="server" Text="Nombre:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Text="Telefono:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="Fuente de reclutamiento:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxDocumento" runat="server" OnTextChanged="TextBoxDocumento_TextChanged"
                                        AutoPostBack="True"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="TextBoxDocumento" FilterType="Numbers" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxApellido" runat="server" OnTextChanged="TextBoxApellido_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        TargetControlID="TextBoxApellido" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                        ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxNombre" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                        TargetControlID="TextBoxNombre" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                        ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxTelefono" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTelefono" runat="server"
                                        TargetControlID="TextBoxTelefono" FilterType="Numbers" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListFuentesReclutamiento" Width="110pt" runat="server">
                                        <asp:ListItem>INTERNET</asp:ListItem>
                                        <asp:ListItem>FISICA</asp:ListItem>
                                        <asp:ListItem>VOLANTEO</asp:ListItem>
                                        <asp:ListItem>AVISO PERIODICO</asp:ListItem>
                                        <asp:ListItem>FERIA</asp:ListItem>
                                        <asp:ListItem>INTERCAMBIO</asp:ListItem>
                                        <asp:ListItem>-- Seleccione --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="columna_grid_centrada">
                                    <asp:ImageButton ID="ImageButtonAgregarALista" runat="server" ImageUrl="~/imagenes/plantilla/save.gif"
                                        OnClick="ImageButtonAgregarALista_Click" style="height: 14px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelListaDeCandidatos" runat="server" Width="888px">
                        <div class="div_cabeza_groupbox_gris">
                            Lista de candidatos
                        </div>
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridViewListarContactosPorSolicitud" runat="server" CssClass="grid_seleccionar_registros"
                                AutoGenerateColumns="False" DataKeyNames="Id_Solicitud_requerimiento,Id_Registro"
                                AllowPaging="True" PageSize="20" Width="884px" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="NUMERACION" HeaderText="NUM" >
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_Registro" HeaderText="Id_Registro" Visible="false">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Id_Solicitud_requerimiento" HeaderText="Solicitud">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                                    <asp:BoundField DataField="Candidato" HeaderText="Candidato" />
                                    <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                    <asp:BoundField DataField="Acepta_Oferta" HeaderText="Acepta Oferta">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fecha_De_Cita" HeaderText="Agendado" >
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Psicologo" HeaderText="Psicologo" >
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:BoundField>
                                    <asp:ButtonField ImageUrl="~/imagenes/plantilla/Agenda.png" CommandName="Agendar"
                                        HeaderText="" ButtonType="Image">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="panelAgendarCandidato" runat="server">
                        <div class="div_cabeza_groupbox_gris">
                            Agenda candidato
                        </div>
                        <table class="table_control_registros" style="border-style: ridge">
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdRegistro" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblRegistrodeLista" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label37" runat="server" Text="Le interesa la oferta:"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem>SI</asp:ListItem>
                                        <asp:ListItem>NO</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelAgendaCandidatoSi" runat="server">
                            <table class="table_control_registros" style="border-style: ridge">
                                <tr>
                                    <td class="div_titulo_actividad_grilla">
                                        <asp:Label ID="Label31" runat="server" Text="Fecha de citacion"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Text="Psicologo" CssClass="style37"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBoxFechaAgenda" runat="server" Enabled="False"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxFechaAgenda"
                                            Format="dd/MM/yyyy" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPsicologo" runat="server" Width="180" CssClass="margin_botones"
                                            Enabled="False" OnSelectedIndexChanged="DropDownListPsicologo_SelectedIndexChanged1"
                                            AutoPostBack="True" OnTextChanged="DropDownListPsicologo_TextChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="AgendarContacto" runat="server" CssClass="margin_botones" Text="Agendar Contacto"
                                            OnClick="AgendarContacto_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridViewAgendaPsicologoPrincipal" runat="server" DataKeyNames="HORA"
                                        CssClass="grid_seleccionar_registros" BorderWidth="1px" AutoGenerateColumns="False"
                                        Width="850px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" CellPadding="3"
                                        ForeColor="Black" GridLines="Vertical" 
                                        onrowcommand="GridViewAgendaPsicologoPrincipal_RowCommand">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="HORA" HeaderText="HORA" Visible="false" />
                                            <asp:BoundField DataField="MINUTOS" HeaderText="HORA" />
                                            <asp:ButtonField ImageUrl="~/imagenes/plantilla/user_add.png" CommandName="AgendarContacto"
                                                HeaderText="Agendar" ButtonType="Image">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:ButtonField>
                                            <asp:TemplateField HeaderText="LISTA DE CONTACTOS">
                                                <ItemTemplate>
                                                    <asp:GridView ID="GridViewAgendaPsicologoListaContactos" runat="server" AutoGenerateColumns="False"
                                                        ShowHeader="False" CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="grid_seleccionar_registros"
                                                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CONTACTO" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCCCC" />
                                                        <headerStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <headerStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                </div>
                </asp:Panel>
                <div class="div_contenedor_formulario">
                    <asp:Panel ID="HojaDeTrabajoRecepcion" runat="server">
                        <div class="div_cabeza_groupbox_gris">
                            Hoja de trabajo Citaciones
                        </div>
                        <table class="table_control_registros" style="border-style: groove">
                            <tr>
                                <td colspan="3" class="label_nombre_modulo">
                                    <asp:Label ID="Label35" runat="server" Text="Busqueda de candidatos."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" Width="290" runat="server">
                                        <asp:ListItem Value="1">Documento</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxFiltro" Width="200" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="ButtonBuscarCandidato" runat="server" Text="Buscar" OnClick="ButtonBuscarCandidato_Click" />
                                </td>
                            </tr>
                        </table>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label10" runat="server" Text="SOLICITUD:" Visible ="false" 
                                        style="font-weight: 700"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="LBL_ID_SOLICITUD" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label20" runat="server" Text="CANDIDATO:" Visible ="false"  
                                        style="font-weight: 700"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="LBL_CANDIDATO" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label11" runat="server" Text="TIPO DE DOCUMENTO:" Visible ="false" 
                                            style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="LBL_TIPO_DOCUMENTO" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="td_izq">
                                        <asp:Label ID="Label12" runat="server" Text="DOCUMENTO:" Visible ="false" 
                                            style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="lbl_DOCUMENTO" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                        </table>
                        <asp:GridView ID="GridViewSeguimientoRecepcion" runat="server" CssClass="grid_seleccionar_registros"
                            AutoGenerateColumns="False" DataKeyNames="REGISTRO,HORA,Id_Registro_Empleado"
                            Width="884px" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="NUMERACION" HeaderText="NUM" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Id_Registro_Empleado" HeaderText="" Visible="false" />
                                <asp:BoundField DataField="Id_Solicitud_requerimiento" HeaderText="CODIGO" Visible="false" />
                                <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" />
                                <asp:BoundField DataField="Psicologo" HeaderText="PSICOLOGO" />
                                <asp:BoundField DataField="FECHA" HeaderText="FECHA">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPRESA" HeaderText="EMPRESA" />
                                <asp:BoundField DataField="CARGO" HeaderText="CARGO" />
                                <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" />
                                <asp:BoundField DataField="CANDIDATO" HeaderText="CANDIDATO" />
                                <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" />
                                <asp:BoundField DataField="Acepta" HeaderText="ACEPTA OFERTA LABORAL" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ASISTE" HeaderText="ASISTE A LA CITA" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:ButtonField ImageUrl="~/imagenes/plantilla/Agenda.png" CommandName="SEGUIMIENTO"
                                    HeaderText="" ButtonType="Image">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="panelPsicologo" runat="server">
                        <div class="div_cabeza_groupbox_gris">
                            Agenda Psicologo
                        </div>
                        <asp:Panel ID="Panel2" runat="server">
                            <table class="table_control_registros" style="border-style: ridge">
                                <tr>
                                    <td class="div_titulo_actividad_grilla">
                                        <asp:Label ID="Label47" runat="server" Text="Fecha de citacion" 
                                            style="font-weight: 700"></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label48" runat="server" Text="Psicologo" CssClass="style37"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBoxFechaAgendaPsicologo" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxFechaAgendaPsicologo"
                                            Format="dd/MM/yyyy" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPsicologoForm" runat="server" Width="180" CssClass="margin_botones"
                                            OnSelectedIndexChanged="DropDownListPsicologo_SelectedIndexChanged1"
                                            AutoPostBack="True" OnTextChanged="DropDownListPsicologo_TextChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="ButtonPsicologofdsf" runat="server" CssClass="margin_botones" 
                                            Text="Ver agenda" onclick="ButtonPsicologofdsf_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridViewAgendaPsicologo" runat="server" DataKeyNames="HORA"
                                        CssClass="grid_seleccionar_registros" BorderWidth="1px" AutoGenerateColumns="False"
                                        Width="850px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" CellPadding="3"
                                        ForeColor="Black" GridLines="Vertical" 
                                        onrowcommand="GridViewAgendaPsicologoPrincipal_RowCommand">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="HORA" HeaderText="HORA" Visible="false" />
                                            <asp:BoundField DataField="MINUTOS" HeaderText="HORA" />
                                            <asp:TemplateField HeaderText="LISTA DE CONTACTOS">
                                                <ItemTemplate>
                                                    <asp:GridView ID="GridViewAgendaPsicologoListaContactosPsicologo" runat="server" AutoGenerateColumns="False"
                                                        ShowHeader="False" CellPadding="3" ForeColor="Black" GridLines="Vertical" CssClass="grid_seleccionar_registros"
                                                        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CONTACTO" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCCCC" />
                                                        <headerStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <headerStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                </div>
                </asp:Panel>
                </div>
            </div>
            </div>
            <div class="div_contenedor_formulario">
                <table class="table_control_registros">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
