<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="condicionesEconomicasSertempoPorIdEspecifico.aspx.cs" Inherits="_Default" %>

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
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" style="text-align:center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
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
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" 
                                        ValidationGroup="CREAR" onclick="Button_GUARDAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_VOLVER_MENU_EMPRESA" runat="server" Text="Volver a la empresa"  
                                        CssClass="margin_botones" ValidationGroup="VOLVER" 
                                        onclick="Button_VOLVER_MENU_EMPRESA_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" 
                                        type="button" value="Salir" onclick="window.close();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Condiciones económicas generales de nómina
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






                <asp:Panel ID="Panel_COD_CONDICIONES" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Código de identificación
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_COD_EMPRESA" runat="server" Text="Código"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_COD_CONDICION" runat="server" 
                                                ReadOnly="True" ValidationGroup="CODIGO"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <table class="table_control_registros">
                    <tr>
                        <td>
                            Factura nómina
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox_FACTURA_NOMINA" runat="server" Text=" " />
                        </td>
                        <td>
                            Modelo de soporte
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_MOD_SOPORTE" runat="server" 
                                ValidationGroup="CREAR">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Modelo de Factura
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_MOD_FACTURA" runat="server" 
                                ValidationGroup="CREAR">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <!-- DropDownList_MOD_SOPORTE -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_MOD_SOPORTE"
                        ControlToValidate="DropDownList_MOD_SOPORTE"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MODELO DE SOPORTE es requerido." 
                        ValidationGroup="CREAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_MOD_SOPORTE"
                        TargetControlID="RequiredFieldValidator_DropDownList_MOD_SOPORTE"
                        HighlightCssClass="validatorCalloutHighlight" />

                <!-- DropDownList_MOD_FACTURA -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_MOD_FACTURA"
                        ControlToValidate="DropDownList_MOD_FACTURA"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MODELO DE FACTURA es requerido." 
                        ValidationGroup="CREAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_MOD_FACTURA"
                        TargetControlID="RequiredFieldValidator_DropDownList_MOD_FACTURA"
                        HighlightCssClass="validatorCalloutHighlight" />

                <table class="table_control_registros">
                    <tr>
                        <td>
                            AIU (%)
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_AD_NOM" runat="server" Width="80px" 
                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                        </td>
                        <td>
                            Admin solo Dev
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox_SOLO_DEV" runat="server" Text=" " />
                        </td>
                    </tr>
                </table>
                <!-- TextBox_AD_NOM -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_NOM"
                        ControlToValidate="TextBox_AD_NOM"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON NÓMINA es requerido." 
                        ValidationGroup="CREAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_NOM"
                        TargetControlID="RequiredFieldValidator_TextBox_AD_NOM"
                        HighlightCssClass="validatorCalloutHighlight" />
                <ajaxToolkit:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender_TextBox_AD_NOM" runat="server" 
                        TargetControlID="TextBox_AD_NOM" FilterType="Numbers,Custom" ValidChars=","/>
                <asp:RangeValidator ID="RangeValidator_TextBox_AD_NOM" runat="server" 
                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON NÓMINA no tiene formato correcto." 
                    MaximumValue="100" MinimumValue="0" Type="Double" 
                    ControlToValidate="TextBox_AD_NOM" Display="None" ValidationGroup="CREAR"></asp:RangeValidator>
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_NOM_1"
                        TargetControlID="RangeValidator_TextBox_AD_NOM"
                        HighlightCssClass="validatorCalloutHighlight" />
                <div class="div_espaciador"></div>
                <table class="table_form_dos_columnas" cellpadding="0" cellspacing="0" width="884">
                    <tr>
                        <td valign="top" style="width:50%">
                            <div class="div_cabeza_groupbox_gris">
                                <asp:Label ID="Label_CONDICIONES_1" runat="server" Text="Condiciones económicas"></asp:Label>
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_condiciones" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            %</td>
                                        <td style=" width:70px;">
                                            Incluye subsidio Trans.
                                        </td>
                                        <td style=" width:60px;">
                                            Se cobra al retiro
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Pensión
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_PENSION" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_PENSION" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Salud
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_SALUD" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_SALUD" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Riesgo prof.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_RIESGOS" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_RIESGO" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Aportes Sena
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_APO_SENA" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_SENA" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Aportes ICBF
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_APO_ICBF" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_ICBF" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Aportes Caja
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_APO_CAJA" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_CAJA" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Vacaciones
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_VACACIONES" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_VACACIONES" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_RET_VAC" runat="server" Text=" " />
                                        </td>
                                    </tr>
                                </table>
                                <!-- TextBox_AD_PENSION -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_PENSION"
                                        ControlToValidate="TextBox_AD_PENSION"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON PENSIÓN es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_PENSION"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_PENSION"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_PENSION" runat="server" 
                                        TargetControlID="TextBox_AD_PENSION" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_PENSION" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON PENSIÓN no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_PENSION" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_PENSION_1"
                                        TargetControlID="RangeValidator_TextBox_AD_PENSION"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_SALUD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_SALUD"
                                        ControlToValidate="TextBox_AD_SALUD"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON SALUD es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_SALUD"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_SALUD"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_SALUD" runat="server" 
                                        TargetControlID="TextBox_AD_SALUD" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_SALUD" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON SALUD no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_SALUD" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_SALUD_1"
                                        TargetControlID="RangeValidator_TextBox_AD_SALUD"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_RIESGOS -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_RIESGOS"
                                        ControlToValidate="TextBox_AD_RIESGOS"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON RIESGOS es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_RIESGOS"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_RIESGOS"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_RIESGOS" runat="server" 
                                        TargetControlID="TextBox_AD_RIESGOS" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_RIESGOS" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON RIESGOS no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_RIESGOS" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_RIESGOS_2"
                                        TargetControlID="RangeValidator_TextBox_AD_RIESGOS"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_APO_SENA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_APO_SENA"
                                        ControlToValidate="TextBox_AD_APO_SENA"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON SENA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_SENA"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_APO_SENA"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_APO_SENA" runat="server" 
                                        TargetControlID="TextBox_AD_APO_SENA" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_APO_SENA" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON SENA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_APO_SENA" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_SENA_2"
                                        TargetControlID="RangeValidator_TextBox_AD_APO_SENA"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_APO_ICBF -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_APO_ICBF"
                                        ControlToValidate="TextBox_AD_APO_ICBF"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON ICBF es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_ICBF"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_APO_ICBF"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_APO_ICBF" runat="server" 
                                        TargetControlID="TextBox_AD_APO_ICBF" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_APO_ICBF" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON ICBF no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_APO_ICBF" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_ICBF_2"
                                        TargetControlID="RangeValidator_TextBox_AD_APO_ICBF"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_APO_CAJA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_APO_CAJA"
                                        ControlToValidate="TextBox_AD_APO_CAJA"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON CAJA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_CAJA"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_APO_CAJA"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_APO_CAJA" runat="server" 
                                        TargetControlID="TextBox_AD_APO_CAJA" FilterType="Numbers,Custom" ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_APO_CAJA" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON CAJA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                    ControlToValidate="TextBox_AD_APO_CAJA" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_APO_CAJA_2"
                                        TargetControlID="RangeValidator_TextBox_AD_APO_CAJA"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_VACACIONES -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_VACACIONES"
                                        ControlToValidate="TextBox_AD_VACACIONES"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON VACACIONES es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_VACACIONES"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_VACACIONES"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_VACACIONES" runat="server" 
                                        TargetControlID="TextBox_AD_VACACIONES" FilterType="Numbers,Custom" 
                                    ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_VACACIONES" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON VACACIONES no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0,00" Type="Double" 
                                    ControlToValidate="TextBox_AD_VACACIONES" Display="None" 
                                    ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_VACACIONES_2"
                                        TargetControlID="RangeValidator_TextBox_AD_VACACIONES"
                                        HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </td>
                        <td valign="top" style="width:50%">
                            <div class="div_cabeza_groupbox_gris">
                                <asp:Label ID="Label_CONDICIONES_2" runat="server" Text="Condiciones económicas"></asp:Label>
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_condiciones" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            %
                                        </td>
                                        <td style=" width:70px;">
                                            Incluye subsidio Trans.
                                        </td>
                                        <td style=" width:60px;">
                                            Se cobra al retiro
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cesantía
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_CESANTIA" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_CESANTIAS" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_RET_CES" runat="server" Text=" " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Int Cesantía
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_INT_CES" runat="server" 
                                                Width="50px" ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_INT_CES" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_RET_INT_CES" runat="server" Text=" " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Prima
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_PRIMA" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_PRIMA" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_RET_PRIM" runat="server" Text=" " />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Seguro vida
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_AD_SEG_VID" runat="server" Width="50px" 
                                                ValidationGroup="CREAR" MaxLength="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_SUB_SEG_VID" runat="server" Text=" " />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <!-- TextBox_AD_CESANTIA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_CESANTIA"
                                        ControlToValidate="TextBox_AD_CESANTIA"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON CESANTIA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_CESANTIA"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_CESANTIA"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_CESANTIA" runat="server" 
                                        TargetControlID="TextBox_AD_CESANTIA" FilterType="Numbers,Custom" 
                                    ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_CESANTIA" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON CESANTIA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0" Type="Double" 
                                    ControlToValidate="TextBox_AD_CESANTIA" Display="None" ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_CESANTIA_2"
                                        TargetControlID="RangeValidator_TextBox_AD_CESANTIA"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_INT_CES -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_INT_CES"
                                        ControlToValidate="TextBox_AD_INT_CES"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON INT CESANTIA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_INT_CES"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_INT_CES"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_INT_CES" runat="server" 
                                        TargetControlID="TextBox_AD_INT_CES" FilterType="Numbers,Custom" 
                                    ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_INT_CES" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON INT CESANTIA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0" Type="Double" 
                                    ControlToValidate="TextBox_AD_INT_CES" Display="None" ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_INT_CES_1"
                                        TargetControlID="RangeValidator_TextBox_AD_INT_CES"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_PRIMA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_PRIMA"
                                        ControlToValidate="TextBox_AD_PRIMA"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON PRIMA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_PRIMA"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_PRIMA"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_PRIMA" runat="server" 
                                        TargetControlID="TextBox_AD_PRIMA" FilterType="Numbers,Custom" 
                                    ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_PRIMA" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON PRIMA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0" Type="Double" 
                                    ControlToValidate="TextBox_AD_PRIMA" Display="None" ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_PRIMA_1"
                                        TargetControlID="RangeValidator_TextBox_AD_PRIMA"
                                        HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_AD_SEG_VID -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_AD_SEG_VID"
                                        ControlToValidate="TextBox_AD_SEG_VID"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El P. ADMON SEGURO VIDA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_SEG_VID"
                                        TargetControlID="RequiredFieldValidator_TextBox_AD_SEG_VID"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_AD_SEG_VID" runat="server" 
                                        TargetControlID="TextBox_AD_SEG_VID" FilterType="Numbers,Custom" 
                                    ValidChars=","/>
                                <asp:RangeValidator ID="RangeValidator_TextBox_AD_SEG_VID" runat="server" 
                                    ErrorMessage="<b>Datos erroneos</b><br />El P. ADMON SEGURO VIDA no tiene formato correcto." 
                                    MaximumValue="100" MinimumValue="0" Type="Double" 
                                    ControlToValidate="TextBox_AD_SEG_VID" Display="None" ValidationGroup="CREAR"></asp:RangeValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_AD_SEG_VID_1"
                                        TargetControlID="RangeValidator_TextBox_AD_SEG_VID"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <div class="div_espaciador"></div>
                                <div class="div_espaciador"></div>
                                <div class="div_espaciador"></div>
                                <table style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            Regimen
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_REGIMEN" runat="server" 
                                                ValidationGroup="CREAR">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Plazo factura
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_DIAS_VNC" runat="server" Width="60px" 
                                                ValidationGroup="CREAR" MaxLength="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_REGIMEN -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_REGIMEN"
                                        ControlToValidate="DropDownList_REGIMEN"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El REGIMEN es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_REGIMEN"
                                        TargetControlID="RequiredFieldValidator_DropDownList_REGIMEN"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                
                                <!-- TextBox_DIAS_VNC -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DIAS_VNC"
                                        ControlToValidate="TextBox_DIAS_VNC"
                                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PLAZO FACTURA es requerido." 
                                        ValidationGroup="CREAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DIAS_VNC"
                                        TargetControlID="RequiredFieldValidator_TextBox_DIAS_VNC"
                                        HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender_TextBox_DIAS_VNC" runat="server" 
                                        TargetControlID="TextBox_DIAS_VNC" FilterType="Numbers" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador"></div>
                <div class="div_cabeza_groupbox_gris">
                    Condiciones especiales de facturación
                </div>
                <div class="div_contenido_groupbox_gris">
                    <asp:TextBox ID="TextBox_OBS_FACT" runat="server" Height="65px" TextMode="MultiLine" 
                        Width="743px" ValidationGroup="CREAR"></asp:TextBox>
                </div>
                <!-- TextBox_OBS_FACT -->
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_FACT"
                        ControlToValidate="TextBox_OBS_FACT"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las CONDICIONES ESPECIALES son requeridas." 
                        ValidationGroup="CREAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_FACT"
                        TargetControlID="RequiredFieldValidator_TextBox_OBS_FACT"
                        HighlightCssClass="validatorCalloutHighlight" />--%>

                <div class="div_espaciador"></div>
                <asp:UpdatePanel ID="UpdatePanel_SERVICIOS_COMPLEMENTARIOS" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:CheckBox ID="CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO" runat="server" 
                            oncheckedchanged="CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO_CheckedChanged" 
                            Text="Empresa (indetermina) servicios complementarios " 
                            AutoPostBack="True" />
                        <asp:Panel ID="Panel_SERVICIOS_COMPLEMENTARIOS" runat="server">
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Servicios Complementarios
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:Panel ID="Panel_SERVICIOS_COMPLEMENTARIOS_ADICIONAR" runat="server">


                                    

                                    <asp:Panel ID="Panel_FONDO_MENSAJE_SERVICIO_COMPLEMENTARIO" runat="server" Visible="false" Style="background-color: #999999;">
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_MENSAJE_SERVICIO_COMPLEMENTARIO" runat="server">
                                        <asp:Image ID="Image_MENSAJE_SERVICIO_COMPLEMENTARIO_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                                            display: block;" />
                                        <asp:Panel ID="Panel_COLOR_MENSAJE_SERVICIO_COMPLEMENTARIO" runat="server" Style="text-align: center">
                                            <asp:Label ID="Label_MENSAJE_SERVICIO_COMPLEMENTARIO" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                                        </asp:Panel>
                                        <div style="text-align: center; margin-top: 15px;">
                                            <asp:Button ID="Button_MENSAJE_SERVICIO_COMPLEMENTARIO" runat="server" 
                                                Text="Cerrar" onclick="Button_MENSAJE_SERVICIO_COMPLEMENTARIO_Click" />
                                        </div>
                                    </asp:Panel>






                                    <table class="table_control_registros" cellpadding="0">
                                        <tr>
                                            <td class="td_izq">
                                                Servicio
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_SERVICIOS_COMPLEMENTARIOS" runat="server" 
                                                    ValidationGroup="SERVICIOCOMP">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_izq">
                                                Tipo de configuración
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CONFIGURACION" runat="server" 
                                                    ValidationGroup="SERVICIOCOMP" AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownList_CONFIGURACION_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_CONFIGURACION -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CONFIGURACION"
                                        ControlToValidate="DropDownList_CONFIGURACION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CONFIGURACIÓN es requerido."
                                        ValidationGroup="SERVICIOCOMP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CONFIGURACION"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CONFIGURACION" HighlightCssClass="validatorCalloutHighlight" />

                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros" cellpadding="0">
                                        <tr>

                                            <td class="td_izq">
                                                % Admon
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_SER_ADMON" runat="server" Width="60px" 
                                                    ValidationGroup="SERVICIOCOMP" MaxLength="9"></asp:TextBox>
                                            </td>
                                            <td class="td_izq">
                                                IVA
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_SER_IVA" runat="server" Width="60px" 
                                                    ValidationGroup="SERVICIOCOMP" MaxLength="9"></asp:TextBox>
                                            </td>
                                            <td class="td_izq">
                                                Valor
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_SER_VALOR" runat="server" Width="80px" 
                                                    ValidationGroup="SERVICIOCOMP" MaxLength="18"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_ADICIONAR_SERVICIO_COMPLEMENTARIO" runat="server" 
                                                    Text="Adicionar" CssClass="margin_botones" 
                                                    onclick="Button_ADICIONAR_SERVICIO_COMPLEMENTARIO_Click" 
                                                    ValidationGroup="SERVICIOCOMP" />
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_SERVICIOS_COMPLEMENTARIOS -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                            ControlToValidate="DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SERVICIO es requerido." 
                                            ValidationGroup="SERVICIOCOMP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                            TargetControlID="RequiredFieldValidator_DropDownList_SERVICIOS_COMPLEMENTARIOS"
                                            HighlightCssClass="validatorCalloutHighlight" />

                                    <!-- TextBox_SER_ADMON -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SER_ADMON"
                                            ControlToValidate="TextBox_SER_ADMON"
                                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El % ADMON es requerido." 
                                            ValidationGroup="SERVICIOCOMP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SER_ADMON"
                                            TargetControlID="RequiredFieldValidator_TextBox_SER_ADMON"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                    <%--<asp:RangeValidator ID="RangeValidator_TextBox_SER_ADMON" runat="server" 
                                        ErrorMessage="<b>Datos erroneos</b><br />El % ADMON no tiene formato correcto." 
                                        MaximumValue="100" MinimumValue="0" Type="Double" 
                                        ControlToValidate="TextBox_SER_ADMON" Display="None" ValidationGroup="SERVICIOCOMP"></asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SER_ADMON_1"
                                            TargetControlID="RangeValidator_TextBox_SER_ADMON"
                                            HighlightCssClass="validatorCalloutHighlight" />--%>
                                    <ajaxToolkit:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender_TextBox_SER_ADMON" runat="server" 
                                            TargetControlID="TextBox_SER_ADMON" FilterType="Numbers,Custom" ValidChars=","/>

                                    <!-- TextBox_SER_IVA -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SER_IVA"
                                            ControlToValidate="TextBox_SER_IVA"
                                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El IVA es requerido." 
                                            ValidationGroup="SERVICIOCOMP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SER_IVA"
                                            TargetControlID="RequiredFieldValidator_TextBox_SER_IVA"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                    <asp:RangeValidator ID="RangeValidator_TextBox_SER_IVA" runat="server" 
                                        ErrorMessage="<b>Datos erroneos</b><br />El IVA no tiene formato correcto." 
                                        MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                        ControlToValidate="TextBox_SER_IVA" Display="None" ValidationGroup="SERVICIOCOMP"></asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SER_IVA_1"
                                            TargetControlID="RangeValidator_TextBox_SER_IVA"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                    <ajaxToolkit:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender_TextBox_SER_IVA" runat="server" 
                                            TargetControlID="TextBox_SER_IVA" FilterType="Numbers,Custom" ValidChars=","/>

                                    <!-- TextBox_SER_VALOR -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SER_VALOR"
                                            ControlToValidate="TextBox_SER_VALOR"
                                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El VALOR es requerido." 
                                            ValidationGroup="SERVICIOCOMP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SER_VALOR"
                                            TargetControlID="RequiredFieldValidator_TextBox_SER_VALOR"
                                            HighlightCssClass="validatorCalloutHighlight" />
                                    <ajaxToolkit:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender_TextBox_SER_VALOR" runat="server" 
                                            TargetControlID="TextBox_SER_VALOR" FilterType="Numbers"/>

                                </asp:Panel>
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros" width="700">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_SERVICIOS_INCLUIDOS" runat="server" Width="700px"  
                                                        AutoGenerateColumns="False" 
                                                        DataKeyNames="ID_SERVICIO,ID_SERVICIO_COMPLEMENTARIO" 
                                                
                                                        onselectedindexchanged="GridView_SERVICIOS_INCLUIDOS_SelectedIndexChanged" >
                                                        <Columns>
                                                            <asp:CommandField HeaderText="Acción" SelectText="Eliminar" 
                                                                ShowSelectButton="True" ButtonType="Image" 
                                                                SelectImageUrl="~/imagenes/plantilla/delete.gif" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="ID_SERVICIO" HeaderText="ID_SERVICIO" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ID_SERVICIO_COMPLEMENTARIO" 
                                                                HeaderText="ID_SERVICIO_COMPLEMENTARIO" Visible="False" />
                                                            <asp:BoundField DataField="NOMBRE_SERVICIO_COMPLEMENTARIO" 
                                                                HeaderText="Nombre Servicio Complementario" />
                                                            <asp:BoundField DataField="AIU" HeaderText="AIU" DataFormatString="{0:P2}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IVA" HeaderText="IVA" DataFormatString="{0:P2}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR" HeaderText="VALOR" 
                                                                DataFormatString="{0:C}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:Panel ID="Panel_FORM_BOTONES_1" runat="server">
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
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" 
                                        ValidationGroup="CREAR" onclick="Button_GUARDAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_VOLVER_MENU_EMPRESA_1" runat="server" Text="Volver a la empresa"  
                                        CssClass="margin_botones" ValidationGroup="VOLVER" 
                                        onclick="Button_VOLVER_MENU_EMPRESA_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR_1" 
                                        type="button" value="Salir" onclick="window.close();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="div_espaciador"></div>
    </asp:Panel>
</asp:Content>

