using AsistLabTask.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace AsistLabTask.Services
{
    public class LocalStorageManager : IStorageManager
    {
        private readonly string _storageRoot;
        private readonly string _tempRoot;
        private readonly string _archiveRoot;

        public LocalStorageManager(IWebHostEnvironment env)
        {
            _storageRoot = Path.Combine(env.ContentRootPath, "Data", "Docs");
            _tempRoot = Path.Combine(env.ContentRootPath, "Data", "Temp");
            _archiveRoot = Path.Combine(env.ContentRootPath, "Data", "Archive");

            Directory.CreateDirectory(_storageRoot);
            Directory.CreateDirectory(_tempRoot);
            Directory.CreateDirectory(_archiveRoot);
        }

        public async Task<string> SaveAsync(string fileName, Stream content)
        {
            var destPath = Path.Combine(_storageRoot, $"{Guid.NewGuid()}_{fileName}");
            using var destStream = File.Create(destPath);
            await content.CopyToAsync(destStream);
            return destPath;
        }

        public async Task<byte[]> ReadFileAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        public Task DeleteAsync(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            return Task.CompletedTask;
        }

        public async Task<string> CopyToTempAsync(string sourcePath)
        {
            var fileName = Path.GetFileName(sourcePath);
            var destPath = Path.Combine(_tempRoot, $"{Guid.NewGuid()}_{fileName}");

            using var sourceStream = File.OpenRead(sourcePath);
            using var destStream = File.Create(destPath);
            await sourceStream.CopyToAsync(destStream);

            return destPath;
        }

        public async Task<string> MoveToArchiveAsync(string sourcePath)
        {
            var fileName = Path.GetFileName(sourcePath);
            var destPath = Path.Combine(_archiveRoot, $"{Guid.NewGuid()}_{fileName}");

            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, destPath);
            }

            return await Task.FromResult(destPath);
        }
    }
}
