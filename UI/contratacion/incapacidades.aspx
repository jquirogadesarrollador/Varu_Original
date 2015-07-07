<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="incapacidades.aspx.cs" Inherits="_Default" %>

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
                                            <asp:DropDownList ID="DropDownList_BUSCAR" runat="server" Width="160px" 
                                                ValidationGroup="BUSCAR_REFERENCIA" 
                                                onselectedindexchanged="DropDownList_BUSCAR_SelectedIndexChanged1" AutoPostBack="true">
                                            </asp:DropDownList>

<%--                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="160px" ValidationGroup="BUSCAR_REFERENCIA"
                                                onselectedindexchanged="DropDownList_BUSCAR_SelectedIndexChanged" 
                                                AutoPostBack="True"></asp:DropDownList>
--%>                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox_BUSCAR" runat="server" Width="220px" 
                                                ValidationGroup="BUSCAR_REFERENCIA">
                                                </asp:TextBox>
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
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button_Buscar"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <%--<asp:Panel ID="Panel_MENSAJES" runat="server">
        <asp:UpdatePanel runat="server" ID="UpdatePanel_MENSAJES">
            <ContentTemplate>
                <div class="div_contenedor_formulario">
                    <div class="div_espacio_validacion_campos">
                        <asp:Label id="Label_MENSAJE" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>--%>
    
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

    <div class="div_espaciador">
    </div>

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
                                        <asp:BoundField DataField="FECHA_INICIA" HeaderText="Fecha Inicia" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_TERMINA" HeaderText="Fecha Termina" >
                                            <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ARCHIVO" HeaderText="Estado" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INCAPACIDAD" HeaderText="Incapacidad?" >
                                            <ItemStyle CssClass="columna_grid_izq" />
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
                                        <asp:BoundField DataField="Regional" HeaderText="Regional" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" >
                                            <ItemStyle CssClass="columna_grid_izq" />
                                        </asp:BoundField>
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

                            <tr>
                                <td class="td_izq">
                                    Fecha de ingreso
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_fecha_ingreso" runat="server" Width="200" ReadOnly="True" ></asp:TextBox>
                                </td>

                                <td class="td_izq">
                                    Fecha de retiro
                                </td>
                                <td class="td_der">
                                    <asp:TextBox ID="TextBox_fecha_retiro" runat="server" Width="200" ReadOnly="True" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="td_izq">
                                    EPS
                                </td>
                                <td colspan="3" class="td_der">
                                    <asp:TextBox ID="TextBox_EPS" runat="server" Width="520" ReadOnly="True" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="td_izq">
                                    ARL
                                </td>
                                <td colspan="3" class="td_der">
                                    <asp:TextBox ID="TextBox_ARL" runat="server" Width="520" ReadOnly="True" ></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <div class="div_espaciador"></div>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    
    <asp:Panel ID="Panel_RESULTADOS_GRID_INCAPACIDADES" runat="server">
        <div class=" div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                <asp:Label ID="Label1" runat="server" Text="Resultados búsqueda de incapacidades por contrato seleccionado"></asp:Label>
            </div>
            <div class="div_contenido_groupbox">
                <div class="div_contenedor_grilla_resultados">
                    <asp:UpdatePanel ID="UpdatePanel_RESULTADOS_BUSQUEDA_INCAPACIDADES" runat="server">
                        <ContentTemplate>
                            <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES" runat="server" Width="885px" 
                                    onselectedindexchanged="GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES_SelectedIndexChanged" 
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                            ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" 
                                            DataFormatString="{0:dd/MM/yyyy}" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_TIPO_INCA" HeaderText="Tipo incapacidad" />
                                        <asp:BoundField DataField="NOMBRE_CLASE_INCA" HeaderText="Clase incapacidad" />
                                        <asp:BoundField DataField="DIAS_INCAP" HeaderText="Días incapacidad" >
                                        <ItemStyle CssClass="columna_grid_der" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DSC_DIAG" HeaderText="Diagnósitico" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES"/>
                            </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="div_espaciador"></div>
            <div class="div_contenido_groupbox">
                <div class="div_para_convencion_hoja_trabajo">
                <table class="tabla_alineada_derecha">
                    <tr>
                        <td>
                            <asp:Label ID="Label_ALERTA_MEDIA" runat="server" Text="Incapacidades suman menos de 180 días"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_amarillo"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_Contrato_Vencido" runat="server" Text="Incapacidades superarón los 180 días"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_azul"></div> 
                        </td>
                        <td>
                            <asp:Label ID="Label_ALERTA_ALTA" runat="server" Text="Incapacidades superarón los 560 días"></asp:Label> 
                            &nbsp;
                        </td>
                        <td class="td_contenedor_colores_hoja_trabajo">
                            <div class="div_color_rojo"></div> 
                        </td>

                    </tr>
                </table>
            </div>
                <table class="table_control_registros">
                    <tr>
                        <td>
                            Total días de incapacidad
                        </td>
                        <td class="td_der">
                            <asp:TextBox ID="TextBox_total_dias_incapacidad" runat="server" Width="200" ReadOnly="True" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>

    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de Incapacidad
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
                                Información Incapacidad</div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_ID" runat="server" Text="ID"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_ID" runat="server" Width="200px" 
                                                    MaxLength="255" ValidationGroup="ADICIONAR" ReadOnly ="true"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Estado
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_estado" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_FECHA" runat="server" 
                                                Text="Fecha elaboración incapacidad por entidad"></asp:Label>
                                            &nbsp;</td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_FECHA" runat="server" Width="200px" 
                                            MaxLength="10" 
                                            ValidationGroup="ADICIONAR"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHA" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_FECHA" />
                                        </td>
                                        <td class="td_izq">
                                            Tramitada por
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_tramitada_por" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_VAL_RECONOCIDO" runat="server" Text="Valor reconocido"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_VAL_RECONOCIDO" runat="server" Width="200px" 
                                                    MaxLength="10" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_NUM_AUTORIZA" runat="server" Text="No. autorización"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_NUM_AUTORIZA" runat="server" Width="200px" 
                                                    MaxLength="10"></asp:TextBox>
                                        </td>    
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Fecha pago nómina desde
                                        </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_fecha_paga_nomina_desde" runat="server" Width="200px" 
                                                MaxLength="10" ValidationGroup="ADICIONAR">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha_paga_nomina_desde" runat="server" 
                                            Format="dd/MM/yyyy" TargetControlID="TextBox_fecha_paga_nomina_desde" />
                                    </td>
                                    <td class="td_izq">
                                        Hasta
                                    </td>
                                    <td class="td_der">
                                        <asp:TextBox ID="TextBox_fecha_paga_nomina_hasta" runat="server" Width="200px" 
                                        MaxLength="10" ValidationGroup="ADICIONAR">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_fecha_paga_nomina_hasta" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="TextBox_fecha_paga_nomina_hasta" />
                                    </td>    
                                </tr>

                                    <asp:UpdatePanel ID="UpdatePanel_dias_incapacidad" runat="server">
                                        <ContentTemplate>
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_FCH_INI_REAL" runat="server" Text="Fechas reales Desde"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_FCH_INI_REAL" runat="server" Width="200px" 
                                                            MaxLength="10" ValidationGroup="ADICIONAR" 
                                                            ontextchanged="TextBox_FCH_INI_REAL_TextChanged" 
                                                            AutoPostBack="true">
                                                    </asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_FCH_INI_REAL" runat="server" 
                                                        Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_INI_REAL" />
                                                </td>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_FCH_TER_REAL" runat="server" Text="Hasta"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:TextBox ID="TextBox_FCH_TER_REAL" runat="server" Width="200px" 
                                                    MaxLength="10" ValidationGroup="ADICIONAR" 
                                                    ontextchanged="TextBox_FCH_TER_REAL_TextChanged" 
                                                    AutoPostBack="true">
                                                    </asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_FCH_TER_REAL" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="TextBox_FCH_TER_REAL" />
                                                </td>    
                                            </tr>
                                            <!-- TextBox_FCH_INI_REAL -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FCH_INI_REAL" 
                                                runat="server" ControlToValidate="TextBox_FCH_INI_REAL" Display="None" 
                                                ErrorMessage="Campo Requerido faltante</br>La FECHA INICIAL REAL es requerida." 
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FCH_INI_REAL" 
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                                TargetControlID="RequiredFieldValidator_FCH_INI_REAL" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FCH_INI_REAL" 
                                                runat="server" TargetControlID="TextBox_FCH_INI_REAL" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>
                                
                                            <!-- TextBox_FCH_TER_REAL -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FCH_TER_REAL" 
                                                runat="server" ControlToValidate="TextBox_FCH_TER_REAL" Display="None" 
                                                ErrorMessage="Campo Requerido faltante</br>La FECHA FINAL REAL es requerida." 
                                                ValidationGroup="ADICIONAR" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FCH_TER_REAL" 
                                                runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                                TargetControlID="RequiredFieldValidator_FCH_TER_REAL" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FCH_TER_REAL" 
                                                runat="server" TargetControlID="TextBox_FCH_TER_REAL" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_DIAS_INCAP" runat="server" Text="Días incapacidad"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_DIAS_INCAP" runat="server" ValidationGroup="GUARDAR" Width="200px" MaxLength="20" ></asp:TextBox>
                                        </td>

                                        <td class="td_izq">
                                            <asp:Label ID="Label_INC_CARENCIA" runat="server" Text="Período de carencia"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_INC_CARENCIA" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_TIPO_INCA" runat="server" Text="Tipo de incapacidad"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_TIPO_INCA" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>
                                                    
                                        <td class="td_izq">
                                            <asp:Label ID="Label_SEVERO" runat="server" Text="Caso servero"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_SEVERO" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                                
                                            <tr>
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_CLASE_INCA" runat="server" Text="Clase de incapacidad"></asp:Label>
                                                </td>
                                                    <td class="td_der">
                                                        <asp:DropDownList 
                                                            ID="DropDownList_CLASE_INCA" 
                                                            runat="server" 
                                                            Width="200px" 
                                                            onselectedindexchanged="DropDownList_CLASE_INCA_SelectedIndexChanged" AutoPostBack="true"> 
                                                        </asp:DropDownList>
                                                    </td>
                                        
                                                <td class="td_izq">
                                                    <asp:Label ID="Label_ID_CONCEPTO" runat="server" Text="Concepto"></asp:Label>
                                                </td>
                                                <td class="td_der">
                                                    <asp:DropDownList ID="DropDownList_ID_CONCEPTO" runat="server" Width="200px"> 
                                                    </asp:DropDownList>
                                                </td>
    
                                            </tr>

                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_PRORROGA" runat="server" Text="Prórroga"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:DropDownList ID="DropDownList_PRORROGA" runat="server" Width="200px"> 
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_VALOR_LIQUIDADO_NOMINA" runat="server" Text="Valor liquidado nómina"></asp:Label>
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_VALOR_LIQUIDADO_NOMINA" runat="server" Width="200px" 
                                                    MaxLength="10" ValidationGroup="ADICIONAR"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            <asp:Label ID="Label_OBS_REG" runat="server" Text="Observaciones"></asp:Label>
                                        </td>
                                        <td colspan="3" class="td_der">
                                            <asp:TextBox ID="TextBox_OBS_REG" runat="server" Width="550px" 
                                                    MaxLength="255" ValidationGroup="ADICIONAR" Height="100px" 
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>    
                                    </tr>
                                </table>
                                
                                <!-- DropDownList_estado-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_estado"
                                    ControlToValidate="DropDownList_estado"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El ESTADO es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_estado"
                                    TargetControlID="RequiredFieldValidator_DropDownList_estado"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- DropDownList_tramitada_por-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_tramitada_por"
                                    ControlToValidate="DropDownList_tramitada_por"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />TRAMITADA POR es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_tramitada_por"
                                    TargetControlID="RequiredFieldValidator_DropDownList_tramitada_por"
                                    HighlightCssClass="validatorCalloutHighlight" />


                                <!-- TextBox_DIAS_INCAP -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DIAS_INCAP"
                                    ControlToValidate="TextBox_DIAS_INCAP"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />Los DIAS DE INCAPACIDAD es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DIAS_INCAP"
                                    TargetControlID="RequiredFieldValidator_DIAS_INCAP"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DIAS_INCAP" 
                                    runat="server" TargetControlID="TextBox_DIAS_INCAP" FilterType="Custom,Numbers"></ajaxToolkit:FilteredTextBoxExtender>

                                <!-- TextBox_VAL_RECONOCIDO -->
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_VAL_RECONOCIDO" 
                                    runat="server" TargetControlID="TextBox_VAL_RECONOCIDO" FilterType="Custom,Numbers" ValidChars=",."></ajaxToolkit:FilteredTextBoxExtender>

                                <!-- DropDownList_INC_CARENCIA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_INC_CARENCIA"
                                    ControlToValidate="DropDownList_INC_CARENCIA"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La CARENCIA es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_INC_CARENCIA"
                                    TargetControlID="RequiredFieldValidator_INC_CARENCIA"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- DropDownList_TIPO_INCA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TIPO_INCA"
                                    ControlToValidate="DropDownList_TIPO_INCA"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El TIPO DE INCAPACIDAD es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_TIPO_INCA"
                                    TargetControlID="RequiredFieldValidator_TIPO_INCA"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- DropDownList_SEVERO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_SEVERO"
                                    ControlToValidate="DropDownList_SEVERO"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El CASOS SERVERO es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_SEVERO"
                                    TargetControlID="RequiredFieldValidator_SEVERO"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- DropDownList_CLASE_INCA -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_CLASE_INCA"
                                    ControlToValidate="DropDownList_CLASE_INCA"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La CLASE DE INCAPACIDAD es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_CLASE_INCA"
                                    TargetControlID="RequiredFieldValidator_CLASE_INCA"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- DropDownList_ID_CONCEPTO -->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ID_CONCEPTO"
                                    ControlToValidate="DropDownList_ID_CONCEPTO"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El CONCEPTO es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_ID_CONCEPTO"
                                    TargetControlID="RequiredFieldValidator_ID_CONCEPTO"
                                    HighlightCssClass="validatorCalloutHighlight"/>

                                <!-- DropDownList_PRORROGA-->
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_PRORROGA"
                                    ControlToValidate="DropDownList_PRORROGA"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />La PRORROGGA es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_PRORROGA"
                                    TargetControlID="RequiredFieldValidator_PRORROGA"
                                    HighlightCssClass="validatorCalloutHighlight" />

                                <!-- TextBox_FECHA -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FECHA" 
                                    runat="server" ControlToValidate="TextBox_FECHA" Display="None" 
                                    ErrorMessage="Campo Requerido faltante</br>La FECHA generación EPS es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FECHA" 
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                    TargetControlID="RequiredFieldValidator_FECHA" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FECHA" 
                                    runat="server" TargetControlID="TextBox_FECHA" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>
                                
                                <!-- TextBox_fecha_paga_nomina_desde -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_fecha_paga_nomina_desde" 
                                    runat="server" ControlToValidate="TextBox_fecha_paga_nomina_desde" Display="None" 
                                    ErrorMessage="Campo Requerido faltante</br>La FECHA PAGO NOMINA DESDE es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_paga_nomina_desde" 
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                    TargetControlID="RequiredFieldValidator_TextBox_fecha_paga_nomina_desde" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_fecha_paga_nomina_desde" 
                                    runat="server" TargetControlID="TextBox_fecha_paga_nomina_desde" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>
                                
                                <!-- TextBox_fecha_paga_nomina_hasta -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_fecha_paga_nomina_hasta" 
                                    runat="server" ControlToValidate="TextBox_fecha_paga_nomina_hasta" Display="None" 
                                    ErrorMessage="Campo Requerido faltante</br>La FECHA PAGO NOMINA HASTA es requerida." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_fecha_paga_nomina_hasta" 
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" 
                                    TargetControlID="RequiredFieldValidator_TextBox_fecha_paga_nomina_hasta" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TextBox_fecha_paga_nomina_hasta" 
                                    runat="server" TargetControlID="TextBox_fecha_paga_nomina_hasta" FilterType="Custom,Numbers" ValidChars="/"></ajaxToolkit:FilteredTextBoxExtender>

                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>
                
                <asp:Panel ID="Panel_NUEVO_DIAGNOSTICO" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Adicionar Diagnostico
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <div class="div_avisos_importantes" style="width:70%; margin:0 auto;">
                            Debe realizar la busqueda del diagnostico. escriba el nombre del diagnostico, oprima el 
                            botón BUSCAR, y luego utilice la lista de DIAGNOSTICOS ENCONTRADOS para seleccionar el 
                            diagnostico.
                        </div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel_BUSCADOR_DIAGNOSTICOS">
                            <ProgressTemplate>
                                <div style="text-align:center; margin:5px; font-weight:bold;">
                                    Buscando Diagnosticos... Por Favor Espere.
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel_BUSCADOR_DIAGNOSTICOS" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Buscador
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_BUSCADOR_DIAGNOSTICO" runat="server" ValidationGroup="BUSCADORDIAGNOSTICO"
                                                Width="220px" MaxLength="15"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button_BUSCADOR_DIAGNOSTICO" runat="server" Text="Buscar" CssClass="margin_botones"
                                                OnClick="Button_BUSCADOR_DIAGNOSTICO_Click" ValidationGroup="BUSCADORDIAGNOSTICO" />
                                        </td>
                                    </tr>
                                </table>

                                <!-- TextBox_BUSCADOR_DIAGNOSTICO -->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_TextBox_BUSCADOR_DIAGNOSTICO" runat="server"
                                    ControlToValidate="TextBox_BUSCADOR_DIAGNOSTICO" Display="None" ErrorMessage="Campo Requerido faltante</br>El DIAGNOSTICO es requerido."
                                    ValidationGroup="BUSCADORDIAGNOSTICO" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_TextBox_BUSCADOR_DIAGNOSTICO"
                                    runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_TextBox_BUSCADOR_DIAGNOSTICO" />

                                <div class="div_espaciador"></div>

                                <table class="table_control_registros">
                                    <tr>
                                        <td>
                                            Seleccione un Diagnostico Encontrado
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList_DSC_DIAG" runat="server" Width="570px" ValidationGroup="DIAGNOSTICOS"> 
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <%--DropDownList_DSC_DIAG--%>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_DropDownList_DSC_DIAG"
                                    ControlToValidate="DropDownList_DSC_DIAG"
                                    Display="None"
                                    ErrorMessage="<b>Campo Requerido faltante</b><br />El DIAGNOSTICO es requerido." 
                                    ValidationGroup="ADICIONAR" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender_DropDownList_DSC_DIAG"
                                    TargetControlID="RequiredFieldValidator_DropDownList_DSC_DIAG"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </asp:Panel>

                <asp:Panel ID="Panel_documento_incapacidad" runat="server">
                    <div class="div_espaciador"></div>
                    <div class="div_cabeza_groupbox_gris">
                        Cargar Incapacidad
                    </div>
                    <div class="div_contenido_groupbox_gris">
                        <table class="table_control_registros">
                                <tr>
                                    <td class="td_izq">
                                        Cargar archivo de la incapacidad:
                                    </td>
                                    <td class="td_der">
                                        <asp:FileUpload ID="FileUpload_archivo" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_izq">
                                        Archivo:
                                    </td>
                                    <td class="td_der">
                                        <asp:HyperLink ID="HyperLink_ARCHIVO" runat="server">Clic aquí para decargar.</asp:HyperLink>
                                    </td>
                                </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_FileUpload_archivo" runat="server"
                        ControlToValidate="FileUpload_archivo" Display="None" ErrorMessage="&lt;b&gt;Campo Requerido faltante&lt;/b&gt;&lt;br /&gt;El ARCHIVO es requerido."
                        ValidationGroup="ADICIONAR" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender_FileUpload_archivo"
                        runat="Server" HighlightCssClass="validatorCalloutHighlight" TargetControlID="RequiredFieldValidator_FileUpload_archivo" />

                        <div class="div_espaciador"></div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel_nomina" runat="server">
                <div class="div_cabeza_groupbox">
                    Relación de pagos de nómina
                </div>
                    <div class="div_espaciador"></div>
                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table class="table_align_izq" cellpadding="0" cellspacing="0">
                                    
                                    <tr>
                                        <td>Dias pagados</td>
                                        <td>
                                            <asp:TextBox ID="TextBox_dias_pagados" runat="server"></asp:TextBox>
                                        </td>
                                        <td>Dias pendientes</td>
                                        <td>
                                            <asp:TextBox ID="TextBox_dias_pendientes" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="grid_seleccionar_registros">
                                <asp:GridView ID="GridView_pagos_nomina" runat="server" Width="885px" 
                                    AutoGenerateColumns="False" DataKeyNames="REGISTRO">
                                    <Columns>
                                        <asp:BoundField DataField="FECHA_INI" HeaderText="Fecha inicial nomina" 
                                            DataFormatString="{0:dd/MM/yyyy}" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha final nomina" 
                                            DataFormatString="{0:dd/MM/yyyy}" >
                                        <ItemStyle CssClass="columna_grid_centrada" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PER_CONT" HeaderText="Periodo" />
                                        <asp:BoundField DataField="SUELDO" HeaderText="Sueldo" />
                                        <asp:BoundField DataField="COD_CONCEPTO" HeaderText="Codigo" />
                                        <asp:BoundField DataField="DESC_ABREV" HeaderText="Concepto" />
                                        <asp:BoundField DataField="DIAS_INCAPACIDAD" HeaderText="Dias" />
                                        <asp:BoundField DataField="VALOR" HeaderText="Valor" />
                                    </Columns>
                                    <headerStyle BackColor="#DDDDDD" />
                                </asp:GridView>
                            </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="Panel_tramite" runat="server">
                <div class="div_cabeza_groupbox">
                    Trámites de incapacidades
                </div>
                    <div class="div_espaciador"></div>
                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table  class="table_control_registros">
                                <tr>
                                    <td class="td_der">Estado del trámite</td>
                                    <td class="td_der">
                                        <asp:DropDownList ID="DropDownList_estado_tramite" runat="server" Width="200px"> 
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="div_cabeza_groupbox_gris">
                                Transcripción
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_transcripcion_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_transcripcion_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_transcripcion_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_transcripcion_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_transcripcion_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_transcripcion_fecha_seguimiento" />

                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_transcripcion_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_transcripcion_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_transcripcion_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_transcripcion_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Liquidación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_liquidacion_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_liquidacion_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_liquidacion_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_liquidacion_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_liquidacion_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_liquidacion_fecha_seguimiento" />
                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_liquidacion_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_liquidacion_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_liquidacion_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_liquidacion_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Reliquidación
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_reliquidacion_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_reliquidacion_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_reliquidacion_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_reliquidacion_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_reliquidacion_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_reliquidacion_fecha_seguimiento" />
                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_reliquidacion_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_reliquidacion_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_reliquidacion_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_reliquidacion_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Cobro
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_cobro_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_cobro_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_cobro_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_cobro_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_cobro_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_cobro_fecha_seguimiento" />

                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_cobro_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_cobro_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_cobro_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_cobro_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Pago
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_pago_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_pago_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_pago_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_pago_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_pago_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_pago_fecha_seguimiento" />

                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_pago_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_pago_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_pago_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_pago_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Objetada
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_objetada_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_objetada_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_objetada_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_objetada_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_objetada_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_objetada_fecha_seguimiento" />
                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_objetada_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_objetada_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_objetada_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_objetada_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="div_espaciador"></div>
                            <div class="div_cabeza_groupbox_gris">
                                Negada
                            </div>
                            <div class="div_contenido_groupbox_gris">
                                <table class="table_control_registros">
                                    <tr>
                                        <td class="td_izq">
                                            Fecha
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_negada_fecha_radicacion" runat="server" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_negada_fecha_radicacion" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_negada_fecha_radicacion" />
                                        </td>
                                        <td class="td_izq">
                                            Fecha de seguimiento
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_negada_fecha_seguimiento" runat="server" Width="100px" 
                                                    MaxLength="10"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_TextBox_negada_fecha_seguimiento" runat="server" 
                                                Format="dd/MM/yyyy" TargetControlID="TextBox_negada_fecha_seguimiento" />
                                        </td>
                                        <td class="td_izq">
                                            VoBo
                                        </td>
                                        <td class="td_der">
                                            <asp:CheckBox ID="CheckBox_negada_VoBo" Text=" " runat="server" />
                                        </td>
                                        <td class="td_izq">
                                            Número
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_negada_numero" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_izq">
                                            Valor
                                        </td>
                                        <td class="td_der">
                                            <asp:TextBox ID="TextBox_negada_valor" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="td_izq">
                                            Notas
                                        </td>
                                        <td class="td_der" colspan="5">
                                            <asp:TextBox ID="TextBox_negada_notas" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="div_espaciador"></div>
                            </div>
                        </td>
                    </tr>
                </table>
                </asp:Panel>



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

