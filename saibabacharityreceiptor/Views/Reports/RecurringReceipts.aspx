<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report of Recurring Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var localRegularReceipts = (List<LocalRegularReceipt>)ViewData["RegularReceipts"];
        if (localRegularReceipts != null && localRegularReceipts.Count > 0)
        {
    %>
    <ul class="ul">
        <li>
            <h2>
                Regular Receipts</h2>
            <p>
                List of regular receipts.</p>
            <table id="Cart_Table">
                <tr class="header">
                    <td style="width: 40px">
                        Sno
                    </td>
                    <td style="width: 320px;">
                        Name
                    </td>
                    <td style="width: 260px">
                        Date
                    </td>
                    <td style="width: 100px">
                        Amount
                    </td>
                    <td style="width: 80px">
                        Mode of payment
                    </td>
                    <td style="width: 320px">
                        Received By
                    </td>
                    <td colspan="2" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
            int index = 1;
                %>
                <%
            foreach (LocalRegularReceipt localRegularReceipt in localRegularReceipts)
            {
                %>
                <tr id="<%=localRegularReceipt.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%=localRegularReceipt.Name%>
                    </td>
                    <td>
                        <%=localRegularReceipt.OnDateTime.ToString("dd MMM yyyy (HH:mm)")%>
                    </td>
                    <td style="text-align: right;">
                        <%= localRegularReceipt.DonationAmount%>
                    </td>
                    <td>
                        <%= localRegularReceipt.ModeOfPayment.ToString()%>
                    </td>
                    <td>
                        <%= localRegularReceipt.DonationReceiverName %>
                    </td>
                    <td style="width: 100px">
                        <span class="delete_button" href="<%="/Reports/EditRegularReceipt/" + localRegularReceipt.ReceiptNumber%>">
                            <img src="/Images/ico-delete.gif" />
                            delete</span>
                    </td>
                    <td style="width: 100px">
                        <span class="edit_button" href="<%="/Reports/EditRegularReceipt/" + localRegularReceipt.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                </tr>
                <%
                index = index + 1;%>
                <%
            }%>
                <tr id="noresultsrow">
                    <td colspan="6">
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
                    <td colspan="6">
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
    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>
</asp:Content>