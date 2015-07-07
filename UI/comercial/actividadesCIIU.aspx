<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="actividadesCIIU.aspx.cs" Inherits="_Default" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE" runat="server" />

            <asp:Panel ID="Panel_FONDO_MENSAJE_SECCION" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJE_ACCION_SECCION" runat="server">
                <asp:Image ID="Image_MENSAJE_ACCION_SECCION" runat="server" Width="50px" Height="50px"
                    Style="margin: 5px auto 8px auto; display: block;" />
                <asp:Panel ID="Panel_COLOR_ACCION_SECCION" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE_ACCION_SECCION" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:Button ID="Button_OK_SECCION" runat="server" Text="Aceptar" CssClass="margin_botones"
                                    OnClick="Button_OK_SECCION_Click" />
                            </td>
                            <td>
                                <asp:Button ID="Button_CANCEL_SECCION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                    OnClick="Button_CANCEL_SECCION_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJE_SECCION" runat="server">
                <asp:Image ID="Image_MENSAJE_SECCION_POPUP" runat="server" Width="50px" Height="50px"
                    Style="margin: 5px auto 8px auto; display: block;" />
                <asp:Panel ID="Panel_COLOR_MENSAJE_SECCION" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE_SECCION" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE_SECCION" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_SECCION_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_SECCION" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        SECCIÓNES
                    </div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_ACCION_SECCION" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_SECCION" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_SECCION" runat="server" />
                        <asp:HiddenField ID="HiddenField_DESCRIPCION_SECCION" runat="server" />
                        
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_SECCIONES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_SECCION" OnRowCommand="GridView_SECCIONES_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar" ValidationGroup="ELIMINARSECICON">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar" ValidationGroup="MODIFICARSECCION">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_SECCION" HeaderText="ID_SECCION" Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sección">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_ID_SECCION" runat="server" ValidationGroup="GUARDARSECCION"
                                                    Width="50px" MaxLength="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ID_SECCION"
                                                    ControlToValidate="TextBox_ID_SECCION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La SECCIÓN es requerida."
                                                    ValidationGroup="GUARDARSECCION" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ID_SECCION"
                                                    TargetControlID="RequiredFieldValidator_TextBox_ID_SECCION" HighlightCssClass="validatorCalloutHighlight" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ID_SECCION" runat="server" TargetControlID="TextBox_ID_SECCION" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars="Ññ" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_DESCRIPCION_SECCION" runat="server" ValidationGroup="GUARDARSECCION"
                                                    Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DESCRIPCION_SECCION"
                                                    ControlToValidate="TextBox_DESCRIPCION_SECCION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                                    ValidationGroup="GUARDARSECCION" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DESCRIPCION_SECCION"
                                                    TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION_SECCION" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVO_SECCION" runat="server" Text="Nueva Sección" CssClass="margin_botones"
                                            ValidationGroup="NUEVOSECCION" OnClick="Button_NUEVO_SECCION_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_SECCION" runat="server" Text="Guardar Sección" CssClass="margin_botones"
                                            ValidationGroup="GUARDARSECCION" OnClick="Button_GUARDAR_SECCION_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_SECCION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARSECCION" OnClick="Button_CANCELAR_SECCION_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>



            <asp:Panel ID="Panel_DIVISION" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        DIVISIONES
                    </div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_ACCION_DIVISION" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_DIVISION" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_DIVISION" runat="server" />
                        <asp:HiddenField ID="HiddenField_DESCRIPCION_DIVISION" runat="server" />
                        
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_DIVISIONES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_DIVISION" onrowcommand="GridView_DIVISIONES_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar" ValidationGroup="ELIMINARDIVISION">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar" ValidationGroup="MODIFICARDIVISION">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_DIVISION" HeaderText="ID_DIVISION" Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="División">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_ID_DIVISION" runat="server" ValidationGroup="GUARDARDIVISION"
                                                    Width="50px" MaxLength="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ID_DIVISION"
                                                    ControlToValidate="TextBox_ID_DIVISION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ID_DIVISIÓN es requerida."
                                                    ValidationGroup="GUARDARDIVISION" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ID_DIVISION"
                                                    TargetControlID="RequiredFieldValidator_TextBox_ID_DIVISION" HighlightCssClass="validatorCalloutHighlight" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ID_DIVISION" runat="server" TargetControlID="TextBox_ID_DIVISION" FilterType="Numbers" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_DESCRIPCION_DIVISION" runat="server" ValidationGroup="GUARDARDIVISION"
                                                    Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DESCRIPCION_DIVISION"
                                                    ControlToValidate="TextBox_DESCRIPCION_DIVISION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                                    ValidationGroup="GUARDARDIVISION" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DESCRIPCION_DIVISION"
                                                    TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION_DIVISION" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVA_DIVISION" runat="server" Text="Nueva División" CssClass="margin_botones"
                                            ValidationGroup="NUEVODIVISION" onclick="Button_NUEVA_DIVISION_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_DIVISION" runat="server" Text="Guardar División" CssClass="margin_botones"
                                            ValidationGroup="GUARDARDIVISION" 
                                            onclick="Button_GUARDAR_DIVISION_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_DIVISION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARDIVISION" 
                                            onclick="Button_CANCELAR_DIVISION_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>



            <asp:Panel ID="Panel_CLASES" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        CLASES
                    </div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_ACCION_CLASE" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_CLASE" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_CLASE" runat="server" />
                        <asp:HiddenField ID="HiddenField_DESCRIPCION_CLASE" runat="server" />
                        
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_CLASES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_CLASE" onrowcommand="GridView_CLASES_RowCommand" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar" ValidationGroup="ELIMINARCLASE">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar" ValidationGroup="MODIFICARCLASE">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_CLASE" HeaderText="ID_CLASE" Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Clase">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_ID_CLASE" runat="server" ValidationGroup="GUARDARCLASE"
                                                    Width="50px" MaxLength="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ID_CLASE"
                                                    ControlToValidate="TextBox_ID_CLASE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ID_CLASE es requerida."
                                                    ValidationGroup="GUARDARCLASE" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ID_CLASE"
                                                    TargetControlID="RequiredFieldValidator_TextBox_ID_CLASE" HighlightCssClass="validatorCalloutHighlight" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ID_CLASE" runat="server" TargetControlID="TextBox_ID_CLASE" FilterType="Numbers" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_DESCRIPCION_CLASE" runat="server" ValidationGroup="GUARDARCLASE"
                                                    Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DESCRIPCION_CLASE"
                                                    ControlToValidate="TextBox_DESCRIPCION_CLASE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                                    ValidationGroup="GUARDARCLASE" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DESCRIPCION_CLASE"
                                                    TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION_CLASE" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVO_CLASE" runat="server" Text="Nueva Clase" CssClass="margin_botones"
                                            ValidationGroup="NUEVOCLASE" onclick="Button_NUEVO_CLASE_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_CLASE" runat="server" Text="Guardar Clase" CssClass="margin_botones"
                                            ValidationGroup="GUARDARCLASE" onclick="Button_GUARDAR_CLASE_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_CLASE" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARCLASE" onclick="Button_CANCELAR_CLASE_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="Panel_ACTIVIDADES" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        ACTIVIDADES
                    </div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_ACCION_ACTIVIDAD" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_ACTIVIDAD" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_ACTIVIDAD" runat="server" />
                        <asp:HiddenField ID="HiddenField_DESCRIPCION_ACTIVIDAD" runat="server" />
                        
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ACTIVIDADES" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_ACTIVIDAD" 
                                    onrowcommand="GridView_ACTIVIDADES_RowCommand" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar" ValidationGroup="ELIMINARACTIVIDAD">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar" ValidationGroup="MODIFICARACTIVIDAD">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_ACTIVIDAD" HeaderText="ID_ACTIVIDAD" Visible="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Actividad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_ID_ACTIVIDAD" runat="server" ValidationGroup="GUARDARACTIVIDAD"
                                                    Width="50px" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ID_ACTIVIDAD"
                                                    ControlToValidate="TextBox_ID_ACTIVIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ID_ACTIVIDAD es requerida."
                                                    ValidationGroup="GUARDARACTIVIDAD" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ID_ACTIVIDAD"
                                                    TargetControlID="RequiredFieldValidator_TextBox_ID_ACTIVIDAD" HighlightCssClass="validatorCalloutHighlight" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ID_ACTIVIDAD" runat="server" TargetControlID="TextBox_ID_ACTIVIDAD" FilterType="Numbers" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_DESCRIPCION_ACTIVIDAD" runat="server" ValidationGroup="GUARDARACTIVIDAD"
                                                    Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DESCRIPCION_ACTIVIDAD"
                                                    ControlToValidate="TextBox_DESCRIPCION_ACTIVIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                                    ValidationGroup="GUARDARACTIVIDAD" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DESCRIPCION_ACTIVIDAD"
                                                    TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION_ACTIVIDAD" HighlightCssClass="validatorCalloutHighlight" />
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
                                        <asp:Button ID="Button_NUEVO_ACTIVIDAD" runat="server" Text="Nueva Actividad" CssClass="margin_botones"
                                            ValidationGroup="NUEVOACTIVIDAD" onclick="Button_NUEVO_ACTIVIDAD_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_ACTIVIDAD" runat="server" 
                                            Text="Guardar Actividad" CssClass="margin_botones"
                                            ValidationGroup="GUARDARACTIVIDAD" 
                                            onclick="Button_GUARDAR_ACTIVIDAD_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CANCELAR_ACTIVIDAD" runat="server" Text="Cancelar" CssClass="margin_botones"
                                            ValidationGroup="CANCELARACTIVIDAD" 
                                            onclick="Button_CANCELAR_ACTIVIDAD_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

