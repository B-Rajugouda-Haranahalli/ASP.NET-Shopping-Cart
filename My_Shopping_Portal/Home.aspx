<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="My_Shopping_Portal.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Calibri">
    <form id="form1" runat="server">
        <div style="padding:40px; margin:5px;border: solid wheat .01pt">
            <div style="margin:10px;text-align:center;padding:5px"><h3>Shop September</h3></div>
            <h4 style="font-variant:small-caps">WelCome : <asp:Label ID="lblUser" runat="server" Text="Guest"></asp:Label></h4>
            <hr style="border:solid red .01pt" />
            <div>
                <div style="padding:10px;width:65%;display:inline-block; border:solid wheat .01pt">

                    <asp:DataList ID="DataList1" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" RepeatColumns="3">
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Height="230px" ImageUrl='<%# Eval("Image") %>' Width="234px" />
                            <br />
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Catid") %>' Text='<%# Eval("Category") %>' OnClick="LinkButton1_Click"></asp:LinkButton>
                            <br />
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:DataList>

                </div>
                <div style="padding:10px;width:25%; float:right; display:inline-block; border:solid wheat .01pt">
                    <table cellspacing="5px"; cellpadding="3px">
                        <tr>
                            <td>Email Id</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td>Password</td>
                            <td>
                                <asp:TextBox ID="txtPwd" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnLogin" runat="server" Height="40px" Width="100px" Text="Login" OnClick="btnLogin_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
           
                <div style="margin-top:20px">
                    <hr style="border:solid red .01pt"/>
                    <div style="text-align:center">
                        Application Developed And Maintained By B Rajugouda Haranahalli;
                    </div>
               </div>
            </div>
        </div>
    </form>
</body>
</html>
