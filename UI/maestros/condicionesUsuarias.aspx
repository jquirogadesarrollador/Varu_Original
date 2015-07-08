<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="condicionesUsuarias.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    

    <asp:Panel ID="Panel_ObjetivosArea" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_objetivos_area">
                <asp:Label ID="Label_ObjetivosArea" runat="server" Text="Texto de objetivos por Área"></asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
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
                                    <input class="margin_botones" id="Button_CERRAR" onclick="window.close();" 
                                        type="button" value="Salir" />
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
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_CLIENTE" 
                                                onselectedindexchanged="DropDownList_BUSCAR_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_CLIENTE" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="BUSCAR_CLIENTE" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    ControlToValidate="DropDownList_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda." 
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="BUSCAR_CLIENTE" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Numbers" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Letras" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars=" -_.,:;ÑñáéíóúÁÉÍÓÚ0123456789" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="div_espaciador"></div>
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

            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align:center;">
                        <asp:Table ID="Table_MENU_1" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align:center;">
                        <asp:Table ID="Table_MENU_2" runat="server" BorderStyle="None" 
                            CssClass="tabla_botones_menu_hoja_trabajo">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_BUSCAR" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="880px" 
                                    AllowPaging="True" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="ID" 
                                    onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="Raz. social" HeaderText="Raz. social" />
                                        <asp:BoundField DataField="NIT" HeaderText="NIT" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dirección" HeaderText="Dirección" />
                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tel 1" HeaderText="Tel 1" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Celular" HeaderText="Celular" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="Activo" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Código" HeaderText="Código" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FormularioSeleccion" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Datos para Selección            
            </div>
            <div class="div_contenido_groupbox"> 
                
                <div class="div_cabeza_groupbox_gris">
                    Contáctos
                </div>
                <div class="div_contenido_groupbox_gris">
                    <div class="div_contenedor_grilla_resultados">
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridView_ContactosSeleccion" runat="server" Width="865px" 
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                    <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="CONT_NOM" HeaderText="Nombre" />
                                    <asp:BoundField DataField="CONT_CARGO" HeaderText="Cargo" />
                                    <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                    <asp:BoundField DataField="CONT_TEL" HeaderText="Teléfono" />
                                </Columns>
                                <headerStyle BackColor="#DDDDDD" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="div_espaciador"></div>

                <div class="div_cabeza_groupbox_gris">
                    Pruebas y Cargos
                </div>
                <div class="div_contenido_groupbox_gris">
                    <div class="div_contenedor_grilla_resultados">
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridView_PruebasCargos" runat="server" Width="864px" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NOMBRE_PRUEBA" HeaderText="Nombre Prueba" />
                                    <asp:TemplateField HeaderText="Cargos">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropDownList_Cargos" runat="server" Width="450px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="columna_grid_centrada" Width="460px" />
                                    </asp:TemplateField>
                                </Columns>
                                <headerStyle BackColor="#DDDDDD" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="div_espaciador"></div>

                <div class="div_cabeza_groupbox_gris">
                    Condiciones de Envío
                </div>
                <div class="div_contenido_groupbox_gris">
                    <div class="div_contenedor_grilla_resultados">
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridView_CondEnvioSeleccion" runat="server" Width="865px" AutoGenerateColumns="False" 
                                DataKeyNames="REGISTRO,REGISTRO_CONTACTO">
                                <Columns>
                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                    <asp:BoundField DataField="REGISTRO_CONTACTO" HeaderText="REGISTRO_CONTACTO" Visible="False" />
                                    <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                    <asp:BoundField DataField="NOMBRE_DEPARTAMENTO" HeaderText="Departamento" />
                                    <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="NOMBRE_CONTACTO" HeaderText="Contácto" />
                                    <asp:BoundField DataField="MAIL_CONTACTO" HeaderText="E-Mail" />
                                    <asp:BoundField DataField="DIR_ENVIO" HeaderText="Dirección envio" />
                                    <asp:BoundField DataField="TEL_ENVIO" HeaderText="Telefono envio">
                                        <ItemStyle CssClass="columna_grid_der" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="COND_ENVIO" HeaderText="Condición">
                                    <ItemStyle CssClass="columna_grid_jus" />
                                    </asp:BoundField>
                                </Columns>
                                <headerStyle BackColor="#DDDDDD" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_FormularioContratacion" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Datos para Contratación            
            </div>
            <div class="div_contenido_groupbox"> 
                
                <div class="div_cabeza_groupbox_gris">
                    Contáctos
                </div>
                <div class="div_contenido_groupbox_gris">
                    <div class="div_contenedor_grilla_resultados">
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridView_ContactosContratacion" runat="server" Width="865px" 
                                AutoGenerateColumns="False" 
                                onselectedindexchanged="GridView_ContactosContratacion_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                    <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="CONT_NOM" HeaderText="Nombre" />
                                    <asp:BoundField DataField="CONT_CARGO" HeaderText="Cargo" />
                                    <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                    <asp:BoundField DataField="CONT_TEL" HeaderText="Teléfono" />
                                </Columns>
                                <headerStyle BackColor="#DDDDDD" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="div_espaciador"></div>

                <table class="table_control_registros">
                    <tr>
                        <td valign="top" style="width:435px;">
                            <div class="div_cabeza_groupbox_gris">
                                Examenes Médicos
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_ExamenesMedicosContratacion" runat="server" Width="418px"
                                                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView_ExamenesMedicosContratacion_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="NOMBRE_EXAMEN" HeaderText="Examen Médico" />
                                                        <asp:TemplateField HeaderText="Cargos">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList_Cargos" runat="server" Width="265px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        <td valign="top" style="width: 435px;">
                            <div class="div_cabeza_groupbox_gris">
                                Bancos por Ciudad
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="div_contenedor_grilla_resultados">
                                            <div class="grid_seleccionar_registros">
                                                <asp:GridView ID="GridView_BancosContratacion" runat="server" Width="418px" AutoGenerateColumns="False"
                                                    AllowPaging="True" OnPageIndexChanging="GridView_BancosContratacion_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                                        <asp:TemplateField HeaderText="Bancos">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DropDownList_Bancos" runat="server" Width="265px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <headerStyle BackColor="#DDDDDD" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>

                <table class="table_control_registros">
                    <tr>
                        <td valign="top" style="width:300px;">
                            <div class="div_cabeza_groupbox_gris">
                                Cobertura
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                       <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_contratacion_cobertura" runat="server" AllowPaging="true"
                                                            AutoGenerateColumns="False" 
                                                            DataKeyNames="Código Ciudad" 
                                                            Width="270px" onrowcommand="GridView_contratacion_cobertura_RowCommand" 
                                                            onpageindexchanging="GridView_contratacion_cobertura_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField DataField="Código Ciudad" HeaderText="Código Ciudad" 
                                                                    Visible="False" />
                                                                <asp:ButtonField CommandName="Ciudad" DataTextField="Ciudad" 
                                                                    HeaderText="Ciudad" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="" SelectText="Cargar" 
                                                                    ShowSelectButton="True" ButtonType="Image"
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                            </div>
                        </td>
                        

                        <td valign="top" style="width:300px;">
                            <div class="div_cabeza_groupbox_gris">
                                Centros de costo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                       <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_contratacion_cc" runat="server"  AllowPaging="true"
                                                            AutoGenerateColumns="False" 
                                                            DataKeyNames="ID_CENTRO_C" 
                                                            Width="270px" onrowcommand="GridView_contratacion_cc_RowCommand" 
                                                            onpageindexchanging="GridView_contratacion_cc_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_CENTRO_C" HeaderText="ID_CENTRO_C" 
                                                                    Visible="False" />
                                                                <asp:ButtonField CommandName="CentroCosto" DataTextField="NOM_CC" 
                                                                    HeaderText="Centro de costo" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="" SelectText="Cargar" 
                                                                    ShowSelectButton="True" ButtonType="Image"
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                            </div>
                        </td>

                        <td valign="top" style="width:300px;">
                            <div class="div_cabeza_groupbox_gris">
                                Sub centros de costo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                       <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_contratacion_subCC" runat="server"  AllowPaging="true"
                                                            AutoGenerateColumns="False"
                                                            DataKeyNames="ID_SUB_C" 
                                                            Width="270px" 
                                                            onpageindexchanging="GridView_contratacion_subCC_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID_SUB_C" HeaderText="ID_SUB_C" 
                                                                    Visible="False" />
                                                                <asp:ButtonField CommandName="SubCentro" DataTextField="NOM_SUB_C" 
                                                                    HeaderText="Subcentro de costo" >
                                                                <ItemStyle CssClass="columna_grid_izq" />
                                                                </asp:ButtonField>
                                                                <asp:CommandField HeaderText="" SelectText="Cargar" 
                                                                    ShowSelectButton="True" ButtonType="Image"
                                                                    SelectImageUrl="~/imagenes/plantilla/view.gif" >
                                                                <ItemStyle CssClass="columna_grid_centrada" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <headerStyle BackColor="#DDDDDD" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                            </div>
                        </td>

                    </tr>
                </table>
                <div class="div_espaciador"></div>
            </div>
        </div>
        <asp:HiddenField ID="HiddenField_contratacion_idEmpresa" runat="server" />
        <asp:HiddenField ID="HiddenField_contratacion_idCiudad" runat="server" />
        <asp:HiddenField ID="HiddenField_contratacion_idCentroCosto" runat="server" />
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server" Visible="False">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Manejo de Empresas
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_CONTROL_REGISTRO" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Control de Registro
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_CRE" runat="server" Text="Fecha de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_CRE" runat="server" Text="Hora de Creación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_CRE" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_CRE" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_FCH_MOD" runat="server" Text="Fecha de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_FCH_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_HOR_MOD" runat="server" Text="Hora de Modificación"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_HOR_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_USU_MOD" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_USU_MOD" runat="server" Enabled="False" 
                                        ReadOnly="True" ValidationGroup="REGISTRO_CONTROL"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <asp:Panel ID="Panel_COD_EMPRESA" runat="server">
                    <div class="div_cabeza_groupbox_gris">
                        Código y estado del cliente
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_COD_EMPRESA" runat="server" Text="Código de Cliente"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_COD_EMPRESA" runat="server" 
                                                ReadOnly="True" ValidationGroup="COD_CLIENTE"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div_espaciador"></div>
                </asp:Panel>
                <table class="table_form_dos_columnas" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table class="table_align_izq" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div class="div_cabeza_groupbox_gris">
                                            Información del Cliente
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_FCH_INGRESO" runat="server" Text="Fecha de Ingreso"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_FCH_INGRESO" runat="server" Width="100px" 
                                                                MaxLength="10" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_NIT_EMPRESA" runat="server" Text="NIT"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_NIT_EMPRESA" runat="server" MaxLength="15" 
                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_DIG_VER" runat="server" Text="DV"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DIG_VER" runat="server" Width="40px" MaxLength="1" 
                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Text="Razón social"></asp:Label>
                                                    </td>
                                                    <td colspan="5" class="td_der">
                                                        <asp:TextBox ID="TextBox_RAZ_SOCIAL" runat="server" Width="380px" 
                                                            MaxLength="60" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_DIR_EMP" runat="server" Text="Dirección"></asp:Label>
                                                    </td>
                                                    <td colspan="5" class="td_der">
                                                        <asp:TextBox ID="TextBox_DIR_EMP" runat="server" Width="380px" MaxLength="40" 
                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad
                                                    </td>
                                                    <td colspan="5" class="td_der">
                                                        <asp:TextBox ID="TextBox_CIUDAD_CLIENTE" runat="server" Width="380px" MaxLength="40" 
                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="div_espaciador"></div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>

                                                    </td>
                                                    <td>
                                                        <div class="div_cabeza_groupbox_gris">
                                                            Telefonos
                                                        </div>
                                                        <div class="div_contenido_groupbox_gris">
                                                            <table >
                                                                <tr>
                                                                    <td class="td_izq">
                                                                        <asp:Label ID="Label_TEL_EMP" runat="server" Text="Telefono 1"></asp:Label>
                                                                    </td>
                                                                    <td class="td_der">
                                                                        <asp:TextBox ID="TextBox_TEL_EMP" runat="server" Width="100px" MaxLength="30" 
                                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                                    </td>
                                                                    <td class="td_izq">
                                                                        <asp:Label ID="Label_TEL_EMP_1" runat="server" Text="Telefono 2"></asp:Label>
                                                                    </td>
                                                                    <td class="td_der">
                                                                        <asp:TextBox ID="TextBox_TEL_EMP_1" runat="server" Width="100px" MaxLength="30" 
                                                                            ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" class="td_izq">
                                                                        <asp:Label ID="Label_CEL_EMP" runat="server" Text="Celular"></asp:Label>
                                                                    </td>
                                                                    <td colspan="2" class="td_der">
                                                                        <asp:TextBox ID="TextBox_CEL_EMP" runat="server" Width="100px" 
                                                                            ValidationGroup="NUEVOCLIENTE" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="div_espaciador"></div>
                            
                            <div class="div_cabeza_groupbox_gris">
                                Información Facturación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_NUM_EMPLEADOS" runat="server" Text="Núm. Empleados"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NUM_EMPLEADOS" runat="server" Width="90px" 
                                                MaxLength="6" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div_espaciador"></div>
                            <asp:Panel ID="Panel_COBERTURA" runat="server">
                                <div class="div_cabeza_groupbox_gris">
                                    Cobertura
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div id="div_contenedor_grid_cobertura_condiciones_usuarias" class="scroll">
                                                <asp:GridView ID="GridView_COVERTURA" runat="server" Width="450px" 
                                                    AllowSorting="True" >
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/imagenes/borrar.png" 
                                                            ShowSelectButton="True" HeaderText="Acción" >
                                                        <ControlStyle Height="15px" Width="15px" />
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </td>
                        <td valign="top">
                            <asp:Panel ID="Panel_ACTIVIDAD_ECONOMICA" runat="server">
                                <div class="div_cabeza_groupbox_gris">
                                    Actividad Económica
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_ACTIVIDAD" runat="server" Text="Actividad"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_ACTIVIDAD_ECONOMICA" runat="server" MaxLength="40" 
                                                            ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_DES_ACTIVIDAD" runat="server" Text="Descripción"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_DES_ACTIVIDAD" runat="server" TextMode="MultiLine" 
                                                            Height="70px" Width="250px" MaxLength="50" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <div class="div_espaciador"></div>
                            <asp:Panel ID="Panel_CIUDAD_ORIGINO_NEGOCIO" runat="server">
                                <div class="div_cabeza_groupbox_gris">
                                    Ciudad que Originó el Negocio
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label8" runat="server" Text="Ciudad"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_CIUDAD_ORIGINO" runat="server" MaxLength="40" 
                                                            ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="div_espaciador"></div>
                            </asp:Panel>
                            <div class="div_cabeza_groupbox_gris">
                                Representante Legal del Cliente
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <table class="table_control_registros">
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_NOM_REP_LEGAL" runat="server" Text="Nombre"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_NOM_REP_LEGAL" runat="server" Width="250px" 
                                                        MaxLength="40" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_CC_REP_LEGAL" runat="server" Text="Cedula"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_CC_REP_LEGAL" runat="server" Width="250px" 
                                                        MaxLength="15" ValidationGroup="NUEVOCLIENTE"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_CIU_CC_REP_LEGAL" runat="server" Text="Ciudad"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_CIUDAD_REPRESENTANTE" runat="server" MaxLength="15" 
                                                        ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Representante Sertempo
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_REP_SERTEMPO" runat="server" Text="Nombre"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_REPRESENTANTE_SERTEMPO" runat="server" MaxLength="15" 
                                                ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Unidad de Negocio
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <asp:Panel ID="Panel_UNIDAD_NEGOCIO" runat="server">
                                    <table>
                                        <tr>
                                            <td class="td_izq">
                                                <asp:Label ID="Label_ID_SICOLOGO" runat="server" Text="Psicólogo"></asp:Label>
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_SICOLOGO" runat="server" MaxLength="15" 
                                                    ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Analista de<br />
                                                Nómina
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_ANALISTA" runat="server" MaxLength="15" 
                                                    ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_izq">
                                                Gestor<br />
                                                Especialista
                                            </td>
                                            <td class="td_der">
                                                <asp:TextBox ID="TextBox_GESTOR" runat="server" MaxLength="15" 
                                                    ValidationGroup="NUEVOCLIENTE" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_Formulario_Nomina" runat="server" Visible="false">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Condiciones de Nómina
            </div>
            <div class="div_contenido_groupbox">

                <div class="div_cabeza_groupbox_gris">
                    Contáctos
                </div>
                <div class="div_contenido_groupbox_gris">
                    <div class="div_contenedor_grilla_resultados">
                        <div class="grid_seleccionar_registros">
                            <asp:GridView ID="GridView_CONTACTOSNOMINA" runat="server" Width="850px" 
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NOMBRE_REGIONAL" HeaderText="Regional" />
                                    <asp:BoundField DataField="NOMBRE_CIUDAD" HeaderText="Ciudad" />
                                    <asp:BoundField DataField="CONT_NOM" HeaderText="Nombre" />
                                    <asp:BoundField DataField="CONT_CARGO" HeaderText="Cargo" />
                                    <asp:BoundField DataField="CONT_MAIL" HeaderText="E-Mail" />
                                    <asp:BoundField DataField="CONT_TEL" HeaderText="Teléfono" />
                                </Columns>
                                <headerStyle BackColor="#DDDDDD" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="div_cabeza_groupbox_gris">
                    Condiciones de Nómina
                </div>

                <div class="div_contenido_groupbox_gris">

                <!--
                <asp:Panel ID="Panel_ID" runat="server" Visible="false" >
                    
                    <div class="div_cabeza_groupbox_gris">
                        Código de identificación
                    </div>
                    
                    <div class="div_contenido_groupbox_gris">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label1" runat="server" Text="Código"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_REGISTRO" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="HiddenField_idEmpresa" runat="server" />
                                <div class="div_espaciador">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div_espaciador">
                    </div>
                </asp:Panel>

