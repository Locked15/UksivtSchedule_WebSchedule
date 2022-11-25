using System.Text.Json;
using WebSchedule.Controllers.Other;
using WebSchedule.Controllers.Cookies;
using WebSchedule.Models.ScheduleElements;

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

            if (CookieFiles.UseDataBase)
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
            ScheduleApiPaths.GroupSelector, GroupName, ScheduleApiPaths.SelectUnsecureSelector, CookieFiles.SelectUnsecure)).Result;

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
        /// Метод для получения полного списка всех групп.
        /// </summary>
        /// <returns>Список с группами.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static List<String> GetGroups()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(String.Format("{0}", ScheduleApiPaths.BaseUrl))
            };
            HttpResponseMessage message = client.GetAsync(String.Format("/{0}", ScheduleApiPaths.GroupsController)).Result;
            if (message.IsSuccessStatusCode)
            {
                String jsonValue = message.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<String>>(jsonValue) ?? Enumerable.Empty<String>().ToList();
            }

            return Enumerable.Empty<String>().ToList();
        }
        #endregion
    }
}
