<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="LinkedU.Reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkedU || Reset</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Styles -->
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/compiled/bootstrap-overrides.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/compiled/theme.css" />

    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css' />

    <link rel="stylesheet" href="css/compiled/reset.css" type="text/css" media="screen" />
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
                            <%
                                if (Session["AccountType"] != null && Session["AccountType"].ToString() == "University")
                                    Response.Write("<li><a href=\"StudentSearch.aspx\">Students</a></li>");
                            %>
                        </ul>
                    </li>
                    <li><a href="Contact.aspx">CONTACT US</a></li>
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
                                    if (Session["AccountType"].ToString() == "Student")
                                        Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"StudentProfile.aspx\">Edit Profile</a></li><li><a href=\"StudentLookup.aspx?id=" + Session["UserID"] + "\">View Profile</a></li>");
                                    else if (Session["AccountType"].ToString() == "University")
                                        Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"UniversityProfile.aspx\">Edit Profile</a></li>");
                                    else if (Session["AccountType"].ToString() == "Admin")
                                        Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Admin.aspx\">Administration</a></li>");

                                    Response.Write("<li><a href=\"Logoff.aspx\">Logoff</a></li></ul>");
                                }
                        <%
                            if (Session["UserName"] != null)
                            {
                                if (Session["AccountType"].ToString() == "Student")
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"StudentProfile.aspx\">Edit Profile</a></li><li><a href=\"StudentLookup.aspx?id=" + Session["UserID"] + "\">View Profile</a></li>");
                                else
                                    Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"UniversityProfile.aspx\">Profile</a></li>");

                                Response.Write("<li><a href=\"Logoff.aspx\">Logoff</a></li></ul>");
                            }
                            else
                            {
                                Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"Sign-In.aspx\">Login</a></li><li><a href=\"Sign-Up.aspx\">Sign Up</a></li></ul>");
                            }
                        %>
                    </li>
                    <li>
                        <a href="#" class="btn btn-sm">
                            <span class="glyphicon-bell"></span>
                            <asp:Label runat="server" ForeColor="Red"></asp:Label>
                        </a>
                        <ul class="dropdown-menu notify-drop">
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
                                <h1>Reset your Password</h1>
                                <h4>Enter your email address below to receive instructions on how to reset your password.</h4>
                                <div class="line"></div>
                            </div>
                            <div class="form">
                                <form runat="server" id="form">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="pnlReset" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" ID="lblAlert"></asp:Label><br />
                                            <br />
                                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class="control-form" />
                                            <div class="col-md-6 remember" id="RememberMe">
                                                <label class="checkbox">
                                                    <asp:CheckBox ID="chkPhone" runat="server" class="control-form" OnCheckedChanged="chkPhone_CheckedChanged" AutoPostBack="true" Text="Send SMS" />
                                                </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCarrier" runat="server" class="control-form" Visible="false" />
                                            <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone" class="control-form" Visible="false" />
                                            <asp:Label ID="lblQuestion" runat="server" Visible="false" Style="text-align: left" /><br />
                                            <asp:TextBox ID="txtAnswer" runat="server" placeholder="Answer" class="control-form" Visible="false" />
                                            <asp:TextBox ID="txtNewPassword" runat="server" placeholder="New Password" class="control-form" Visible="false" />
                                            <asp:TextBox ID="txtNewPasswordConfirm" runat="server" placeholder="New Password Confirm" class="control-form" Visible="false" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Reset Password" OnClick="btnSubmit_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </form>
                            </div>
                        </div>
                    </div>
                    <p class="already">Know your password? <a href="Sign-In.aspx">Sign in</a></p>
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