-->
                <asp:UpdatePanel ID="UpdatePanel_DATOS" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel_DATOS" runat="server">
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Período de pago
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_ID_PERIODO_PAGO" runat="server" 
                                            Width="150px" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_izq">
                                        Fechas de pago
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FECHA_PAGOS" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Pagar subsidio de transporte?
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_PAGAR_SUB_TRANSPORTE" runat="server" />
                                    </td>
                                    <td>
                                        Período de pago subsidio de transporte
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBoxList ID="CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Promedio dominical
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_CALC_PROM_DOMINICAL" runat="server" />
                                    </td>
                                    <td class="td_izq">
                                        Ajustar SMMLV
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_AJUSTAR_SMLV" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Mostrar Unificada
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_MOSTRAR_UNIFICADA" runat="server" />
                                    </td>
                                    <td class="td_izq">
                                        No descontar seguridad social al trabajador
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_DES_SEG_SOC_TRAB" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="td_izq">
                                        Base horas extras
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_BAS_HOR_EXT" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="td_izq">
                                        Fecha inicia primera nómina
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FCH_INI_PRI_PER_NOM" runat="server" Width="150px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_FCH_INI_PRI_PER_NOM" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_INI_PRI_PER_NOM" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_FCH_INI_PRI_PER_NOM"
                                            runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_FCH_INI_PRI_PER_NOM"
                                            ValidChars="/">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Último período de nomina
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_ULT_PERIODO" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        Fecha último período de nomina
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FCH_ULT_LIQ_PER" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Último período de memorando
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_ULT_PERIODO_MEM" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td class="td_izq">
                                        Fecha último período de memorando
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_FCH_ULT_LIQ_MEM" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Módelo calcúlo de rentención
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_CALCULO_RETENCION_FUENTE" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                                                        <td class="td_izq">
                                        <asp:Label ID="Label_PORCENTAJE_FACT_CENTRO" runat="server" 
                                            Text="Porcenteje Facturación:"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBox_PORCENTAJE_FACT" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label_PORCENTAJE" runat="server" 
                                                        Text="%"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Fact. Parafiscales fin de mes:
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_FACT_PARAFISCALES" runat="server"/>
                                    </td>
                                    <td class="td_izq">
                                        Dia sabado no hábil:
                                    </td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_SABADO_NO_HABIL" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="td_izq">
                                        Liquidar Ordinarias en el ultimo periodo del mes</td>
                                    <td class="td_der">
                                        <asp:CheckBox ID="CheckBox_ORDINARIAS_ULTIMO_PERIODO" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_ID_PERIODO_PAGO -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ID_PERIODO_PAGO"
                                ControlToValidate="DropDownList_ID_PERIODO_PAGO" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El PERIODO DE PAGO es requerido."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ID_PERIODO_PAGO"
                                TargetControlID="RequiredFieldValidator_DropDownList_ID_PERIODO_PAGO" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- DropDownList_BAS_HOR_EXT -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BAS_HOR_EXT"
                                ControlToValidate="DropDownList_BAS_HOR_EXT" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La BASE HORAS EXTRAS es requerida."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BAS_HOR_EXT"
                                TargetControlID="RequiredFieldValidator_DropDownList_BAS_HOR_EXT" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- TextBox_FCH_INI_PRI_PER_NOM -->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_FCH_INI_PRI_PER_NOM"
                                runat="server" ControlToValidate="TextBox_FCH_INI_PRI_PER_NOM" Display="None"
                                ErrorMessage="Campo Requerido faltante</br>La FECHA INICIA PRIMERA NÓMINA es requerida."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_FCH_INI_PRI_PER_NOM"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_FCH_INI_PRI_PER_NOM" />
                            <!-- DropDownList_CALCULO_RETENCION_FUENTE -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CALCULO_RETENCION_FUENTE"
                                ControlToValidate="DropDownList_CALCULO_RETENCION_FUENTE" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El MODELO CALCULO RETENCION es requerido."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CALCULO_RETENCION_FUENTE"
                                TargetControlID="RequiredFieldValidator_DropDownList_CALCULO_RETENCION_FUENTE"
                                HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="div_espaciador">
                </div>

                <table class="table_form_dos_columnas" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="Panel_incapacidades" runat="server">
                                <div class="div_cabeza_groupbox_gris">
                                    Conceptos establecidos para incapacidades
                                </div>
                                <div class="div_contenido_groupbox_gris">
                                    <asp:UpdatePanel ID="UpdatePanel_incapacidades" runat="server">
                                        <ContentTemplate>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        <asp:Label ID="Label_ID_CONCEPTO_INCAPACIDAD" runat="server" Text="Concepto"></asp:Label>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_ID_CONCEPTO_INCAPACIDAD" runat="server" Width="250px"
                                                            AutoPostBack="True" ValidationGroup="AdicionarConceptoIncapacidad">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:TextBox ID="TextBox_PORCENTAJE_PAGO_INCAPACIDAD" runat="server" Width="50px"
                                                            ValidationGroup="AdicionarConceptoIncapacidad"></asp:TextBox>
                                                    </td>
                                                    <td class="td_centrada">
                                                        <asp:ImageButton ID="ImageButton_adicionar_pago_incapacidad" runat="server" Height="20px"
                                                            ImageUrl="~/imagenes/adicionar.png" Width="20px"
                                                            ValidationGroup="AdicionarConceptoIncapacidad" 
                                                            ToolTip="Adicionar concepto de incapacidad" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- DropDownList_ID_CONCEPTO_INCAPACIDAD -->
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_ID_CONCEPTO_INCAPACIDAD"
                                                ControlToValidate="DropDownList_ID_CONCEPTO_INCAPACIDAD" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El CONCEPTO es requerido."
                                                ValidationGroup="AdicionarConceptoIncapacidad" />
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_ID_CONCEPTO_INCAPACIDAD"
                                                TargetControlID="RequiredFieldValidator_DropDownList_ID_CONCEPTO_INCAPACIDAD"
                                                HighlightCssClass="validatorCalloutHighlight" />
                                            <!-- TextBox_PORCENTAJE_PAGO_INCAPACIDAD -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_PORCENTAJE_PAGO_INCAPACIDAD"
                                                runat="server" ControlToValidate="TextBox_PORCENTAJE_PAGO_INCAPACIDAD" Display="None"
                                                ErrorMessage="Campo Requerido faltante</br>El PORCENTAJE para INCAPACIDAD es requerido."
                                                ValidationGroup="AdicionarConceptoIncapacidad" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_PORCENTAJE_PAGO_INCAPACIDAD"
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_PORCENTAJE_PAGO_INCAPACIDAD" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_PORCENTAJE_PAGO_INCAPACIDAD"
                                                runat="server" TargetControlID="TextBox_PORCENTAJE_PAGO_INCAPACIDAD" FilterType="Custom,Numbers"
                                                ValidChars=",">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="div_espaciador">
                                            </div>
                                            <div class="div_contenedor_grilla_resultados">
                                                <div class="grid_seleccionar_registros">
                                                    <asp:GridView ID="GridView_ID_CONCEPTO_INCAPACIDAD" runat="server" Width="100%" AllowPaging="True" 
                                                        AutoGenerateColumns="False" DataKeyNames="Código, Cod_Concepto, Concepto,Porcentaje" 
                                                        onpageindexchanging="GridView_ID_CONCEPTO_INCAPACIDAD_PageIndexChanging"
                                                        onselectedindexchanged="GridView_ID_CONCEPTO_INCAPACIDAD_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/delete.gif" >
                                                            <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                            </asp:CommandField>
                                                            <asp:BoundField DataField="Código" HeaderText="Id" Visible="False" >
                                                            <FooterStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Cod_Concepto" HeaderText="Codigo" Visible="True" >
                                                            <FooterStyle CssClass="columna_grid_centrada" />
                                                            <headerStyle HorizontalAlign="Center" />
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Concepto" HeaderText="Concepto">
                                                            <FooterStyle CssClass="columna_grid_izq" />
                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje">
                                                            <FooterStyle CssClass="columna_grid_centrada" />
                                                            <headerStyle HorizontalAlign="Center" />
                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>

                                                    <%--<asp:GridView ID="GridView_ID_CONCEPTO_INCAPACIDAD" runat="server" Width="100%">
                                                        <Columns>
                                                            <asp:CommandField SelectText="Eliminar" ShowSelectButton="True" ButtonType="Image"
                                                                SelectImageUrl="~/imagenes/plantilla/delete.gif">
                                                                <ItemStyle Width="100px" CssClass="columna_grid_centrada" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>--%>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                            <div class="div_espaciador">
                            </div>
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="Panel_ESTRUCTURA_MODELO_NOMINA" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel_ESTRUCTURA_MODELO_NOMINA" runat="server">
                        <ContentTemplate>
                            <table width="95%">
                                <tr>
                                    <td valign="top" style="width: 33%;">
                                        <div class="div_cabeza_groupbox_gris">
                                            <asp:Label ID="Label_COBERTURA" runat="server" Text="COBERTURA"></asp:Label>
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <asp:UpdatePanel runat="server" ID="UP_MENSAJE_COBERTURA">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel_MENSAJE_COBERTURA" runat="server">
                                                        <div class="div_espacio_validacion_campos">
                                                            <asp:Label ID="Label_MENSAJE_COBERTURA" runat="server" ForeColor="Red" />
                                                        </div>
                                                        <div class="div_espaciador">
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_COBERTURA" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="Código Ciudad, Ciudad" Width="275px" OnRowCommand="GridView_COBERTURA_RowCommand"
                                                                    OnSelectedIndexChanged="GridView_COBERTURA_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Código Ciudad" HeaderText="Código Ciudad" Visible="False" />
                                                                        <asp:ButtonField CommandName="Ciudad" DataTextField="Ciudad" HeaderText="Ciudad">
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:ButtonField>
                                                                        <asp:CommandField HeaderText="Condiciones" SelectText="Ver" ShowSelectButton="True"
                                                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view.gif">
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="HiddenField_cobertura" runat="server" Value="0" />
                                        </div>
                                    </td>
                                    <td valign="top" style="width: 33%;">
                                        <div class="div_cabeza_groupbox_gris">
                                            <asp:Label ID="Label_CC" runat="server" Text="CENTROS DE COSTO"></asp:Label>
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <asp:UpdatePanel runat="server" ID="UP_MENJSAJE_CC">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel_MENSAJE_CC" runat="server">
                                                        <div class="div_espacio_validacion_campos">
                                                            <asp:Label ID="Label_MENSAJE_CC" runat="server" ForeColor="Red" />
                                                        </div>
                                                        <div class="div_espaciador">
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_CENTROS_DE_COSTO" runat="server" AutoGenerateColumns="False"
                                                                    Width="275px" DataKeyNames="ID_CENTRO_C,COD_CC, NOM_CC" OnRowCommand="GridView_CENTROS_DE_COSTO_RowCommand"
                                                                    OnSelectedIndexChanged="GridView_CENTROS_DE_COSTO_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID_CENTRO_C" HeaderText="ID_CENTRO_C" Visible="False" />
                                                                        <asp:BoundField DataField="COD_CC" HeaderText="Código CC" Visible="False" />
                                                                        <asp:ButtonField CommandName="centrocosto" DataTextField="NOM_CC" HeaderText="Centro de Costo"
                                                                            Text="CC">
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:ButtonField>
                                                                        <asp:CommandField HeaderText="Condiciones" SelectText="Ver" ShowSelectButton="True"
                                                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view.gif">
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="HiddenField_cc" runat="server" Value="0" />
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <div class="div_cabeza_groupbox_gris">
                                            <asp:Label ID="Label_SUB_CC" runat="server" Text="SUB CENTROS DE COSTO"></asp:Label>
                                        </div>
                                        <div class="div_contenido_groupbox_gris">
                                            <asp:UpdatePanel runat="server" ID="UP_MENSAJE_SUB_CC">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel_MENSAJE_SUB_CC" runat="server">
                                                        <div class="div_espacio_validacion_campos">
                                                            <asp:Label ID="Label_MENSAJE_SUB_CC" runat="server" ForeColor="Red" />
                                                        </div>
                                                        <div class="div_espaciador">
                                                        </div>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td>
                                                        <div class="div_contenedor_grilla_resultados">
                                                            <div class="grid_seleccionar_registros">
                                                                <asp:GridView ID="GridView_SUB_CENTROS_DE_COSTO" runat="server" AutoGenerateColumns="False"
                                                                    DataKeyNames="ID_SUB_C,COD_SUB_C,NOM_SUB_C" Width="275px" 
                                                                    OnRowCommand="GridView_SUB_CENTROS_DE_COSTO_RowCommand">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID_SUB_C" HeaderText="ID_SUB_C" Visible="False" />
                                                                        <asp:BoundField DataField="COD_SUB_C" HeaderText="COD_SUB_C" Visible="False" />
                                                                        <asp:ButtonField CommandName="subcentrocosto" DataTextField="NOM_SUB_C" HeaderText="Sub Centro"
                                                                            Text="Button">
                                                                            <ItemStyle CssClass="columna_grid_izq" />
                                                                        </asp:ButtonField>
                                                                        <asp:CommandField HeaderText="Condiciones" InsertText="Ver" SelectText="Ver" ShowSelectButton="True"
                                                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view.gif">
                                                                            <ItemStyle CssClass="columna_grid_centrada" />
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                    <headerStyle BackColor="#DDDDDD" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="HiddenField_subCC" runat="server" Value="0" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="HiddenField_descripcion" runat="server" Value="" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="GridView_COBERTURA" />
                            <asp:PostBackTrigger ControlID="GridView_CENTROS_DE_COSTO" />
                            <asp:PostBackTrigger ControlID="GridView_SUB_CENTROS_DE_COSTO" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                </div>

            </div>
        </div>
    </asp:Panel>

    <div class="div_espaciador"></div>
</asp:Content>

