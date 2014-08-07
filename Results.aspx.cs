using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Global.foods.Servings == 0)
        {
            Global.foods.Servings = 1;
            ServingsError.Text = "You didn't say how many this serves! Default 1 serving.";
        }

        if (Global.foods.Name == null || Global.foods.Name == "Recipe Title")
        {
            Global.foods.Name = "My Recipe";
            NameError.Text = "You didn't give your recipe a name! Default My Recipe.";
        }

        BindIngredientData();
        BindRecipeData();
    }

    protected void BindIngredientData()
    {
        DataTable foodsource = new DataTable();
        
        foodsource.Columns.Add("Measure");
        foodsource.Columns.Add("SelectedUnit");
        foodsource.Columns.Add("NDB_Number");
        foodsource.Columns.Add("Description");
        foodsource.Columns.Add("Calories", typeof(decimal));          // 0
        foodsource.Columns.Add("Protein", typeof(decimal));           // 1
        foodsource.Columns.Add("Total fat", typeof(decimal));         // 2
        foodsource.Columns.Add("Carbs", typeof(decimal));             // 3
        foodsource.Columns.Add("Fiber", typeof(decimal));             // 4
        foodsource.Columns.Add("Sugars", typeof(decimal));            // 5
        foodsource.Columns.Add("Sat fat", typeof(decimal));           // 6
        foodsource.Columns.Add("Mono fat", typeof(decimal));          // 7
        foodsource.Columns.Add("Poly fat", typeof(decimal));          // 8
        foodsource.Columns.Add("Cholesterol", typeof(decimal));       // 9
        
        
        foreach (FoodList x in Global.foods.FoodLists)
        {
                x.calculatednutrients = Global.CalculateNutrition(x);
                foodsource.Rows.Add(x.Measure, x.SelectedUnit, x.NDB_Number, x.Description,
                    x.calculatednutrients[0],
                    x.calculatednutrients[1],
                    x.calculatednutrients[2],
                    x.calculatednutrients[3],
                    x.calculatednutrients[4],
                    x.calculatednutrients[5],
                    x.calculatednutrients[6],
                    x.calculatednutrients[7],
                    x.calculatednutrients[8],
                    x.calculatednutrients[9]);
        }

        ShoppingList.DataSource = foodsource;
        ShoppingList.DataBind();
    }

    protected void BindRecipeData()
    {
        DataTable recipesource = new DataTable();
        decimal[] totals = new decimal[10];
        int[] portions = new int[Global.foods.Servings];

        recipesource.Columns.Add("Servings", typeof(int));
        recipesource.Columns.Add("Calories", typeof(decimal));          // 0
        recipesource.Columns.Add("Protein", typeof(decimal));           // 1
        recipesource.Columns.Add("Total fat", typeof(decimal));         // 2
        recipesource.Columns.Add("Carbs", typeof(decimal));             // 3
        recipesource.Columns.Add("Fiber", typeof(decimal));             // 4
        recipesource.Columns.Add("Sugars", typeof(decimal));            // 5
        recipesource.Columns.Add("Sat fat", typeof(decimal));           // 6
        recipesource.Columns.Add("Mono fat", typeof(decimal));          // 7
        recipesource.Columns.Add("Poly fat", typeof(decimal));          // 8
        recipesource.Columns.Add("Cholesterol", typeof(decimal));       // 9


        // Iterate through ingredients in recipe and sum up each nutrient
        foreach (FoodList x in Global.foods.FoodLists)
        {
            for (int i = 0; i < 10; i++)
            {
                totals[i] += x.calculatednutrients[i];
            }
        }

        totals = Array.ConvertAll(totals, x=>x/Global.foods.Servings);

        for (int i = 1; i <= Global.foods.Servings; i++ )
        {
            recipesource.Rows.Add(i, (totals[0] * i), (totals[1] * i), (totals[2] * i), (totals[3] * i), (totals[4] * i), (totals[5] * i), 
                (totals[6] * i), (totals[7] * i), (totals[8] * i), (totals[9] * i));
        }

        RecipeInFull.DataSource = recipesource;
        RecipeInFull.DataBind();
    }
    protected void ResetHome_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Clear();
        
        Response.Redirect("Default.aspx");
    }
    protected void Account_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAccount.aspx");
    }
    protected void Home_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void SaveRecipe_Click(object sender, EventArgs e)
    {
        NutritionEntities4 bob = new NutritionEntities4();
        
            UserFunctions.addRecipe(bob);
            Label lbl = (Label)LoginView2.FindControl("SavedMessage");
            lbl.Text = "Recipe has been saved to your account!";
        
    }


}