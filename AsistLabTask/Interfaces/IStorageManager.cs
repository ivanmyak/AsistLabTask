namespace AsistLabTask.Interfaces
{
    /// <summary>
    /// Интерфейс для управления файлами в хранилище.
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// Копирует файл из основного хранилища во временное.
        /// Возвращает путь к новому файлу.
        /// </summary>
        Task<string> CopyToTempAsync(string sourcePath);

        /// <summary>
        /// Удаляет файл из хранилища.
        /// </summary>
        Task DeleteAsync(string path);

        /// <summary>
        /// Получает байты файла для скачивания.
        /// </summary>
        Task<byte[]> ReadFileAsync(string path);

        /// <summary>
        /// Сохраняет файл в основное хранилище.
        /// </summary>
        Task<string> SaveAsync(string fileName, Stream content);
        
        /// <summary>
        /// Перемещает файл в Архив на определённый срок ("soft delete")
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        Task<string> MoveToArchiveAsync(string sourcePath);
    }
}
