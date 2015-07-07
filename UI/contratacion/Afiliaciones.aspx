<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="Afiliaciones.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <asp:HiddenField ID="HiddenField_TIPO_BUSQUEDA_ACTUAL" runat="server" />
    <asp:HiddenField ID="HiddenField_FILTRO_DROP" runat="server" />       
    <asp:HiddenField ID="HiddenField_FILTRO_DATO" runat="server" />

    <asp:HiddenField ID="HiddenField_ID_EMPRESA" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_SOLICITUD" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_REQUERIMIENTO" runat="server" />
    <asp:HiddenField ID="HiddenField_ID_OCUPACION" runat="server" />
    <asp:HiddenField ID="HiddenField_NUM_DOC_IDENTIDAD" runat="server" />

    <asp:HiddenField ID="HiddenField_ID_PERFIL" runat="server" />

    <asp:HiddenField ID="HiddenField_CIUDAD_REQ" runat="server" />

    <asp:HiddenField ID="HiddenField_persona" runat="server" />

    <asp:Panel ID="Panel_INFO_ADICIONAL_MODULO" runat="server">
        <div class="div_contenedor_formulario">
            <div id="div_info_adicional_modulo">
                <asp:Label ID="Label_INFO_ADICIONAL_MODULO" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" Style="margin: 0 auto;
        text-align: center;">
        <div class="div_botones_internos">
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: justify">
                        <asp:Table ID="Table_MENU" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>


    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_contenedor_formulario">
            <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
                <tr>
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
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    
    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel_FONDO_MENSAJE" runat="server" Visible="false" Style="background-color: #999999;">
            </asp:Panel>
            <asp:Panel ID="Panel_MENSAJES" runat="server">
                <asp:Image ID="Image_MENSAJE_POPUP" runat="server" Width="50px" Height="50px" style="margin: 5px auto 8px auto; display:block;" />
                <asp:Panel ID="Panel_COLOR_FONDO_POPUP" runat="server" style="text-align:center">
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red" >Este es el 
                    mensaje, tiene un&nbsp; texto de acuerdo 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button_CERRAR_MENSAJE" runat="server" Text="Cerrar" 
                        OnClick="Button_CERRAR_MENSAJE_Click" style="height: 26px" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_RESULTADOS_GRID" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label_RESULTADOS_GRILLA" runat="server" Text="Resultados de la busqueda"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_para_convencion_hoja_trabajo">
                    <table class="tabla_alineada_derecha">
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label_ALERTA_BAJA" runat="server" Text="0"></asp:Label>
                                &nbsp;Personas enviadas a contratar hoy.
                            </td>
                            <td class="style2">
                                <div class="div_color_verde">
                                </div>
                            </td>
                            <td>
                                <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="0"></asp:Label>
                                &nbsp;Personas en plazo de contratación.
                            </td>
                            <td class="td_contenedor_colores_hoja_trabajo">
                                <div class="div_color_amarillo">
                                </div>
                            </td>
                            <td>
                                <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="0"></asp:Label>
                                &nbsp;Personas con plazo de contratación vencido.
                            </td>
                            <td class="td_contenedor_colores_hoja_trabajo">
                                <div class="div_color_rojo">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div_contenedor_grilla_resultados">
                    <div class="grid_seleccionar_registros">
                        <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA" runat="server" Width="885px" AutoGenerateColumns="False"
                            DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION" AllowPaging="True"
                            OnRowCommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view.gif"
                                    Text="Seleccionar">
                                    <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                <asp:BoundField DataField="ID_EMPRESA" HeaderText="ID_EMPRESA" Visible="False" />
                                <asp:BoundField DataField="ID_OCUPACION" HeaderText="ID_OCUPACION" Visible="False" />
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo Documento Identidad">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identidad">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" DataFormatString="% {0:C}">
                                    <ItemStyle CssClass="columna_grid_der" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORARIO" HeaderText="Horario" />
                                <asp:BoundField DataField="DURACION" HeaderText="Duración" />
                                <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado">
                                    <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:BoundField>
                                <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                <asp:BoundField DataField="fechaPorContrar" HeaderText="Fecha Enviado a Contratar"
                                    DataFormatString="{0:dd/M/yyyy}">
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

    <asp:Panel ID="Panel_INFORMACION_CANDIDATO_SELECCION" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información Empleado Seleccionado
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Nombre
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NOMBRE_TRABAJADOR" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="td_izq">
                            Documento de Identidad
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_NUM_DOC_IDENTIDAD" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Empresa
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RAZ_SOCIAL" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Cargo
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_CARGO" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Riesgo
                        </td>
                        <td class="td_der">
                            <asp:Label ID="Label_RIESGO" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_UbicacionTrabajador" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Ubicación del Trabajador
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td class="td_izq">
                            Ciudad
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_CIUDAD_TRABAJADOR" runat="server" Width="260px"
                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_CIUDAD_TRABAJADOR_SelectedIndexChanged"
                                ValidationGroup="UBICACION">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label_CIUDAD_SELECCIONADA" runat="server" Text=" " Height="20px" Width="20px"
                                BackColor="#009900"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Centro de costo
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_CC_TRABAJADOR" runat="server" Width="260px" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownList_CC_TRABAJADOR_SelectedIndexChanged" ValidationGroup="DATOS_CONTRATO">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label_CC_SELECCIONADO" runat="server" Text=" " Height="20px" Width="20px"
                                BackColor="#009900"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_izq">
                            Sub centro de costo
                        </td>
                        <td class="td_der">
                            <asp:DropDownList ID="DropDownList_SUB_CENTRO_TRABAJADOR" runat="server" Width="260px"
                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SUB_CENTRO_TRABAJADOR_SelectedIndexChanged"
                                ValidationGroup="DATOS_CONTRATO">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label_SUBC_SELECCIONADO" runat="server" Text=" " Height="20px" Width="20px"
                                BackColor="#009900"></asp:Label>
                        </td>
                    </tr>
                </table>
                <!-- DropDownList_CIUDAD_TRABAJADOR -->
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_CIUDAD_TRABAJADOR"
                    ControlToValidate="DropDownList_CIUDAD_TRABAJADOR" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La CIUDADes requerida."
                    ValidationGroup="UBICACION" />
                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_CIUDAD_TRABAJADOR"
                    TargetControlID="RequiredFieldValidator_DropDownList_CIUDAD_TRABAJADOR" HighlightCssClass="validatorCalloutHighlight" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_ARP" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="div_espaciador">
                </div>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Administración de ARL
                    </div>
                    <div class="div_contenido_groupbox">
                        <asp:HiddenField ID="HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION" runat="server" />
                        <asp:Panel ID="Panel_FONDO_MENSAJE_ARP" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_ARP" runat="server">
                            <asp:Image ID="Image_MENSAJE_ARP_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_ARP_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_ARP" runat="server" ForeColor="Red">Este es el 
                                mensaje, tiene un&nbsp; texto de acuerdo a 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_ARP" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_ARP_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_registros_ARP" runat="server">
                            <div class="div_espaciador">
                                <asp:HiddenField ID="HiddenField_id_arl" runat="server" />
                            </div>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de radicación</td>
                                    <td colspan="2" class="td_der">
                                        <asp:TextBox ID="TextBox_fecha_r" runat="server" Width="260px"></asp:TextBox>
                                    </td>
                                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="TextBox_fecha_r"
                                        Format="dd/MM/yyyy" />
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_ARP" runat="server" ValidationGroup="ARP"
                                            AutoPostBack="true" Width="260px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td colspan="3" style="text-align: center;">
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_ARP_OBSERVACIONES" runat="server" TextMode="MultiLine" ValidationGroup="ARP"
                                            Width="780px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Button ID="Button_GUARDAR_ARP" runat="server" CssClass="margin_botones" OnClick="Button_GUARDAR_ARP_Click"
                                            Text="Guardar" ValidationGroup="ARP" />
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_fecha_r-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_fecha_r" 
                                runat="server" ControlToValidate="TextBox_fecha_r" Display="None" 
                                ErrorMessage="Campo Requerido faltante</br>La FECHA DE RADICACION es requerida." 
                                ValidationGroup="ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_r" 
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                TargetControlID="RequiredFieldValidator_TextBox_fecha_r" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_fecha_r" 
                                runat="server" TargetControlID="TextBox_fecha_r" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                            <!-- DropDownList_ENTIDAD_ARP-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ENTIDAD_ARP"
                                runat="server" ControlToValidate="DropDownList_ENTIDAD_ARP" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ENTIDAD es requerido."
                                ValidationGroup="ARP" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_ARP"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_ARP" />
                        </asp:Panel>
                        <table class="table_control_registros" width="700">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_ARP" runat="server" Width="700px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_ARP,ID_SOLICITUD,ID_REQUERIMIENTO,REGISTRO" OnRowCommand="GridView_ARP_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                    <asp:BoundField DataField="ID_ARP" HeaderText="ID_ARP" Visible="False" />
                                                    <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                    <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_SOLICITUD" Visible="False" />
                                                    <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/MM/yyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                    <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USU_CRE" HeaderText="Usuario Crea">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <div class="div_espaciador">
    </div>

    <asp:Panel ID="Panel_EPS" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Entidad promotora de salud
                        
                    </div>
                    <asp:HiddenField ID="HiddenField_id_eps" runat="server" />
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_FONDO_MENSAJE_EPS" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_EPS" runat="server">
                            <asp:Image ID="Image_MENSAJE_EPS_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_EPS_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_EPS" runat="server" ForeColor="Red">Este es el 
                                mensaje, tiene un texto de acuerdo a la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_EPS" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_EPS_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_registro_EPS" runat="server">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de radicación</td>
                                    <td colspan="2" class="td_der">
                                        <asp:TextBox ID="TextBox_fecha_EPS" runat="server" Width="260px"></asp:TextBox>
                                    </td>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fecha_EPS" runat="server" TargetControlID="TextBox_fecha_EPS"
                                        Format="dd/MM/yyyy" />
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_EPS" runat="server" ValidationGroup="EPS"
                                            AutoPostBack="true" Width="260px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td style="text-align: center;">
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_COMENTARIOS_EPS" runat="server" TextMode="MultiLine" ValidationGroup="EPS"
                                            Width="780px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_EPS" runat="server" CssClass="margin_botones" OnClick="Button_GUARDAR_EPS_Click"
                                            Text="Guardar" ValidationGroup="EPS" />
                                    </td>
                                </tr>
                            </table>

                            <!-- TextBox_fecha_EPS-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_fecha_EPS" 
                                runat="server" ControlToValidate="TextBox_fecha_EPS" Display="None" 
                                ErrorMessage="Campo Requerido faltante</br>La FECHA DE RADICACION es requerida." 
                                ValidationGroup="EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_EPS" 
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                TargetControlID="RequiredFieldValidator_TextBox_fecha_EPS" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_fecha_EPS" 
                                runat="server" TargetControlID="TextBox_fecha_EPS" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>


                            <!-- DropDownList_ENTIDAD_ARP-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ENTIDAD_EPS"
                                runat="server" ControlToValidate="DropDownList_ENTIDAD_EPS" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La Entidad es requerida."
                                ValidationGroup="EPS" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_EPS"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_EPS" />
                        </asp:Panel>
                        <table class="table_control_registros" width="700">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_EPS" runat="server" Width="700px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_EPS,REGISTRO,ID_SOLICITUD,ID_REQUERIMIENTO" OnRowCommand="GridView_EPS_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                    <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                    <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                    <asp:BoundField DataField="ID_EPS" HeaderText="ID_EPS" Visible="False" />
                                                    <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/MM/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                    <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USU_CRE" HeaderText="Usuario Crea">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <div class="div_espaciador">
    </div>

    <asp:Panel ID="Panel_AFP" runat="server">
        <asp:UpdatePanel ID="UpdatePanel_AFP" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Administradora de fondos de pensiones
                    </div>
                    <asp:HiddenField ID="HiddenField_id_afp" runat="server" />
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_FONDO_MENSAJE_AFP" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_AFP" runat="server">
                            <asp:Image ID="Image_MENSAJE_AFP_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_AFP_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_AFP" runat="server" ForeColor="Red">Este es el 
                                mensaje, tiene un&nbsp; texto de acuerdo a 
                                la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_AFP" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_AFP_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_registros_afp" runat="server">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de radicación
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:TextBox ID="TextBox_Fecha_AFP" runat="server" Width="260px"></asp:TextBox>
                                    </td>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_AFP" runat="server" TargetControlID="TextBox_Fecha_AFP"
                                        Format="dd/MM/yyyy" />
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:DropDownList ID="DropDownList_AFP" runat="server" ValidationGroup="AFP" AutoPostBack="true"
                                            Width="260px" style="height: 22px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Pensionado
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:DropDownList ID="DropDownList_pensionado" runat="server" ValidationGroup="AFP"
                                            AutoPostBack="true" Width="260px" OnSelectedIndexChanged="DropDownList_pensionado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panel_tipo_pensionado" runat="server">
                                <div class="div_espaciador">
                                </div>
                                <table class="table_control_registros" cellpadding="0">
                                    <tr>
                                        <td class="td_izq">
                                            Tipo pensionado
                                        </td>
                                        <td colspan="2" class="td_der">
                                            <asp:DropDownList ID="DropDownList_tipo_pensionado" runat="server" ValidationGroup="AFP_tipo"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            Número de resolución o del trámite
                                        </td>
                                        <td colspan="2" class="td_der">
                                            <asp:TextBox ID="TextBox_Numero_resolucion_tramite" runat="server" ValidationGroup="AFP_tipo"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td style="text-align: center;">
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_COMENTARIOS_AFP" runat="server" TextMode="MultiLine" ValidationGroup="AFP"
                                            Width="780px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_AFP" runat="server" CssClass="margin_botones" OnClick="Button_GUARDAR_AFPClick"
                                            Text="Guardar" ValidationGroup="AFP" />
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_Fecha_AFP-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Fecha_AFP" 
                                runat="server" ControlToValidate="TextBox_Fecha_AFP" Display="None" 
                                ErrorMessage="Campo Requerido faltante</br>La FECHA DE RADICACION es requerida." 
                                ValidationGroup="AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Fecha_AFP" 
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                TargetControlID="RequiredFieldValidator_TextBox_Fecha_AFP" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Fecha_AFP" 
                                runat="server" TargetControlID="TextBox_Fecha_AFP" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_tipo_pensionado"
                                    runat="server" ControlToValidate="DropDownList_tipo_pensionado" Display="None"
                                    ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El tipo de pensionado es requerido."
                                    ValidationGroup="AFP" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_tipo_pensionado"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_tipo_pensionado" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Numero_resolucion_tramite"
                                    ControlToValidate="TextBox_Numero_resolucion_tramite" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />Necesita digitar el número de la resolucion o del tramite segun el caso."
                                    ValidationGroup="AFP" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Numero_resolucion_tramite"
                                    TargetControlID="RequiredFieldValidator_TextBox_Numero_resolucion_tramite" HighlightCssClass="validatorCalloutHighlight" />


                            <!-- DropDownList_AFP-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_AFP" runat="server"
                                ControlToValidate="DropDownList_AFP" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;La ENTIDAD es requerida."
                                ValidationGroup="AFP" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_AFP"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_AFP" />
                        </asp:Panel>
                        <table class="table_control_registros" width="700">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_AFP" runat="server" Width="700px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_F_PENSIONES,REGISTRO,ID_SOLICITUD,ID_REQUERIMIENTO" OnRowCommand="GridView_AFP_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                    <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                    <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                    <asp:BoundField DataField="ID_F_PENSIONES" HeaderText="ID_F_PENSIONES" Visible="False" />
                                                    <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/MM/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                    <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USU_CRE" HeaderText="Usuario Crea">
                                                    <ItemStyle CssClass="columna_grid_centrada" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <div class="div_espaciador">
    </div>

    <asp:Panel ID="Panel_CCF" runat="server">
        <asp:UpdatePanel ID="UpdatePanel_CCF" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="div_contenedor_formulario">
                    <div class="div_cabeza_groupbox">
                        Caja de compensación familiar
                    </div>
                    <asp:HiddenField ID="HiddenField_id_ccf" runat="server" />
                    <div class="div_contenido_groupbox">
                        <asp:Panel ID="Panel_FONDO_MENSAJE_CCF" runat="server" Visible="false" Style="background-color: #999999;">
                        </asp:Panel>
                        <asp:Panel ID="Panel_MENSAJES_CCF" runat="server">
                            <asp:Image ID="Image_MENSAJE_CCF_POPUP" runat="server" Width="50px" Height="50px"
                                Style="margin: 5px auto 8px auto; display: block;" />
                            <asp:Panel ID="Panel_COLOR_FONDO_CCF_POPUP" runat="server" Style="text-align: center">
                                <asp:Label ID="Label_MENSAJE_CCF" runat="server" ForeColor="Red">Este es el 
                                mensaje, tiene untexto de acuerdo a 
                                la acción correspondiente.</asp:Label>
                            </asp:Panel>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="Button_CERRAR_MENSAJE_CCF" runat="server" Text="Cerrar" OnClick="Button_CERRAR_MENSAJE_CCF_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Registro_CCF" runat="server">
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td class="td_izq">
                                        Fecha de radicación
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:TextBox ID="TextBox_Fecha_Caja" runat="server" Width="260px"></asp:TextBox>
                                    </td>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fecha_caja" runat="server" TargetControlID="TextBox_Fecha_Caja"
                                        Format="dd/MM/yyyy" />
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Entidad
                                    </td>
                                    <td colspan="2" class="td_der">
                                        <asp:DropDownList ID="DropDownList_ENTIDAD_Caja" runat="server" ValidationGroup="CCF"
                                            AutoPostBack="true" Width="260px" 
                                            onselectedindexchanged="DropDownList_ENTIDAD_Caja_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="Panel_CiudadCaja" runat="server">
                                            <div class="div_espaciador">
                                            </div>
                                            <table class="table_control_registros">
                                                <tr>
                                                    <td class="td_izq">
                                                        Departamento:
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_DepartamentoCajaC" runat="server" AutoPostBack="true"
                                                            Width="260px" OnSelectedIndexChanged="DropDownList_DepartamentoCajaC_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_izq">
                                                        Ciudad:
                                                    </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList ID="DropDownList_CiudadCajaC" runat="server" ValidationGroup="CCF"
                                                            Width="260px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <table class="table_control_registros" cellpadding="0">
                                <tr>
                                    <td style="text-align: center;">
                                        Observaciones<br />
                                        <asp:TextBox ID="TextBox_observaciones_Caja" runat="server" TextMode="MultiLine"
                                            ValidationGroup="CCF" Width="780px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button_GUARDAR_CAJA" runat="server" CssClass="margin_botones" OnClick="Button_GUARDAR_CAJA_Click"
                                            Text="Guardar" ValidationGroup="CCF" />
                                    </td>
                                </tr>
                            </table>

                            <!-- TextBox_Fecha_Caja-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_Fecha_Caja" 
                                runat="server" ControlToValidate="TextBox_Fecha_Caja" Display="None" 
                                ErrorMessage="Campo Requerido faltante</br>La FECHA DE RADICACION es requerida." 
                                ValidationGroup="CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_Fecha_Caja" 
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                TargetControlID="RequiredFieldValidator_TextBox_Fecha_Caja" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Fecha_Caja" 
                                runat="server" TargetControlID="TextBox_Fecha_Caja" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>


                            <!-- DropDownList_ENTIDAD_CAJA-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_ENTIDAD_Caja"
                                runat="server" ControlToValidate="DropDownList_ENTIDAD_Caja" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El CARGO es requerido."
                                ValidationGroup="CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_ENTIDAD_Caja"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_ENTIDAD_Caja" />

                            <!-- DropDownList_CiudadCajaC-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DropDownList_CiudadCajaC"
                                runat="server" ControlToValidate="DropDownList_CiudadCajaC" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;la CIUDAD es requerida."
                                ValidationGroup="CCF" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_DropDownList_CiudadCajaC"
                                runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_DropDownList_CiudadCajaC" />
                        </asp:Panel>
                        <table class="table_control_registros" width="700">
                            <tr>
                                <td>
                                    <div class="div_contenedor_grilla_resultados">
                                        <div class="grid_seleccionar_registros">
                                            <asp:GridView ID="GridView_CAJA" runat="server" Width="760px" AutoGenerateColumns="False"
                                                DataKeyNames="ID_CAJA_C,REGISTRO,ID_SOLICITUD,ID_REQUERIMIENTO,ID_DEPARTAMENTO,ID_CIUDAD" OnRowCommand="GridView_CAJA_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="seleccionar" ImageUrl="~/imagenes/areas/view2.gif"
                                                        Text="Seleccionar">
                                                        <ItemStyle CssClass="columna_grid_centrada" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" Visible="False" />
                                                    <asp:BoundField DataField="ID_SOLICITUD" HeaderText="ID_SOLICITUD" Visible="False" />
                                                    <asp:BoundField DataField="ID_REQUERIMIENTO" HeaderText="ID_REQUERIMIENTO" Visible="False" />
                                                    <asp:BoundField DataField="ID_CAJA_C" HeaderText="ID_CAJA_C" Visible="False" />
                                                    <asp:BoundField DataField="FECHA_R" HeaderText="Fecha radicacion" DataFormatString="{0:dd/MM/yyyy}">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOM_ENTIDAD" HeaderText="Nombre Entidad" />
                                                    <asp:BoundField DataField="DATOS_CIUDAD" HeaderText="Ciudad" />
                                                    <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                                        <ItemStyle CssClass="columna_grid_jus" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USU_CRE" HeaderText="Usuario Crea">
                                                        <ItemStyle CssClass="columna_grid_centrada" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    
</asp:Content>
