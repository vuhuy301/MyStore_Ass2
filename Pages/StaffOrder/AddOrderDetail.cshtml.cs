using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class AddOrderDetailModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public AddOrderDetailModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? OrderId { get; set; }

        public IActionResult OnGet(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            OrderId = orderId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");

            OrderDetail = new OrderDetail
            {
                OrderId = OrderId.Value
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
                return Page();
            }

            _context.OrderDetails.Add(OrderDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./OrderDetail", new { orderId = OrderDetail.OrderId });
        }
    }
}