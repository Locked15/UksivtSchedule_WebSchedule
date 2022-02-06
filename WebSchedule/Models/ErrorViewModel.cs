using Bool = System.Boolean;

namespace WebSchedule.Models
{
    /// <summary>
    ///  ласс, представл€ющий модель данных дл€ страницы ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ƒействие, вызвавшее ошибку.
        /// </summary>
        public String? RequestId { get; set; }

        /// <summary>
        /// ѕеременна€ отвечающа€ за то, нужно ли показывать сообщение с действием.
        /// </summary>
        public Bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
    }
}
