
<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="hojaTrabajoContratacion.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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
    
    
    <asp:Panel ID="Panel_ObjetivosArea" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_objetivos_area">
                Control y aseguramiento a nivel nacional de la información de los procesos de contratación de personal,
                incapacidades, autoliquidación, retiros de personal y temporalidad.
            </div>
        </div>
    </asp:Panel>
    
    
    <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" style="margin:0 auto; text-align:center;">
        <div class="div_botones_internos">
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align:center;">
                        <asp:Table ID="Table_MENU" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:Table ID="Table_MENU_1" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:Table ID="Table_MENU_2" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:Table ID="Table_MENU_3" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div class="div_cabeza_groupbox">
                                    Sección de busqueda
                                </div>
                                <div class="div_contenido_groupbox">
                                    <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged">
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Button_BUSCAR" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>


    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td>
                            <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_rojo"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_Contrato_Vencido" runat="server" Text="0"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_azul"></div> 
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
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" Width="885px" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION,NUM_DOC_IDENTIDAD" 
                            onselectedindexchanged="GridView_HOJA_DE_TRABAJO_SelectedIndexChanged" >
                            <Columns>
                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                    ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:CommandField>
                                <asp:BoundField DataField="Regional" HeaderText="Regional"/>
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad"/>
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" Visible=false
                                    HeaderText="Tipo Documento Identidad" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identidad" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" Visible=false
                                    DataFormatString="$ {0:C}" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado" />
                                <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                <asp:BoundField DataField="fechaPorContrar" 
                                    HeaderText="Fecha Enviado a Contratar" DataFormatString="{0:dd/M/yyyy}" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="div_espaciador"></div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

