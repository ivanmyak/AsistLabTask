using AsistLabTask.Data;
using AsistLabTask.Entities;
using AsistLabTask.Enums;
using AsistLabTask.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AsistLabTask.Services
{
    public class DocumentShareService : IDocumentShareService
    {
        private readonly TaskDbContext _db;
        private readonly IStorageManager _storageManager;

        public DocumentShareService(TaskDbContext db, IStorageManager storageManager)
        {
            _db = db;
            _storageManager = storageManager;
        }

        /// <summary>
        /// Создаёт временную копию документа и возвращает токен для доступа.
        /// </summary>
        public async Task<string> CreateShareCopyAsync(Guid originalDocumentId)
        {
            var original = await _db.Documents.FindAsync(originalDocumentId);
            if (original == null) throw new InvalidOperationException("Документ не найден");

            // Копируем файл в Temp
            var newPath = await _storageManager.CopyToTempAsync(original.StoragePath);

            var copy = new Document
            {
                Id = Guid.NewGuid(),
                OwnerId = original.OwnerId,
                Name = original.Name,
                Description = original.Description,
                MimeType = original.MimeType,
                FileSize = original.FileSize,
                StoragePath = newPath,
                Status = DocumentStatus.Active,
                IsTemporary = true,
                ShareToken = Guid.NewGuid().ToString("N"),
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(15), // срок жизни ссылки
                HandleByAt = original.HandleByAt
            };

            _db.Documents.Add(copy);
            await _db.SaveChangesAsync();

            return copy.ShareToken!;
        }

        /// <summary>
        /// Отдаёт файл по токену и сразу удаляет временную копию.
        /// </summary>
        public async Task<(string FilePath, string MimeType)> DownloadAndDeleteAsync(string shareToken)
        {
            var doc = await _db.Documents.FirstOrDefaultAsync(d => d.ShareToken == shareToken && d.IsTemporary);
            if (doc == null) throw new InvalidOperationException("Временный документ не найден или уже удалён");

            var filePath = doc.StoragePath;
            var mimeType = doc.MimeType;

            // Удаляем запись и файл
            _db.Documents.Remove(doc);
            await _db.SaveChangesAsync();

            await _storageManager.DeleteAsync(filePath);

            return (filePath, mimeType);
        }
    }
}
