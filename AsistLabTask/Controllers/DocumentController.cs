using AsistLabTask.Data;
using AsistLabTask.Entities;
using AsistLabTask.Enums;
using AsistLabTask.Interfaces;
using AsistLabTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AsistLabTask.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IStorageManager _storage;
        private readonly IDocumentShareService _shareService;
        private readonly UserManager<User> _userManager;
        private readonly TaskDbContext _context;

        public DocumentController(
            IStorageManager storage,
            IDocumentShareService shareService,
            UserManager<User> userManager,
            TaskDbContext context)
        {
            _storage = storage;
            _shareService = shareService;
            _userManager = userManager;
            _context = context;
        }

        // 📌 Список документов
        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            var docs = _context.Documents.Where(d => d.OwnerId == user.Id && !d.IsDeleted && !d.IsTemporary).ToList();
            return View(docs);
        }

        // 📌 Загрузка документа
        public IActionResult Upload() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file, string name, string? description, DateTimeOffset handleByAt)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Файл не выбран");
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            var path = await _storage.SaveAsync(file.FileName, file.OpenReadStream());

            var doc = new Document
            {
                Id = Guid.NewGuid(),
                OwnerId = user.Id,
                Name = name,
                Description = description,
                MimeType = file.ContentType,
                FileSize = file.Length,
                StoragePath = path,
                Status = DocumentStatus.Active,
                CreatedAt = DateTimeOffset.UtcNow,
                HandleByAt = handleByAt
            };

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        // 📌 Просмотр документа
        public async Task<IActionResult> Details(Guid id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return NotFound();
            return View(doc);
        }

        // 📌 Мягкое удаление
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            var archivedPath = await _storage.MoveToArchiveAsync(doc.StoragePath);
            doc.IsDeleted = true;
            doc.DeletedAt = DateTimeOffset.UtcNow;
            doc.StoragePath = archivedPath;
            doc.ExpiresAt = DateTimeOffset.UtcNow.AddDays(30);

            _context.Documents.Update(doc);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        // 📌 Шаринг документа
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(Guid id)
        {
            var token = await _shareService.CreateShareCopyAsync(id);
            var link = Url.Action("DownloadShared", "Document", new { token }, Request.Scheme);
            TempData["ShareLink"] = link;
            return RedirectToAction(nameof(List));
        }

        // 📌 Скачивание временной копии
        [AllowAnonymous]
        [HttpGet("document/share/{token}")]
        public async Task<IActionResult> DownloadShared(string token)
        {
            var (filePath, mimeType) = await _shareService.DownloadAndDeleteAsync(token);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, mimeType, Path.GetFileName(filePath));
        }
    }
}
