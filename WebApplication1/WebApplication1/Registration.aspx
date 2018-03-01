<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebApplication1.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 30%;
            height: 30px;
        }
        .auto-style4 {
            height: 30px;
            width: 51%;
        }
        .auto-style6 {
            width: 128px;
            height: 30px;
        }
        .auto-style7 {
            width: 30%;
            height: 26px;
        }
        .auto-style8 {
            width: 128px;
            height: 26px;
        }
        .auto-style9 {
            height: 26px;
            width: 51%;
        }
        .auto-style10 {
            width: 30%;
        }
        .auto-style13 {
            width: 128px;
        }
        .auto-style14 {
            width: 51%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td colspan="2">
                        <h1>Registrera en Användare</h1>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">Namn:</td>
                    <td class="auto-style13">
                        <asp:TextBox ID="TextBoxNamn" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style14">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxNamn" ErrorMessage="Ange Namn" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">Användarnamn</td>
                    <td class="auto-style13">
                        <asp:TextBox ID="TextBoxAnvändarnamn" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                    </td>
                    <td class="auto-style14">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxAnvändarnamn" ErrorMessage="Ange Användarnamn" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">Lösenord</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="TextBoxLösen" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxLösen" ErrorMessage="Ange Lösenord" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Bekräfta Lösenord</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="TextBoxBekräfta" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxLösen" ErrorMessage="Bekräfta Lösenord" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxLösen" ControlToValidate="TextBoxBekräfta" ErrorMessage="Samma Lösenord krävs" ForeColor="Red"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style13">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Skapa" />
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
