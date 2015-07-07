<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="asociarMotivosRotacionRetiro.aspx.cs" Inherits="_AsociacionMotivosRotacionRetiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    



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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el 
                    mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" 
                        OnClick="Button_CERRAR_MENSAJE_Click" style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Botones de acción
            </div>
            <div class="div_contenido_groupbox">
                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                    <tr>
                        <td rowspan="0">
                            <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                OnClick="Button_MODIFICAR_Click" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
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
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_GrillaMotivosRotacion" runat="server">
        <div class="div_espaciador"></div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Lista de Motivos de Rotación y Retiro
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_MotivosRotacion" runat="server" Width="885px" AutoGenerateColumns="False"
                            
                            DataKeyNames="ID_MAESTRA_ROTACION,ID_DETALLE_ROTACION,ID_ROTACION_EMPRESA" >
                            <AlternatingRowStyle BackColor="#DDDDDD" />
                            <Columns>
                                
                                <asp:BoundField DataField="TITULO_MAESTRA_ROTACION" HeaderText="Categoría" />
                                <asp:BoundField DataField="TITULO" HeaderText="Motivo" />
                                <asp:TemplateField HeaderText="Asociar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox_Configurado" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="columna_grid_centrada" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_PIE" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Botones de acción
            </div>
            <div class="div_contenido_groupbox">
                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                    <tr>
                        <td rowspan="0">
                            <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                OnClick="Button_MODIFICAR_Click" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                OnClick="Button_GUARDAR_Click" ValidationGroup="GUARDAR" />
                        </td>
                        <td rowspan="0">
                            <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

</asp:Content>

