using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// External references for user functions via page updates in recipes.
/// </summary>
public class UserFunctions
{
	public static void addRecipe(NutritionEntities4 context)
    {
        Recipe nom = Global.foods;
        nom.UserID = (Guid)Membership.GetUser().ProviderUserKey;

        context.Recipes.Add(nom);
        foreach (FoodList x in nom.FoodLists)
        {
            context.FoodLists.Add(x);
        }
        context.SaveChanges();
    }

    public static void getRecipeIntoGlobal(int id)
    {
        RecipeRepository rr = new RecipeRepository();
        Global.foods = rr.GetById(id);        
    }

    public static void deleteRecipe(NutritionEntities4 context, int id)
    {
        Recipe nom = (from n in context.Recipes
                      where n.RecipeID == id
                      select n).FirstOrDefault();
        List<FoodList> fl = (from f in context.FoodLists
                             where f.RecipeID == id
                             select f).ToList();
        context.Recipes.Remove(nom);
        foreach (FoodList x in fl)
        {
            context.FoodLists.Remove(x);
        }
        context.SaveChanges();
    }

    public static void updateRecipe(NutritionEntities4 context, int id) //not working - damn it
    {
        addRecipe(context);
        deleteRecipe(context, id);        
    }
}