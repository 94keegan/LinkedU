﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityNameExample.aspx.cs" Inherits="LinkedU.examples.UniversityNameExample" %>

<%@ Import Namespace="LinkedU" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>LinkedU || University Details</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />

	<!-- Styles -->
	<link href="/css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="/css/compiled/bootstrap-overrides.css" type="text/css" />
	<link rel="stylesheet" type="text/css" href="/css/compiled/theme.css" />

	<link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css' />

	<link rel="stylesheet" href="/css/compiled/contact.css" type="text/css" media="screen" />
	<link rel="stylesheet" type="text/css" href="/css/lib/animate.css" media="screen, projection" />
    <link rel="stylesheet" href="/css/LinkedU.css" type="text/css" media="screen" />

    <link href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />

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
                    <li><a href="/Default.aspx">HOME</a></li>
                    <!--Add more menus here above the Contact Us-->

                    <li><a href="/Contact.aspx">CONTACT US</a></li>
                    <li class="dropdown active">
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
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"#\">Logoff</a></li></ul>");
                                }
                                else
                                {
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Sign-In.aspx\">Login</a></li></ul>");
                                }
                            %>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
        <asp:TextBox ID="TextBoxUniversity" runat="server" placeholder="Select from list..."></asp:TextBox>
        <asp:TextBox ID="UniversityID" runat="server"></asp:TextBox>
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
                        <br />
                        <li><a href='Contact.aspx'>Click Here to Send Us A Note</a></li>
                    </ul>
                </div>
            </div>
            <div class='row credits'>
                <div class='col-md-12'>
                    <div class='row copyright'>
                        <div class='col-md-12'>
                            2017 CeMaST. All rights reserved.<br />
                            Development &amp; Design: ISU Spring 2017 IT363 Class
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </footer>

    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="http://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/theme.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#TextBoxUniversity").autocomplete({
                source: "/autocomplete/UniversityName.aspx",
                minlength: 3,
                select: function (event, ui) {
                    $("#UniversityID").val(ui.item.id);
                }
            });

            $("#TextBoxUniversity").keypress(function() {
                $("#UniversityID").val("");
            });

        });
    </script>
</body>
</html>


