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
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var staffQuery = from s in _context.Staffs
                             select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                staffQuery = staffQuery.Where(s => s.Name.Contains(SearchString));
            }

            StaffList = await staffQuery.ToListAsync();
        }
    }
}
