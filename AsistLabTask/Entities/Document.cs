using AsistLabTask.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistLabTask.Entities
{
    /// <summary>
    /// Основная сущность документа, принадлежащего пользователю.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Уникальный идентификатор документа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Владелец документа.
        /// </summary>
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;

        [Required, MaxLength(200)]
        public string Name { get; set; } = default!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        /// <summary>
        /// Текущий статус документа.
        /// </summary>
        public DocumentStatus Status { get; set; } = DocumentStatus.Active;

        /// <summary>
        /// Признак временного документа (копия для шаринга).
        /// </summary>
        public bool IsTemporary { get; set; }

        /// <summary>
        /// Токен для доступа к временной копии.
        /// </summary>
        [MaxLength(100)]
        public string? ShareToken { get; set; }

        /// <summary>
        /// Срок действия временной копии.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTimeOffset? ExpiresAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset HandleByAt { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DeletedAt { get; set; }

        [Required, MaxLength(500)]
        public string StoragePath { get; set; } = default!;

        public long FileSize { get; set; }

        [Required, MaxLength(100)]
        public string MimeType { get; set; } = default!;

        public ICollection<DocumentHistoryEvent > ArchiveEvents { get; set; } = new List<DocumentHistoryEvent>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

}
