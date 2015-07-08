<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="TiposActividad.aspx.cs" Inherits="_TiposActividad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />

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
                                    Botones de Acción
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


            <asp:Panel ID="Panel_TIPOS" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Tipos de Actividad
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                <asp:HiddenField ID="HiddenField_ID_TIPO_ACTIVIDAD" runat="server" />
                                <asp:HiddenField ID="HiddenField_NOMBRE" runat="server" />
                                <asp:HiddenField ID="HiddenField_ACTIVA" runat="server" />
                                <asp:HiddenField ID="HiddenField_SECCIONES_HABILITADAS" runat="server" />

                                <asp:GridView ID="GridView_Tipos" runat="server" Width="880px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_TIPO_ACTIVIDAD" onrowcommand="GridView_Tipos_RowCommand">
                                    <Columns>

                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="40px" />
                                        </asp:ButtonField>

                                        <asp:TemplateField HeaderText="Tipo">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Nombre" runat="server" ValidationGroup="GUARDARTIPO"
                                                    Width="350px"></asp:TextBox>
                                                <!-- TextBox_Nombre -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Nombre"
                                                    ControlToValidate="TextBox_Nombre" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                                    ValidationGroup="GUARDARTIPO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Nombre"
                                                    TargetControlID="RequiredFieldValidator_TextBox_Nombre" HighlightCssClass="validatorCalloutHighlight" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DropDownList_Estado" runat="server" ValidationGroup="GUARDARTIPO"
                                                    Width="180px">
                                                    <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                    <asp:ListItem Value="True">ACTIVO</asp:ListItem>
                                                    <asp:ListItem Value="False">INACTIVO</asp:ListItem>
                                                </asp:DropDownList>
                                                <!-- DropDownList_Estado -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Estado"
                                                    ControlToValidate="DropDownList_Estado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                                                    ValidationGroup="GUARDARTIPO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Estado"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_Estado" HighlightCssClass="validatorCalloutHighlight" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Secciones Habilitadas En Resultados">
                                            <ItemTemplate>
                                                <asp:CheckBoxList ID="CheckBoxList_Secciones" runat="server" ValidationGroup="GUARDARTIPO">
                                                    <asp:ListItem Value="Resultados Encuesta">Resultados Encuesta</asp:ListItem>
                                                    <asp:ListItem Value="Control Asistencia">Control Asistencia</asp:ListItem>
                                                    <asp:ListItem Value="Entidades Colaboradoras">Entidades Colaboradoras</asp:ListItem>
                                                    <asp:ListItem Value="Compromisos">Compromisos</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </ItemTemplate>
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
                                    <asp:Button ID="Button_NUEVO_TIPO" runat="server" Text="Nuevo"
                                        CssClass="margin_botones" ValidationGroup="NUEVOTIPO" 
                                        onclick="Button_NUEVO_TIPO_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="Button_GUARDAR_TIPO" runat="server" Text="Guardar"
                                        CssClass="margin_botones" ValidationGroup="GUARDARTIPO" 
                                        onclick="Button_GUARDAR_TIPO_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_CANCELAR_TIPO" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELARTIPO" onclick="Button_CANCELAR_TIPO_Click" />
                                </td>
                            </tr>
                        </table>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

