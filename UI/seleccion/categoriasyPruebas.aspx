<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="categoriasyPruebas.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_contenedor_formulario">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                        <div class="div_cabeza_groupbox">
                            Botones de acción
                        </div>
                        <div class="div_contenido_groupbox">
                            <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td>
                                    <asp:Button ID="Button_NUEVA_CATEGOORIA" runat="server" Text="Nueva Categoría" 
                                        CssClass="margin_botones" onclick="Button_NUEVA_CATEGOORIA_Click"/>
                                </td>
                                <td>
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <div class="div_contenedor_formulario">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <div class="div_espacio_validacion_campos">
                        <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>           
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" 
                    Text="Categorías actuales"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                            OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" OnSelectedIndexChanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged"
                            AutoGenerateColumns="False" DataKeyNames="ID_CATEGORIA">
                            <Columns>
                                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                    ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:CommandField>
                                <asp:BoundField DataField="NOM_CATEGORIA" HeaderText="Nombre" />
                                <asp:BoundField DataField="OBS_CATEGORIA" HeaderText="Observaciones" >
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_CATEGORIA" HeaderText="ID_CATEGORIA" Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO_CATEGORIAS" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_espaciador"></div>
            <div class="div_cabeza_groupbox">
                Información de la Categoría Seleccionada
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
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                
                <asp:Panel ID="Panel_ID_CATEGORIA" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Identificador
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Identificador
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_ID_CATEGORIA" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>

                <asp:Panel ID="Panel_DATOS_CATEGORIA" runat="server">
                    <div style="margin: 0 auto; display: block; width: 580px;">
                        <div class="div_cabeza_groupbox_gris">
                            Datos
                        </div>
                        <div class="div_contenido_groupbox_gris">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_NOM_FUENTE" runat="server" Text="Nombre Categoría"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_NOM_CATEGORIA" runat="server" Width="400px" MaxLength="50"
                                            ValidationGroup="CATEGORIA"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label_OBSERVACIONES" runat="server" Text="Observaciones"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox_Observaciones" runat="server" Width="509px" TextMode="MultiLine"
                                            ValidationGroup="CATEGORIA" Height="132px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_NOM_CATEGORIA -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NOM_CATEGORIA"
                                ControlToValidate="TextBox_NOM_CATEGORIA" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NOMBRE es requerido."
                                ValidationGroup="CATEGORIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NOM_CATEGORIA"
                                TargetControlID="RequiredFieldValidator_TextBox_NOM_CATEGORIA" HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_NOM_CATEGORIA"
                                runat="server" TargetControlID="TextBox_NOM_CATEGORIA" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                ValidChars=" " />

                            <!-- TextBox_Observaciones -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Observaciones"
                                ControlToValidate="TextBox_Observaciones" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Las OBSERVACIONES son requeridas."
                                ValidationGroup="CATEGORIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Observaciones"
                                TargetControlID="RequiredFieldValidator_TextBox_Observaciones" HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Observaciones"
                                runat="server" TargetControlID="TextBox_Observaciones" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                 InvalidChars="'´/\" ValidChars="().:;_-!¡?¿<> " />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_CATEGORIA" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_espaciador"></div>
            <div class="div_cabeza_groupbox">
                Botones de acción -Categorías-
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Button ID="Button_NUEVA_CATEGORIA" runat="server" Text="Nueva Categoría" 
                                CssClass="margin_botones" onclick="Button_NUEVA_CATEGORIA_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_MODIFICAR_CATEGORIA" runat="server" Text="Modificar Categoría" 
                                CssClass="margin_botones" onclick="Button_MODIFICAR_CATEGORIA_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_GUARDAR_CATEGORIA" runat="server" Text="Guardar Categoría" 
                                CssClass="margin_botones" onclick="Button_GUARDAR_CATEGORIA_Click" 
                                ValidationGroup="CATEGORIA" />
                        </td>
                        <td>
                            <asp:Button ID="Button_CANCELAR_CATEGORIA" runat="server" Text="Cancelar" 
                                CssClass="margin_botones" onclick="Button_CANCELAR_CATEGORIA_Click" />
                        </td>
                        <td>
                            <input class="margin_botones" id="Button1" onclick="window.close();" type="button"
                                value="Salir" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_MENSAJES_PRUEBAS" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_espacio_validacion_campos">
                <asp:Label ID="Label_MENSAJES_PRUEBAS" runat="server" ForeColor="Red" />
            </div> 
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO_PRUEBAS" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                INformación de Pruebas Asociadas a la Categoría</div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_PRUEBAS_CONFIGURADAS_ACTUALMENTE" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Asociadas Actualmente</div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros" width="750">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_GRILLA_PRUEBAS" runat="server" Width="750px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_PRUEBA,ID_CATEGORIA" 
                                                onrowcommand="GridView_GRILLA_PRUEBAS_RowCommand" >
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" HeaderText="Actualizar" 
                                                        ImageUrl="~/imagenes/areas/edit.gif" CommandName="modificar">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ID_PRUEBA" HeaderText="ID_PRUEBA" Visible="False" />
                                                    <asp:BoundField DataField="ID_CATEGORIA" HeaderText="ID_CATEGORIA" Visible="False" />
                                                    <asp:BoundField DataField="NOM_PRUEBA" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="OBS_PRUEBA" HeaderText="Observaciones" />
                                                    <asp:TemplateField HeaderText = "Manual">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink_MANUAL_ADJUNTO" runat="server">Ver manual</asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField> 
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="Panel_NUEVA_PRUEBA" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Información Prueba Seleccionada</div>
                    <div class="div_contenido_groupbox_gris">
                                    
                        <asp:Panel ID="Panel_ID_PRUEBA" runat="server">
                            <div class="div_cabeza_groupbox_gris">
                                Identificador
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Identificador:
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID_PRUEBA" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        
                        <table class="table_control_registros" cellpadding="0">
                            <tr>
                                <td class="td_izq">
                                    Nombre:
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NOM_PRUEBA" runat="server" Width="300px" ValidationGroup="EFECTIVIDAD"
                                        MaxLength="255"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Comentarios<br />
                                    <asp:TextBox ID="TextBox_Comentarios" runat="server" Width="388px" ValidationGroup="EFECTIVIDAD"
                                        TextMode="MultiLine" Height="88px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel_ARCHIVO_ACTUAL" runat="server">
                            <table class="table_control_registros" width="400">
                                <tr>
                                    <td style="text-align:right; padding-right:10px;">
                                        Descargar archivo: 
                                        <asp:HyperLink ID="HyperLink_ARCHIVO_PRUEBA_SELECCIONADA" runat="server" 
                                            NavigateUrl="http://">Clic aquí</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_FILEUPLOAD_ARCHIVO" runat="server">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Manual de la Prueba
                                    </td>
                                    <td class="td_der">
                                        <asp:FileUpload ID="FileUpload_ARCHIVO" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </asp:Panel>

            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_PRUEBAS" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Botones de acción -Pruebas-
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Button ID="Button_NNUEVA_PRUEBA" runat="server" Text="Nueva Prueba" 
                                CssClass="margin_botones" onclick="Button_NNUEVA_PRUEBA_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_GUARDAR_PRUEBA" runat="server" Text="Guardar Prueba" 
                                CssClass="margin_botones" onclick="Button_GUARDAR_PRUEBA_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button_CANCELAR_PRUEBA" runat="server" Text="Cancelar" 
                                CssClass="margin_botones" onclick="Button_CANCELAR_PRUEBA_Click" />
                        </td>
                        <td>
                            <input class="margin_botones" id="Button_SALIR_PRUEBA" onclick="window.close();" type="button"
                                value="Salir" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

</asp:Content>

