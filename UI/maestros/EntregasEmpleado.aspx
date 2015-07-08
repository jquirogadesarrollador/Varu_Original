<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="EntregasEmpleado.aspx.cs" Inherits="_EntregasEmpleados" %>

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

            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />       
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

            <asp:HiddenField ID="HiddenField_ID_EMPLEADO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_CONTRATO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_CIUDAD" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_OCUPACION" runat="server" />

            <asp:HiddenField ID="HiddenField_ID_DOCUMENTO_ENTREGA" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
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
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                ValidationGroup="GUARDAR" OnClick="Button_GUARDAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_IMPRIMIR" runat="server" Text="Imprimir" CssClass="margin_botones"
                                                ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
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
                                ValidChars=" -_.,:;ÑñáéíóúÁÉÍÓÚ" />
                        </td>
                    </tr>
                </table>
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
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_EMPLEADO,ID_CONTRATO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION,ID_CIUDAD" AllowPaging="True"
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px"/>
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="DOCUMENTO_IDENTIDAD" HeaderText="Doc. Identidad" />
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            
            <asp:Panel ID="Panel_InformacionEmpleadoSeleccionado" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Datos Empleado Seleccionado
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Nombres:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NombresEmpleado" runat="server" 
                                        Text="Nombres del Empleado" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Doc. Identidad:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NumDocIdentidadEmpleado" runat="server" 
                                        Text="Documento Identidad" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Empresa:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_Empresa" runat="server" 
                                        Text="Nombre Empresa" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Cargo:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_Cargo" runat="server" 
                                        Text="Cargo Empleado" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="div_espaciador"></div>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Talla Camisa:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_TallaCamisa" runat="server" 
                                        Text="Talla Camisa" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Talla Pantalón:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_TallaPantalon" runat="server" 
                                        Text="Talla Pantalón" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Talla Zapatos:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_TallaZapatos" runat="server" 
                                        Text="Talla Zapatos" Font-Bold="True" Font-Italic="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_GrillaEntregasProximas" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Lista de Pendientes
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_Entregas" runat="server" Width="885px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_INDEX,ID_ASIGNACION_SC,ID_EMPLEADO,ID_PRODUCTO,CANTIDAD_ENTREGADA_INICIAL,NOMBRE_SERVICIO_COMPLEMENTARIO" 
                                    onrowcommand="GridView_Entregas_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                            Text="Seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px"/>
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Producto">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_NombreProducto" runat="server" Width="350px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Cantidad" runat="server" Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entregados">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_CantidadEntregada" runat="server" Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Proyectada">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_FechaProyectada" runat="server" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo Entrega">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_TipoEntrega" runat="server" Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_ConfiguracionProducto" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Configuración Producto
                    </div>
                    <div class="div_contenido_groupbox">

                        <asp:HiddenField ID="HiddenField_ID_INDEX" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_ASIGNACION_SC" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_PRODUCTO_SELECCIONADO" runat="server" />
                        <asp:HiddenField ID="HiddenField_CANTIDAD_TOTAL" runat="server" />
                        <asp:HiddenField ID="HiddenField_CANTIDAD_ENTREGADA" runat="server" />
                        <asp:HiddenField ID="HiddenField_FECHA_PROYECTADA_ENTREGA" runat="server" />
                        <asp:HiddenField ID="HiddenField_TIPO_ENTREGA" runat="server" />

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Producto:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NombreProducto" runat="server" Text="Nombre Producto Seleccionado" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Talla:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_TallaProducto" runat="server" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="DropDownList_TallaProducto_SelectedIndexChanged" 
                                        Width="100px" ValidationGroup="ADJUNTAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>                        
                        </table>
                        <!-- DropDownList_TallaProducto -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TallaProducto"
                            ControlToValidate="DropDownList_TallaProducto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La TALLA es requerida."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TallaProducto"
                            TargetControlID="RequiredFieldValidator_DropDownList_TallaProducto" HighlightCssClass="validatorCalloutHighlight" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Proveedor:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_Proveedor" runat="server" Width="500px" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="DropDownList_Proveedor_SelectedIndexChanged" ValidationGroup="ADJUNTAR">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_izq">
                                    Factura:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_Factura" runat="server" 
                                        AutoPostBack="True"
                                        Width="200px" 
                                        onselectedindexchanged="DropDownList_Factura_SelectedIndexChanged" 
                                        ValidationGroup="ADJUNTAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>                        
                        </table>
                        <!-- DropDownList_Proveedor -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Proveedor"
                            ControlToValidate="DropDownList_Proveedor" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PROVEEDOR es requerido."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Proveedor"
                            TargetControlID="RequiredFieldValidator_DropDownList_Proveedor" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- DropDownList_Factura -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Factura"
                            ControlToValidate="DropDownList_Factura" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FACTURA es requerida."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Factura"
                            TargetControlID="RequiredFieldValidator_DropDownList_Factura" HighlightCssClass="validatorCalloutHighlight" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Cantidad:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_CantidadProducto" runat="server" ValidationGroup="ADJUNTAR"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    Max:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CantidadMax" runat="server" Text="Cantidad Maxima" 
                                        Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Disponible:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CantidadDisponible" runat="server" 
                                        Text="Cantidad Disponible" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                                </td>
                            </tr>                        
                        </table>
                        <!-- TextBox_CantidadProducto -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_CantidadProducto"
                            ControlToValidate="TextBox_CantidadProducto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CANTIDAD es requerida."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CantidadProducto"
                            TargetControlID="RequiredFieldValidator_TextBox_CantidadProducto" HighlightCssClass="validatorCalloutHighlight" />
                        <asp:RangeValidator ID="RangeValidator_TextBox_CantidadProducto" runat="server" Display="None"
                            MinimumValue="0" MaximumValue="100" Type="Integer" ControlToValidate="TextBox_CantidadProducto"
                            ErrorMessage="<b>Campo con datos incorrectos</b><br />La CANTIDAD digitada excede la cantidad máxima o la existente."></asp:RangeValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_CantidadProducto1"
                            TargetControlID="RangeValidator_TextBox_CantidadProducto" HighlightCssClass="validatorCalloutHighlight" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_CantidadProducto"
                                runat="server" TargetControlID="TextBox_CantidadProducto" FilterType="Numbers" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_AdjuntarAEntrega" runat="server" 
                                        Text="Adjuntar Producto" onclick="Button_AdjuntarAEntrega_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_ConfiguracionEquipos" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Configuración Equipos
                    </div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_ID_INDEX_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_ASIGNACION_SC_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_PRODUCTO_SELECCIONADO_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_CANTIDAD_TOTAL_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_CANTIDAD_ENTREGADA_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_FECHA_PROYECTADA_ENTREGA_EQUIPOS" runat="server" />
                        <asp:HiddenField ID="HiddenField_TIPO_ENTREGA_EQUIPOS" runat="server" />


                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Producto:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_NombreProductoEquipos" runat="server" Text="Nombre Producto Seleccionado" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Talla:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_TallaEquipos" runat="server" 
                                        AutoPostBack="True"  
                                        Width="100px" ValidationGroup="ADJUNTAR" 
                                        onselectedindexchanged="DropDownList_TallaEquipos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>                        
                        </table>
                        <!-- DropDownList_TallaEquipos -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TallaEquipos"
                            ControlToValidate="DropDownList_TallaEquipos" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La TALLA es requerida."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TallaEquipos"
                            TargetControlID="RequiredFieldValidator_DropDownList_TallaEquipos" HighlightCssClass="validatorCalloutHighlight" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Proveedor:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_ProveedorEquipos" runat="server" Width="500px" 
                                        AutoPostBack="True" ValidationGroup="ADJUNTAR" 
                                        onselectedindexchanged="DropDownList_ProveedorEquipos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_izq">
                                    Factura:
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_FacturaEquipos" runat="server" 
                                        AutoPostBack="True"
                                        Width="200px"  
                                        ValidationGroup="ADJUNTAR" 
                                        onselectedindexchanged="DropDownList_FacturaEquipos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>                        
                        </table>
                        <!-- DropDownList_ProveedorEquipos -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ProveedorEquipos"
                            ControlToValidate="DropDownList_ProveedorEquipos" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PROVEEDOR es requerido."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ProveedorEquipos"
                            TargetControlID="RequiredFieldValidator_DropDownList_ProveedorEquipos" HighlightCssClass="validatorCalloutHighlight" />

                        <!-- DropDownList_FacturaEquipos -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_FacturaEquipos"
                            ControlToValidate="DropDownList_FacturaEquipos" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FACTURA es requerida."
                            ValidationGroup="ADJUNTAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_FacturaEquipos"
                            TargetControlID="RequiredFieldValidator_DropDownList_FacturaEquipos" HighlightCssClass="validatorCalloutHighlight" />

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Max:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CantidadMaxEquipos" runat="server" Text="Cantidad Maxima" 
                                        Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                                </td>
                                <td class="td_izq">
                                    Disponible:
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CantidadDisponibleEquipos" runat="server" 
                                        Text="Cantidad Disponible" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                                </td>
                            </tr>                        
                        </table>

                        <asp:Panel ID="Panel_GrillaEquipos" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_SeleccionarEquipos" runat="server" Width="884px" AutoGenerateColumns="False"
                                        DataKeyNames="ID_EQUIPO,ID_LOTE">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Marca">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_MARCA" runat="server" Width="110" ValidationGroup="GUARDARGRILLA"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_MARCA"
                                                        ControlToValidate="TextBox_MARCA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La MARCA es requerida."
                                                        ValidationGroup="GUARDARGRILLA" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_MARCA"
                                                        TargetControlID="RequiredFieldValidator_TextBox_MARCA" HighlightCssClass="validatorCalloutHighlight" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modelo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_MODELO" runat="server" Width="110" ValidationGroup="GUARDARGRILLA"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_MODELO"
                                                        ControlToValidate="TextBox_MODELO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MODELO es requerido."
                                                        ValidationGroup="GUARDARGRILLA" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_MODELO"
                                                        TargetControlID="RequiredFieldValidator_TextBox_MODELO" HighlightCssClass="validatorCalloutHighlight" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serie">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_SERIE" runat="server" Width="110"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMEI">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_IMEI" runat="server" Width="110"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Núm Celular">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_NUM_CELULAR" runat="server" Width="110"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NUM_CELULAR"
                                                        runat="server" TargetControlID="TextBox_NUM_CELULAR" FilterType="Numbers" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seleccionar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox_SeleccionEquipo" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox_SeleccionEquipo_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <headerStyle BackColor="#DDDDDD" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>

                        

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_AdjuntarEquipos" runat="server" 
                                        Text="Adjuntar Equipos" onclick="Button_AdjuntarEquipos_Click" />
                                </td>
                            </tr>
                        </table>



                        
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_AdjuntosAEntrega" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Lista de Productos Adjuntos a Entrega
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_AdjuntosAEntrega" runat="server" Width="883px" AutoGenerateColumns="False"
                                    
                                    DataKeyNames="ID_DETALLE_ENTREGA,ID_INDEX,ID_ASIGNACION_SC,ID_LOTE,ID_DOCUMENTO,ID_PRODUCTO,CANTIDAD_TOTAL,FECHA_PROYECTADA_ENTREGA,TIPO_ENTREGA" 
                                    onrowcommand="GridView_AdjuntosAEntrega_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="22px"/>
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Producto">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_NombreProducto" runat="server" Width="250px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Talla">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Talla" runat="server" Width="45px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Cantidad" runat="server" Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proveedor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_NombreProveedor" runat="server" Width="260px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Factura - Lote">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_FacturaLote" runat="server" Width="170px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_EquiposAdjuntosAEntrega" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Lista de Equipos Adjuntos a Entrega
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_EquiposAdjuntosAEntrega" runat="server" 
                                    Width="883px" AutoGenerateColumns="False"
                                    DataKeyNames="ID_DETALLE_ENTREGA,ID_INDEX,ID_ASIGNACION_SC,ID_LOTE,ID_DOCUMENTO,ID_PRODUCTO,ID_EQUIPO,FECHA_PROYECTADA_ENTREGA,TIPO_ENTREGA,CANTIDAD_TOTAL" 
                                    onrowcommand="GridView_EquiposAdjuntosAEntrega_RowCommand" >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="22px"/>
                                        </asp:ButtonField>

                                        <asp:TemplateField HeaderText="Marca / Modelo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_MARCA" runat="server" Width="140" ValidationGroup="GUARDARGRILLA"></asp:TextBox><br />
                                                    <asp:TextBox ID="TextBox_MODELO" runat="server" Width="140" ValidationGroup="GUARDARGRILLA"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serie">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_SERIE" runat="server" Width="120"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMEI / Num. Celular">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_IMEI" runat="server" Width="130"></asp:TextBox><br />
                                                    <asp:TextBox ID="TextBox_NUM_CELULAR" runat="server" Width="130"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proveedor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_NombreProveedor" runat="server" Width="240px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Factura - Lote">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_FacturaLote" runat="server" Width="140px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES_ABAJO" runat="server">
                <div class="div_espaciador">
                </div>
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
                                                <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                    OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    OnClick="Button_CANCELAR_Click" ValidationGroup="CANCELAR" />
                                            </td>
                                            <td colspan="0">
                                                <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones" ValidationGroup="IMPRIMIR" OnClick="Button_IMPRIMIR_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_IMPRIMIR_1" />
            <asp:PostBackTrigger ControlID="Button_IMPRIMIR" />
        </Triggers>
    </asp:UpdatePanel>

    

</asp:Content>

