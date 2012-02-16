﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<saibabacharityreceiptor.Models.RecurringReceipt>" %>
<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<% using (Html.BeginForm(ViewData["PostAction"].ToString(), "Home", FormMethod.Post, new { enctype = "multipart/form-data" }))
   {%>
<ul class="ul">
    <li>
        <h2>
            Recurring Receipt</h2>
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
            First Name</label>
        <%: Html.TextBoxFor(m => m.FirstName, new { @id = "TxtName", @class = "text txtname", @maxlength = "16" })%>
        <label class="star">
            *</label>
    </li>
    <li>
        <label class="label">
            MI</label>
        <%: Html.TextBoxFor(m => m.Mi, new { @id = "TxtMi", @class = "text txtmi", @maxlength = "8" })%>
    </li>
    <li>
        <label class="label">
            Last Name</label>
        <%: Html.TextBoxFor(m => m.LastName, new { @id = "TxtLastName", @class = "text txtlastname", @maxlength = "16" })%>
    </li>
    <li>
        <label class="label">
            Address 1</label>
        <%: Html.TextAreaFor(m => m.Address, new { @id = "TxtAddress", @class = "text txtaddress", @maxlength = "255" })%>
        <label class="star">
            *</label>
    </li>
    <li>
        <label class="label">
            Address 2</label>
        <%: Html.TextAreaFor(m => m.Address2, new { @id = "TxtAddress2", @class = "text txtaddress2", @maxlength = "255" })%>
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
            Contact No</label>
        <%: Html.TextBoxFor(m => m.Contact, new { @id = "TxtContact", @class = "text txtcontact", @maxlength="12" })%>
        <label class="star">
            *</label>
    </li>
    <li>
        <label class="label">
            Recurrence Amount Details</label>
        <table class="recurrenceTable">
            <thead>
                <tr>
                    <th>
                        <label class="label">
                            Amount</label>
                    </th>
                    <th>
                        <label class="label">
                            Mode of Payment</label>
                    </th>
                    <th>
                        <label class="label">
                            Date</label>
                    </th>
                    <th>
                    </th>
                    <th>
                    </th>
                </tr>
            </thead>
            <tbody>
                <%
var recurrenceData = (List<RecurrenceData>)ViewData["RecurringDetails"];
if (recurrenceData == null || recurrenceData.Count == 0)
{
                %>
                <tr>
                    <td>
                        <input type="text" name="RecurrenceAmount" id="RecurrenceAmount" class="text txtrecurrenceamount" />
                    </td>
                    <td>
                        <select id="CmbModeOfPayment" class="cmbModeOfPayment" name="RecurrenceModeofPayment">
                            <option value="Mode of Payment" title="Mode of Payment" <%
           if (string.IsNullOrEmpty(ViewData["selectedModeOfPayment"].ToString()))
           {%> selected="selected" <%
           }%>>Mode of Payment</option>
                            <%
    var modeOfPayment = (List<string>)ViewData["modeOfPayment"];%>
                            <%
    foreach (var payment in modeOfPayment)
    {
        if (payment.Trim().ToLower() == "goods")
            continue;
                            %>
                            <%
        if (ViewData["selectedModeOfPayment"].ToString().ToLower() == payment.ToLower())
        {%>
                            <option value="<%=payment%>" title="<%=payment%>" selected="selected">
                                <%
        }
        else
        {
                                %>
                                <option value="<%=payment%>" title="<%=payment%>">
                                    <%
        }%>
                                    <%=payment%>
                                </option>
                                <%
    }%>
                        </select>
                    </td>
                    <td>
                        <input type="text" name="RecurrenceDates" id="TxtRecurrenceDates" class="text txtrecurrencedates smallbox" />
                    </td>
                    <td>
                        <label class="star">
                            *</label>
                    </td>
                    <td>
                        <span class="btnaction">+</span>
                    </td>
                </tr>
                <%
}
else if (recurrenceData != null)
{
    int index = 1;
    foreach (RecurrenceData recuData in recurrenceData)
    {

                %>
                <tr>
                    <td>
                        <input type="text" name="RecurrenceAmount" id="Text1" class="text txtrecurrenceamount"
                            value="<%= recuData.Amount %>" />
                    </td>
                    <td>
                        <select id="Select1" class="cmbModeOfPayment" name="RecurrenceModeofPayment">
                            <option value="Mode of Payment" title="Mode of Payment">Mode of Payment</option>
                            <%
        var modeOfPayment = (List<string>)ViewData["modeOfPayment"];%>
                            <%
        foreach (var payment in modeOfPayment)
        {
            if (payment.Trim().ToLower() == "goods")
                continue;
                            %>
                            <%
            if (recuData.ModeOfPayment.ToLower() == payment.ToLower())
            {%>
                            <option value="<%=payment%>" title="<%=payment%>" selected="selected">
                                <%
            }
            else
            {
                                %>
                                <option value="<%=payment%>" title="<%=payment%>">
                                    <%
            }%>
                                    <%=payment%>
                                </option>
                                <%
        }%>
                        </select>
                    </td>
                    <td>
                        <input type="text" name="RecurrenceDates" id="Text2" class="text txtrecurrencedates smallbox"
                            value="<%= recuData.Date %>" />
                    </td>
                    <td>
                        <label class="star">
                            *</label>
                    </td>
                    <td>
                        <span class="btnaction">+</span>
                        <% if (index++ > 1)
                           {%>
                        <span class="btnactionremove">-</span>
                        <%
                           }%>
                    </td>
                </tr>
                <%
    }
}%>
            </tbody>
        </table>
    </li>
    <li class="clear"></li>
    <li>
        <label class="label">
            Donation Receiver</label>
        <select id="CmbDonationReceivedBy" class="CmbDonationReceivedBy" name="CmbDonationReceivedBy">
            <option value="Select receiver" title="Select receiver">--Select receiver--</option>
            <%
       var donationReceivers = (List<string>)ViewData["donationReceivers"];%>
            <% foreach (var receiver in donationReceivers)
               {
                   if (ViewData["selectedDonationReceivedBy"].ToString().ToLower() == receiver.ToLower())
                   {
            %>
            <option value="<%=receiver%>" title="<%=receiver%>" selected="selected">
                <%
                   }
                   else
                   {%>
                <option value="<%=receiver%>" title="<%=receiver%>">
                    <%
                   }%>
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
    <li>
        <label class="label">
            Signature Image
        </label>
        <input type="file" name="SignatureImage" />
        <%=Html.ValidationMessageFor(m => m.SignatureImage)%>
    </li>
    <li></li>
    <li>
        <input type="submit" value="Submit" />
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