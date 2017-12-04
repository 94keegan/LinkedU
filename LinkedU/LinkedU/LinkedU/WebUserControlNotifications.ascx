<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlNotifications.ascx.cs" Inherits="LinkedU.WebUserControlNotifications" %>

                     <li class="dropdown" runat="server" id="GlobalNotifications">
                         <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <span class="glyphicon-bell"></span>
                            <asp:Label ID="GlobalNotificationCount" runat="server" ForeColor="Red"></asp:Label>
                        </a>
                        <ul ID="GlobalNotificationItems" class="dropdown-menu notify-drop" runat="server">
                        </ul>
                    </li>