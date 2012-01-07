<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.RegularReceiptModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    General Receipt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <ul class="ul">
        <li>
            <h1>
                Shirdi Saibaba Temple Arizona donations</h1>
        </li>
        <li>
            <label class="label">
                Receipt Number
            </label>
            <label class="label">
                <span class="instruction">auto generated receipt id</span>
            </label>
            <%: Html.TextBoxFor(m => m.ReceiptNumber, new { @id = "TxtReceiptNumber", @class = "text txtreceiptno" , @maxlength="10", @readonly="readonly" })%>
        </li>
        <li>
            <label class="label">
                Date Received
            </label>
            <label class="label">
                <span class="instruction">Your date should be in the format of [ DD / MM / YYYY ]</span>
            </label>
            <%: Html.TextBoxFor(m => m.DateReceived, new { @id = "TxtDateReceived", @class = "text txtdate", @maxlength = "10" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Name</label>
            <%: Html.TextBoxFor(m => m.Name, new { @id = "TxtName", @class = "text txtname" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Address</label>
            <%: Html.TextAreaFor(m => m.Address, new { @id = "TxtAddress", @class = "text txtaddress" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Email</label>
            <%: Html.TextBoxFor(m => m.Email, new { @id = "TxtEmail", @class = "text txtemail" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Contact</label>
            <%: Html.TextBoxFor(m => m.Contact, new { @id = "TxtContact", @class = "text txtcontact", @maxlength="15" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Donation Amount</label>
            <%: Html.TextBoxFor(m => m.DonationAmount, new { @id = "TxtDonationAmount", @class = "text txtdonationamount" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Donation Amount in words</label>
            <%: Html.TextBoxFor(m => m.DonationAmountinWords, new { @id = "TxtDonationAmountinWords", @class = "text txtdonationinwords" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Mode of Payment</label>
            <select id="CmbModeOfPayment" class="cmbModeOfPayment" name="cmbModeOfPayment">
                <option value="Select Mode of Payment" title="Select Mode of Payment" selected="selected">
                    --Select Mode of Payment--</option>
                <%
           var modeOfPayment = (List<string>)ViewData["modeOfPayment"];%>
                <% foreach (var payment in modeOfPayment)
                   { %>
                <option value="<%= payment %>" title="<%= payment %>">
                    <%= payment %>
                </option>
                <% } %>
            </select>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Donation Received By</label>
            <select id="CmbDonationReceivedBy" class="CmbDonationReceivedBy" name="CmbDonationReceivedBy">
                <option value="Select receiver" title="Select receiver">--Select receiver--</option>
                <%
           var donationReceivers = (List<string>)ViewData["donationReceivers"];%>
                <% foreach (var receiver in donationReceivers)
                   { %>
                <option value="<%= receiver %>" title="<%= receiver %>">
                    <%= receiver %>
                </option>
                <% } %>
            </select>
            <label class="star">
                *</label>
        </li>
        <li></li>
        <li>
            <input type="submit" value="Submit my donation" />
        </li>
    </ul>
    <%
       }%>
    <!-- Loading script at end for fast render -->
    <link href="/Content/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/JQValidation.js" type="text/javascript"></script>
    <script src="/Scripts/receiptor.js" type="text/javascript"></script>
</asp:Content>