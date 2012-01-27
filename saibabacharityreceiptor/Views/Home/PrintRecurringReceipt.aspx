<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Recurring Receipt</title>
    <link href="/Content/receipt.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:window.print();">
    <div class="container">
        <%
            var receptData = (ReceiptData)ViewData["Receipt_Data"]; %>
        <div class="empty-header">
        </div>
        <div class="header">
            <h3>
                Donation Receipt</h3>
        </div>
        <div class="data">
            <ul class="receipt">
                <li class="one"><span class="field">Receipt #:</span><span class="value"><%= receptData.ReceiptNumber %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">First name:</span><span class="value"><%= receptData.FirstName %></span></li>
                <li class="three"><span class="field">MI:</span><span class="value"><%= receptData.Mi %></span></li>
                <li class="three"><span class="field">Last name:</span><span class="value"><%= receptData.LastName %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Address:</span><span class="value"><%= receptData.Address %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">City:</span><span class="value"><%= receptData.City %></span></li>
                <li class="three"><span class="field">State:</span><span class="value"><%= receptData.State %></span></li>
                <li class="three"><span class="field">Zip:</span><span class="value"><%= receptData.ZipCode %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Email:</span><span class="value"><%= receptData.Email %></span></li>
                <li class="two"><span class="field">Phone:</span><span class="value"><%= receptData.Contact %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received date:</span><span class="value"><%= receptData.DateReceived.ToString("dd MMM yyyy") %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one">
                    <table>
                        <tr class="Tableheader">
                            <td>
                                Recurring ID
                            </td>
                            <td>
                                Date received
                            </td>
                            <td>
                                Mode of donation
                            </td>
                            <td>
                                Amount in USD
                            </td>
                        </tr>
                        <%
                            int recurringId = 1;
                            foreach (DateTime recurringDate in receptData.RecurringDates)
                            { %>
                        <tr>
                            <td>
                                <%= recurringId ++%>
                            </td>
                            <td>
                                <%= recurringDate.ToString("MM dd yyyy") %>
                            </td>
                            <td>
                                <%= receptData.ModeOfPayment %>
                            </td>
                            <td>
                                <%= receptData.DonationAmount %>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                </li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation amount received in USD:</span><span
                    class="value"><%= receptData.DonationAmount %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received in words:</span><span class="value"><%= receptData.DonationAmountinWords %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Mode of donation:</span><span class="value"><%= receptData.ModeOfPayment %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="box">
            <ul>
                <li class="two"><span class="field">Donation received by:</span><span class="value"><%= receptData.DonationReceiverName %></span></li>
                <li class="two"><span class="field">Signature:</span><span class="value"></span></li>
            </ul>
            <div class="clear">
            </div>
            <ul>
                <li class="two"><span class="field">Shridi Saibaba Temple Arizona</span><span class="value"></span></li>
                <li class="two"><span class="field">Issues date:</span><span class="value"><%= receptData.IssuedDate.ToString("dd MMM yyyy") %></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="notes">
            <ul>
                <li>No goods or services were provided in exchange for these contributions.</li>
                <li>This document is necessary for any available federal income tax deduction for your
                    contribution. Please retain it for your records.</li>
            </ul>
        </div>
        <div class="slogan">
            <h3>
                “Dharma will put an end to Karma”</h3>
        </div>
        <div class="thanks">
            <h3>
                Thank You – Jai Sairam!</h3>
        </div>
        <div class="barcode">
            <img src="#" />
        </div>
    </div>
</body>
</html>