<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="unidadNegocio.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <asp:HiddenField ID="HiddenField_ID_EMPRESA_USUARIO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />

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
                        OnClick="Button_CERRAR_MENSAJE_Click" style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="ADICIONAR" onclick="Button_GUARDAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador"></div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Usuarios Asociados a Cliente
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" 
                                    AutoGenerateColumns="False" DataKeyNames="ID_EMPRESA_USUARIO,ID_USUARIO" 
                                    onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                            ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ID_EMPRESA_USUARIO" HeaderText="ID_EMPRESA_USUARIO" 
                                            Visible="False" />
                                        <asp:BoundField DataField="ID_USUARIO" HeaderText="ID_USUARIO" 
                                            Visible="False" />
                                        <asp:BoundField DataField="USU_LOG" HeaderText="Nombre Usuario" />
                                        <asp:BoundField DataField="NOMBRES_EMPLEADO" HeaderText="Nombre Empleado" />
                                        <asp:BoundField DataField="DOCUMENTO_EMPLEADO" 
                                            HeaderText="Documento Identidad" />
                                        <asp:BoundField DataField="UNIDAD_NEGOCIO" HeaderText="Unidad de Negocio" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:Panel ID="Panel_FONDO_MENSAJE_ASIGNACION_UN" runat="server" Visible="false"
                Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJE_ASIGNACION_UN" runat="server">
                <asp:Image ID="Image_MENSAJE_ASIGNACION_UN_POPUP" runat="server" Width="50px"
                    Height="50px" Style="margin: 5px auto 8px auto; display: block;" />
                <asp:Panel ID="Panel_COLOR_MENSAJE_ASIGNACION_UN" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE_ASIGNACION_UN" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_MENSAJE_ASIGNACION_UN" runat="server" Text="Cerrar" 
                        onclick="Button_MENSAJE_ASIGNACION_UN_Click" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_ASIGNACION_UNIDAD_NEGOCIO" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Asignación a Unidad de Negocio
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_UNIDAD_NEGOCIO" runat="server" />
                        <asp:HiddenField ID="HiddenField_UNIDAD_NEGOCIO" runat="server" />
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView_ASIGNACION_UN" runat="server" Width="400px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_UNIDAD_NEGOCIO,UNIDAD_NEGOCIO" 
                                                onrowcommand="GridView_ASIGNACION_UN_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                        Text="Eliminar" ValidationGroup="UNELIMINAR">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="60px"/>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ID_UNIDAD_NEGOCIO" HeaderText="ID_UNIDAD_NEGOCIO" Visible="False" />
                                                    <asp:TemplateField HeaderText="Unidad de Negocio">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DropDownList_UN" runat="server" Width="300px" ValidationGroup="UNGUARDAR" OnSelectedIndexChanged="DropDownList_UN_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_UN"
                                                                ControlToValidate="DropDownList_UN" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La UNIDAD DE NEGOCIO es requerida."
                                                                ValidationGroup="UNGUARDAR" />
                                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_UN"
                                                                TargetControlID="RequiredFieldValidator_DropDownList_UN" HighlightCssClass="validatorCalloutHighlight" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>

                                            <div class="div_espaciador">
                                            </div>

                                            <div style="text-align: left; margin-left: 10px;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="Button_NUEVO_ASIGNACION" runat="server" Text="Nueva Asignación" CssClass="margin_botones"
                                                                ValidationGroup="UNNUEVO" onclick="Button_NUEVO_ASIGNACION_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button_GUARDAR_ASIGNACION" runat="server" 
                                                                Text="Guardar Asignación" CssClass="margin_botones"
                                                                ValidationGroup="UNGUARDAR" onclick="Button_GUARDAR_ASIGNACION_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button_CANCELAR_ASIGNACION" runat="server" Text="Cancelar" CssClass="margin_botones"
                                                                ValidationGroup="UNCANCELAR" onclick="Button_CANCELAR_SERVICIO_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>  
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
                
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
        <div class="div_espaciador"></div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="ADICIONAR"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button1" onclick="window.close();" 
                                        type="button" value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>

