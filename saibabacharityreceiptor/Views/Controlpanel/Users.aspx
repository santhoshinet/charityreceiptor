<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<saibabacharityreceiptor.Models.RegisterModel>" %>

<%@ Import Namespace="saibabacharityreceiptor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Control panel - list of Users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="ul">
        <li>
            <h2>
                Manage Users</h2>
            <p>
                List of contributors.</p>
            <table id="Cart_Table">
                <tr class="header">
                    <td style="width: 60px">
                        Sno
                    </td>
                    <td style="width: 300px;">
                        Username
                    </td>
                    <td style="width: 400px">
                        Email
                    </td>
                    <td style="width: 250px">
                        Donation Receiver?
                    </td>
                    <td style="width: 80px">
                        Admin?
                    </td>
                    <td colspan="2" class="lastcol">
                        Actions
                    </td>
                </tr>
                <%
                    var users = (List<LocalUser>)ViewData["UserList"];
                    int index = 1;
                %>
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
            <h3>
                <input class="LnkAddnewuser" type="button" value="Add new user" href="/controlpanel/AddUser" />
            </h3>
        </li>
    </ul>
    <link href="/Content/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.highlight-3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.quicksearch.js" type="text/javascript"></script>
    <script src="/Scripts/Users.js" type="text/javascript"></script>
</asp:Content>