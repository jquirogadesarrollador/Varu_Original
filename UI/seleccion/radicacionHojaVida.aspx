<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="radicacionHojaVida.aspx.cs" Inherits="_RadicacionHojaVida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       

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

    <div class="div_espaciador"></div>
</asp:Content>
