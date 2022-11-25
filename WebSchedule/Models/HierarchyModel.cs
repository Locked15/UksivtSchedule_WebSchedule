using System.Text.Json;
using WebSchedule.Controllers.Schedule.Getter;

namespace WebSchedule.Models
{
    /// <summary>
    /// Класс, представляющий собой иерархическую модель.
    /// </summary>
    public class HierarchyModel
    {
        #region Область: Свойства.
        /// <summary>
        /// Название отделения.
        /// </summary>
        public String? BranchName { get; set; }

        /// <summary>
        /// Название направления обучения.
        /// </summary>
        public String? DirectionName { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public String? GroupName { get; set; }

        /// <summary>
        /// Список, содержащий полный список всех групп.
        /// <br/>
        /// Используется при поиске.
        /// </summary>
        public static List<String> AllGroups { get; private set; }
        #endregion

        #region Область: Конструкторы.
        /// <summary>
        /// Конструктор класса по умолчанию.
        /// </summary>
        public HierarchyModel()
        {

        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="branchName">Название отделения.</param>
        /// <param name="directionName">Название направления обучения.</param>
        /// <param name="groupName">Название группы.</param>
        public HierarchyModel(String? branchName, String? directionName, String? groupName)
        {
            BranchName = branchName;
            DirectionName = directionName;
            GroupName = groupName;
        }

        /// <summary>
        /// Статический конструктор класса.
        /// </summary>
        static HierarchyModel()
        {
            AllGroups = new(1);
        }
        #endregion

        #region Область: Методы.
        /// <summary>
        /// Метод для инициализации названий всех групп.
        /// </summary>
        /// <param name="path">Путь к приложению, развернутому на сервере.</param>
        public static void InitializeAllGroups(String path)
        {
            String filePath = Path.Combine(path, "Controllers", "Other", "Data", "AllGroups.json");

            // Чтобы ускорить работу сайта, используем файл:
            if (File.Exists(filePath))
            {
                AllGroups = JsonSerializer.Deserialize<List<String>>(File.ReadAllText(filePath)) ?? Enumerable.Empty<String>().ToList();
            }

            // В ином случае, будем считывать данные из API:
            else
            {
                AllGroups = ScheduleApi.GetGroups();

                AllGroups = AllGroups.ToList();
                String value = JsonSerializer.Serialize(AllGroups, new JsonSerializerOptions { WriteIndented = true });

                try
                {
                    using StreamWriter sw1 = new(filePath, false, System.Text.Encoding.Default);
                    sw1.Write(value);
                }
                catch (Exception ex)
                {
                    // Log it.
                }
            }
        }
        #endregion
    }
}
