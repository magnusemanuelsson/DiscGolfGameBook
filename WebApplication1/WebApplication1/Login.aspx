<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 137px;
            text-align: left;
        }
        .auto-style3 {
            width: 137px;
            height: 30px;
            text-align: left;
        }
        .auto-style4 {
            height: 30px;
            text-align: left;
        }
        .auto-style5 {
            text-align: center;
            font-size: xx-large;
        }
        .auto-style6 {
            height: 30px;
            width: 88px;
            text-align: left;
        }
        .auto-style7 {
            width: 88px;
            text-align: left;
        }
        .auto-style11 {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style5">
                <strong>Log in</strong></div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">Username</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="TextBoxAnvändarnamn" runat="server" OnTextChanged="TextBox1_TextChanged" Width="153px"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxAnvändarnamn" ErrorMessage="Enter username"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password</td>
                    <td class="auto-style7">
                        <asp:TextBox ID="TextBoxLösen" runat="server" TextMode="Password" Width="153px"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxLösen" ErrorMessage="Enter password"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style7">
                        <asp:Button ID="ButtonLoggain" runat="server" OnClick="ButtonLoggain_Click" Text="Log in" />
                    </td>
                    <td class="auto-style11">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registration.aspx">Sign up</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style7">
                        &nbsp;</td>
                    <td class="auto-style11">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
    <p class="auto-style11">
        &nbsp;</p>
</body>
</html>
