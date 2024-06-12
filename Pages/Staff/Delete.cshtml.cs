using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;
using System.Threading.Tasks;

namespace MyStore_WebApp.Pages.Staff
{
    public class DeleteModel : PageModel
    {
        private readonly MyStoreContext _context;

        public DeleteModel(MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var staff = await _context.Staffs.FindAsync(Id);

            if (staff == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./View");
        }
    }
}
