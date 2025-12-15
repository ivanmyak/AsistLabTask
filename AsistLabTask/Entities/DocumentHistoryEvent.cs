using AsistLabTask.Enums;

namespace AsistLabTask.Entities
{
    /// <summary>
    /// Событие истории документа.
    /// </summary>
    public class DocumentHistoryEvent
    {
        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор документа, к которому относится событие.
        /// </summary>
        public Document Document { get; set; } = default!;

        /// <summary>
        /// Тип события.
        /// </summary>
        public DocumentEventType EventType { get; set; }

        /// <summary>
        /// Дата и время события.
        /// </summary>
        public DateTimeOffset At { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Пользователь, вызвавший событие (если применимо).
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Дополнительные данные события в формате JSON.
        /// </summary>
        public string? PayloadJson { get; set; }
    }
}
