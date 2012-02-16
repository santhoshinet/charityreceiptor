<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Receipt</title>
    <link href="/Content/receipt.css" rel="stylesheet" type="text/css" />
</head>
<body onload="window.print();">
    <%
        var receipts = (List<ReceiptData>)ViewData["Receipt_Data"];
        if (receipts != null && receipts.Count > 0)
        {
            foreach (ReceiptData receiptData in receipts)
            {
                switch (receiptData.ReceiptType)
                {
                    case "GeneralReceipt":
                        {
    %>
    <div class="border">
    <div class="data logo">
        <img src="/Images/Report_Header.png" alt="logo" />
    </div>
    <div class="clear">
    </div>
    <div class="container">
        <div class="header">
            <h3>
                Donation Receipt</h3>
        </div>
        <div class="data">
            <ul class="receipt">
                <li class="one"><span class="field">Receipt #:</span><span class="value"><%= receiptData.ReceiptNumber%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">First name:</span><span class="value"><%= receiptData.FirstName%></span></li>
                <li class="three"><span class="field">MI:</span><span class="value"><%= receiptData.Mi%></span></li>
                <li class="three"><span class="field">Last name:</span><span class="value"><%= receiptData.LastName%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <table style="border-width: 0px; padding: 0px; margin: 0px;">
                <tr>
                    <td>
                        <div class="field">
                            Address1:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address%>
                        </div>
                    </td>
                    <td>
                        <div class="field">
                            Address2:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address2 %></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">City:</span><span class="value"><%= receiptData.City%></span></li>
                <li class="three"><span class="field">State:</span><span class="value"><%= receiptData.State%></span></li>
                <li class="three"><span class="field">Zip:</span><span class="value"><%= receiptData.ZipCode%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Email:</span><span class="value"><%= receiptData.Email%></span></li>
                <li class="two"><span class="field">Phone:</span><span class="value"><%= receiptData.Contact%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received date:</span><span class="value"><%= receiptData.DateReceived.ToString("dd MMM yyyy")%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation amount received in $:</span><span class="value"><%= receiptData.DonationAmount%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received in words:</span><span class="value"><%= receiptData.DonationAmountinWords%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Mode of donation:</span><span class="value"><%= receiptData.ModeOfPayment%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="box">
            <ul>
                <li class="two"><span class="field">Donation received by:</span><span class="value"><%= receiptData.DonationReceiverName%></span></li>
                <li class="two">
                    <div class="signature">
                        Signature:</div>
                    <span class="value">
                        <image src="/signature/<%= receiptData.SignatureId %>" alt=""></image>
                    </span></li>
            </ul>
            <div class="clear">
            </div>
            <ul>
                <li class="two"><span class="field">Shridi Saibaba Temple Arizona</span><span class="value"></span></li>
                <li class="two"><span class="field">Issues date:</span><span class="value"><%= receiptData.IssuedDate.ToString("dd MMM yyyy")%></span></li>
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
            <img src="http://shirdisaibabaaz.org/barcode/<%= receiptData.ReceiptNumber  %>" alt="barcode" />
        </div>
    </div>
    </div>
    <%
break;
                        }
                    case "RecurringReceipt":
                        {
    %>
    <div class="border">
    <div class="data logo">
        <img src="/Images/Report_Header.png" alt="logo" />
    </div>
    <div class="clear">
    </div>
    <div class="container">
        <div class="header">
            <h3>
                Donation Receipt</h3>
        </div>
        <div class="data">
            <ul class="receipt">
                <li class="one"><span class="field">Receipt #:</span><span class="value"><%= receiptData.ReceiptNumber%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">First name:</span><span class="value"><%= receiptData.FirstName%></span></li>
                <li class="three"><span class="field">MI:</span><span class="value"><%= receiptData.Mi%></span></li>
                <li class="three"><span class="field">Last name:</span><span class="value"><%= receiptData.LastName%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <table style="border-width: 0px; padding: 0px; margin: 0px;">
                <tr>
                    <td>
                        <div class="field">
                            Address1:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address%>
                        </div>
                    </td>
                    <td>
                        <div class="field">
                            Address2:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address2 %></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">City:</span><span class="value"><%= receiptData.City%></span></li>
                <li class="three"><span class="field">State:</span><span class="value"><%= receiptData.State%></span></li>
                <li class="three"><span class="field">Zip:</span><span class="value"><%= receiptData.ZipCode%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Email:</span><span class="value"><%= receiptData.Email%></span></li>
                <li class="two"><span class="field">Phone:</span><span class="value"><%= receiptData.Contact%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received date:</span><span class="value"><%= receiptData.DateReceived.ToString("dd MMM yyyy")%></span></li>
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
                                Amount in $
                            </td>
                        </tr>
                        <%
int recurringId = 1;
foreach (var recurringDetail in receiptData.RecurringDetails)
{ %>
                        <tr>
                            <td>
                                <%= recurringId ++%>
                            </td>
                            <td>
                                <%= recurringDetail.Date %>
                            </td>
                            <td>
                                <%= recurringDetail.ModeOfPayment %>
                            </td>
                            <td>
                                <%= recurringDetail.Amount %>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                </li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="box">
            <ul>
                <li class="two"><span class="field">Donation received by:</span><span class="value"><%= receiptData.DonationReceiverName%></span></li>
                <li class="two">
                    <div class="signature">
                        Signature:</div>
                    <span class="value">
                        <image src="/signature/<%= receiptData.SignatureId %>" alt=""></image>
                    </span></li>
            </ul>
            <div class="clear">
            </div>
            <ul>
                <li class="two"><span class="field">Shridi Saibaba Temple Arizona</span><span class="value"></span></li>
                <li class="two"><span class="field">Issues date:</span><span class="value"><%= receiptData.IssuedDate.ToString("dd MMM yyyy")%></span></li>
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
            <img src="/barcode/<%= receiptData.ReceiptNumber  %>" alt="barcode" />
        </div>
    </div>
    </div>
    <%
break;
                        }
                    case "MerchandiseReceipt":
                        {
    %>
    <div class="border">
    <div class="data logo">
        <img src="/Images/Report_Header.png" alt="logo" />
    </div>
    <div class="clear">
    </div>
    <div class="container">
        <div class="header">
            <h3>
                Donation Receipt - Merchandise</h3>
        </div>
        <div class="data">
            <ul class="receipt">
                <li class="one"><span class="field">Receipt #:</span><span class="value"><%= receiptData.ReceiptNumber%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">First name:</span><span class="value"><%= receiptData.FirstName%></span></li>
                <li class="three"><span class="field">MI:</span><span class="value"><%= receiptData.Mi%></span></li>
                <li class="three"><span class="field">Last name:</span><span class="value"><%= receiptData.LastName%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <table style="border-width: 0px; padding: 0px; margin: 0px;">
                <tr>
                    <td>
                        <div class="field">
                            Address1:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address%>
                        </div>
                    </td>
                    <td>
                        <div class="field">
                            Address2:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address2 %></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">City:</span><span class="value"><%= receiptData.City%></span></li>
                <li class="three"><span class="field">State:</span><span class="value"><%= receiptData.State%></span></li>
                <li class="three"><span class="field">Zip:</span><span class="value"><%= receiptData.ZipCode%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Email:</span><span class="value"><%= receiptData.Email%></span></li>
                <li class="two"><span class="field">Phone:</span><span class="value"><%= receiptData.Contact%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Donation received date:</span><span class="value"><%= receiptData.DateReceived.ToString("dd MMM yyyy")%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Goods received:</span><span class="value"><%= receiptData.MerchandiseItem%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Goods FMV in $:</span><span class="value"><%= receiptData.FmvValue%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="box">
            <ul>
                <li class="two"><span class="field">Donation received by:</span><span class="value"><%= receiptData.DonationReceiverName%></span></li>
                <li class="two">
                    <div class="signature">
                        Signature:</div>
                    <span class="value">
                        <image src="/signature/<%= receiptData.SignatureId %>" alt=""></image>
                    </span></li>
            </ul>
            <div class="clear">
            </div>
            <ul>
                <li class="two"><span class="field">Shridi Saibaba Temple Arizona</span><span class="value"></span></li>
                <li class="two"><span class="field">Issues date:</span><span class="value"><%= receiptData.IssuedDate.ToString("dd MMM yyyy")%></span></li>
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
            <img src="/barcode/<%= receiptData.ReceiptNumber  %>" alt="barcode" />
        </div>
    </div>
    </div>
    <%
break;
                        }
                    case "ServicesReceipt":
                        {
    %>
    <div class="border">
    <div class="data logo">
        <img src="/Images/Report_Header.png" alt="logo" />
    </div>
    <div class="clear">
    </div>
    <div class="container">
        <div class="header">
            <h3>
                Donation Receipt - Service</h3>
        </div>
        <div class="data">
            <ul class="receipt">
                <li class="one"><span class="field">Receipt #:</span><span class="value"><%= receiptData.ReceiptNumber%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">First name:</span><span class="value"><%= receiptData.FirstName%></span></li>
                <li class="three"><span class="field">MI:</span><span class="value"><%= receiptData.Mi%></span></li>
                <li class="three"><span class="field">Last name:</span><span class="value"><%= receiptData.LastName%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <table style="border-width: 0px; padding: 0px; margin: 0px;">
                <tr>
                    <td>
                        <div class="field">
                            Address1:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address%>
                        </div>
                    </td>
                    <td>
                        <div class="field">
                            Address2:</div>
                    </td>
                    <td>
                        <div class="value">
                            <%= receiptData.Address2 %></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="three"><span class="field">City:</span><span class="value"><%= receiptData.City%></span></li>
                <li class="three"><span class="field">State:</span><span class="value"><%= receiptData.State%></span></li>
                <li class="three"><span class="field">Zip:</span><span class="value"><%= receiptData.ZipCode%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Email:</span><span class="value"><%= receiptData.Email%></span></li>
                <li class="two"><span class="field">Phone:</span><span class="value"><%= receiptData.Contact%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Service received date:</span><span class="value"><%= receiptData.DateReceived.ToString("dd MMM yyyy")%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">Service type:</span><span class="value"><%= receiptData.ServiceType%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="two"><span class="field">Service Duration (No.of hrs/ day):</span><span
                    class="value"><%= receiptData.HoursServed%></span></li>
                <li class="two"><span class="field">Rate per hr/day:</span><span class="value"><%= receiptData.RatePerHrOrDay%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <ul>
                <li class="one"><span class="field">FMV in $:</span><span class="value"><%= receiptData.FmvValue%></span></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="box">
            <ul>
                <li class="two"><span class="field">Donation received by:</span><span class="value"><%= receiptData.DonationReceiverName%></span></li>
                <li class="two">
                    <div class="signature">
                        Signature:</div>
                    <span class="value">
                        <image src="/signature/<%= receiptData.SignatureId %>" alt=""></image>
                    </span></li>
            </ul>
            <div class="clear">
            </div>
            <ul>
                <li class="two"><span class="field">Shridi Saibaba Temple Arizona</span><span class="value"></span></li>
                <li class="two"><span class="field">Issues date:</span><span class="value"><%= receiptData.IssuedDate.ToString("dd MMM yyyy")%></span></li>
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
            <img src="/barcode/<%= receiptData.ReceiptNumber  %>" alt="barcode" />
        </div>
    </div>
    </div>
    <%
break;
                        }
    %>
    <%
                }
            }
        }
        else
        {%>
    <div class="empty">
        <h3>
            The receipt not found for the given id."
        </h3>
    </div>
    <%
        }%>
</body>
</html>