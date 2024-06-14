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
    public class DeleteOrderDetailsModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public DeleteOrderDetailsModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } // Remove "= default!" here

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderDetails
                                        .Include(od => od.Order)
                                        .Include(od => od.Product)
                                        .FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetail = await _context.OrderDetails.FindAsync(id);

            if (OrderDetail != null)
            {
                _context.OrderDetails.Remove(OrderDetail);
                await _context.SaveChangesAsync();
            }

            // Redirect back to the OrderDetail page with orderId parameter
            return RedirectToPage("/StaffOrder/OrderDetail", new { orderId = OrderDetail.OrderId });
        }
    }
}


