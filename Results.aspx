<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recipe Analyzer - Results</title>
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
                            <asp:Button ID="Account" Text="Account Management" runat="server" BackColor="#006699" ForeColor="White"  OnClick="Account_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="SaveRecipe" Text="Save Current Recipe" runat="server" BackColor="#006699" ForeColor="White" OnClick="SaveRecipe_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="SavedMessage" runat="server" ></asp:Label>
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            Hello, guest.
                        </AnonymousTemplate>
                    </asp:LoginView>
                    <br />
                    <asp:LoginStatus ID="LoginStatus2" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/Logout.aspx" />
            </div>
        <br />
        <div id="Button Row">
            <asp:Button ID="ResetHome" Text="Make Another Recipe" BackColor="#006699"  ForeColor="white" runat="server" OnClick="ResetHome_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Home" Text="Edit Current Recipe" BackColor="#006699"  ForeColor="white" runat="server" OnClick="Home_Click" />
        </div>
        
        <div id="Recipe Heading">
            <h2>Title: <% =Global.foods.Name %>
            <br />
            Serves [<% =Global.foods.Servings %>] people
            <br />
            <% =Global.foods.FoodLists.Count %> ingredients
            </h2>
        </div>

        <div id="Error Labels">
            <asp:Label ID="ServingsError" runat="server"></asp:Label>
            <br />
            <asp:Label ID="NameError" runat="server"></asp:Label>
        </div>
        <br />
        FULL INGREDIENTS
        <br />
        <div id="Full Ingredients">
        <asp:GridView ID="ShoppingList" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            CellPadding="3" DataKeyNames="NDB_Number"     
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
            <Columns>
                <asp:BoundField DataField="measure" HeaderText="Measure" SortExpression="measure" />
                <asp:BoundField DataField="SelectedUnit" HeaderText="Unit" SortExpression="SelectedUnit" />

                <asp:BoundField DataField="NDB_Number" HeaderText="ID" SortExpression="NDB_Number"/>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />


                <asp:BoundField DataField="Calories" HeaderText="Calories" SortExpression="Calories" HtmlEncode="false" DataFormatString="{0:0.00}" />
                <asp:BoundField DataField="Protein" HeaderText="Protein" SortExpression="Protein" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Total fat" HeaderText="Total fat" SortExpression="Total fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>                
                <asp:BoundField DataField="Carbs" HeaderText="Carbs" SortExpression="Carbs" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                 <asp:BoundField DataField="Fiber" HeaderText="Fiber" SortExpression="Fiber" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Sugars" HeaderText="Sugars" SortExpression="Sugars" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Sat fat" HeaderText="Sat fat" SortExpression="Sat fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Mono fat" HeaderText="Mono fat" SortExpression="Mono fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Poly fat" HeaderText="Poly fat" SortExpression="Poly fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Cholesterol" HeaderText="Cholesterol" SortExpression="Cholesterol" HtmlEncode="false" DataFormatString="{0:0.00}"/>
               
            </Columns>
            <EmptyDataTemplate>Not using any ingredients is a pretty extreme diet! Click the blue button above to add ingredients to your recipe!</EmptyDataTemplate>
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
        NUTRITION BREAKDOWN BY SERVINGS CONSUMED
        <br />
        <div id="Recipe Breakdown">
            <asp:GridView ID="RecipeInFull" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            CellPadding="3"
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
            <Columns>
                <asp:BoundField DataField="Servings" HeaderText="Servings" SortExpression="Servings" />
                <asp:BoundField DataField="Calories" HeaderText="Calories" SortExpression="Calories" HtmlEncode="false" DataFormatString="{0:0.00}" />
                <asp:BoundField DataField="Protein" HeaderText="Protein" SortExpression="Protein" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Total fat" HeaderText="Total fat" SortExpression="Total fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>                
                <asp:BoundField DataField="Carbs" HeaderText="Carbs" SortExpression="Carbs" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                 <asp:BoundField DataField="Fiber" HeaderText="Fiber" SortExpression="Fiber" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Sugars" HeaderText="Sugars" SortExpression="Sugars" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Sat fat" HeaderText="Sat fat" SortExpression="Sat fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Mono fat" HeaderText="Mono fat" SortExpression="Mono fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>
                <asp:BoundField DataField="Poly fat" HeaderText="Poly fat" SortExpression="Poly fat" HtmlEncode="false" DataFormatString="{0:0.00}"/>

                <asp:BoundField DataField="Cholesterol" HeaderText="Cholesterol" SortExpression="Cholesterol" HtmlEncode="false" DataFormatString="{0:0.00}"/>
               
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
    </form>
</body>
</html>
