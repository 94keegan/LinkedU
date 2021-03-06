﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityLookup.aspx.cs" Inherits="LinkedU.UniversityLookup" %>

<%@ Register Src="~/WebUserControlNotifications.ascx" TagName="GlobalNotifications" TagPrefix="gn" %>

<%@ Import Namespace="LinkedU" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>LinkedU || University Details</title>
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
</head>
<body>

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
                    <li class=""><a href="Default.aspx">HOME</a></li>
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
                    <li><a href="Contact.aspx">CONTACT US</a></li>
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

    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class='col-lg-push-10'>
                        <asp:Panel runat="server">
                            <asp:Label CssClass="h1" ID="UniversityName" runat="server"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class='col-md-4'>
                        <asp:Image ID="UniversityLogo" runat="server" ImageUrl="~/img/Logo_Placeholder.png" style="width:100%" />
                        <asp:Panel ID="UniversityInformation" runat="server">
                        </asp:Panel>
                    </div>
                    <div class='col-md-5'>
                        <asp:Panel runat="server" HorizontalAlign="Right">
                            <asp:LinkButton runat="server" CssClass="btn btn-sm btn-default" PostBackUrl="~/RequestMoreInfo.aspx" Text="More Information">
                                <span aria-hidden="true" class="glyphicon glyphicon-envelope"></span>&nbsp;Request More Information
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-sm btn-default" OnClick="Apply_Click" Text="Apply for Admission">
                                <span aria-hidden="true" class="glyphicon glyphicon-send"></span>&nbsp;Apply for Admission
                            </asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel ID="UniversityMap" runat="server" CssClass="iframe-loading">
                        </asp:Panel>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
            </div>
        </div>
    </form>

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
                <div class='col-sm-5 touch'>
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
</body>
</html>

