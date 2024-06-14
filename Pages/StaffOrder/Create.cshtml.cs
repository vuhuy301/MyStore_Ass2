using System;
using System.Collections.Generic;
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
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public CreateModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Set default value for OrderDate to today's date
            Order = new Order
            {
                OrderDate = DateTime.Today // Set default value to today's date
            };

            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "StaffId");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "StaffId");
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
