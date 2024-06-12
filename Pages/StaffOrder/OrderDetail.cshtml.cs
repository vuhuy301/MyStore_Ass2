using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class OrderDetailModel : PageModel
    {
        private readonly MyStoreContext _context;

        public OrderDetailModel(MyStoreContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            OrderDetails = await _context.OrderDetails
                                         .Where(od => od.OrderId == orderId)
                                         .ToListAsync();

            if (OrderDetails == null || OrderDetails.Count == 0)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
