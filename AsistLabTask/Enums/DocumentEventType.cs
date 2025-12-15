using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Enums
{
    public enum DocumentEventType
    {
        [Display(Name = "Создан")]
        Created,

        [Display(Name = "Обновлён")]
        Updated,

        [Display(Name = "Поделился")]
        Shared,

        [Display(Name = "Мягко удалён")]
        SoftDeleted,

        [Display(Name = "Полностью удалён")]
        HardDeleted
    }
}
