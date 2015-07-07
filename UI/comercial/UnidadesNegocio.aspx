<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="UnidadesNegocio.aspx.cs" Inherits="comercial_UnidadesNegocio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>  
    
    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador"></div>
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
                                        CssClass="margin_botones" onclick="Button_NUEVO_Click" 
                                        ValidationGroup="NUEVO_CLIENTE" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="Guardar"/>
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
            </tr>
        </table>
    </asp:Panel>  


    <asp:Panel ID="Panel_empresas_unidad_negocio" runat="server">
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
                                <asp:GridView ID="GridView_unidades_negocio" runat="server" Width="885px" 
                                    AutoGenerateColumns="False" DataKeyNames="ID_EMPRESA_USUARIO,ID_USUARIO">
                                    <Columns>
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
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">

            <div class="div_cabeza_groupbox">
                Unidad de negocio
            </div>
            <div class="div_contenido_groupbox">

                <asp:Panel ID="Panel_unidad_negocio" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        Información
                    </div>
                    <div class="div_contenido_groupbox_gris">

                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    Empresas
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_empresas" runat="server" ValidationGroup="Guardar"  Width="510px" ControlToValidate="DropDownList_empresas">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Unidad de Negocio
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_unidad_negocio" runat="server" ValidationGroup="Guardar"  Width="510px" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    Usuario
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_usuario" runat="server" ValidationGroup="Guardar"  Width="510px" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_empresas"
                            ControlToValidate="DropDownList_empresas" InitialValue="0" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La EMPRESA es requerida."
                            ValidationGroup="Guardar" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_empresas"
                            TargetControlID="RequiredFieldValidator_DropDownList_empresas" HighlightCssClass="validatorCalloutHighlight" />

                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_unidad_negocio"
                            ControlToValidate="DropDownList_unidad_negocio" InitialValue="0" Display="None"
                            ErrorMessage="<b>Campo Requerido faltante</b><br />La UNIDAD DE NEGOCIO es requerida."
                            ValidationGroup="Guardar" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_unidad_negocio"
                            TargetControlID="RequiredFieldValidator_DropDownList_unidad_negocio" HighlightCssClass="validatorCalloutHighlight" />

                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_usuario"
                            ControlToValidate="DropDownList_usuario" InitialValue="0" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El USUARIO es requerido."
                            ValidationGroup="Guardar" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                            TargetControlID="RequiredFieldValidator_DropDownList_usuario" HighlightCssClass="validatorCalloutHighlight" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
</asp:Content>

