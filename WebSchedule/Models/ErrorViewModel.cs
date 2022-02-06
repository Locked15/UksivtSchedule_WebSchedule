using Bool = System.Boolean;

namespace WebSchedule.Models
{
    /// <summary>
    /// �����, �������������� ������ ������ ��� �������� ������.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ��������, ��������� ������.
        /// </summary>
        public String? RequestId { get; set; }

        /// <summary>
        /// ���������� ���������� �� ��, ����� �� ���������� ��������� � ���������.
        /// </summary>
        public Bool ShowRequestId => !String.IsNullOrEmpty(RequestId);
    }
}
