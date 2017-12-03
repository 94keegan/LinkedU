<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleAppointment.aspx.cs" Inherits="LinkedU.ScheduleAppointment" %>

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
            height: 42px;
            width: 138px;
        }
        .auto-style7 {
            width: 138px;
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
                                        Response.Write("<ul class=\"dropdown-menu\"><li><a href=\"StudentProfile.aspx\">Profile</a></li>");
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
                </ul>
            </div>
        </div>
    </div>

    <div id="contact" style="margin-top:0;padding-top:70px;">
        <div class="container">
            <div class="section_header">
                <h3>Schedule an Appointment</h3>
            </div>
            <div class="row contact">
                <p>Please fill out all fields below. An email will be sent to the University you&#39;re interested in asking to schedule an appointment at your selected time. You will be cc&#39;d on that email, and if the University is available they will reply all with contact details. </p>
                <br />
                <table class="nav-justified">
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style4">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Enabled="true" Text="Select Meeting Type" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Phone" Value="1"></asp:ListItem>
                     <asp:ListItem Text="In Person" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style6">

                            <asp:DropDownList ID="DropDownList2" runat="server">
                               <asp:ListItem Enabled="true" Text="Select Time" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="8am to 9am" Value="1"></asp:ListItem>
                    <asp:ListItem Text="9am to 10am" Value="2"></asp:ListItem>
                                <asp:ListItem Text="10am to 11am" Value="3"></asp:ListItem>
                                <asp:ListItem Text="1pm to 2pm" Value="4"></asp:ListItem>
                                  <asp:ListItem Text="2pm to 3pm" Value="5"></asp:ListItem>
                                <asp:ListItem Text="3pm to 4pm" Value="6"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td class="auto-style5">

                            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Enter your name</td>
                        <td class="auto-style2">
                            <input class="name form-control" name="Name" type="text" placeholder="Name" required="required" /></td>
                        <td class="auto-style7">

                            &nbsp;</td>
                        <td>

                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Enter the email you would like to be contacted at</td>
                        
                        <td class="auto-style2">
                             &nbsp;<input class="mail form-control" name="email" type="text" placeholder="Email" required="required" />
                        <td class="auto-style7">

                            <br />
                            <br />
                        </td>
                        <td>

                <asp:Button ID="btnRequest" runat="server" OnClick="btnRequest_Click" Text="Schedule Appointment" CssClass="btn-primary" />

                        </td>
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
