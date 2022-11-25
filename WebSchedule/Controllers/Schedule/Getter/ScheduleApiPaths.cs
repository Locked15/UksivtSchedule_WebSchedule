namespace WebSchedule.Controllers.Schedule.Getter
{
    /// <summary>
    /// Вспомогательный класс для "ScheduleApi", содержащий URL-пути.
    /// </summary>
    internal static class ScheduleApiPaths
    {
        #region Область: Базовые поля.
        /// <summary>
        /// Статическое поле, содержащее базовый URL сайта с API.
        /// </summary>
        internal static readonly String BaseUrl;

        /// <summary>
        /// Статическое поле, содержащее URL путь к контроллерам по дням.
        /// </summary>
        internal static readonly String PathToDay;
        #endregion

        #region Область: Контроллеры.
        /// <summary>
        /// Статическое поле, содержащее URL путь к контроллеру расписания по ассетам.
        /// </summary>
        internal static readonly String AssetScheduleController;

        /// <summary>
        /// Статическое поле, содержащее URL путь к контроллеру расписания по БД.
        /// </summary>
        internal static readonly String DbScheduleController;

        /// <summary>
        /// Статическое поле, содержащее URL путь к контроллеру замен.
        /// </summary>
        internal static readonly String ChangesController;

        /// <summary>
        /// Статическое поле, содержащее URL путь к контроллеру групп.
        /// </summary>
        internal static readonly String GroupsController;
        #endregion

        #region Область: Селекторы (параметры).
        /// <summary>
        /// Статическое поле, содержащее параметр индекса дня.
        /// </summary>
        internal static readonly String DaySelector;

        /// <summary>
        /// Статическое поле, содержащее параметр названия группы.
        /// </summary>
        internal static readonly String GroupSelector;

        /// <summary>
        /// Статическое поле, содержащее параметр выбора небезопасных значений из БД.
        /// </summary>
        internal static readonly String SelectUnsecureSelector;

        /// <summary>
        /// Статическое поле, содержащее параметр отделения.
        /// </summary>
        internal static readonly String FolderSelector;

        /// <summary>
        /// Статическое поле, содержащее параметр направления обучения.
        /// </summary>
        internal static readonly String SubFolderSelector;
        #endregion

        /// <summary>
        /// Статический конструктор класса.
        /// </summary>
        static ScheduleApiPaths()
        {
            #region Область: Инициализация базовых значений.
            BaseUrl = "http://uksivtscheduleapi.azurewebsites.net";

            PathToDay = "/api/";
            #endregion

            #region Область: Инициализация контроллеров.
            AssetScheduleController = "schedule/day";
            DbScheduleController = "scheduledatabaseday";

            ChangesController = "changes/day";

            GroupsController = "api/structure/summary";
            #endregion

            #region Область: Инициализация параметров.
            DaySelector = "dayIndex=";
            GroupSelector = "groupName=";

            SelectUnsecureSelector = "selectunsecure=";

            FolderSelector = "folder=";
            SubFolderSelector = "subFolder=";
            #endregion
        }
    }
}
