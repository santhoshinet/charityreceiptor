﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.SearchModel>" %>

<%@ Import Namespace="saibabacharityreceiptor.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Search Receipts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("SearchReceipts", "Reports"))
       { %>
    <ul class="ul SearchContainer">
        <li>
            <h2>
                Search receipts</h2>
        </li>
        <li>
            <table>
                <tr>
                    <td>
                        <span>Start date:</span><%= Html.TextBoxFor(m => m.StartDate, new { @id = "TxtStartDate", @class = "text txtdate", @maxlength = "10" })%>
                    </td>
                    <td>
                        <span>End Date:</span><%= Html.TextBoxFor(m => m.EndDate, new { @id = "TxtEndDate", @class = "text txtdate", @maxlength = "10" })%>
                    </td>
                    <td>
                        <span>Type of receipt:</span>
                        <select id="CmbTypeOfReceipt" class="cmbTypeofReceipt" name="TypeOfReceipt">
                            <%
var modeOfPayment = (List<string>)ViewData["typeofreceipts"];
foreach (var payment in modeOfPayment)
{
    if (ViewData["selectedTypeOfReceipt"].ToString().ToLower() == payment.ToLower())
    {%>
                            <option value="<%=payment%>" title="<%=payment%>" selected="selected">
                                <%
    }
    else
    {
                                %>
                                <option value="<%=payment%>" title="<%=payment%>">
                                    <%
    }%>
                                    <%=payment%>
                                </option>
                                <%
}%>
                        </select>
                    </td>
                    <td>
                        <span>Records per page</span>
                        <select id="cmbrecordsperpage" name="Maxrecordsperpage">
                            <option title="20" value="20">20</option>
                            <option title="40" value="40">40</option>
                            <option title="60" value="60">60</option>
                            <option title="80" value="80">80</option>
                            <option title="100" value="100">100</option>
                        </select>
                    </td>
                </tr>
            </table>
        </li>
        <li>
            <table>
                <tr>
                    <td>
                        <span>ReceiptID:</span><%= Html.TextBoxFor(m => m.ReceiptId, new { @id = "TxtReceiptID", @class = "text txtreceiptid", @maxlength = "25" })%>
                    </td>
                    <td>
                        <span>Fist Name:</span><%= Html.TextBoxFor(m => m.FirstName, new { @id = "TxtFirstname", @class = "text txtfirstname", @maxlength = "255" })%>
                    </td>
                    <td>
                        <span>Last Name:</span><%= Html.TextBoxFor(m => m.LastName, new { @id = "TxtLastname", @class = "text txtlastname", @maxlength = "255" })%>
                    </td>
                    <td style="width: 240px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%= Html.HiddenFor(m => m.PageIndex, new { @id = "hdnPageindex"}) %>
                        <input type="submit" value="Search" />
                    </td>
                </tr>
            </table>
        </li>
    </ul>
    <% } %>
    <div class="clear">
    </div>
    <ul class="ul">
        <li>
            <h2>
                Results</h2>
            <%
                var receiptDatas = (List<ReceiptData>)ViewData["SeachReceipts"];
                if (receiptDatas != null && receiptDatas.Count > 0)
                {
            %>
            <table id="Cart_Table">
                <tr class="header">
                    <td style="width: 40px">
                        Sno
                    </td>
                    <td style="width: 11%;">
                        First name
                    </td>
                    <td style="width: 150px">
                        MI
                    </td>
                    <td style="width: 130px">
                        Last name
                    </td>
                    <td style="width: 80px">
                        Received date
                    </td>
                    <td style="width: 11%">
                        Receipt type
                    </td>
                    <td colspan="4" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
                    int index = 1;
                %>
                <%
                    foreach (ReceiptData receiptData in receiptDatas)
                    {
                %>
                <tr id="<%=receiptData.ReceiptNumber%>">
                    <td>
                        <%=index%>
                    </td>
                    <td>
                        <%=receiptData.FirstName%>
                    </td>
                    <td>
                        <%= receiptData.Mi %>
                    </td>
                    <td>
                        <%= receiptData.LastName%>
                    </td>
                    <td>
                        <%=receiptData.DateReceived.ToString("dd MMM yyyy (HH:mm)")%>
                    </td>
                    <td>
                        <%= receiptData.ReceiptType%>
                    </td>
                    <td style="width: 60px">
                        <span class="delete_button">
                            <img src="/Images/ico-delete.gif" />delete</span>
                    </td>
                    <td style="width: 50px">
                        <span class="edit_button" href="<%="/EditReceipt/" + receiptData.ReceiptNumber%>">
                            <img src="/Images/edit.gif" />
                            edit</span>
                    </td>
                    <td style="width: 30px">
                        <a href="<%="/PrintReceipt/" + receiptData.ReceiptNumber%>" target="_blank">Print</a>
                    </td>
                    <td style="width: 20px">
                        <a href="<%="/DownloadReceipt/" + receiptData.ReceiptNumber%>" target="_blank">Pdf</a>
                    </td>
                </tr>
                <%
                        index = index + 1;%>
                <%
                    }%>
                <tr id="noresultsrow">
                    <td colspan="10">
                        There is no result found your query.
                    </td>
                </tr>
                <%
                    var hasPrevious = (bool)ViewData["HasPrevious"];
                    var hasNext = (bool)ViewData["HasNext"];
                    var pageIndex = Convert.ToInt32(ViewData["pageIndex"]);
                    if (hasNext || hasPrevious)
                    {
                %>
                <tr class="footer">
                    <%
                        if (hasPrevious)
                        {%>
                    <td>
                        <a class="prevNavigation" pageindex="<%= pageIndex - 1%>" href="#">Prev</a>
                    </td>
                    <%
                        }
                        else
                        {%>
                    <td>
                    </td>
                    <%
                        }%>
                    <td colspan="8">
                    </td>
                    <%
                        if (hasNext)
                        {%>
                    <td>
                        <a class="nextNavigation" pageindex="<%= pageIndex + 1%>" href="#">Next</a>
                    </td>
                    <%
                        }
                        else
                        {%>
                    <td>
                    </td>
                    <%
                        }%>
                </tr>
                <%
                    }%>
            </table>
            </>
            <%
                }
                else
                {%>
            <p>
                Records not found.</p>
        </li>
        <%
                }%>
    </ul>
    <link href="/Content/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="/Content/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/JQValidation.js" type="text/javascript"></script>
    <script src="/Scripts/Search.js" type="text/javascript"></script>
    <script src="/Scripts/Reports.js" type="text/javascript"></script>
</asp:Content>