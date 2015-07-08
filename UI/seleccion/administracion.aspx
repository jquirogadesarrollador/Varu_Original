<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true"
    CodeFile="administracion.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        
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
            <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td style="text-align: center;">
                        <asp:Table ID="Table_MENU_1" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div class="div_espaciador">
    </div>
   <%-- <table class="table_control_registros" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="text-align: center;">
                <rsweb:ReportViewer ID="ReportViewer_indicador" runat="server" ScriptMode="Release"
                    ProcessingMode="Remote" Width="900px" Height="750px">
                    <ServerReport ReportPath="/IndicadorRequisiciones" ReportServerUrl="http://sertempo-web/ReportServer" />
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>--%>
</asp:Content>
