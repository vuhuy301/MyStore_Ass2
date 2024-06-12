using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly MyStore_WebApp.Models.MyStoreContext _context;

        public IndexModel(MyStore_WebApp.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? ProductName { get; set; }
        public SelectList? UnitPrice { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchPrice { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category).ToListAsync();
                var products = from p in _context.Products
                               select p;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    products = products.Where(s => s.ProductName.Contains(SearchString));
                }
                if (!string.IsNullOrEmpty(SearchPrice))
                {
                    products = products.Where(s => s.UnitPrice.ToString().Contains(SearchPrice));
                }
                Product = await products.ToListAsync();
            }
        }
    }
}
