<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinkedU.Default" %>

<%@ Register Src="~/WebUserControlNotifications.ascx" TagName="GlobalNotifications" TagPrefix="gn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkedU</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Styles -->
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/compiled/bootstrap-overrides.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/compiled/theme.css" />

    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,900,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css' />

    <link rel="stylesheet" href="css/compiled/index.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/compiled/services.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="css/lib/animate.css" media="screen, projection" />
    <link rel="stylesheet" href="css/lib/flexslider.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/LinkedU.css" type="text/css" media="screen" />

    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>
<body class="pull_top">

    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle pull-right" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a href="Default.aspx" class="navbar-brand"><strong>LinkedU</strong></a>
            </div>

            <!--Begin Navigation template for other pages-->
            <div class="collapse navbar-collapse navbar-ex1-collapse" role="navigation">
                <ul class="nav navbar-nav navbar-right">
                    <li class="active"><a href="Default.aspx">HOME</a></li>
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
            <!--End Navigation template for other pages-->
        </div>
    </div>

    <section id="feature_slider" class="lol">
        <!--
            Each slide is composed by <img> and .info
            - .info's position is customized with css in index.css
            - each <img> parallax effect is declared by the following params inside its class:

            example: class="asset left-472 sp600 t120 z3"
            left-472 means left: -472px from the center
            sp600 is speed transition
            t120 is top to 120px
            z3 is z-index to 3
            Note: Maintain this order of params

            For the backgrounds, you can combine from the bgs folder :D
        -->
        <article class="slide" id="showcasing" style="background: url('img/backgrounds/landscape.png') repeat-x top center;">
            <img class="asset left-30 sp600 t120 z1" src="img/slides/scene2/linkedu.png" />
            <div class="info">
                <h2>LinkedU, A place for Students and Universities to Connect.</h2>
            </div>
        </article>
        <article class="slide" id="ideas" style="background: url('img/backgrounds/aqua.jpg') repeat-x top center;">
            <div class="info">
                <h2>Featured University of the Day</h2>
                <asp:HyperLink ID="HyperLinkFeaturedUniversity" runat="server" NavigateUrl="~/UniversityLookup.aspx?uid={0}" >
                    <asp:Image ID="ImageFeaturedUniversity" CssClass="asset left-210 sp600 t113 z2" runat="server" />
                </asp:HyperLink>
            </div>
        </article>
        <article class="slide" id="tour" style="background: url('img/backgrounds/color-splash.jpg') repeat-x top center;">
            <img class="asset left-350 sp450 t135 z2" src="img/slides/scene2/linkedu.png" />
            <div class="info">
                <h2>Want your University featured? </h2>
                <a href="PayPalCheckout.aspx" style="color: black">Click Here</a>
            </div>
        </article>
        <article class="slide" id="responsive" style="background: url('img/backgrounds/indigo.jpg') repeat-x top center;">
            <img class="asset left-472 sp600 t120 z3" src="img/slides/scene4/html5.png" />
            <img class="asset left-190 sp500 t120 z2" src="img/slides/scene4/css3.png" />
            <div class="info">
                <h2><strong>Linked U</strong>
                </h2>
            </div>
        </article>
    </section>

    <div id="process">
        <div class="container">
            <div class="section_header">
                <h3>Services</h3>
            </div>
            <div class="row services_circles">
                <div class="col-sm-4 description">
                    <div class="text active">
                        <h4>Search for students and universities.</h4>
                        <p>Students can search for universities closest to their home and get directions on how to get there. Universities can search to recruit new students and show just how great they are.</p>
                    </div>
                    <div class="text">
                        <h4>Apply for schools.</h4>
                        <p>After students have completed their profile, they can apply for their favorite universities with ease. Applying is only a click away!</p>
                    </div>
                    <div class="text">
                        <h4>Explore your options.</h4>
                        <p>Explore LinkedU and finding the right university for you. Get all the information you need to make your decision.</p>
                    </div>
                </div>

                <div class="col-sm-7 col-xs-12 areas">
                    <div class="circle active">
                        <img src="img/plan.png" />
                        <span>Search</span>
                    </div>
                    <div class="circle">
                        <img src="img/develop.png" />
                        <span>Apply</span>
                    </div>
                    <div class="circle last_circle">
                        <img src="img/design.png" />
                        <span>Explore</span>
                    </div>
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





