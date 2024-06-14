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
        public int OrderId { get; set; }
        public decimal TotalOrderPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null)
            {
                return Page();
            }

            OrderId = orderId.Value;

            OrderDetails = await _context.OrderDetails
                                         .Include(od => od.Product)
                                         .Where(od => od.OrderId == orderId)
                                         .ToListAsync();

            if (OrderDetails == null || OrderDetails.Count == 0)
            {
                return Page();
            }

            // Calculate the total order price
            TotalOrderPrice = OrderDetails.Sum(od => od.TotalPrice);

            return Page();
        }
    }
}