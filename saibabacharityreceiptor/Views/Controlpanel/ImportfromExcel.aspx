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
                Download the excel file <a href="/template.xlsx">here</a></p>
        </li>
        <li>
            <label class="label">
                Upload excel file</label>
            <input type="file" name="ExcelFile" />
            <%=Html.ValidationMessageFor(m => m.ExcelFile)%>
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
</asp:Content>