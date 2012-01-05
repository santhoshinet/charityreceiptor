<%@  Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div class="Container">
        <ul class="ul">
            <li>
                <h2>
                    Change Password</h2>
            </li>
            <li>
                <label class="label">
                    Your password has been changed successfully.</label>
            </li>
            <li><a href="/controlpanel">go home</a> </li>
        </ul>
    </div>
</asp:Content>