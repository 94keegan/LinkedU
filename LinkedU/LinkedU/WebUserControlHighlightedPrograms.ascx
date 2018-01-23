<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlHighlightedPrograms.ascx.cs" Inherits="LinkedU.HighlightedProgram" %>

    <tr>
        <td>
            <asp:DropDownList runat="server" ID="ProgramType">
                <asp:ListItem Text="Undergraduate"></asp:ListItem>
                <asp:ListItem Text="Graduate"></asp:ListItem>
                <asp:ListItem Text="Athletic"></asp:ListItem>
                <asp:ListItem Text="Community"></asp:ListItem>
                <asp:ListItem Text="Employment"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="ProgramName" runat="server" Width="15em" />
        </td>
        <td>
            <asp:TextBox ID="ProgramURL" runat="server" Width="15em" />
        </td>
        <td>
            <asp:LinkButton CssClass="btn" CommandName="Delete" CommandArgument='<%# ProgramName.Text  %>' ID="ButtonDeleteProgram" runat="server">
                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>Delete
            </asp:LinkButton>
        </td>
    </tr>
