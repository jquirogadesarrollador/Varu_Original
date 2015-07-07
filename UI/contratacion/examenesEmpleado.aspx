<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="examenesEmpleado.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

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

    <asp:HiddenField ID="HiddenField_ESTADO_PROCESO" runat="server" />

    <asp:HiddenField ID="HiddenField_persona" runat="server" />

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

    <asp:Panel ID="Panel_BOTONES_INTERNOS" runat="server" Width="900px" style="margin:0 auto; text-align:center;">
        <div class="div_espaciador"></div>
        <div class="div_botones_internos">
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: justify">
                        <asp:Table ID="Table_MENU" runat="server">
                        </asp:Table>
                        <asp:Table ID="Table_MENU_1" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORM_BOTONES" runat="server">
        <div class="div_espaciador"></div>
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
                Resultados de la busqueda
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_para_convencion_hoja_trabajo">
                    <table class="tabla_alineada_derecha">
                        <tr>
                            <td class="td_izq">
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
                            DataKeyNames="ID_REQUERIMIENTO,ID_SOLICITUD,ID_EMPRESA,ID_OCUPACION" 
                            AllowPaging="True" 
                            onpageindexchanging="GridView_RESULTADOS_BUSQUEDA_PageIndexChanging" 
                            onrowcommand="GridView_RESULTADOS_BUSQUEDA_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="seleccionar" 
                                    ImageUrl="~/imagenes/areas/view.gif" Text="Seleccionar">
                                <ItemStyle CssClass="columna_grid_centrada" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="Apellidos" />
                                <asp:BoundField DataField="TIP_DOC_IDENTIDAD" HeaderText="Tipo Documento Identidad" />
                                <asp:BoundField DataField="NUM_DOC_IDENTIDAD" HeaderText="Número Identidad" />
                                <asp:BoundField DataField="ID_Y_NOMBRE_CARGO" HeaderText="Cargo" />
                                <asp:BoundField DataField="RAZ_SOCIAL" HeaderText="Empresa" />
                                <asp:BoundField DataField="SALARIO" HeaderText="Salario" />
                                <asp:BoundField DataField="HORARIO" HeaderText="Horario" />
                                <asp:BoundField DataField="DURACION" HeaderText="Duración" />
                                <asp:BoundField DataField="ESTADO_PROCESO" HeaderText="Estado" />
                                <asp:BoundField DataField="USUARIO_PROCESO" HeaderText="Usuario" />
                                <asp:BoundField DataField="fechaPorContrar" HeaderText="Fecha Enviado a Contratar"
                                    DataFormatString="{0:dd/M/yyyy}" />
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

                <asp:Panel ID="Panel_informacionUbicacionTrabajador" runat="server">
                    <div class="div_espaciador"></div>
                    <table class="table_control_registros" style="text-align:center; font-weight:bold;">
                        <tr>
                            <td>
                                <asp:Label ID="Label_infoUbicacionTrabajador" runat="server" Text="informacion de la ubicacion del tabajador"></asp:Label>
                            </td>
                        </tr>
                    
                    </table>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_CONFIGURACION_EXAMENES" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
                <div class="div_cabeza_groupbox">
                    Seleccione la ubicación donde trabajará la persona
                </div>
            <div class="div_contenido_groupbox">

                <asp:HiddenField ID="HiddenField_ID_CIUDAD_SELECCIONADA" runat="server" />
                <asp:HiddenField ID="HiddenField_ID_CENTRO_C_SELECCIONADO" runat="server" />
                <asp:HiddenField ID="HiddenField_ID_SUB_C_SELECCIONADO" runat="server" />

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:HiddenField ID="HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION" runat="server" />

                        <table class="table_control_registros">
                            <tr>
                                <td>
                                    <asp:Label ID="Label_convencion_verde" runat="server" Text=" " Height="10px" Width="10px"
                                        BackColor="#009900"></asp:Label>
                                </td>
                                <td>
                                    Las Condiciones de contratación ESTAN configuradas
                                </td>
                                <td>
                                    <asp:Label ID="Label_convencion_rojo" runat="server" Text=" " Height="10px" Width="10px"
                                        BackColor="#009900" style="background-color: #FF0000"></asp:Label>
                                </td>
                                <td>
                                    Las Condiciones de contratación NO ESTAN configuradas
                                </td>
                            </tr>
                        </table>

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
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                
                <div class="div_espaciador">
                </div>
                
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <asp:Button ID="Button_ACEPTAR_UBICACION" runat="server" 
                                CssClass="margin_botones" Text="Seleccionar" ValidationGroup="UBICACION" 
                                onclick="Button_ACEPTAR_UBICACION_Click" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_GRILLA_CONFIGURACION_EXAMENES" runat="server">
        <div class="div_espaciador">
        </div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Configuración de Examenes Médicos
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros" width="800">
                    <tr>
                        <td>
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_Examenes_Configurados" runat="server" Width="798px" AutoGenerateColumns="False"
                                        DataKeyNames="ID_PRODUCTO">
                                        <Columns>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Examen" >
                                            <ItemStyle CssClass="columna_grid_jus" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Recomendaciones">
                                                <ItemStyle CssClass="columna_grid_jus" Width="150px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Laboratorio">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DropDownList_Proveedor" runat="server" ValidationGroup="ADICIONAR"
                                                        Width="400px">
                                                    </asp:DropDownList>                                                    
                                                    <!-- DropDownList_Proveedor -->
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_Proveedor"
                                                        ControlToValidate="DropDownList_Proveedor" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El Laboratorio es requerido."
                                                        ValidationGroup="ADICIONAR" />
                                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_Proveedor"
                                                        TargetControlID="RequiredFieldValidator_DropDownList_Proveedor" HighlightCssClass="validatorCalloutHighlight" />

                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
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
            
    <asp:Panel ID="Panel_RESULTADOS_EXAMENES_MEDICOS" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Resultados Examenes Medicos
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td>
                            <div class="div_contenedor_grilla_resultados">
                                <div class="grid_seleccionar_registros">
                                    <asp:GridView ID="GridView_EXAMENES_REALIZADOS" runat="server" Width="875px" AutoGenerateColumns="False"
                                        DataKeyNames="REGISTRO,ID_PRODUCTO,REGISTRO_PROVEEDOR,REGISTRO_PRODUCTOS_PROVEEDOR,ID_ORDEN">
                                        <Columns>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Examen" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOM_PROVEEDOR_SECTOR" HeaderText="Laboratorio" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Verificado">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox_EXAMENES_ENTREGADOS" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" Width="50px"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Autos Con Recomendacion">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox_Autos_Recomendacion" runat="server" Width="230px" Height="65px" ValidationGroup="BUSCAR_CLIENTE"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resultados Examen">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="FileUpload_ARCHIVO" runat="server" />
                                                    <br />
                                                    <br />
                                                    <asp:HyperLink ID="HyperLink_ARCHIVO_EXAMEN" runat="server" Style="margin: 0 auto; text-align: center;">Ver Resultados</asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="columna_grid_centrada" />
                                            </asp:TemplateField>
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

    <asp:Panel ID="Panel_CONFIGURACION_FORMA_PAGO" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Configuración Forma de Pago de Nómina</div>
            <div class="div_contenido_groupbox">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="table_control_registros">
                            <tr>
                                <td class="td_izq">
                                    <asp:Label ID="Label_forma_Pago" runat="server" Text="Forma de Pago de Nómina"></asp:Label>
                                </td>
                                <td class="td_der">
                                    <asp:DropDownList ID="DropDownList_forma_pago" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownList_forma_pago_SelectedIndexChanged" Width="260px"
                                        ValidationGroup="ADICIONAR">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <!-- DropDownList_forma_pago -->
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_forma_pago"
                            ControlToValidate="DropDownList_forma_pago" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La FORMA DE PAGO es requerida."
                            ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_forma_pago"
                            TargetControlID="RequiredFieldValidator_DropDownList_forma_pago" HighlightCssClass="validatorCalloutHighlight" />

                        <asp:Panel ID="Panel_FORMA_CONSIGNACION_BANCARIA" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:CheckBox ID="CheckBox_TIENE_CUENTA" runat="server" 
                                            Text="Trabajador Ya Tiene Cuenta" 
                                            OnCheckedChanged="CheckBox_TIENE_CUENTA_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_entidad_bancaria" runat="server" Text="Entidad Bancaria"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_entidad_bancaria" runat="server" Width="260px" ValidationGroup="ADICIONAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_Tipo_Cuenta" runat="server" Text="Tipo de Cuenta"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_TIPO_CUENTAS" runat="server" Width="260px" ValidationGroup="ADICIONAR">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <!-- DropDownList_entidad_bancaria -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_entidad_bancaria"
                                ControlToValidate="DropDownList_entidad_bancaria" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />La ENTIDAD BANCARIA es requerida."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_entidad_bancaria"
                                TargetControlID="RequiredFieldValidator_DropDownList_entidad_bancaria" HighlightCssClass="validatorCalloutHighlight" />
                            <!-- DropDownList_TIPO_CUENTAS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_TIPO_CUENTAS"
                                ControlToValidate="DropDownList_TIPO_CUENTAS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE CUENTA es requerido."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_TIPO_CUENTAS"
                                TargetControlID="RequiredFieldValidator_DropDownList_TIPO_CUENTAS" HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>

                        <asp:Panel ID="Panel_CUENTA_BANCARIA" runat="server">
                            <div class="div_espaciador">
                            </div>
                            <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        <asp:Label ID="Label_Numero_CuentaS" runat="server" Text="Número de Cuenta"></asp:Label>
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_Numero_CuentaS" runat="server" Width="260px" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_Numero_CuentaS" runat="server"
                                            TargetControlID="TextBox_Numero_CuentaS" FilterType="Numbers" />
                                    </td>
                                </tr>
                            </table>
                            <!-- TextBox_Numero_CuentaS -->
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TextBox_Numero_CuentaS"
                                ControlToValidate="TextBox_Numero_CuentaS" Display="None" ErrorMessage="<b>Campo Requerido faltante</b><br />El NUMERO DE CUENTA es requerido."
                                ValidationGroup="ADICIONAR" />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TextBox_Numero_CuentaS"
                                TargetControlID="RequiredFieldValidator_TextBox_Numero_CuentaS" HighlightCssClass="validatorCalloutHighlight" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel_GUARDAR_PROCESO" runat="server">
        <div class="div_espaciador">
        </div>
        <table class="table_control_registros">
            <tr>
                <td>
                    <asp:Button ID="Button_Guardar_PROCESO" runat="server" Text="Guardar Proceso" 
                        CssClass="margin_botones" ValidationGroup="ADICIONAR" 
                        onclick="Button_Guardar_PROCESO_Click" />
                </td>
                <td>
                    <asp:Button ID="Button_DescartarPorExamenes" runat="server" Text="Informar Descarte" 
                        CssClass="margin_botones" ValidationGroup="DESCARTE" 
                        onclick="Button_DescartarPorExamenes_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_BOTONES_MENU" runat="server">
        <div class="div_espaciador"></div>
        <table class="tabla_contiene_botones" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <div class="div_cabeza_groupbox">
                        Botones de contratos
                    </div>
                    <div class="div_contenido_groupbox">
                        <table cellpadding="0" cellspacing="0" border="0" style="margin: 0 auto;">
                            <tr>
                                <td rowspan="0">
                                    <asp:Button ID="Button_Imprimir_Ordenes" runat="server" Text="Imprimir Orden Exámenes"
                                        CssClass="margin_botones" OnClick="Button_Imprimir_Click" ValidationGroup="CREAR" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_Imprimir_Carta" runat="server" Text="Imprimir Carta Apertura"
                                        CssClass="margin_botones" OnClick="Button_Imprimir_Carta_Click" ValidationGroup="CREAR" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_Imprimir_Autos" runat="server" Text="Imprimir Autos de Recomendación"
                                        CssClass="margin_botones" OnClick="Button_Imprimir_Autos_Click" ValidationGroup="CREAR" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Panel_informarDescarte" runat="server">
        <div class="div_espaciador"></div>
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Informar Descarte            
            </div>
            <div class="div_contenido_groupbox">
                <table class="table_control_registros">
                    <tr>
                        <td style="text-align:center;">
                            Al informar descarte, al trabajador seleciconado no se le podrá continuar la contratación,
                            y se envía un E-Mail al Jefe de Contratación para que termine el proceso de Descarte.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; font-weight:bold;">
                            <br />
                            Observaciones<br />
                            <asp:TextBox ID="TextBox_ObservacionesDescarte" runat="server" 
                                TextMode="MultiLine" ValidationGroup="INFORMARDESCARTE" Height="69px" Width="824px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                              
                            <asp:Button ID="Button_InformarDescarte" runat="server" Text="Informar" 
                                ValidationGroup="INFORMARDESCARTE" onclick="Button_InformarDescarte_Click"/>
                              
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
