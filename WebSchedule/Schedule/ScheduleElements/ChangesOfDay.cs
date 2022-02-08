using Bool = System.Boolean;

namespace WebSchedule.Schedule.ScheduleElements
{
    /// <summary>
    /// Класс, представляющий сущность замен, вытягиваемых из API.
    /// </summary>
    public class ChangesOfDay
    {
        #region Область: Свойства.
        /// <summary>
        /// Свойство, отвечающее за то, что элемент с заменами на указанный день найден.
        /// <br/>
        /// Это нужно, чтобы можно было выводить разные сообщения, в зависимости от того,
        /// не найдены ли замены для текущей даты вообще, или не найдены только для указанной группы.
        /// </summary>
        public Bool ChangesFound { get; set; }

        /// <summary>
        /// Свойство, отвечающее за то, являются ли замены абсолютными.
        /// </summary>
        public Bool AbsoluteChanges { get; set; }

        /// <summary>
        /// Свойство, отвечающее за дату, на которую предназначены замены.
        /// </summary>
        public DateTime? ChangesDate { get; set; }

        /// <summary>
        /// Список с новыми парами.
        /// </summary>
        public List<Lesson> NewLessons { get; set; }

        /// <summary>
        /// Статическое поле с заменами по умолчанию.
        /// </summary>
        public static ChangesOfDay DefaultChanges { get; private set;}
        #endregion

        #region Область: Конструкторы.
        /// <summary>
        /// Конструктор класса по умолчанию.
        /// </summary>
        public ChangesOfDay()
        {

        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="absoluteChanges">Замены на весь день?</param>
        /// <param name="newLessons">Список с новыми парами.</param>
        public ChangesOfDay(Bool absoluteChanges, List<Lesson> newLessons)
        {
            AbsoluteChanges = absoluteChanges;
            NewLessons = newLessons;
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="absoluteChanges">Замены на весь день?</param>
        /// <param name="changesDate">Дата, на которую предназначенны замены.</param>
        /// <param name="newLessons">Список с новыми парами.</param>
        public ChangesOfDay(Bool absoluteChanges, DateTime? changesDate, List<Lesson> newLessons)
        {
            AbsoluteChanges = absoluteChanges;
            ChangesDate = changesDate;
            NewLessons = newLessons;
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="changesFound">Найден ли элемент с заменами на указанный день.</param>
        /// <param name="absoluteChanges">Замены на весь день?</param>
        /// <param name="changesDate">Дата (даже предполагаемая) замен.</param>
        /// <param name="newLessons">Список с новыми парами.</param>
		public ChangesOfDay(Bool changesFound, Bool absoluteChanges, DateTime? changesDate, List<Lesson> newLessons)
		{
			ChangesFound = changesFound;
			AbsoluteChanges = absoluteChanges;
			ChangesDate = changesDate;
			NewLessons = newLessons;
		}

		/// <summary>
		/// Статический конструктор класса.
		/// </summary>
		static ChangesOfDay()
        {
			DefaultChanges = new ChangesOfDay(false, Enumerable.Empty<Lesson>().ToList())
			{
				ChangesFound = false
			};
		}
        #endregion
    }
}
