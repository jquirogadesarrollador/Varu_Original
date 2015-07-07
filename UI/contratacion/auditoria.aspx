<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="auditoria.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA" runat="server" />
    <asp:HiddenField ID="HiddenField_CONTRATOS_DROP" runat="server" />
    <asp:HiddenField ID="HiddenField_CONTRATOS_DATO" runat="server" />

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField_PRESENTACION" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_CONTRATO" runat="server" />
    <asp:HiddenField ID="HiddenField_SECCIONES_SIN_AUDITORIA" runat="server" />

    <asp:Panel ID="Panel_OPCION_TIPO_HOJA" runat="server">
        <div class="div_contenedor_formulario">
            <table class="table_control_registros">
                <tr>
                    <td>
                        <asp:Panel ID="Panel_LINK_HOJA_TRABAJO" runat="server">
                            <asp:LinkButton ID="LinkButton_LINK_HOJA_TRABAJO" runat="server" OnClick="LinkButton_LINK_HOJA_TRABAJO_Click">Hoja de trabajo de auditoría</asp:LinkButton>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel_LINK_SELECCIONAR_CONTRATO" runat="server">
                            <asp:LinkButton ID="LinkButton_LINK_SELECCIONAR_CONTRATO" runat="server" OnClick="LinkButton_LINK_SELECCIONAR_CONTRATO_Click">Auditoría de contratos activos</asp:LinkButton>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_FORM_BOTONES_Y_BUSQUEDA" runat="server">
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td colspan="0">
                                    <asp:Button ID="Button_AUDITADO_2" runat="server" Text="Contrato auditado" ValidationGroup="AUDITAR"
                                        CssClass="margin_botones" OnClick="Button_AUDITADO_Click" />
                                </td>
                                <td colspan="0">
                                    <asp:Button ID="Button_VOLVER_2" runat="server" Text="Volver al menú" ValidationGroup="VOLVER"
                                        CssClass="margin_botones" OnClick="Button_VOLVER_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR_2" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <asp:UpdateProgress ID="UpdateProgress_BUSQUEDA" runat="server" AssociatedUpdatePanelID="UpdatePanel_BUSQUEDA">
                        <ProgressTemplate>
                            <div class="div_update_process">
                                <div class="div_aviso_update_process">
                                    <table style="width: 100%; height: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabeL_cargando" runat="server" Text="Cargando datos de contrato..."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel_BUSQUEDA" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" ValidationGroup="BUSCAR_REFERENCIA"
                                                CssClass="margin_botones" OnClick="Button_BUSCAR_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                ControlToValidate="DropDownList_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda."
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                ControlToValidate="TextBox_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar."
                                ValidationGroup="BUSCAR_REFERENCIA" />
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
    <%--                            <!-- TextBox_OBS_EPS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_EPS"
                                ControlToValidate="TextBox_OBS_EPS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="AFILIACION_EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_EPS"
                                TargetControlID="RequiredFieldValidator_TextBox_OBS_EPS" HighlightCssClass="validatorCalloutHighlight" />
