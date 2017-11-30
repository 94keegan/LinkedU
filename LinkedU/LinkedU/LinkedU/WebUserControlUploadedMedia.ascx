<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlUploadedMedia.ascx.cs" Inherits="LinkedU.UploadMedia" %>


    <tr>
        <td>
            <asp:Label ID="LabelMediaType" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="LabelMediaName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:ImageButton CommandName="Delete" CommandArgument='<%# LabelMediaName.Text  %>' ID="ButtonDeleteMedia" runat="server"></asp:ImageButton>
        </td>
    </tr>