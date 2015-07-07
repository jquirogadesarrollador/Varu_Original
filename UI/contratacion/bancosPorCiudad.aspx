<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="bancosPorCiudad.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="div_contenedor_formulario">
        <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
            <div class="div_espaciador"></div>
        </asp:Panel>
        <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div style="margin:0 auto; display:block;">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                                CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                                ValidationGroup="GUARDAR"/>
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                                CssClass="margin_botones" ValidationGroup="MODIFICAR" 
                                                onclick="Button_MODIFICAR_Click"/>
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_VOLVER" runat="server" Text="Volver al menú"  
                                                CssClass="margin_botones" onclick="Button_VOLVER_Click" />
                                        </td>
                                        <td colspan="0">
                                            <input id="Button_CERRAR" type="button" value="Salir" onclick="window.close();" class="margin_botones"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="div_espaciador"></div>
        </asp:Panel>
        <asp:Panel ID="Panel_MENSAJES" runat="server">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
            <div class="div_cabeza_groupbox">
                Cobertura de la empresa
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_DATOS" runat="server" Width="885px" AutoGenerateColumns="False" 
                            DataKeyNames="Código Ciudad" onrowcommand="GridView_DATOS_RowCommand" >
                            <Columns>
                                <asp:ButtonField CommandName="seleccionar" 
                                    Text="Seleccionar" ButtonType="Image" 
                                    ImageUrl="~/imagenes/plantilla/view2.gif" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="Código Ciudad" HeaderText="ID_CIUDAD" 
                                    Visible="False" />
                                <asp:BoundField DataField="Regional" HeaderText="Regional" />
                                <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="div_espaciador"></div>
        </asp:Panel>
        <asp:Panel ID="Panel_FORMULARIO" runat="server">
            <div class="div_cabeza_groupbox">
                Configuración de entidades bancarias
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_INFO_CIUDAD_SELECCIONADA" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Información ciudad seleccionada
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Ciudad
                                </td>
                                <td class="td_der">
                                    <asp:Label ID="Label_CIUDAD_SELECCIONADA" runat="server" Text="" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:Panel ID="Panel_LISTA_BANCOS_ASIGNADOS" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Entidades bancarias asignadas
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_MENSAJE_LISTA_BANCOS" runat="server">
                                    <div class="div_espacio_validacion_campos">
                                        <asp:Label id="Label_MENSAJE_LISTA_BANCOS" runat="server" ForeColor="Red" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel_GRILLA_BANCOS_ASIGNADOS" runat="server">
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <table class="table_control_registros" width="500">
                                                <tr>
                                                    <td>
                                                         <asp:GridView ID="GridView_LISTA_BANCOS_POR_CIUDAD" runat="server" Width="500px" 
                                                            AutoGenerateColumns="False" DataKeyNames="REGISTRO,REGISTRO_CON_BANCO_EMPRESA" 
                                                            onrowcommand="GridView_LISTA_BANCOS_POR_CIUDAD_RowCommand" >
                                                            <Columns>
                                                                <asp:ButtonField CommandName="eliminar" Text="Eliminar" ButtonType="Image" 
                                                                    ImageUrl="~/imagenes/plantilla/delete.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                                <asp:BoundField DataField="NOM_BANCO" HeaderText="Nombre entidad" />
                                                                <asp:BoundField DataField="REGISTRO_CON_BANCO_EMPRESA" 
                                                                    HeaderText="REGISTRO_CON_BANCO_EMPRESA" Visible="False" />
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>       
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel_LISTA_BANCOS" runat="server">
                                    <div class="div_espaciador"></div>
                                    <div class="div_cabeza_groupbox_gris">
                                        Selección de entidades bancarias
                                    </div>
                                    <div class="div_contenido_groupbox_gris">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    Seleccione entidad
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_BANCO" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="div_espaciador"></div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button_AGREGAR_BANCO" runat="server" Text="Adicionar" 
                                                        CssClass="margin_botones" onclick="Button_AGREGAR_BANCO_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel_BOTONES_1" runat="server">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div style="margin:0 auto; display:block;">
                            <div class="div_cabeza_groupbox">
                                Botones de acción
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td colspan="0">
                                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                                CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                                ValidationGroup="GUARDAR"/>
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar"  
                                                CssClass="margin_botones" ValidationGroup="MODIFICAR" 
                                                onclick="Button_MODIFICAR_Click"/>
                                        </td>
                                        <td colspan="0">
                                            <asp:Button ID="Button_VOLVER_1" runat="server" Text="Volver al menú"  
                                                CssClass="margin_botones" onclick="Button_VOLVER_Click" />
                                        </td>
                                        <td colspan="0">
                                            <input id="Button_SALIR_1" type="button" value="Salir" onclick="window.close();" class="margin_botones"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="div_espaciador"></div>
        </asp:Panel>
    </div>
</asp:Content>

