<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="descartePersonal.aspx.cs" Inherits="_DescartePersonalSeleccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    

    <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />

    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

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
                        onclick="Button_CERRAR_MENSAJE_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:Panel ID="Panel_botones_accion" runat="server">
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
                                    <input class="margin_botones" id="Button_SALIR" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
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

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador"></div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                            AutoGenerateColumns="False" 
                            DataKeyNames="ID_SOLICITUD,ID_REQUERIMIENTO,NUM_DOC_IDENTIDAD" 
                            onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand" 
                            onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identificación">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SEXO" HeaderText="Sexo">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ARCHIVO" HeaderText="Estado">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_DATOS_SOLICITUD_INGRESO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Datos de la Solicitud de Ingreso
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Núm. Documento
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NUM_DOC_IDENTIDAD" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Nombre
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NOMBRE_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Estado
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_ESTADO_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador">
                </div>
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Ciudad
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_CIUDAD_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Dirección
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_DIRECCION_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Sector
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_SECTOR_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="div_espaciador">
                </div>
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Teléfono
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_TELEFONO_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="td_izq">
                            E-Mail
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_E_MAIL_SOLICITUD_INGRESO" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BITACORA_HOJA" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Bitacora de hoja de vida
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_BITACORA" runat="server" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="REGISTRO,ID_SOLICITUD" Width="885px">
                            <Columns>
                                <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" 
                                    Visible="False" />
                                <asp:BoundField DataField="FECHA_R" DataFormatString="{0:dd/M/yyyy}" 
                                    HeaderText="Fecha registro" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLASE_REGISTRO" HeaderText="Clase" />
                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" />
                                <asp:BoundField DataField="COMENTARIOS" HeaderText="Comentarios" >
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FCH_CRE" DataFormatString="{0:dd/M/yyyy}" 
                                    HeaderText="Fecha creación" >
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

    <asp:Panel ID="Panel_DESCARTE" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Descarte de Personal
            </div>
            <div class="div_contenido_groupbox">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel_TIPOS_DESCARTE" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Tipo de Descarte
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadioButtonList_TIPOS_DESCARTE" runat="server" 
                                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                                
                                                onselectedindexchanged="RadioButtonList_TIPOS_DESCARTE_SelectedIndexChanged" 
                                                ValidationGroup="REALIZARDESCARTE">
                                                <asp:ListItem Value="DESC. SELECCION">Proceso de Selección</asp:ListItem>
                                                <asp:ListItem Value="DESC. DESINTERES">Desinterés en la Oferta</asp:ListItem>
                                                <asp:ListItem Value="POR CLIENTE">Por Cliente</asp:ListItem>
                                                <asp:ListItem Value="DESC. OTROS">Otros</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <!-- RadioButtonList_TIPOS_DESCARTE -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RadioButtonList_TIPOS_DESCARTE"
                                    ControlToValidate="RadioButtonList_TIPOS_DESCARTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE DESCARTE es requerido."
                                    ValidationGroup="REALIZARDESCARTE" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_RadioButtonList_TIPOS_DESCARTE"
                                    TargetControlID="RequiredFieldValidator_RadioButtonList_TIPOS_DESCARTE" HighlightCssClass="validatorCalloutHighlight" />

                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel_MOTIVO_DESCARTE" runat="server">
                            <div class="div_espaciador"></div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Motivo
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_LISTA_MOTIVOS_DESCARTE" runat="server" 
                                            ValidationGroup="REALIZARDESCARTE">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_LISTA_MOTIVOS_DESCARTE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_LISTA_MOTIVOS_DESCARTE" ControlToValidate="DropDownList_LISTA_MOTIVOS_DESCARTE"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MOTIVO es requerido."
                                ValidationGroup="REALIZARDESCARTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_LISTA_MOTIVOS_DESCARTE"
                                TargetControlID="RequiredFieldValidator_DropDownList_LISTA_MOTIVOS_DESCARTE" HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>

                        <asp:Panel ID="Panel_OBSERVACIONES_DESCARTE" runat="server">
                            <div class="div_espaciador"></div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <div style="text-align:center;">
                                            Observaciones
                                        </div>
                                        <asp:TextBox ID="TextBox_OBSERVACIONES_DESCARTE" runat="server" Height="100px" 
                                            TextMode="MultiLine" Width="800px" ValidationGroup="REALIZARDESCARTE"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_OBSERVACIONES_DESCARTE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBSERVACIONES_DESCARTE" ControlToValidate="TextBox_OBSERVACIONES_DESCARTE"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES o COMENTARIOS son requeridos."
                                ValidationGroup="REALIZARDESCARTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBSERVACIONES_DESCARTE"
                                TargetControlID="RequiredFieldValidator_TextBox_OBSERVACIONES_DESCARTE" HighlightCssClass="validatorCalloutHighlight" />

                        </asp:Panel>

                        <div class="div_espaciador"></div>

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_DESCARTAR" runat="server" Text="Realizar Descarte" 
                                        onclick="Button_DESCARTAR_Click" ValidationGroup="REALIZARDESCARTE" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_CANCELAR_DESCARTE" runat="server" Text="Cancelar" 
                                        onclick="Button_CANCELAR_DESCARTE_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Button_DESCARTAR" />
                        <asp:PostBackTrigger ControlID="Button_CANCELAR_DESCARTE" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

