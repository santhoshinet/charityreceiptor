<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.SearchModel>" %>

<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("SearchReceipts", "Reports"))
       { %>
    <ul class="ul">
        <li>
            <h2>
                Search receipts</h2>
        </li>
        <li>
            <ul>
                <li><span>Start date:</span><%= Html.TextBoxFor(m => m.StartDate) %></li>
                <li><span>End Date:</span><%= Html.TextBoxFor(m => m.EndDate)%></></li>
                <li><span>Type of receipt:</span>
                    <select id="CmbTypeOfReceipt" class="cmbTypeofReceipt" name="TypeOfReceipt">
                        <%
var modeOfPayment = (List<string>)ViewData["typeofreceipts"];
foreach (var payment in modeOfPayment)
{
    if (ViewData["selectedTypeOfReceipt"].ToString().ToLower() == payment.ToLower())
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
                </li>
                <li><span>Records per page</span>
                    <select id="cmbrecordsperpage" name="Maxrecordsperpage">
                        <option title="20" value="20">20</option>
                        <option title="40" value="40">40</option>
                        <option title="60" value="60">60</option>
                        <option title="80" value="80">80</option>
                        <option title="100" value="100">100</option>
                    </select>
                </li>
            </ul>
            <%= Html.HiddenFor(m => m.PageIndex) %>
            <input type="submit" value="Search" />
        </li>
    </ul>
    <% } %>
    <div class="clear">
    </div>
    <ul class="ul">
        <li>
            <h2>
                Results</h2>
        <%
            var receiptDatas = (List<ReceiptData>)ViewData["SeachReceipts"];
            if (receiptDatas != null && receiptDatas.Count > 0)
            {
        %>
            <table id="Cart_Table">
                <tr class="header">
                    <td style="width: 40px">
                        Sno
                    </td>
                    <td style="width: 11%;">
                        First name
                    </td>
                    <td style="width: 150px">
                        MI
                    </td>
                    <td style="width: 130px">
                        Last name
                    </td>
                    <td style="width: 80px">
                        Received date
                    </td>
                    <td style="width: 11%">
                        Receipt type
                    </td>
                    <td colspan="4" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
                int index = 1;
                %>
                <%
                foreach (ReceiptData receiptData in receiptDatas)
                {
                %>
                <tr id="<%=receiptData.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%=receiptData.FirstName%>
                    </td>
                    <td>
                        <%= receiptData.Mi %>
                    </td>
                    <td>
                        <%= receiptData.LastName%>
                    </td>
                    <td>
                        <%=receiptData.DateReceived.ToString("dd MMM yyyy (HH:mm)")%>
                    </td>
                    <td>
                        <%= receiptData.ReceiptType%>
                    </td>
                    <td style="width: 60px">
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />delete</span>
                    </td>
                    <td style="width: 50px">
                        <span class="edit_button" href="<%="/EditReceipt/" + receiptData.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <td style="width: 30px">
                        <a href="<%="/PrintReceipt/" + receiptData.ReceiptNumber%>" target="_blank">Print</a>
                    </td>
                    <td style="width: 20px">
                        <a href="<%="/DownloadReceipt/" + receiptData.ReceiptNumber%>" target="_blank">Pdf</a>
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
        </>
        <%
            }
            else
            {%>
            <p>
                Records not found.</p>
        </li>
        <%
        }%>
    </ul>
    <link href="/Content/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/Reports.js" type="text/javascript"></script>
</asp:Content>