<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.RecurringReceipt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Generate Recurring Receipt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("RecurringReceipt_Ctrl"); %>
</asp:Content>