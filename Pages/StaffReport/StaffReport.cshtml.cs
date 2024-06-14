using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore_WebApp.Models;
using MyStore_WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore_WebApp.Pages.StaffReport
{
    public class StaffReportModel : PageModel
    {
        private readonly MyStoreContext _context;

        public StaffReportModel(MyStoreContext context)
        {
            _context = context;
        }

        public List<OrderViewModel> Orders { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void OnGet()
        {
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;

            LoadOrders();
        }

        public void OnPost(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate ?? DateTime.Now.AddMonths(-1);
            EndDate = endDate ?? DateTime.Now;

            LoadOrders();
        }

        private void LoadOrders()
        {
            // Lấy StaffId từ Session
            int? staffId = HttpContext.Session.GetInt32("UserId");

            // Kiểm tra nếu StaffId không null (người dùng đã đăng nhập)
            if (staffId != null)
            {
                Orders = _context.Orders
                    .Where(o => o.StaffId == staffId && o.OrderDate >= StartDate && o.OrderDate < EndDate.Value.AddDays(1))
                    .Select(o => new OrderViewModel
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        StaffId = o.StaffId,
                    })
                    .ToList();
            }
            else
            {
                // Nếu không có StaffId, gán Orders là danh sách trống
                Orders = new List<OrderViewModel>();
            }
        }
    }
}
