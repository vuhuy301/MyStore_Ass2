using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using MyStore_WebApp.Data;
using MyStore_WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class StaffOrderListModel : PageModel
    {
        private readonly MyStoreContext _context;

        public StaffOrderListModel(MyStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                Orders = await _context.Orders
                                       .Include(o => o.Staff)
                                       .Include(o => o.OrderDetails)
                                       .Where(o => o.StaffId == userId)
                                       .ToListAsync();
            }
            else
            {
                Orders = new List<Order>();
            }
        }
    }
}