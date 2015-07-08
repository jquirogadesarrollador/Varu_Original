<%@ Page Title="" Language="C#" MasterPageFile="~/formularios.master" AutoEventWireup="true" CodeFile="sinPermisos.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 0 auto; text-align: center; display: block;">
        <asp:HyperLink ID="HyperLink_ANTERIOR" runat="server">Volver a la Ventana Anterior</asp:HyperLink><br />
        <br />
        <asp:ImageButton runat="server" 
            ImageUrl="~/imagenes/plantilla/AccesoRestringido.png" ID="Button_ANTERIOR"></asp:ImageButton><br />
        <br />
        <asp:HyperLink ID="HyperLink_ANTERIOR_1" runat="server">Volver a la Ventana Anterior</asp:HyperLink>
    </div>
</asp:Content>

