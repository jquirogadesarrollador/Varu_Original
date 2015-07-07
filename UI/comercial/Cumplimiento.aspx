<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="Cumplimiento.aspx.cs" Inherits="comercial_Cumplimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                                    <asp:Button ID="Button_EXPORTAR" runat="server" Text="Exportar" 
                                        CssClass="margin_botones" onclick="Button_EXPORTAR_Click"/>
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
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            Digite el año a consultar (AAAA)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_buscar" runat="server" Width="220px" ValidationGroup="Buscar" CssClass="margin_botones"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="Buscar" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_buscar"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="Buscar" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers" runat="server" TargetControlID="TextBox_buscar" FilterType="Numbers" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_HOJA_DE_TRABAJO" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_para_convencion_hoja_trabajo">
                
            </div>
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Hoja de trabajo"></asp:Label>
                [LAS CIFRAS SON PRESENTADAS EN MILLONES DE PESOS]
            </div>
            <div class="div_contenido_groupbox">
            <div style="overflow:scroll; height:380px; width:auto ">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_HOJA_DE_TRABAJO" runat="server" Width="885px" 
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Regional" HeaderText="Regional" ItemStyle-Font-Size="Small"/>
                                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" ItemStyle-Font-Size="Small"/>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" ItemStyle-Font-Size="Small"/>
                                <asp:BoundField DataField="Enero" HeaderText="Enero">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Febrero" HeaderText="Febrero">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Marzo" HeaderText="Marzo">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Abril" HeaderText="Abril">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mayo" HeaderText="Mayo">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Junio" HeaderText="Junio">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Julio" HeaderText="Julio">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Agosto" HeaderText="Agosto">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Septiembre" HeaderText="Septiembre">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Octubre" HeaderText="Octubre">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Noviembre" HeaderText="Noviembre">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Diciembre" HeaderText="Diciembre">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total">
                                <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="div_espaciador">
                </div>
            </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

