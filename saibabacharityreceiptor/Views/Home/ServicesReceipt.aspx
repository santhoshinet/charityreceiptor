<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.ServicesReceipt>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Generate Services Receipt
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("ServicesReceipt_Ctrl"); %>
</asp:Content>