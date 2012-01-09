<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<saibabacharityreceiptor.Models.RegisterModel>" %>
<div class="Container small">
    <% using (Html.BeginForm())
       { %>
    <ul class="ul">
        <li>
            <h2>
                Create new user</h2>
            <%= Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
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
                Email
            </label>
            <%= Html.TextBoxFor(m => m.Email, new { @id = "TxtEmail", @class = "text txtemail", @maxlength = "50" })%>
            <%= Html.ValidationMessageFor(m => m.Email) %>
        </li>
        <li>
            <label class="label">
                Password
            </label>
            <%= Html.PasswordFor(m => m.Password, new { @id = "Txtpassword", @class = "text txtpassword", @maxlength = "20" })%>
            <%= Html.ValidationMessageFor(m => m.Password) %>
        </li>
        <li>
            <label class="label">
                Confirm password
            </label>
            <%= Html.PasswordFor(m => m.ConfirmPassword, new { @id = "TxtConfirmpassword", @class = "text txtconfirmpassword", @maxlength = "20" })%>
            <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
        </li>
        <li>
            <%= Html.CheckBoxFor(m => m.DonationReceiver) %>
            <%= Html.LabelFor(m => m.DonationReceiver) %>
        </li>
        <li>
            <%= Html.CheckBoxFor(m => m.Admin) %>
            <%= Html.LabelFor(m => m.Admin) %>
        </li>
        <li>
            <input type="submit" value="Register" />
        </li>
    </ul>
    <% } %>
</div>