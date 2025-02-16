using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tashpa11.Model;

namespace Tashpa11.Pages.Login
{
    public class ExitModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.SetString("Admin", "");
            HttpContext.Session.SetString("Username", "");
            HttpContext.Session.SetString("FirstName", "");

            return RedirectToPage("/Index");

        }
    }
}
