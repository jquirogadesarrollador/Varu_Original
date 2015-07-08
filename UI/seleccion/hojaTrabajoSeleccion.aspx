<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="hojaTrabajoSeleccion.aspx.cs" Inherits="_Default" %>

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

            <asp:Panel ID="Panel_ObjetivosArea" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_objetivos_area">
                        CUMPLIR LAS REQUISICIONES EN LOS TIEMPOS ESTABLECIDOS (OPORTUNIDAD) Y ENVIAR EL
                        PERSONAL ACORDE AL PERFIL SOLICITADO POR EL CLIENTE (EFECTIVIDAD).
                    </div>
                </div>
            </asp:Panel>

            <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
            <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />
            <asp:HiddenField ID="HiddenField_PAGINA_GRID" runat="server" />

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

            <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
                text-align: center;">
                <div class="div_espaciador">
                </div>
                <div class="div_botones_internos">
                    <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Table ID="Table_MENU" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Table ID="Table_MENU_1" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Table ID="Table_MENU_2" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <div class="div_cabeza_groupbox">
                                    Sección de Busqueda
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged"
                                                    CssClass="margin_botones">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE"
                                                    CssClass="margin_botones" ontextchanged="TextBox_BUSCAR_TextChanged"></asp:TextBox>
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
                                <!--TextBox_BUSCAR-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar."
                                    ValidationGroup="BUSCAR_CLIENTE" />
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
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_para_convencion_hoja_trabajo">
                        <table class="tabla_alineada_derecha">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label>
                                    &nbsp;Requisiciones creadas hoy.
                                </td>
                                <td class="td_der">
                                    <div class="div_color_verde">
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text=""></asp:Label>
                                    &nbsp;Requisiciones en plazo de entrega.
                                </td>
                                <td class="td_contenedor_colores_hoja_trabajo">
                                    <div class="div_color_amarillo">
                                    </div>
                                </td>
                                <td>
                                    <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text=""></asp:Label>
                                    &nbsp;Requisiciones con plazo de entrega vencido.
                                </td>
                                <td class="td_contenedor_colores_hoja_trabajo">
                                    <div class="div_color_rojo">
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
                                    DataKeyNames="ID_REQUERIMIENTO,COD_EMPRESA,COD_OCUPACION" 
                                    OnSelectedIndexChanged="GridView_HOJA_DE_TRABAJO_SelectedIndexChanged" 
                                    onprerender="GridView_HOJA_DE_TRABAJO_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="NUMERACION" HeaderText="Num">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" ButtonType="Image"
                                            SelectImageUrl="~/imagenes/plantilla/view2.gif" />
                                        <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="Num. Req.">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_EMPRESA" HeaderText="COD_EMPRESA" Visible="False" />
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                        <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_REQ" HeaderText="Req. Tipo" />
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha Apertura" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_REQUERIDA" HeaderText="Vence" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_OCUPACION" HeaderText="COD_OCUPACION" Visible="False" />
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo Req." />
                                        <asp:BoundField DataField="CANTIDAD" HeaderText="Req." />
                                        <asp:BoundField DataField="ENVIADOS" HeaderText="Env." />
                                        <asp:BoundField DataField="CONTRATAR" HeaderText="Por Cont." />
                                        <asp:BoundField DataField="DESCARTADOS" HeaderText="Desc." />
                                        <asp:BoundField DataField="FALTAN" HeaderText="Faltan" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>

