﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Edit Merchandise Receipt</title>
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
    <link href="/Content/document.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <% Html.RenderPartial("MerchandiseReceipt_Ctrl"); %>
</body>
</html>