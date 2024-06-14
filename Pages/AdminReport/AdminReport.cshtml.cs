using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore_WebApp.Pages.AdminReport
{
    public class AdminReportModel : PageModel
    {
        private readonly MyStoreContext _context;

        public AdminReportModel(MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetSearchOrders(DateTime startDate, DateTime endDate)
        {
            var orders = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.StaffId,
                    TotalPrice = o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToList();

            return new JsonResult(orders);
        }
    }
}
