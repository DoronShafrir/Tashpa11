using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using Tashpa11.Model;

namespace Tashpa11.Pages.Login
{
    public class LogUpModel : PageModel
    {
        [BindProperty]
        public string AdminYN { get; set; }
        [BindProperty]
        public string TeacherYN { get; set; }
        [BindProperty]
        public Person person { get; set; }
        public string msg { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost(string Name, string FName, string UserName, string Password)
        {
            

            // בדיקה אם קיים שם משתמש
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\OneDrive\DSH\Doron\sources\repos\Tashpa11\Tashpa11\App_Data\User.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM Person WHERE UserName = '{person.UserName}'";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, "names");

            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                //msg.Style.Add("color", "red");
                msg = "User Name has been taken, try another one";
                return Page();
            }
            else
            {

                // בניית השורה להוספה
                DataRow dr = ds.Tables["names"].NewRow();

                try
                {

                    dr["Name"] = person.Name;
                    dr["FName"] = person.FName;
                    dr["UserName"] = person.UserName;
                    dr["Password"] = person.Password;
                    dr["Admin"] = bool.Parse(AdminYN.ToString());
                    dr["Teacher"] = bool.Parse(TeacherYN.ToString());
                    //dr["Admin"] = 0;
                    ds.Tables["names"].Rows.Add(dr);


                    // עדכון הדאטה סט בבסיס הנתונים
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.UpdateCommand = builder.GetInsertCommand();
                    adapter.Update(ds, "names");

                    return RedirectToPage("/Index");
                }
                catch
                {
                    return Page();

                }

            }
        }
    }
}
