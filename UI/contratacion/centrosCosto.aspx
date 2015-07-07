<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="centrosCosto.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--PANEL CONTENEDOR DE LA VENTANA PROCESAMIENTO--%>
    <asp:UpdateProgress ID="UpdateProgress_Procesamiento" runat="server" style="display: none">
        <ProgressTemplate>
            <%--CONTENEDOR DE LA VENTANA PROCESAMIENTO--%>
            <asp:Panel ID="Panel_ContenedorProcesamiento" runat="server">
                <%--FONDO DE LA VENTANA DE PROCESAMIENTO--%>
                <asp:Panel ID="Panel_FondoProcesamiento" runat="server" CssClass="conf_panel_fondo_ventana_emergente">
                </asp:Panel>
                <%--VENTANA EMERGENTE CON MENSAJE--%>
                <asp:Panel ID="Panel_VentanaProcesamiento" runat="server" CssClass="conf_panel_ventana_emergente">
                    <div style="border: 2px solid #006600; height: 210px; margin: 2px;">
                        <div style="border: 1px solid #006600; height: 204px; margin: 2px;">
                            <div style="height: 75px;">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_label_ventana_procesando" style="text-align: center; color: #BBBBBB;
                                        font-weight: bold; font-size: 130%;">
                                        <asp:Label ID="Label_Procesamiento" runat="server" CssClass="label_ventana_procesando">Procesando...</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="Image_Procesamiento" runat="server" CssClass="img_ventana_emergente"
                                            Height="19px" ImageUrl="~/imagenes/loading11.gif" Width="220px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel_Procesamiento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                        <td>
                            <div style="margin: 0 auto; display: block;">
                                <div class="div_cabeza_groupbox">
                                    Botones de acción
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td colspan="0">
                                                <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                    OnClick="Button_NUEVO_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                    OnClick="Button_GUARDAR_Click" ValidationGroup="CC" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                    OnClick="Button_MODIFICAR_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    OnClick="Button_CANCELAR_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_LISTA_CONTRATOS" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                                    OnClick="Button_LISTA_CONTRATOS_Click" />
                                            </td>
                                            <td colspan="0">
                                                <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos de Centros de Costos
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_DATOS" runat="server" Width="885px" OnSelectedIndexChanged="GridView_CONTRATOS_SERVICIO_SelectedIndexChanged"
                                    AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView_DATOS_PageIndexChanging"
                                    DataKeyNames="ID_CENTRO_C">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_CENTRO_C" HeaderText="ID_CENTRO_C" Visible="False" />
                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre ciudad" />
                                        <asp:BoundField DataField="NOM_CC" HeaderText="Nombre centro costo" />
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
                        Centro de Costo Seleccionado
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
                                            <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_IDENTIFICADOR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Identificador de Centro de Costo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Identificador
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_REGISTRO" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_CENTRO_COSTO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Información Ciudad y Centro de Costo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Ciudad
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_CIUDAD" runat="server" ValidationGroup="CC">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            Centro de costo
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NOMBRE_CENTRO_COSTO" runat="server" Width="200px" ValidationGroup="CC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_CIUDAD -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD"
                                    ControlToValidate="DropDownList_CIUDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIIUDAD es requerida."
                                    ValidationGroup="CC" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD"
                                    TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD" HighlightCssClass="validatorCalloutHighlight" />
                                <!-- TextBox_NOMBRE_CENTRO_COSTO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOMBRE_CENTRO_COSTO"
                                    ControlToValidate="TextBox_NOMBRE_CENTRO_COSTO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE DE CC es requerido."
                                    ValidationGroup="CC" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOMBRE_CENTRO_COSTO"
                                    TargetControlID="RequiredFieldValidator_TextBox_NOMBRE_CENTRO_COSTO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_INFORMACION_ADICIONAL_CC" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Configuración de Centro de Costo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Periodo de Pago
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_PERIODO_PAGO" runat="server" Width="250px" ValidationGroup="CC">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Entidad Bancaria
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_BANCO" runat="server" Width="250px" ValidationGroup="CC">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="CheckBox_EXCENTO_IVA" runat="server" Text="Exento de IVA" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- DropDownList_PERIODO_PAGO -->
                                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_PERIODO_PAGO"
                                ControlToValidate="DropDownList_PERIODO_PAGO"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PERIODO DE PAGO es requerido." 
                            ValidationGroup="CC" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_PERIODO_PAGO"
                                TargetControlID="RequiredFieldValidator_DropDownList_PERIODO_PAGO"
                                HighlightCssClass="validatorCalloutHighlight" />--%>
                                <!-- DropDownList_BANCO -->
                                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BANCO"
                                ControlToValidate="DropDownList_BANCO"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El BANCO es requerido." 
                            ValidationGroup="CC" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BANCO"
                                TargetControlID="RequiredFieldValidator_DropDownList_BANCO"
                                HighlightCssClass="validatorCalloutHighlight" />--%>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_OcultarCC" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Ocultar Centro de Costo?
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_OcultarCC" runat="server" Text="Ocultar Centro de Costo" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORM_BOTONES_1" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div style="margin: 0 auto; display: block;">
                                <div class="div_cabeza_groupbox">
                                    Botones de Acción
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td colspan="0">
                                                <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                    OnClick="Button_NUEVO_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                    OnClick="Button_GUARDAR_Click" ValidationGroup="CC" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                    OnClick="Button_MODIFICAR_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    OnClick="Button_CANCELAR_Click" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver al menú" CssClass="margin_botones"
                                                    OnClick="Button_LISTA_CONTRATOS_Click" />
                                            </td>
                                            <td colspan="0">
                                                <input id="Button_SALIR" type="button" value="Salir" onclick="window.close();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel_SUB_CENTROS" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Sub Centros de Costo
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_GRID_SUB_CENTROS" runat="server">
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_SUB_C" runat="server" Width="885px" AutoGenerateColumns="False"
                                        DataKeyNames="ID_SUB_C,ID_EMPRESA,ID_CENTRO_C" OnSelectedIndexChanged="GridView_SUB_C_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField HeaderText="Actualizar" InsertText="Actualizar" SelectText="Actualizar"
                                                ShowSelectButton="True" />
                                            <asp:BoundField DataField="ID_SUB_C" HeaderText="ID_SUB_C" Visible="False" />
                                            <asp:BoundField DataField="COD_SUB_C" HeaderText="Cód sub centro" />
                                            <asp:BoundField DataField="NOM_SUB_C" HeaderText="Nombre" />
                                            <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                            <asp:BoundField DataField="ID_CENTRO_C" HeaderText="ID_CENTRO_C" Visible="False" />
                                            <asp:BoundField DataField="USU_CRE" HeaderText="Usuario crea." />
                                            <asp:BoundField DataField="FCH_CRE" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha crea." />
                                            <asp:BoundField DataField="USU_MOD" HeaderText="Usuario mod." />
                                            <asp:BoundField DataField="FCH_MOD" DataFormatString="{0:dd/M/yyyy}" HeaderText="Fecha mod." />
                                        </Columns>
                                        <headerStyle BackColor="#DDDDDD" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVO_SUB_C" runat="server" Text="Nuevo sub centro de costo"
                                        OnClick="Button_NUEVO_SUB_C_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
