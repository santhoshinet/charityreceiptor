<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<saibabacharityreceiptor.Models.LogOnModel>" %>
<% using (Html.BeginForm("LogOn", "Account"))
   { %>
<ul class="ul">
    <li>
        <h2>
            Log On</h2>
        <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
    </li>
    <li>
        <label class="label">
            Username
        </label>
        <%= Html.TextBoxFor(m => m.UserName, new { @id = "TxtUsername", @class = "text txtusername", @maxlength = "50" })%>
        <%= Html.ValidationMessageFor(m => m.UserName) %>
    </li>
    <li>
        <label class="label">
            Password
        </label>
        <%= Html.PasswordFor(m => m.Password, new { @id = "TxtPassword", @class = "text txtpassword", @maxlength = "20" })%>
        <%= Html.ValidationMessageFor(m => m.Password) %>
    </li>
    <li>
        <%= Html.CheckBoxFor(m => m.RememberMe) %>
        <%= Html.LabelFor(m => m.RememberMe) %>
    </li>
    <li>
        <input type="submit" value="Log On" />
    </li>
</ul>
<% } %>