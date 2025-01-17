using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using Tashpa11.Model;

namespace Tashpa11.Pages
{
    public class LoginModel : PageModel
    {
        public string msg { get; set; }
        public void OnGet()
        {
        }


        public IActionResult OnPost(string userName, string password)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\OneDrive\DSH\Doron\sources\repos\Tashpa11\Tashpa11\App_Data\User.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM Names WHERE UName = '{userName}' AND Pass = '{password}'";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, "names");

            int count = ds.Tables[0].Rows.Count;
            if (count > 0)

            {

                Name name = new Name();
                name.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                name.LName = ds.Tables[0].Rows[0]["LName"].ToString();
                name.UName = ds.Tables[0].Rows[0]["UName"].ToString();
                name.Admin = bool.Parse(ds.Tables[0].Rows[0]["Admin"].ToString());
                string IsAdmin = (name.Admin == true) ? "Admin" : "NotAdmin";

                HttpContext.Session.SetString("Admin", IsAdmin);


                HttpContext.Session.SetString("Username", name.UName);
                HttpContext.Session.SetString("FirstName", name.FName);
                HttpContext.Session.SetString("LastName", name.LName);

                return RedirectToPage("/Index");
            }
            else
            {
                msg = "Wrong username or password";
                return Page();
            }

            //this.UserName = HttpContext.Session.GetString("FirstName").ToString();
            //this.Password = HttpContext.Session.GetString("LastName").ToString();
            //bool b = HttpContext.Session.GetString("FirstName").ToString().IsNullOrEmpty();


        }
        //        catch
        //        {
        //            return RedirectToPage("/Index");
        //}


    }
}

