using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore_WebApp.Models;

namespace MyStore_WebApp.Pages.Admin
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
            var defaultCategoryId = 1;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", defaultCategoryId);
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid || _context.Products == null || Product == null || _context.Categories == null)
            {
                return Page();
            }
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("./ManageProduct");
        }
    }
}
