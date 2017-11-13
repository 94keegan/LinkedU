﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign-Up.aspx.cs" Inherits="LinkedU.Sign_Up" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkedU || Sign Up</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Styles -->
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/compiled/bootstrap-overrides.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/compiled/theme.css" />

    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css' />

    <link rel="stylesheet" href="css/compiled/sign-up.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/compiled/reset.css" type="text/css" media="screen" />
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

                    <li><a href="Contact.aspx">CONTACT US</a></li>
                    <li class="dropdown active">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">UserName <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Logoff</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div id="reset_pwd" class="reset_page">
        <div class="container">
            <div class="row">
                <div class="col-md-12 box_wrapper">
                    <div class="col-md-12">
                        <div class="box">
                            <div class="head">
                                <h1>Create your account</h1>
                                <h4>Enter your username, email address, password, and access code to create your account.</h4>
                                <div class="line"></div>
                            </div>
                            <div class="form">
                                <form runat="server" id="form">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="pnlAccountType" runat="server">
                                        <ContentTemplate>
                                            <% if (!string.IsNullOrWhiteSpace(SignupError)) Response.Write(string.Concat("<p style='color:red'>", SignupError, "</p>")); %>
                                            <asp:DropDownList ID="ddlAccountType" runat="server" class="control-form" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Student" />
                                                <asp:ListItem Text="University" />
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtUniversityName" runat="server" placeholder="University Name" class="control-form" Visible="false" />
                                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" class="control-form" required="required" />
                                            <asp:TextBox ID="txtLastname" runat="server" placeholder="Last Name" class="control-form" required="required" />
                                            <asp:TextBox ID="txtUserName" runat="server" placeholder="User Name" class="control-form" required="required" OnTextChanged="txtUserName_OnTextChanged" AutoPostBack="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:TextBox ID="txtQuestion" runat="server" placeholder="Security Question" class="control-form" required="required" />
                                    <asp:TextBox ID="txtAnswer" runat="server" placeholder="Security Answer" class="control-form" required="required" />
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class="control-form" required="required" />
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" class="control-form" required="required" />
                                    <asp:TextBox ID="txtConfPassword" runat="server" placeholder="Confirm Password" class="control-form" required="required" />
                                    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
                                </form>
                            </div>
                        </div>
                    </div>
                    <p class="already">Already have an account? <a href="Sign-In.aspx">Sign in</a></p>
                </div>
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
                            2017 CeMaST. All rights reserved.<br />
                            Development &amp; Design: ISU Spring 2017 IT363 Class
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
