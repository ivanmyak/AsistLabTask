using AsistLabTask.Data;
using AsistLabTask.Entities;

namespace AsistLabTask.Interfaces
{
    public interface IDocumentShareService
    {
        Task<string> CreateShareCopyAsync(Guid originalDocumentId);
        Task<(string FilePath, string MimeType)> DownloadAndDeleteAsync(string shareToken);
    }
}
