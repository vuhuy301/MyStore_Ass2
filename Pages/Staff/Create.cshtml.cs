using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore_WebApp.Models;
using System.Threading.Tasks;

namespace MyStore_WebApp.Pages.Staff
{
    public class CreateModel : PageModel
    {
        private readonly MyStoreContext _context;

        public CreateModel(MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyStore_WebApp.Models.Staff Staff { get; set; }

        public void OnGet()
        {
            Staff = new MyStore_WebApp.Models.Staff();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Staffs.Add(Staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./View");
        }
    }
}
