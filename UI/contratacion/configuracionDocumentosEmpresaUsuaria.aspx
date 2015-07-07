<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="configuracionDocumentosEmpresaUsuaria.aspx.cs" Inherits="_ConfDocsTrabajador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_CONFIGURACION" runat="server" />

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

    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
        <div class="div_espaciador"></div>
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
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="ADICIONAR" onclick="Button_GUARDAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_SELECCION_TIPO_CONF" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Configuración de Entrega de Documentos a Clientes
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList_TIPO_ENTREGA" runat="server" RepeatDirection="Horizontal"
                                        ValidationGroup="ADICIONAR" 
                                        OnSelectedIndexChanged="RadioButtonList_TIPO_ENTREGA_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                        <asp:ListItem Value="CON">Requiere Entrega de Documentos</asp:ListItem>
                                        <asp:ListItem Value="SIN">No Requiere Entrega de Documentos</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <%-- RadioButtonList_TIPO_ENTREGA --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_TIPO_ENTREGA"
                            ControlToValidate="RadioButtonList_TIPO_ENTREGA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe configurar la entrega de docuemntos."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_TIPO_ENTREGA"
                            TargetControlID="RequiredFieldValidator_RadioButtonList_TIPO_ENTREGA" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_SELECCION_DOCUMENTOS_ENTREGABLES" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Selección de Documentos Entregables
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros" style="width: 850px;">
                            <tr>
                                <td valign="top" style="width: 50%;">
                                    <div class="div_cabeza_groupbox_gris">
                                        Documentación de Selección
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros" style="text-align: left;">
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="CheckBoxList_DOCUMENTOS_SELECCION" runat="server">
                                                        <asp:ListItem Value="INFORME_SELECCION">INFORME DE SELECCIÓN</asp:ListItem>
                                                        <asp:ListItem Value="ARCHIVOS_PRUEBAS">PRUEBAS PSICOTECNICAS</asp:ListItem>
                                                        <asp:ListItem Value="REFERENCIA_PERSONAL_ARCHIVO">REFERENCIAS PERSONALES</asp:ListItem>
                                                        <asp:ListItem Value="REFERENCIA_LABORAL_ARCHIVO">REFERENCIAS LABORALES</asp:ListItem>
                                                        <asp:ListItem Value="REFERENCIA_LABORAL">REFERENCIACIÓN</asp:ListItem>
                                                        <asp:ListItem Value="CERTIFCADO_ESTUDIO">CERTIFICADO DE ESTUDIO</asp:ListItem>
                                                        <asp:ListItem Value="ANTECEDENTES_DISCIPLINARIOS">ANTECEDENTES DISCIPLINARIOS</asp:ListItem>
                                                        <asp:ListItem Value="ANTECEDENTES_JUDICIALES">ANTECEDENTES JUDICIALES</asp:ListItem>
                                                        <asp:ListItem Value="COPIA_CEDULA">COPIA DE LA CEDULA</asp:ListItem>
                                                        <asp:ListItem Value="PASE">PASE</asp:ListItem>
                                                        <asp:ListItem Value="CERTIFCADO_SIMIT">CERTIFICADO SIMIT</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="div_espaciador">
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td>
                                                            Seleccione Contácto:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList_CONTACTO_SELECCION" runat="server" Width="295px"
                                                                OnSelectedIndexChanged="DropDownList_CONTACTO_SELECCION_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox_EMAIL_SELECCION" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- TextBox_EMAIL_SELECCION -->
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EMAIL_SELECCION" runat="server"
                                                    ControlToValidate="TextBox_EMAIL_SELECCION" Display="None" ErrorMessage="Campo Requerido faltante</br>El E-MAIL DEL CONTÁCTO DE SELECCIÓN es requerido."
                                                    ValidationGroup="ADICIONAR" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EMAIL_SELECCION"
                                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EMAIL_SELECCION" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                                <td valign="top" style="width: 50%;">
                                    <div class="div_cabeza_groupbox_gris">
                                        Documentación de Contratación
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros" style="text-align: left;">
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="CheckBoxList_DOCUEMENTOS_CONTRATACION" runat="server">
                                                        <asp:ListItem Value="ARCHIVOS_EXAMENES">Examenes Medicos -Resultados-</asp:ListItem>
                                                        <asp:ListItem Value="EXAMENES">Examenes Medicos -Autos Recomendación-</asp:ListItem>
                                                        <asp:ListItem Value="CONTRATO">Contrato</asp:ListItem>
                                                        <asp:ListItem Value="CLAUSULAS">Clausulas</asp:ListItem>
                                                        <asp:ListItem Value="ARCHIVOS_AFILIACIONES">Afiliaciones</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="div_espaciador">
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td>
                                                            Seleccione Contácto:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList_CONTACTO_CONTRATACION" runat="server" Width="295px"
                                                                OnSelectedIndexChanged="DropDownList_CONTACTO_CONTRATACION_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox_EMAIL_CONTRATACION" runat="server" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- TextBox_EMAIL_CONTRATACION -->
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_EMAIL_CONTRATACION"
                                                    runat="server" ControlToValidate="TextBox_EMAIL_CONTRATACION" Display="None"
                                                    ErrorMessage="Campo Requerido faltante</br>El E-MAIL DEL CONTÁCTO DE CONTRATACIÓN es requerido."
                                                    ValidationGroup="ADICIONAR" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_EMAIL_CONTRATACION"
                                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_EMAIL_CONTRATACION" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    

    <asp:Panel ID="Panel_BOTONES_ABAJO" runat="server">
        <div class="div_espaciador"></div>
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
                                        CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="ADICIONAR" onclick="Button_GUARDAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

