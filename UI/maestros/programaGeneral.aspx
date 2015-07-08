<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="programaGeneral.aspx.cs" Inherits="_ProgramaGeneral" %>

<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    <asp:HiddenField ID="HiddenField_ANNO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_AREA" runat="server" />
    
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
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" Style="margin: 5px auto 8px auto;
                    display: block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" Style="text-align: center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_Click"
                        Style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
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
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        ValidationGroup="NUEVO" OnClick="Button_NUEVO_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        ValidationGroup="ADICIONAR" OnClick="Button_GUARDAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_IMPRIMIR" runat="server" Text="Imprimir" CssClass="margin_botones"
                                        ValidationGroup="IMPRIMIR" onclick="Button_IMPRIMIR_Click" />
                                </td>
                                <td rowspan="0">
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" type="button"
                                        value="Salir" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador">
        </div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="ID_PROGRAMA_GENERAL,ANNO,ID_AREA" AllowPaging="True" OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging"
                            OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                    Text="seleccionar">
                                    <ItemStyle CssClass="columna_grid_centrada" Width="60px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ANNO" HeaderText="Año">
                                    <ItemStyle CssClass="columna_grid_centrada" Width="100px" />
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

    <asp:Panel ID="PanelNombrePrograma" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Nombre Programa
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Nombre del Programa
                        </td>
                        <td class="td_der">
                            <asp:TextBox ID="TextBox_NombrePrograma" runat="server" Width="600px" ValidationGroup="ADICIONAR"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_NombrePrograma"
                    ControlToValidate="TextBox_NombrePrograma" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SUB PROGRAMA es requerido."
                    ValidationGroup="ADICIONAR" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_NombrePrograma"
                    TargetControlID="RequiredFieldValidator_TextBox_NombrePrograma" HighlightCssClass="validatorCalloutHighlight" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_ImagenPrograma" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Imagen del Programa
            </div>
            <div class="div_contenido_groupbox">

                <asp:HiddenField ID="HiddenField_ImagenPrograma" runat="server" />

                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Image ID="Image_Programa" runat="server" />
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="Panel_SubirArchivoImagenPrograma" runat="server">
                    <div class="div_espaciador"></div>
                    <table class="table_control_registros">
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload_ImagenPrograma" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="Panel_CabeceraHtml" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Primera Parte del Programa General
            </div>
            <div class="div_contenido_groupbox">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="text-align: left;">
                            <CKEditor:CKEditorControl ID="CKEditor_PrimeraParte" BasePath="~/ckeditor" 
                                runat="server" Width="880px" ValidationGroup="ADICIONAR"></CKEditor:CKEditorControl>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CKEditor_PrimeraParte" ControlToValidate="CKEditor_PrimeraParte"
                                Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La PArte Incial del Documento es requerida."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CKEditor_PrimeraParte"
                                TargetControlID="RequiredFieldValidator_CKEditor_PrimeraParte" HighlightCssClass="validatorCalloutHighlight" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_ArbolPrograma" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Esquema del Programa
            </div>
            <div class="div_contenido_groupbox">
                
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        





                        <asp:HiddenField ID="HiddenField_ID_PROGRAMA_GENERAL" runat="server" />
                        <asp:HiddenField ID="HiddenField_TIPO_NODO_SELECCIONADO" runat="server" />
                        <asp:HiddenField ID="HiddenField_ID_NODO_SELECCIONADO" runat="server" />
                    
                        <div class="div_titulo_programa_general">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 25px">
                                        <asp:ImageButton ID="ImageButton_AdicionarSubprograma" runat="server" 
                                            ImageUrl="~/imagenes/plantilla/imgProgramaAdd.png" 
                                            onclick="ImageButton_AdicionarSubprograma_Click" />
                                    </td>
                                    <td style="width: 25px">
                                        <asp:ImageButton ID="ImageButton_AdicionarActividad" runat="server" 
                                            ImageUrl="~/imagenes/plantilla/imgActividadAdd.png" 
                                            onclick="ImageButton_AdicionarActividad_Click" />
                                    </td>
                                    <td style="width: 100%">
                                        <asp:Label ID="Label_TituloProgramaGeneral" runat="server" Text="Actividades que comprenden el Programa General para el año 0000"></asp:Label>
                                    </td>
                                    <td style="width: 25px">
                                        <asp:Image ID="Image_EsquemaProgramaEmpresa" runat="server" ImageUrl="~/imagenes/plantilla/imgEmpresa.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="div_contenedor_grilla_resultados">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_EsquemaPrograma" runat="server" Width="886px" ShowHeader="False"
                                    AutoGenerateColumns="False" 
                                    DataKeyNames="INDEX,ID_DETALLE_GENERAL,ID_PROGRAMA_GENERAL,TIPO,ID_DETALLE_GENERAL_PADRE,ID_SUBPROGRAMA,ID_ACTIVIDAD" 
                                    onrowcommand="GridView_EsquemaPrograma_RowCommand" 
                                    onprerender="GridView_EsquemaPrograma_PreRender">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/plantilla/delete.gif"
                                            Text="Eliminar">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="25px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="nuevo_programa" ImageUrl="~/imagenes/plantilla/imgProgramaAdd.png"
                                            Text="NuevoPrograma">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="29px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="nueva_actividad" ImageUrl="~/imagenes/plantilla/imgActividadAdd.png"
                                            Text="NuevaActividad">
                                            <ItemStyle CssClass="columna_grid_centrada" Width="29px" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Numeración y Nombre">
                                            <ItemTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="border: solid 1px #bbbbbb; padding: 2px 5px 2px 2px;">
                                                            <asp:Label ID="Label_numeracionPrograma" runat="server" Text="0.0." Font-Bold="True"
                                                                Font-Names="Consolas"></asp:Label>
                                                        </td>
                                                        <td style="width: 100%; background-color: #eeeeee; border: solid 1px #bbbbbb; padding: 2px">
                                                            <asp:Label ID="Label_NombreSubprogramaActividad" runat="server" Font-Bold="true"
                                                                Text="Nombre del subprograma o actividad"></asp:Label>:
                                                            <asp:Label ID="Label_DescripcionProgramaActividad" runat="server" Text="Aca va la descrpcion del programa o actividad."></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imagen Programa ó Actividad">
                                            <ItemTemplate>
                                                <asp:Image ID="Image_ProgramaActividad" runat="server" Width="25px" ImageUrl="~/imagenes/plantilla/imgActividad.png" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_jus" Width="25px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>

                        <asp:Panel ID="Panel_FONDO_MENSAJE_ARBOL" runat="server" Visible="false" 
                            Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_ARBOL" runat="server">
                            <asp:Image ID="Image_MENSAJE_ARBOL_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_ARBOL_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_ARBOL" runat="server" ForeColor="Red">Este es el mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_ARBOL" runat="server" Text="Cerrar" 
                                    Style="height: 26px" onclick="Button_CERRAR_MENSAJE_ARBOL_Click" />
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="Panel_FONDO_CONFIRMACION_ELIMINACION" runat="server" Visible="false" 
                            Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_CONFIRMACION_ELIMINACION" runat="server">
                            <div style="text-align:center; color:#aa0000; margin:8px; font-weight:bold;">
                                Ventana de Confirmación
                            </div>
                            <table style="margin:2px 0px 2px 8px;">
                                <tr>
                                    <td class="td_der">
                                        Tipo:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label_TipoEntidadEliminacion" runat="server" Text="SUBPROGRAMA / ACTIVIDAD"
                                            Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_der">
                                        ID:
                                    </td>
                                    <td class="td_der">
                                        <asp:Label ID="Label1_IdentificadorEntidadEliminacion" runat="server" Text="ID" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                            <div style="text-align: center; color: Red;">
                                Esta seguro que desea eliminar:<br />
                                <br />
                                <asp:Label ID="Label_DescripcionEntidadEliminacion" runat="server" Text="Descripción de la actividad / subprograma seleccionado para eliminación."
                                    Font-Bold="true" ForeColor="#000066"></asp:Label>
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_AceptarEliminacion" runat="server" Text="Aceptar" 
                                            CssClass="margin_botones" onclick="Button_AceptarEliminacion_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button_CancelarEliminacion" runat="server" Text="Cancelar" 
                                            CssClass="margin_botones" onclick="Button_CancelarEliminacion_Click" />
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador">
                            </div>
                        </asp:Panel>



                        <asp:Panel ID="Panel_FONDO_NUEVO_SUBPROGAMA" runat="server" Visible="false" 
                            Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_NUEVO_SUBPROGRAMA" runat="server">
                            <asp:Panel ID="Panel_InfoSubPrograma" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Información del Subprograma
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Nombre:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_IdSubPrograma" runat="server" Width="350px" ValidationGroup="SUBPROGRAMA"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownList_IdSubPrograma_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_izq">
                                                Estado:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_EstadoSubPrograma" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_IdSubPrograma"
                                        ControlToValidate="DropDownList_IdSubPrograma" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El SUB PROGRAMA es requerido."
                                        ValidationGroup="SUBPROGRAMA" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_IdSubPrograma"
                                        TargetControlID="RequiredFieldValidator_DropDownList_IdSubPrograma" HighlightCssClass="validatorCalloutHighlight" />
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        <tr>
                                            <td style="text-align: center;">
                                                Descripción
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:TextBox ID="TextBox_DescripcionSubPrograma" runat="server" TextMode="MultiLine"
                                                    Width="727px" Height="78px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel_BOTONES_SUB_PROGRAMA" runat="server">
                                        <div class="div_espaciador">
                                        </div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button_GUARDAR_SUB_PROGRAMA" runat="server" Text="Guardar" ValidationGroup="SUBPROGRAMA"
                                                        CssClass="margin_botones" OnClick="Button_GUARDAR_SUB_PROGRAMA_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_CANCELAR_SUB_PROGRAMA" runat="server" Text="Cancelar" ValidationGroup="CANCELARSUBPROGRAMA"
                                                        CssClass="margin_botones" OnClick="Button_CANCELAR_SUB_PROGRAMA_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="Panel_FONDO_ACTIVIDAD" runat="server" Visible="false" 
                            Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_CONTENIDO_ACTIVIDAD" runat="server">
                            <asp:Panel ID="Panel_InfoActividad" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <div class="div_cabeza_groupbox_gris">
                                    Información de la Actividad
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_control_registros">
                                        <tr>
                                            <td class="td_izq">
                                                Nombre:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_IdActividad" runat="server" ValidationGroup="ACTIVIDAD"
                                                    Width="270px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_IdActividad_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_der">
                                                Tipo:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList_Tipo" runat="server" Width="135px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="td_izq">
                                                Sector:
                                            </td>
                                            <td class="td_der">
                                                <asp:DropDownList ID="DropDownList_Sector" runat="server" Width="135px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="div_espaciador">
                                    </div>
                                    <table class="table_control_registros">
                                        </tr>
                                        <td>
                                            Estado:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_EstadoActividad" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </table>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_IdActividad"
                                        ControlToValidate="DropDownList_IdActividad" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ACTIVIDAD es requerida."
                                        ValidationGroup="ACTIVIDAD" />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_IdActividad"
                                        TargetControlID="RequiredFieldValidator_DropDownList_IdActividad" HighlightCssClass="validatorCalloutHighlight" />

                                    <div class="div_espaciador">
                                    </div>

                                    <table class="table_control_registros">
                                        <tr>
                                            <td style="text-align: center;">
                                                Descripción
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:TextBox ID="TextBox_DescripcionActividad" runat="server" TextMode="MultiLine"
                                                    Width="727px" Height="78px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel_BotonesActividad" runat="server">
                                        <div class="div_espaciador">
                                        </div>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button_GAURDAR_ACTIVIDAD" runat="server" Text="Guardar" ValidationGroup="ACTIVIDAD"
                                                        CssClass="margin_botones" OnClick="Button_GAURDAR_ACTIVIDAD_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button_CANCELAR_ACTIVIDAD" runat="server" Text="Cancelar" ValidationGroup="CANCELARACTIVIDAD"
                                                        CssClass="margin_botones" OnClick="Button_CANCELAR_ACTIVIDAD_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_ParteFinalPrograma" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Ultima Parte del Programa General
            </div>
            <div class="div_contenido_groupbox">
                <div style="text-align: left;">
                    <CKEditor:CKEditorControl ID="CKEditorControl_ParteFinal" BasePath="~/ckeditor" 
                        runat="server" Width="880px"></CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CKEditorControl_ParteFinal" ControlToValidate="CKEditorControl_ParteFinal"
                        Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ULTIMA PARTE del Documento es requerida."
                        ValidationGroup="ADICIONAR" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CKEditorControl_ParteFinal"
                        TargetControlID="RequiredFieldValidator_CKEditorControl_ParteFinal" HighlightCssClass="validatorCalloutHighlight" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_ACCION_PIE" runat="server">
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
                                    <asp:Button ID="Button_NUEVO_1" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        ValidationGroup="NUEVO" OnClick="Button_NUEVO_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR_1" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        ValidationGroup="MODIFICAR" OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar" CssClass="margin_botones"
                                        ValidationGroup="ADICIONAR" OnClick="Button_GUARDAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar" CssClass="margin_botones"
                                        ValidationGroup="CANCELAR" OnClick="Button_CANCELAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones"
                                        ValidationGroup="IMPRIMIR" onclick="Button_IMPRIMIR_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
