using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public LoginModel(MyStoreContext dbContext)
        {
            _context = dbContext;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            var user = await _context.Staffs.FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
            var account = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("account");
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Name);
                HttpContext.Session.SetInt32("UserId", user.StaffId);
                HttpContext.Session.SetInt32("Role", user.Role);
                return RedirectToPage("/Index");
            }
            else if (username.Equals(account["username"]) && password.Equals(account["password"]))
            {
                HttpContext.Session.SetString("Username", account["username"]);
                HttpContext.Session.SetInt32("UserId", 1);
                HttpContext.Session.SetInt32("Role", 1);
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
