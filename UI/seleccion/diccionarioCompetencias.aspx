<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="diccionarioCompetencias.aspx.cs" Inherits="_DiccionarioCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" 
                        CssClass="margin_botones" onclick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador">
        </div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div style="margin: 0 auto; display: block;">
                        <div class="div_cabeza_groupbox">
                            Botones de acción
                        </div>
                        <div class="div_contenido_groupbox">
                            <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                <tr>
                                    <td colspan="0">
                                        <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                            OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                    </td>
                                    <td colspan="0">
                                        <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                            OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                    </td>
                                    <td colspan="0">
                                        <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                    </td>
                                    <td colspan="0">
                                        <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" class="margin_botones"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_DICCIONARIO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Diccionario de Competencias y Habilidades</div>
            <div class="div_contenido_groupbox">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_COMPETENCIA" runat="server" />
                                <asp:HiddenField ID="HiddenField_COMPETENCIA" runat="server" />
                                <asp:HiddenField ID="HiddenField_DEFINICION" runat="server" />
                                <asp:HiddenField ID="HiddenField_AREACOMPETENCIA" runat="server" />
                                
                                <div class="div_para_convencion_hoja_trabajo">
                                    <table class="tabla_alineada_derecha">
                                        <tr>
                                            <td class="td_izq">
                                                Competencias y Habilidades Gerenciales
                                            </td>
                                            <td class="td_der">
                                                <div class="div_color_amarillo">
                                                </div>
                                            </td>
                                            <td>
                                                Competencias y Habilidades Comerciales
                                            </td>
                                            <td class="td_contenedor_colores_hoja_trabajo">
                                                <div class="div_color_verde">
                                                </div>
                                            </td>
                                            <td>
                                                Competencias y Habilidades Administrativas
                                            </td>
                                            <td class="td_contenedor_colores_hoja_trabajo">
                                                <div class="div_color_gris">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <asp:GridView ID="GridView_COMPETENCIAS" runat="server" Width="880px" 
                                    AutoGenerateColumns="False" DataKeyNames="ID_COMPETENCIA" 
                                    onrowcommand="GridView_COMPETENCIAS_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_COMPETENCIA" HeaderText="ID_COMPETENCIA" 
                                            Visible="False" />
                                        <asp:TemplateField HeaderText="Competencia ó Habilidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_COMPETENCIA" runat="server" ValidationGroup="GUARDARCOMPETENCIA" Width="220px"></asp:TextBox>
                                                <!-- TextBox_COMPETENCIA -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_COMPETENCIA"
                                                    ControlToValidate="TextBox_COMPETENCIA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La COMPETENCIA es requerida."
                                                    ValidationGroup="GUARDARCOMPETENCIA" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_COMPETENCIA"
                                                    TargetControlID="RequiredFieldValidator_TextBox_COMPETENCIA" HighlightCssClass="validatorCalloutHighlight" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Definición">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_DEFINICION" runat="server" ValidationGroup="GUARDARCOMPETENCIA"
                                                    TextMode="MultiLine" Width="360px" Height="60px"></asp:TextBox>
                                                <!-- TextBox_DEFINICION -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DEFINICION"
                                                    ControlToValidate="TextBox_DEFINICION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DEFINICIÓN es requerida."
                                                    ValidationGroup="GUARDARCOMPETENCIA" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DEFINICION"
                                                    TargetControlID="RequiredFieldValidator_TextBox_DEFINICION" HighlightCssClass="validatorCalloutHighlight" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Área">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList_AreaCompetencia" runat="server" 
                                                    ValidationGroup="GUARDARCOMPETENCIA" Width="150px">
                                                    <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                    <asp:ListItem Value="Administrativas">Administrativas</asp:ListItem>
                                                    <asp:ListItem Value="Comerciales">Comerciales</asp:ListItem>
                                                    <asp:ListItem Value="Gerenciales">Gerenciales</asp:ListItem>
                                                </asp:DropDownList>
                                                <!-- DropDownList_AreaCompetencia -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_AreaCompetencia"
                                                    ControlToValidate="DropDownList_AreaCompetencia" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El AREA es requerida."
                                                    ValidationGroup="GUARDARCOMPETENCIA" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_AreaCompetencia"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_AreaCompetencia" HighlightCssClass="validatorCalloutHighlight" />
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
                        <div style="text-align: left; margin-left: 10px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_NUEVA_COMPETENCIA" runat="server" 
                                            Text="Nueva Competencia / Habilidad" CssClass="margin_botones"
                                            ValidationGroup="NUEVACOMPETENCIA" 
                                            onclick="Button_NUEVA_COMPETENCIA_Click"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_COMPETENCIA" runat="server" 
                                            Text="Guardar Competencia / Habilidad" CssClass="margin_botones"
                                            ValidationGroup="GUARDARCOMPETENCIA" 
                                            onclick="Button_GUARDAR_COMPETENCIA_Click"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_COMPETENCIA" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARCOMPETENCIA" 
                                            onclick="Button_CANCELAR_COMPETENCIA_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_FORM_BOTONES_ABAJO" runat="server">
        <div class="div_espaciador">
        </div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div style="margin: 0 auto; display: block;">
                        <div class="div_cabeza_groupbox">
                            Botones de acción
                        </div>
                        <div class="div_contenido_groupbox">
                            <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                <tr>
                                    <td colspan="0">
                                        <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                            OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                    </td>
                                    <td colspan="0">
                                        <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                            OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                    </td>
                                    <td colspan="0">
                                        <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

