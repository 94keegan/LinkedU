﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="LinkedU.Admin" %>

<%@ Register Src="~/WebUserControlNotifications.ascx" TagName="GlobalNotifications" TagPrefix="gn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>LinkedU || Contact</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />

	<!-- Styles -->
	<link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
	<link rel="stylesheet" href="css/compiled/bootstrap-overrides.css" type="text/css" />
	<link rel="stylesheet" type="text/css" href="css/compiled/theme.css" />

	<link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css' />

	<link rel="stylesheet" href="css/compiled/contact.css" type="text/css" media="screen" />
	<link rel="stylesheet" type="text/css" href="css/lib/animate.css" media="screen, projection" />
    <link rel="stylesheet" href="css/LinkedU.css" type="text/css" media="screen" />

	<!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <style type="text/css">
        .auto-style1 {
            width: 323px;
        }
        .auto-style2 {
            width: 236px;
        }
        .auto-style3 {
            width: 323px;
            height: 42px;
        }
        .auto-style4 {
            width: 236px;
            height: 42px;
        }
        .auto-style5 {
            height: 42px;
        }
        .auto-style6 {
            width: 79%;
            margin-right: 541px;
        }
    </style>
</head>
<body>

	<form id="form1" runat="server">

	<div class="navbar navbar-inverse navbar-static-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="Default.aspx" class="navbar-brand"><strong>LinkedU</strong></a>
            </div>

            <div class="collapse navbar-collapse navbar-ex1-collapse" role="navigation">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="Default.aspx">HOME</a></li>
                    <!--Add more menus here above the Contact Us-->

                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">SEARCH<b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="UniversitySearch.aspx">Universities</a></li>
                                <%
                                if (Session["AccountType"] != null && Session["AccountType"].ToString() == "University")
                                    Response.Write("<li><a href=\"StudentSearch.aspx\">Students</a></li>");
                            %>
                        </ul>
                    </li>
                    <li class="active"><a href="Contact.aspx">CONTACT US</a></li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <%
                                if (Session["UserName"] != null)
                                {
                                    Response.Write(Session["UserName"]);
                                }
                                else
                                {
                                    Response.Write("Guest");
                                }
                            %>
                            <b class="caret"></b></a>
                        <%
                            if (Session["UserName"] != null)
                            {
                                if (Session["AccountType"].ToString() == "Student")
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"StudentProfile.aspx\">Edit Profile</a></li><li><a href=\"StudentLookup.aspx?id=" + Session["UserID"] + "\">View Profile</a></li>");
                                else if (Session["AccountType"].ToString() == "University")
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"UniversityProfile.aspx\">Edit Profile</a></li>");
                                else if (Session["AccountType"].ToString() == "Admin")
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Admin.aspx\">Administration</a></li>");

                                Response.Write("<li><a href=\"Logoff.aspx\">Logoff</a></li></ul>");
                            }
                            else
                            {
                                Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Sign-In.aspx\">Login</a></li><li><a href=\"Sign-Up.aspx\">Sign Up</a></li></ul>");
                            }
                        %>
                    </li>
                    <gn:GlobalNotifications ID="GlobalNotificationControl" runat="server" />
                </ul>
            </div>
        </div>
    </div>

    <div id="contact" style="margin-top:0;padding-top:70px;">
        <div class="container">
            <div class="section_header">
                <h3>Admin Page</h3>
            </div>
            <div class="row contact">
                <p>Enter the University you would like to mark for review below.</p>
                <br />
                <table class="auto-style6">
                    <tr>
                        <td class="auto-style3">Enter the school ID you wish to be marked for review</td>
                        <td class="auto-style4">
                            &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Text="Mark for Review" Value="1"></asp:ListItem>
                     <asp:ListItem Text="UnMark for Review" Value="2"></asp:ListItem>
                                <asp:ListItem Text="View all marked Universities" Value="3"></asp:ListItem>

                            </asp:DropDownList>

                        </td>
                        <td class="auto-style4">

                            <input class="name form-control" name="UniversityID" type="text" placeholder="UniversityID" style="width: 85%" /></td>
                        <td class="auto-style5">

                <asp:Button ID="btnRequest" runat="server" OnClick="btnRequest_Click" Text="Submit" CssClass="btn-primary" />

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">
                            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td>

                            <asp:Label ID="lblResult" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        
                        <td class="auto-style2">
                            &nbsp;</td>
                        
                        <td class="auto-style2">
                            &nbsp;</td>
                        <td>

                            &nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

	<!-- starts footer -->
    <footer id='footer'>
        <div class='container'>
            <div class='row info'>
                <div class='col-sm-6 residence'>
                    <ul>
                        <li>Illinois State University</li>
                        <li>Normal, IL</li>
                    </ul>
                </div>
                <div class='col-sm-5'>
                    <ul>
                        <li><strong>Contact</strong></li>
                        <li><a href='Contact.aspx'>Click Here to Send Us A Note</a></li>
                    </ul>
                </div>
            </div>
            <div class='row credits'>
                <div class='col-md-12'>
                    <div class='row copyright'>
                        <div class='col-md-12'>
                            2017 LinkedU. All rights reserved.<br />
                            Development &amp; Design: Team JKMZ
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </footer>

    <!--www.scrolltotop.com-->
    <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>
    <script type='text/javascript' src='http://arrow.scrolltotop.com/arrow66.js'></script>

    <!-- Scripts -->
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/theme.js"></script>
    <script type="text/javascript" src="js/index-slider.js"></script>
    </form>
</body>
</html>
