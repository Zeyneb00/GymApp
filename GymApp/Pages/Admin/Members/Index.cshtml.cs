using Microsoft.AspNetCore.Mvc.RazorPages;
using GymApp.Data;
using GymApp.Models;
using System.Collections.Generic;
using System.Linq;
using MemberModel = GymApp.Models.Member;


namespace GymApp.Pages.Admin.Members
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<MemberModel> Members { get; set; }

        public void OnGet()
        {
            Members = _context.Members
                .OrderBy(m => m.FirstName)
                .ToList();
        }
    }
}
