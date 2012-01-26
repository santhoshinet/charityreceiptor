<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<html>
<head id="Head1" runat="server">
    <title>Edit Regular Receipt</title>
    <link href="/Content/FormLayout.css" rel="stylesheet" type="text/css" />
    <link href="/Content/document.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <% Html.RenderPartial("RegularReceipt_Ctrl"); %>
</body>
</html>