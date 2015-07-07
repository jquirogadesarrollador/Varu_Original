<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="eps.aspx.cs" Inherits="contratacion_eps" %>

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

    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo"  
                                        CssClass="margin_botones" onclick="Button_NUEVO_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="ADICIONAR"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel_busqueda" runat="server">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                onselectedindexchanged="DropDownList_BUSCAR_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="BUSCAR_REFERENCIA" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    ControlToValidate="DropDownList_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda." 
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Numbers" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Letras" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel_MENSAJES">
        <ContentTemplate>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="div_espaciador"></div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_Buscar"/>
        </Triggers>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" 
                                    AllowPaging="True" 
                                    onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="id">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="nit" HeaderText="Nit" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="digito_verificacion" 
                                            HeaderText="Digito verificación" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono"/>
                                        <asp:BoundField DataField="ACTIVO" HeaderText="Activo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>

                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA"/>
                            </Triggers>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Referencias Laborales Personales
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
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:UpdatePanel ID="UpdatePanel_DATOS" runat="server">
                <ContentTemplate>
                <asp:Panel ID="Panel_DATOS" runat="server">
                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox_gris">
                                Información Entidad</div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            
                                        </td>
                                        <td class="td_der">
                                            
                                        </td>

                                        <td class="td_izq">
                                            <asp:Label ID="Label_ID" runat="server" Text="ID"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID" runat="server" Width="250px" 
                                                    MaxLength="255" ValidationGroup="ADICIONAR" ReadOnly ="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_NOM_ENTIDAD" runat="server" Text="Nombre"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NOM_ENTIDAD" runat="server" Width="250px" 
                                                    MaxLength="30" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>

                                        <td class="td_izq">
                                            <asp:Label ID="Label_NIT" runat="server" Text="Nit"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NIT" runat="server" Width="250px" 
                                                    MaxLength="9" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_DV" runat="server" Text="DV"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_DV" runat="server" Width="250px" 
                                                    MaxLength="1" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_COD_ENTIDAD" runat="server" Text="Código"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_COD_ENTIDAD" runat="server" Width="250px" 
                                                    MaxLength="6" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>    
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_DIR_ENTIDAD" runat="server" Text="Dirección"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_DIR_ENTIDAD" runat="server" ValidationGroup="GUARDAR" 
                                                Width="250px" MaxLength="20" ></asp:TextBox>
                                        </td>

                                        <td class="td_izq">
                                            <asp:Label ID="Label_TEL_ENTIDAD" runat="server" Text="Teléfono"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_TEL_ENTIDAD" runat="server" ValidationGroup="GUARDAR" 
                                                Width="250px" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CONTACTO" runat="server" Text="Contactar a"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_CONTACTO" runat="server" ValidationGroup="GUARDAR" 
                                                Width="250px" MaxLength="30"></asp:TextBox>
                                        </td>
                                                    
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CARGO" runat="server" Text="Cargo"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_CARGO" runat="server" Width="250px" 
                                                    MaxLength="20" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:CheckBox ID="CheckBox_ESTADO" runat="server" Text="Activo" 
                                                TextAlign="Left" />
                                        </td>
                                        <td class="td_der">
                                            
                                        </td>
                                                    
                                        <td class="td_izq">
                                        </td>
                                        <td class="td_der">
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_NOM_ENTIDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NOM_ENTIDAD"
                                    ControlToValidate="TextBox_NOM_ENTIDAD"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_NOM_ENTIDAD"
                                    TargetControlID="RequiredFieldValidator_NOM_ENTIDAD"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_DV -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DV"
                                    ControlToValidate="TextBox_DV"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El DIGITO DE VERIFICACION es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DV"
                                    TargetControlID="RequiredFieldValidator_DV"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DV" runat="server" TargetControlID="TextBox_DV" FilterType="Numbers" />

                                <!-- TextBox_COD_ENTIDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_COD_ENTIDAD"
                                    ControlToValidate="TextBox_COD_ENTIDAD"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El CODIGO ENTIDAD es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_COD_ENTIDAD"
                                    TargetControlID="RequiredFieldValidator_COD_ENTIDAD"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_NIT -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NIT"
                                    ControlToValidate="TextBox_NIT"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El NIT es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                    TargetControlID="RequiredFieldValidator_NIT"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NIT" runat="server" TargetControlID="TextBox_NIT" FilterType="Numbers" />

                                <!-- TextBox_DIR_ENTIDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DIR_ENTIDAD"
                                    ControlToValidate="TextBox_DIR_ENTIDAD"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La DIRECCCION es requerida."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DIR_ENTIDAD"
                                    TargetControlID="RequiredFieldValidator_DIR_ENTIDAD"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                
                                <!-- TextBox_TEL_ENTIDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TEL_ENTIDAD"
                                    ControlToValidate="TextBox_TEL_ENTIDAD"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TEL_ENTIDAD"
                                    TargetControlID="RequiredFieldValidator_TEL_ENTIDAD"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                
                                <!-- TextBox_CONTACTO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CONTACTO"
                                    ControlToValidate="TextBox_CONTACTO"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El CONTACTO es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CONTACTO"
                                    TargetControlID="RequiredFieldValidator_CONTACTO"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                
                                <!-- TextBox_CARGO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CARGO"
                                    ControlToValidate="TextBox_CARGO"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO es requerido."
                                    ValidationGroup="ADICIONAR"/>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CARGO"
                                    TargetControlID="RequiredFieldValidator_CARGO"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <div class="div_espaciador"></div>
                                            
                            </div>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
<%--                <Triggers>
                    <asp:PostBackTrigger ControlID="DropDownList_regional" />
                    <asp:PostBackTrigger ControlID="DropDownList_regional" />
                </Triggers>
--%>                </asp:UpdatePanel>
                
                <div class="div_espaciador"></div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                               <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="ADICIONAR"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>