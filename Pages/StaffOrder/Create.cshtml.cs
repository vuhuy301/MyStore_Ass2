using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class CreateModel : PageModel
    {
        private readonly MyStoreContext _context;

        public CreateModel(MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Retrieve StaffId from session
            int? staffId = HttpContext.Session.GetInt32("UserId");
            if (staffId == null)
            {
                // Redirect to login page if StaffId is not in session
                return RedirectToPage("/Account/Login");
            }

            // Create a new Order and set the StaffId and OrderDate from session
            Order = new Order
            {
                StaffId = staffId.Value,
                OrderDate = DateTime.Today // Set OrderDate to today's date
            };

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve StaffId from session
            int? staffId = HttpContext.Session.GetInt32("UserId");
            if (staffId == null)
            {
                // Redirect to login page if StaffId is not in session
                return RedirectToPage("/Account/Login");
            }

            // Set the StaffId from session
            Order.StaffId = staffId.Value;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set OrderDate to today's date
            Order.OrderDate = DateTime.Today;

            if (_context.Orders == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}