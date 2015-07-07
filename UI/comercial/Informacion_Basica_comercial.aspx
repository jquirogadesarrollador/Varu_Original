<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="Informacion_Basica_comercial.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--VENTANA EMERGENTE CON MENSAJE--%>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField_REGISTRO_CONTRATO" runat="server" />
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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
                <div class="div_espaciador">
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Información básica comercial"></asp:Label>
                    </div>
                    <div>
                        <div>
                            <div  class="grid_seleccionar_registros" style="margin: 0 auto; display: block;
                                width: 800px;"> 
                                <asp:Panel id="div_GridViewAdministradorInfoBasicaComercia" runat="server">
                                    <div>
                                        <div>
                                            <table class="table_align_izq">
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Label ID="Label3" runat="server" Text="Ingresar un nuevo ítem en la tabla de registro"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txt_Nuevo_Registro" runat="server" Width="766px" Height="62px" 
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="Grabar" runat="server" 
                                                            ImageUrl="~/imagenes/plantilla/save.gif" onclick="Grabar_Click" 
                                                            style="height: 14px"  />
                                                    </td>
                                                </tr>
                                            </table>
                                    
                                        </div>
                                        <asp:GridView ID="GridViewAdministradorInfoBasicaComercia" runat="server" 
                                            Width="836px" AutoGenerateColumns="False"
                                            DataKeyNames="CODIGO" 
                                            onrowcommand="GridViewAdministradorInfoBasicaComercia_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/delete.gif" 
                                                    Text="_Borrar" CommandName="Borrar" >
                                                <ItemStyle Width="5px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/edit.gif" 
                                                    Text="_Editar" CommandName="Editar"  >
                                                <ItemStyle Width="5px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName = "Grabar" ButtonType="Image" ImageUrl="~/imagenes/plantilla/save.gif" 
                                                    Text="Botón" >
                                                <ItemStyle Width="5px" />
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="Codigo" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Codigo" runat="server" Text='<%# Bind("CODIGO") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCODIGO" runat="server" Text='<%# Bind("CODIGO") %>' Visible="False"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripción">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TXTDESCRIPCION" Text='<%# Bind("DESCRIPCION") %>' 
                                                            runat="server" Height="16px" Width="784px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXTDESCRIPCION" Text='<%# Bind("DESCRIPCION") %>' 
                                                            runat="server" Height="16px" Width="787px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1300px" Font-Size="5pt" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SINO" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TXT_SINO" runat="server" Text='<%# Bind("SINO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                            <asp:Label ID="TXT_SINO" runat="server" Text='<%# Bind("SINO") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="SINO" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TXT_ESTADO_REG" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                            <asp:Label ID="TXT_ESTADO_REG" runat="server"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <headerStyle BackColor="#DDDDDD" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="836px" AutoGenerateColumns="False"
                                    DataKeyNames="CODIGO" 
                                    onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand1">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/delete.gif" 
                                            Text="_Borrar" CommandName="Borrar" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/imagenes/plantilla/edit.gif" 
                                            Text="_Editar" CommandName="Editar"  />
                                        <asp:ButtonField CommandName = "Grabar" ButtonType="Image" ImageUrl="~/imagenes/plantilla/save.gif" 
                                            Text="Botón" />
                                        <asp:TemplateField HeaderText="Codigo" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="Codigo" runat="server" Text='<%# Bind("CODIGO") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCODIGO" runat="server" Text='<%# Bind("CODIGO") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                            <EditItemTemplate>
                                                <asp:Label ID="DESCRIPCION" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDESCRIPCION" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SI / NO">
