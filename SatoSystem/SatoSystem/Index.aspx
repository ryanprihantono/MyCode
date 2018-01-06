<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SatoSystem.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sato Management System</title>
    <style type="text/css">
        .style1
        {
            width: 126px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div align="center">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/assets/images/header-top-sato.jpg" />
            </div>
    </div>
    <div>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    <asp:Menu ID="Menu1" runat="server" BackColor="#F7F6F3" 
                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#7C6F57" onmenuitemclick="Menu1_MenuItemClick" 
                        StaticSubMenuIndent="10px">
                        <StaticSelectedStyle BackColor="#5D7B9D" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                        <DynamicMenuStyle BackColor="#F7F6F3" />
                        <DynamicSelectedStyle BackColor="#5D7B9D" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                        <Items>
                            <asp:MenuItem Text="Transaction" Value="Transaction">
                                <asp:MenuItem Text="Report" Value="Report"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Employee" Value="Employee">
                                <asp:MenuItem Text="Attendance" Value="Attendance"></asp:MenuItem>
                            </asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
