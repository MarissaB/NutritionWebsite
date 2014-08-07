using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginButton_Click(object sender, AuthenticateEventArgs e)
    {
        if (MyFunctionToValidateUser(Login1.UserName, Login1.Password))
        {
            FormsAuthentication.SetAuthCookie(Login1.UserName, true);


            //optionally keep the username in the session and treat this as
            //the authentication of the user for the whole application


            //now redirect to the page you like to 
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            
            //give proper messageing...
            //either using your own control or accessing the
            //login contrl's error labels
        }
    }


    //This is custom function to validate user    
    public bool MyFunctionToValidateUser(string userName, string password)
    {
        //Use your internal logic to validate user
        //You can even retrieve the user information from you tables in the database
        //and validate against the input user credentials



        //But here goes the simplest form
        if (Membership.ValidateUser(userName, password))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
