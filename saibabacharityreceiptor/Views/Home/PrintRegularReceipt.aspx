<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Receipt</title>
</head>
<body onload="javascript:window.print();">
    <%
        var receptData = (ReceiptData)ViewData["Receipt_Data"]; %>
    <%= receptData.FirstName %>
</body>
</html>