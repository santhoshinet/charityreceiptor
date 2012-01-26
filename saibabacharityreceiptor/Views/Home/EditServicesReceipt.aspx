<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Edit Services Receipt</title>
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
    <link href="/Content/document.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <% Html.RenderPartial("ServicesReceipt_Ctrl"); %>
</body>
</html>