<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recipe Analyzer - Home</title>
</head>
<body>
    <header><h3>Recipe Analyzer by <% =Global.Programmer %></h3></header>
    <form id="form1" runat="server">

        <div id="Login Handling">
        <asp:LoginView ID="LoginView2" runat="server">
                        <LoggedInTemplate>
                            Welcome back,
                            <asp:LoginName ID="LoginName1" runat="server" />.
                            <br />
                            <asp:Button ID="Account" Text="Account Management" runat="server" BackColor="#006699" ForeColor="White" OnClick="Account_Click" />
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            Hello, guest.
                        </AnonymousTemplate>
                    </asp:LoginView>
                    <br />
                    <asp:LoginStatus ID="LoginStatus2" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/Logout.aspx" />
        <br />
            </div>

        <div id="Input Boxes">
            <h4><asp:TextBox ID="RecipeTitle" runat="server" Width="220px">Recipe Title</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;Servings:
                <asp:TextBox ID="Servings" runat="server" Width="50px">0</asp:TextBox>
                <br /><br />
                <asp:TextBox ID="SearchBox" runat="server" Text="Search for Ingredients" Width="300px"></asp:TextBox>
            </h4>
        </div>
        
        <div id="Button Row">
            <asp:Button ID="Search" runat="server" Text="Search" BackColor="#006699"  ForeColor="white" OnClick="BindSearchResultData" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="AddToList" runat="server" Text="Add to recipe" OnClick="AddToList_Click" BackColor="#006699"  ForeColor="white"/>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="RemoveFromRecipe" runat="server" Text="Remove from recipe" OnClick="DeleteFromRecipe_Click" BackColor="#006699"  ForeColor="white"/>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ClearRecipe" runat="server" Text="Clear Recipe" OnClick="DumpSession" BackColor="#006699"  ForeColor="white" />         
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Calculate" runat="server" Text="Calculate" OnClick="Calculate_Click" BackColor="#006699"  ForeColor="white"/>
        </div>
        
        <div id="Error Messages">
            <asp:Label ID="SelectedIngredient" runat="server" BorderStyle="Dotted" BorderWidth="1"></asp:Label>
            <br />
            <asp:Label ID ="RecipeList" runat="server" BorderStyle="Dotted" BorderWidth="1"></asp:Label>
            <br />
            <asp:Label ID="AnotherError" runat="server" BorderStyle="Dotted" BorderWidth="1"></asp:Label>
            <br />
            <br />
        </div>
        
        <div id="Grids">
        <asp:GridView ID="IngredientList" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            CellPadding="3" DataKeyNames="NDB_Number" DataSource='<%# Global.foods.FoodLists.ToList() %>'
            OnRowDataBound="IngredientList_RowDataBound"
             ShowHeaderWhenEmpty="true"
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:TemplateField HeaderText="Measure" ControlStyle-Width="75px">
                    <ItemTemplate>
                        <asp:TextBox ID="MeasureBox" AutoPostBack="true" runat="server" OnTextChanged="MeasureBox_TextChanged">
                        </asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit" ControlStyle-Width="125px">
                    <ItemTemplate>
                        <asp:DropDownList ID="UnitDropdown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="UnitDropdown_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:BoundField DataField="NDB_Number" HeaderText="ID" SortExpression="NDB_Number"/>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                
            </Columns>
            <EmptyDataTemplate>Add some ingredients using the search!</EmptyDataTemplate>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Local %>" SelectCommand="SELECT [NDB_Number], [Description], [Calories (kcal)], [Protein], [Total fat], [Carbohydrate], [Fiber], [Sugars], [Saturated fat], [Mono fat], [Poly fat], [Cholesterol] FROM [Ingredients]"></asp:SqlDataSource>
            <br />
        <asp:GridView ID="SearchResults" runat="server" AllowPaging="True" OnPageIndexChanging="SearchResults_PageIndexChanging" DataKeyNames="NDB_Number" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="NDB_Number" HeaderText="ID" SortExpression="NDB_Number"/>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Calories (kcal)" HeaderText="Calories" SortExpression="Calories (kcal)" />
                <asp:BoundField DataField="Protein" HeaderText="Protein" SortExpression="Protein" />

                <asp:BoundField DataField="Total fat" HeaderText="Total fat" SortExpression="Total fat" />
                <asp:BoundField DataField="Carbohydrate" HeaderText="Carbs" SortExpression="Carbohydrate" />
                <asp:BoundField DataField="Fiber" HeaderText="Fiber" SortExpression="Fiber" />
                <asp:BoundField DataField="Sugars" HeaderText="Sugars" SortExpression="Sugars" />

                <asp:BoundField DataField="Saturated fat" HeaderText="Sat fat" SortExpression="Saturated fat" />
                <asp:BoundField DataField="Mono fat" HeaderText="Mono fat" SortExpression="Mono fat" />
                <asp:BoundField DataField="Poly fat" HeaderText="Poly fat" SortExpression="Poly fat" />

                <asp:BoundField DataField="Cholesterol" HeaderText="Cholesterol" SortExpression="Cholesterol" />
            </Columns>
            <EmptyDataTemplate>No matching search results. Run a new search!</EmptyDataTemplate>
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
        
    </form>
</body>
</html>
