using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;
using System;
using System.Threading.Tasks;

namespace MyStore_WebApp.Pages.StaffOrder
{
    public class AddOrderModel : PageModel
    {
        private readonly MyStoreContext _context;

        public AddOrderModel(MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order NewOrder { get; set; }

        public IActionResult OnGet()
        {
            // Redirect to login page if user is not authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login");
            }

            // Set default order date to today
            NewOrder = new Order { OrderDate = DateTime.Today };

            // Retrieve staff ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login page if staff ID is not found in session
                return RedirectToPage("/Account/Login");
            }

            // Set the staff ID in NewOrder
            NewOrder.StaffId = userId.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Orders.Add(NewOrder);
                await _context.SaveChangesAsync();
                return RedirectToPage("/StaffOrder/StaffOrderList");
            }
            catch (DbUpdateException)
            {
                // Log the exception for further investigation
                // Logger.LogError(ex, "Error adding order");

                // Handle the error appropriately, such as displaying an error message to the user
                ModelState.AddModelError(string.Empty, "An error occurred while adding the order. Please try again later.");
                return Page();
            }
        }
    }
}