<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Select your receipt print option
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="ul">
        <li>
            <%
                var receiptId = ViewData["ReceiptID"].ToString();%>
            <h2>
                Print your receipt</h2>
            <p>
                Select your option.</p>
            <div class="container">
                <div class="right">
                    <a href="/PrintReceipt/<%= receiptId %>" target="_blank">Print receipt</a>
                </div>
            </div>
            <div class="clear">
            </div>
        </li>
    </ul>
</asp:Content>