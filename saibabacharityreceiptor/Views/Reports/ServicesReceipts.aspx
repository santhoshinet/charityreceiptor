<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report of Services Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var localServicesReceipts = (List<LocalServicesReceipt>)ViewData["ServicesReceipts"];
        if (localServicesReceipts != null && localServicesReceipts.Count > 0)
        {
    %>
    <ul class="ul">
        <li>
            <h2>
                Services Receipts</h2>
            <p>
                List of services receipts.</p>
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
                    <td style="width: 50px">
                        Hours Served
                    </td>
                    <td style="width: 22%">
                        Audited By
                    </td>
                    <td colspan="4" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
            int index = Convert.ToInt32(ViewData["RecordIndex"]);
                %>
                <%
            foreach (LocalServicesReceipt localServicesReceipt in localServicesReceipts)
            {
                %>
                <tr id="<%=localServicesReceipt.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%= localServicesReceipt.ReceiptNumber %>
                    </td>
                    <td>
                        <%=localServicesReceipt.FirstName%>
                    </td>
                    <td>
                        <%= localServicesReceipt.LastName %>
                    </td>
                    <td>
                        <%=localServicesReceipt.OnDateTime.ToString("dd MMM yyyy")%>
                    </td>
                    <td style="text-align: right;">
                        <%= localServicesReceipt.HoursServed%>
                    </td>
                    <td>
                        <%= localServicesReceipt.DonationReceiverName%>
                    </td>
                    <td style="width: 60px">
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />delete</span>
                    </td>
                    <td style="width: 50px">
                        <span class="edit_button" href="<%="/EditReceipt/" + localServicesReceipt.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <td style="width: 30px">
                        <a href="<%="/PrintReceipt/" + localServicesReceipt.ReceiptNumber%>" target="_blank">
                            Print</a>
                    </td>
                    <td style="width: 20px">
                        <a href="<%="/DownloadReceipt/" + localServicesReceipt.ReceiptNumber%>" target="_blank">
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