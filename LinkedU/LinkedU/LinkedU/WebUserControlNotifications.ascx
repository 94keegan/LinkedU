<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlNotifications.ascx.cs" Inherits="LinkedU.WebUserControlNotifications" %>

                     <li class="dropdown" runat="server" id="GlobalNotifications">
                         <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <span class="glyphicon-bell"></span>
                            <sup ID="GlobalNotificationCount" runat="server" style="color:red"></sup>
                        </a>
                        <ul ID="GlobalNotificationItems" class="dropdown-menu notify-drop" runat="server">
                        </ul>
                    </li>