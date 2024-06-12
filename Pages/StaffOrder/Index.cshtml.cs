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
    public class IndexModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public IndexModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get; set; } = default!;
        public int StaffId { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? SearchDate { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy StaffId từ session
            StaffId = HttpContext.Session.GetInt32("UserId") ?? 0;

            if (StaffId != 0)
            {
                // Lọc đơn đặt hàng bởi nhân viên đăng nhập và tùy chọn theo ngày tìm kiếm
                IQueryable<Order> ordersQuery = _context.Orders
                    .Where(o => o.StaffId == StaffId)
                    .Include(o => o.Staff);

                if (SearchDate.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.OrderDate.Date == SearchDate.Value.Date);
                }

                Order = await ordersQuery.ToListAsync();
            }
            else
            {
                // Xử lý trường hợp không có nhân viên đăng nhập
                Order = new List<Order>();
            }
        }
    }
}
