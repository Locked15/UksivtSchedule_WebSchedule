using Microsoft.AspNetCore.Mvc;
using WebSchedule.Models;

namespace WebSchedule.Controllers
{
	/// <summary>
	/// Класс, представляющий контроллер расписания.
	/// </summary>
	public class ScheduleController : Controller
	{
		#region Область: Поля.
		/// <summary>
		/// Логгер.
		/// </summary>
		private readonly ILogger<ScheduleController> logger;
		#endregion

		#region Область: Конструктор.
		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="logger">Логгер.</param>
		public ScheduleController(ILogger<ScheduleController> logger)
		{
			this.logger = logger;
		}
		#endregion

		#region Область: Методы.
		/// <summary>
		/// Событие, срабатывающее при вызове из разметки.
		/// </summary>
		/// <returns>Новую страницу выбора дня.</returns>
		public IActionResult Day(String groupName)
		{
			return View("Day", new ScheduleModel(groupName));
		}

		/// <summary>
		/// Событие, срабатывающее при вызове из разметки.
		/// </summary>
		/// <param name="groupName">Название группы.</param>
		/// <param name="dayIndex">Индекс дня.</param>
		/// <returns>Новая страница с итоговым расписанием.</returns>
		public IActionResult Final(String groupName, Int32 dayIndex)
		{
			return View("Final", new ScheduleModel(groupName, dayIndex));
		}
        #endregion
    }
}
