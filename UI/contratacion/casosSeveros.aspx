<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="casosSeveros.aspx.cs" Inherits="contratacion_casosSeveros" %>

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

    <asp:Panel ID="Panel_FORM_BOTONES_ENCABEZADO" runat="server">
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
                                        CssClass="margin_botones" onclick="Button_NUEVO_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar"  
                                        CssClass="margin_botones" onclick="Button_MODIFICAR_Click"/>
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar"  
                                        CssClass="margin_botones" onclick="Button_GUARDAR_Click" 
                                        ValidationGroup="ADICIONAR"/>
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
                <td valign="top">
                    <asp:UpdatePanel ID="UpdatePanel_busqueda" runat="server">
                        <ContentTemplate>
                            <div class="div_cabeza_groupbox">
                                Sección de busqueda
                            </div>
                            <div class="div_contenido_groupbox">
                                <table cellpadding="0" cellspacing="0" border="0" style="margin:0 auto;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                onselectedindexchanged="DropDownList_BUSCAR_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" ValidationGroup="BUSCAR_REFERENCIA" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCAR" runat="server" Text="Buscar" 
                                                onclick="Button_BUSCAR_Click" ValidationGroup="BUSCAR_REFERENCIA" 
                                                CssClass="margin_botones" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- DropDownList_BUSCAR -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    ControlToValidate="DropDownList_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Debe seleccionar el campo sobre el cual desea realizar la busqueda." 
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_DropDownList_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_BUSCAR"
                                    ControlToValidate="TextBox_BUSCAR"
                                    Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar los datos a buscar." 
                                ValidationGroup="BUSCAR_REFERENCIA" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_BUSCAR"
                                    TargetControlID="RequiredFieldValidator_TextBox_BUSCAR"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Numbers" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Numbers" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_BUSCAR_Letras" runat="server" TargetControlID="TextBox_BUSCAR" FilterType="Custom, LowercaseLetters, UppercaseLetters" ValidChars=" ,.:;-_ñÑáéíóúÁÉÍÓÚ()[]{}" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel_MENSAJES">
        <ContentTemplate>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="div_espaciador"></div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_Buscar"/>
        </Triggers>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" 
                                    AllowPaging="True" 
                                    onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="NUM_CONTRATO,ID_EMPLEADO">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="NUM_CONTRATO" HeaderText="No. Contrato" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID Empleado" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACTIVO" HeaderText="Activo" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                        <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="No. Documento" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA"/>
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_DATOS_CONTRATO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información del contrato
            </div>
            <div class="div_contenido_groupbox">
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                            
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_NUM_CONTRATO" runat="server" Text="No. contrato"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NUM_CONTRATO" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_ACTIVO" runat="server" Text="Activo"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_ACTIVO" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Text="Empresa"></asp:Label>
                                </td>
                                <td colspan="3" class="td_der">
                                    <asp:TextBox ID="TextBox_RAZ_SOCIAL" runat="server" Width="520" ReadOnly="True" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_ID_EMPLEADO" runat="server" Text="Id Empleado"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_ID_EMPLEADO" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_NUM_DOC_IDENTIDAD" runat="server" Text="No. Documento"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NUM_DOC_IDENTIDAD" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_APELLIDOS" runat="server" Text="Apellidos"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_APELLIDOS" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                                <td class="td_izq">
                                    <asp:Label ID="Label_NOMBRES" runat="server" Text="Nombres"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_NOMBRES" runat="server" ReadOnly="True" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_RESULTADOS_GRID_CASOS_SEVEROS" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label1" runat="server" Text="Información de Gestantes/Casos Severos"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA_CASOS_SEVEROS" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS" runat="server" Width="885px" 
                                    AllowPaging="True" 
                                    onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS_PageIndexChanging" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO">
                                   <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="FECHA_R" HeaderText="Fecha" 
                                            DataFormatString="{0:dd/MM/yyyy}" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OBS_REG" HeaderText="Observaciones" >
                                        <ItemStyle CssClass="columna_grid_jus" />
                                        </asp:BoundField>
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS"/>
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de Gestantes/Casos Severos
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

                <asp:UpdatePanel ID="UpdatePanel_DATOS" runat="server">
                <ContentTemplate>
                <asp:Panel ID="Panel_DATOS" runat="server">
                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                        <tr>
                        <td>
                            <div class="div_cabeza_groupbox_gris">
                                Información de Gestantes/Casos Severos</div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                        </td>
                                        
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID" runat="server" Width="200px" MaxLength="10" Visible="false"></asp:TextBox>
                                        </td>

                                        <td class="td_izq">
                                        </td>

                                        <td class="td_der">
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA_R" runat="server" Text="Fecha"></asp:Label>
                                        </td>
                                        
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA_R" runat="server" Width="200px" 
                                                    MaxLength="10" ValidationGroup="ADICIONAR"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA_R" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_FECHA_R" />
                                        </td>

                                        <td class="td_izq">
                                        </td>

                                        <td class="td_der">
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_OBS_REG" runat="server" Text="Observaciones"></asp:Label>
                                        </td>
                                        
                                        <td class="td_der" colspan="3">
                                            <asp:TextBox ID="TextBox_OBS_REG" runat="server" Width="500px" 
                                                    MaxLength="255" ValidationGroup="ADICIONAR" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <div class="div_espaciador"></div>
                                </table>
                                
                                <!-- TextBox_FECHA_R -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FECHA_R" 
                                    runat="server" ControlToValidate="TextBox_FECHA_R" Display="None" 
                                    ErrorMessage="Campo Requerido faltante</br>La FECHA es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FECHA_R" 
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                    TargetControlID="RequiredFieldValidator_FECHA_R" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FECHA_R" 
                                    runat="server" TargetControlID="TextBox_FECHA_R" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                                <!-- TextBox_OBS_REG -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_OBS_REG"
                                    ControlToValidate="TextBox_OBS_REG"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La Observación es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_OBS_REG"
                                    TargetControlID="RequiredFieldValidator_OBS_REG"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES_PIE" runat="server">
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
                                        ValidationGroup="ADICIONAR"/>
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

