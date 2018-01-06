<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SatoSystem.Report" Title="Sato Management System" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width:100%;">
    <tr>
        <td>
            <table>
                <tr>
                    <td>Date </td>
                    <td><asp:TextBox runat="server" ID="startDate"></asp:TextBox> - <asp:TextBox runat="server" ID="endDate"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Location</td>
                    <td>
                        <asp:DropDownList ID="cmbLocation" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                    </td>
                </tr>
            </table> 
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<script type="text/javascript">
    $(function() {
        $("#<%= this.startDate.ClientID %>").datepicker();
        $("#<%= this.endDate.ClientID %>").datepicker();
    });
</script>
</asp:Content>
