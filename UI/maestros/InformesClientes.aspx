<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="InformesClientes.aspx.cs" Inherits="_InformeClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
            <asp:HiddenField ID="HiddenField_DROP_BUSQUEDA" runat="server" />
            <asp:HiddenField ID="HiddenField_DATO_BUSQUEDA" runat="server" />

            <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
                <div class="div_contenedor_formulario">
                    <div id="div_info_adicional_modulo">
                        <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
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
                                ValidChars=" -_.,:;ÑñáéíóúÁÉÍÓÚ" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="Panel_ResultadosBusquedaEmpresas" runat="server">
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Registros Encontrados
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_EmpresasEncontradas" runat="server" Width="880px" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="ID" 
                                    OnPageIndexChanging="GridView_EmpresasEncontradas_PageIndexChanging" 
                                    onrowcommand="GridView_EmpresasEncontradas_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                            ImageUrl="~/imagenes/areas/view2.gif" Text="Seleccionar">
                                        <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="Raz. social" HeaderText="Raz. social" />
                                        <asp:BoundField DataField="NIT" HeaderText="NIT">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tel 1" HeaderText="Tel 1">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Celular" HeaderText="Celular">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="Activo">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Código" HeaderText="Código">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_EmpresaSeleccionada" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Empresa Seleccionada
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Razón Social:
                                </td>
                                <td>
                                    <asp:Label ID="Label_RazonSocial" runat="server" Text="Nombre de la empresa seleccionada" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    NIT:
                                </td>
                                <td>
                                    <asp:Label ID="Label_NitEmpresa" runat="server" Text="Nit de la empresa" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_PROGRAMA,ID_PROGRAMA_GENERAL,ID_EMPRESA,ID_AREA,ID_PRESUPUESTO" 
                                    OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="imprimir" ImageUrl="~/imagenes/plantilla/printer_view.gif"
                                            Text="Imprimir">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ANNO" HeaderText="Año">
                                        <ItemStyle CssClass="columna_grid_centrada" Width="40px"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TITULO" HeaderText="Programa">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Fecha Inicial">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_FechaInicial" runat="server" Width="120px" AutoPostBack="True"
                                                    OnTextChanged="TextBox_Fechas_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaInicial" runat="server"
                                                    TargetControlID="TextBox_FechaInicial" Format="dd/MM/yyyy" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FechaInicial"
                                                    runat="server" TargetControlID="TextBox_FechaInicial" FilterType="Custom,Numbers"
                                                    ValidChars="/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="122px"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Final">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_FechaFinal" runat="server" Width="120px" 
                                                    AutoPostBack="True" ontextchanged="TextBox_Fechas_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FechaFinal" runat="server"
                                                    TargetControlID="TextBox_FechaFinal" Format="dd/MM/yyyy" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FechaFinal"
                                                    runat="server" TargetControlID="TextBox_FechaFinal" FilterType="Custom,Numbers"
                                                    ValidChars="/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="122px"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conclusiones">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Conclusiones" runat="server" Width="360px" TextMode="MultiLine" Height="80px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="362px"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

