﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="LinkedU.Contact" %>

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
                    <li><a href="Default.aspx">HOME</a></li>
                    <!--Add more menus here above the Contact Us-->

                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">SEARCH<b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="UniversitySearch.aspx">Universities</a></li>
                            <li><a href="StudentSearch.aspx">Students</a></li>
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
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Profile.aspx\">Profile</a></li><li><a href=\"Logoff.aspx\">Logoff</a></li></ul>");
                                }
                                else
                                {
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Sign-In.aspx\">Login</a></li><li><a href=\"Sign-Up.aspx\">Sign Up</a></li></ul>");
                                }
                            %>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div id="contact">
        <div class="container">
            <div class="section_header">
                <h3>Get in touch</h3>
            </div>
            <div class="row contact">
                <p>What you think is important to us, and we've got you covered 110%. Our team will review your message and reply back as soon as possible.</p>
                <asp:Label ID="lblAlert" runat="server" Visible="false" /><br />
                <form id="form1" runat="server">
                    <div class="row form">
                        <div class="col-sm-6 row-col">
                            <div class="box">
                                <asp:TextBox ID="txtName" runat="server" class="name form-control" placeholder="Name" required="required" />
                                <asp:TextBox ID="txtEmail" runat="server" class="mail form-control" placeholder="Email" required="required" />
                            </div>
                        </div>
                        <div class="col-sm-6 row-col">
                            <div class="box">
                                <asp:TextBox id="txtMessage" TextMode="multiline" style="resize:none;" Columns="50" Rows="5" runat="server" class="form-control" required="required" placeholder="Type a message here..." />
                            </div>
                        </div>
                    </div>

                    <div class="row submit">
                        <div class="col-md-3 right">
                            <br/>
                            <asp:Button ID="btnSubmit" runat="server" Text="Send your message" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </form>
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

    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/theme.js"></script>
</body>
</html>
