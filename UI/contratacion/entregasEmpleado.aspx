<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="entregasEmpleado.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel_panel_Info" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="HiddenField_persona" runat="server" />
                        <asp:HiddenField ID="HiddenField_idEntrega" runat="server" />
                        <asp:HiddenField ID="HiddenField_idBodega" runat="server" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpDatePanel_Botones_internos" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
                text-align: center;">
                <div class="div_botones_internos">
                    <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: justify">
                                <asp:Table ID="Table_MENU" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="div_contenedor_formulario">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                    <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                        <tr>
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_para_convencion_hoja_trabajo">
                            <table class="tabla_alineada_derecha">
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label>
                                        &nbsp;Personas enviadas a contratar hoy.
                                    </td>
                                    <td class="style2">
                                        <div class="div_color_verde">
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label>
                                        &nbsp;Personas en plazo de contratación.
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_amarillo">
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label>
                                        &nbsp;Personas con plazo de contratación vencido.
                                    </td>
                                    <td class="td_contenedor_colores_hoja_trabajo">
                                        <div class="div_color_rojo">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo Documento Identidad">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identidad">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                        <asp:BoundField DataField="SALARIO" HeaderText="Salario" DataFormatString="$ {0:C}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HORARIO" HeaderText="Horario" />
                                        <asp:BoundField DataField="DURACION" HeaderText="Duración">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                        <asp:BoundField DataField="fechaPorContrar" HeaderText="Fecha Enviado a Contratar"
                                            DataFormatString="{0:dd/M/yyyy}">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel_Entregas_Configurados" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_Entregas_configurados" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Dotación y Epp<br />
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_MENSAJE_Entregas" runat="server">
                            <div class="div_espacio_validacion_campos">
                                <asp:Label ID="Label_MENSAJE_Entregas" runat="server" ForeColor="Red" />
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <table class="table_control_registros" width="700">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_Entregas_Configurados" runat="server" Width="700px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_PRODUCTO">
                                                <Columns>
                                                    <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" Visible="False" />
                                                    <asp:BoundField DataField="ID_ASIGNACION_SC" HeaderText="ID_ASIGNACION_SC" Visible="False" />
                                                    <asp:BoundField DataField="ID_ENTREGA" HeaderText="ID_ENTREGA" Visible="False" />
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Elemento" />
                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Cantidad">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_Cantidad" runat="server" Width="25px" ValidationGroup="ENTREGAS"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Cantidad"
                                                                ControlToValidate="TextBox_Cantidad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CANTIDAD son requeridos."
                                                                ValidationGroup="ENTREGAS" />
                                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Cantidad"
                                                                TargetControlID="RequiredFieldValidator_TextBox_Cantidad" HighlightCssClass="validatorCalloutHighlight" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Cantidad"
                                                                runat="server" TargetControlID="TextBox_Cantidad" FilterType="Numbers" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Talla">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_Talla" runat="server" Width="50px" ValidationGroup="ENTREGAS">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Talla"
                                                                ControlToValidate="DropDownList_Talla" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar la talla que esta entregando."
                                                                ValidationGroup="ENTREGAS" />
                                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Talla"
                                                                TargetControlID="RequiredFieldValidator_DropDownList_Talla" HighlightCssClass="validatorCalloutHighlight" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Marca">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_Marca" runat="server" Width="100px" ValidationGroup="ENTREGAS" Visible = "false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Modelo">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_Modelo" runat="server" Width="100px" ValidationGroup="ENTREGAS" Visible = "false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serie">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_Serie" runat="server" Width="100px" ValidationGroup="ENTREGAS" Visible = "false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IMEI">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_IMEI" runat="server" Width="100px" ValidationGroup="ENTREGAS" Visible = "false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Número Celular">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBox_Celular" runat="server" Width="100px" ValidationGroup="ENTREGAS" Visible = "false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView_lotes" runat="server" Width="700px" AutoGenerateColumns="False"
                            DataKeyNames="ID_PRODUCTO,ID_LOTE" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" Visible="true" />
                                <asp:BoundField DataField="ID_LOTE" HeaderText="ID_LOTE" Visible="true" />
                                <asp:BoundField DataField="CANTIDAD_LOTE" HeaderText="CANTIDAD_LOTE" Visible="true" />
                                <asp:BoundField DataField="TALLA" HeaderText="TALLA" Visible="true" />
                                <asp:BoundField DataField="CANTIDAD_ENTREGA" HeaderText="CANTIDAD_ENTREGA" Visible="true" />
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="HiddenField_Faltantes" runat="server" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel_botones_menu" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_BOTONES_MENU" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de entregas
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Imprimir" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                OnClick="Button_Imprimir_Click" ValidationGroup="ENTREGAS" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_Guardar" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="ENTREGAS" OnClick="Button_Guardar_Click" />
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
            <asp:PostBackTrigger ControlID="Button_Guardar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
