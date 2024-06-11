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

        public async Task OnGetAsync(DateTime? searchDate)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
           
                IQueryable<Order> ordersQuery = _context.Orders
                                                         .Include(o => o.Staff)
                                                         .Include(o => o.OrderDetails)
                                                         .Where(o => o.StaffId == userId);

                if (searchDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.OrderDate.Date == searchDate.Value.Date);
                }

                Orders = await ordersQuery.ToListAsync();
            }
            else
            {
                Orders = new List<Order>();
            }
        }
    }
}