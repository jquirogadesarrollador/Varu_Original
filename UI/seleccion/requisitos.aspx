<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="requisitos.aspx.cs" Inherits="seleccion_requisitos" %>

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
            
            <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />

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
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones" 
                                                ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
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
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                ValidationGroup="BUSCAR_REFERENCIA" CssClass="margin_botones" />
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
                                    DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,NUM_DOC_IDENTIDAD"
                                    AllowPaging="True" 
                                    OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                                    onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                            ImageUrl="~/imagenes/areas/view2.gif" Text="Seleccionar">
                                        <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                        <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="No. Documento">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="TIPO_REQ" HeaderText="Req. tipo" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Apertura" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/M/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo Requerido" />
                                        <asp:BoundField DataField="FECHA_CIERRE" HeaderText="Fecha Cierre" DataFormatString="{0:dd/M/yyyy}">
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

            <asp:Panel ID="Panel_FORMULARIO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Requisitos para iniciar el proceos de contratación
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

                        <asp:Panel ID="Panel_candidato_contratar" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Información del Canditato a Contratar
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_NOMBRE_EMPRESA" runat="server" Text="Empresa"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="TextBox_NOMBRE_EMPRESA" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Número Requerimiento:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="LabelIdRequerimiento" runat="server" Text="ID_REQUERIMIENTO" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_CIUDAD" runat="server" Text="Ciudad"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="TextBox_CIUDAD" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Número documento:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_NumDocIdentidad" runat="server" Text="NUM_DOC_IDENTIDAD" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Nombre:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_Nombre" runat="server" Text="NOMBRES" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Cargo:
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label_Cargo" runat="server" Text="CARGO" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador">
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_requisitos" runat="server">
                            <div class="div_contenido_groupbox">
                                <div class="div_contenedor_grilla_resultados">
                                    <div class="grid_seleccionar_registros">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView_Documentos" runat="server" Width="860px" AutoGenerateColumns="False"
                                                        DataKeyNames="ID_CONTROL_REQUISITOS,ID_PRUEBA,ID_DOCUMENTO,DEROGADO">
                                                        <Columns>
                                                            <asp:BoundField DataField="TIPO_REQUISITO" HeaderText="Requisito">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento">
                                                                <ItemStyle CssClass="columna_grid_jus" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Cumplido">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox_Cumplido" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="columna_grid_centrada" Width="80px"/>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <headerStyle BackColor="#DDDDDD" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
                <div class="div_espaciador">
                </div>
                <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones" 
                                                ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

