using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IngredientList.DataSource = Global.foods.FoodLists.ToList();
            IngredientList.DataBind();
            RecipeTitle.Text = Global.foods.Name;
            Servings.Text = Global.foods.Servings.ToString();   
        }
    }

    

    protected void AddToList_Click(object sender, EventArgs e) 
    {
        if (SearchResults.SelectedDataKey == null)
        {
            SelectedIngredient.Text = "No ingredient selected."; 
        }
        else
        {
            FoodList fl = new FoodList();
            fl.NDB_Number = Convert.ToInt32(SearchResults.SelectedDataKey.Value);
            Global.foods.FoodLists.Add(fl);

            IngredientList.DataSource = Global.foods.FoodLists.ToList();

            IngredientList.DataBind();
            IngredientList.SelectedIndex = IngredientList.Rows.Count - 1;
        }
    }

    public string BuildIngredientQuery()
    {
        string[] search = SearchBox.Text.Split(' ');
        string query = "SELECT * FROM Ingredients ";
        foreach (string x in search)
        {
            if (x.Equals(search.First()))
            {
                query = query + "WHERE Description LIKE '%" + x + "%' AND "; // Prevents unnecessary "WHERE" in middle of search clauses
            }
            if (x.Equals(search.Last()))
            {
                query = query + "Description LIKE '%" + x + "%' "; // Prevents unnecessary "AND" before "ORDER BY" clause
            }
            else
            {
                query = query + "Description LIKE '%" + x + "%' AND ";
            }
        }
        query = query + "ORDER BY CASE WHEN Description LIKE '" + SearchBox.Text + "%' THEN 0 ELSE 1 END ASC, Description ASC";
        return query;
    }

    public void BindSearchResultData(object sender, EventArgs e)
    {
        SelectedIngredient.Text = "";
        AnotherError.Text = "";
        SearchResults.SelectedIndex = -1;
        SearchResults.DataSource = GetIngredients(BuildIngredientQuery());
        SearchResults.DataBind();
        
    }

    public static DataSet GetIngredients(string query)
    {
        DataSet dataSet = new DataSet();
        using (SqlConnection connection = Connection.GetDBConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataSet, "Ingredients");
        }
        return dataSet;
    }

    public void SearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SearchResults.SelectedIndex = -1;
        SearchResults.PageIndex = e.NewPageIndex;
        SearchResults.DataSource = GetIngredients(BuildIngredientQuery());
        SearchResults.DataBind();
    }
    protected void DumpSession(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Clear();
        IngredientList.DataBind();
        SearchResults.DataBind();
        SelectedIngredient.Text = "";
        AnotherError.Text = "";
        SearchResults.SelectedIndex = -1;
    }
    protected void Calculate_Click(object sender, EventArgs e)
    {
        Global.foods.Name = RecipeTitle.Text;
        Global.foods.Servings = Convert.ToInt32(Servings.Text);
        Response.Redirect("Results.aspx");
        
    }
    protected void DeleteFromRecipe_Click(object sender, EventArgs e)
    {
        if (IngredientList.SelectedDataKey != null)
        {
            var id = Convert.ToInt32(IngredientList.SelectedDataKey.Value);
            var listofdeletions = new List<FoodList>();
            foreach (FoodList x in Global.foods.FoodLists)
            {
                if (x.NDB_Number == id)
                    listofdeletions.Add(x);
            }
            foreach (var y in listofdeletions)
            {
                Global.foods.FoodLists.Remove(y);
            }
            IngredientList.DataSource = Global.foods.FoodLists.ToList();
            IngredientList.DataBind();
        }
        else
        {
            AnotherError.Text = "No ingredient selected for deletion.";
        }
    }



    
    protected void UnitDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList ddl = (DropDownList)gvr.FindControl("UnitDropdown");
        if (IngredientList.SelectedDataKey == null)
        {
            AnotherError.Text = "Select an ingredient first!";
        }
        else
        {
            Global.SetSelectedWeight(Convert.ToInt32(IngredientList.SelectedDataKey.Value), ddl.SelectedItem.Value);
            
        }
    }


    protected void IngredientList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (e.Row.FindControl("UnitDropdown") as DropDownList);
            int nomID = Convert.ToInt32(IngredientList.DataKeys[e.Row.RowIndex].Value);
            ddl.DataSource = Global.Weights(nomID);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));

            // Set selected value to the current selected weight, if available (stops ddl from resetting every time the gridview is changed)
            if (Global.GetSelectedWeight(nomID) != null)
            {
                ddl.SelectedValue = Global.GetSelectedWeight(nomID);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }

            // Set value to the current input of measure, if available (stops tb from resetting every time the gridview is changed)
            TextBox tb = (e.Row.FindControl("MeasureBox") as TextBox);
            tb.Text = Global.GetSelectedMeasure(nomID).ToString();
            
            
        }
    }    


    protected void MeasureBox_TextChanged(object sender, EventArgs e)
    {
        
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        TextBox tb = (TextBox)gvr.FindControl("MeasureBox");
        if (IngredientList.SelectedDataKey == null)
        {
            AnotherError.Text = "Select an ingredient first!";
        }
        else
        {
            Global.SetSelectedMeasure(Convert.ToInt32(IngredientList.SelectedDataKey.Value), Convert.ToDecimal(tb.Text));
            
        }
    }
    protected void Account_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAccount.aspx");
    }
}
