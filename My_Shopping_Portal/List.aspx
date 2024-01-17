<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="My_Shopping_Portal.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Calibri">
    <form id="form1" runat="server">
        <div style="padding:40px; margin:5px; border:solid wheat .01pt">
            <div style="margin:10px; text-align:center; padding:5px"><h3>Shop September</h3></div>
            <hr style="border:solid red .01pt"/>
            <div style="border-bottom:solid .01pt wheat">
                <div style="width:25%; display:inline-block; margin:10px">
                    Welcome : <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </div>
                <div style="width:25%; display:inline-block; margin:10px">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="42px" ImageUrl="~/Images/Cart.png" Width="51px" OnClick="ImageButton1_Click" />
                    <asp:LinkButton ID="lnkCart" runat="server" OnClick="lnkCart_Click" ></asp:LinkButton>
                </div>
                <div style="width:25%; display:inline-block; margin:10px">
                    <asp:LinkButton ID="btnHome" runat="server" OnClick="btnHome_Click" >Buy More Items</asp:LinkButton>
                </div>
            </div>
            
            <div style="display:block; height:auto">
                <div>
                <div style="padding:10px; width:25%; display:inline-block; border:solid wheat .01pt">
                    <div style="font-variant:small-caps; height: 106px; width: 421px;">
                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:CheckBoxList ID="cblVendors" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblVendors_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div style="padding:10px; width:65%; float:right; border:solid wheat .01pt">
    <asp:GridView ID="dvProductes" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" AutoGenerateColumns="False" Width="457px" style="margin-right: 86px">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:TemplateField HeaderText="Product">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Product") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Manufacturer">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Vendor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("price") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add to cart">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Pid") %>' OnClick="LinkButton1_Click">Add To Cart</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </div>
                    <br />
                </div>
                </div>
                <div style="margin-top:20px">
                   <hr style="border:solid red .01pt"/>
                    <div style="text-align:center">
                        Application  Developed and maintained by Tech Novice Solutions&trade;
                    </div>
                </div>
           
        </div>
    </form>
</body>
</html>