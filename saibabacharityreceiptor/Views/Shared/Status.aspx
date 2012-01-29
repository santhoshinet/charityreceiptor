<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Status</title>
</head>
<body>
    <%
        Html.RenderPartial("Status_Ctrl");%>
</body>
</html>