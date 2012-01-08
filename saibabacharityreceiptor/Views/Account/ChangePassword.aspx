<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    
        <% using (Html.BeginForm())
           { %>
        <ul class="ul">
            <li>
                <h2>
                    Change Password</h2>
                <%= Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.") %>
            </li>
            <li>
                <label class="label">
                    Old password
                </label>
                <%= Html.PasswordFor(m => m.OldPassword, new { @id = "TxtOldpassword", @class = "text txtoldpassword", @maxlength = "20" })%>
                <%= Html.ValidationMessageFor(m => m.OldPassword) %>
            </li>
            <li>
                <label class="label">
                    New password
                </label>
                <%= Html.PasswordFor(m => m.NewPassword, new { @id = "TxtNewpassword", @class = "text txtnewpassword", @maxlength = "20" }) %>
                <%= Html.ValidationMessageFor(m => m.NewPassword) %>
            </li>
            <li>
                <label class="label">
                    Confirm password
                </label>
                <%= Html.PasswordFor(m => m.ConfirmPassword, new { @id = "TxtConfirmpassword", @class = "text txtconfirmpassword", @maxlength = "20" })%>
                <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
            </li>
            <li>
                <input type="submit" value="Change Password" />
            </li>
        </ul>
        <% } %>
    
</asp:Content>