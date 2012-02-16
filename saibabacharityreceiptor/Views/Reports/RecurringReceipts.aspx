<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report of Recurring Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var localReccuringReceipts = (List<LocalRecurrenceReceipt>)ViewData["RecurringReceipts"];
        if (localReccuringReceipts != null && localReccuringReceipts.Count > 0)
        {
    %>
    <ul class="ul">
        <li>
            <h2>
                Recurring Receipts</h2>
            <p>
                List of Recurring receipts.</p>
            <table id="Cart_Table">
                <tr class="header">
                    <td class="sno">
                        Sno
                    </td>
                    <td class="recid">
                        Receipt ID
                    </td>
                    <td class="fistname">
                        First Name
                    </td>
                    <td class="lastname">
                        Last Name
                    </td>
                    <td class="tdate">
                        Date
                    </td>
                    <td colspan="3">
                        Recurring Details
                    </td>
                    <td>
                        Received By
                    </td>
                    <td colspan="4" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
            int index = Convert.ToInt32(ViewData["RecordIndex"]);
                %>
                <%
            foreach (LocalRecurrenceReceipt localRecurringReceipt in localReccuringReceipts)
            {
                %>
                <tr id="<%=localRecurringReceipt.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%= localRecurringReceipt.ReceiptNumber %>
                    </td>
                    <td>
                        <%=localRecurringReceipt.FirstName%>
                    </td>
                    <td>
                        <%= localRecurringReceipt.LastName %>
                    </td>
                    <td>
                        <%=localRecurringReceipt.OnDateTime.ToString("dd MMM yyyy")%>
                    </td>
                    <td style="text-align: right;" colspan="3">
                        <% foreach (RecurrenceData recurringdata in localRecurringReceipt.RecurringDatas)
                           {
                        %>
                        <%=  "($" + recurringdata.Amount + " , " + recurringdata.ModeOfPayment + " , " + recurringdata.Date + " )" + "<br />" %>
                        <% } %>
                    </td>
                    <td>
                        <%= localRecurringReceipt.DonationReceiverName%>
                    </td>
                    <td style="width: 60px">
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />delete</span>
                    </td>
                    <td style="width: 50px">
                        <span class="edit_button" href="<%="/EditReceipt/" + localRecurringReceipt.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <td style="width: 30px">
                        <a href="<%="/PrintReceipt/" + localRecurringReceipt.ReceiptNumber%>" target="_blank">
                            Print</a>
                    </td>
                    <td style="width: 20px">
                        <a href="<%="/DownloadReceipt/" + localRecurringReceipt.ReceiptNumber%>" target="_blank">
                            Pdf</a>
                    </td>
                </tr>
                <%
                index = index + 1;%>
                <%
            }%>
                <tr id="noresultsrow">
                    <td colspan="10">
                        There is no result found your query.
                    </td>
                </tr>
                <%
            var hasPrevious = (bool)ViewData["HasPrevious"];
            var hasNext = (bool)ViewData["HasNext"];
            var pageIndex = Convert.ToInt32(ViewData["pageIndex"]);
            if (hasNext || hasPrevious)
            {
                %>
                <tr class="footer">
                    <%
                if (hasPrevious)
                {%>
                    <td>
                        <a href="/Reports/RegularReceipts/<%= pageIndex - 1%>">Prev</a>
                    </td>
                    <%
                }
                else
                {%>
                    <td>
                    </td>
                    <%
                }%>
                    <td colspan="8">
                    </td>
                    <%
                if (hasNext)
                {%>
                    <td>
                        <a href="/Reports/RegularReceipts/<%= pageIndex + 1%>">Next</a>
                    </td>
                    <%
                }
                else
                {%>
                    <td>
                    </td>
                    <%
                }%>
                </tr>
                <%
            }%>
            </table>
        </li>
    </ul>
    <%
        }
        else
        {%>
    <ul class="ul">
        <li>
            <p>
                Records not found.</p>
        </li>
    </ul>
    <%
        }%>
    <link href="/Content/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/Reports.js" type="text/javascript"></script>
    <link href="/Content/reports.css" rel="stylesheet" type="text/css" />
</asp:Content>