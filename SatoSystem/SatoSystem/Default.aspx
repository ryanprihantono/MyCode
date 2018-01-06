<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SatoSystem._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sato Management System</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div align="center">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/assets/images/header-top-sato.jpg" />
            </div>
        </div>
        <div>
            <div align="center">
                <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
                    BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                    Font-Size="0.8em" ForeColor="#333333" onloggingin="Login1_LoggingIn">
                    <TextBoxStyle Font-Size="0.8em" />
                    <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                    <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                        ForeColor="White" />
                </asp:Login>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
