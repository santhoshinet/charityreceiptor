<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Control panel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="ul">
        <li>
            <% if ((bool)ViewData["IsheDonationReceiver"])
               {%>
            <h2>
                Generate a receipt</h2>
            <p>
                Select your receipt type.</p>
            <div class="container">
                <div class="right">
                    <a href="/RegularReceipt">Regular Receipt </a>
                </div>
                <div class="right">
                    <a href="/RecurringReceipt">Reccuring Receipt</a></div>
                <div class="right">
                    <a href="/MerchandiseReceipt">Merchandise Receipt</a></div>
                <div class="right">
                    <a href="/ServicesReceipt">Services Receipt</a></div>
            </div>
            <div class="clear">
            </div>
            <%
               }%>
            <%
                if ((bool)ViewData["IsheAdmin"])
                {
            %>
            <h2>
                Control panel</h2>
            <p>
                Manage your resources.</p>
            <div class="container">
                <div class="right">
                    <a href="/Controlpanel/users">Manage Users</a></div>
                <div class="right">
                    <a href="/Controlpanel/Reports">Reports</a></div>
                <div class="right">
                    <a href="/Controlpanel/ImportfromExcel">Import from Excel</a></div>
                <div class="right">
                    <a href="/Controlpanel/ExporttoExcel">Export to Excel</a></div>
                <div class="right">
                    <a href="/Reports/SearchReceipts">Search Receipts</a></div>
            </div>
            <%
                }%>
            <div class="clear">
            </div>
        </li>
    </ul>
</asp:Content>