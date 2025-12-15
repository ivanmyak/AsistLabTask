using AsistLabTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = default!;

        [Required]
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = default!;

        [Required, MaxLength(2000)]
        public string Text { get; set; } = default!;

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Источник координат (браузер или выбор на карте).
        /// </summary>
        public LocationSource? Source { get; set; }
    }
}
