<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="Permisos.aspx.cs" Inherits="Operaciones_Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">

    function OnTreeClick(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }

    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }

    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                checkUncheckSwitch = true;
            }
            else //checkbox unchecked
            {
                var isAllSiblingsUnChecked = AreAllSiblingsUnChecked(srcChild);
                if (!isAllSiblingsUnChecked)
                    checkUncheckSwitch = true;
                else
                    checkUncheckSwitch = false;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }

    function AreAllSiblingsUnChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    

    
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
                    <asp:Label ID="Label_MENSAJE" runat="server" ForeColor="Red">Este es el mensaje, tiene un  texto de acuerdo a la acción correspondiente.</asp:Label>
                </asp:Panel>
                <div style="text-align: center; margin-top: 15px;">
                    <asp:Button ID="Button1" runat="server" Text="Cerrar" Style="height: 26px"
                        OnClick="Button_CERRAR_MENSAJE_Click" />
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
                                    <asp:Button ID="Button_NUEVO" runat="server" Text="Nuevo" CssClass="margin_botones"
                                        OnClick="Button_NUEVO_Click" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_GUARDAR" runat="server" Text="Guardar" CssClass="margin_botones"
                                        OnClick="Button_GUARDAR_Click" ValidationGroup="USUARIO" />
                                </td>
                                <td rowspan="0">
                                    <asp:Button ID="Button_MODIFICAR" runat="server" Text="Actualizar" CssClass="margin_botones"
                                        OnClick="Button_MODIFICAR_Click" />
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
            </tr>
        </table>
    </asp:Panel>
        
    <asp:Panel ID="Panel_FORMULARIO" runat="server">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div class="div_contenedor_formulario">
            <div class="div_cabeza_groupbox">
                Información de permisos para la visualización del Manual del Cliente, por proceso
            </div>
            <div class="div_contenido_groupbox">
                <asp:Panel ID="Panel_DATOS" runat="server">
                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <div class="div_cabeza_groupbox_gris">
                                    Configurar permisos para la visualización del Manual del Cliente
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Procesos con permisos 
                                            </div>                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="div_contenido_groupbox">
                                                <div class="div_contenedor_grilla_resultados">
                                                    <div class="grid_seleccionar_registros">
                                                        <asp:GridView ID="GridView_procesos_con_permisos" runat="server" AllowPaging="True" 
                                                                AutoGenerateColumns="False" DataKeyNames="PROCESO" 
                                                            onrowcommand="GridView_procesos_con_permisos_RowCommand">
                                                                <Columns>
                                                                    <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                                                                        ButtonType="Image" SelectImageUrl="~/imagenes/plantilla/view2.gif" >
                                                                        <ItemStyle CssClass="columna_grid_centrada" />
                                                                    </asp:CommandField>
                                                                    <asp:BoundField DataField="PROCESO" HeaderText="Proceso"/>
                                                                </Columns>
                                                                <headerStyle BackColor="#DDDDDD" />
                                                            </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                            <td>
                                <div class="div_contenido_groupbox_gris">
                                    <table class="table_align_izq" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <div class="div_cabeza_groupbox_gris">
                                                Asignar permisos
                                            </div>                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der">
                                            <br />
                                            Seleccione el proceso al que desea asignar permisos para la 
                                            visualización de información en el Manual del Cliente.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der">
                                            <br />
                                            <asp:DropDownList ID="DropDownList_Proceso" runat="server" AutoPostBack="true" 
                                                onselectedindexchanged="DropDownList_Proceso_SelectedIndexChanged">
                                                <asp:ListItem Value="Seleccione">Seleccione</asp:ListItem>
                                                <asp:ListItem Value="Comercial">Comercial</asp:ListItem>
                                                <asp:ListItem Value="Seleccion">Seleccion</asp:ListItem>
                                                <asp:ListItem Value="Contratacion">Contratacion</asp:ListItem>
                                                <asp:ListItem Value="Nomina">Nomina</asp:ListItem>
                                                <asp:ListItem Value="Facturacion">Facturacion</asp:ListItem>
                                                <asp:ListItem Value="Contabilidad">Contabilidad</asp:ListItem>
                                                <asp:ListItem Value="Financiera">Financiera</asp:ListItem>
                                                <asp:ListItem Value="Juridica">Juridica</asp:ListItem>
                                                <asp:ListItem Value="SaludIntegral">Salud Integral</asp:ListItem>
                                                <asp:ListItem Value="Operaciones">Operaciones</asp:ListItem>
                                                <asp:ListItem Value="BienestarSocial">Bienestar Social</asp:ListItem>
                                                <asp:ListItem Value="Rse">RSE</asp:ListItem>
                                                <asp:ListItem Value="ComprasEInventario">Compras e Inventario</asp:ListItem>
                                            </asp:DropDownList> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td_der">
                                            <br />
                                            <asp:TreeView ID="TreeView_seccion" runat="server" ShowLines ="true" ShowCheckBoxes="All">
                                                <Nodes>
                                                    <asp:TreeNode Text="Secciones del Manual del Cliente" 
                                                        Value="Secciones Manual del Cliente">
                                                        <asp:TreeNode Text="Comercial" Value="Comercial">
                                                            <asp:TreeNode Text="Contactos" Value="ComercialContactos">
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Unidad de Negocio" Value="ComercialUnidadNegocio">
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Cobertura" Value="ComercialCobertura">
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Condiciones Económicas" Value="ComercialCondicionesEconomicas">
                                                            </asp:TreeNode>
                                                        </asp:TreeNode>
                                                        <asp:TreeNode Text="Selección" Value="Seleccion"></asp:TreeNode>
                                                        <asp:TreeNode Text="Contratación" Value="Contratacion"></asp:TreeNode>
                                                        <asp:TreeNode Text="Nómina" Value="Nomina"></asp:TreeNode>
                                                        <asp:TreeNode Text="Facturación" Value="Facturacion"></asp:TreeNode>
                                                        <asp:TreeNode Text="Contabilidad" Value="Contabilidad"></asp:TreeNode>
                                                        <asp:TreeNode Text="Financiera" Value="Financiera"></asp:TreeNode>
                                                        <asp:TreeNode Text="Jurídica" Value="Juridica"></asp:TreeNode>
                                                        <asp:TreeNode Text="Salud Integral" Value="SaludIntegral"></asp:TreeNode>
                                                        <asp:TreeNode Text="Operaciones" Value="Operaciones"></asp:TreeNode>
                                                        <asp:TreeNode Text="Bienestar Social" Value="BienestarSocial"></asp:TreeNode>
                                                        <asp:TreeNode Text="RSE" Value="Rse"></asp:TreeNode>
                                                        <asp:TreeNode Text="Compras e Inventario" Value="ComprasEInventario"></asp:TreeNode>
                                                    </asp:TreeNode>
                                                </Nodes>
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div class="div_espaciador"></div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField_accion" runat="server" />
</asp:Content>