<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="Presupuestos.aspx.cs" Inherits="_Presupuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>
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

            <asp:HiddenField ID="HiddenField_ANIO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_REGIONAL" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_CIUDAD" runat="server" />
            <asp:HiddenField ID="HiddenField_ANIO_CON_PRESUPUESTO" runat="server" />

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

            <asp:Panel ID="Panel_PresupuestoRegional" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Selección de Año y Regional
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros" width="880px">
                            <tr>
                                <td valign="top" style="width: 50%;">
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_Anios" runat="server" Width="430px" AutoGenerateColumns="False"
                                                AllowPaging="True" onpageindexchanging="GridView_Anios_PageIndexChanging" 
                                                onrowcommand="GridView_Anios_RowCommand" DataKeyNames="ANIO">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                                        Text="seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ANIO" HeaderText="Año">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PRESUPUESTO" HeaderText="Presupuesto" 
                                                        DataFormatString="{0:C}">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                                <td valign="top" style="width: 50%;">
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_Regionales" runat="server" Width="430px" AutoGenerateColumns="False" 
                                                onrowcommand="GridView1_RowCommand" DataKeyNames="ID_REGIONAL">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                                        Text="seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ID_REGIONAL" HeaderText="Id Regional">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Regional" />
                                                    <asp:BoundField DataField="PRESUPUESTO" HeaderText="Presupuesto" 
                                                        DataFormatString="{0:C}">
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_Ciudades" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Ciudades de la Regional Seleccionada
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_Ciudades" runat="server" Width="880px" AutoGenerateColumns="False"
                                                AllowPaging="True" 
                                                onpageindexchanging="GridView_Ciudades_PageIndexChanging" 
                                                onrowcommand="GridView_Ciudades_RowCommand" DataKeyNames="ID_CIUDAD">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                                        Text="seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ID_CIUDAD" HeaderText="Id Ciudad">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE_DEPARTAMENTO" HeaderText="Departamento">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Nombre Ciudad" DataField="NOMBRE">
                                                    <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PRESUPUESTO" HeaderText="Presupuesto" 
                                                        DataFormatString="{0:C}" >
                                                    <ItemStyle CssClass="columna_grid_der" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DetallesPorAnio" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Presupuesto Anual
                    </div>
                    <div class="div_contenido_groupbox">
                        <table class="table_control_registros" width="880">
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Enero
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoEnero" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoEnero"
                                        runat="server" TargetControlID="TextBox_PresupuestoEnero" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Febrero
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoFebrero" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoFebrero"
                                        runat="server" TargetControlID="TextBox_PresupuestoFebrero" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Marzo
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoMarzo" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoMarzo"
                                        runat="server" TargetControlID="TextBox_PresupuestoMarzo" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Abril
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoAbril" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoAbril"
                                        runat="server" TargetControlID="TextBox_PresupuestoAbril" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Mayo
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoMayo" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoMayo"
                                        runat="server" TargetControlID="TextBox_PresupuestoMayo" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Junio
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoJunio" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoJunio"
                                        runat="server" TargetControlID="TextBox_PresupuestoJunio" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Julio
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoJulio" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoJulio"
                                        runat="server" TargetControlID="TextBox_PresupuestoJulio" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Agosto
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoAgosto" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoAgosto"
                                        runat="server" TargetControlID="TextBox_PresupuestoAgosto" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Septiembre
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoSeptiembre" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoSeptiembre"
                                        runat="server" TargetControlID="TextBox_PresupuestoSeptiembre" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Octubre
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoOctubre" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoOctubre"
                                        runat="server" TargetControlID="TextBox_PresupuestoOctubre" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Noviembre
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoNoviembre" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoNoviembre"
                                        runat="server" TargetControlID="TextBox_PresupuestoNoviembre" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Diciembre
                                    </div> 
                                    <div class="div_contenido_groupbox_gris" style="padding-top:20px; padding-bottom:20px;">
                                        <asp:TextBox ID="TextBox_PresupuestoDiciembre" runat="server" CssClass="money"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PresupuestoDiciembre"
                                        runat="server" TargetControlID="TextBox_PresupuestoDiciembre" FilterType="Numbers,Custom"
                                        ValidChars=".," />
                                </td>
                            </tr>
                        </table>

                        <%-- TextBox_PresupuestoEnero --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoEnero"
                            ControlToValidate="TextBox_PresupuestoEnero" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA ENERO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoEnero"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoEnero" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoFebrero --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoFebrero"
                            ControlToValidate="TextBox_PresupuestoFebrero" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA FEBRERO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoFebrero"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoFebrero" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoMarzo --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoMarzo"
                            ControlToValidate="TextBox_PresupuestoMarzo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA MARZO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoMarzo"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoMarzo" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoAbril --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoAbril"
                            ControlToValidate="TextBox_PresupuestoAbril" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA ABRIL es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoAbril" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoMayo --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoMayo"
                            ControlToValidate="TextBox_PresupuestoMayo" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA MAYO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoMayo"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoMayo" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoJunio --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoJunio"
                            ControlToValidate="TextBox_PresupuestoJunio" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA JUNIO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoJunio" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoJulio --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoJulio"
                            ControlToValidate="TextBox_PresupuestoJulio" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA JULIO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoJulio"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoJulio" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoAgosto --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoAgosto"
                            ControlToValidate="TextBox_PresupuestoAgosto" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA AGOSTO es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoAgosto"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoAgosto" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoSeptiembre --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoSeptiembre"
                            ControlToValidate="TextBox_PresupuestoSeptiembre" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA SEPTIEMBRE es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoSeptiembre"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoSeptiembre" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoOctubre --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoOctubre"
                            ControlToValidate="TextBox_PresupuestoOctubre" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA OCTUBRE es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoOctubre"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoOctubre" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoNoviembre --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoNoviembre"
                            ControlToValidate="TextBox_PresupuestoNoviembre" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA NOVIEMBRE es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoNovimebre"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoNoviembre" HighlightCssClass="validatorCalloutHighlight" />

                        <%-- TextBox_PresupuestoDiciembre --%>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_PresupuestoDiciembre"
                            ControlToValidate="TextBox_PresupuestoDiciembre" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PRESUPUESTO PARA DICIEMBRE es requerido."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_PresupuestoDiciembre"
                            TargetControlID="RequiredFieldValidator_TextBox_PresupuestoDiciembre" HighlightCssClass="validatorCalloutHighlight" />
                    </div>

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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