--%>
    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_espacio_validacion_campos">
                <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_RESULTADOS_BUSQUEDA_CONTRATOS" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label4" runat="server" Text="Resultado de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA_CONTRATOS" runat="server" Width="885px"
                            AutoGenerateColumns="False" PageSize="5" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_CONTRATOS_RowCommand"
                            
                            
                            DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION,ALERTA,ID_EMPLEADO,REGISTRO_CONTRATO,REGISTRO_PERFIL" >
                            <Columns>
                                <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" ButtonType="Image"
                                    ImageUrl="~/imagenes/plantilla/view2.gif">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Requisición" />
                                <asp:BoundField DataField="REGISTRO_CONTRATO" HeaderText="Núm contrato" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="DOCUMENTO_IDENTIDAD" HeaderText="Número Identidad"></asp:BoundField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombre">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" DataFormatString="$ {0:C}" />
                                <asp:BoundField DataField="NOMBRE_HORARIO" HeaderText="Horario" />
                                <asp:BoundField DataField="NOMBRE_DURACION" HeaderText="Duración" />
                                <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                <asp:BoundField DataField="FCH_INGRESO" HeaderText="Fecha contrato" DataFormatString="{0:dd/M/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ALERTA" HeaderText="ALERTA" Visible="False" />
                                <asp:BoundField DataField="REGISTRO_PERFIL" HeaderText="REGISTRO_PERFIL" 
                                    Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td>
                            <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label>
                            &nbsp;Personas contratadas hoy.
                        </td>
                        <td>
                            <div class="div_color_verde">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label>
                            &nbsp;Personas en plazo de auditoría.
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label>
                            &nbsp;Personas con plazo de auditoría vencido.
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_rojo">
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ENVIO" runat="server" Text="0"></asp:Label>
                            &nbsp;Personas sin envío de documentación.
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_morado">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Hoja de trabajo"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION,ALERTA,ID_EMPLEADO,REGISTRO_CONTRATO,REGISTRO_PERFIL"
                            OnRowCommand="GridView_HOJA_DE_TRABAJO_RowCommand" 
                            PageSize="5" 
                            onpageindexchanging="GridView_HOJA_DE_TRABAJO_PageIndexChanging">
                            <Columns>
                                <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" ButtonType="Image"
                                    ImageUrl="~/imagenes/plantilla/view2.gif">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Requisición" />
                                <asp:BoundField DataField="REGISTRO_CONTRATO" HeaderText="Núm contrato" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="DOCUMENTO_IDENTIDAD" HeaderText="Número Identidad"></asp:BoundField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombre">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" DataFormatString="$ {0:C}" />
                                <asp:BoundField DataField="NOMBRE_HORARIO" HeaderText="Horario" />
                                <asp:BoundField DataField="NOMBRE_DURACION" HeaderText="Duración" />
                                <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                <asp:BoundField DataField="FCH_INGRESO" HeaderText="Fecha contrato" DataFormatString="{0:dd/M/yyyy}">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ALERTA" HeaderText="ALERTA" Visible="False" />
                                <asp:BoundField DataField="REGISTRO_PERFIL" HeaderText="REGISTRO_PERFIL" 
                                    Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_PRINCIPAL" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Áreas auditadas
            </div>
            <div class="div_contenido_groupbox_gris">
                <asp:UpdatePanel ID="UpdatePanel_SOLICITUD_INGRESO" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel_CABEZA_SOLICITUD_INGRESO" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Solicitud de ingreso
                                        <asp:Label ID="Label_SOLICITUD_INGRESO_AUDITADO" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_SOLICITUD_INGRESO" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_SOLICITUD_INGRESO" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_SOLICITUD_INGRESO" runat="server" CssClass="div_contenido_groupbox">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Id solicitud
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="TextBox_ID_SOLICITUD" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td class="td_izq">
                                        Id empleado
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_EMPLEADO" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Apellidos
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_APELLIDOS" runat="server" ValidationGroup="SOLICITUD" MaxLength="30"
                                            Width="260px"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        Nombres
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_NOMBRES" runat="server" ValidationGroup="SOLICITUD" MaxLength="30"
                                            Width="260px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Sexo
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_SEXO" runat="server" ValidationGroup="SOLICITUD"
                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SEXO_SelectedIndexChanged"
                                            Width="260px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_izq">
                                        F. Nacimiento
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FCH_NACIMIENTO" runat="server" ValidationGroup="SOLICITUD"
                                            MaxLength="10" Width="100px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FCH_NACIMIENTO" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_NACIMIENTO" />
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_APELLIDOS -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_APELLIDOS"
                                runat="server" TargetControlID="TextBox_APELLIDOS" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                ValidChars=" ÑñáéíóúÁÉÍÓÚ" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_APELLIDOS" runat="server"
                                ControlToValidate="TextBox_APELLIDOS" Display="None" ErrorMessage="Campo Requerido faltante</br>Los APELLIDOS son requeridos."
                                ValidationGroup="SOLICITUD" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_APELLIDOS"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_APELLIDOS" />
                            <!-- TextBox_NOMBRES -->
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NOMBRES"
                                runat="server" TargetControlID="TextBox_NOMBRES" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                ValidChars=" ÑñáéíóúÁÉÍÓÚ" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_NOMBRES" runat="server"
                                ControlToValidate="TextBox_NOMBRES" Display="None" ErrorMessage="Campo Requerido faltante</br>Los NOMBRES son requeridos."
                                ValidationGroup="SOLICITUD" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_NOMBRES"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_NOMBRES" />
                            <!-- DropDownList_SEXO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_SEXO" runat="server"
                                ControlToValidate="DropDownList_SEXO" Display="None" ErrorMessage="Campo Requerido faltante</br>El SEXO es requerida."
                                ValidationGroup="SOLICITUD" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_SEXO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_SEXO" />
                            <!-- TextBox_FCH_NACIMIENTO -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FCH_NACIMIENTO" runat="server"
                                ControlToValidate="TextBox_FCH_NACIMIENTO" Display="None" ErrorMessage="Campo Requerido faltante</br>La FECHA DE NACIMIENTO es requerida."
                                ValidationGroup="SOLICITUD" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FCH_NACIMIENTO"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FCH_NACIMIENTO" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FCH_NACIMIENTO"
                                runat="server" TargetControlID="TextBox_FCH_NACIMIENTO" FilterType="Custom,Numbers"
                                ValidChars="/">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <table class="table_control_registros">
                                <tr>
                                    <td style="width: 50%;" valign="top">
                                        <div class="div_cabeza_groupbox_gris">
                                            Identificación aspirante
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Doc. Identidad
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_TIP_DOC_IDENTIDAD" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="260px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Número
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_NUM_DOC_IDENTIDAD" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="255px" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Departamento
                                                    </td>
                                                    <td class="td_der" colspan="3">
                                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO_CEDULA" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="260px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_CEDULA_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad
                                                    </td>
                                                    <td class="td_der" colspan="3">
                                                        <asp:DropDownList ID="DropDownList_CIU_CEDULA" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="260px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Teléfono
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_TEL_ASPIRANTE" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="255px" MaxLength="20"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        E-Mail
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_E_MAIL" runat="server" ValidationGroup="SOLICITUD" Width="255px"
                                                            MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Cat. Conducción
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_CAT_LIC_COND" runat="server" Width="260px" ValidationGroup="SOLICITUD">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_TIP_DOC_IDENTIDAD -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_TIP_DOC_IDENTIDAD"
                                                runat="server" ControlToValidate="DropDownList_TIP_DOC_IDENTIDAD" Display="None"
                                                ErrorMessage="Campo Requerido faltante</br>El TIPO DE DOCUMENTO es requerida."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_TIP_DOC_IDENTIDAD"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_TIP_DOC_IDENTIDAD" />
                                            <!-- TextBox_NUM_DOC_IDENTIDAD -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_NUM_DOC_IDENTIDAD"
                                                runat="server" ControlToValidate="TextBox_NUM_DOC_IDENTIDAD" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE DOCUMENTO es requerido."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_NUM_DOC_IDENTIDAD"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_NUM_DOC_IDENTIDAD" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUM_DOC_IDENTIDAD"
                                                runat="server" TargetControlID="TextBox_NUM_DOC_IDENTIDAD" FilterType="Numbers">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <!-- DropDownList_CIU_CEDULA -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CIU_CEDULA" runat="server"
                                                ControlToValidate="DropDownList_CIU_CEDULA" Display="None" ErrorMessage="Campo Requerido faltante</br>La CIUDAD DE EXPEDICIÓN es requerida."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CIU_CEDULA"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CIU_CEDULA" />
                                            <!-- TextBox_TEL_ASPIRANTE -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_TEL_ASPIRANTE"
                                                ControlToValidate="TextBox_TEL_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TELEFONO DEL ASPIRANTE es requerido."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_TEL_ASPIRANTE"
                                                TargetControlID="RequiredFieldValidator_TextBox_TEL_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_TextBox_TEL_ASPIRANTE"
                                                runat="server" TargetControlID="TextBox_TEL_ASPIRANTE" FilterType="Numbers,Custom"
                                                ValidChars="()[]{}- extEXT" />
                                            <!-- TextBox_E_MAIL -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_E_MAIL"
                                                ControlToValidate="TextBox_E_MAIL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El E-MAIL del aspirante es requerido."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_E_MAIL"
                                                TargetControlID="RequiredFieldValidator_TextBox_E_MAIL" HighlightCssClass="validatorCalloutHighlight" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_TextBox_E_MAIL" Display="None"
                                                runat="server" ErrorMessage="<b>E-Mail incorrecto</b><br />El E-MAIL tiene un formato incorrecto."
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox_E_MAIL"
                                                ValidationGroup="SOLICITUD"></asp:RegularExpressionValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_E_MAIL_1"
                                                TargetControlID="RegularExpressionValidator_TextBox_E_MAIL" HighlightCssClass="validatorCalloutHighlight" />
                                        </div>
                                    </td>
                                    <td style="width: 50%;" valign="top">
                                        <div class="div_cabeza_groupbox_gris">
                                            Datos de contácto y adicionales
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Dirección
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DIR_ASPIRANTE" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="255px" MaxLength="30"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Departamento
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_DEPARTAMENTO_ASPIRANTE" runat="server" Width="260px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList_DEPARTAMENTO_ASPIRANTE_SelectedIndexChanged"
                                                            ValidationGroup="SOLICITUD">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_CIU_ASPIRANTE" runat="server" Width="260px" ValidationGroup="SOLICITUD">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Sector
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_SECTOR" runat="server" ValidationGroup="SOLICITUD" Width="255px"
                                                            MaxLength="15"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <asp:Panel ID="Panel_LIB_MILITAR" runat="server" Visible="false">
                                                        <td class="td_izq">
                                                            Libreta militar
                                                        </td>
                                                        <td class="td_der" colspan="3">
                                                            <asp:TextBox ID="TextBox_LIB_MILITAR" runat="server" ValidationGroup="GUARDAR" Width="255px"
                                                                MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <!-- TextBox_LIB_MILITAR -->
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_LIB_MILITAR" runat="server"
                                                            ControlToValidate="TextBox_LIB_MILITAR" Display="None" ErrorMessage="Campo Requerido faltante</br>El NÚMERO DE LA LIBRETA MILITAR es requerido."
                                                            ValidationGroup="GUARDAR" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_LIB_MILITAR"
                                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_LIB_MILITAR" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_LIB_MILITAR"
                                                            runat="server" TargetControlID="TextBox_LIB_MILITAR" FilterType="Numbers" />
                                                    </asp:Panel>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Aspiración
                                                        <br />
                                                        salarial
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_ASPIRACION_SALARIAL" runat="server" ValidationGroup="SOLICITUD"
                                                            Width="255px" MaxLength="15"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- TextBox_DIR_ASPIRANTE -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_DIR_ASPIRANTE"
                                                ControlToValidate="TextBox_DIR_ASPIRANTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDAD DEL ASPIRANTE es requerida."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_DIR_ASPIRANTE"
                                                TargetControlID="RequiredFieldValidator_TextBox_DIR_ASPIRANTE" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- DropDownList_CIU_ASPIRANTE -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CIU_ASPIRANTE"
                                                runat="server" ControlToValidate="DropDownList_CIU_ASPIRANTE" Display="None"
                                                ErrorMessage="Campo Requerido faltante</br>La CIUDAD DEL ASPIRANTE es requerida."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CIU_ASPIRANTE"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CIU_ASPIRANTE" />
                                            <!-- TextBox_SECTOR -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SECTOR"
                                                ControlToValidate="TextBox_SECTOR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SECTOR es requerido."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SECTOR"
                                                TargetControlID="RequiredFieldValidator_TextBox_SECTOR" HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_ASPIRACION_SALARIAL -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_ASPIRACION_SALARIAL"
                                                runat="server" ControlToValidate="TextBox_ASPIRACION_SALARIAL" Display="None"
                                                ErrorMessage="Campo Requerido faltante</br>La ASPIRACIÓN SALARIAL es requerida."
                                                ValidationGroup="SOLICITUD" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_ASPIRACION_SALARIAL"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_ASPIRACION_SALARIAL" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_ASPIRACION_SALARIAL"
                                                runat="server" TargetControlID="TextBox_ASPIRACION_SALARIAL" FilterType="Numbers" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <div style="text-align: right;">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button_Actualizar" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_Actualizar_Click" ValidationGroup="SOLICITUD" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_SOLICITUD_INGRESO"
                            runat="Server" TargetControlID="Panel_CONTENIDO_SOLICITUD_INGRESO" ExpandControlID="Panel_CABEZA_SOLICITUD_INGRESO"
                            CollapseControlID="Panel_CABEZA_SOLICITUD_INGRESO" Collapsed="True" TextLabelID="Label_SOLICITUD_INGRESO"
                            ImageControlID="Image_SOLICITUD_INGRESO" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <div class="div_espaciador">
                        </div>
                        <asp:Panel ID="Panel_CABEZA_AFILIACION_ARP" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Afiliación -Riesgos profesionales-
                                        <asp:Label ID="Label_AFILIACION_ARP_AUDITORIA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_AFILIACION_ARP" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_AFILIACION_ARP" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_AFILIACION_ARP" runat="server" CssClass="div_contenido_groupbox">
                            
                            <asp:HiddenField ID="HiddenField_FECHA_R_ARP" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_RADICACION_ARP" runat="server" />
                            <asp:HiddenField ID="HiddenField_ENTIDAD_ARP" runat="server" />
                            <asp:HiddenField ID="HiddenField_OBS_ARP" runat="server" />
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Id afiliación
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_AFLIACION_ARP" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Iniciación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_R_ARP" runat="server" Width="255" ValidationGroup="AFILIACION_ARP"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_R_ARP" runat="server"
                                            TargetControlID="TextBox_FECHA_R_ARP" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Fecha Radicación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_RADICACION_ARP" runat="server" Width="255" ValidationGroup="AFILIACION_ARP"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_RADICACION_ARP" runat="server"
                                            TargetControlID="TextBox_FECHA_RADICACION_ARP" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_ARP" runat="server" Width="260" ValidationGroup="AFILIACION_ARP">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            
                            <asp:Panel ID="Panel_ARCHIVO_AFILIACION_ARP" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Documento de Radicación Actual
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ARCHIVO_AFILIACION_ARP" runat="server">Ver Archivo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="Panel_SUBIR_ARCHIVO_AFILIACION_ARP" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Actualizar Archivo de Radicación
                                        </td>
                                        <td class="td_der">
                                            <asp:FileUpload ID="FileUpload_ARCHIVO_AFILIACION_ARP" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_OBS_ARP" runat="server" TextMode="MultiLine" Width="550px"
                                            Height="100px" ValidationGroup="AFILIACION_ARP"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FECHA_R_ARP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_R_ARP"
                                ControlToValidate="TextBox_FECHA_R_ARP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de iniciación es requerida."
                                ValidationGroup="AFILIACION_ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_R_ARP"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_R_ARP" HighlightCssClass="validatorCalloutHighlight" />
                            
                            <!-- TextBox_FECHA_RADICACION_ARP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_RADICACION_ARP"
                                ControlToValidate="TextBox_FECHA_RADICACION_ARP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de radicación es requerida."
                                ValidationGroup="AFILIACION_ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_RADICACION_ARP"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_RADICACION_ARP" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- DropDownList_ENTIDAD_ARP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ENTIDAD_ARP"
                                ControlToValidate="DropDownList_ENTIDAD_ARP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD es requerida."
                                ValidationGroup="AFILIACION_ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_ARP"
                                TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_ARP" HighlightCssClass="validatorCalloutHighlight" />

