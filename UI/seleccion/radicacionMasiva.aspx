<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="radicacionMasiva.aspx.cs" Inherits="_RadicacionMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    

    <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
    </asp:Panel>
    <asp:Panel ID="Panel_MENSAJES" runat="server">
        <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
            display: block;" />
        <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
            <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" Text="Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente."></asp:Label>
        </asp:Panel>
        <div style="text-align: center; margin-top: 15px;">
            <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" CssClass="margin_botones"
                OnClick="Button_CERRAR_MENSAJE_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_UploadFile" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Selección de Archivo Plano
            </div>
            <div class="div_contenido_groupbox">
                <table cellpadding="2" cellspacing="2" class="table_control_registros">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload_ArchivoPlano" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="Button_Cargar" runat="server" Text="Realizar Cargue Masivo" 
                                CssClass="margin_botones" onclick="Button_Cargar_Click"/>
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="Panel_EspecificacionesArchivo" runat="server">
                    <asp:Panel ID="Panel_CABEZA_ESPECIFICACIONES" runat="server" CssClass="div_cabeza_groupbox_gris">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 87%;">
                                    Especificaciones del Archivo Plano
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 80%">
                                                <asp:Label ID="Label_ESPECIFICACIONES" runat="server">(Mostrar detalles...)</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="Image_ESPECIFICACIONES" runat="server" CssClass="img_cabecera_hoja" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel_CONTENIDO_ESPECIFICACIONES" runat="server" CssClass="div_contenido_groupbox_gris">
                        <table cellpadding="3" cellspacing="5" class="table_control_registros">
                            <tr>
                                <td style="text-align:center; font-weight:bold; background-color:#EEEEEE; border:1px solid #cccccc;">
                                    NO
                                </td>
                                <td style="text-align:center; font-weight:bold; background-color:#EEEEEE; border:1px solid #cccccc;">
                                    CAMPO
                                </td>
                                <td style="text-align:center; font-weight:bold; background-color:#EEEEEE; border:1px solid #cccccc;">
                                    DESCRIPCIÓN
                                </td>
                                <td style="text-align:center; font-weight:bold; background-color:#EEEEEE; border:1px solid #cccccc;">
                                    OBLIGATORIO
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    1
                                </td>
                                <td style="text-align:left; border:1px solid #cccccc;">
                                    TIPO_DOCUMENTO_IDENTIDAD
                                </td>
                                <td style="text-align:justify; border:1px solid #cccccc;">
                                    Determina el tipo de documento de identidad. valores posibles &#39;CC&#39; &#39;TI&#39; &#39;CE&#39; 
                                    &#39;PA&#39;</td>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    SI
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    2
                                </td>
                                <td style="text-align:left; border:1px solid #cccccc;">
                                    NUMERO_DOCUMENTO
                                </td>
                                <td style="text-align:justify; border:1px solid #cccccc;">
                                    Corresponde al numero de identificación del aspirante, solo se admiten numeros, (sin puntos, comas,  espacios ó lineas).
                                </td>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    SI
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    3
                                </td>
                                <td style="text-align:left; border:1px solid #cccccc;">
                                    NOMBRES_ASPIRANTE
                                </td>
                                <td style="text-align:justify; border:1px solid #cccccc;">
                                    Corresponde al los nombres del aspirante.
                                </td>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    SI
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    4
                                </td>
                                <td style="text-align:left; border:1px solid #cccccc;">
                                    APELLIDOS_ASPIRANTE
                                </td>
                                <td style="text-align:justify; border:1px solid #cccccc;">
                                    Corresponde al los apellidos del aspirante.
                                </td>
                                <td style="text-align:center; border:1px solid #cccccc;">
                                    SI
                                </td>
                            </tr>
                        </table>
                        <div class="div_espaciador"></div>
                        <div style="text-align:center; color:Red; margin:0px auto;">
                            NOTA: Los campos deben estar separados por el caracter punto y coma ';'.
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ESPECIFICACIONES" runat="Server"
                        TargetControlID="Panel_CONTENIDO_ESPECIFICACIONES" ExpandControlID="Panel_CABEZA_ESPECIFICACIONES"
                        CollapseControlID="Panel_CABEZA_ESPECIFICACIONES" Collapsed="True" TextLabelID="Label_ESPECIFICACIONES"
                        ImageControlID="Image_ESPECIFICACIONES" ExpandedText="(Ocultar detalles...)" CollapsedText="(Mostrar detalles...)"
                        ExpandedImage="~/imagenes/plantilla/collapse.jpg" CollapsedImage="~/imagenes/plantilla/expand.jpg"
                        SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador">
        </div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td colspan="0">
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo Cargue" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:Panel ID="Panel_GrillaResultadosCargue" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Resultados de Cargue Masivo
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Total Registros Procesados:
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_TotalRegistroProcesados" runat="server" Text="000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Registros Omitidos (Ya Ingresados):
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RegistrosOmitidos" runat="server" Text="000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Registros Erroneos:
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RegistrosErroneos" runat="server" Text="000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Registros Ingresados Correctamente:
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RegistrosInsertados" runat="server" Text="000"></asp:Label>
                        </td>
                    </tr>
                </table>

                <div class="div_espaciador"></div>

                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_ResultadosCargue" runat="server" Width="884px" 
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="CODIGO_ERROR" HeaderText="Error">
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LINEA" HeaderText="Línea">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CAMPO" HeaderText="Campo">
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                <ItemStyle CssClass="columna_grid_jus" />
                                </asp:BoundField>
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
        <div class="div_espaciador">
        </div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de acción
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td colspan="0">
                                    <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo Cargue" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" ValidationGroup="NUEVO" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

