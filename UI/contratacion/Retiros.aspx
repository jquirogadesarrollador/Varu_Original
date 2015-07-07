<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="Retiros.aspx.cs" Inherits="contratacion_Retiros" %>

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

            <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <div class="div_cabeza_groupbox">
                                Botones de Acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="Actualizar" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_RevertirRetiro" runat="server" Text="Reversar" CssClass="margin_botones"
                                                ValidationGroup="REVERSAR" onclick="Button_RevertirRetiro_Click" />
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
                            <div class="div_cabeza_groupbox">
                                Sección de Busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="buscar"
                                                OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <td>
                                                <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA">
                                                </asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                    ValidationGroup="buscar" CssClass="margin_botones" />
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
                        </td>
                    </tr>
                </table>

                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_Button_RevertirRetiro" runat="server" TargetControlID="Button_RevertirRetiro"
                    ConfirmText="Esta Seguro de la reversión de este retiro Temporal?" />


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
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Resultados de la Busqueda
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                                    AutoGenerateColumns="False" DataKeyNames="id_empleado">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="regional" HeaderText="Regional">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="empresa" HeaderText="Empresa">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_empleado" HeaderText="Id Empleado" Visible="false">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="activo" HeaderText="Act.">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_documento" HeaderText="Documento">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="salario" HeaderText="Salario">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_ingreso" HeaderText="F. Ingreso">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_retiro" HeaderText="F. Retiro">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_liquidacion" HeaderText="F. Liq">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="caso_severo" HeaderText="Sev.">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="embarazada" HeaderText="Emb.">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DATOS_RETIROS" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Información del Trabajador
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenField_Archivo" runat="server" />
                                </td>
                                <td>
                                </td>
                                <td class="td_izq">
                                    Activo
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_activo" runat="server" Width="200" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Regional
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_regional" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    Ciudad
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_ciudad" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Empresa
                                </td>
                                <td colspan="3" class="td_der">
                                    <asp:TextBox ID="TextBox_empresa" runat="server" Width="520" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Id empleado
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_id_empleado" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    No. Documento
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_numero_documento" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Apellidos
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_apellidos" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    Nombres
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_nombre" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Fecha de ingreso
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_fecha_ingreso" runat="server" Width="200" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    Fecha de liquidación
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_fecha_liquidacion" runat="server" Width="200" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Cargo
                                </td>
                                <td colspan="3" class="td_der">
                                    <asp:TextBox ID="TextBox_cargo" runat="server" Width="520" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Caso severo
                                </td>
                                <td class="td_der">
                                    <asp:CheckBox ID="CheckBox_caso_severo" runat="server" Enabled="false" />
                                </td>
                                <td class="td_izq">
                                    Embarazada
                                </td>
                                <td class="td_der">
                                    <asp:CheckBox ID="CheckBox_embarazada" runat="server" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <asp:Panel ID="Panel_informacionRetiro" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_contenedor_formulario">
                        <div class="div_cabeza_groupbox">
                            Información del Retiro
                        </div>
                        <div class="div_contenido_groupbox">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de Retiro
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_fecha_retiro" runat="server" Width="200"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha_retiro" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="TextBox_fecha_retiro" />
                                    </td>
                                    <td class="td_izq">
                                        Estado
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_estado" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Notas de retiro
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_notas" runat="server" Width="520px" Height="118px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_Carpeta" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_contenedor_formulario">
                        <div class="div_cabeza_groupbox">
                            Carpeta
                        </div>
                        <div class="div_contenido_groupbox">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Ubicación de la carpeta
                                    </td>
                                    <td colspan="3" class="td_der">
                                        <asp:TextBox ID="TextBox_carpeta" runat="server" Width="520px" Height="83px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_fecha_retiro -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_fecha_retiro" runat="server"
                                ControlToValidate="TextBox_fecha_retiro" Display="None" ErrorMessage="Campo Requerido faltante</br>La FECHA RETIRO es requerida."
                                ValidationGroup="Actualizar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_retiro"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_fecha_retiro" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_fecha_retiro"
                                runat="server" TargetControlID="TextBox_fecha_retiro" FilterType="Custom,Numbers"
                                ValidChars="/">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <!-- DropDownList_estado-->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_estado"
                                ControlToValidate="DropDownList_estado" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido."
                                ValidationGroup="Actualizar" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_estado"
                                TargetControlID="RequiredFieldValidator_DropDownList_estado" HighlightCssClass="validatorCalloutHighlight" />
                            <%--TextBox_notas--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_notas" runat="server"
                                ControlToValidate="TextBox_notas" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;Las NOTAS son requeridas."
                                ValidationGroup="Actualizar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_notas"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_notas" />
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
