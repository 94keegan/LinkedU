<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityProfile.aspx.cs" Inherits="LinkedU.UniversityProfile" %>

<%@ Import Namespace="LinkedU" %>

<%@ Register src="~/WebUserControlUploadedMedia.ascx" tagname="UploadMedia" tagprefix="um" %>
<%@ Register Src="~/WebUserControlHighlightedPrograms.ascx" TagName="HighlightedProgram" TagPrefix="hp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkedU || University Profile</title>
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

    <form id="form1" runat="server">
        <h2>Edit Profile</h2>
        <asp:Wizard ID="Wizard1" runat="server" StepStyle-Wrap="False" StepStyle-VerticalAlign="Top" SideBarStyle-VerticalAlign="Top" CancelDestinationPageUrl="~/Default.aspx" CellPadding="5" CellSpacing="5" DisplayCancelButton="True" ActiveStepIndex="0" ValidateRequestMode="Enabled" OnNextButtonClick="Wizard1_NextButtonClick" OnFinishButtonClick="Wizard1_FinishButtonClick" NavigationStyle-HorizontalAlign="Left">
            <CancelButtonStyle CssClass="btn btn-sm" />
            <FinishCompleteButtonStyle CssClass="btn btn-sm" />
            <FinishPreviousButtonStyle CssClass="btn btn-sm" />
            <NavigationStyle HorizontalAlign="Left"></NavigationStyle>
            <StartNextButtonStyle CssClass="btn btn-sm" />
            <StepNextButtonStyle CssClass="btn btn-sm" />
            <StepPreviousButtonStyle CssClass="btn btn-sm" />
            <SideBarStyle VerticalAlign="Top" Wrap="True" HorizontalAlign="Left"></SideBarStyle>
            <StepStyle Wrap="True" Font-Bold="False"></StepStyle>
            <WizardSteps>
                <asp:WizardStep ID="WizardStepHighlightedPrograms" runat="server" Title="Highlighted Programs">
                    <asp:Button ID="ButtonAddProgram" runat="server" CssClass="btn" OnClick="ButtonAddProgram_Click" Text="Add Program" />
                    <table id="TableHighlightedPrograms" class="table">
                        <tr>
                            <th>Category</th>
                            <th>Name</th>
                            <th>URL</th>
                            <th>Action</th>
                        </tr>
                        <asp:Repeater runat="server" ID="RepeaterHighlightedPrograms" OnItemCommand="RepeaterHighlightedPrograms_ItemCommand">
                            <ItemTemplate>
                                <hp:HighlightedProgram ID="highlightedProgram" runat="server" Data="<%# Container.DataItem %>" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepUploadFiles" runat="server" Title="Upload Media">
                    <asp:DropDownList ID="DropDownListMediaType" runat="server">
                        <asp:ListItem Text="Logo"></asp:ListItem>
                        <asp:ListItem Text="Brochure"></asp:ListItem>
                        <asp:ListItem Text="Campus Map"></asp:ListItem>
                        <asp:ListItem Text="Image"></asp:ListItem>
                        <asp:ListItem Text="Video"></asp:ListItem>
                        <asp:ListItem Text="Audio"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:FileUpload ID="FileUploadMedia" runat="server" Style="display: inline-block" accept="image/png" />
                    <asp:LinkButton ID="LinkButtonUploadMedia" runat="server" OnClick="LinkButtonUploadMedia_Click" CssClass="btn btn-primary">
                        <span aria-hidden="true" class="glyphicon glyphicon-upload"></span>Upload
                    </asp:LinkButton>

                    <table id="TableUploadedMedia" class="table">
                        <tr>
                            <th>Type</th>
                            <th>Name</th>
                            <th>Action</th>
                        </tr>
                        <asp:Repeater runat="server" ID="RepeaterUploadedMedia" OnItemCommand="RepeaterUploadedMedia_ItemCommand">
                            <ItemTemplate>
                                <um:UploadMedia ID="uploadedMedia" runat="server" Data="<%# Container.DataItem %>"></um:UploadMedia>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>

                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepNewsletter" runat="server" Title="Stay Current">
                    <h4>Newletter</h4>
                    <asp:CheckBox ID="CheckBoxNewsletter" Checked="true" Text="&nbsp;&nbsp;Keep me up-to-date on everything going on at LinkedU with weekly newsletters" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepSummary" runat="server" Title="Summary">
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryPrograms">
                        <h4>Highlighted Programs</h4>
                        <asp:Table runat="server" CssClass="table" ID="SummaryPrograms">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Category</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                                <asp:TableHeaderCell>URL</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:Panel>
                    <h4>Newsletter</h4>
                    <asp:Panel runat="server" ID="PanelSummaryNewsletter">
                        <asp:Label runat="server" Text="Opt-in" />
                        <asp:Label runat="server" ID="SummaryNewsletter" />
                    </asp:Panel>
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

    <script type="text/javascript">
        $("#Wizard1_DropDownListMediaType").on("change", function () {
            switch ($(this).val()) {
                case "Logo":
                    $("#Wizard1_FileUploadMedia").prop("accept", "image/png");
                    break;
                case "Image":
                    $("#Wizard1_FileUploadMedia").prop("accept", "image/*");
                    break;
                case "Video":
                    $("#Wizard1_FileUploadMedia").prop("accept", "video/*");
                    break;
                case "Audio":
                    $("#Wizard1_FileUploadMedia").prop("accept", "audio/*");
                    break;
                default:
                    $("#Wizard1_FileUploadMedia").prop("accept", "application/pdf, .doc, .docx");


            }
        });
    </script>
</body>
</html>
