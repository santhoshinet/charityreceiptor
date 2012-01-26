<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.MerchandiseReceipt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Merchandise Receipt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("MerchandiseReceipt_Ctrl"); %>
</asp:Content>