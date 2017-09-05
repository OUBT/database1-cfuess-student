<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="Sales.OrderDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2><asp:Literal runat="server" ID="OrderTitle"></asp:Literal></h2>
        <h4><asp:Literal runat="server" ID="OrderTitle2"></asp:Literal></h4>
        <asp:HyperLink ID="lnkBack" runat="server">Back to Orders</asp:HyperLink>
        <div>
            <asp:GridView ID="OrderDetailGrid" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCancelingEdit="OrderDetailGrid_RowCancelingEdit" OnRowEditing="OrderDetailGrid_RowEditing" OnRowUpdating="OrderDetailGrid_RowUpdating" OnRowDataBound="OrderDetailGrid_RowDataBound">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
        <br />
            <asp:Button ID="btnShowAdd" runat="server" Text="Add to Order" OnClick="btnShowAdd_Click" />
            
            <asp:Panel ID="pnlAddProduct" runat="server" Visible="False">
                <asp:DropDownList ID="ddlProduct" runat="server"></asp:DropDownList>
                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" OnClick="btnAddCancel_Click" />
        </asp:Panel>
    </form>
</body>
</html>
