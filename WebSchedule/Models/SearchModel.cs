﻿namespace WebSchedule.Models
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
            Request = request != null ? request.Trim(' ') : String.Empty;

            Options = HierarchyModel.AllGroups.Where(group => group.Contains(Request, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        #endregion
    }
}
