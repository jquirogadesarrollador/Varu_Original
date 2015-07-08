<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="CondicionesSeleccion.aspx.cs" Inherits="_Default" %>

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
    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_contenedor_formulario">
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
                                                ValidationGroup="NUEVO" onclick="Button_GUARDAR_Click"/>
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
                                            <asp:Button ID="Button_VOLVER" runat="server" Text="Volver al menú"  
                                                CssClass="margin_botones" onclick="Button_VOLVER_Click"  />
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
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <div class="div_contenedor_formulario">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Registro de condiciones de envio
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_DATOS" runat="server" Width="885px" 
                            AutoGenerateColumns="False" 
                            onselectedindexchanged="GridView_DATOS_SelectedIndexChanged" 
                            DataKeyNames="REGISTRO,REGISTRO_CONTACTO">
                            <Columns>
                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                    ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:CommandField>
                                <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                <asp:BoundField DataField="REGISTRO_CONTACTO" HeaderText="REGISTRO_CONTACTO" 
                                    Visible="False" />
                                <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                <asp:BoundField DataField="NOMBRE_DEPARTAMENTO" HeaderText="Departamento" />
                                <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                <asp:BoundField DataField="NOMBRE_CONTACTO" HeaderText="Contácto" />
                                <asp:BoundField DataField="MAIL_CONTACTO" HeaderText="E-Mail" />
                                <asp:BoundField DataField="DIR_ENVIO" HeaderText="Dirección envio" />
                                <asp:BoundField DataField="TEL_ENVIO" HeaderText="Telefono envio" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="COND_ENVIO" HeaderText="Condición">
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Datos de la condición de envio
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Control de Registro
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_CRE" runat="server" Text="Fecha de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:Panel ID="Panel_IDENTIFICADOR" runat="server">
                    <div style="margin:0 auto; display:block;">
                        <div class="div_cabeza_groupbox_gris">
                            Identificador
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Identificador de envio
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_REGISTRO" runat="server" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>

                <div class="div_cabeza_groupbox_gris">
                    Datos de persona que solicita
                </div>
                <div class="div_contenido_groupbox_gris">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq" style="width:120px">
                                        Nombre
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_CONT_NOMBRE" runat="server" Width="400px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_CONT_NOMBRE_SelectedIndexChanged" 
                                            ValidationGroup="NUEVO">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:120px">
                                        E-Mail
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_CONT_MAIL" runat="server" Width="395" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_CONT_NOMBRE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CONT_NOMBRE"
                                    ControlToValidate="DropDownList_CONT_NOMBRE"
                                    Display="None" 
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La CONTÁCTO es requerido." ValidationGroup="NUEVO" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CONT_NOMBRE"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CONT_NOMBRE"
                                    HighlightCssClass="validatorCalloutHighlight" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="div_espaciador"></div>
                <div class="div_cabeza_groupbox_gris">
                    Datos básicos de la condición de envio
                </div>
                <div class="div_contenido_groupbox_gris">
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq" style="width:120px">
                                Dirección
                            </td>
                            <td class="td_der">
                                <asp:TextBox ID="TextBox_DIR_ENVIO" runat="server" Width="395px" 
                                    ValidationGroup="NUEVO" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <!-- TextBox_DIR_ENVIO -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DIR_ENVIO"
                            ControlToValidate="TextBox_DIR_ENVIO"
                            Display="None" 
                            ErrorMessage="<b>Campo Requerido faltante</b><br />La DIRECCIÓN es requerida." ValidationGroup="NUEVO" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DIR_ENVIO"
                            TargetControlID="RequiredFieldValidator_TextBox_DIR_ENVIO"
                            HighlightCssClass="validatorCalloutHighlight" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq" style="width:120px">
                                        Regional
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" Width="400px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_REGIONAL_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:120px">
                                        Departamento
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" Width="400px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_DEPARTAMENTO_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq" style="width:120px">
                                        Ciudad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" Width="400px" 
                                            ValidationGroup="NUEVO">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_CIUDAD -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD"
                                    ControlToValidate="DropDownList_CIUDAD"
                                    Display="None" 
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD es requerida." ValidationGroup="NUEVO" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD"
                                    HighlightCssClass="validatorCalloutHighlight" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table class="table_control_registros">
                        <tr>
                            <td class="td_izq" style="width:120px">
                                Telefono
                            </td>
                            <td class="td_der">
                                <asp:TextBox ID="TextBox_TEL_ENVIO" runat="server" Width="395px" 
                                    ValidationGroup="NUEVO" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Condiciones de envio<br /> &nbsp;<asp:TextBox ID="TextBox_COND_ENVIO" 
                                    runat="server" Width="514px" Height="57px" ValidationGroup="NUEVO" 
                                    MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <!-- TextBox_TEL_ENVIO -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_TEL_ENVIO"
                            ControlToValidate="TextBox_TEL_ENVIO"
                            Display="None" 
                            ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO es requerido." ValidationGroup="NUEVO" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_TEL_ENVIO"
                            TargetControlID="RequiredFieldValidator_TextBox_TEL_ENVIO"
                            HighlightCssClass="validatorCalloutHighlight" />
                    <ajaxToolkit:FilteredTextBoxExtender
                            ID="FilteredTextBoxExtender_TextBox_TEL_ENVIO" runat="server" 
                            TargetControlID="TextBox_TEL_ENVIO" FilterType="Numbers,Custom" ValidChars="()[]{}- extEXT"/>

                    <!-- TextBox_COND_ENVIO -->
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_COND_ENVIO"
                            ControlToValidate="TextBox_COND_ENVIO"
                            Display="None" 
                            ErrorMessage="<b>Campo Requerido faltante</b><br />Las FUNCIONES es requerido." ValidationGroup="NUEVO" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_COND_ENVIO"
                            TargetControlID="RequiredFieldValidator_TextBox_COND_ENVIO"
                            HighlightCssClass="validatorCalloutHighlight" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="div_espaciador"></div>
</asp:Content>

