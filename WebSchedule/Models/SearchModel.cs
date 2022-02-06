namespace WebSchedule.Models
{
    public class SearchModel
    {
        #region Область: Свойства.
        /// <summary>
        /// Поисковый запрос.
        /// </summary>
        public String? Request { get; set; }

        /// <summary>
        /// Поисковая выдача.
        /// </summary>
        public List<String?> Options { get; set; }
        #endregion

        #region Область: Конструктор.
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="request">Поисковый запрос.</param>
        public SearchModel(String request)
        {
            Request = request ?? String.Empty;

            Options = HierarchyModel.AllGroups.Where(group => group.Contains(Request)).ToList();
        }
        #endregion
    }
}
