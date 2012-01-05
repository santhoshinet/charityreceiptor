<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Control panel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <h2>
            Control panel</h2>
        <p>
            Manage your resources.</p>
        <div class="container">
            <div class="left">
                <a href="/Controlpanel/users">Manage Users</a></div>
            <div class="right">
                <a href="/Controlpanel/products"></a>
            </div>
        </div>
    </div>
</asp:Content>