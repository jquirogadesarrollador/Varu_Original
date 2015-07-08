<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="entidadesColaboradoras.aspx.cs" Inherits="_EntidadesColaboradoras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:HiddenField ID="HiddenField_ID_ENTIDAD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
    
    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
    
    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el 
                    mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

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
                                <td rowspan="0">
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO_REGISTRO_ACOSET" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_BUSCAR" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="ID_ENTIDAD" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand"
                            AllowPaging="True" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                    Text="seleccionar">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado">
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

    <asp:Panel ID="Panel_InformacionEntidad" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de la Entidad Colaboradora
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <asp:Panel ID="Panel_CABEZA_REGISTRO" runat="server" CssClass="div_cabeza_groupbox_gris">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                            <tr>
                                <td style="width:87%;">
                                    Control de registro
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                                        <tr>
                                            <td style="font-size:80%">
                                                <asp:Label ID="Label_REGISTRO" runat="server">(Mostrar detalles...)</asp:Label>    
                                            </td>
                                            <td>
                                                <asp:Image ID="Image_REGISTRO" runat="server" CssClass="img_cabecera_hoja"/>
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
                                    <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_REGISTRO" runat="Server"
                        TargetControlID="Panel_CONTENIDO_REGISTRO"
                        ExpandControlID="Panel_CABEZA_REGISTRO"
                        CollapseControlID="Panel_CABEZA_REGISTRO" 
                        Collapsed="True"
                        TextLabelID="Label_REGISTRO"
                        ImageControlID="Image_REGISTRO"    
                        ExpandedText="(Ocultar detalles...)"
                        CollapsedText="(Mostrar detalles...)"
                        ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                        CollapsedImage="~/imagenes/plantilla/expand.jpg"
                        SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>  
                </asp:Panel>

                <asp:Panel ID="Panel_InformacionBasicaEntidad" runat="server">
                    <div class="div_espaciador"></div>
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq">
                                Nombre:
                            </td>
                            <td class="td_der">
                                <asp:TextBox ID="TextBox_NombreEntidad" runat="server" 
                                    ValidationGroup="ADICIONAR" Width="400px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Panel ID="Panel_EstadoEntidad" runat="server">
                                    Estado
                                    <asp:DropDownList ID="DropDownList_EstadoEntidad" runat="server" ValidationGroup="ADICIONAR"
                                        Width="200px">
                                    </asp:DropDownList>
                                    <%--DropDownList_EstadoEntidad--%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EstadoEntidad"
                                        ControlToValidate="DropDownList_EstadoEntidad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO de la Entidad es requerido."
                                        ValidationGroup="ADICIONAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EstadoEntidad"
                                        TargetControlID="RequiredFieldValidator_DropDownList_EstadoEntidad" HighlightCssClass="validatorCalloutHighlight" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <%--TextBox_NombreEntidad--%>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NombreEntidad"
                        ControlToValidate="TextBox_NombreEntidad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE de la Entidad es requerido."
                        ValidationGroup="ADICIONAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NombreEntidad"
                        TargetControlID="RequiredFieldValidator_TextBox_NombreEntidad" HighlightCssClass="validatorCalloutHighlight" />

                    <div class="div_espaciador"></div>

                    <table class="table_control_registros">
                        <tr>
                            <td style="text-align:center;">
                                Descripción<br />
                                <asp:TextBox ID="TextBox_DescripcionEntidad" runat="server" 
                                    ValidationGroup="ADICIONAR" TextMode="MultiLine" Height="92px" Width="826px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <%--TextBox_DescripcionEntidad--%>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DescripcionEntidad"
                        ControlToValidate="TextBox_DescripcionEntidad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN de la Entidad es requerida."
                        ValidationGroup="ADICIONAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DescripcionEntidad"
                        TargetControlID="RequiredFieldValidator_TextBox_DescripcionEntidad" HighlightCssClass="validatorCalloutHighlight" />

                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
