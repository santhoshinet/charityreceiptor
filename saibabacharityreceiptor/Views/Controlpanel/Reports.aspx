<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="ul">
        <li>
            <h2>
                Reports</h2>
            <p>
            </p>
            <div class="container">
                <div class="right">
                    <a href="/Reports/RegularReceipts">Regular Receipts</a></div>
                <div class="right">
                    <a href="/Reports/RecurringReceipts">Recurring Receipts</a></div>
                <div class="right">
                    <a href="/Reports/MerchandiseReceipts">Merchandise Reports</a></div>
                <div class="right">
                    <a href="/Reports/ServicesReceipts">Services Receipts</a></div>
            </div>
            <div class="clear">
            </div>
        </li>
    </ul>
</asp:Content>