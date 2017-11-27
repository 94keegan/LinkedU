<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinkedU.Default" %>

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
    <link rel="stylesheet" type="text/css" href="css/lib/animate.css" media="screen, projection" />

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
            <img class="asset left-30 sp600 t120 z1" src="img/slides/scene1/macbook.png" />
            <div class="info">
                <h2>Beautiful theme for showcasing your works.</h2>
            </div>
        </article>
        <article class="slide" id="ideas" style="background: url('img/backgrounds/aqua.jpg') repeat-x top center;">
            <div class="info">
                <h2>We love to turn ideas into beautiful things.</h2>
            </div>
            <img class="asset left-480 sp600 t260 z1" src="img/slides/scene2/left.png" />
            <img class="asset left-210 sp600 t213 z2" src="img/slides/scene2/middle.png" />
            <img class="asset left60 sp600 t260 z1" src="img/slides/scene2/right.png" />
        </article>
        <article class="slide" id="tour" style="background: url('img/backgrounds/color-splash.jpg') repeat-x top center;">
            <img class="asset left-472 sp650 t210 z3" src="img/slides/scene3/ipad.png" />
            <img class="asset left-365 sp600 t270 z4" src="img/slides/scene3/iphone.png" />
            <img class="asset left-350 sp450 t135 z2" src="img/slides/scene3/desktop.png" />
            <img class="asset left-185 sp550 t220 z1" src="img/slides/scene3/macbook.png" />
            <div class="info">
                <h2>Fully Responsive theme</h2>
                <a href="features.html">TOUR THE PRODUCT</a>
            </div>
        </article>
        <article class="slide" id="responsive" style="background: url('img/backgrounds/indigo.jpg') repeat-x top center;">
            <img class="asset left-472 sp600 t120 z3" src="img/slides/scene4/html5.png" />
            <img class="asset left-190 sp500 t120 z2" src="img/slides/scene4/css3.png" />
            <div class="info">
                <h2>Responsive <strong>HTML5 & CSS3</strong>
                    Theme
                </h2>
            </div>
        </article>
    </section>

    <div id="showcase">
        <div class="container">
            <div class="section_header">
                <h3>Our Services</h3>
            </div>
            <div class="row feature_wrapper">
                <!-- Features Row -->
                <div class="features_op1_row">
                    <!-- Feature -->
                    <div class="col-sm-4 feature first">
                        <div class="img_box">
                            <a href="services.html">
                                <img src="img/service1.png" />
                                <span class="circle">
                                    <span class="plus">&#43;</span>
                                </span>
                            </a>
                        </div>
                        <div class="text">
                            <h6>Responsive theme</h6>
                            <p>
                                There are many variations of passages of generators on the  embarrassing hidden in   content here making it look like.
                            </p>
                        </div>
                    </div>
                    <!-- Feature -->
                    <div class="col-sm-4 feature">
                        <div class="img_box">
                            <a href="services.html">
                                <img src="img/service2.png" />
                                <span class="circle">
                                    <span class="plus">&#43;</span>
                                </span>
                            </a>
                        </div>
                        <div class="text">
                            <h6>Easy customization</h6>
                            <p>
                                There are many variations of passages of generators on the  embarrassing hidden in   content here making it look like.
                            </p>
                        </div>
                    </div>
                    <!-- Feature -->
                    <div class="col-sm-4 feature last">
                        <div class="img_box">
                            <a href="services.html">
                                <img src="img/service3.png" />
                                <span class="circle">
                                    <span class="plus">&#43;</span>
                                </span>
                            </a>
                        </div>
                        <div class="text">
                            <h6>Made with love</h6>
                            <p>
                                There are many variations of passages of generators on the  embarrassing hidden in   content here making it look like.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="features">
        <div class="container">
            <div class="section_header">
                <h3>Features</h3>
            </div>
            <div class="row feature">
                <div class="col-sm-6">
                    <img src="img/showcase1.png" class="img-responsive" />
                </div>
                <div class="col-sm-6 info">
                    <h3>
                        <img src="img/features-ico1.png" />
                        Beautiful on all devices
                    </h3>
                    <p>
                        There are many variations of passages of Lorem Ipsum available, but the randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text.
                    </p>
                </div>
            </div>
            <div class="row feature">
                <div class="col-sm-6 pic-right">
                    <img src="img/showcase2.png" class="pull-right img-responsive" />
                </div>
                <div class="col-sm-6 info info-left">
                    <h3>
                        <img src="img/features-ico2.png" />
                        Blog page included
                    </h3>
                    <p>
                        There are many variations of passages of Lorem Ipsum available, but the randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text.
                    </p>
                </div>
            </div>
            <div class="row feature">
                <div class="col-sm-6">
                    <img src="img/showcase3.png" class="img-responsive" />
                </div>
                <div class="col-sm-6 info">
                    <h3>
                        <img src="img/features-ico3.png" />
                        Simple and clean coming soon page
                    </h3>
                    <p>
                        There are many variations of passages of Lorem Ipsum available, but the randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text.
                    </p>
                </div>
            </div>
        </div>
    </div>


    <!-- Pricing Option -->
    <div id="in_pricing">
        <div class="container">
            <div class="section_header">
                <h3>Pricing</h3>
            </div>

            <div class="row charts_wrapp">
                <!-- Plan Box -->
                <div class="col-sm-4">
                    <div class="plan">
                        <div class="wrapper">
                            <h3>Lite</h3>
                            <div class="price">
                                <span class="dollar">$</span>
                                <span class="qty">35</span>
                                <span class="month">/month</span>
                            </div>
                            <div class="features">
                                <p>
                                    <strong>10</strong>
                                    Shared Projects
                                </p>
                                <p>
                                    <strong>4</strong>
                                    Team Members
                                </p>
                                <p>
                                    <strong>10</strong>
                                    Storage
                                </p>
                            </div>
                            <a class="order" href="pricing.html">ORDER NOW</a>
                        </div>
                    </div>
                </div>
                <!-- Plan Box -->
                <div class="col-sm-4 pro">
                    <div class="plan">
                        <div class="wrapper">
                            <img class="ribbon" src="img/badge.png" />
                            <h3>Pro</h3>
                            <div class="price">
                                <span class="dollar">$</span>
                                <span class="qty">99</span>
                                <span class="month">/month</span>
                            </div>
                            <div class="features">
                                <p>
                                    <strong>10</strong>
                                    Shared Projects
                                </p>
                                <p>
                                    <strong>25</strong>
                                    Team Members
                                </p>
                                <p>
                                    <strong>Unlimited</strong>
                                    Storage
                                </p>
                                <p>
                                    <strong>Plus </strong>
                                    Phone Support
                                </p>
                            </div>
                            <a class="order" href="pricing.html">ORDER NOW</a>
                        </div>
                    </div>
                </div>
                <!-- Plan Box -->
                <div class="col-sm-4 standar">
                    <div class="plan">
                        <div class="wrapper">
                            <h3>Standard</h3>
                            <div class="price">
                                <span class="dollar">$</span>
                                <span class="qty">65</span>
                                <span class="month">/month</span>
                            </div>
                            <div class="features">
                                <p>
                                    <strong>10</strong>
                                    Shared Projects
                                </p>
                                <p>
                                    <strong>4</strong>
                                    Team Members
                                </p>
                                <p>
                                    <strong>10</strong>
                                    Storage
                                </p>
                            </div>
                            <a class="order" href="pricing.html">ORDER NOW</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="start">
                <p>Start your free, no-risk, 14 day trial! </p>
                <a href="pricing.html">Start Now!</a>
            </div>
        </div>
    </div>

    <div id="clients">
        <div class="container">
            <div class="section_header">
                <h3>Clients</h3>
            </div>
            <div class="row">
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client1"></div>
                </div>
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client2"></div>
                </div>
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client3"></div>
                </div>
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client1"></div>
                </div>
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client2"></div>
                </div>
                <div class="col-md-2 col-sm-4 client">
                    <div class="img client3"></div>
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
