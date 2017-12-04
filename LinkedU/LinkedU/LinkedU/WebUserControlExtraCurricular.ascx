<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlExtraCurricular.ascx.cs" Inherits="LinkedU.ExtraCurricular" %>

    <tr>
        <td>
            <asp:DropDownList runat="server" ID="ectype">
                <asp:ListItem Text="Student Government" Value="1"></asp:ListItem>
                <asp:ListItem Text="Academic Team/Club" Value="2"></asp:ListItem>
                <asp:ListItem Text="Internship" Value="3"></asp:ListItem>
                <asp:ListItem Text="Culture Club" Value="4"></asp:ListItem>
                <asp:ListItem Text="Volunteer/Community Service" Value="5"></asp:ListItem>
                <asp:ListItem Text="Student Newspaper" Value="6"></asp:ListItem>
                <asp:ListItem Text="Athletics" Value="7"></asp:ListItem>
                <asp:ListItem Text="Arts" Value="8"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="ecname" runat="server" Width="15em" />
        </td>
        <td>
            <asp:LinkButton CssClass="btn" CommandName="Delete" CommandArgument='<%# ecname.Text  %>' ID="ButtonDeleteExtraCurricular" runat="server">
                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>Delete
            </asp:LinkButton>
        </td>
    </tr>
