<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.ExporttoExcelModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Export to Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("ExporttoExcel", "Reports"))
       { %>
    <ul class="ul SearchContainer">
        <li>
            <h2>
                Export to excel</h2>
        </li>
        <li>
            <table>
                <tr>
                    <td>
                        <span>Start date:</span><%= Html.TextBoxFor(m => m.StartDate, new { @id = "TxtStartDate", @class = "text txtdate", @maxlength = "10" })%>
                    </td>
                    <td>
                        <span>End Date:</span><%= Html.TextBoxFor(m => m.EndDate, new { @id = "TxtEndDate", @class = "text txtdate", @maxlength = "10" })%>
                    </td>
                    <td>
                        <span>Type of receipt:</span>
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
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <input type="submit" value="Export to excel" />
                    </td>
                </tr>
            </table>
        </li>
    </ul>
    <% } %>
    <div class="clear">
    </div>
    <link href="/Content/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/Search.js" type="text/javascript"></script>
</asp:Content>