<%--                            <!-- TextBox_OBS_ARP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_ARP"
                                ControlToValidate="TextBox_OBS_ARP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="AFILIACION_ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_ARP"
                                TargetControlID="RequiredFieldValidator_TextBox_OBS_ARP" HighlightCssClass="validatorCalloutHighlight" />
--%>                            <div class="div_espaciador">
                            </div>
                            <asp:Panel ID="Panel_GRILLA_ARP" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ARP" runat="server" Width="700px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_ARP,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO" 
                                                        OnRowCommand="GridView_ARP_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/copiar.jpg" ItemStyle-CssClass="columna_grid_centrada"
                                                                Text="Copiar" CommandName="copiar" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                            <asp:BoundField DataField="ID_ARP" HeaderText="ID_ARP" Visible="False" />
                                                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" 
                                                                Visible="False" />
                                                            <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Iniciación" DataFormatString="{0:dd/M/yyyy}"
                                                                ItemStyle-CssClass="columna_grid_centrada" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FECHA_RADICACION" DataFormatString="{0:dd/M/yyyy}" 
                                                                HeaderText="Fecha radicacion">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" 
                                                                ItemStyle-CssClass="columna_grid_jus" >
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
                            </asp:Panel>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_AFILIACION_ARP" runat="server" Text="Actualizar"
                                            OnClick="Button_ACTUALIZAR_AFILIACION_ARP_Click" ValidationGroup="AFILIACION_ARP" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_AFILIACION_ARP"
                            runat="Server" TargetControlID="Panel_CONTENIDO_AFILIACION_ARP" ExpandControlID="Panel_CABEZA_AFILIACION_ARP"
                            CollapseControlID="Panel_CABEZA_AFILIACION_ARP" Collapsed="True" TextLabelID="Label_BOTON_AFILIACION_ARP"
                            ImageControlID="Image_BOTON_AFILIACION_ARP" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        
                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_AFILIACION_EPS" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Afiliación -Entidad promorora de salud-
                                        <asp:Label ID="Label_AFILIACION_EPS_AUDITADA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_AFILIACION_EPS" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_AFILIACION_EPS" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_AFILIACION_EPS" runat="server" CssClass="div_contenido_groupbox">
                            <asp:HiddenField ID="HiddenField_FECHA_R_EPS" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_RADICACION_EPS" runat="server" />
                            <asp:HiddenField ID="HiddenField_ENTIDAD_EPS" runat="server" />
                            <asp:HiddenField ID="HiddenField_OBS_EPS" runat="server" />

                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Id afiliación
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_AFILIACION_EPS" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Iniciación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_R_EPS" runat="server" Width="255" ValidationGroup="AFILIACION_EPS"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_R_EPS" runat="server"
                                            TargetControlID="TextBox_FECHA_R_EPS" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Radicación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_RADICACION_EPS" runat="server" Width="255" ValidationGroup="AFILIACION_EPS"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_RADICACION_EPS" runat="server"
                                            TargetControlID="TextBox_FECHA_RADICACION_EPS" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_EPS" runat="server" Width="260" ValidationGroup="AFILIACION_EPS">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <asp:Panel ID="Panel_ARCHIVO_AFILIACION_EPS" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Documento de Radicación Actual
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ARCHIVO_AFILIACION_EPS" runat="server">Ver Archivo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="Panel_SUBIR_ARCHIVO_AFILIACION_EPS" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Actualizar Archivo de Radicación
                                        </td>
                                        <td class="td_der">
                                            <asp:FileUpload ID="FileUpload_ARCHIVO_AFILIACION_EPS" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_OBS_EPS" runat="server" TextMode="MultiLine" Width="550px"
                                            Height="100" ValidationGroup="AFILIACION_EPS"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FECHA_R_EPS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_R_EPS"
                                ControlToValidate="TextBox_FECHA_R_EPS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de Iniciación es requerida."
                                ValidationGroup="AFILIACION_EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_R_EPS"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_R_EPS" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_FECHA_RADICACION_EPS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_RADICACION_EPS"
                                ControlToValidate="TextBox_FECHA_RADICACION_EPS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de Radicación es requerida."
                                ValidationGroup="AFILIACION_EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_RADICACION_EPS"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_RADICACION_EPS" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- DropDownList_ENTIDAD_EPS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ENTIDAD_EPS"
                                ControlToValidate="DropDownList_ENTIDAD_EPS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD es requerida."
                                ValidationGroup="AFILIACION_EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_EPS"
                                TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_EPS" HighlightCssClass="validatorCalloutHighlight" />
<%--                            <!-- TextBox_OBS_EPS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_EPS"
                                ControlToValidate="TextBox_OBS_EPS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="AFILIACION_EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_EPS"
                                TargetControlID="RequiredFieldValidator_TextBox_OBS_EPS" HighlightCssClass="validatorCalloutHighlight" />
