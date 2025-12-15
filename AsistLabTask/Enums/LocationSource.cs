using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Enums
{
    public enum LocationSource
    {
        [Display(Name = "Выбор пользователя на карте")]
        ManualGeolocation,

        [Display(Name = "Автоматическое определение браузером")]
        BrowserGeolocation
    }
}
