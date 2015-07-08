<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="contactos.aspx.cs" Inherits="_Default" %>

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="div_contenedor_formulario">
        <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div style="margin:0 auto; display:block;">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" 
                                                CssClass="margin_botones" onclick="Button_NUEVO_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                                CssClass="margin_botones" 
                                                ValidationGroup="GUARDAR" onclick="Button_GUARDAR_Click"/>
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                                CssClass="margin_botones" onclick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                                CssClass="margin_botones" onclick="Button_CANCELAR_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_LISTA_CONTACTOS" runat="server" Text="Volver al menú"  
                                                CssClass="margin_botones" onclick="Button_LISTA_CONTACTOS_Click"  />
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
            <div class="div_espaciador"></div>
        </asp:Panel>
    </div>
    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Datos de contáctos"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_CONTACTOS" runat="server" Width="885px" 
                            onselectedindexchanged="GridView_CONTACTOS_SelectedIndexChanged" >
                            <Columns>
                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                    ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:CommandField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <div class="div_contenedor_formulario">
        <asp:Panel ID="Panel_FORMULARIO" runat="server">
            <div class="div_cabeza_groupbox">
                Contácto
            </div>
            <div class="div_contenido_groupbox">








                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <asp:Panel ID="Panel_CABEZA_REGISTRO" runat="server" CssClass="div_cabeza_groupbox_gris">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 87%;">
                                    Control de registro
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 80%">
                                                <asp:Label ID="Label_REGISTRO" runat="server">(Mostrar detalles...)</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image_REGISTRO" runat="server" CssClass="img_cabecera_hoja" />
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
                                    <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" ReadOnly="True"
                                        ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_REGISTRO" runat="Server"
                        TargetControlID="Panel_CONTENIDO_REGISTRO" ExpandControlID="Panel_CABEZA_REGISTRO"
                        CollapseControlID="Panel_CABEZA_REGISTRO" Collapsed="True" TextLabelID="Label_REGISTRO"
                        ImageControlID="Image_REGISTRO" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                        ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                        SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </asp:Panel>







                <asp:Panel ID="Panel_IDENTIFICADOR" runat="server">
                    <div class="div_espaciador"></div>
                    <div style="margin:0 auto; display:block; width:580px;">
                        <div class="div_cabeza_groupbox_gris">
                            Identificador
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_IDENTIFICADOR" runat="server" Text="Identificador del Contácto"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_REGISTRO" runat="server" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_ESTADO" runat="server" Text="Estado"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ESTADO" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <div class="div_espaciador"></div>
                <asp:Panel ID="Panel_CONTACTOS_ACTUALES" runat="server">
                    <div style="margin:0 auto; display:block; width:580px;">
                        <div class="div_cabeza_groupbox_gris">
                            Contactos actuales
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <div class="div_avisos_importantes">
                                Antes de crear un contácto, verifique si no se encuentra en la siguiente lista, en caso de encontrarlo
                                seleccionelo para cargar automáticamente todos sus datos, para facilitar la edición del mismo.
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Nombre de Contácto
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_CONTACTOS_EXISTENTES" runat="server" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_CONTACTOS_EXISTENTES_SelectedIndexChanged" 
                                            Width="280px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>

                <asp:Panel ID="Panel_DATOS_CONTACTO" runat="server">
                    <div style="margin:0 auto; display:block; width:580px;">
                        <div class="div_cabeza_groupbox_gris">
                            Datos personales
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_NOM" runat="server" Text="Nombre"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_NOM" runat="server" Width="400px" MaxLength="50" 
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_CARGO" runat="server" Text="Cargo"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_CARGO" runat="server" Width="400px" 
                                            MaxLength="40" ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_MAIL" runat="server" Text="E-Mail"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_MAIL" runat="server" Width="400px" MaxLength="200" 
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONT_NOM"
                                    ControlToValidate="TextBox_CONT_NOM"
                                    Display="None" 
                                ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE del contácto es requerido." ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONT_NOM"
                                    TargetControlID="RequiredFieldValidator_TextBox_CONT_NOM"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender_TextBox_CONT_NOM"
                                    runat="server" 
                                    TargetControlID="TextBox_CONT_NOM"
                                    FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                    ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}"/>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONT_CARGO"
                                    ControlToValidate="TextBox_CONT_CARGO"
                                    Display="None" 
                                ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO del contácto es requerido." ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONT_CARGO"
                                    TargetControlID="RequiredFieldValidator_TextBox_CONT_CARGO"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender_TextBox_CONT_CARGO"
                                    runat="server" 
                                    TargetControlID="TextBox_CONT_CARGO"
                                    FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                    ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}"/>

                            <%--  TextBox_CONT_MAIL  --%>
                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONT_MAIL"
                                    ControlToValidate="TextBox_CONT_MAIL"
                                    Display="None" 
                                ErrorMessage="<b>Campo Requerido faltante</b><br />El E-Mail del contácto es requerido." ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONT_MAIL"
                                    TargetControlID="RequiredFieldValidator_TextBox_CONT_MAIL"
                                    HighlightCssClass="validatorCalloutHighlight" />--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_CONT_MAIL" Display="None" 
                                runat="server" ErrorMessage="<b>E-Mail incorrecto</b><br />El EMAIL tiene un formato incorrecto." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ControlToValidate="TextBox_CONT_MAIL" ValidationGroup="GUARDAR"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONT_MAIL1"
                                    TargetControlID="RegularExpressionValidator_TextBox_CONT_MAIL"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_TEL" runat="server" Text="Teléfono"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_TEL" runat="server" MaxLength="20" Width="100px" 
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_TEL1" runat="server" Text="Teléfono 2"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_TEL1" runat="server" MaxLength="20" Width="100px" 
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CONT_CELULAR" runat="server" Text="Celular"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_CELULAR" runat="server" MaxLength="20" 
                                            Width="100px" ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CONT_TEL"
                                    ControlToValidate="TextBox_CONT_TEL"
                                    Display="None" 
                                ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO del contácto es requerido." ValidationGroup="GUARDAR" 
                                     />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CONT_TEL"
                                    TargetControlID="RequiredFieldValidator_TextBox_CONT_TEL"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender_TextBox_CONT_TEL" runat="server" 
                                    TargetControlID="TextBox_CONT_TEL" FilterType="Numbers,Custom" ValidChars="()[]{}- extEXT"/>

                            <ajaxToolkit:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender_TextBox_CONT_TEL1" runat="server" 
                                    TargetControlID="TextBox_CONT_TEL1" FilterType="Numbers,Custom" ValidChars="()[]{}- extEXT"/>

                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CONT_CELULAR" runat="server" TargetControlID="TextBox_CONT_CELULAR" FilterType="Numbers" />
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_REGIONAL" runat="server" Text="Regional"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" 
                                                    AutoPostBack="True" 
                                                    Width="200px" ValidationGroup="GUARDAR" 
                                                    onselectedindexchanged="DropDownList_REGIONAL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_CONT_DEPARTAMENTO" runat="server" Text="Departamento"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CONT_DEPARTAMENTO" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownList_CONT_DEPARTAMENTO_SelectedIndexChanged" 
                                                    Width="200px" ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_CONT_CIUDAD" runat="server" Text="Ciudad"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CONT_CIUDAD" runat="server" Width="200px" 
                                                    ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CONT_CIUDAD"
                                            ControlToValidate="DropDownList_CONT_CIUDAD"
                                            Display="None" 
                                            ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD del contácto es requeridA." ValidationGroup="GUARDAR" 
                                             />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CONT_CIUDAD"
                                            TargetControlID="RequiredFieldValidator_DropDownList_CONT_CIUDAD"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                        <asp:Panel ID="Panel_CARTERA" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Datos para Contactos de Cartera
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_avisos_importantes">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Dias de pago:
                                            </td>
                                            <td class="td_der"> 
                                                <asp:TextBox ID="TextBox_DIAS_PAGO" runat="server" MaxLength="10" Width="50px" 
                                                    ValidationGroup="GUARDAR" ToolTip="Digite los dias para pago."></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label1" runat="server" Text="Forma de Pago:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_FORMA_PAGO" runat="server" 
                                                    AutoPostBack="True" 
                                                    Width="200px" ValidationGroup="GUARDAR" 
                                                    onselectedindexchanged="DropDownList_FORMA_PAGO_SelectedIndexChanged" >
                                                    <asp:ListItem Value="forma">Seleccione Forma de Pago...</asp:ListItem>
                                                    <asp:ListItem>Consignacion</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>Transferencia</asp:ListItem>
                                                    <asp:ListItem>Efectivo</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label2" runat="server" Text="banco:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_BANCOS" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownList_BANCOS_SelectedIndexChanged" 
                                                    Width="200px" ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label3" runat="server" Text="Cuenta:"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CUENTAS" runat="server" Width="200px" 
                                                    ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_INMEDIATO" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Contácto inmediato
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <div class="div_avisos_importantes">
                                    <div style="text-align:center;">
                                        Solo puede existir un contácto INMEDIATO por proceso.
                                    </div>
                                </div>
                                <asp:CheckBox ID="CheckBox_INMEDIATO" runat="server" Text="Contácto inmediato" />
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

