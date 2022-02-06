namespace WebSchedule.Models
{
	/// <summary>
	/// Класс-модель для расписания.
	/// </summary>
	public class ScheduleModel
	{
		#region Область: Свойства.
		/// <summary>
		/// Индекс дня (0:6).
		/// </summary>
		public Int32 DayId { get; set; }

		/// <summary>
		/// Название группы.
		/// </summary>
		public String GroupName { get; set; }
		#endregion

		#region Область: Конструктор.
		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="groupName">Название группы.</param>
		public ScheduleModel(String groupName)
		{
			GroupName = groupName;
		}

		/// <summary>
		/// Конструктор класса.
		/// </summary>
		/// <param name="groupName">Название группы.</param>
		/// <param name="dayId">Индекс дня.</param>
		public ScheduleModel(String groupName, Int32 dayId)
		{
			DayId = dayId;
			GroupName = groupName;
		}
		#endregion
	}
}
