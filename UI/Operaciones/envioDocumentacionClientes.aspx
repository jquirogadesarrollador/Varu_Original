<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="envioDocumentacionClientes.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    
    
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPLEADO" runat="server" />       
    <asp:HiddenField ID="HiddenField_ID_CONTRATO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_REFERENCIA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" Style="height: 26px"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_SELECICON_EMPRESA" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Selección de Empresa
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            Empresa:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList_ID_EMPRESA" runat="server" 
                                onselectedindexchanged="DropDownList_ID_EMPRESA_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_SELECCION_EMPLEADO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Selección de Trabajador
            </div>
            <div class="div_contenido_groupbox">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_BUSCADOR_CARGOS">
                    <ProgressTemplate>
                        <div style="text-align: center; margin: 5px; font-weight: bold;">
                            Buscando Trabajadores... Por Favor Espere.
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
                                    <asp:TextBox ID="TextBox_BUSCADOR_TRABAJADOR" runat="server" ValidationGroup="BUSCADORTRABAJADOR"
                                        Width="300px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button_BUSCADOR_TRABAJADOR" runat="server" Text="Buscar" CssClass="margin_botones"
                                        ValidationGroup="BUSCADORTRABAJADOR" 
                                        onclick="Button_BUSCADOR_TRABAJADOR_Click" />
                                </td>
                            </tr>
                        </table>
                        <!-- TextBox_BUSCADOR_TRABAJADOR -->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_BUSCADOR_TRABAJADOR"
                            runat="server" ControlToValidate="TextBox_BUSCADOR_TRABAJADOR" Display="None"
                            ErrorMessage="Campo Requerido faltante</br>El TRABAJADOR es requerido." ValidationGroup="BUSCADORTRABAJADOR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_BUSCADOR_TRABAJADOR"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_BUSCADOR_TRABAJADOR" />
                        <div class="div_espaciador">
                        </div>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    Seleccione un Trabajador Encontrado
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList_ID_TRABAJADOR" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="DropDownList_ID_TRABAJADOR_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="DropDownList_ID_TRABAJADOR" />
                        <asp:PostBackTrigger ControlID= "Button_BUSCADOR_TRABAJADOR" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_DATOS_TRABAJADOR" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información del Trabajador
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Nombre
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NOMBRE_TRABAJADOR" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Documento de Identidad
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_TIP_DOC_IDENTIDAD" runat="server" Font-Bold="True"> </asp:Label><asp:Label ID="Label_NUM_DOC_IDENTIDAD" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Empresa
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_INFORMACION_CONTRATOS" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Historial de Contratos
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_para_convencion_hoja_trabajo">
                    <table class="tabla_alineada_derecha">
                        <tr>
                            <td>
                                Contrato seleccionado
                            </td>
                            <td class="td_contenedor_colores_hoja_trabajo">
                                <div class="div_color_azul">
                                </div>
                            </td>
                            <td>
                                Contrato Activo
                            </td>
                            <td class="td_contenedor_colores_hoja_trabajo">
                                <div class="div_color_verde">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_CONTRATOS" runat="server" Width="883px" AutoGenerateColumns="False"
                            DataKeyNames="ID_CONTRATO,ID_EMPLEADO,ID_REQUERIMIENTO,CONTRATO_ESTADO" 
                            onrowcommand="GridView_CONTRATOS_RowCommand" >
                            <Columns>
                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                    SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ID_CONTRATO" HeaderText="Núm contrato" />
                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="Núm empleado" />
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Núm requsición" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa cliente" />
                                <asp:BoundField DataField="FECHA_INICIA" HeaderText="Fecha inicio" DataFormatString="{0:dd/M/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERMINA" HeaderText="Fecha termina" DataFormatString="{0:dd/M/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLASE_CONTRATO" HeaderText="Clase contrato" />
                                <asp:BoundField DataField="TIPO_CONTRATO" HeaderText="Tipo contrato" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTRATO_ESTADO" HeaderText="Estado contrato" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_DOCUMENTACION" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Selección de Documentoación a Enviar
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros" style="width: 850px;">
                    <tr>
                        <td valign="top" style="width: 50%;">
                            <div class="div_cabeza_groupbox_gris">
                                Documentación de Selección
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros" style="text-align:left;">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="CheckBoxList_DOCUMENTOS_SELECCION" runat="server">
                                                <asp:ListItem Value="INFORME_SELECCION">Informe Selección</asp:ListItem>
                                                <asp:ListItem Value="ARCHIVOS_PRUEBAS">Archivos Pruebas</asp:ListItem>
                                                <asp:ListItem Value="REFERENCIA_LABORAL">Confirmación Referencia Laboral</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>

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
                                            ValidationGroup="ENVIAR" />
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
                                <table class="table_control_registros" style="text-align:left;">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="CheckBoxList_DOCUEMENTOS_CONTRATACION" runat="server">
                                                <asp:ListItem Value="ARCHIVOS_EXAMENES">Examenes Medicos -Resultados-</asp:ListItem>
                                                <asp:ListItem Value="EXAMENES">Examenes Medicos -Autos Recomendación-</asp:ListItem>
                                                <asp:ListItem Value="CONTRATO">Contrato</asp:ListItem>
                                                <asp:ListItem Value="CLAUSULAS">Clausulas</asp:ListItem>
                                                <asp:ListItem Value="ARCHIVOS_AFILIACIONES">Archivos Afiliaciones</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
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
                                            ValidationGroup="ENVIAR" />
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

    <asp:Panel ID="Panel_BOTONES_ABAJO" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Botones de Acción
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Button ID="Button_VERIFICAR_DOCUMENTOS" runat="server" 
                                Text="Verificar Docs" ValidationGroup="VERIFICAR" CssClass="margin_botones" 
                                onclick="Button_VERIFICAR_DOCUMENTOS_Click"/>
                        </td>
                        <td>
                            <asp:Button ID="Button_ENVIAR_DOCUMENTOS" runat="server" Text="Enviar Docs" 
                                ValidationGroup="ENVIAR" CssClass="margin_botones" 
                                onclick="Button_ENVIAR_DOCUMENTOS_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
