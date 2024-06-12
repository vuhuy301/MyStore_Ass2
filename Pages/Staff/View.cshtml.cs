using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.Staff
{
    public class ViewModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;
        public ViewModel(MyStoreContext dbContext)
        {
            _context = dbContext;
        }
        public IList<MyStore_WebApp.Models.Staff> StaffList { get; set; }

        public async Task OnGetAsync()
        {
            StaffList = await _context.Staffs.ToListAsync();
        }
    }
}
