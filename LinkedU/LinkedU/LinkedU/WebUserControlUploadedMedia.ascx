<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlUploadedMedia.ascx.cs" Inherits="LinkedU.UploadMedia" %>


    <tr>
        <td>
            <asp:HiddenField ID="MediaID" runat="server"  />
            <asp:Label ID="LabelMediaType" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="LabelMediaName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:LinkButton CssClass="btn" CommandName="Delete" CommandArgument='<%# MediaID.Value  %>' ID="ButtonDeleteMedia" runat="server">
                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>Delete
            </asp:LinkButton>
        </td>
    </tr>