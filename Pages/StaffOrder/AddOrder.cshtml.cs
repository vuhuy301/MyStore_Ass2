using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyStore_WebApp.Models;
using System;
using System.Threading.Tasks;
namespace MyStore_WebApp.Pages.StaffOrder
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MyStore_WebApp.Data;
    using MyStore_WebApp.Models;
    using System;
    using System.Threading.Tasks;

    namespace MyStore_WebApp.Pages.StaffOrder
    {
        public class AddOrderModel : PageModel
        {
            private readonly MyStoreDbContext _context;

            public AddOrderModel(MyStoreDbContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Order NewOrder { get; set; }

            public async Task<IActionResult> OnGetAsync()
            {
                NewOrder = new Order();
                NewOrder.OrderDate = DateTime.Now.Date;

                // Retrieve staff ID from session
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    // Redirect to login page or handle the case where user is not logged in
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

                _context.Orders.Add(NewOrder);
                await _context.SaveChangesAsync();

                return RedirectToPage("/StaffOrder/StaffOrderList");
            }
        }
    }