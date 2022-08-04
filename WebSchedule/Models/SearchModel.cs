using WebSchedule.Controllers.Other;

namespace WebSchedule.Models
{
    /// <summary>
    /// Модель поисковой системы и результатов поиска.
    /// </summary>
    public class SearchModel
    {
        #region Область: Свойства.
        /// <summary>
        /// Поисковый запрос.
        /// </summary>
        public String? BaseRequest { get; set; }

        /// <summary>
        /// Нормализованный поисковый запрос.
        /// </summary>
        public String? NormalRequest { get; set; }

        /// <summary>
        /// Поисковая выдача.
        /// </summary>
        public List<String?> Options { get; set; }

        /// <summary>
        /// Hello darkness, my old friend; <br />
        /// I've come to talk with you again; <br />
        /// Because the visions softly creeping; <br />
        /// Left it's seeds while I was sleeping.
        /// <br /> <br />
        /// <b>Rest In Peace: 19П-5 (2019 — 2022).</b>
        /// </summary>
        public Boolean RestInPeace { get; set; } = false;
        #endregion

        #region Область: Конструктор.
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="request">Поисковый запрос.</param>
        public SearchModel(String request)
        {
            BaseRequest = request;
            NormalRequest = NormalizeSearchRequest(BaseRequest);
            Options = GetAvailableOptions();
        }
        #endregion

        #region Область: Функции.
        /// <summary>
        /// Проводит нормализацию поискового запроса, чтобы избежать проблем с поиском возможных совпадений.
        /// <br/>
        /// Кроме того, инициализирует свойство "RestInPeace".
        /// </summary>
        /// <param name="baseRequest">Базовый запрос (то, что пользователь ввел в строке поиска).</param>
        /// <returns>Нормализованный поисковый запрос.</returns>
        private String? NormalizeSearchRequest(String baseRequest)
        {
            String normalizedValue = baseRequest?.ToLower() ?? String.Empty;
            normalizedValue = normalizedValue.ReplaceAll(String.Empty, "-", "—", "_", " ");

            if (normalizedValue.Equals("19п5"))
            {
                RestInPeace = true;
            }

            return normalizedValue;
        }

        /// <summary>
        /// Проводит инициализацию возможной выдачи на поисковый запрос.
        /// </summary>
        /// <returns>Список возможной поисковой выдачи.</returns>
        private List<String?> GetAvailableOptions()
        {
            List<String?> options = new(1);
            options = HierarchyModel.AllGroups.FindAll(group =>
            {
                group = group.ToLower();
                group = group.ReplaceAll(String.Empty, "-", "—", "_", " ");

                return group.Contains(NormalRequest ?? String.Empty);
            });

            return options.OrderBy(o => o).ToList();
        }
        #endregion
    }
}
