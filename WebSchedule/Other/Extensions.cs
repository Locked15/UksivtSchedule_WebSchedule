namespace WebSchedule.Other
{
    /// <summary>
    /// Статический класс, содержащий методы расширения.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Метод расширения, позволяющий получить день по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс дня (0 : 6).</param>
        /// <returns>День, соответствующий данному индексу.</returns>
        /// <exception cref="IndexOutOfRangeException">Введен некорректный индекс.</exception>
        public static String GetDayByIndex(this Int32 index)
        {
            switch (index)
            {
                case 0:
                    return "Понедельник";

                case 1:
                    return "Вторник";

                case 2:
                    return "Среда";

                case 3:
                    return "Четверг";

                case 4:
                    return "Пятница";

                case 5:
                    return "Суббота";

                case 6:
                    return "Воскресенье";

                default:
                    throw new IndexOutOfRangeException($"Введен некорректный индекс ({index}).");
            }
        }

        /// <summary>
        /// Метод расширения, позволяющий получить индекс по названию дня.
        /// </summary>
        /// <param name="day">Название дня.</param>
        /// <returns>День, соответствующий данному индексу.</returns>
        /// <exception cref="ArgumentException">Введен некорректный день.</exception>
        public static Int32 GetIndexByDay(this String day)
        {
            day = day.TranslateDay();
            day = day.ToLower();

            switch (day)
            {
                case "понедельник":
                    return 0;

                case "вторник":
                    return 1;

                case "среда":
                    return 2;

                case "четверг":
                    return 3;

                case "пятница":
                    return 4;

                case "суббота":
                    return 5;

                case "воскресенье":
                    return 6;

                default:
                    throw new ArgumentException($"Введен некорректный день ({day}).");
            }
        }

        /// <summary>
        /// Метод для получения переведенного названия дня.
        /// </summary>
        /// <param name="day">Название дня на другом языке.</param>
        /// <returns>Название дня на русском.</returns>
        public static String TranslateDay(this String day)
        {
            day = day.ToLower();

            switch (day)
            {
                case "monday":
                    return "Понедельник";

                case "tuesday":
                    return "Вторник";

                case "wednesday":
                    return "Среда";

                case "thursday":
                    return "Четверг";

                case "friday":
                    return "Пятница";

                case "saturday":
                    return "Суббота";

                case "sunday":
                    return "Воскресенье";

                default:
                    return day;
            }
        }
    }
}
