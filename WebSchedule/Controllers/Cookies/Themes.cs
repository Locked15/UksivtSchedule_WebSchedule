namespace WebSchedule.Controllers.Cookies
{
    /// <summary>
    /// Позволяет конвертировать значения перечисления в другие типы.
    /// </summary>
    public static class ThemeConverter
    {
        /// <summary>
        /// Преобразует значение перечисления тем в строковое представление.
        /// </summary>
        /// <param name="theme">Текущая тема.</param>
        /// <returns>Её строковое представление.</returns>
        public static String ToString(Theme theme)
        {
            return theme switch
            {
                Theme.Light => "Светлая",
                Theme.Dark => "Тёмная",

                _ => "???"
            };
        }

        /// <summary>
        /// Преобразует строковое значение в одно из значений перечисления.
        /// </summary>
        /// <param name="theme">Строковое название темы.</param>
        /// <returns>Элемент перечисления, соответствующий указанной теме.</returns>
        public static Theme FromString(String theme)
        {
            return theme.ToLower() switch
            {
                "light" or "светлая" => Theme.Light,
                "dark" or "тёмная" or "темная" => Theme.Dark,

                _ => Theme.Light,
            };
        }
    }

    /// <summary>
    /// Перечисление, используемое для определения темы сайта.
    /// </summary>
    public enum Theme
    {
        /// <summary>
        /// Светлая тема.
        /// </summary>
        Light = 0,

        /// <summary>
        /// Темная тема.
        /// </summary>
        Dark = 1
    }
}
