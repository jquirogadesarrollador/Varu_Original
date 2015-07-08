<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="preguntasRefLaboral.aspx.cs" Inherits="_PreguntasRefLaboral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="HiddenField_ID_CATEGORIA_SEL" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PREGUNTA_SEL" runat="server" />
            <asp:HiddenField ID="HiddenField_PROCESO" runat="server" />


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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>

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
                                                <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar Categorías" CssClass="margin_botones"
                                                    OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_MODIFICAR_PREGUNTAS" runat="server" Text="Actualizar Preguntas"
                                                    CssClass="margin_botones" ValidationGroup="MODIFICARPREGUNTAS" 
                                                    onclick="Button_MODIFICAR_PREGUNTAS_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                            </td>
                                            <td colspan="0">
                                                <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" class="margin_botones" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_Categorias" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Categorización de Preguntas para Confirmación de Referencias
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">

                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA_CAT" runat="server" />
                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_CAT" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_CATEGORIA" runat="server" />
                                <asp:HiddenField ID="HiddenField_NOMBRE_CAT" runat="server" />

                                <asp:GridView ID="GridView_CATEGORIAS" runat="server" Width="883px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_CATEGORIA" onrowcommand="GridView_CATEGORIAS_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Nombre Categoría">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_NOMBRE_CAT" runat="server" ValidationGroup="GUARDARCAT" Width="750px"></asp:TextBox>
                                                <!-- TextBox_NOMBRE_CAT -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRE_CAT"
                                                    ControlToValidate="TextBox_NOMBRE_CAT" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DE LA CATEGORÍA es requerido."
                                                    ValidationGroup="GUARDARCAT" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRE_CAT"
                                                    TargetControlID="RequiredFieldValidator_TextBox_NOMBRE_CAT" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVA_CAT" runat="server" Text="Nueva Categoría" CssClass="margin_botones"
                                            ValidationGroup="NUEVACAT" onclick="Button_NUEVA_CAT_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_CAT" runat="server" Text="Guardar Categoría" CssClass="margin_botones"
                                            ValidationGroup="GUARDARCAT" onclick="Button_GUARDAR_CAT_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_CAT" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARCAT" onclick="Button_CANCELAR_CAT_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_PREGUNTAS" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Preguntas Asociadas a la Categoría Seleccionada</div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_PREGUNTA" runat="server" />
                                <asp:HiddenField ID="HiddenField_PREGUNTA" runat="server" />
                                <asp:GridView ID="GridView_PREGUNTAS" runat="server" Width="883px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_PREGUNTA,ID_CATEGORIA" 
                                    OnRowCommand="GridView_PREGUNTAS_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_PREGUNTA" HeaderText="ID_PREGUNTA" Visible="False" />
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_PREGUNTA" runat="server" ValidationGroup="GUARDARPREGUNTA"
                                                    TextMode="MultiLine" Width="750px" Height="60px"></asp:TextBox>
                                                <!-- TextBox_PREGUNTA -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PREGUNTA"
                                                    ControlToValidate="TextBox_PREGUNTA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TEXTO DE LA PREGUNTA es requerido."
                                                    ValidationGroup="GUARDARPREGUNTA" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PREGUNTA"
                                                    TargetControlID="RequiredFieldValidator_TextBox_PREGUNTA" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVA_PREGUNTA" runat="server" Text="Nueva Pregunta" CssClass="margin_botones"
                                            ValidationGroup="NUEVAPREGUNTA" OnClick="Button_NUEVA_PREGUNTA_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_PREGUNTA" runat="server" Text="Guardar Pregunta" CssClass="margin_botones"
                                            ValidationGroup="GUARDARPREGUNTA" OnClick="Button_GUARDAR_PREGUNTA_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_PREGUNTA" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARPREGUNTA" OnClick="Button_CANCELAR_PREGUNTA_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                                                <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar Categorías" CssClass="margin_botones"
                                                    OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_MODIFICAR_PREGUNTAS_1" runat="server" Text="Actualizar Preguntas" CssClass="margin_botones"
                                                    ValidationGroup="MODIFICARPREGUNTAS" OnClick="Button_MODIFICAR_PREGUNTAS_Click" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
