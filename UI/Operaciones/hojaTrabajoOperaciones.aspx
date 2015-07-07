<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="hojaTrabajoOperaciones.aspx.cs" Inherits="_BienestarSocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="Panel_ObjetivosArea" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_objetivos_area">
                Garantizar la Prestación del Servicio Post Venta a Nuestros Clientes a Nivel Nacional a Través del
                Desarrollo y Ejecución de Actividades de Cumplimiento de Requisitos con el Respectivo Acompañamiento Profesional y Oportunidad de Respuesta.
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
        text-align: center;">
        <div class="div_botones_internos">
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: center;">
                        <asp:Table ID="Table_MENU" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: center;">
                        <asp:Table ID="Table_MENU_1" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_GRILLA_CALENDARIO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Calendario de Actividades
            </div>
            <div class="div_contenido_groupbox">
                
                <asp:Panel ID="Panel_CabezaConvencionesIconos" runat="server" CssClass="div_cabeza_groupbox_gris">
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 87%;">
                                Convenciones -Iconos-
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                    <tr>
                                        <td style="font-size: 80%">
                                            <asp:Label ID="Label_ConvencionesIconos" runat="server">(Mostrar detalles...)</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image_ConvencionesIconos" runat="server" CssClass="img_cabecera_hoja" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="Panel_ContenidoConvencionesIconos" runat="server" CssClass="div_contenido_groupbox_gris">
                    <table class="table_control_registros" cellpadding="2" cellspacing="4" style="font-size: 85%;
                        width: 95%;">
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconAprobadoEnEspera" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconAprobadoEnEspera.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                Actividad creada correctamente, a la espera de ser ejecutada.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconCanceladoCliente" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconCanceladoCliente.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                Actividad cancelada por cliente.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconCanceladoSertempo" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconCanceladoSertempo.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                Actividad cancelada por Empleador.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconCorrecto" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconCorrecto.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                Actividad ejecutada correctamente y el encargado ya registró en el sistema los resultados.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconNoEjecutada" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconNoEjecutada.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                (1)La actividad debe ser ejecutada hoy ó (2) la actividad ya se ejecutó pero el 
                                encargado no ha reportado los resultados.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconReprogCancCliente" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconReprogCancCliente.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                La actividad fue cancelada por el cliente, y fue reprogramada una ó más veces.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconReprogCancSertempo" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconReprogCancSertempo.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                La actividad fue cancelada por el empleador, y fue reprogramada una ó más veces.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconReprogCorrecto" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconReprogCorrecto.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                La actividad fue creada correctamente, el encargado registró resultados, y esta actividad fue reprogramada una ó más veces.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconReprogEspera" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconReprogEspera.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                La actividad se encuentra a la espera de ser ejecutada, y esta actividad fue reprogramada una ó más veces.
                            </td>
                        </tr>
                        <tr>
                            <td style="border: solid 1px #999999; background-color: #dddddd; text-align: center;
                                width: 40px;">
                                <asp:Image ID="Image_IconReprogNoEjecutada" runat="server" ImageUrl="~/imagenes/IconosCalendario/IconReprogNoEjecutada.png"
                                    CssClass="image_icon_convenciones" />
                            </td>
                            <td style="border: solid 1px #aaaaaa; text-align: justify;">
                                A la actividad no se le han registrado resultados, y además fue reprogramada una ó más veces.
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ConvencionesIconos"
                    runat="Server" TargetControlID="Panel_ContenidoConvencionesIconos" ExpandControlID="Panel_CabezaConvencionesIconos"
                    CollapseControlID="Panel_CabezaConvencionesIconos" Collapsed="True" TextLabelID="Label_ConvencionesIconos"
                    ImageControlID="Image_ConvencionesIconos" ExpandedText="(Ocultar detalles...)"
                    CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                    CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                </ajaxToolkit:CollapsiblePanelExtender>








                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Button ID="Button_Anterior" runat="server" Text="<<<---" ValidationGroup="CALENDARIO"
                                OnClick="Button_Anterior_Click" />
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_Mes" runat="server" ValidationGroup="CALENDARIO"
                                AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="DropDownList_Mes_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_Anno" runat="server" Width="80px" ValidationGroup="CALENDARIO"
                                AutoPostBack="True" CausesValidation="True" OnTextChanged="TextBox_Anno_TextChanged"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Anno" runat="server"
                                TargetControlID="TextBox_Anno" FilterType="Numbers" />
                        </td>
                        <td>
                            <asp:Button ID="Button_Siguiente" runat="server" Text="--->>>" OnClick="Button_Siguiente_Click"
                                ValidationGroup="CALENDARIO" />
                        </td>
                    </tr>
                </table>
                <!-- TextBox_Anno -->
                <asp:RangeValidator ID="RangeValidator_TextBox_Anno" runat="server" ControlToValidate="TextBox_Anno"
                    Display="None" MaximumValue="3000" MinimumValue="2012" Type="Integer" ValidationGroup="CALENDARIO"
                    ErrorMessage="<b>Campo Requerido erroneo</b><br />El AÑO debe ser mayor a 2012."></asp:RangeValidator>
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Anno"
                    TargetControlID="RangeValidator_TextBox_Anno" HighlightCssClass="validatorCalloutHighlight" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Anno"
                    ControlToValidate="TextBox_Anno" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El AÑO es requerido."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Anno_1"
                    TargetControlID="RequiredFieldValidator_TextBox_Anno" HighlightCssClass="validatorCalloutHighlight" />
                <%--DropDownList_Mes--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Mes"
                    ControlToValidate="DropDownList_Mes" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MES es requerido."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Mes"
                    TargetControlID="RequiredFieldValidator_DropDownList_Mes" HighlightCssClass="validatorCalloutHighlight" />
                
                <div class="div_espaciador">
                </div>


                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Regional:
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_Regional" runat="server" Width="200px" 
                                ValidationGroup="CALENDARIO" AutoPostBack="True" CausesValidation="True" 
                                onselectedindexchanged="DropDownList_Regional_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="td_izq">
                            Ciudad:
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_Ciudad" runat="server" Width="200px" 
                                ValidationGroup="CALENDARIO" AutoPostBack="True" CausesValidation="True" 
                                onselectedindexchanged="DropDownList_Ciudad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="td_izq">
                            Empresa:
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_Empresa" runat="server" Width="220px" 
                                ValidationGroup="CALENDARIO" AutoPostBack="True" CausesValidation="True" 
                                onselectedindexchanged="DropDownList_Empresa_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <%--DropDownList_Regional--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Regional"
                    ControlToValidate="DropDownList_Regional" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />LA REGIONAL es requerida."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Regional"
                    TargetControlID="RequiredFieldValidator_DropDownList_Regional" HighlightCssClass="validatorCalloutHighlight" />
                <%--DropDownList_Ciudad--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Ciudad"
                    ControlToValidate="DropDownList_Ciudad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />LA CIUDAD es requerida."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Ciudad"
                    TargetControlID="RequiredFieldValidator_DropDownList_Ciudad" HighlightCssClass="validatorCalloutHighlight" />
                <%--DropDownList_Empresa--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Empresa"
                    ControlToValidate="DropDownList_Empresa" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />LA EMPRESA es requerida."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Empresa"
                    TargetControlID="RequiredFieldValidator_DropDownList_Empresa" HighlightCssClass="validatorCalloutHighlight" />

                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Encargado:
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_Encargado" runat="server" Width="200px" 
                                ValidationGroup="CALENDARIO" AutoPostBack="True" CausesValidation="True" 
                                onselectedindexchanged="DropDownList_Encargado_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="td_izq">
                            Estado Actividad:
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_EstadoActividad" runat="server" ValidationGroup="CALENDARIO" 
                                Width="200px" AutoPostBack="True" CausesValidation="True" 
                                onselectedindexchanged="DropDownList_EstadoActividad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <%--DropDownList_Encargado--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Encargado"
                    ControlToValidate="DropDownList_Encargado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ENCARGADO es requerido."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Encargado"
                    TargetControlID="RequiredFieldValidator_DropDownList_Encargado" HighlightCssClass="validatorCalloutHighlight" />
                <%--DropDownList_EstadoActividad--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EstadoActividad"
                    ControlToValidate="DropDownList_EstadoActividad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO DE LA ACTIVIDAD es requerido."
                    ValidationGroup="CALENDARIO" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EstadoActividad"
                    TargetControlID="RequiredFieldValidator_DropDownList_EstadoActividad" HighlightCssClass="validatorCalloutHighlight" />

                <table class="table_control_registros">
                    <tr>
                        <td>
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_CALENDARIO" runat="server" Width="882px" AutoGenerateColumns="False">
                                        <headerStyle BackColor="#DDDDDD" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Domingo">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaDomingo" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesDomingo" runat="server" AutoGenerateColumns="False"
                                                            CssClass="grilla_actividades" DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" ShowHeader="False"
                                                            CellSpacing="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lunes">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaLunes" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesLunes" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" ShowHeader="False"
                                                            CellSpacing="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Martes">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaMartes" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesMartes" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" ShowHeader="False"
                                                            CellSpacing="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Miercoles">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaMiercoles" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesMiercoles" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" CellSpacing="1"
                                                            ShowHeader="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jueves">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaJueves" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesJueves" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" CellSpacing="1"
                                                            ShowHeader="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Viernes">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaViernes" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesViernes" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" ShowHeader="false"
                                                            CellSpacing="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sabado">
                                                <ItemTemplate>
                                                    <div class="div_numero_dia_grilla_Actividades">
                                                        <asp:Label ID="Label_NumeroDiaSabado" runat="server" Text="00"></asp:Label>
                                                    </div>
                                                    <div class="div_grilla_actividades">
                                                        <asp:GridView ID="GridView_ActividadesSabado" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ID_ACTIVIDAD,ID_DETALLE" CssClass="grilla_actividades" ShowHeader="False"
                                                            CellSpacing="1">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <div class="div_titulo_actividad_grilla">
                                                                            <asp:HyperLink ID="HyperLink_NombreActividad" runat="server" Text="Titulo Actividad" Target="_blank"></asp:HyperLink>
                                                                        </div>
                                                                        <table class="table_horario_e_imagen_act">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HyperLink ID="HyperLink_HorarioActividad" runat="server" Text="Desde:<br>09:00: a.m.<br>Hasta:<br>10:23 p.m." Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                                <td style="width:40px;">
                                                                                    <asp:Image ID="Image_EstadoActividad" runat="server" style="width:40px; height:40px;"/>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:HyperLink ID="HyperLink_Atividad" runat="server" Text="Nombre Actividad" Target="_blank"></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EditRowStyle VerticalAlign="Top" />
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="126px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle VerticalAlign="Top" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
