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
<<<<<<< HEAD
                Download the excel file here - <a href="/template.xlsx">Excel-2007</a>
            </p>
=======
                Download the excel template file <a href="/template.xlsx">here</a></p>
>>>>>>> 057b2276016c3c2e9752b3024f3594d9d65a97d8
            <p class="error">
                <%= ViewData["Status"].ToString()%>
            </p>
        </li>
        <li>
            <label class="label">
                Upload excel file</label>
            <input type="file" name="ExcelFile" />
            <%=Html.ValidationMessageFor(m => m.ExcelFile)%>
        </li>
        <li>
            <input type="submit" value="Upload" accept="*.xls|*.xlsx" accept="application/excel" />
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