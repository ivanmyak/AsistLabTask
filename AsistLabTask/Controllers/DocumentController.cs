using AsistLabTask.Data;
using AsistLabTask.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistLabTask.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly TaskDbContext _context;
        private readonly UserManager<User> _userManager;

        public DocumentController(TaskDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);

            var docs = _context.Documents
                .Where(d => d.OwnerId == user.Id && !d.IsDeleted)
               .AsNoTracking() 
                .ToList();

            return View(docs);
        }
    }
}
