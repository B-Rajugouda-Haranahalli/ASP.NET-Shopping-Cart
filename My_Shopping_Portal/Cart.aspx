<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="My_Shopping_Portal.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Calibri">
    <form id="form1" runat="server">
        <div style="padding:40px; margin:5px;border: solid wheat .01pt">
            <div style="margin:10px;text-align:center;padding:5px"><h3>Shop September</h3></div>
            <hr style="border:solid red .01pt" />
            <div>
                <div style="padding:10px;width:65%;display:inline-block; border:solid wheat .01pt">

                    

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="310px" style="margin-right: 0px" Width="673px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Product") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Vendor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("Pid") %>' Height="49px" ImageUrl="~/Images/Add.png" Width="51px" OnClick="ImageButton1_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="52px" ImageUrl="~/Images/Remove.png" Width="49px" CommandArgument='<%# Eval("Pid") %>' OnClick="ImageButton2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <div>
                        Total Amount: <asp:Label ID="lbltotalamount" runat="server"></asp:Label>
                        <br />
                        <asp:Button ID="btnpayment" runat="server" Text="Click To Make Payment" OnClick="btnpayment_Click" />
                    </div>
                </div>
                <div style="padding:10px;width:25%; float:right; display:inline-block; border:solid wheat .01pt">
                    
                
                
                    <asp:LinkButton ID="lnkHome" runat="server" OnClick="lnkHome_Click">Home</asp:LinkButton>
                    
                
                
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
