﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSchedule.Models;
using WebSchedule.Controllers.Other;
using WebSchedule.Controllers.Cookies;
using WebSchedule.Controllers.Schedule.Getter;

namespace WebSchedule.Controllers
{
    /// <summary>
    /// Класс-контроллер всех страниц, доступных из заголовка.
    /// </summary>
    public class HomeController : Controller
    {
        #region Область: Поля.
        /// <summary>
        /// Внутреннее поле, содержащее объект, нужный для логирования.
        /// </summary>
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Внутреннее поле, содержащее данные о среде, где развернуто приложение.
        /// </summary>
        private readonly IHostEnvironment env;
        #endregion

        #region Область: Конструктор.
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="logger">Объект, нужный для логирования.</param>
        /// <param name="environment">Объект, содержащий данные об окружении, где развернут сайт.</param>
        public HomeController(ILogger<HomeController> logger, IHostEnvironment environment)
        {
            Task.Run(() => HierarchyModel.InitializeAllGroups(environment.ContentRootPath));
            Task.Run(() => new ScheduleApi(DateTime.Now.DayOfWeek.ToString().GetIndexByDay(), "19П-3").GetChanges());

            this.logger = logger;
            env = environment;
        }
        #endregion

        #region Область: Методы.
        /// <summary>
        /// Событие, срабатывающее при вызове из разметки.
        /// </summary>
        /// <returns>Новая страница с именем "Index".</returns>
        public IActionResult MainPage()
        {
            return View("MainPage");
        }

        /// <summary>
        /// Событие, срабатывающее при вызове из разметки.
        /// </summary>
        /// <returns>Новая страница с именем "Privacy".</returns>
        public IActionResult Info()
        {
            return View("Info");
        }

        /// <summary>
        /// Событие, срабатывающее при вызове из разметки.
        /// </summary>
        /// <returns>Страница с соглашением.</returns>
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        /// <summary>
        /// Событие, срабатывающее при вызове из разметки.
        /// </summary>
        /// <returns>Новая страница.</returns>
        public IActionResult Settings()
        {
            return View("Settings");
        }

        /// <summary>
        /// Событие, срабатывающее при сохранении настроек.
        /// <br/>
        /// Сохраняет параметры и перемещает пользователя обратно на главную страницу.
        /// </summary>
        /// <param name="useDb">Использовать базу данных для получения значений?</param>
        /// <param name="selectUnsecure">Выбирать небезопасные значения?</param>
        /// <param name="newTheme">Название новой темы.</param>
        /// <returns>Главная страница.</returns>
        public IActionResult SaveSettings(String useDb, String selectUnsecure, String newTheme)
        {
            CookieFiles.SetCookies(useDb == "on", selectUnsecure == "on", ThemeConverter.FromString(newTheme));

            HttpContext.Response.Cookies.Append("UseDataBase", CookieFiles.UseDataBase.ToString(), 
                                                new CookieOptions() { Expires = DateTime.Now.AddMonths(3), 
                                                                      HttpOnly = true });

            HttpContext.Response.Cookies.Append("SelectUnsecure", CookieFiles.SelectUnsecure.ToString(), 
                                                new CookieOptions() { Expires = DateTime.Now.AddMonths(3), 
                                                                      HttpOnly = true });

            HttpContext.Response.Cookies.Append("CurrentTheme", CookieFiles.CurrentTheme.ToString(), 
                                                new CookieOptions() { Expires = DateTime.Now.AddMonths(3), 
                                                                      HttpOnly = true });

            return RedirectPermanent("~/Home");
        }

        /// <summary>
        /// Событие, срабатывающее при вызове из разметки.
        /// </summary>
        /// <param name="groupName">Группа, которую хочет найти пользователь.</param>
        /// <returns>Новая страница поиска с результатами.</returns>
        public IActionResult Search(String groupName)
        {
            SearchModel model = new(groupName);

            if (model.Options.Count == 1)
            {
                return View("~/Views/Schedule/Day.cshtml", new ScheduleModel(model.Options.First()));
            }

            return View("Search", new SearchModel(groupName));
        }

        /// <summary>
        /// Событие, возникающее при ошибке.
        /// </summary>
        /// <returns>Создает новую страницу ошибки.</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
