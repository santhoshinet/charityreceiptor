<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.RegisterModel>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Control panel - list of Users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContent">
        <h3>
            List of products.</h3>
        <div class="searchbox">
            Search
            <input type="text" id="txtsearchcriteria" />
        </div>
        <table id="Cart_Table">
            <thead>
                <tr>
                    <th style="width: 60px">
                        Sno
                    </th>
                    <th style="width: 150px;">
                        Username
                    </th>
                    <th style="width: 400px">
                        Email
                    </th>
                    <th style="width: 450px">
                        Donation Receiver?
                    </th>
                    <th style="width: 150px">
                        Admin?
                    </th>
                    <th colspan="2">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="empty-row">
                    <td colspan="7">
                        <%
                            var users = (List<LocalUser>)ViewData["UserList"];
                            int index = 1;
                        %>
                        <table>
                            <% foreach (var user in users)
                               {
                            %>
                            <tr id="<%= user.Id %>">
                                <td>
                                    <%= index %>
                                </td>
                                <td>
                                    <%= user.Username %>
                                </td>
                                <td>
                                    <%= user.Email %>
                                </td>
                                <td>
                                    <% if (user.IsheDonationReceiver)
                                       {%>
                                    Yes
                                    <%
                                       }
                                       else
                                       {%>
                                    No
                                    <%
                                       }%>
                                </td>
                                <td>
                                    <% if (user.IsheAdmin)
                                       {%>
                                    Yes
                                    <%
                                       }
                                       else
                                       {%>
                                    No
                                    <%
                                       }%>
                                </td>
                                <td style="width: 150px">
                                    <span class="delete_button">
                                        <img src="/Images/ico-delete.gif" />
                                        delete</span>
                                </td>
                                <td style="width: 100px">
                                    <span class="edit_button" href="<%= "/Controlpanel/edituser/" + user.Id %>">
                                        <img src="/Images/edit.gif" />
                                        edit</span>
                                </td>
                            </tr>
                            <%
                                   index = index + 1;%>
                            <%
                               } %>
                            <tr id="noresultsrow">
                                <td colspan="6">
                                    There is no result found your query.
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <h3>
            <a href="/controlpanel/AddUser" class="LnkAddnewuser">Add new user </a>
        </h3>
    </div>
    <link href="/Content/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="/Content/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>
    <script src="/Scripts/Users.js" type="text/javascript"></script>
</asp:Content>