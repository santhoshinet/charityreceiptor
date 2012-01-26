<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.LogOnModel>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Log On</title>
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
    <link href="/Content/document.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <% Html.RenderPartial("Logon_Ctrl"); %>
</body>
</html>