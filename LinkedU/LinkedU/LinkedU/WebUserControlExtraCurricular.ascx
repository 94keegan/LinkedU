<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlExtraCurricular.ascx.cs" Inherits="LinkedU.ExtraCurricular" %>

    <tr>
        <td>
            <asp:DropDownList runat="server" ID="ectype">
                <%-- 
                    1	Student Government
                    2	Academic Team/Club
                    3	Internship
                    4	Culture Club
                    5	Volunteer/Community Service
                    6	Student Newspaper
                    7	Athletics
                    8	Arts
                 --%>
                <asp:ListItem Text="Student Government" Value="1"></asp:ListItem>
                <asp:ListItem Text="Academic Team/Club" Value="2"></asp:ListItem>
                <asp:ListItem Text="Internship" Value="3"></asp:ListItem>
                <asp:ListItem Text="Culture Club" Value="4"></asp:ListItem>
                <asp:ListItem Text="Volunteer/Community Service" Value="5"></asp:ListItem>
                <asp:ListItem Text="Student Newspaper" Value="6"></asp:ListItem>
                <asp:ListItem Text="Athletics" Value="7"></asp:ListItem>
                <asp:ListItem Text="Arts" Value="8"></asp:ListItem>
                <asp:ListItem Text="< Remove >" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="ecname" runat="server" Width="15em" />
        </td>
    </tr>