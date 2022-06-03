using System.Text.Json;
using WebSchedule.Controllers.Other;
using WebSchedule.Models.ScheduleElements;
using Bool = System.Boolean;

namespace WebSchedule.Controllers.Schedule.Getter
{
    /// <summary>
    /// Класс, содержащий логику для получения данных из API.
    /// </summary>
    public class ScheduleApi
    {
        #region Область: Поля.
        /// <summary>
        /// Поле, содержащее индекс нужного дня.
        /// </summary>
        public Int32 DayInd;

        /// <summary>
        /// Поле, содержащее название группы.
        /// </summary>
        public String GroupName;
        #endregion

        #region Область: Свойства.
        /// <summary>
        /// Свойство, отвечающее за то, будут ли данные вытягиваться из БД или ассетов.
        /// </summary>
        public static Bool UseDataBase { get; set; }

        /// <summary>
        /// Свойство, отвечающее за то, будут ли использоваться "небезопасные" данные.
        /// <br/>
        /// Работает только при вытягивании данных из БД.
        /// </summary>
        public static Bool SelectUnsecure { get; set; }
        #endregion

        #region Область: Конструкторы класса.
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="day">Индекс нужного дня.</param>
        /// <param name="group">Название нужной группы.</param>
        public ScheduleApi(Int32 day, String group)
        {
            DayInd = day;
            GroupName = group;
        }

        /// <summary>
        /// Статический конструктор класса.
        /// </summary>
        static ScheduleApi()
        {
            UseDataBase = false;

            SelectUnsecure = false;
        }
        #endregion

        #region Область: Методы.
        /// <summary>
        /// Метод для получения расписания из API.
        /// </summary>
        /// <returns>Расписание на день.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public DaySchedule GetSchedule()
        {
            DaySchedule baseSchedule;
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(String.Format("{0}{1}", ScheduleApiPaths.BaseUrl, ScheduleApiPaths.PathToDay))
            };

            if (UseDataBase)
            {
                try
                {
                    baseSchedule = GetDataBaseSchedule(in client);
                }

                catch
                {
                    baseSchedule = DaySchedule.DefaultSchedule;
                }
            }

            else
            {
                try
                {
                    baseSchedule = GetAssetSchedule(in client);
                }
                
                catch
                {
                    baseSchedule = DaySchedule.DefaultSchedule;
                }
            }

            baseSchedule.Day = baseSchedule.Day.TranslateDay();
            return baseSchedule;
        }

        /// <summary>
        /// Вынесенный в отдельный метод, функционал получения оригинального расписания из БД.
        /// </summary>
        /// <param name="client">HTTP-клиент для обращения к API.</param>
        /// <returns>Расписание.</returns>
        private DaySchedule GetDataBaseSchedule(in HttpClient client)
        {
            client.BaseAddress = new Uri(String.Format("{0}{1}", client.BaseAddress?.OriginalString, ScheduleApiPaths.DbScheduleController));

            HttpResponseMessage message = client.GetAsync(String.Format("?{0}{1}&{2}{3}&{4}{5}", ScheduleApiPaths.DaySelector, DayInd,
            ScheduleApiPaths.GroupSelector, GroupName, ScheduleApiPaths.SelectUnsecureSelector, SelectUnsecure)).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<DaySchedule>(jsonValue);
            }

            return DaySchedule.DefaultSchedule;
        }

        /// <summary>
        /// Вынесенный в отдельный метод, функционал получения оригинального расписания из ассетов.
        /// </summary>
        /// <param name="client">HTTP-клиент для обращения к API.</param>
        /// <returns>Расписание.</returns>
        private DaySchedule GetAssetSchedule(in HttpClient client)
        {
            client.BaseAddress = new Uri(String.Format("{0}{1}", client.BaseAddress?.OriginalString, ScheduleApiPaths.AssetScheduleController));

            HttpResponseMessage message = client.GetAsync(String.Format("?{0}{1}&{2}{3}", ScheduleApiPaths.DaySelector, DayInd,
ScheduleApiPaths.GroupSelector, GroupName)).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<DaySchedule>(jsonValue);
            }

            return DaySchedule.DefaultSchedule;
        }

        /// <summary>
        /// Метод для получения замен из API.
        /// </summary>
        /// <returns>Объект, содержащий данные о заменах на день.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public ChangesOfDay GetChanges()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(String.Format("{0}{1}{2}", ScheduleApiPaths.BaseUrl, ScheduleApiPaths.PathToDay, ScheduleApiPaths.ChangesController))
            };

            HttpResponseMessage message = client.GetAsync(String.Format("?{0}{1}&{2}{3}",
            ScheduleApiPaths.DaySelector, DayInd, ScheduleApiPaths.GroupSelector, GroupName)).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<ChangesOfDay>(jsonValue);
            }

            return ChangesOfDay.DefaultChanges;
        }

        ///<summary>
        /// Метод для получения основных отделений из API.
        /// </summary>
        /// <returns>Список с отделениями.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static List<String> GetBranches()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(String.Format("{0}/{1}", ScheduleApiPaths.BaseUrl, ScheduleApiPaths.FolderController))
            };

            HttpResponseMessage message = client.GetAsync(client.BaseAddress).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<String>>(jsonValue);
            }

            return Enumerable.Empty<String>().ToList();
        }

        ///<summary>
        /// Метод для получения направлений обучения.
        /// </summary>
        /// <param name="folder">Нужное отделение для получения направлений.</param>
        /// <returns>Список с направлениями для указанного отделения.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static List<String> GetSubFolders(String folder)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(String.Format("{0}/{1}", ScheduleApiPaths.BaseUrl, ScheduleApiPaths.SubFolderController))
            };

            HttpResponseMessage message = client.GetAsync(String.Format("?{0}{1}", ScheduleApiPaths.FolderSelector, folder)).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<String>>(jsonValue);
            }

            return Enumerable.Empty<String>().ToList();
        }

        ///<summary>
        /// Метод для получения групп.
        /// </summary>
        /// <param name="folder">Отделение, в котором нужно искать группы.</param>
        /// <param name="subFolder">Направление обучения для получения групп.</param>
        /// <returns>Список с группами.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static List<String> GetGroups(String folder, String subFolder)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(String.Format("{0}/{1}", ScheduleApiPaths.BaseUrl, ScheduleApiPaths.GroupsController))
            };

            HttpResponseMessage message = client.GetAsync(String.Format("?{0}{1}&{2}{3}", ScheduleApiPaths.FolderSelector, 
            folder, ScheduleApiPaths.SubFolderSelector, subFolder)).Result;

            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<String>>(jsonValue);
            }

            return Enumerable.Empty<String>().ToList();
        }
        #endregion
    }
}
