using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymApp.Pages.Admin.Lessons
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupLesson Lesson { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> DaysOfWeek { get; set; }

        public IActionResult OnGet(int id)
        {
            Lesson = _context.GroupLessons.Find(id);
            if (Lesson == null)
                return RedirectToPage("./Index");

            LoadDropdowns();
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var existing = _context.GroupLessons.Find(id);
            if (existing == null)
                return RedirectToPage("./Index");

            // Update fields
            existing.Name = Lesson.Name;
            existing.CategoryId = Lesson.CategoryId;
            existing.DayOfWeek = Lesson.DayOfWeek;
            existing.Time = Lesson.Time;
            existing.InstructorName = Lesson.InstructorName;
            existing.StudioLocation = Lesson.StudioLocation;
            existing.MaxCapacity = Lesson.MaxCapacity;

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        private void LoadDropdowns()
        {
            CategoryList = _context.GroupLessonCategories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            DaysOfWeek = Enum.GetValues(typeof(DayOfWeek))
                .Cast<DayOfWeek>()
                .Select(d => new SelectListItem
                {
                    Value = d.ToString(),
                    Text = d.ToString()
                }).ToList();
        }
    }
}