<%--                                            <EditItemTemplate>
                                             <asp:Label ID="lblRadioButon" runat="server"></asp:Label>
                                                <asp:RadioButtonList ID="RadioButtonListSN" runat="server" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem>SI</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </EditItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRadioButon" runat="server"></asp:Label>
                                                <asp:RadioButtonList ID="RadioButtonListSN" runat="server" 
                                                    RepeatDirection="Horizontal" Height="16px" Width="98px">
                                                    <asp:ListItem>SI</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aclaración">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_ACLARACION" runat="server" Text='<%# Bind("ACLARACION") %>'
                                                    Height="17px" Width="350px" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TXT_ACLARACION" runat="server" Text='<%# Bind("ACLARACION") %>'
                                                    Height="17px" Width="350px" TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" Width="350px" Font-Size="7pt" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SINO" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="TXT_SINO" runat="server" Text='<%# Bind("SINO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                    <asp:Label ID="TXT_SINO" runat="server" Text='<%# Bind("SINO") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SINO" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="TXT_ESTADO_REG" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                    <asp:Label ID="TXT_ESTADO_REG" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="ACTUALIZAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                    ValidationGroup="NUEVOADICIONALCOMERCIAL" onclick="ACTUALIZAR_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                    ValidationGroup="CANCELARADICIONALCOMERCIAL" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                        Detalle del Contrato Comercial</div>
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
                        <asp:Panel ID="Panel_IDENTIFICADOR" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Empresa cliente
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            mpresa
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NUMERO_CONTRATO" runat="server" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorTextBox_NUMERO_CONTRATO"
                                    ControlToValidate="TextBox_NUMERO_CONTRATO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NÚMERO DE CONTRATO es requerido."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                    TargetControlID="RequiredFieldValidatorTextBox_NUMERO_CONTRATO" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_FECHAS_CONTRATO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Información básica comercial</div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            &nbsp;
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label1" runat="server" Text="Descripción"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            SI
                                        </td>
                                        <td class="td_der">
                                            NO
                                        </td>
                                        <td class="td_der">
                                            <asp:Label ID="Label2" runat="server" Text="Aclaración"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            &nbsp;
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="txt_descripcion" runat="server" MaxLength="10" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_SI" runat="server" />
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_NO" runat="server" />
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="txt_aclaracion" runat="server" Height="25px" Width="367px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_inicio_contrato_comercial"
                                    ControlToValidate="TextBox_inicio_contrato_comercial" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE INICIO es requerida."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_inicio_contrato_comercial"
                                    TargetControlID="RequiredFieldValidator_TextBox_inicio_contrato_comercial" HighlightCssClass="validatorCalloutHighlight" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_fecha_vencimiento_contrato_comercial"
                                    ControlToValidate="TextBox_fecha_vencimiento_contrato_comercial" Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE VENCIMIENTO es requerida."
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_fecha_vencimiento_contrato_comercial"
                                    TargetControlID="RequiredFieldValidator_TextBox_fecha_vencimiento_contrato_comercial"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                <asp:CompareValidator ID="CompareValidator_fechas_contrato" runat="server" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE INICIO NO puede ser menor que la FECHA DE VENCIMIENTO."
                                    Display="None" ControlToCompare="TextBox_inicio_contrato_comercial" ControlToValidate="TextBox_fecha_vencimiento_contrato_comercial"
                                    Operator="GreaterThan" Type="Date" ValidationGroup="ADICIONAR"></asp:CompareValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CompareValidator_fechas_contrato"
                                    TargetControlID="CompareValidator_fechas_contrato" HighlightCssClass="validatorCalloutHighlight" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_OBJETIVO_CONTRATO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Objeto del Contrato Comericial</div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Clase Servicio:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_TIPO_SERVICIO_RESPECTIVO" runat="server" Width="755px"
                                                ValidationGroup="ADICIONAR">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros">
                                    <tr>
                                        <td style="text-align: center;">
                                            Objeto de Contrato
                                            <br />
                                            <asp:TextBox ID="TextBox_DetalleServicioRespectivo" runat="server" Height="96px"
                                                TextMode="MultiLine" Width="838px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_FIRMADO" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Información (Firma)
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FIRMADO" runat="server" Text="Firmado"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_FIRMADO" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Enviado" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_cabeza_groupbox_gris">
                                Información (Enviado)
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_der">
                                            <asp:Label ID="Label_ENVIO_CTE" runat="server" Text="Enviado"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_ENVIO_CTE" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="Panel_HistorialEnvios" runat="server">
                                    <div class="div_espaciador">
                                    </div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Historial Envios y Devoluciones
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                        <asp:HiddenField ID="HiddenField_ID_ACCION" runat="server" />
                                        <asp:HiddenField ID="HiddenField_FECHA_ACCION" runat="server" />
                                        <asp:HiddenField ID="HiddenField_TIPO_ACCION" runat="server" />
                                        <asp:HiddenField ID="HiddenField_OBSERVACIONES" runat="server" />
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_HistorialEnvios" runat="server" Width="849px" AutoGenerateColumns="False"
                                                    DataKeyNames="ID_ACCION">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fecha Acción">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox_FECHA_ACCION" runat="server" ValidationGroup="ACCION" Width="110px"></asp:TextBox>
                                                                <%--TextBox_FECHA_ACCION--%>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FECHA_ACCION" runat="server"
                                                                    TargetControlID="TextBox_FECHA_ACCION" Format="dd/MM/yyyy" />
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_FECHA_ACCION"
                                                                    ControlToValidate="TextBox_FECHA_ACCION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FECHA DE LA ACCIÓN es requerida."
                                                                    ValidationGroup="ACCION" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_FECHA_ACCION"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_FECHA_ACCION" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="110px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList_TIPO_ACCION" runat="server" ValidationGroup="ACCION"
                                                                    Width="150px">
                                                                    <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                                                    <asp:ListItem Value="ENVIO">ENVIO</asp:ListItem>
                                                                    <asp:ListItem Value="DEVOLUCIÓN">DEVOLUCIÓN</asp:ListItem>
                                                                    <asp:ListItem Value="CORRECCIÓN">CORRECCIÓN</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--DropDownList_TIPO_ACCION--%>
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TIPO_ACCION"
                                                                    ControlToValidate="DropDownList_TIPO_ACCION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE ACCIÓN es requerido."
                                                                    ValidationGroup="ACCION" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TIPO_ACCION"
                                                                    TargetControlID="RequiredFieldValidator_DropDownList_TIPO_ACCION" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="110px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Observaciones">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TextBox_OBSERVACIONES_ACCION" runat="server" ValidationGroup="ACCION"
                                                                    TextMode="MultiLine" Height="60px" Width="555px"></asp:TextBox>
                                                                <%--TextBox_OBSERVACIONES_ACCION--%>
                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_OBSERVACIONES_ACCION"
                                                                    ControlToValidate="TextBox_OBSERVACIONES_ACCION" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVICIONES son requeridas."
                                                                    ValidationGroup="ACCION" />
                                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_OBSERVACIONES_ACCION"
                                                                    TargetControlID="RequiredFieldValidator_TextBox_OBSERVACIONES_ACCION" HighlightCssClass="validatorCalloutHighlight" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel_BotonesAccion" runat="server">
                                            <div class="div_espaciador">
                                            </div>
                                            <div style="text-align: left; margin-left: 10px;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="Button_NUEVA_ACCION" runat="server" Text="Nueva Acción" CssClass="margin_botones"
                                                                ValidationGroup="ACCION_NUEVA" OnClick="Button_NUEVA_ACCION_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button_GUARDAR_ACCION" runat="server" Text="Guardar Acción" CssClass="margin_botones"
                                                                ValidationGroup="ACCION" OnClick="Button_GUARDAR_ACCION_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button_CANCELAR_ACCION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                ValidationGroup="ACCION_CANCELAR" OnClick="Button_CANCELAR_ACCION_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
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
                                            <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                                ValidationGroup="NUEVO" OnClick="Button_NUEVO_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                                ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                        </td>
                                        <td rowspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                                OnClick="Button_GUARDAR_Click" ValidationGroup="ADICIONAR" />
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
