<%@ Page Language="C#" AutoEventWireup="true" CodeFile="areas.aspx.cs" Inherits="areas_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SISER -SELECCIÓN DE PROCESOS-</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="div_barra_superior">
            <img id="img_barra_superior" alt="" src="../imagenes/plantilla/plantilla_barra_superior.png"/>
        </div>



    <div class="div_contenedor_pagina">
        <table border="0" cellpadding="0" cellspacing="0" width="957">
            <tr>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="240" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="498" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="219" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="1" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td rowspan="5">
                </td>
                <td>
                </td>
                <td rowspan="5">
                    <!-- EN ESTA IMG SE CARGA LA IMG DEL AREA -->
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="77" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td id="td_fondo_cabeza_login_azul">
                    <table class="table_control_registros" style="width: 498px;">
                        <tr>
                            <td class="letra_usuario_conectado">
                            </td>
                            <td class="letra_link_cerrar_pass">
                                <asp:LinkButton ID="LinkButton_CERRAR_SESION" runat="server" OnClick="LinkButton_CERRAR_SESION_Click">CERRAR</asp:LinkButton>
                            </td>
                            <td class="letra_link_cerrar_pass">
                                <asp:HyperLink ID="HyperLink_CAMBIAR_PASSWORD" runat="server" NavigateUrl="~/seguridad/cambioPassword.aspx"
                                    Target="_blank">CAMBIAR PASSWORD</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="32" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img name="plantlla_cabeza_azul_r3_c2" src="../imagenes/plantilla/plantlla_cabeza_azul_r3_c2.png"
                        width="498" height="5" border="0" id="plantlla_cabeza_azul_r3_c2" alt="" />
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="5" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td id="td_fondo_cabeza_azul_info_modulo">
                    <!-- EN ESTA SECCIÓN SE CARGA EL NOMBRE DEL MODULO -->
                    <asp:Label ID="Label_NOMBRE_MODULO" runat="server"></asp:Label>
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="32" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img name="plantlla_cabeza_azul_r5_c2" src="../imagenes/plantilla/plantlla_cabeza_azul_r5_c2.png"
                        width="498" height="6" border="0" id="plantlla_cabeza_azul_r5_c2" alt="" />
                </td>
                <td>
                    <img src="../imagenes/plantilla/spacer.gif" width="1" height="6" border="0" alt="" />
                </td>
            </tr>
        </table>

        <!-- EN ESTA SECCION SE CARGA EL NOMBRE DEL AREA -->
        <!-- <asp:Label ID="Label_NOMBRE_AREA" runat="server"></asp:Label> -->
        </div>
        <div class="div_contenedor_pagina">
            <div class="div_superior_contenido"></div>
            <div class="div_medio_contenido">
                <div class="div_contenedor_contenido">
                    <table class="table_control_registros">
                        <tr>
                            <td style="text-align:center;">
                                <asp:Table ID="Table_MENU" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table class="table_control_registros">
                        <tr>
                            <td style="text-align:center;">
                                <asp:Table ID="Table_MENU_1" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table class="table_control_registros">
                        <tr>
                            <td style="text-align:center;">
                                <asp:Table ID="Table_MENU_2" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table class="table_control_registros">
                        <tr>
                            <td style="text-align:center;">
                                <asp:Table ID="Table_MENU_3" runat="server">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </div>    
            </div>
            <div class="div_inferior_contenido"></div>
        </div>
        
        <div id="div_barra_inferior">
            <img id="img_barra_inferior" alt="" src="../imagenes/plantilla/plantilla_barra_superior.png"/>
        </div>




    </form>
</body>
</html>
