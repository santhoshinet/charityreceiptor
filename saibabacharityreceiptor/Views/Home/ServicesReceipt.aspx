﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.ServicesReceipt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Generate Services Receipt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm(ViewData["PostAction"].ToString()))
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
                <span class="instruction">Your date should be in the format of [ MM / DD / YYYY ]</span>
            </label>
            <%: Html.TextBoxFor(m => m.DateReceived, new { @id = "TxtDateReceived", @class = "text txtdate", @maxlength = "10" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Name</label>
            <%: Html.TextBoxFor(m => m.FirstName, new { @id = "TxtName", @class = "text txtname", @maxlength = "16" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Mi</label>
            <%: Html.TextBoxFor(m => m.Mi, new { @id = "TxtMi", @class = "text txtmi", @maxlength = "8" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Last Name</label>
            <%: Html.TextBoxFor(m => m.LastName, new { @id = "TxtLastName", @class = "text txtlastname", @maxlength = "16" })%>
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
                City</label>
            <%: Html.TextBoxFor(m => m.City, new { @id = "TxtCity", @class = "text txtcity", @maxlength = "15" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                State</label>
            <%: Html.TextBoxFor(m => m.State, new { @id = "TxtState", @class = "text txtstate", @maxlength = "15" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Zip Code</label>
            <%: Html.TextBoxFor(m => m.ZipCode, new { @id = "TxtZipCode", @class = "text txtzipcode", @maxlength = "10" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Email</label>
            <%: Html.TextBoxFor(m => m.Email, new { @id = "TxtEmail", @class = "text txtemail", @maxlength = "30" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Contact</label>
            <%: Html.TextBoxFor(m => m.Contact, new { @id = "TxtContact", @class = "text txtcontact", @maxlength="12" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                Service Type</label>
            <%: Html.TextBoxFor(m => m.ServiceType, new { @id = "TxtServiceType", @class = "text txtservicetype", @maxlength = "42" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                No Of hours served</label>
            <%: Html.TextBoxFor(m => m.HoursServed, new { @id = "Txthoursserved", @class = "text txthoursserved", @maxlength = "15" })%>
            <label class="star">
                *</label>
        </li>
        <li>
            <label class="label">
                FMV Value</label>
            <%: Html.TextBoxFor(m => m.FmvValue, new { @id = "TxtFrmValue", @class = "text txtfmvvalue", @maxlength = "15" })%>
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
        <li>
            <label class="label">
                Issued Date
            </label>
            <label class="label">
                <span class="instruction">Your date should be in the format of [ MM / DD / YYYY ]</span>
            </label>
            <%: Html.TextBoxFor(m => m.IssuedDate, new { @id = "TxtIssuedDate", @class = "text txtdate", @maxlength = "10" })%>
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