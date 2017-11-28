<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversitySearch.aspx.cs" Inherits="LinkedU.UniversitySearch" %>

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
                    <li class=""><a href="Default.aspx">HOME</a></li>
                    <!--Add more menus here above the Contact Us-->

                    <li class="dropdown active">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">SEARCH<b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="UniversitySearch.aspx">Universities</a></li>
                            <li><a href="StudentSearch.aspx">Students</a></li>
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

    <form id="form1" runat="server">

        <h3>University Search</h3>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <p>
                        <asp:Label CssClass="search-label" Text="Name:" runat="server" />
                        <asp:TextBox ID="SearchName" runat="server" Width="400"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Label CssClass="search-label" Text="Degree offering:" runat="server" />
                        <asp:DropDownList ID="SearchHighestLevel" runat="server" Width="400">
                            <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="1">Award of less than one academic year</asp:ListItem>
                            <asp:ListItem Value="2">At least 1, but less than 2 academic yrs</asp:ListItem>
                            <asp:ListItem Value="3">Associate's degree</asp:ListItem>
                            <asp:ListItem Value="4">At least 2, but less than 4 academic yrs</asp:ListItem>
                            <asp:ListItem Value="5">Bachelor's degree</asp:ListItem>
                            <asp:ListItem Value="6">Postbaccalaureate certificate</asp:ListItem>
                            <asp:ListItem Value="7">Master's degree</asp:ListItem>
                            <asp:ListItem Value="8">Post-master's certificate</asp:ListItem>
                            <asp:ListItem Value="9">Doctor's degree</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
<%--                        <asp:Label CssClass="search-label" Text="Region:" runat="server" />
                        <asp:DropDownList ID="SearchRegion" runat="server" Width="150">
                            <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="1">New England</asp:ListItem>
                            <asp:ListItem Value="2">Mid East</asp:ListItem>
                            <asp:ListItem Value="3">Great Lakes</asp:ListItem>
                            <asp:ListItem Value="4">Plains</asp:ListItem>
                            <asp:ListItem Value="5">Southeast</asp:ListItem>
                            <asp:ListItem Value="6">Southwest</asp:ListItem>
                            <asp:ListItem Value="7">Rocky Mountains</asp:ListItem>
                            <asp:ListItem Value="8">Far West</asp:ListItem>
                            <asp:ListItem Value="9">Outlying</asp:ListItem>
                            <asp:ListItem Value="0">US Service Schools</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label CssClass="search-label" Text="State:" runat="server" />
                        <asp:DropDownList ID="SearchState" runat="server" Width="60">
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label CssClass="search-label" Text="City:" runat="server" />
                        <asp:TextBox ID="SearchCity" runat="server" Width="200"></asp:TextBox>--%>
                        <asp:Label CssClass="search-label" Text="Within " runat="server"></asp:Label>
                        <asp:TextBox ID="TextBoxSearchRadius" TextMode="Number" runat="server" Text="50" Width="5em"></asp:TextBox>
                        <asp:Label CssClass="search-label" Text=" miles of " runat="server"></asp:Label>
                        <asp:DropDownList ID="DropDownRadiusCenter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownRadiusCenter_SelectedIndexChanged">
                            <asp:ListItem Text="My Home" Value="me" Enabled="false"></asp:ListItem>
                            <asp:ListItem Text="Address" Value="addr"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBoxSearchCenter" runat="server" Width="20em"></asp:TextBox>
                    </p>
                    <asp:Button Text="Search" OnClick="SearchValue_TextChanged" runat="server" CssClass="btn-sm" />
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <asp:Image ID="UpdateInProgress" AlternateText="loading..." ImageUrl="~/img/ajax-loader.gif" runat="server" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div class="container-fluid">
                    <asp:Table ID="ResultTable" Caption="Results" runat="server" CssClass="table" Visible="false">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>School Name</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Address</asp:TableHeaderCell>
                            <asp:TableHeaderCell>City</asp:TableHeaderCell>
                            <asp:TableHeaderCell>State</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Zip Code</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Distance</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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