--%>                            <div class="div_espaciador">
                            </div>
                            <asp:Panel ID="Panel_GRILLA_EPS" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_EPS" runat="server" Width="700px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_EPS,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO" 
                                                        OnRowCommand="GridView_EPS_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/copiar.jpg" ItemStyle-CssClass="columna_grid_centrada"
                                                                Text="Copiar" CommandName="copiar" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                            <asp:BoundField DataField="ID_EPS" HeaderText="ID_EPS" Visible="False" />
                                                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Iniciación" DataFormatString="{0:dd/M/yyyy}"
                                                                ItemStyle-CssClass="columna_grid_centrada" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FECHA_RADICACION" DataFormatString="{0:dd/M/yyyy}" 
                                                                HeaderText="Fecha Radicacion">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" 
                                                                ItemStyle-CssClass="columna_grid_jus" >
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
                            </asp:Panel>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_AFILIACION_EPS" runat="server" Text="Actualizar"
                                            OnClick="Button_ACTUALIZAR_AFILIACION_EPS_Click" 
                                            ValidationGroup="AFILIACION_EPS" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_AFILIACION_EPS" runat="Server"
                            TargetControlID="Panel_CONTENIDO_AFILIACION_EPS" ExpandControlID="Panel_CABEZA_AFILIACION_EPS"
                            CollapseControlID="Panel_CABEZA_AFILIACION_EPS" Collapsed="True" TextLabelID="Label_BOTON_AFILIACION_EPS"
                            ImageControlID="Image_BOTON_AFILIACION_EPS" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>



                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_AFILIACION_CCF" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Afiliación -Caja de compensación familiar-
                                        <asp:Label ID="Label_AFILIACION_CCF_AUDITADA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_AFILIACION_CCF" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_AFILIACION_CCF" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_AFILIACION_CCF" runat="server" CssClass="div_contenido_groupbox">
                            <asp:HiddenField ID="HiddenField_FECHA_R_CAJA" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_RADICACION_CAJA" runat="server" />
                            <asp:HiddenField ID="HiddenField_ENTIDAD_Caja" runat="server" />
                            <asp:HiddenField ID="HiddenField_OBS_CAJA" runat="server" />

                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Id afiliación
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_AFILIACION_CAJA_C" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Iniciación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_R_CAJA" runat="server" Width="255" ValidationGroup="AFILIACION_CCF"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_R_CAJA" runat="server"
                                            TargetControlID="TextBox_FECHA_R_CAJA" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Radicación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_RADICACION_CAJA" runat="server" Width="255" ValidationGroup="AFILIACION_CCF"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_RADICACION_CAJA" runat="server"
                                            TargetControlID="TextBox_FECHA_RADICACION_CAJA" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_Caja" runat="server" Width="260" 
                                            ValidationGroup="AFILIACION_CCF" AutoPostBack="True" 
                                            onselectedindexchanged="DropDownList_ENTIDAD_Caja_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel_CiudadCaja" runat="server">
                                            <div class="div_espaciador">
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Departamento:
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_DepartamentoCajaC" runat="server" AutoPostBack="true"
                                                            Width="260px" OnSelectedIndexChanged="DropDownList_DepartamentoCajaC_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad:
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_CiudadCajaC" runat="server" ValidationGroup="AFILIACION_CCF"
                                                            Width="260px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel_ARCHIVO_AFILIACION_CAJA" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Documento de Radicación Actual
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ARCHIVO_AFILIACION_CAJA" runat="server">Ver Archivo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="Panel_SUBIR_ARCHIVO_AFILIACION_CAJA" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Actualizar Archivo de Radicación
                                        </td>
                                        <td class="td_der">
                                            <asp:FileUpload ID="FileUpload_ARCHIVO_AFILIACION_CAJA" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_OBS_CAJA" runat="server" TextMode="MultiLine" Width="550px"
                                            Height="100" ValidationGroup="AFILIACION_CCF"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FECHA_R_CAJA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_R_CAJA"
                                ControlToValidate="TextBox_FECHA_R_CAJA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de iniciación es requerida."
                                ValidationGroup="AFILIACION_CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_R_CAJA"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_R_CAJA" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_FECHA_RADICACION_CAJA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_RADICACION_CAJA"
                                ControlToValidate="TextBox_FECHA_RADICACION_CAJA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de radicación es requerida."
                                ValidationGroup="AFILIACION_CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_RADICACION_CAJA"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_RADICACION_CAJA" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- DropDownList_ENTIDAD_Caja -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ENTIDAD_Caja"
                                ControlToValidate="DropDownList_ENTIDAD_Caja" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD es requerida."
                                ValidationGroup="AFILIACION_CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_Caja"
                                TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_Caja" HighlightCssClass="validatorCalloutHighlight" />
                            
                            <!-- TextBox_OBS_CAJA -->
                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_CAJA"
                                ControlToValidate="TextBox_OBS_CAJA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="AFILIACION_CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_CAJA"
                                TargetControlID="RequiredFieldValidator_TextBox_OBS_CAJA" HighlightCssClass="validatorCalloutHighlight" />--%> 
                            
                            <!-- DropDownList_CiudadCajaC-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CiudadCajaC"
                                runat="server" ControlToValidate="DropDownList_CiudadCajaC" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;la CIUDAD es requerida."
                                ValidationGroup="AFILIACION_CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CiudadCajaC"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CiudadCajaC" />

                            <div class="div_espaciador">
                            </div>
                            <asp:Panel ID="Panel_GRILLA_CAJA" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_CAJA" runat="server" Width="700px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_CAJA_C,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO" 
                                                        OnRowCommand="GridView_CAJA_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/copiar.jpg" ItemStyle-CssClass="columna_grid_centrada"
                                                                HeaderText="Copiar" Text="Copiar" CommandName="copiar" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                            <asp:BoundField DataField="ID_CAJA_C" HeaderText="ID_CAJA_C" Visible="False" />
                                                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Iniciación" DataFormatString="{0:dd/M/yyyy}"
                                                                ItemStyle-CssClass="columna_grid_centrada" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FECHA_RADICACION" DataFormatString="{0:dd/M/yyyy}" 
                                                                HeaderText="Fecha Radicación">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                            <asp:BoundField DataField="DATOS_CIUDAD" HeaderText="Ciudad" />
                                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" 
                                                                ItemStyle-CssClass="columna_grid_jus" >
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
                            </asp:Panel>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_AFILIACION_CAJA_C" runat="server" Text="Actualizar"
                                            OnClick="Button_ACTUALIZAR_AFILIACION_CAJA_C_Click" 
                                            ValidationGroup="AFILIACION_CCF" />
                                    </td>
                                </tr>
                            </table>




                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_AFILIACION_CCF" runat="Server"
                            TargetControlID="Panel_CONTENIDO_AFILIACION_CCF" ExpandControlID="Panel_CABEZA_AFILIACION_CCF"
                            CollapseControlID="Panel_CABEZA_AFILIACION_CCF" Collapsed="True" TextLabelID="Label_BOTON_AFILIACION_CCF"
                            ImageControlID="Image_BOTON_AFILIACION_CCF" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>



                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_AFILIACION_AFP" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Afiliación -Administradora de fondos y pensiones-
                                        <asp:Label ID="Label_AFILIACION_AFP_AUDITADA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_AFILIACION_AFP" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_AFILIACION_AFP" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_AFILIACION_AFP" runat="server" CssClass="div_contenido_groupbox">
                            <asp:HiddenField ID="HiddenField_FECHA_R_AFP" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_RADICACION_AFP" runat="server" />
                            <asp:HiddenField ID="HiddenField_ENTIDAD_AFP" runat="server" />
                            <asp:HiddenField ID="HiddenField_pensionado" runat="server" />
                            <asp:HiddenField ID="HiddenField_tipo_pensionado" runat="server" />
                            <asp:HiddenField ID="HiddenField_resolucion_tramite" runat="server" />
                            <asp:HiddenField ID="HiddenField_OBS_AFP" runat="server" />

                           
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Id afiliación
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_AFILIACION_F_PENSIONES" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Iniciación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_R_AFP" runat="server" Width="255" 
                                            ValidationGroup="AFILIACION_AFP"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_R_AFP" runat="server"
                                            TargetControlID="TextBox_FECHA_R_AFP" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Fecha de radicación
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_RADICACION_AFP" runat="server" Width="255" 
                                            ValidationGroup="AFILIACION_AFP"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_RADICACION_AFP" runat="server"
                                            TargetControlID="TextBox_FECHA_RADICACION_AFP" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_AFP" runat="server" Width="260px" 
                                            ValidationGroup="AFILIACION_AFP">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <asp:Panel ID="Panel_ARCHIVO_AFILIACION_AFP" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Documento de Radicación Actual
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_ARCHIVO_AFILIACION_AFP" runat="server">Ver Archivo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="Panel_SUBIR_ARCHIVO_AFILIACION_AFP" runat="server">
                                <div class="div_espaciador"></div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Actualizar Archivo de Radicación
                                        </td>
                                        <td class="td_der">
                                            <asp:FileUpload ID="FileUpload_ARCHIVO_AFILIACION_AFP" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <div class="div_espaciador"></div>

                            <div style="border: 1px solid #cccccc; background-color: #eeeeee; padding: 5px; margin: 5px;">
                                <b>Estado de pensión</b><br />
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Pensionado
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_pensionado" runat="server" AutoPostBack="true"
                                                Width="260px" ValidationGroup="AFILIACION_AFP" 
                                                OnSelectedIndexChanged="DropDownList_pensionado_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="panel_TIPO_PENSIONADO" runat="server">
                                    <div class="div_espaciador"></div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Tipo pensionado
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:DropDownList ID="DropDownList_tipo_pensionado" runat="server" ValidationGroup="AFILIACION_AFP"
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Número de resolución<br />o del trámite
                                            </td>
                                            <td colspan="2" class="td_der">
                                                <asp:TextBox ID="TextBox_Numero_resolucion_tramite" runat="server" ValidationGroup="AFILIACION_AFP"
                                                    Width="255px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <%-- DropDownList_tipo_pensionado --%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_tipo_pensionado"
                                        runat="server" ControlToValidate="DropDownList_tipo_pensionado" Display="None"
                                        ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El tipo de pensionado es requerido."
                                        ValidationGroup="AFILIACION_AFP" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_tipo_pensionado"
                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_tipo_pensionado" />
                                    <%-- TextBox_Numero_resolucion_tramite --%>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Numero_resolucion_tramite"
                                        ControlToValidate="TextBox_Numero_resolucion_tramite" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar el número de la resolucion o del tramite segun el caso."
                                        ValidationGroup="AFILIACION_AFP" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Numero_resolucion_tramite"
                                        TargetControlID="RequiredFieldValidator_TextBox_Numero_resolucion_tramite" HighlightCssClass="validatorCalloutHighlight" />
                                </asp:Panel>
                            </div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_OBS_AFP" runat="server" TextMode="MultiLine" Width="550px"
                                            Height="100" ValidationGroup="AFILIACION_AFP"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_FECHA_R_AFP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_R_AFP"
                                ControlToValidate="TextBox_FECHA_R_AFP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de iniciación es requerida."
                                ValidationGroup="AFILIACION_AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_R_AFP"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_R_AFP" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- TextBox_FECHA_RADICACION_AFP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_RADICACION_AFP"
                                ControlToValidate="TextBox_FECHA_RADICACION_AFP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La Fecha de radicación es requerida."
                                ValidationGroup="AFILIACION_AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_RADICACION_AFP"
                                TargetControlID="RequiredFieldValidator_TextBox_FECHA_RADICACION_AFP" HighlightCssClass="validatorCalloutHighlight" />

                            <!-- DropDownList_AFP -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_AFP"
                                ControlToValidate="DropDownList_AFP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD es requerida."
                                ValidationGroup="AFILIACION_AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_AFP"
                                TargetControlID="RequiredFieldValidator_DropDownList_AFP" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- DropDownList_pensionado -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_pensionado"
                                ControlToValidate="DropDownList_pensionado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El campo PENSIONADO es requerido."
                                ValidationGroup="AFILIACION_AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_pensionado"
                                TargetControlID="RequiredFieldValidator_DropDownList_pensionado" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_OBS_AFP -->
<%--                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBS_AFP"
                                ControlToValidate="TextBox_OBS_AFP" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="AFILIACION_AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBS_AFP"
                                TargetControlID="RequiredFieldValidator_TextBox_OBS_AFP" HighlightCssClass="validatorCalloutHighlight" />
