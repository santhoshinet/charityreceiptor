<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Status</title>
    <link href="/Content/stylesheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="loginContent">
        <div class="container">
            <div class="middle">
                <%= ViewData["Status"] %></div>
        </div>
    </div>
</body>
</html>