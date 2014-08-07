using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FoodList
/// </summary>
public partial class FoodList
{
    public decimal[] calculatednutrients { get; set; }
    public string Description
    {
        get
        {
            return Global.SwapDescription(NDB_Number);
        }
    }


}