<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="PresupuestoPrograma.aspx.cs" Inherits="_PresupuestoPrograma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('.money').mask('000.000.000.000.000,00', { reverse: true });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    

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

    <asp:UpdatePanel ID="UpdatePanel_Procesamiento" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_PRES_GEN_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_ANNO_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_MONTO_PRES_GEN_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_ASIGNADO_PRES_GEN_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_EJECUTADO_PRES_GEN_SELECCIONADO" runat="server" />

            <asp:HiddenField ID="HiddenField_ID_PRESUPUESTO_SELECCIONADO" runat="server" />
            <asp:HiddenField ID="HiddenField_ID_EMPRESA_SELECCIONADA" runat="server" />

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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
                <div class="div_espaciador">
                </div>
                <div class=" div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Historial de Presupuestos"></asp:Label>
                        &nbsp;Generales Por Año</div>
                    <div class="div_contenido_groupbox">
                        
                        <asp:HiddenField ID="HiddenField_INDEX_GRIDVIEW_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_PAGE_GRIDVIEW_PRES_GEN" runat="server" />
                
                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_MONTO_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_DESCRIPCION_PRES_GEN" runat="server" />


                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_PresupuestosAnio" runat="server" Width="885px" AutoGenerateColumns="False"
                                    
                                    DataKeyNames="ID_PRES_GEN,ANIO,ID_AREA,MONTO,EJECUTADO,ASIGNADO,DESCRIPCION" 
                                    onrowcommand="GridView_PresupuestosAnio_RowCommand" 
                                    onprerender="GridView_PresupuestosAnio_PreRender">
                                    <Columns>

                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                            Text="seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="guardar" ImageUrl="~/imagenes/plantilla/save.gif"
                                            Text="Guardar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="2px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="cancelar" ImageUrl="~/imagenes/plantilla/grid_cancelar.png"
                                            Text="Cancelar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:BoundField DataField="ANIO" HeaderText="Año">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Presupuesto General">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Monto" runat="server" Width="116px" CssClass="money" Font-Size="10px" Text="0"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Monto"
                                                    runat="server" TargetControlID="TextBox_Monto" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Asignado">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Asignado" runat="server" Width="116px" CssClass= "money" Font-Size="10px" Text="0"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Asignado"
                                                    runat="server" TargetControlID="TextBox_Asignado" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ejecutado">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Ejecutado" runat="server" Width="116px" CssClass="money" Font-Size="10px" Text="0"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Ejecutado"
                                                    runat="server" TargetControlID="TextBox_Ejecutado" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Saldo">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Saldo" runat="server" Width="116px" CssClass="money" Font-Size="10px" Text="0"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Saldo"
                                                    runat="server" TargetControlID="TextBox_Saldo" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Descripcion" runat="server" Width="216px" Height="50px" TextMode="MultiLine" Font-Size="10px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DatosPresupuestoAnioSeleccionado" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    
                    <asp:Panel ID="Panel_CABEZA_CONTROL_PRES_GEN" runat="server" CssClass="div_cabeza_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 87%;">
                                    Control Modificaciones Presupuesto General Seleccionado
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 80%">
                                                <asp:Label ID="Label_CONTROL_PRES_GEN" runat="server">(Mostrar detalles...)</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image_CONTROL_PRES_GEN" runat="server" CssClass="img_cabecera_hoja" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel_CONTENIDO_CONTROL_PRES_GEN" runat="server" CssClass="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_HistorialPresupuestoGeneral" runat="server" Width="882px"
                                    AutoGenerateColumns="False" DataKeyNames="ID_HIST_GEN,ID_PRES_GEN" AllowPaging="True" 
                                    
                                    onpageindexchanging="GridView_HistorialPresupuestoGeneral_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="FCH_CRE"  HeaderText="Fecha" 
                                            DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_CRE" HeaderText="Usuario">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MONTO_NEW" HeaderText="Presupuesto" 
                                            DataFormatString="{0:C2}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION_NEW" HeaderText="Descripción">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_CONTROL_PRES_GEN" runat="Server"
                        TargetControlID="Panel_CONTENIDO_CONTROL_PRES_GEN" ExpandControlID="Panel_CABEZA_CONTROL_PRES_GEN"
                        CollapseControlID="Panel_CABEZA_CONTROL_PRES_GEN" Collapsed="True" TextLabelID="Label_CONTROL_PRES_GEN"
                        ImageControlID="Image_CONTROL_PRES_GEN" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                        ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                        SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>

                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DetallesPresupuestoGeneral" runat="server">
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Detalles Presupuesto Por Empresa
                    </div>
                    <div class="div_contenido_groupbox">

                        <asp:HiddenField ID="HiddenField_INDEX_GRID_DET_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_PAGE_GRID_DET_PRES_GEN" runat="server" />

                        <asp:HiddenField ID="HiddenField_ACCION_GRILLA_DET_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_DET_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_PRESUPUESTO_DET_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_PRESUPUESTO_DET_PRES_GEN" runat="server" />
                        <asp:HiddenField ID="HiddenField_OBSERVACIONES_DET_PRES_GEN" runat="server" />
                        
                        <asp:HiddenField ID="HiddenField_LETRA_PAGINACION_EMPRESA" runat="server" />

                        <table cellspacing="5" cellpadding="2" class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LinkButton_a" runat="server" onclick="LinkButton_a_Click">A</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_b" runat="server" onclick="LinkButton_b_Click" >B</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_c" runat="server" onclick="LinkButton_c_Click" >C</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton3_d" runat="server" 
                                        onclick="LinkButton3_d_Click" >D</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_e" runat="server" onclick="LinkButton_e_Click" >E</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_f" runat="server" onclick="LinkButton_f_Click" >F</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_g" runat="server" onclick="LinkButton_g_Click" >G</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_h" runat="server" onclick="LinkButton_h_Click" >H</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_i" runat="server" onclick="LinkButton_i_Click" >I</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_j" runat="server" onclick="LinkButton_j_Click" >J</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_k" runat="server" onclick="LinkButton_k_Click" >K</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_l" runat="server" onclick="LinkButton_l_Click" >L</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_m" runat="server" onclick="LinkButton_m_Click" >M</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_n" runat="server" onclick="LinkButton_n_Click" >N</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_o" runat="server" onclick="LinkButton_o_Click" >O</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_p" runat="server" onclick="LinkButton_p_Click" >P</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_q" runat="server" onclick="LinkButton_q_Click">Q</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_r" runat="server" onclick="LinkButton_r_Click" >R</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_s" runat="server" onclick="LinkButton_s_Click" >S</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_t" runat="server" onclick="LinkButton_t_Click" >T</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_u" runat="server" onclick="LinkButton_u_Click" >U</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_v" runat="server" onclick="LinkButton_v_Click" >V</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_w" runat="server" onclick="LinkButton_w_Click" >W</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_x" runat="server" onclick="LinkButton_x_Click" >X</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_y" runat="server" onclick="LinkButton_y_Click">Y</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton_z" runat="server" onclick="LinkButton_z_Click">Z</asp:LinkButton>
                                </td>
                            </tr>
                        </table>

                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_DetallesPresupuestoGeneral" runat="server" 
                                    Width="883px" AutoGenerateColumns="False"
                                    
                                    
                                    DataKeyNames="ID_PRESUPUESTO,ID_EMPRESA,ANNO,PRESUPUESTO,OBSERVACIONES,ID_AREA,ID_PRES_GENERAL,ASIGNADO,EJECUTADO" 
                                    onrowcommand="GridView_DetallesPresupuestoGeneral_RowCommand" 
                                    onprerender="GridView_DetallesPresupuestoGeneral_PreRender" PageSize="20">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                            Text="seleccionar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/plantilla/edit.gif"
                                            Text="Actualizar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="guardar" ImageUrl="~/imagenes/plantilla/save.gif"
                                            Text="Guardar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:ButtonField ButtonType="Image" CommandName="cancelar" ImageUrl="~/imagenes/plantilla/grid_cancelar.png"
                                            Text="Cancelar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="21px" />
                                        </asp:ButtonField>

                                        <asp:BoundField DataField="ANNO" HeaderText="Año">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        
                                        <asp:TemplateField HeaderText="Presupuesto Empresa">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_Presupuesto" runat="server" Width="98px" CssClass="money" Font-Size="10px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Presupuesto"
                                                    runat="server" TargetControlID="TextBox_Presupuesto" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Asignado">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_AsignadoEmpresa" runat="server" Width="98px" CssClass= "money"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_AsignadoEmpresa"
                                                    runat="server" TargetControlID="TextBox_AsignadoEmpresa" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ejecutado">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_EjecutadoEmpresa" runat="server" Width="98px" CssClass="money"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_EjecutadoEmpresa"
                                                    runat="server" TargetControlID="TextBox_EjecutadoEmpresa" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Saldo">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_SaldoEmpresa" runat="server" Width="98px" CssClass="money"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_SaldoEmpresa"
                                                    runat="server" TargetControlID="TextBox_SaldoEmpresa" FilterType="Numbers,Custom"
                                                    ValidChars=".," />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextBox_ObservacionesEmpresa" runat="server" Width="160px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_HistorialDetallePresupuestoGeneral" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">




                    <asp:Panel ID="Panel_CABEZA_CONTROL_DET_PRES_GEN" runat="server" CssClass="div_cabeza_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 87%;">
                                    Control Modificaciones Presupuesto Empresa Seleccionada
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 80%">
                                                <asp:Label ID="Label_CONTROL_DET_PRES_GEN" runat="server">(Mostrar detalles...)</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image_CONTROL_DET_PRES_GEN" runat="server" CssClass="img_cabecera_hoja" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel_CONTENIDO_CONTROL_DET_PRES_GEN" runat="server" CssClass="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_HistorialEmpresaSeleccionada" runat="server" Width="882px"
                                    AutoGenerateColumns="False" DataKeyNames="ID_HIST_PRESUPUESTO,ID_PRESUPUESTO"
                                    AllowPaging="True" PageSize="5">
                                    <Columns>
                                        <asp:BoundField DataField="FCH_CRE" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USU_CRE" HeaderText="Usuario">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRESUPUESTO_NEW" HeaderText="Presupuesto" DataFormatString="{0:C2}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBS_NEW" HeaderText="Observaciones">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>


                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_CONTROL_DET_PRES_GEN" runat="Server"
                        TargetControlID="Panel_CONTENIDO_CONTROL_DET_PRES_GEN" ExpandControlID="Panel_CABEZA_CONTROL_DET_PRES_GEN"
                        CollapseControlID="Panel_CABEZA_CONTROL_DET_PRES_GEN" Collapsed="True" TextLabelID="Label_CONTROL_DET_PRES_GEN"
                        ImageControlID="Image_CONTROL_DET_PRES_GEN" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                        ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                        SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>

                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_DetallePorMes" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Detalle Por Mes para La Empresa Y Año Seleccionado
                    </div>
                    <div class="div_contenido_groupbox">

                        <table class="table_control_registros" width="880">
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Enero
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoEneroAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesEnero" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesEnero_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoEneroEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Febrero
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoFebreroAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesFebrero" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesFebrero_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoFebreroEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Marzo
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoMarzoAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesMarzo" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesMarzo_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoMarzoEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Abril
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoAbrilAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesAbril" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesAbril_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoAbrilEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Mayo
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoMayoAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesMayo" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesMayo_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoMayoEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Junio
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoJunioAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesJunio" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesJunio_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoJunioEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Julio
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoJulioAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesJulio" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesJulio_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoJulioEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Agosto
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoAgostoAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesAgosto" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesAgosto_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoAgostoEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Septiembre
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoSeptiembreAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesSeptiembre" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesSeptiembre_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoSeptiembreEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Octubre
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoOctubreAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetalleOctubre" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetalleOctubre_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoOctubreEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Noviembre
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoNoviembreAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesNoviembre" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesNoviembre_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoNoviembreEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="div_cabeza_groupbox_gris">
                                        Diciembre
                                    </div>
                                    <div class="div_contenido_groupbox_gris" style="padding-top: 20px; padding-bottom: 20px;">
                                        <table class="table_control_registros">
                                            <tr>
                                                <td style="text-align: center;">
                                                    Asignado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoDicimebreAsignado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton_DetallesDiciembre" runat="server" 
                                                        ImageUrl="~/imagenes/plantilla/view2.gif" 
                                                        onclick="ImageButton_DetallesDiciembre_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    Ejecutado<br />
                                                    <asp:TextBox ID="TextBox_PresupuestoDiciembreEjecutado" runat="server" CssClass="money"
                                                        Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel_GrillaDetallesActividadesDelMes" runat="server">
                <div class="div_espaciador"></div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Actividades Programadas Para La Empresa Seleccionada
                    </div>
                    <div class="div_contenido_groupbox">
                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_DetallesActividadesDelMes" runat="server" Width="882px"
                                    AutoGenerateColumns="False" DataKeyNames="ID_DETALLE,ID_PROGRAMA,ID_DETALLE_GENERAL,ID_ACTIVIDAD,ID_CIUDAD,ID_AREA_ACTIVIDAD,ID_REGIONAL" >
                                    <Columns>
                                        <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="NOMBRE_ACTIVIDAD" HeaderText="Actividad" />
                                        <asp:BoundField DataField="FECHA_ACTIVIDAD" HeaderText="Fecha" 
                                            DataFormatString="{0:dd/MM/yyyy}" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="PRESUPUESTO_APROBADO" HeaderText="Presupuesto" DataFormatString="{0:C2}">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_ESTADO" HeaderText="Estado">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

