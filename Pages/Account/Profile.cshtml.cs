using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.Account
{
    public class ProfileModel : PageModel
    {

        private readonly MyStore_WebApp.Models.MyStoreContext _context;
        public ProfileModel(MyStoreContext dbContext)
        {
            _context = dbContext;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string oldpass, string newpass, string cfpass) {
            var user = await _context.Staffs.FirstOrDefaultAsync(u => u.Name == HttpContext.Session.GetString("Username"));
            if (oldpass != user.Password)
            {
                ViewData["ErrorMessage"] = "Invalid password.";
                return Page();
            }
            else if(oldpass == newpass){
                ViewData["ErrorMessage"] = "Old password equal new password";
                return Page();
            }
            else if(newpass != cfpass)
            {
                ViewData["ErrorMessage"] = "New password not equal confirm password";
                return Page();
            }
            else
            {
                user.Password = cfpass;
                _context.SaveChanges();
                ViewData["ErrorMessage"] = "Changepass success!";
                return Page();
            }
        }
    }
}
