<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateStudentProfile.aspx.cs" Inherits="LinkedU.CreateStudentProfile" %>

<%@ Import Namespace="LinkedU" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>LinkedU || University Search</title>
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
        <asp:Wizard ID="Wizard1" runat="server">
            <WizardSteps>
                <asp:WizardStep ID="WizardStepBasicInformation" runat="server" Title="Basic Information">
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelAddress1" runat="server" Text="Street" />
                        <asp:TextBox ID="TextBoxAddress1" runat="server" AutoCompleteType="HomeStreetAddress"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelAddress2" runat="server" Text="Apt/PO" />
                        <asp:TextBox ID="TextBoxAddress2" runat="server"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelCity" runat="server" Text="City" />
                        <asp:TextBox ID="TextBoxCity" runat="server" AutoCompleteType="HomeCity"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelState" runat="server" Text="State" />
                        <asp:TextBox ID="TextBoxState" runat="server" AutoCompleteType="HomeState"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelZipCode" runat="server" Text="Zip" />
                        <asp:TextBox ID="TextBoxZipCode" runat="server" AutoCompleteType="HomeZipCode"></asp:TextBox>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepTestScores" runat="server" Title="Test Scores">
                    <asp:LinkButton ID="ButtonAddTestScore" Text="Add Test Score" runat="server" OnClick="ButtonAddTestScore_Click" CssClass="btn btn-sm">
                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                    </asp:LinkButton>

                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
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
                            2017 CeMaST. All rights reserved.<br>
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

