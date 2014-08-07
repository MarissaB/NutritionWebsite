using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CurrentUser"] = Membership.GetUser().ProviderUserKey.ToString();
    }
    protected void Home_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void DeleteRecipe_Click(object sender, EventArgs e)
    {
        if (UserAccountRecipes.SelectedDataKey == null)
        {
            ErrorMessage.Text = "Select a recipe first.";
        }
        else
        {
            NutritionEntities4 bob = new NutritionEntities4();
            int id = Convert.ToInt32(UserAccountRecipes.SelectedDataKey.Value);
            UserFunctions.deleteRecipe(bob, id);

            UserAccountRecipes.DataBind();
            UserAccountRecipes.SelectedIndex = -1;

            ErrorMessage.Text = "Recipe was deleted!";
        }
    }
    protected void EditRecipe_Click(object sender, EventArgs e)
    {
        if (UserAccountRecipes.SelectedDataKey == null)
        {
            ErrorMessage.Text = "Select a recipe first.";
        }
        else
        {
            int id = Convert.ToInt32(UserAccountRecipes.SelectedDataKey.Value);
            UserFunctions.getRecipeIntoGlobal(id);
            
            Response.Redirect("Default.aspx");
        }
    }
    protected void NewRecipe_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Clear();
        
        Response.Redirect("Default.aspx");
    }
}