using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Enums
{
    public enum DocumentStatus
    {
        [Display(Name = "Удалённый")]
        Deleted = -1,

        [Display(Name = "В архиве")]
        Archive = 0,

        [Display(Name = "Активный")]
        Active,
    }
}
