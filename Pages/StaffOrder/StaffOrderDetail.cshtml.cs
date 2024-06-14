using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class StaffOrderDetailModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public StaffOrderDetailModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

      public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null || _context.OrderDetails == null)
            {
                return Page();
            }

            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.OrderDetailId == orderId);
            if (orderdetail == null)
            {
                return Page();
            }
            else
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}
