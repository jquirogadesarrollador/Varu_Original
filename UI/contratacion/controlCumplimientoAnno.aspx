<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="controlCumplimientoAnno.aspx.cs" Inherits="_Default" %>

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
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="div_espaciador"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label> 
                            &nbsp;Alerta BAJA.
                        </td>
                        <td class="style2">
                            <div class="div_color_verde"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label> 
                            &nbsp;Alerta MEDIA.
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label> 
                            &nbsp;Alerta ALTA
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_rojo"></div> 
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" 
                    Text="Contratos en rango de vencimiento"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" Width="885px" 
                            AutoGenerateColumns="False" >
                            <Columns>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" 
                                    HeaderText="Tipo Documento Identidad" >
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identidad" >
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOM_OCUPACION" HeaderText="Cargo" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="DESCRIPCION" 
                                    HeaderText="Objeto Servicio Respectivo" >
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERMINA" DataFormatString="{0:dd/M/yyyy}" 
                                    HeaderText="Fecha terminación" >
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
    <div class="div_espaciador"></div>
</asp:Content>

