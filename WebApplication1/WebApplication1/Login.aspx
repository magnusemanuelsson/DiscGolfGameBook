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
        }
        .auto-style3 {
            width: 137px;
            height: 30px;
        }
        .auto-style4 {
            height: 30px;
        }
        .auto-style5 {
            text-align: center;
            font-size: xx-large;
        }
        .auto-style6 {
            height: 30px;
            width: 133px;
        }
        .auto-style7 {
            width: 133px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style5">
                <strong>Logga in</strong></div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">Användarnamn</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="TextBoxAnvändarnamn" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxAnvändarnamn" ErrorMessage="Fyll i användarnamn"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Lösenord</td>
                    <td class="auto-style7">
                        <asp:TextBox ID="TextBoxLösen" runat="server" TextMode="Password" Width="123px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxLösen" ErrorMessage="Fyll i lösenord"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style7">
                        <asp:Button ID="ButtonLoggain" runat="server" OnClick="ButtonLoggain_Click" Text="Logga in" />
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registration.aspx">Ny användare registrera här!</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
