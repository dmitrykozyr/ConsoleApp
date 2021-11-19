<!-- Созданный мной User Control -->
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebFormUserControl_Contact.ascx.cs" Inherits="WebForms.UserControls.WebFormUserControl_Contact" %>
<table>
    <tr>
        <td><strong>Name </strong></td>
        <td><asp:TextBox runat="server" ID="txtName"/></td>
    </tr>
    <tr>
        <td><strong>Message </strong></td>
        <td><asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine"/></td>
    </tr>
</table>