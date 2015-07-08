<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="fuentesReclutamiento.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    

    <asp:HiddenField ID="HiddenField_ID_FUENTE" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el 
                    mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
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
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" ValidationGroup="MODIFICAR" />
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
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                            OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging"
                            AutoGenerateColumns="False" DataKeyNames="ID_FUENTE" 
                            onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOM_FUENTE" HeaderText="Nombre" />
                                <asp:BoundField DataField="TEL_FUENTE" HeaderText="Telefono">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ENCARGADO" HeaderText="Contácto" />
                                <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                <asp:BoundField DataField="ID_FUENTE" HeaderText="ID_FUENTE" Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Fuentes de Reclutamiento
            </div>
            <div class="div_contenido_groupbox">

                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <asp:Panel ID="Panel_CABEZA_REGISTRO" runat="server" CssClass="div_cabeza_groupbox_gris">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                            <tr>
                                <td style="width:87%;">
                                    Control de Registro
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


                <asp:Panel ID="Panel_DATOS_FUENTE" runat="server">
                    <div class="div_espaciador"></div>
                        <div class="div_cabeza_groupbox_gris">
                            Datos de la fuente de reclutamiento
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_NOM_FUENTE" runat="server" Text="Nombre Fuente"></asp:Label>
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_NOM_FUENTE" runat="server" Width="400px" MaxLength="50"
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_DIR_FUENTE" runat="server" Text="Dirección Fuente"></asp:Label>
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_DIR_FUENTE" runat="server" Width="400px" MaxLength="40"
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_ENCARGADO" runat="server" Text="Contactar a"></asp:Label>
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_ENCARGADO" runat="server" Width="400px" MaxLength="40" ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_CARGO_ENC" runat="server" Text="Cargo del contacto"></asp:Label>
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_CARGO_ENC" runat="server" Width="400px" MaxLength="40" ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_TEL_FUENTE" runat="server" Text="Teléfono Contácto"></asp:Label>
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_TEL_FUENTE" runat="server" Width="400px" MaxLength="40"
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_TEL_FUENTE"
                                            runat="server" TargetControlID="TextBox_TEL_FUENTE" FilterType="Numbers,Custom"
                                            ValidChars="()[]{}- extEXT" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        E-Mail
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_EMAIL_CONTACTO" runat="server" Width="400px" MaxLength="40"
                                            ValidationGroup="GUARDAR"></asp:TextBox>
                                    </td>
                                </tr>

                            </table>

                            <!-- TextBox_NOM_FUENTE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOM_FUENTE"
                                ControlToValidate="TextBox_NOM_FUENTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE del contácto es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOM_FUENTE"
                                TargetControlID="RequiredFieldValidator_TextBox_NOM_FUENTE" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_DIR_FUENTE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DIR_FUENTE"
                                ControlToValidate="TextBox_DIR_FUENTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Dirección del contácto es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DIR_FUENTE"
                                TargetControlID="RequiredFieldValidator_TextBox_DIR_FUENTE" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_ENCARGADO -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_ENCARGADO"
                                ControlToValidate="TextBox_ENCARGADO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El Encargado del contácto es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_ENCARGADO"
                                TargetControlID="RequiredFieldValidator_TextBox_ENCARGADO" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_CARGO_ENC -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CARGO_ENC"
                                ControlToValidate="TextBox_CARGO_ENC" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO DEL CONTACTO es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CARGO_ENC"
                                TargetControlID="RequiredFieldValidator_TextBox_CARGO_ENC" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_EMAIL_CONTACTO -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_EMAIL_CONTACTO"
                                ControlToValidate="TextBox_EMAIL_CONTACTO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El E-MAIL es requerido."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EMAIL_CONTACTO"
                                TargetControlID="RequiredFieldValidator_TextBox_EMAIL_CONTACTO" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_EMAIL_CONTACTO" Display="None" 
                                runat="server" ErrorMessage="<b>E-Mail incorrecto</b><br />El EMAIL tiene un formato incorrecto." 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                ControlToValidate="TextBox_EMAIL_CONTACTO" ValidationGroup="GUARDAR"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_EMAIL_CONTACTO_1"
                                    TargetControlID="RegularExpressionValidator_TextBox_EMAIL_CONTACTO"
                                    HighlightCssClass="validatorCalloutHighlight" />

                            <div class="div_espaciador"></div>
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_REGIONAL" runat="server" Text="Regional"></asp:Label>
                                            </td>
                                            <td colspan="3" class="td_der">
                                                <asp:DropDownList ID="DropDownList_REGIONAL" runat="server" AutoPostBack="True" Width="260px"
                                                    ValidationGroup="GUARDAR" OnSelectedIndexChanged="DropDownList_REGIONAL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_DEPARTAMENTO" runat="server" Text="Departamento"></asp:Label>
                                            </td>
                                            <td colspan="3" class="td_der">
                                                <asp:DropDownList ID="DropDownList_DEPARTAMENTO" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_SelectedIndexChanged" Width="260px"
                                                    ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_CIUDAD" runat="server" Text="Ciudad"></asp:Label>
                                            </td>
                                            <td colspan="3" class="td_der">
                                                <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" Width="260px" ValidationGroup="GUARDAR">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>

                                    <!-- DropDownList_CIUDAD -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD"
                                        ControlToValidate="DropDownList_CIUDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD del contácto es requerida."
                                        ValidationGroup="GUARDAR" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="div_espaciador"></div>

                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <div style="text-align:center">
                                            Observaciones
                                        </div>
                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="822px" TextMode="MultiLine"
                                            ValidationGroup="GUARDAR" Height="103px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>   
                            
                            <!-- TextBox_Observaciones -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Observaciones" ControlToValidate="TextBox_Observaciones"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="GUARDAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Observaciones"
                                TargetControlID="RequiredFieldValidator_TextBox_Observaciones" HighlightCssClass="validatorCalloutHighlight" />                        
                        </div>
                </asp:Panel>

                <asp:Panel ID="Panel_BotonOcultar" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Ocultar Fuente de Reclutamiento
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        
                        <asp:Button ID="Button_OcultarFuente" runat="server" Text="Ocultar" 
                            onclick="Button_OcultarFuente_Click" />
                        
                    </div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel_FONDO_MENSAJE_ABAJO" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES_ABAJO" runat="server">
                <asp:Image ID="Image_MENSAJE_ABAJO_POPUP" runat="server" Width="50px" Height="50px"
                    Style="margin: 5px auto 8px auto; display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_ABAJO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE_ABAJO" runat="server" ForeColor="Red">Este es el 
                    mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE_ABAJO" runat="server" Text="Cerrar" 
                        onclick="Button_CERRAR_MENSAJE_ABAJO_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_CARGOS_FUENTE" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Cargos en Fuente
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_GRILLA_OCUPACIONES_FUENTE" runat="server">
                            <table class="table_control_registros" width="700">
                                <tr>
                                    <td>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_CARGOS_INCLUIDOS" runat="server" Width="700px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_FUENTE,ID_OCUPACION,Registro">
                                                    <Columns>
                                                        <asp:BoundField DataField="Registro" HeaderText="Registro" Visible="False" />
                                                        <asp:BoundField DataField="ID_FUENTE" HeaderText="ID_FUENTE" Visible="False" />
                                                        <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Comentarios" />
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <div class="div_espaciador">
                        </div>
                        
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVO_CARGO" runat="server" Text="Nuevo Cargo" CssClass="margin_botones"
                                        onclick="Button_NUEVO_CARGO_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="Panel_NUEVO_CARGO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Adicionar Cargo en Fuente
                            </div>
                            <div class="div_contenido_groupbox_gris">

                                <div class="div_avisos_importantes" style="width:70%; margin:0 auto;">
                                    Debe realizar la busqueda del cargo. escriba el nombre del cargo, oprima el 
                                    botón BUSCAR, y luego utilice la lista de CARGOS ENCONTRADOS para seleccionar el 
                                    cargo.
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_BUSCADOR_CARGOS">
                                    <ProgressTemplate>
                                        <div style="text-align:center; margin:5px; font-weight:bold;">
                                            Buscando Cargos... Por Favor Espere.
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="UpdatePanel_BUSCADOR_CARGOS" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Buscador
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_BUSCADOR_CARGO" runat="server" ValidationGroup="BUSCADORCARGO"
                                                        Width="220px" MaxLength="15"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_BUSCADOR_CARGO" runat="server" Text="Buscar" CssClass="margin_botones"
                                                        OnClick="Button_BUSCADOR_CARGO_Click" ValidationGroup="BUSCADORCARGO" />
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_BUSCADOR_CARGO -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_BUSCADOR_CARGO" runat="server"
                                            ControlToValidate="TextBox_BUSCADOR_CARGO" Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="BUSCADORCARGO" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_BUSCADOR_CARGO"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_BUSCADOR_CARGO" />

                                        <div class="div_espaciador"></div>

                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    Seleccione un Cargo Encontrado
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList_ID_OCUPACION" runat="server" 
                                                        ValidationGroup="CARGOS">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_ID_OCUPACION -->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_OCUPACION"
                                            runat="server" ControlToValidate="DropDownList_ID_OCUPACION" 
                                            Display="None" ErrorMessage="Campo Requerido faltante</br>El CARGO es requerido."
                                            ValidationGroup="CARGOS" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_OCUPACION"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_OCUPACION" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="div_espaciador">
                                </div>

                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td>
                                            <div style="text-align: center">
                                                Comentario
                                            </div>
                                            <asp:TextBox ID="TextBox_Cargo_Comentario" runat="server" TextMode="MultiLine" ValidationGroup="CARGOS"
                                                Width="794px" Height="79px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!--TextBox_Cargo_Comentario -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Cargo_Comentario"
                                    ControlToValidate="TextBox_Cargo_Comentario" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El COMENTARIO es requerido."
                                    ValidationGroup="CARGOS" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Cargo_Comentario"
                                    TargetControlID="RequiredFieldValidator_TextBox_Cargo_Comentario" HighlightCssClass="validatorCalloutHighlight" />

                                <div class="div_espaciador">
                                </div>

                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_ADICIONAR_CARGO" runat="server" CssClass="margin_botones"
                                                OnClick="Button_ADICIONAR_CARGO_Click" Text="Adicionar" 
                                                ValidationGroup="CARGOS" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_CANCELAR_CARGO" runat="server" Text="Cancelar"  CssClass="margin_botones"
                                                onclick="Button_CANCELAR_CARGO_Click" ValidationGroup="CANCELARCARGOS" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_COMUNICACIONES" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Comunicaciones
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_GRILLA_COMUNICACIONES" runat="server">
                            <table class="table_control_registros" width="700">
                                <tr>
                                    <td>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_COMUNICACION" runat="server" Width="700px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_FUENTE,REGISTRO">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_FUENTE" HeaderText="ID_FUENTE" Visible="False" />
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha comunicación" DataFormatString="{0:dd/M/yyy}" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Comunicación" />
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                        <div class="div_espaciador">
                        </div>

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVA_COMUNICACION" runat="server" CssClass="margin_botones"
                                        Text="Nueva Comunicación" onclick="Button_NUEVA_COMUNICACION_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:Panel ID="Panel_NUEVA_COMUNICACION" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Nueva Comunicación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_fecha" runat="server" Width="110px" ValidationGroup="COMUNICACION"
                                                MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha" runat="server"
                                                TargetControlID="TextBox_fecha" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_fecha-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_fecha"
                                    ControlToValidate="TextBox_fecha" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />la FECHA es requerida."
                                    ValidationGroup="COMUNICACION" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_fecha"
                                    TargetControlID="RequiredFieldValidator_TextBox_fecha" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td>
                                            <div style="text-align: center">
                                                Comunicación
                                            </div>
                                            <asp:TextBox ID="TextBox_Comentarios_comunicacion" runat="server" Width="784px" ValidationGroup="COMUNICACION"
                                                TextMode="MultiLine" Height="73px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!--TextBox_Comentarios -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Comentarios_comunicacion"
                                    ControlToValidate="TextBox_Comentarios_comunicacion" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El comentario es requerido."
                                    ValidationGroup="COMUNICACION" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Comentarios_comunicacion"
                                    TargetControlID="RequiredFieldValidator_TextBox_Comentarios_comunicacion" HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_ADICIONAR_COMUNICACION" runat="server" Text="Adicionar" CssClass="margin_botones"
                                                OnClick="Button_ADICIONAR_COMUNICACION_Click" ValidationGroup="COMUNICACION" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_CANCALAR_COMUNICACION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                onclick="Button_CANCALAR_COMUNICACION_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
