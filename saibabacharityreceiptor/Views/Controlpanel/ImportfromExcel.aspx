<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.ExcelModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Import from Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("ImportfromExcel", "Controlpanel", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <ul class="ul">
        <li>
            <h2>
                Import from Excel</h2>
            <p>
                Download the excel file here - <a href="/template.xlsx">Excel-2007</a>
            </p>
            <p class="error">
                <%= ViewData["Status"].ToString()%>
            </p>
        </li>
        <li>
            <label class="label">
                Upload excel file</label>
            <input type="file" name="ExcelFile" class="ExcelFile" />
            <%=Html.ValidationMessageFor(m => m.ExcelFile)%>
        </li>
        <li>
            <label class="label">
                Upload signature file</label>
            <input type="file" name="SignatureFile" />
            <%=Html.ValidationMessageFor(m => m.SignatureFile )%>
        </li>
        <li>
            <input type="submit" value="Upload" />
        </li>
        <li>
            <div class="clear">
            </div>
        </li>
    </ul>
    <%
       }%>
    <script src="/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/excelImport.js" type="text/javascript"></script>
</asp:Content>