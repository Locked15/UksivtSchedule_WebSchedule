using Bool = System.Boolean;

namespace WebSchedule.Controllers.Cookies
{
    /// <summary>
    /// Класс, содержащий всю логику, необходимую для работы с Cookie-файлами.
    /// </summary>
    public static class CookieFiles
    {
        #region Область: Свойства, связанные с Cookie-файлами.

        /// <summary>
        /// Свойство, отвечающее за то, будут ли данные вытягиваться из БД или ассетов.
        /// </summary>
        public static Bool UseDataBase { get; private set; } = false;

        /// <summary>
        /// Свойство, отвечающее за то, будут ли использоваться "небезопасные" данные.
        /// <br/>
        /// Работает только при вытягивании данных из БД.
        /// </summary>
        public static Bool SelectUnsecure { get; private set; } = false;

        /// <summary>
        /// Статическое свойство, содержащее значение, отвечающее за то, прочитаны ли Cookie-файлы.
        /// <br/>
        /// Нужно, чтобы куки считывались только в самом начале работы сайта.
        /// </summary>
        public static Bool CookiesReaded { get; set; } = false;

        /// <summary>
        /// Статическое свойство, содержащее текущую тему сайта.
        /// </summary>
        public static Theme CurrentTheme { get; set; } = Theme.Light;
        #endregion

        #region Область: Функции.

        /// <summary>
        /// Статический метод, позволяющий прочитать Cookie-файлы прямо в коде из отправленного контекста.
        /// <br/>
        /// Позволяет сократить код, просто вызывая эту функцию, вместо создания одной и той же логики в разных файлах.
        /// </summary>
        /// <param name="context">Контекст приложения для чтения файлов Cookies.</param>
        /// <returns>
        /// Чтобы вызов функции в RazorPages можно было обернуть в одиночную строку, она должна что-либо возвращать.
        /// В данном случае функция возвращает успешность считывания файлов.
        /// </returns>
        public static Object? ReadCookies(dynamic context)
        {
            if (!CookiesReaded)
            {
                try
                {
                    IRequestCookieCollection? cookies = context.Context.Request.Cookies;

                    if (cookies.ContainsKey("UseDataBase"))
                    {
                        UseDataBase = cookies["UseDataBase"] == "on";
                    }

                    if (cookies.ContainsKey("SelectUnsecure"))
                    {
                        SelectUnsecure = cookies["SelectUnsecure"] == "on";
                    }

                    if (cookies.ContainsKey("CurrentTheme"))
                    {
                        CurrentTheme = ThemeConverter.FromString(cookies["CurrentTheme"] ?? "светлая");
                    }
                }

                catch
                {
                    UseDataBase = false;
                    SelectUnsecure = false;

                    CurrentTheme = Theme.Light;
                }

                CookiesReaded = true;
            }

            return null;
        }

        /// <summary>
        /// Статический метод, позволяющий установить значения Cookie-файлов.
        /// </summary>
        /// <param name="useDataBase">Использовать базу данных как источник?</param>
        /// <param name="selectUnsecure">Выбирать небезопасные значения из БД?</param>
        /// <param name="theme">Текущая тема сайта.</param>
        public static void SetCookies(Bool? useDataBase, Bool? selectUnsecure, Theme? theme)
        {
            UseDataBase = useDataBase ?? false;
            SelectUnsecure = selectUnsecure ?? false;

            CurrentTheme = theme ?? Theme.Light;
        }
        #endregion

        /// <summary>
        /// Внутренний класс, содержащий функции, нужные для получения цветов сайта.
        /// </summary>
        public static class VisualPart
        {
            #region Подобласть: Константы для шаблонизации.

            /// <summary>
            /// Содержит шаблон для создания цветов, для указания оных в стилях элементов страницы.
            /// <br/> <br/>
            /// При форматировании строки цвета отправляются в таком порядке: 
            /// <list type="number">
            ///     <item>Красный;</item>
            ///     <item>Зеленый;</item>
            ///     <item>Синий.</item>
            /// </list>
            /// </summary>
            private const String RgbColorTemplate = "rgb({0}, {1}, {2})";

            /// <summary>
            /// Содержит шаблон для создания цветов с поддержкой альфа-канала (прозрачности), для указания оных в стилях элементов.
            /// </summary>
            private const String RgbaColorTemplate = "rgba({0}, {1}, {2}, {3})";
            #endregion

            #region Подобласть: Цвета текста.

