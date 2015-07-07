<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="condicionesEconomicasEficiencia.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>

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
                                    <asp:Button ID="Button_VOLVER" runat="server" Text="Volver a la empresa"  
                                        CssClass="margin_botones" ValidationGroup="VOLVER" 
                                        onclick="Button_VOLVER_Click"/>
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

    <asp:Panel ID="Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Replicar condiciones de Grupo Empresarial
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            Seleccionar Empresa del Grupo
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_EMPRESAS_DEL_GRUPO" runat="server" 
                                Width="260px" ValidationGroup="REPLICAR">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button_COPIAR_CONDICIONES_GRUPO" runat="server" Text="Replicar" 
                                CssClass="margin_botones" ValidationGroup="REPLICAR" 
                                onclick="Button_COPIAR_CONDICIONES_GRUPO_Click" />
                        </td>
                    </tr>
                </table>
                <!-- DropDownList_EMPRESAS_DEL_GRUPO -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_EMPRESAS_DEL_GRUPO"
                        ControlToValidate="DropDownList_EMPRESAS_DEL_GRUPO"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA es requerida." 
                        ValidationGroup="REPLICAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_EMPRESAS_DEL_GRUPO"
                        TargetControlID="RequiredFieldValidator_DropDownList_EMPRESAS_DEL_GRUPO"
                        HighlightCssClass="validatorCalloutHighlight" />
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Condiciones económicas generales de nómina
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
                            AIU (%)</td>
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
                        <td>
                            Aplica monetización
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox_APL_MTZ" runat="server" Text=" " />
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
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:Panel ID="Panel_SECCION_SERVICIOS" runat="server">
        <asp:UpdatePanel ID="UpdatePanel_SERVICIOS" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="div_contenedor_formulario">
                <asp:Panel ID="Panel_SERVICIOS_ACTUALES_GENERAL" runat="server">
                    <div class="div_cabeza_groupbox">
                        Servicios incluidos
                    </div>
                    <div class="div_contenido_groupbox">

                        <asp:Panel ID="Panel_FONDO_MENSAJES_SERVICIOS_ACTUALES" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_SERVICIOS_ACTUALES" runat="server">
                            <asp:Image ID="Image_MENSAJES_SERVICIOS_ACTUALES_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_MENSAJES_SERVICIOS_ACTUALES" runat="server" style="text-align:center">
                                <asp:Label ID="Label_MENSAJES_SERVICIOS_ACTUALES" runat="server" ForeColor="Red" >Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJES_SERVICIOS_ACTUALES" runat="server" 
                                    Text="Cerrar" style="height: 26px" 
                                    onclick="Button_CERRAR_MENSAJES_SERVICIOS_ACTUALES_Click" />
                            </div>
                        </asp:Panel>

                        
                        <asp:Panel ID="Panel_GRID_SERVICIOS_ACTUALES" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Lista de servicios
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros" width="700">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_SERVICIOS_INCLUIDOS" runat="server" Width="700px"  
                                                        AutoGenerateColumns="False" 
                                                
                                                        DataKeyNames="ID_EMPRESA,ID_SERVICIO,ID_SERVICIO_POR_EMPRESA" 
                                                        onrowcommand="GridView_SERVICIOS_INCLUIDOS_RowCommand" 
                                                        onselectedindexchanged="GridView_SERVICIOS_INCLUIDOS_SelectedIndexChanged" >
                                                        <Columns>
                                                            <asp:CommandField SelectText="" 
                                                                ShowSelectButton="True" ButtonType="Image" 
                                                                SelectImageUrl="~/imagenes/plantilla/delete.gif" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:CommandField>
                                                            <asp:ButtonField CommandName="seleccionar" ButtonType="Image" 
                                                                ImageUrl="~/imagenes/plantilla/view2.gif" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ID_SERVICIO" HeaderText="ID_SERVICIO" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ID_SERVICIO_POR_EMPRESA" 
                                                                HeaderText="ID_SERVICIO_POR_EMPRESA" Visible="False" />
                                                            <asp:BoundField DataField="NOMBRE_SERVICIO" HeaderText="Nombre Servicio" />
                                                            <asp:BoundField DataField="AIU" HeaderText="AIU" DataFormatString="{0:P2}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IVA" HeaderText="IVA" DataFormatString="{0:P2}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VALOR" HeaderText="TARIFA" 
                                                                DataFormatString="{0:C}" >
                                                            <ItemStyle CssClass="columna_grid_der" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="observaciones" HeaderText="DESCRIPCIÓN" >
                                                            <ItemStyle CssClass="columna_grid_jus" />
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
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_GRID_DETALLES_SERVICIO_SELECCIONADO" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Lista de detalles del Servicio: <asp:Label ID="Label_SERVICIO_SELECCIONADO" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros" width="500">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_DETALLES_SERVICIO_SELECCIONADO" runat="server" Width="500px"  
                                                        AutoGenerateColumns="False" 
                                                        DataKeyNames="ID_SERVICIO,NOMBRE_SERVICIO,ID_SERVICIO_COMPLEMENTARIO" 
                                                        onselectedindexchanged="GridView_DETALLES_SERVICIO_SELECCIONADO_SelectedIndexChanged" >
                                                        <Columns>
                                                            <asp:CommandField SelectText="" 
                                                                ShowSelectButton="True" ButtonType="Image" 
                                                                SelectImageUrl="~/imagenes/plantilla/delete.gif" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="ID_SERVICIO" HeaderText="ID_SERVICIO" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="NOMBRE_SERVICIO" HeaderText="NOMBRE_SERVICIO" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="ID_SERVICIO_COMPLEMENTARIO" 
                                                                HeaderText="ID_SERVICIO_COMPLEMENTARIO" Visible="False" />
                                                            <asp:BoundField DataField="NOMBRE_SERVICIO_COMPLEMENTARIO" 
                                                                HeaderText="Nombre del detalle" >
                                                            <ItemStyle CssClass="columna_grid_jus" />
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
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:Panel ID="Panel_BOTON_NUEVO_SERVICIO" runat="server">
                    <div class="div_cabeza_groupbox">
                        Crear nuevo servicio
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVO_SERVICIO" runat="server" Text="Nuevo Servicio" 
                                        CssClass="margin_botones" onclick="Button_NUEVO_SERVICIO_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:Panel ID="Panel_NUEVO_SERVICIO" runat="server">
                    <div class="div_cabeza_groupbox">
                        Incluir nuevo servicio
                    </div>    
                    <div class="div_contenido_groupbox">

                        
                        <asp:Panel ID="Panel_FONDO_MENSAJES_NUEVO_SERVICIO" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_NUEVO_SERVICIO" runat="server">
                            <asp:Image ID="Image_MENSAJES_NUEVO_SERVICIO_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_MENSAJES_NUEVO_SERVICIO" runat="server" style="text-align:center">
                                <asp:Label ID="Label_MENSAJES_NUEVO_SERVICIO" runat="server" ForeColor="Red" >Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_MENSAJES_NUEVO_SERVICIO" runat="server" 
                                    CssClass="margin_botones" Text="Cerrar" 
                                    onclick="Button_MENSAJES_NUEVO_SERVICIO_Click" />
                            </div>
                        </asp:Panel>



                        <asp:Panel ID="Panel_NUEVO_SERVICIO_ADICIONAR_SERVICIO" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Nombre servicio:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_NOMBRE_NUEVO_SERVICIO" runat="server" 
                                            ValidationGroup="CONFIGURAR"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        Tipo de configuración
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_CONFIGURACION" runat="server" 
                                            ValidationGroup="CONFIGURAR" AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_CONFIGURACION_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_CONFIGURACION -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CONFIGURACION"
                                ControlToValidate="DropDownList_CONFIGURACION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CONFIGURACIÓN es requerido."
                                ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CONFIGURACION"
                                TargetControlID="RequiredFieldValidator_DropDownList_CONFIGURACION" HighlightCssClass="validatorCalloutHighlight" />

                            <div class="div_espaciador"></div>

                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        % Admon
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_NUEVO_AIU" runat="server" Width="60px" ValidationGroup="CONFIGURAR"
                                            MaxLength="9"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        IVA
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_NUEVO_IVA" runat="server" Width="60px" ValidationGroup="CONFIGURAR"
                                            MaxLength="9"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        Tarifa
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_NUEVA_TARIFA" runat="server" Width="80px" ValidationGroup="CONFIGURAR"
                                            MaxLength="18"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_COMPROBAR_NUEVO_SERVICIO" runat="server" Text="Configurar"
                                            ValidationGroup="CONFIGURAR" OnClick="Button_COMPROBAR_NUEVO_SERVICIO_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Descripción
                                    </td>
                                    <td class="td_der" colspan="8">
                                        <asp:TextBox ID="TextBox_OBSERVACIONES" runat="server" Width="100%" ValidationGroup="CONFIGURAR"
                                            MaxLength="250" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <!-- TextBox_NOMBRE_NUEVO_SERVICIO -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRE_NUEVO_SERVICIO"
                                ControlToValidate="TextBox_NOMBRE_NUEVO_SERVICIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE NUEVO SERVICIO es requerido."
                                ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRE_NUEVO_SERVICIO"
                                TargetControlID="RequiredFieldValidator_TextBox_NOMBRE_NUEVO_SERVICIO" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_NUEVO_AIU -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NUEVO_AIU"
                                    ControlToValidate="TextBox_NUEVO_AIU"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El % ADMON es requerido." 
                                    ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVO_AIU"
                                    TargetControlID="RequiredFieldValidator_TextBox_NUEVO_AIU"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <%--<asp:RangeValidator ID="RangeValidator_TextBox_NUEVO_AIU" runat="server" 
                                ErrorMessage="<b>Datos erroneos</b><br />El % ADMON no tiene formato correcto." 
                                MaximumValue="100" MinimumValue="0,01" Type="Double" 
                                ControlToValidate="TextBox_NUEVO_AIU" Display="None" ValidationGroup="CONFIGURAR"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVO_AIU_1"
                                    TargetControlID="RangeValidator_TextBox_NUEVO_AIU"
                                    HighlightCssClass="validatorCalloutHighlight" />--%>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUEVO_AIU"
                                runat="server" TargetControlID="TextBox_NUEVO_AIU" FilterType="Numbers,Custom"
                                ValidChars="," />

                            <!-- TextBox_NUEVO_IVA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NUEVO_IVA"
                                ControlToValidate="TextBox_NUEVO_IVA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El IVA es requerido."
                                ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVO_IVA"
                                TargetControlID="RequiredFieldValidator_TextBox_NUEVO_IVA" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RangeValidator ID="RangeValidator_TextBox_NUEVO_IVA" runat="server" ErrorMessage="<b>Datos erroneos</b><br />El IVA no tiene formato correcto."
                                MaximumValue="100" MinimumValue="0,01" Type="Double" ControlToValidate="TextBox_NUEVO_IVA"
                                Display="None" ValidationGroup="CONFIGURAR"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVO_IVA_1"
                                TargetControlID="RangeValidator_TextBox_NUEVO_IVA" HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUEVO_IVA"
                                runat="server" TargetControlID="TextBox_NUEVO_IVA" FilterType="Numbers,Custom"
                                ValidChars="," />
                            <!-- TextBox_NUEVA_TARIFA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NUEVA_TARIFA"
                                    ControlToValidate="TextBox_NUEVA_TARIFA"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La TARIFA es requerida." 
                                    ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVA_TARIFA"
                                    TargetControlID="RequiredFieldValidator_TextBox_NUEVA_TARIFA"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <%--<asp:RangeValidator ID="RangeValidator_TextBox_NUEVA_TARIFA" runat="server" 
                                ErrorMessage="<b>Datos erroneos</b><br />La TARIFA no puede ser 0 'cero'." 
                                MaximumValue="1000000000" MinimumValue="1" Type="Integer"
                                ControlToValidate="TextBox_NUEVA_TARIFA" Display="None" ValidationGroup="CONFIGURAR"></asp:RangeValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUEVA_TARIFA_1"
                                    TargetControlID="RangeValidator_TextBox_NUEVA_TARIFA"
                                    HighlightCssClass="validatorCalloutHighlight" />--%>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUEVA_TARIFA"
                                runat="server" TargetControlID="TextBox_NUEVA_TARIFA" FilterType="Numbers" />

                            <!-- TextBox_OBSERVACIONES -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBSERVACIONES"
                                ControlToValidate="TextBox_OBSERVACIONES" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La DESCRIPCIÓN es requerida."
                                ValidationGroup="CONFIGURAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBSERVACIONES"
                                TargetControlID="RequiredFieldValidator_TextBox_OBSERVACIONES" HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>
                        <asp:Panel ID="Panel_NUEVO_SERVICIO_SERVICIO_ADICINADO" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Nombre servicio:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_NOMBRE_NUEVO_SERVICIO" runat="server" Text="NOMBRE" 
                                            Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </td>
                                    <td class="td_izq">
                                        % Admon
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_AIU_NUEVO_SERVICIO" runat="server" Text="AIU" 
                                            Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </td>
                                    <td class="td_izq">
                                        IVA
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_IVA_NUEVO_SERVICIO" runat="server" Text="IVA" 
                                            Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </td>
                                    <td class="td_izq">
                                        Tarifa
                                    </td>
                                    <td>
                                        <asp:Label ID="Label_TARIFA_NUEVO_SERVICIO" runat="server" Text="TARIFA" 
                                            Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Descripción
                                    </td>
                                    <td class="td_der" colspan="8">
                                        <asp:Label ID="Label_OBSERVACIONES" runat="server" Text="DESCRIPCION" 
                                            Font-Bold="True" ForeColor="#000099" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_NUEVO_SERVICIO_INGRESAR_DETALLES" runat="server">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Detalle del servicio
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_DETALLES_SERVICIO" runat="server" 
                                            ValidationGroup="SERVICIOCOMP">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button CssClass="margin_botones" ID="Button_INGRESAR_DETALLE_AL_SERVICIO" 
                                            runat="server" Text="Ingresar detalle" 
                                            onclick="Button_INGRESAR_DETALLE_AL_SERVICIO_Click" 
                                            ValidationGroup="NUEVODETALLE" />
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_DETALLES_SERVICIO -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_DETALLES_SERVICIO"
                                    ControlToValidate="DropDownList_DETALLES_SERVICIO"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El DETALLE DEL SERVICIO es requerido." 
                                    ValidationGroup="NUEVODETALLE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_DETALLES_SERVICIO"
                                    TargetControlID="RequiredFieldValidator_DropDownList_DETALLES_SERVICIO"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_NUEVO_SERVICIO_GRID_DETALLES" runat="server">
                            <table class="table_control_registros" width="500">
                                <tr>
                                    <td>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_DETALLES_NUEVO_SERVICIO" runat="server" Width="500px"  
                                                    AutoGenerateColumns="False" 
                                                    DataKeyNames="ID_SERVICIO,NOMBRE_SERVICIO,ID_SERVICIO_COMPLEMENTARIO" 
                                            
                                            
                                                    onselectedindexchanged="GridView_DETALLES_NUEVO_SERVICIO_SelectedIndexChanged" >
                                                    <Columns>
                                                        <asp:CommandField SelectText="" 
                                                            ShowSelectButton="True" ButtonType="Image" 
                                                            SelectImageUrl="~/imagenes/plantilla/delete.gif" >
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="ID_SERVICIO" HeaderText="ID_SERVICIO" 
                                                            Visible="False" />
                                                        <asp:BoundField DataField="NOMBRE_SERVICIO" HeaderText="NOMBRE_SERVICIO" 
                                                            Visible="False" />
                                                        <asp:BoundField DataField="ID_SERVICIO_COMPLEMENTARIO" 
                                                            HeaderText="ID_SERVICIO_COMPLEMENTARIO" Visible="False" />
                                                        <asp:BoundField DataField="NOMBRE_SERVICIO_COMPLEMENTARIO" 
                                                            HeaderText="Nombre del detalle" >
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_NUEVO_SERVICIO_BOTON_INCLUIR_SERVICIO" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button CssClass="margin_botones" ID="Button_INCLUIR_NUEVO_SERVICIO" 
                                            runat="server" Text="Agregar servicio" 
                                            onclick="Button_INCLUIR_NUEVO_SERVICIO_Click" />
                                    </td>
                                    <td>
                                        <asp:Button CssClass="margin_botones" ID="Button_CANCELAR_NUEVO_SERVICIO" 
                                            runat="server" Text="Cancelar edición" 
                                            onclick="Button_CANCELAR_NUEVO_SERVICIO_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
                                    <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver a la empresa"  
                                        CssClass="margin_botones" ValidationGroup="VOLVER" 
                                        onclick="Button_VOLVER_Click"/>
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
    <asp:Panel ID="Panel_ESTRUCTURA_CENTRO_COSTOS" runat="server">
        <asp:UpdatePanel ID="UpdatePanel_ESTRUCTURA_CENTRO_COSTOS" runat="server">
            <ContentTemplate>
                <div class="div_contenedor_formulario">

                    <%--CONVENCIONES DE COLORES PARA IDENTIFICAR ESTADOS DE LAS UBICACIONES--%>
                    <table class="table_control_registros" width="90%" style=" font-size:10px";>
                        <tr>
                            <td colspan="2" style="text-align:center; font-weight:bold; background-color:#dddddd;">
                                Convenciones
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px; width:15%">
                                <asp:Label ID="Label_nombre_entidad" runat="server" Text="Nombre con Link" 
                                    Font-Underline="True" ForeColor="#0033CC"></asp:Label>
                            </td>
                            <td style="padding:2px;">
                                Si un nombre tiene esta forma significa que se puede dar clic para desplegar las ubicaciones dependientes.
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px; width:15%; background-color:#00ff00">
                                
                            </td>
                            <td style="padding:2px;">
                                Si una ubicación tiene fondo verde significa que ya tiene condiciones comerciales configuradas.
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px; width:15%; background-color:#ff0000">
                                
                            </td>
                            <td style="padding:2px;">
                                Si una ubicación tiene fondo rojo significa que no tiene condiciones comerciales y deben ser configuradas cuanto antes.
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px; width:15%;">
                                <asp:Image ID="Image_ejemploCondiciones" runat="server" 
                                    ImageUrl="~/imagenes/plantilla/view.gif" style="margin:0 auto; display:block;"/>
                            </td>
                            <td style="padding:2px;">
                                Dando clic en la imagen en forma de lupa se accede a la configuración de las condiciones comerciales de la ubicación.
                            </td>
                        </tr>
                    </table>


                    <table width="100%">
                        <tr>
                            <td valign="top" style="width:33%;">
                                <div class="div_cabeza_groupbox_gris">
                                    <asp:Label ID="Label_COBERTURA" runat="server" Text="COBERTURA"></asp:Label>
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel runat="server" ID="UP_MENSAJE_COBERTURA">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel_MENSAJE_COBERTURA" runat="server">
                                                <div class="div_espacio_validacion_campos">
                                                    <asp:Label id="Label_MENSAJE_COBERTURA" runat="server" ForeColor="Red" />
                                                </div>
                                                <div class="div_espaciador"></div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_COBERTURA" runat="server" 
                                                            AutoGenerateColumns="False" 
                                                            DataKeyNames="Código Ciudad" 
                                                            Width="270px" onrowcommand="GridView_COBERTURA_RowCommand" 
                                                            onselectedindexchanged="GridView_COBERTURA_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField DataField="Código Ciudad" HeaderText="Código Ciudad" 
                                                                    Visible="False" />
                                                                <asp:ButtonField CommandName="Ciudad" DataTextField="Ciudad" 
                                                                    HeaderText="Ciudad" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="Condiciones" SelectText="Ver" 
                                                                    ShowSelectButton="True" ButtonType="Image" 
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td valign="top" style="width:33%;">
                                <div class="div_cabeza_groupbox_gris">
                                    <asp:Label ID="Label_CC" runat="server" Text="CENTROS DE COSTO"></asp:Label>
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel runat="server" ID="UP_MENJSAJE_CC">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel_MENSAJE_CC" runat="server">
                                                <div class="div_espacio_validacion_campos">
                                                    <asp:Label id="Label_MENSAJE_CC" runat="server" ForeColor="Red" />
                                                </div>
                                                <div class="div_espaciador"></div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_CENTROS_DE_COSTO" runat="server" 
                                                            AutoGenerateColumns="False" 
                                                            Width="270px" DataKeyNames="ID_CENTRO_C,COD_CC" 
                                                            onrowcommand="GridView_CENTROS_DE_COSTO_RowCommand" 
                                                            onselectedindexchanged="GridView_CENTROS_DE_COSTO_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_CENTRO_C" HeaderText="ID_CENTRO_C" 
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="COD_CC" HeaderText="Código CC" Visible="False" />
                                                                <asp:ButtonField CommandName="centrocosto" DataTextField="NOM_CC" 
                                                                    HeaderText="Centro de Costo" Text="CC" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="Condiciones" SelectText="Ver" 
                                                                    ShowSelectButton="True" ButtonType="Image" 
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td valign="top">
                                <div class="div_cabeza_groupbox_gris">
                                    <asp:Label ID="Label_SUB_CC" runat="server" Text="SUB CENTROS DE COSTO"></asp:Label>
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel runat="server" ID="UP_MENSAJE_SUB_CC">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel_MENSAJE_SUB_CC" runat="server">
                                                <div class="div_espacio_validacion_campos">
                                                    <asp:Label id="Label_MENSAJE_SUB_CC" runat="server" ForeColor="Red" />
                                                </div>
                                                <div class="div_espaciador"></div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_SUB_CENTROS_DE_COSTO" runat="server" 
                                                            AutoGenerateColumns="False" 
                                                            DataKeyNames="ID_SUB_C,COD_SUB_C" 
                                                            Width="270px" 
                                                            onselectedindexchanged="GridView_SUB_CENTROS_DE_COSTO_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_SUB_C" HeaderText="ID_SUB_C" Visible="False" />
                                                                <asp:BoundField DataField="COD_SUB_C" HeaderText="COD_SUB_C" Visible="False" />
                                                                <asp:ButtonField CommandName="subcentrocosto" DataTextField="NOM_SUB_C" 
                                                                    HeaderText="Sub Centro" Text="Button" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="Condiciones" InsertText="Ver" SelectText="Ver" 
                                                                    ShowSelectButton="True" ButtonType="Image" 
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

