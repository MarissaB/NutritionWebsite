<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccount.aspx.cs" Inherits="UserAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recipe Analyzer - User Account Management</title>
</head>
<body>
    <header><h3>Recipe Analyzer by <% =Global.Programmer %></h3></header>
    <form id="form1" runat="server">
        <div id="Navigation">
        <asp:Button ID="Home" runat="server" BackColor="#006699" ForeColor="White" OnClick="Home_Click" Text="Home" />
        </div>
        <h2>Account Management</h2>
        Select a recipe you wish to modify.
        <br />

            <br />
        <div id="ButtonRow">
            <asp:Button ID="DeleteRecipe" runat="server" Text="Delete Recipe" BackColor="#006699"  ForeColor="white"  OnClick="DeleteRecipe_Click"/>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="EditRecipe" runat="server" Text="Edit Recipe" BackColor="#006699" ForeColor="White" OnClick="EditRecipe_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="NewRecipe" runat="server" Text="New Recipe" BackColor="#006699" ForeColor="White" OnClick="NewRecipe_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="ErrorMessage" runat="server" />
        </div>
        <br />
            <asp:GridView ID="UserAccountRecipes" runat="server" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="RecipeID" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:BoundField DataField="RecipeID" HeaderText="RecipeID" ReadOnly="True" SortExpression="RecipeID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Servings" HeaderText="Servings" SortExpression="Servings" />
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

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Local %>" SelectCommand="SELECT * FROM [Recipes] WHERE ([UserID] = @UserID)">
                <SelectParameters>
                    <asp:SessionParameter Name="UserID" SessionField="CurrentUser" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

        
    </form>
</body>
</html>