--%>                            <div class="div_espaciador">
                            </div>
                            <asp:Panel ID="Panel_GRILLA_AFP" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_AFP" runat="server" Width="700px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_F_PENSIONES,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO" 
                                                        OnRowCommand="GridView_AFP_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/copiar.jpg" ItemStyle-CssClass="columna_grid_centrada"
                                                                Text="Copiar" CommandName="copiar" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                            <asp:BoundField DataField="ID_F_PENSIONES" HeaderText="ID_F_PENSIONES" Visible="False" />
                                                            <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_SOLICITUD" Visible="False" />
                                                            <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Iniciación" DataFormatString="{0:dd/M/yyyy}"
                                                                ItemStyle-CssClass="columna_grid_centrada" >
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FECHA_RADICACION" DataFormatString="{0:dd/M/yyyy}" 
                                                                HeaderText="Fecha Radicacion">
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" 
                                                                ItemStyle-CssClass="columna_grid_jus" >
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
                            </asp:Panel>
                            
                            <div class="div_espaciador"></div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_AFILIACION_F_PENSIONES" runat="server" Text="Actualizar"
                                            ValidationGroup="AFILIACION_AFP" 
                                            OnClick="Button_ACTUALIZAR_AFILIACION_F_PENSIONES_Click" />
                                    </td>
                                </tr>
                            </table>



                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_AFILIACION_AFP" runat="Server"
                            TargetControlID="Panel_CONTENIDO_AFILIACION_AFP" ExpandControlID="Panel_CABEZA_AFILIACION_AFP"
                            CollapseControlID="Panel_CABEZA_AFILIACION_AFP" Collapsed="True" TextLabelID="Label_BOTON_AFILIACION_AFP"
                            ImageControlID="Image_BOTON_AFILIACION_AFP" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>









                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_CONTRATO" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Auditoría de contrato 
                                        <asp:Label ID="Label_CONTRATO_AUDITADA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_CONTRATO" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_CONTRATO" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_CONTRATO" runat="server" CssClass="div_contenido_groupbox">

                            <asp:HiddenField ID="HiddenField_CIUDAD_TRABAJADOR" runat="server" />
                            <asp:HiddenField ID="HiddenField_CENTRO_C_TRABAJADOR" runat="server" />
                            <asp:HiddenField ID="HiddenField_SUB_C_TRABAJADOR" runat="server" />
                            <asp:HiddenField ID="HiddenField_PAGO_DIAS_PRODUCTIVIDAD" runat="server" />
                            <asp:HiddenField ID="HiddenField_SALARIO" runat="server" />
                            <asp:HiddenField ID="HiddenField_VALOR_NOMINA" runat="server" />
                            <asp:HiddenField ID="HiddenField_VALOR_CONTRATO" runat="server" />
                            <asp:HiddenField ID="HiddenField_SAL_INT" runat="server" />
                            <asp:HiddenField ID="HiddenField_PERIODO_PAGO" runat="server" />
                            <asp:HiddenField ID="HiddenField_ESTADOS_SENA" runat="server" />
                            <asp:HiddenField ID="HiddenField_CLASE_CONTRATO" runat="server" />
                            <asp:HiddenField ID="HiddenField_TIPO_CONTRATO" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_INICIA" runat="server" />
                            <asp:HiddenField ID="HiddenField_FECHA_TERMINA" runat="server" />

                            <asp:HiddenField ID="HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION" runat="server" />

                            <asp:HiddenField ID="HiddenField_RIESGO_INICIAL" runat="server" />
                                                        
                            <asp:Panel ID="Panel_MENSAJE_CONTRATO" runat="server">
                                <div class="div_espacio_validacion_campos">
                                    <asp:Label ID="Label_MENSAJE_CONTRATO" runat="server" ForeColor="Red" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="Panel_contrato_empresa" runat="server">
                                <div class="div_espaciador"></div>
                                <div class="div_cabeza_groupbox_gris">
                                    Datos de contratacion con la EMPRESA USUARIA
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                            <td>
                                                Fecha de inicio
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_fecha_inicia_contrato_empresa" runat="server" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                Fecha de vencimiento
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_fecha_vence_contrato_empresa" runat="server" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <tr>
                                                <td colspan="4">
                                                    Objeto del contrato/servicio respectivo
                                                </td>
                                            </tr>
                                        </table>


                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox_objeto_contrato" runat="server" ReadOnly="true" 
                                                    Width="800px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                <div class="div_espaciador"></div>

                            </asp:Panel>

                            <asp:Panel ID="Panel_UBICACION_TRABAJADOR" runat="server">
                                <div class="div_espaciador"></div>
                                <div class="div_cabeza_groupbox_gris">
                                    Ubicación Trabajador
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                Empresa
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_EMPRESA_TRABAJADOR" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <div class="div_espaciador">
                                    </div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Cargo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CARGO_TRABAJADOR" runat="server" 
                                                    Width="260px" AutoPostBack="True" 
                                                    
                                                    onselectedindexchanged="DropDownList_CARGO_TRABAJADOR_SelectedIndexChanged" 
                                                    ValidationGroup="DATOS_CONTRATO" Height="16px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Ciudad
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CIUDAD_TRABAJADOR" runat="server" 
                                                    Width="260px" AutoPostBack="True" 
                                                    
                                                    onselectedindexchanged="DropDownList_CIUDAD_TRABAJADOR_SelectedIndexChanged" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_CIUDAD_SELECCIONADA" runat="server" Text=" " Height="20px" 
                                                    Width="20px" BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Centro de costo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CC_TRABAJADOR" runat="server" Width="260px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="DropDownList_CC_TRABAJADOR_SelectedIndexChanged" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_CC_SELECCIONADO" runat="server" Text=" " Height="20px" 
                                                    Width="20px" BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Sub centro de costo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_SUB_CENTRO_TRABAJADOR" runat="server" 
                                                    Width="260px" AutoPostBack="True" 
                                                    
                                                    onselectedindexchanged="DropDownList_SUB_CENTRO_TRABAJADOR_SelectedIndexChanged" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label_SUBC_SELECCIONADO" runat="server" Text=" " Height="20px" 
                                                    Width="20px" BackColor="#009900"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_CARGO_TRABAJADOR -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CARGO_TRABAJADOR"
                                        ControlToValidate="DropDownList_CARGO_TRABAJADOR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CARGO es requerido."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CARGO_TRABAJADOR"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CARGO_TRABAJADOR" HighlightCssClass="validatorCalloutHighlight" />

                                    <!-- DropDownList_CIUDAD_TRABAJADOR -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD_TRABAJADOR"
                                        ControlToValidate="DropDownList_CIUDAD_TRABAJADOR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDADes requerida."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD_TRABAJADOR"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD_TRABAJADOR" HighlightCssClass="validatorCalloutHighlight" />

                                    <div style=" margin:8px auto; text-align:center; width:600px; background-color:#eeeeee;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Riesgo:
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_RIESGO_EMPLEADO" runat="server" 
                                                        ValidationGroup="DATOS_CONTRATO" AutoPostBack="True" 
                                                        onselectedindexchanged="DropDownList_RIESGO_EMPLEADO_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align:center;">
                                                    <asp:Label ID="Label_RIESGO_INICIAL" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- DropDownList_RIESGO_EMPLEADO -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_RIESGO_EMPLEADO" ControlToValidate="DropDownList_RIESGO_EMPLEADO"
                                            Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El RIESGO es requerido."
                                            ValidationGroup="DATOS_CONTRATO" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_RIESGO_EMPLEADO"
                                            TargetControlID="RequiredFieldValidator_DropDownList_RIESGO_EMPLEADO" HighlightCssClass="validatorCalloutHighlight" />
                                    </div>
                                </div>
                            </asp:Panel>

                            
                            

                            <asp:Panel ID="Panel_SALARIO_TRABAJADOR" runat="server">
                                <div class="div_espaciador"></div>
                                <div class="div_cabeza_groupbox_gris">
                                    Salario
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    
                                    <asp:Panel ID="Panel_SALARIO_NOM_VALOR_UNIDAD" runat="server">
                                        <div class="div_espaciador"></div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Salario nómina
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_SALARIO_NOMINA" runat="server" 
                                                        ValidationGroup="DATOS_CONTRATO"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_SALARIO_NOMINA -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SALARIO_NOMINA"
                                            ControlToValidate="TextBox_SALARIO_NOMINA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El VALOR NÓMINA es requerido."
                                            ValidationGroup="DATOS_CONTRATO" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SALARIO_NOMINA"
                                            TargetControlID="RequiredFieldValidator_TextBox_SALARIO_NOMINA" HighlightCssClass="validatorCalloutHighlight" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_SALARIO_NOMINA" runat="server" TargetControlID="TextBox_SALARIO_NOMINA" FilterType="Numbers" />

                                    </asp:Panel>

                                    <asp:Panel ID="Panel_SALARIO" runat="server">
                                        <div class="div_espaciador"></div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Salario
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_SALARIO" runat="server" 
                                                        ValidationGroup="DATOS_CONTRATO"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- TextBox_SALARIO -->
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_SALARIO"
                                            ControlToValidate="TextBox_SALARIO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SALARIO es requerido."
                                            ValidationGroup="DATOS_CONTRATO" />
                                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_SALARIO"
                                            TargetControlID="RequiredFieldValidator_TextBox_SALARIO" HighlightCssClass="validatorCalloutHighlight" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_SALARIO" runat="server" TargetControlID="TextBox_SALARIO" FilterType="Numbers" />
                                    </asp:Panel>
                                    
                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                Salario integral
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_SALARIO_INTEGRAL" runat="server" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_SALARIO_INTEGRAL -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_SALARIO_INTEGRAL"
                                        ControlToValidate="DropDownList_SALARIO_INTEGRAL" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Indique si es SALARIO INTEGRAL o no."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_SALARIO_INTEGRAL"
                                        TargetControlID="RequiredFieldValidator_DropDownList_SALARIO_INTEGRAL" HighlightCssClass="validatorCalloutHighlight" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="Panel_DATOS_CONTRATO" runat="server">
                                <div class="div_espaciador"></div>
                                <div class="div_cabeza_groupbox_gris">
                                    Información de contratación con el TRABAJADOR
                                </div>
                                   <table class="table_control_registros">
                                        <tr>
                                            <td colspan="4">
                                                Funciones del cargo
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox_funciones_cargo" runat="server" Height="100px" 
                                                    ReadOnly="true" TextMode="MultiLine" Width="800px"></asp:TextBox>
                                            </td>
                                        </tr>

                                   </table> 
                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                            <td>
                                                Periodo de pago
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_PERIODO_PAGO" runat="server" 
                                                    ValidationGroup="DATOS_CONTRATO" Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                    </table>
                                    <!-- DropDownList_PERIODO_PAGO -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_PERIODO_PAGO"
                                        ControlToValidate="DropDownList_PERIODO_PAGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PERIODO DE PAGO es requerido."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_PERIODO_PAGO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_PERIODO_PAGO" HighlightCssClass="validatorCalloutHighlight" />

                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList_ESTADOS_SENA" runat="server" 
                                                    RepeatDirection="Horizontal" ValidationGroup="DATOS_CONTRATO">
                                                    
                                                    <asp:ListItem Value="PRACTICANTE">Prácticante universitario</asp:ListItem>
                                                    <asp:ListItem Value="LECTIVO">SENA Lectivo</asp:ListItem>
                                                    <asp:ListItem Value="PRODUCTIVO">SENA Productivo</asp:ListItem>
                                                    <asp:ListItem Value="NINGUNO">Ninguno</asp:ListItem>
                                                    
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- RadioButtonList_ESTADOS_SENA -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_ESTADOS_SENA"
                                        ControlToValidate="RadioButtonList_ESTADOS_SENA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_ESTADOS_SENA"
                                        TargetControlID="RequiredFieldValidator_RadioButtonList_ESTADOS_SENA" HighlightCssClass="validatorCalloutHighlight" />

                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Clase contrato
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CLASE_CONTRATO" runat="server" Width="260px" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Tipo contrato
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_TIPO_CONTRATO" runat="server" Width="260px" 
                                                    ValidationGroup="DATOS_CONTRATO">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_CLASE_CONTRATO -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CLASE_CONTRATO"
                                        ControlToValidate="DropDownList_CLASE_CONTRATO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CLASE DE CONTRATO es requerido."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CLASE_CONTRATO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CLASE_CONTRATO" HighlightCssClass="validatorCalloutHighlight" />

                                    <!-- DropDownList_TIPO_CONTRATO -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TIPO_CONTRATO"
                                        ControlToValidate="DropDownList_TIPO_CONTRATO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CONTRATO es requerido."
                                        ValidationGroup="DATOS_CONTRATO" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TIPO_CONTRATO"
                                        TargetControlID="RequiredFieldValidator_DropDownList_TIPO_CONTRATO" HighlightCssClass="validatorCalloutHighlight" />
                                    
                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Fecha inicio
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_FECHA_INICIO" runat="server" Width="110px" 
                                                    ValidationGroup="DATOS_CONTRATO"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Fecha Fin
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_FECHA_FIN" runat="server" Width="110px" 
                                                    ValidationGroup="DATOS_CONTRATO"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </asp:Panel>

                            <asp:UpdatePanel ID="UpdatePanel_FormaPago" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel_NoLaboroDobleRegistro" runat="server">
                                        <div class="div_espaciador">
                                        </div>
                                        <div class="div_cabeza_groupbox_gris">
                                            Selección Especial
                                        </div>
                                        <div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_NoLaboro" runat="server" Text="No Laboró" AutoPostBack="True"
                                                            OnCheckedChanged="CheckBox_NoLaboro_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_DobleRegistro" runat="server" Text="No Ingresó" 
                                                            OnCheckedChanged="CheckBox_DobleRegistro_CheckedChanged" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_FORMA_PAGO" runat="server">
                                        <div class="div_espaciador">
                                        </div>
                                        <div class="div_cabeza_groupbox_gris">
                                            Forma de pago
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_forma_Pago" runat="server" Text="Forma de Pago"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_forma_pago" runat="server" AutoPostBack="true"
                                                            Width="260px" ValidationGroup="DATOS_CONTRATO" OnSelectedIndexChanged="DropDownList_forma_pago_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_forma_pago -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_forma_pago"
                                                ControlToValidate="DropDownList_forma_pago" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FORMA DE PAGO es requerida."
                                                ValidationGroup="DATOS_CONTRATO" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_forma_pago"
                                                TargetControlID="RequiredFieldValidator_DropDownList_forma_pago" HighlightCssClass="validatorCalloutHighlight" />
                                            <asp:Panel ID="Panel_ENTIDAD_BANCARIA_Y_CUENTA" runat="server">
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Entidad bancaria
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_entidad_bancaria" runat="server" Width="260px"
                                                                ValidationGroup="DATOS_CONTRATO">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Tipo de Cuenta
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:DropDownList ID="DropDownList_TIPO_CUENTA" runat="server" Width="260px" ValidationGroup="DATOS_CONTRATO">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            Número de Cuenta
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:TextBox ID="TextBox_NUMERO_CUENTA" runat="server" Width="255px" ValidationGroup="DATOS_CONTRATO"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUMERO_CUENTA"
                                                                runat="server" TargetControlID="TextBox_NUMERO_CUENTA" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- DropDownList_entidad_bancaria -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_entidad_bancaria"
                                                    ControlToValidate="DropDownList_entidad_bancaria" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La LA ENTIDAD BANCARIA es requerida."
                                                    ValidationGroup="DATOS_CONTRATO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_entidad_bancaria"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_entidad_bancaria" HighlightCssClass="validatorCalloutHighlight" />
                                                <!-- DropDownList_TIPO_CUENTA -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TIPO_CUENTA"
                                                    ControlToValidate="DropDownList_TIPO_CUENTA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CUENTA es requerido."
                                                    ValidationGroup="DATOS_CONTRATO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TIPO_CUENTA"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_TIPO_CUENTA" HighlightCssClass="validatorCalloutHighlight" />
                                                <!-- TextBox_NUMERO_CUENTA -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NUMERO_CUENTA"
                                                    ControlToValidate="TextBox_NUMERO_CUENTA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NÚMERO DE CUENTA es requerido."
                                                    ValidationGroup="DATOS_CONTRATO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NUMERO_CUENTA"
                                                    TargetControlID="RequiredFieldValidator_TextBox_NUMERO_CUENTA" HighlightCssClass="validatorCalloutHighlight" />
                                            </asp:Panel>

                                            <asp:Panel ID="Panel_PagoCheque" runat="server">
                                                <div class="div_espaciador"></div>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Cheque Reg:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList_ChequeReg" runat="server" 
                                                                ValidationGroup="DATOS_CONTRATO" Width="260px" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- DropDownList_ChequeReg -->
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ChequeReg" ControlToValidate="DropDownList_ChequeReg"
                                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />CHEQUE REG es requerido."
                                                    ValidationGroup="DATOS_CONTRATO" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ChequeReg"
                                                    TargetControlID="RequiredFieldValidator_DropDownList_ChequeReg" HighlightCssClass="validatorCalloutHighlight" />
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>


 
                            <asp:Panel ID="Panel_FONDO_AVISO" runat="server" Visible="false" Style="background-color: #999999;">
                            </asp:Panel>
                            <asp:Panel ID="Panel_AVISO" runat="server">
                                <asp:Image ID="Image_AVISO_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                                    display: block;" ImageUrl="~/imagenes/plantilla/advertencia_popup.png" />
                                <asp:Panel ID="Panel_COLOR_FONDO_AVISO" runat="server" Style="text-align: center">
                                    <asp:Label ID="Label_AVISO" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                                </asp:Panel>
                                <div style="text-align: center; margin-top: 15px;">

                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_ACEPTAR_AVISO" runat="server" Text="Aceptar" 
                                                    CssClass="margin_botones" onclick="Button_ACEPTAR_AVISO_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_NO_AVISO" runat="server" Text="Cancelar" CssClass="margin_botones" />
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </div>
                            </asp:Panel>

                            <div class="div_espaciador"></div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALZIAR_CONTRATO" runat="server" Text="Actualizar"
                                            ValidationGroup="DATOS_CONTRATO" 
                                            onclick="Button_ACTUALZIAR_CONTRATO_Click"/>
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_CONTRATO" runat="Server"
                            TargetControlID="Panel_CONTENIDO_CONTRATO" ExpandControlID="Panel_CABEZA_CONTRATO"
                            CollapseControlID="Panel_CABEZA_CONTRATO" Collapsed="True" TextLabelID="Label_BOTON_CONTRATO"
                            ImageControlID="Image_BOTON_CONTRATO" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>









                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_CONCEPTOS_FIJOS" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Parametrización de conceptos fijos
                                        <asp:Label ID="Label_CONCEPTOS_FIJOS_AUDITADA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_CONCEPTOS_FIJOS" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_CONCEPTOS_FIJOS" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_CONCEPTOS_FIJOS" runat="server" CssClass="div_contenido_groupbox">
                            
                            <asp:Panel ID="Panel_MENSAJES_CONCEPTOS_FIJOS" runat="server">
                                <div class="div_espacio_validacion_campos">
                                    <asp:Label ID="Label_MENSAJES_CONCEPTOS_FIJOS" runat="server" ForeColor="Red" />
                                </div>
                            </asp:Panel>

                            <div class="div_cabeza_groupbox_gris">
                                Clausulas del Trabajador
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_LISTA_CLAUSULAS_PARAMETRIZADAS" runat="server" Width="845px"
                                                        AutoGenerateColumns="False" DataKeyNames="ID_CLAUSULA,ID_EMPLEADO" 
                                                        OnRowCommand="GridView_LISTA_CLAUSULAS_PARAMETRIZADAS_RowCommand">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                                                Text="Seleccionar">
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ID_CLAUSULA" HeaderText="ID_CLAUSULA" Visible="false">
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="false">
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TIPO_CLAUSULA" HeaderText="Tipo">
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion">
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Archivo">
                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="FileUpload_ARCHIVO" runat="server" />
                                                                    <asp:HyperLink ID="HyperLink_ARCHIVO" runat="server">Ver Clausulas</asp:HyperLink>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_FileUpload_archivo" runat="server"
                                                                        ControlToValidate="FileUpload_ARCHIVO" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ARCHIVO es requerido."
                                                                        ValidationGroup="ACTUALIZAR" />
                                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FileUpload_archivo"
                                                                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_FileUpload_archivo" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="PanelAdicionarClausula" runat="server">
                                    <div class="div_espaciador"></div>
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width:60%;">
                                            
                                            </td>
                                            <td style="text-align:right; padding-right:5px;">
                                                <asp:Button ID="Button_AdicionarClausula" runat="server" 
                                                    Text="Adicionar Nueva Clausula" onclick="Button_AdicionarClausula_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel_DatosNuevaClausula" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Datos Nueva Clausula
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Tipo de Clausula:
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_ID_TIPO_CLAUSULA" runat="server" Width="500px" ValidationGroup="ADD_CLAUSULA">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    Descripción de la Clausula:
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_DESCRIPCION" runat="server" Height="99px" TextMode="MultiLine" ValidationGroup="ADD_CLAUSULA"
                                                        Width="500px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    Cargar archivo(Word) de la clausula:
                                                </td>
                                                <td class="td_der">
                                                    <asp:FileUpload ID="FileUpload_ArchivoClausula" runat="server" />
                                                </td>
                                            </tr>
                                        </table>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_TIPO_CLAUSULA"
                                            runat="server" ControlToValidate="DropDownList_ID_TIPO_CLAUSULA" Display="None"
                                            ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El TIPO DE CLAUSULA es requerido."
                                            ValidationGroup="ADD_CLAUSULA" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_TIPO_CLAUSULA"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_TIPO_CLAUSULA" />
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_DESCRIPCION"
                                            runat="server" ControlToValidate="TextBox_DESCRIPCION" Display="None"
                                            ErrorMessage="Campo Requerido faltante<BR>ELa DESCRIPCION es requerida."
                                            ValidationGroup="ADD_CLAUSULA" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_DESCRIPCION"
                                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION" />

                                        <div class="div_espaciador"></div>

                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align:center;">
                                                    <asp:Button ID="Button_ProcesarNuevaClausula" runat="server" Text="Aceptar" 
                                                        CssClass="margin_botones" ValidationGroup="ADD_CLAUSULA" 
                                                        onclick="Button_ProcesarNuevaClausula_Click" />
                                                </td>
                                                <td style="text-align:center;">
                                                    <asp:Button ID="Button_CancelarnuevaClausula" runat="server" Text="Cancelar" 
                                                        CssClass="margin_botones" ValidationGroup="CANCELAR_CLAUSULA" 
                                                        onclick="Button_CancelarnuevaClausula_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>

                            <asp:Panel ID="Panel_CONCEPTOS_FIJOS_PARAMETRIZADOS" runat="server">
                                <div class="div_espaciador"></div>
                                <div style="border: 1px solid #cccccc; background-color: #eeeeee;">
                                    <b>Conceptos fijos parametrizados</b><br />
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS" runat="server" Width="846px"
                                                AutoGenerateColumns="False" 
                                                DataKeyNames="REGISTRO,ID_EMPLEADO,ID_CONCEPTO,ID_CLAUSULA,LIQ_Q_1,LIQ_Q_2,LIQ_Q_3,LIQ_Q_4" 
                                                onrowcommand="GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField CommandName="eliminar" Text="Eliminar" ButtonType="Image" ImageUrl="~/imagenes/plantilla/delete.gif">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" HtmlEncode="False" 
                                                        HtmlEncodeFormatString="False" Visible="False" />
                                                    <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="ID_CONCEPTO" HeaderText="ID_CONCEPTO" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="DESC_CONCEPTO" HeaderText="Concepto fijo">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CAN_PRE" HeaderText="Cantidad cuotas">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VAL_PRE" HeaderText="Valor cuota" 
                                                        DataFormatString="{0:N0}">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERIODOS_LIQUIDACION" 
                                                        HeaderText="Périodos para liquidar" >
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID_CLAUSULA" HeaderText="Núm clausula" >
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LIQ_Q_1" HeaderText="LIQ_Q_1" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_2" HeaderText="LIQ_Q_2" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_3" HeaderText="LIQ_Q_3" Visible="False" />
                                                    <asp:BoundField DataField="LIQ_Q_4" HeaderText="LIQ_Q_4" Visible="False" />
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="Panel_NUEVO_CONCEPTO_FIJO" runat="server">
                                <div class="div_espaciador"></div>
                                <div class="div_cabeza_groupbox_gris">
                                    Nuevo concepto fijo
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Id clausula relacionada
                                            </td>
                                            <td class="td_der">
                                                <asp:Label ID="Label_ID_CLAUSULA_RELACIONADA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:UpdatePanel ID="UpdatePanel_ConceptosFijos" runat="server">
                                        <ContentTemplate>
                                            <div class="div_espaciador">
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Forma de pago (nómina)
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_FORMA_PAGO_NOMINA" runat="server" Width="260px"
                                                            
                                                            OnSelectedIndexChanged="DropDownList_FORMA_PAGO_NOMINA_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_FORMA_PAGO_NOMINA -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_FORMA_PAGO_NOMINA"
                                                ControlToValidate="DropDownList_FORMA_PAGO_NOMINA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FORMA DE PAGO (NOMINA) es requerida."
                                                ValidationGroup="CONCEPTOS_FIJOS" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_FORMA_PAGO_NOMINA"
                                                TargetControlID="RequiredFieldValidator_DropDownList_FORMA_PAGO_NOMINA" HighlightCssClass="validatorCalloutHighlight" />
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        Selecciones periodos de pago:
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_PER_1" runat="server" Text="Período 1" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_PER_2" runat="server" Text="Período 2" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_PER_3" runat="server" Text="Período 3" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox_PER_4" runat="server" Text="Período 4" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    <div class="div_espaciador"></div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Concepto Fijo
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_CONCEPTOS_FIJOS" runat="server" 
                                                    Width="260px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Número de Cuotas<br />
                                                (0 para infinitas)
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_CAN_PRE" runat="server" Width="255px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Valor Cuota
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_VAL_PRE" runat="server" Width="255px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- DropDownList_CONCEPTOS_FIJOS -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CONCEPTOS_FIJOS"
                                        ControlToValidate="DropDownList_CONCEPTOS_FIJOS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CONCEPTO FIJO es requerido."
                                        ValidationGroup="CONCEPTOS_FIJOS" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CONCEPTOS_FIJOS"
                                        TargetControlID="RequiredFieldValidator_DropDownList_CONCEPTOS_FIJOS" HighlightCssClass="validatorCalloutHighlight" />
                                    <!-- TextBox_CAN_PRE -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CAN_PRE"
                                        ControlToValidate="TextBox_CAN_PRE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CANTIDAD DE CUOTAS es requerida."
                                        ValidationGroup="CONCEPTOS_FIJOS" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CAN_PRE"
                                        TargetControlID="RequiredFieldValidator_TextBox_CAN_PRE" HighlightCssClass="validatorCalloutHighlight" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CAN_PRE" runat="server" TargetControlID="TextBox_CAN_PRE" FilterType="Numbers" />
                                    <!-- TextBox_VAL_PRE -->
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_VAL_PRE"
                                        ControlToValidate="TextBox_VAL_PRE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El VALOR DE CADA CUOTA es requerido."
                                        ValidationGroup="CONCEPTOS_FIJOS" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_VAL_PRE"
                                        TargetControlID="RequiredFieldValidator_TextBox_VAL_PRE" HighlightCssClass="validatorCalloutHighlight" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_VAL_PRE" runat="server" TargetControlID="TextBox_VAL_PRE" FilterType="Numbers" />

                                    <div class="div_espaciador"></div>
                                   
                                    <table class="table_control_registros">
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button_ADICIONAR_CONCEPTO_FIJO" runat="server" 
                                                    Text="Adicionar concepto" onclick="Button_ADICIONAR_CONCEPTO_FIJO_Click" 
                                                    ValidationGroup="CONCEPTOS_FIJOS" CssClass="margin_botones"/>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_CANCELAR_ADICION_CONCEPTO" runat="server" 
                                                    Text="Cancelar" CssClass="margin_botones" 
                                                    onclick="Button_CANCELAR_ADICION_CONCEPTO_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>

                            <div class="div_espaciador"></div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALZIAR_CONCEPTOS_FIJOS" runat="server" Text="Actualizar y auditar conceptos fijos"
                                            ValidationGroup="CONCEPTOS_FIJOS" 
                                            onclick="Button_ACTUALZIAR_CONCEPTOS_FIJOS_Click"/>
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_CONCEPTOS_FIJOS" runat="Server"
                            TargetControlID="Panel_CONTENIDO_CONCEPTOS_FIJOS" ExpandControlID="Panel_CABEZA_CONCEPTOS_FIJOS"
                            CollapseControlID="Panel_CABEZA_CONCEPTOS_FIJOS" Collapsed="True" TextLabelID="Label_BOTON_CONCEPTOS_FIJOS"
                            ImageControlID="Image_BOTON_CONCEPTOS_FIJOS" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>

                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_EXAMENES" runat="server" CssClass="div_cabeza_groupbox_gris">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Examenes y Autos de Recomendación
                                        <asp:Label ID="Label_EXAMENES_AUDITORIA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_EXAMENES" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_EXAMENES" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_EXAMENES" runat="server" CssClass="div_contenido_groupbox">
                            
                            <asp:Panel ID="Panel_MENSAJE_EXAMENES" runat="server">
                                <div class="div_espacio_validacion_campos">
                                    <asp:Label ID="Label_MENSAJE_EXAMENES" runat="server" ForeColor="Red" />
                                </div>
                            </asp:Panel>

                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_EXAMENES_REALIZADOS" runat="server" Width="800px" AutoGenerateColumns="False"
                                                    DataKeyNames="REGISTRO,ID_PRODUCTO,REGISTRO_PROVEEDOR,REGISTRO_PRODUCTOS_PROVEEDOR,ID_ORDEN">
                                                    <Columns>
                                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                        <asp:BoundField DataField="REGISTRO_PRODUCTOS_PROVEEDOR" HeaderText="REGISTRO_PRODUCTOS_PROVEEDOR"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="REGISTRO_PROVEEDOR" HeaderText="REGISTRO_PROVEEDOR" Visible="False" />
                                                        <asp:BoundField DataField="ID_ORDEN" HeaderText="ID_ORDEN" Visible="False" />
                                                        <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID_PRODUCTO" Visible="False" />
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Examen" />
                                                        <asp:BoundField DataField="NOM_PROVEEDOR" HeaderText="Laboratorio" />
                                                        <asp:TemplateField HeaderText="Autos con Recomendación">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox_Autos_Recomendacion" runat="server" Width="500px" Height="60px" ValidationGroup="BUSCAR_CLIENTE"
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="500px"/>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            
                            <div class="div_espaciador"></div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_EXAMENES" runat="server" Text="Actualizar"
                                            ValidationGroup="EXAMENES" onclick="Button_ACTUALIZAR_EXAMENES_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_IMPRIMIR_AUTOS" runat="server" Text="Imprimir Autos"
                                            ValidationGroup="EXAMENES_AUTOS" onclick="Button_IMPRIMIR_AUTOS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_EXAMENES" runat="Server"
                            TargetControlID="Panel_CONTENIDO_EXAMENES" ExpandControlID="Panel_CABEZA_EXAMENES"
                            CollapseControlID="Panel_CABEZA_EXAMENES" Collapsed="True" TextLabelID="Label_BOTON_EXAMENES"
                            ImageControlID="Image_BOTON_EXAMENES" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>

                        <div class="div_espaciador"></div>

                        <asp:Panel ID="Panel_CABEZA_ENVIOARCHIVOS" runat="server" CssClass="div_cabeza_groupbox_gris">

                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 87%;">
                                        Envío de Archivos a Cliente
                                        <asp:Label ID="Label_ENVIOARCHIVOS_AUDITORIA" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                            <tr>
                                                <td style="font-size: 80%">
                                                    <asp:Label ID="Label_BOTON_ENVIOARCHIVOS" runat="server">(Mostrar detalles...)</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image_BOTON_ENVIOARCHIVOS" runat="server" CssClass="img_cabecera_hoja" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_ENVIOARCHIVOS" runat="server" CssClass="div_contenido_groupbox">
                            
                            <asp:Panel ID="Panel_MENSAJE_ENVIOARCHOVOS" runat="server">
                                <div class="div_espacio_validacion_campos">
                                    <asp:Label ID="Label_MENSAJE_ENVIOARCHIVOS" runat="server" ForeColor="Red" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="Panel_SELECCION_DE_ARCHIVOS_A_ENVIAR" runat="server">
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
                                                                <asp:ListItem Value="INFORME_SELECCION">Informe Selección</asp:ListItem>
                                                                <asp:ListItem Value="ARCHIVOS_PRUEBAS">Archivos Pruebas</asp:ListItem>
                                                                <asp:ListItem Value="REFERENCIA_LABORAL">Confirmación Referencia Laboral</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="div_espaciador">
                                                </div>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Contácto:
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:Label ID="Label_NOMBRE_CONTACTO_SELECCION" runat="server" Text="Nombre del contacto de seleccion"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td_izq">
                                                            E-Mail:
                                                        </td>
                                                        <td class="td_der">
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
                                                                <asp:ListItem Value="ARCHIVOS_AFILIACIONES">Archivos Afiliaciones</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="div_espaciador">
                                                </div>
                                                <table class="table_control_registros">
                                                    <tr>
                                                        <td class="td_izq">
                                                            Contácto:
                                                        </td>
                                                        <td class="td_der">
                                                            <asp:Label ID="Label_NOMBRE_CONTACTO_CONTRATACION" runat="server" Text="Nombre del Contácto de Contratación"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            E-Mail:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox_EMAIL_CONTRATACION" runat="server" Width="300px" ValidationGroup="ENVIAR"></asp:TextBox>
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
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            
                            <div class="div_espaciador"></div>
                            
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_REVISAR_DOCUMENTOS" runat="server" Text="Revisar"
                                            ValidationGroup="REVISARENVIO" CssClass="margin_botones" 
                                            onclick="Button_REVISAR_DOCUMENTOS_Click"/>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_ACTUALIZAR_ENVIOARCHIVOS" runat="server" Text="Enviar" CssClass="margin_botones"
                                            ValidationGroup="ENVIAR" onclick="Button_ACTUALIZAR_ENVIOARCHIVOS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>


                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ENVIOARCHIVOS" runat="Server"
                            TargetControlID="Panel_CONTENIDO_ENVIOARCHIVOS" ExpandControlID="Panel_CABEZA_ENVIOARCHIVOS"
                            CollapseControlID="Panel_CABEZA_ENVIOARCHIVOS" Collapsed="True" TextLabelID="Label_BOTON_ENVIOARCHIVOS"
                            ImageControlID="Image_BOTON_ENVIOARCHIVOS" ExpandedText="(Ocultar detalles...)"
                            CollapsedText="(Mostrar detalles...)" ExpandedImage="~/imagenes/plantilla/collapse.jpg"
                            CollapsedImage="~/imagenes/plantilla/expand.jpg" SuppressPostBack="true">
                        </ajaxToolkit:CollapsiblePanelExtender>


                        <asp:Panel ID="Panel_INFORMACION_FINAL" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox">
                                Información Final
                            </div>
                            <div class="div_contenido_groupbox">
                                <div class="div_aviso_terminacion_audtoria">
                                    Si ya termino la actualización de las secciones de auditoría, puede cerrar el proceso
                                    de auditoría para este contrato utilizando el botón <b>Contrato auditado</b>
                                </div>
                            </div>
                        </asp:Panel>


                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button_Actualizar" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALIZAR_AFILIACION_ARP" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALIZAR_AFILIACION_EPS" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALIZAR_AFILIACION_CAJA_C" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALIZAR_AFILIACION_F_PENSIONES" />
                        <asp:PostBackTrigger ControlID="GridView_LISTA_CLAUSULAS_PARAMETRIZADAS" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALZIAR_CONCEPTOS_FIJOS"/>
                        <asp:PostBackTrigger ControlID="Button_ACTUALZIAR_CONTRATO" />
                        <asp:PostBackTrigger ControlID="Button_ACTUALIZAR_EXAMENES" />
                        <asp:PostBackTrigger ControlID="Button_IMPRIMIR_AUTOS" />
                        <asp:PostBackTrigger ControlID="Button_REVISAR_DOCUMENTOS" />
                        <asp:PostBackTrigger ControlID="Button_ProcesarNuevaClausula" />
                        <asp:PostBackTrigger ControlID="Button_ACEPTAR_AVISO" />
                        <asp:PostBackTrigger ControlID="Button_NO_AVISO" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_FORM_BOTONES_1" runat="server">
        <div class="div_contenedor_formulario">
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
                                            <asp:Button ID="Button_AUDITADO_1" runat="server" Text="Contrato auditado" ValidationGroup="AUDITAR"
                                                CssClass="margin_botones" OnClick="Button_AUDITADO_Click" />
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver al menú" ValidationGroup="VOLVER"
                                                CssClass="margin_botones" OnClick="Button_VOLVER_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="div_espaciador">
        </div>
    </asp:Panel>
</asp:Content>
