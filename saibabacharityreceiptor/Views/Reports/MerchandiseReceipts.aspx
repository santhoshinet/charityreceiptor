<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report of Merchandise Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        var localMerchandiseReceipts = (List<LocalMerchandiseReceipt>)ViewData["MerchandiseReceipts"];
        if (localMerchandiseReceipts != null && localMerchandiseReceipts.Count > 0)
        {
    %>
    <ul class="ul">
        <li>
            <h2>
                Merchandise Receipts</h2>
            <p>
                List of merchandise receipts.</p>
            <table id="Cart_Table">
                <tr class="header">
                    <td style="width: 40px">
                        Sno
                    </td>
                    <td style="width: 11%;">
                        Name
                    </td>
                    <td style="width: 150px">
                        Date
                    </td>
                    <td style="width: 130px">
                        Merchandise Item
                    </td>
                    <td style="width: 80px">
                        Value
                    </td>
                    <td style="width: 11%">
                        Received By
                    </td>
                    <td colspan="4" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
            int index = 1;
                %>
                <%
            foreach (LocalMerchandiseReceipt localMerchandiseReceipt in localMerchandiseReceipts)
            {
                %>
                <tr id="<%=localMerchandiseReceipt.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%=localMerchandiseReceipt.Name%>
                    </td>
                    <td>
                        <%=localMerchandiseReceipt.OnDateTime.ToString("dd MMM yyyy (HH:mm)")%>
                    </td>
                    <td>
                        <%= localMerchandiseReceipt.MerchandiseItem %>
                    </td>
                    <td>
                        <%= localMerchandiseReceipt.Value %>
                    </td>
                    <td>
                        <%= localMerchandiseReceipt.DonationReceiverName%>
                    </td>
                    <td style="width: 60px">
                        <span class="delete_button" href="<%="/Reports/EditRegularReceipt/" + localMerchandiseReceipt.ReceiptNumber%>">
                            <img src="/Images/ico-delete.gif" />
                            delete</span>
                    </td>
                    <td style="width: 50px">
                        <span class="edit_button" href="<%="/Reports/EditRegularReceipt/" + localMerchandiseReceipt.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <td style="width: 30px">
                        <a href="<%="/PrintReceipt/" + localMerchandiseReceipt.ReceiptNumber%>" target="_blank">
                            Print</a>
                    </td>
                    <td style="width: 20px">
                        <a href="<%="/DownloadReceipt/" + localMerchandiseReceipt.ReceiptNumber%>" target="_blank">
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
</asp:Content>