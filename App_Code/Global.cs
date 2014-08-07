using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Global session variables and functions
/// </summary>
public class Global
{
    public static string Programmer
    {
        get { return ConfigurationManager.AppSettings["Programmer"]; }
    }
    public static Ingredient LookupFood(int id)
    {
        Ingredient foo = new Ingredient();
        var nomrepository = new IngredientRepository();
        foo = nomrepository.GetById(id);
        return foo;
    }


    public static Recipe foods
    {
        get
        {
            if (HttpContext.Current.Session["foods"] == null)
            {
                HttpContext.Current.Session["foods"] = new Recipe();
            }
            return HttpContext.Current.Session["foods"] as Recipe;
        }

        set { HttpContext.Current.Session.Add("foods", value); }
    }

    public static string[] Weights(int id)
    {
        Ingredient foo = LookupFood(id);
        string[] weights = new string[2] { foo.Weight1_Description, foo.Weight2_Description };
        
        return weights;
    }
    public static void SetSelectedWeight(int id, string weight)
    {
        Ingredient foo = LookupFood(id);
        foreach (FoodList x in Global.foods.FoodLists)
        {
            if (x.NDB_Number == foo.NDB_Number)
            {
                x.SelectedUnit = weight;
            }
        }
    }
    public static string GetSelectedWeight(int id)
    {
        Ingredient foo = LookupFood(id);
        string unit = "";
        foreach (FoodList x in Global.foods.FoodLists)
        {
            if (x.NDB_Number == foo.NDB_Number)
            {
                unit = x.SelectedUnit;
            }
        }
        return unit;
    }

    public static void SetSelectedMeasure(int id, decimal measure)
    {
        Ingredient foo = LookupFood(id);
        foreach (FoodList x in Global.foods.FoodLists)
        {
            if (x.NDB_Number == foo.NDB_Number)
            {
                x.Measure = measure;
            }
        }
    }
    public static decimal GetSelectedMeasure(int id)
    {
        Ingredient foo = LookupFood(id);
        decimal measure = 0;
        foreach (FoodList x in Global.foods.FoodLists)
        {
            if (x.NDB_Number == foo.NDB_Number)
            {
                measure = x.Measure;
            }
        }
        return measure;
    }

    public static decimal[] CalculateNutrition(FoodList nom)
    {
        Ingredient y = LookupFood(nom.NDB_Number);
        decimal[] nutrients = new decimal[10];
        if (nom.SelectedUnit == y.Weight1_Description)
        {
            nutrients[0] = (nom.Measure * (decimal)y.Calories__kcal_) / (decimal)y.Weight1;
            nutrients[1] = (nom.Measure * (decimal)y.Protein) / (decimal)y.Weight1;
            nutrients[2] = (nom.Measure * (decimal)y.Total_fat) / (decimal)y.Weight1;
            nutrients[3] = (nom.Measure * (decimal)y.Carbohydrate) / (decimal)y.Weight1;
            nutrients[4] = (nom.Measure * (decimal)y.Fiber) / (decimal)y.Weight1;
            nutrients[5] = (nom.Measure * (decimal)y.Sugars) / (decimal)y.Weight1;
            nutrients[6] = (nom.Measure * (decimal)y.Saturated_fat) / (decimal)y.Weight1;
            nutrients[7] = (nom.Measure * (decimal)y.Mono_fat) / (decimal)y.Weight1;
            nutrients[8] = (nom.Measure * (decimal)y.Poly_fat) / (decimal)y.Weight1;
            nutrients[9] = (nom.Measure * (decimal)y.Cholesterol) / (decimal)y.Weight1;
        }

        if (nom.SelectedUnit == y.Weight2_Description)
        {
            nutrients[0] = (nom.Measure * (decimal)y.Calories__kcal_) / (decimal)y.Weight2;
            nutrients[1] = (nom.Measure * (decimal)y.Protein) / (decimal)y.Weight2;
            nutrients[2] = (nom.Measure * (decimal)y.Total_fat) / (decimal)y.Weight2;
            nutrients[3] = (nom.Measure * (decimal)y.Carbohydrate) / (decimal)y.Weight2;
            nutrients[4] = (nom.Measure * (decimal)y.Fiber) / (decimal)y.Weight2;
            nutrients[5] = (nom.Measure * (decimal)y.Sugars) / (decimal)y.Weight2;
            nutrients[6] = (nom.Measure * (decimal)y.Saturated_fat) / (decimal)y.Weight2;
            nutrients[7] = (nom.Measure * (decimal)y.Mono_fat) / (decimal)y.Weight2;
            nutrients[8] = (nom.Measure * (decimal)y.Poly_fat) / (decimal)y.Weight2;
            nutrients[9] = (nom.Measure * (decimal)y.Cholesterol) / (decimal)y.Weight2;
        }
        return nutrients;
    }    

    public static string SwapDescription(int id)
    {
        return LookupFood(id).Description;
    }
}