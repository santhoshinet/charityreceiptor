<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Content/status.css" rel="stylesheet" type="text/css" />
<div class="status_container">
    <div>
        <%= ViewData["Status"] %></div>
</div>