<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="ManualServicio.aspx.cs" Inherits="_ManualServicio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:HiddenField ID="HiddenField_ID_VERSIONAMIENTO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />

    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />       
    <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

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
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="USUARIO" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_Imprimir" runat="server" Text="Imprimir" CssClass="margin_botones"
                                        ValidationGroup="IMPRIMIR" onclick="Button_Imprimir_Click" />
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
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE"
                                                OnSelectedIndexChanged="DropDownList_BUSCAR_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" OnClick="Button_BUSCAR_Click"
                                                ValidationGroup="BUSCAR_CLIENTE" CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                ControlToValidate="DropDownList_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda."
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                ControlToValidate="TextBox_BUSCAR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar."
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                TargetControlID="RequiredFieldValidator_TextBox_BUSCAR" HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers"
                                runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Numbers" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Letras"
                                runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_BUSCAR" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class="div_espaciador"></div>
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AllowPaging="True"
                            OnPageIndexChanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" AutoGenerateColumns="False"
                            DataKeyNames="ID" OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="Raz. social" HeaderText="Raz. social" />
                                <asp:BoundField DataField="NIT" HeaderText="NIT">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                <asp:BoundField DataField="Telefono" HeaderText="Telefono">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tel 1" HeaderText="Tel 1">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Celular" HeaderText="Celular">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Código" HeaderText="Código">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            </Columns>
                            <headerStyle BackColor="#DDDDDD" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_ContenedorManual" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Manual de Servicio
            </div>
            <div class="div_contenido_groupbox">

                <asp:Panel ID="Panel_Cabacera" runat="server">
                    <table border="1" cellpadding="2" cellspacing="0" class="table_control_registros"
                        width="98%">
                        <tr>
                            <td colspan="2" style="width: 35%;">
                                <asp:Image ID="Image_LOGO_EMPRESA_MANUAL" runat="server" Height="60px" Width="150px" />
                            </td>
                            <td colspan="3" style="font-weight: bold; text-align: center; width: 75%">
                                PROCESO GESTIÓN COMERCIAL
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="font-weight: bold; text-align: center;">
                                MANUAL DE SERVICIO
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>FECHA DE EMISIÓN</b><br />
                                <asp:Label ID="Label_FECHA_EMISION" runat="server" Text="fecha emisión"></asp:Label>
                            </td>
                            <td colspan="2">
                                <b>VERSIÓN:</b><br />
                                <asp:Label ID="Label_VERSION" runat="server" Text="version"></asp:Label>
                            </td>
                            <td>
                                <b>APLICAR A PARTIR:</b><br />
                                <asp:Label ID="Label_APLICAR_A_APARTIR" runat="server" Text="a partir"></asp:Label>
                            </td>
                            <td>
                                <b>PAGINA</b><br />
                                <asp:Label ID="Label_PAGINA" runat="server" Text="pagina"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnl_Info_basica_Comercial" runat = "SERVER">
                    <div>
                    
                        
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_IdentificacionCliente" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        1. IDENTIFICACIÓN DEL CLIENTE
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table id="tabla_identificacion_cliente" class="table_control_registros">
                            <tr>
                                <td class="td_label">
                                    Razón Social:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Text="razon social"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label">
                                    Actividad Económica:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_ActividadEconomica" runat="server" Text="actividad economica"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label">
                                    NIT:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_NIT" runat="server" Text="nit"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label">
                                    Representante Legal:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_REPRESENTANTE_LEGAL" runat="server" Text="representante legal"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label">
                                    Cedula de Ciudadania:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_CEDULA_REPRESENTANTE" runat="server" Text="cedula representante"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label">
                                    Dirección y Ciudad:
                                </td>
                                <td class="td_label">
                                    <asp:Label ID="Label_DIRECCION_CLIENTE" runat="server" Text="direccion y ciudad del cliente"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Telefono:
                                </td>
                                <td class="td_label" style="height: 21px">
                                    <asp:Label ID="Label_Telefono" runat="server" Text="telefono"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="titulos_manual">
                            1.1 Información básica comercial
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GVInfoBasica" runat="server" Width="800px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" >
                                        <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="SI" DataField="SI" >
                                        <ItemStyle CssClass="columna_grid_centrada" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO" HeaderText="NO" >
                                        <ItemStyle CssClass="columna_grid_centrada" Width="24px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="ACLARACION">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Height="50px" 
                                                    Text='<%# Bind("ACLARACION") %>' Width="400px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACLARACION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>  
                    </div>
                </asp:Panel>
                

                <asp:Panel ID="Panel_ControlModificaciones" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        CONTROL DE MODIFICACIONES
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ControlModificaciones" runat="server" Width="800px" 
                                    AutoGenerateColumns="False" DataKeyNames="ID_HISTORIAL,ID_VERSIONAMIENTO,ID_EMPRESA">
                                    <Columns>
                                        <asp:BoundField HeaderText="VERSION" DataField="VERSION_COMPLETA">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_EMISION" DataFormatString="{0:dd/M/yyyy}" 
                                            HeaderText="Fecha Emisión">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AREA" HeaderText="AREA">
                                        <ItemStyle CssClass="columna_gris_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCION" HeaderText="ACCIÓN">
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CAMBIO" DataField="CAMBIO">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QUIEN ELABORA EL CAMBIO" DataField="USU_CRE" />
                                        <asp:TemplateField HeaderText="Ver Manual">
                                            <ItemTemplate>
                                                <div style="text-align:center;">
                                                    <asp:HyperLink ID="HyperLink_archivo" runat="server">Ver Manual</asp:HyperLink>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel_GestionComercial" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        2. GESTIÓN COMERCIAL
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <div class="titulos_manual">
                            2.1 CONTÁCTOS COMERCIALES
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ContactosComerciales" runat="server" Width="800px" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="Contácto" DataField="CONT_NOM"></asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        <asp:BoundField HeaderText="Cargo" DataField="CONT_CARGO"></asp:BoundField>
                                        <asp:BoundField DataField="CONT_TEL" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="titulos_manual">
                            2.2 UNIDAD DE NEGOCIO
                        </div>

                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_UNIDAD_NEGOCIO" runat="server" AutoGenerateColumns="False"
                                        Width="800px" DataKeyNames="ID_EMPRESA_USUARIO,ID_USUARIO">
                                        <Columns>
                                            <asp:BoundField DataField="ID_EMPRESA_USUARIO" HeaderText="ID_EMPRESA_USUARIO" Visible="False" />
                                            <asp:BoundField DataField="ID_USUARIO" HeaderText="ID_USUARIO" Visible="False" />
                                            <asp:BoundField DataField="USU_LOG" HeaderText="Usuario" />
                                            <asp:BoundField DataField="NOMBRES_EMPLEADO" HeaderText="Empleado" />
                                            <asp:BoundField DataField="UNIDAD_NEGOCIO" HeaderText="Unidad de Negocio" />
                                        </Columns>
                                        <headerStyle BackColor="#DDDDDD" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="titulos_manual">
                            2.3 COBERTURA
                        </div>

                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_COBERTURA" runat="server" Width="800px"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Código Ciudad" HeaderText="Código Ciudad">
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Regional" HeaderText="Regional" />
                                            <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                            <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel_AdicionalesComercial" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <div class="titulos_manual">
                                2.3 DESCRIPCIONES ADICIONALES
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    
                                    <asp:HiddenField ID="HiddenField_ACCION_GRILLA" runat="server" />
                                    <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA" runat="server" />
                                    <asp:HiddenField ID="HiddenField_ID_ADICIONAL" runat="server" />
                                    <asp:HiddenField ID="HiddenField_TITULO" runat="server" />
                                    <asp:HiddenField ID="HiddenField_DESCRIPCION" runat="server" />

                                    <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_AdicionalesComercial" runat="server" Width="100%" 
                                                AutoGenerateColumns="False" 
                                                OnRowCommand="GridView_AdicionalesComercial_RowCommand" DataKeyNames="ID_ADICIONAL,ID_EMPRESA">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                        Text="Eliminar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Actualizar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField HeaderText="Descripciones Adicionales">
                                                        <ItemTemplate>
                                                            <div style="text-align: left; font-weight: bold;">
                                                                <asp:TextBox ID="TextBox_TituloAdicional" runat="server" Width="350px"></asp:TextBox>
                                                            </div>
                                                            <asp:TextBox ID="TextBox_DescripcionAdicional" runat="server" TextMode="MultiLine"
                                                                Width="100%" Height="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:Panel ID="Panel_BotonesAdicionalesComercial" runat="server">
                                        <div style="text-align: left; margin-left: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button_NUEVOADICIONALCOMERCIAL" runat="server" Text="Nuevo Adicional"
                                                            CssClass="margin_botones" ValidationGroup="NUEVOADICIONALCOMERCIAL" OnClick="Button_NUEVOADICIONALCOMERCIAL_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_GUARDARADICIONALCOMERCIAL" runat="server" Text="Guardar Adicional"
                                                            CssClass="margin_botones" ValidationGroup="GUARDARADICIONALCOMERCIAL" OnClick="Button_GUARDARADICIONALCOMERCIAL_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_CANCELARADICIONALCOMERCIAL" runat="server" Text="Cancelar"
                                                            CssClass="margin_botones" ValidationGroup="CANCELARADICIONALCOMERCIAL" OnClick="Button_CANCELARADICIONALCOMERCIAL_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </asp:Panel>


                <asp:Panel ID="Panel_Seleccion" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        3. PROCESO DE SEECCIÓN
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <div class="titulos_manual">
                            3.1 CARGOS Y PERFILES
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_PERFILES" runat="server" Width="800px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO,ID_EMPRESA">
                                    <Columns>
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="CARGO" />
                                        <asp:BoundField DataField="EDAD_MAX" HeaderText="EDAD MÁXIMA">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EDAD_MIN" HeaderText="EDAD MÍNIMA">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEXO" HeaderText="SEXO">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EXPERIENCIA" HeaderText="EXPERIENCIA" />
                                        <asp:BoundField DataField="NIV_ESTUDIOS" HeaderText="NIVEL ESTUDIOS" />
                                        <asp:BoundField DataField="REGISTRO" HeaderText="ID" Visible="False" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="div_espaciador">
                        </div>
                        <div class="titulos_manual">
                            3.2 DOCUMENTOS Y PRUEBAS APLICADAS A PERFILES
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_DocumentosPruebasPerfiles" runat="server" Width="800px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO">
                                    <Columns>
                                        <asp:BoundField DataField="NOM_OCUPACION" HeaderText="CARGO" />

                                        <asp:BoundField DataField="DOCUMENTOS_REQUERIDOS" 
                                            HeaderText="Documentos Requeridos">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRUEBAS_APLICADAS" HeaderText="Pruebas Aplicadas">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>

                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="div_espaciador">
                        </div>
                        <div class="titulos_manual">
                            3.3 CONDICIONES DE ENVIO
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 800px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_CondicionesEnvioSeleccion" runat="server" Width="800px"
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO,REGISTRO_CONTACTO">
                                    <Columns>
                                        <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                        <asp:BoundField DataField="NOMBRE_DEPARTAMENTO" HeaderText="Departamento" />
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="NOMBRE_CONTACTO" HeaderText="Contácto" />
                                        <asp:BoundField DataField="MAIL_CONTACTO" HeaderText="E-Mail" />
                                        <asp:BoundField DataField="DIR_ENVIO" HeaderText="Dirección envio" />
                                        <asp:BoundField DataField="TEL_ENVIO" HeaderText="Telefono envio">
                                            <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COND_ENVIO" HeaderText="Condiciones">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>

                        <asp:Panel ID="Panel_AdicionalesSeleccion" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <div class="titulos_manual">
                                2.3 DESCRIPCIONES ADICIONALES
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    
                                    <asp:HiddenField ID="HiddenField_ACCION_GRILLA_SELECCION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_ID_ADICIONAL_SELECCION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_TITULO_SELECCION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_DESCRIPCION_SELECCION" runat="server" />

                                    <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_AdicionalesSeleccion" runat="server" Width="100%" 
                                                AutoGenerateColumns="False" DataKeyNames="ID_ADICIONAL,ID_EMPRESA" 
                                                onrowcommand="GridView_AdicionalesSeleccion_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                        Text="Eliminar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Actualizar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField HeaderText="Descripciones Adicionales">
                                                        <ItemTemplate>
                                                            <div style="text-align: left; font-weight: bold;">
                                                                <asp:TextBox ID="TextBox_TituloAdicional" runat="server" Width="350px"></asp:TextBox>
                                                            </div>
                                                            <asp:TextBox ID="TextBox_DescripcionAdicional" runat="server" TextMode="MultiLine"
                                                                Width="100%" Height="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:Panel ID="Panel_BotonesAdicionalesSeleccion" runat="server">
                                        <div style="text-align: left; margin-left: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button_NUEVOADICIONALSELECCION" runat="server" Text="Nuevo Adicional"
                                                            CssClass="margin_botones" ValidationGroup="NUEVOADICIONALSELECCION" 
                                                            onclick="Button_NUEVOADICIONALSELECCION_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_GUARDARADICIONALSELECCION" runat="server" Text="Guardar Adicional"
                                                            CssClass="margin_botones" ValidationGroup="GUARDARADICIONALSELECCION" 
                                                            onclick="Button_GUARDARADICIONALSELECCION_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_CANCELARADICIONALSELECCION" runat="server" Text="Cancelar"
                                                            CssClass="margin_botones" ValidationGroup="CANCELARADICIONALSELECCION" 
                                                            onclick="Button_CANCELARADICIONALSELECCION_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </asp:Panel>


                <asp:Panel ID="Panel_Contratacion" runat="server">
                    <div class="div_espaciador">
                    </div>
                    <div class="div_cabeza_groupbox_gris">
                        4. CONTRATACIÓN Y RELACIONES LABORALES
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <div class="titulos_manual">
                            4.1 INFORMACIÓN BÁSICA DE CONDICIONES DE CONTRATACIÓN POR PERFIL
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 840px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_CondicionesContratacion" runat="server" Width="840px"
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO_CONTRATACION">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="PERFIL" HeaderText="Perfil" />
                                        <asp:BoundField DataField="NOM_SUB_C" HeaderText="Sub centro" />
                                        <asp:BoundField DataField="NOM_CC" HeaderText="Centro costo" />
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        
                                        <asp:BoundField DataField="PORCENTAJE_RIESGO" HeaderText="Riesgo %">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOC_TRAB" 
                                            HeaderText="Documentos Entregados al  Trabajador">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBS_CTE" HeaderText="Requerimientos">
                                            <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>


                        <div class="div_espaciador">
                        </div>

                        <div class="titulos_manual">
                            4.2 EXÁMENES MEDICOS DE INGRESO POR PERFIL
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 840px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ExamenesMedicosPerfil" runat="server" Width="840px"
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO_CONTRATACION">
                                    <Columns>
                                        <asp:BoundField DataField="PERFIL" HeaderText="Perfil" />
                                        <asp:BoundField DataField="NOM_SUB_C" HeaderText="Sub centro" />
                                        <asp:BoundField DataField="NOM_CC" HeaderText="Centro costo" />
                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                        
                                        <asp:BoundField DataField="EXAMENES_MEDICOS_REQUERIDOS"
                                            HeaderText="Examenes Medicos Requeridos" HtmlEncode="False" 
                                            HtmlEncodeFormatString="False">
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="div_espaciador">
                        </div>


                        <div class="titulos_manual">
                            4.3 CLAUSULAS ADICIONALES AL CONTRATO
                        </div>
                        <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;
                            width: 840px;">
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_ClausulasAdicionalesContrato" runat="server" Width="840px" AutoGenerateColumns="False"
                                    DataKeyNames="REGISTRO">
                                    <Columns>
                                        <asp:BoundField DataField="PERFIL" HeaderText="Perfil" />

                                        <asp:BoundField DataField="CLAUSULAS_ADICIONALES" 
                                            HeaderText="Clausulas Adicionales" HtmlEncode="False" 
                                            HtmlEncodeFormatString="False">
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="div_espaciador">
                        </div>

                        <div class="titulos_manual">
                            4.4 ENVÍO DE DOCUMENTOS AL CLIENTE
                        </div>
                        <div style="margin:5px auto; text-align:justify; display: block;
                            width: 840px;">
                            <asp:Label ID="Label_TextoEnvíoDocuemntosCliente" runat="server" Text="texto correspondiente a la configuración de envío de docuemntos al cliente."></asp:Label>
                        </div>


                        <asp:Panel ID="Panel_AdicionalesContratacion" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <div class="div_espaciador">
                            </div>
                            <div class="titulos_manual">
                                4.5 DESCRIPCIONES ADICIONALES
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    
                                    <asp:HiddenField ID="HiddenField_ACCION_GRILLA_CONTRATACION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_ID_ADICIONAL_CONTRATACION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_TITULO_CONTRATACION" runat="server" />
                                    <asp:HiddenField ID="HiddenField_DESCRIPCION_CONTRATACION" runat="server" />

                                    <div class="div_contenedor_grilla_resultados" style="margin: 0 auto; display: block;">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_AdicionalesContratacion" runat="server" Width="100%" 
                                                AutoGenerateColumns="False" DataKeyNames="ID_ADICIONAL,ID_EMPRESA" 
                                                onrowcommand="GridView_AdicionalesContratacion_RowCommand" >
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="eliminar" ImageUrl="~/imagenes/areas/delete.gif"
                                                        Text="Eliminar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" CommandName="modificar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Actualizar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField HeaderText="Descripciones Adicionales">
                                                        <ItemTemplate>
                                                            <div style="text-align: left; font-weight: bold;">
                                                                <asp:TextBox ID="TextBox_TituloAdicional" runat="server" Width="350px"></asp:TextBox>
                                                            </div>
                                                            <asp:TextBox ID="TextBox_DescripcionAdicional" runat="server" TextMode="MultiLine"
                                                                Width="100%" Height="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <headerStyle BackColor="#DDDDDD" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:Panel ID="Panel_BotonesAdicionalesContratacion" runat="server">
                                        <div style="text-align: left; margin-left: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="Button_NUEVOADICIONALCONTRATACION" runat="server" Text="Nuevo Adicional"
                                                            CssClass="margin_botones" ValidationGroup="NUEVOADICIONALCONTRATACION" 
                                                            onclick="Button_NUEVOADICIONALCONTRATACION_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_GUARDARADICIONALCONTRATACION" runat="server" Text="Guardar Adicional"
                                                            CssClass="margin_botones" ValidationGroup="GUARDARADICIONALCONTRATACION" 
                                                            onclick="Button_GUARDARADICIONALCONTRATACION_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button_CANCELARADICIONALCONTRATACION" runat="server" Text="Cancelar"
                                                            CssClass="margin_botones" ValidationGroup="CANCELARADICIONALCONTRATACION" 
                                                            onclick="Button_CANCELARADICIONALCONTRATACION_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>

                    </div>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="Panel_BOTONES_ACCION_1" runat="server">
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
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR_1" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="NUEVOCLIENTE"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_IMPRIMIR_1" runat="server" Text="Imprimir" CssClass="margin_botones"
                                        ValidationGroup="IMPRIMIR" onclick="Button_Imprimir_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_CANCELAR_1" runat="server" Text="Cancelar"  
                                        CssClass="margin_botones" ValidationGroup="CANCELAR" 
                                        onclick="Button_CANCELAR_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>