            /// <summary>
            /// Функция для получения цвета текста заголовков.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно указать в стиле CSS.</returns>
            public static String GetTextColorForHeaders()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "black",
                    Theme.Dark => "azure",
                    Theme.Gray => "white",

                    _ => "white",
                };
            }

            /// <summary>
            /// Функция для получения цвета обычного текста.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно указать в стиле CSS.</returns>
            public static String GetTextColorForPlains()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "black",
                    Theme.Dark => "azure",
                    Theme.Gray => String.Format(RgbColorTemplate, 180, 180, 180),

                    _ => "white",
                };
            }
            #endregion

            #region Подобласть: Задние цвета.

            /// <summary>
            /// Функция для получения цвета заднего фона верхней панели навигации.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно отправить в стиль CSS.</returns>
            public static String GetViewsHeaderBackgroundColor()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "white",
                    Theme.Dark => String.Format(RgbColorTemplate, 14, 20, 26),
                    Theme.Gray => String.Format(RgbColorTemplate, 32, 32, 32),

                    _ => "azure"
                };
            }

            /// <summary>
            /// Функция для получения цвета заднего фона основного содержимого.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно отправить в стиль CSS.</returns>
            public static String GetViewsContentBackgroundColor()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "white",
                    Theme.Dark => String.Format(RgbColorTemplate, 14, 20, 26),
                    Theme.Gray => String.Format(RgbColorTemplate, 24, 24, 24),

                    _ => "azure"
                };
            }

            /// <summary>
            /// Функция для получения цвета заднего фона нижней панели.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно отправить в стиль CSS.</returns>
            public static String GetViewsFooterBackgroundColor()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "white",
                    Theme.Dark => String.Format(RgbColorTemplate, 14, 20, 26),
                    Theme.Gray => String.Format(RgbColorTemplate, 32, 32, 32),

                    _ => "azure"
                };
            }
            #endregion

            #region Подобласть: Цвета активных элементов.

            /// <summary>
            /// Функция для получения соответствующего текущей теме, класса CSS для кнопок.
            /// </summary>
            /// <returns>Строковое представление стиля, которое будет использоваться при построении модели DOM.</returns>
            public static String GetButtonsColorClass()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "btn-outline-dark",
                    Theme.Dark or Theme.Gray => "btn-outline-light",

                    _ => "btn-outline-light"
                };
            }

            /// <summary>
            /// Функция для получения соответствующего текущей теме, класса CSS для ссылок.
            /// </summary>
            /// <returns>Строковое представление стиля, которое будет использоваться при построении модели DOM.</returns>
            public static String GetLinksColorClass()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "darkgray",
                    Theme.Dark => "wheat",
                    Theme.Gray => String.Format(RgbaColorTemplate, 35, 125, 232, 100),

                    _ => "white"
                };
            }

            /// <summary>
            /// Функция для получения цвета заднего фона строки поиска.
            /// </summary>
            /// <returns>Строковое представление цвета, которое можно отправить в стиль CSS.</returns>
            public static String GetSearchPanelBackgroundColor()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "white",
                    Theme.Dark => String.Format(RgbColorTemplate, 14, 20, 26),
                    Theme.Gray => String.Format(RgbColorTemplate, 18, 18, 18),

                    _ => "azure"
                };
            }
            #endregion

            #region Подобласть: Цвета расписания.

            /// <summary>
            /// Функция для получения класса CSS заголовка таблицы с расписанием.
            /// </summary>
            /// <returns>Строковое представление стиля, которое будет использоваться при построении модели DOM.</returns>
            public static String GetScheduleTableHeadColorClass()
            {
                return CurrentTheme switch
                {
                    Theme.Light => "table-dark",
                    Theme.Dark => "table-light",

                    _ => "table-default"
                };
            }

            /// <summary>
            /// Функция для получения класса CSS элементов расписания.
            /// </summary>
            /// <param name="changed">Изменялась ли указанная пара. Значение по умолчанию: False.</param>
            /// <returns>Строковое представление стиля, которое будет использоваться при построении модели DOM.</returns>
            public static String GetScheduleElementsColorClass(Bool changed = false)
            {
                if (changed)
                {
                    return "table-info";
                }

                else
                {
                    return CurrentTheme switch
                    {
                        Theme.Light => "table-light",
                        Theme.Dark => "table-dark",

                        _ => "table-default"
                    };
                }
            }
            #endregion
        }
    }
}
