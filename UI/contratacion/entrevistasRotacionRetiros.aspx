<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="entrevistasRotacionRetiros.aspx.cs" Inherits="_EntrevistaRotacionRetiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    


    <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPLEADO" runat="server" />
    <asp:HiddenField ID="HiddenField_REGISTRO_CONTRATO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO" runat="server" />

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
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" style="text-align:center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" 
                        OnClick="Button_CERRAR_MENSAJE_Click" style="height: 26px" />
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
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_IMPRIMIR" runat="server" Text="Imprimir" 
                                        CssClass="margin_botones" ValidationGroup="IMPRIMIR" 
                                        onclick="Button_IMPRIMIR_Click" />
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
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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

        <div class="div_espaciador"></div>

        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Resultados de la busqueda
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                            AutoGenerateColumns="False" DataKeyNames="ID_SOLICITUD,ID_EMPLEADO,ID_EMPRESA,NUM_CONTRATO"
                            OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                            OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad">
                                    <ItemStyle CssClass="columna_grid_izq" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Regional" HeaderText="Regional">
                                    <ItemStyle CssClass="columna_grid_izq" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_CONTRATO" HeaderText="Número contrato">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ACTIVO" HeaderText="Contrato activo">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo documento">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número identidad">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos"></asp:BoundField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres"></asp:BoundField>
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo"></asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_DATOS_TRABAJADOR" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información del Trabajador
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Apellidos
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_APELLIDOS_TRABAJADOR" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Nombres
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NOMBRES_TRABAJADOR" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Tipo Documento
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_TIPO_DOCUMENTO" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Número documento
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NUM_DOC_IDENTIDAD" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Empresa
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Número contrato
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_REGISTRO_CONTRATO" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="td_izq">
                            Estado
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_ESTADO" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                        </td>
                        <td class="td_der">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_GrillaMotivosRotacion" runat="server">
        <div class="div_espaciador"></div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Lista de Motivos de Rotación y Retiro
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_MotivosRotacion" runat="server" Width="885px" AutoGenerateColumns="False"
                            
                            
                            DataKeyNames="ID_MAESTRA_ROTACION,ID_DETALLE_ROTACION,ID_ROTACION_EMPRESA,ID_DETALLE_ROTACION_EMPLEADO" >
                            <Columns>
                                
                                <asp:BoundField DataField="TITULO_MAESTRA_ROTACION" HeaderText="Categoría" />
                                <asp:BoundField DataField="TITULO" HeaderText="Motivo" />
                                <asp:TemplateField HeaderText="Aplicar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox_Configurado" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="columna_grid_centrada" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_Observaciones" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Observaciones
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox_Observaciones" runat="server" 
                                ValidationGroup="ADICIONAR" TextMode="MultiLine" Height="120px" Width="851px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <%--TextBox_Observaciones--%>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Observaciones" ControlToValidate="TextBox_Observaciones"
                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda."
                    ValidationGroup="ADICIONAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Observaciones"
                    TargetControlID="RequiredFieldValidator_TextBox_Observaciones" HighlightCssClass="validatorCalloutHighlight" />
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_BOTONES_PIE" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Botones de acción
            </div>
            <div class="div_contenido_groupbox">
                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                    <tr>
                        <td rowspan="0">
                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                OnClick="Button_MODIFICAR_Click" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones"
                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>