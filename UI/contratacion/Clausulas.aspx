<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="Clausulas.aspx.cs" Inherits="contratacion_Clausulas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="HiddenField_id_clausula" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />

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
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo"  
                                        CssClass="margin_botones" onclick="Button_NUEVO_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="Adicionar"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_ACTUALIZAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" 
                                        onclick="Button_ACTUALIZAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>

                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel_busqueda" runat="server">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" 
                                                ValidationGroup="BUSCAR_REFERENCIA">
                                                </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="BUSCAR_REFERENCIA" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_Buscar"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
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
                        OnClick="Button_CERRAR_MENSAJE_Click" style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="div_espaciador">
    </div>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" 
                                    AllowPaging="True" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="ID_CLAUSULA" 
                                    onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="ID_CLAUSULA" HeaderText="ID" Visible="false">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_CLAUSULA" HeaderText="Tipo" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Ocupación">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO">
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA"/>
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_Informacion_Clausula" runat="server">
        <div>
            <asp:Panel ID="Panel_Clausulas" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Clausula
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_cabeza_groupbox_gris">
                            Información de la clausula
                        </div>
                        <div class="div_espaciador">
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Empresa:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_ID_EMPRESA" runat="server" Text="" Width = "500px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Tipo de clausula:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ID_TIPO_CLAUSULA" runat="server" Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Estado:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ID_ESTADO" runat="server" Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Descripción de la clausula:
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_DESCRIPCION" runat="server" Height="99px" 
                                            TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Ocupación aplica la clausula:
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ID_OCUPACION" runat="server" Width="500px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Cargar archivo(Word) de la clausula:
                                    </td>
                                    <td class="td_der">
                                        <asp:FileUpload ID="FileUpload_archivo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Archivo:
                                    </td>
                                    <td class="td_der">
                                        <asp:HyperLink ID="HyperLink_ARCHIVO" runat="server">Clic aquí para decargar.</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_TIPO_CLAUSULA" runat="server"
                            ControlToValidate="DropDownList_ID_TIPO_CLAUSULA" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El TIPO DE CLAUSULA es requerido."
                            ValidationGroup="Adicionar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_TIPO_CLAUSULA"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_TIPO_CLAUSULA" />

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_ESTADO" runat="server"
                            ControlToValidate="DropDownList_ID_ESTADO" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ESTADO es requerido."
                            ValidationGroup="Adicionar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_ESTADO"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_ESTADO" />

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_DESCRIPCION" runat="server"
                            ControlToValidate="TextBox_DESCRIPCION" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La DESCRIPCION es requerida."
                            ValidationGroup="Adicionar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_DESCRIPCION"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_DESCRIPCION" />
                            
<%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ID_OCUPACION" runat="server"
                            ControlToValidate="DropDownList_ID_OCUPACION" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La OCUPACIÓN es requerida."
                            ValidationGroup="Adicionar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ID_OCUPACION"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ID_OCUPACION" />
--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FileUpload_archivo" runat="server"
                            ControlToValidate="FileUpload_archivo" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ARCHIVO es requerido."
                            ValidationGroup="Adicionar" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FileUpload_archivo"
                            runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_FileUpload_archivo" />

                            <div class="div_espaciador">
                            </div>
                        </div>
                        
                        <div class="div_espaciador">
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>

</asp:Content>

