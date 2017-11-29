<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="LinkedU.StudentProfile" %>

<%@ Import Namespace="LinkedU" %>

<%@ Register src="WebUserControlExtraCurricular.ascx" tagname="ExtraCurricular" tagprefix="ec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkedU || Student Profile</title>
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
                <asp:WizardStep ID="WizardStepBasicInformation" runat="server" Title="Demographics">
                    <asp:ValidationSummary ID="ValidationSummaryDemographics" runat="server" ValidationGroup="Demographics" HeaderText="Correct the following before continuing:" />
                    <asp:Panel runat="server">
                        <h4>Age</h4>
                        <asp:TextBox ID="TextBoxAge" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAge" runat="server" ControlToValidate="TextBoxAge" Display="None" ErrorMessage="Age cannot be blank" ValidationGroup="Demographics"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorGender" runat="server" ControlToValidate="RadioButtonGender" Display="None" ErrorMessage="Gender must be specified" ValidationGroup="Demographics"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRace" runat="server" ControlToValidate="RadioButtonRace" Display="None" ErrorMessage="Race/Ethnicity must be specified" ValidationGroup="Demographics"></asp:RequiredFieldValidator>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="floatleft">
                        <h4>Gender</h4>
                        <asp:RadioButtonList runat="server" ID="RadioButtonGender">
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                            <asp:ListItem Text="Non-binary" Value="O"></asp:ListItem>
                            <asp:ListItem Text="Prefer not to say" Value="X"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="floatleft">
                        <h4>Race/Ethnicity</h4>
                        <asp:RadioButtonList runat="server" ID="RadioButtonRace">
                            <asp:ListItem Text="African American/Black" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Asian/Pacific Islander" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Hispanic/Latino" Value="H"></asp:ListItem>
                            <asp:ListItem Text="Multiracial" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Native American/American Indian" Value="I"></asp:ListItem>
                            <asp:ListItem Text="White" Value="W"></asp:ListItem>
                            <asp:ListItem Text="Not Listed" Value="O"></asp:ListItem>
                            <asp:ListItem Text="Prefer not to say" Value="X"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepAddress" runat="server" Title="Address">
                    <asp:ValidationSummary ID="ValidationSummaryAddress" runat="server" ValidationGroup="Address" HeaderText="Correct the following before continuing:" />
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelAddress1" runat="server" Text="Street" />
                        <asp:TextBox ID="TextBoxAddress1" runat="server" AutoCompleteType="HomeStreetAddress" Width="17em" OnTextChanged="TextBoxAddress1_TextChanged"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelAddress2" runat="server" Text="Apt/PO" />
                        <asp:TextBox ID="TextBoxAddress2" runat="server" Width="17em" OnTextChanged="TextBoxAddress1_TextChanged"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelCity" runat="server" Text="City/State" />
                        <asp:TextBox ID="TextBoxCity" runat="server" AutoCompleteType="HomeCity" Width="12em" OnTextChanged="TextBoxAddress1_TextChanged"></asp:TextBox>
                        <asp:TextBox ID="TextBoxState" runat="server" AutoCompleteType="HomeState" Width="4em" OnTextChanged="TextBoxAddress1_TextChanged"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelZipCode" runat="server" Text="Zip" />
                        <asp:TextBox ID="TextBoxZipCode" runat="server" AutoCompleteType="HomeZipCode" OnTextChanged="TextBoxAddress1_TextChanged"></asp:TextBox>
                    </asp:Panel>
                    <asp:HiddenField ID="HiddenFieldAddressLatitude" runat="server" />
                    <asp:HiddenField ID="HiddenFieldAddressLongitude" runat="server" />
                    <asp:HiddenField ID="HiddenFieldAddressFormatted" runat="server" />
                    <asp:Panel runat="server">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress1" runat="server" ControlToValidate="TextBoxAddress1" ErrorMessage="Street address cannot be blank" ValidationGroup="Address" Visible="True" Display="None"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ControlToValidate="TextBoxCity" ErrorMessage="City cannot be blank" ValidationGroup="Address" Visible="True" Display="None"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorState" runat="server" ControlToValidate="TextBoxState" ErrorMessage="State cannot be blank" ValidationGroup="Address" Visible="True" Display="None"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorZip" runat="server" ControlToValidate="TextBoxZipCode" ErrorMessage="Zip code cannot be blank" ValidationGroup="Address" Visible="True" Display="None"></asp:RequiredFieldValidator>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepEducation" runat="server" Title="Education">
                    <asp:ValidationSummary ID="ValidationSummaryEducation" runat="server" ValidationGroup="Education" HeaderText="Correct the following before continuing:" />
                    <asp:Panel runat="server">
                        <h5>GPA</h5>
                        <asp:TextBox ID="TextBoxGpa" runat="server" Width="4em"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorGpa" runat="server" ErrorMessage="GPA is outside of the valid range" MinimumValue="0" MaximumValue="5.0" Type="Double" ValidationGroup="Education" Display="None" ControlToValidate="TextBoxGpa"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <h5>High School</h5>
                        <asp:TextBox ID="TextBoxHighSchool" runat="server" Width="15em"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <h5>Graduation Year</h5>
                        <asp:TextBox ID="TextBoxGraduationYear" runat="server" Width="5em" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorGraduationYear" runat="server" ErrorMessage="Graduation Year specified is not valid" MinimumValue="1900" MaximumValue="2020" Type="Integer" ValidationGroup="Education" Display="None" ControlToValidate="TextBoxGraduationYear"></asp:RangeValidator>
                    </asp:Panel>
                    </asp:WizardStep>
                <asp:WizardStep ID="WizardStepTestScores" runat="server" Title="Test Scores">
                    <asp:ValidationSummary ID="ValidationSummaryTestSCores" runat="server" ValidationGroup="TestScores" HeaderText="Correct the following before continuing:" />
                    <h5>Undergraduate</h5>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelActScore" runat="server" Text="ACT" />
                        <asp:TextBox ID="TextBoxActScore" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorAct" runat="server" ErrorMessage="ACT score is outside of the valid range (1 - 36)" MinimumValue="1" MaximumValue="36" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxActScore"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelSatScore" runat="server" Text="SAT" />
                        <asp:TextBox ID="TextBoxSatScore" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorSat" runat="server" ErrorMessage="SAT score is outside of the valid range (400 - 1600)" MinimumValue="400" MaximumValue="1600" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxSatScore"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelPsat" runat="server" Text="PSAT" />
                        <asp:TextBox ID="TextBoxPsat" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorPsat" runat="server" ErrorMessage="PSAT score is outside of the valid range (320 - 1520)" MinimumValue="320" MaximumValue="1520" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxPsat"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelPsatNmsqt" runat="server" Text="PSAT/NSMQT" />
                        <asp:TextBox ID="TextBoxPsatNmsqt" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorPsatNmsqt" runat="server" ErrorMessage="PSAT/NSMQT score is outside of the valid range (320 - 1520)" MinimumValue="320" MaximumValue="1520" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxPsatNmsqt"></asp:RangeValidator>
                    </asp:Panel>
                    <h5>GRE</h5>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelGreVerbal" runat="server" Text="Verbal" />
                        <asp:TextBox ID="TextBoxGreVerbal" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorGreVerbal" runat="server" ErrorMessage="GRE Verbal score is outside of the valid range (130 - 170)" MinimumValue="130" MaximumValue="170" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxGreVerbal"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelGreQuantitative" runat="server" Text="Quantitative" />
                        <asp:TextBox ID="TextBoxGreQuantitative" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorGreQuantitative" runat="server" ErrorMessage="GRE Quantitative score is outside of the valid range (130 - 170)" MinimumValue="130" MaximumValue="170" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxGreQuantitative"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelGreWritten" runat="server" Text="Written" />
                        <asp:TextBox ID="TextBoxGreWritten" runat="server" TextMode="Number" step="0.5"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorGreWritten" runat="server" ErrorMessage="GRE Written score is outside of the valid range (0 - 6)" MinimumValue="0" MaximumValue="6" Type="Double" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxGreWritten"></asp:RangeValidator>
                    </asp:Panel>
                    <h5>Other Graduate</h5>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelLsat" runat="server" Text="LSAT" />
                        <asp:TextBox ID="TextBoxLsat" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorLsat" runat="server" ErrorMessage="LSAT score is outside of the valid range (120 - 180)" MinimumValue="120" MaximumValue="180" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxLsat"></asp:RangeValidator>
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label ID="LabelMcat" runat="server" Text="MCAT" />
                        <asp:TextBox ID="TextBoxMcat" runat="server" TextMode="Number"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidatorMcat" runat="server" ErrorMessage="MCAT score is outside of the valid range (472 - 528)" MinimumValue="472" MaximumValue="528" Type="Integer" ValidationGroup="TestScores" Display="None" ControlToValidate="TextBoxMcat"></asp:RangeValidator>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepExtraCurriculars" runat="server" Title="Extra Curriculars">
                    <asp:Button ID="ButtonAddExtraCurricular" runat="server" CssClass="btn" OnClick="ButtonAddExtraCurricular_Click" Text="Add Extra Curricular" />
                    <table id="TableExtraCurriculars" class="table">
                        <tr>
                            <th>Type</th>
                            <th>Name</th>
                        </tr>
                        <asp:Repeater runat="server" ID="ExtraCurriculars">
                        <ItemTemplate>
                            <ec:ExtraCurricular ID="extraCurricular" runat="server" Data="<%# Container.DataItem %>"></ec:ExtraCurricular>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepNewsletter" runat="server" Title="Stay Current">
                    <h4>Newletter</h4>
                    <asp:CheckBox ID="CheckBoxNewsletter" Checked="true" Text="&nbsp;&nbsp;Keep me up-to-date on everything going on at LinkedU with weekly newsletters" runat="server"/>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStepSummary" runat="server" Title="Summary">
                    <h4>Demographics</h4>
                    <asp:Panel runat="server">
                        <asp:Label runat="server" Text="Age" />
                        <asp:Label runat="server" ID="SummaryAge" />
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label runat="server" Text="Gender" />
                        <asp:Label runat="server" ID="SummaryGender" />
                    </asp:Panel>
                    <asp:Panel runat="server">
                        <asp:Label runat="server" Text="Race/Ethnicity" />
                        <asp:Label runat="server" ID="SummaryRace" />
                    </asp:Panel>
                    <h4>Address</h4>
                    <asp:Panel runat="server">
                        <asp:Label runat="server" Text="Address" />
                        <asp:Label runat="server" ID="SummaryAddress" />
                    </asp:Panel>
                    <h4>Education</h4>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryGpa">
                        <asp:Label runat="server" Text="GPA" />
                        <asp:Label runat="server" ID="SummaryGpa" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryHighSchool">
                        <asp:Label runat="server" Text="High School" />
                        <asp:Label runat="server" ID="SummaryHighSchool" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryGraduationYear">
                        <asp:Label runat="server" Text="Graduation Year" />
                        <asp:Label runat="server" ID="SummaryGraduationYear" />
                    </asp:Panel>
                    <h4>Test Scores</h4>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryAct">
                        <asp:Label runat="server" Text="ACT" />
                        <asp:Label runat="server" ID="SummaryAct" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummarySat">
                        <asp:Label runat="server" Text="SAT" />
                        <asp:Label runat="server" ID="SummarySat" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryPsat">
                        <asp:Label runat="server" Text="PSAT" />
                        <asp:Label runat="server" ID="SummaryPsat" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryPsatnsmqt">
                        <asp:Label runat="server" Text="PSAT/NSMQT" />
                        <asp:Label runat="server" ID="SummaryPsatnsmqt" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryGreV">
                        <asp:Label runat="server" Text="GRE Verbal" />
                        <asp:Label runat="server" ID="SummaryGreV" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryGreQ">
                        <asp:Label runat="server" Text="GRE Quantitative" />
                        <asp:Label runat="server" ID="SummaryGreQ" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryGreW">
                        <asp:Label runat="server" Text="GRE Written" />
                        <asp:Label runat="server" ID="SummaryGreW" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryLsat">
                        <asp:Label runat="server" Text="LSAT" />
                        <asp:Label runat="server" ID="SummaryLsat" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryMcat">
                        <asp:Label runat="server" Text="MCAT" />
                        <asp:Label runat="server" ID="SummaryMcat" />
                    </asp:Panel>
                    <asp:Panel runat="server" Visible="false" ID="PanelSummaryExtraCurriculars">
                        <h4>Extra Curriculars</h4>
                        <asp:Table runat="server" CssClass="table" ID="SummaryExtraCurriculars">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Type</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
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
</body>
</html>

