using Bool = System.Boolean;

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

        /// <summary>
        /// Метод для проверки группы на её отношение к первому курсу.
        /// </summary>
        /// <param name="groupName">Название группы, которую нужно проверить.</param>
        /// <returns>Логическое значение, отвечающее за принадлежность группы.</returns>
        public static Bool CheckToFirstCourse(this String groupName)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (date.Month < 9)
            {
                date = date.AddYears(-1);
            }

            String yearPath = date.Year.ToString()[2..4];
            return groupName.Contains(yearPath);
        }

        /// <summary>
        /// Метод для проверки группы на её отношение ко второму курсу.
        /// </summary>
        /// <param name="groupName">Название группы, которую нужно проверить.</param>
        /// <returns>Логическое значение, отвечающее за принадлежность группы.</returns>
        public static Bool CheckToSecondCourse(this String groupName)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (date.Month < 9)
            {
                date = date.AddYears(-1);
            }

            date = date.AddYears(-1);

            String yearPath = date.Year.ToString()[2..4];
            return groupName.Contains(yearPath);
        }

        /// <summary>
        /// Метод для получения строки с временем пар.
        /// </summary>
        /// <param name="number">Номер текущей пары.</param>
        /// <param name="dayInd">Индекс текущего дня.</param>
        /// <param name="groupName">Название группы.</param>
        /// <returns>Строка, содержащая значения времени пар.</returns>
        public static String GetTime(this Int32 number, Int32 dayInd, String groupName)
        {
            // Обычные дни.
            if (dayInd != 6)
            {
                switch (number)
                {
                    case 0:
                        return "7:50 — 9:20";

                    case 1:
                        return "9:30 — 10:15\n10:20 — 11:05";

                    case 2:
                        if (CheckToFirstCourse(groupName))
                        {
                            return "11:15 — 12:00\n12:45 — 13:30";
                        }

                        else if (CheckToSecondCourse(groupName))
                        {
                            return "11:15 — 12:00\n12:05 — 12:25";
                        }

                        else
                        {
                            return "12:00 — 12:45\n12:45 — 13:30";
                        }

                    case 3:
                        return "13:35 — 14:20\n14:25 — 15:10";

                    case 4:
                        return dayInd == 2 ? 
                        "16:10 — 17:30" : "15:20 — 16:50";

                    case 5:
                        return dayInd == 2 ? 
                        "17:40 — 18:50" : "17:00 — 18:20";

                    default:
                        return dayInd == 2 ? 
                        "19:00 — 20:10" : "18:30 — 19:50";

                }
            }

            // Суббота.
            else
            {
                return number switch
                {
                    0 => "8:00 — 9:20",

                    1 => "9:30 — 10:50",

                    2 => "11:00 — 12:20",

                    3 => "12:30 — 13:50",

                    4 => "14:00 — 15:20",

                    5 => "15:30 — 16:50",

                    _ => "17:00 — 18:20",
                };
            }
        }
    }
}
