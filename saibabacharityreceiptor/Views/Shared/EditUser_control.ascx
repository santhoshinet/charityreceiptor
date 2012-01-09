<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<saibabacharityreceiptor.Models.RegisterModel>" %>
<div class="Container small">
    <% using (Html.BeginForm())
       { %>
    <ul class="ul">
        <li>
            <h2>
                Edit user</h2>
            <%= Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        </li>
        <li>
            <label class="label">
                Username
            </label>
            <%= Html.TextBoxFor(m => m.UserName, new { @id = "TxtUsername", @class = "text txtusername", @maxlength = "50", @readOnly = "readOnly" })%>
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
            <%= Html.CheckBoxFor(m => m.DonationReceiver) %>
            <%= Html.LabelFor(m => m.DonationReceiver) %>
        </li>
        <li>
            <%= Html.CheckBoxFor(m => m.Admin) %>
            <%= Html.LabelFor(m => m.Admin) %>
        </li>
        <li>
            <%= Html.HiddenFor(m => m.UserId) %>
            <input type="submit" value="Update" />
        </li>
    </ul>
    <% } %>
</div>