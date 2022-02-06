using WebSchedule.Schedule.ScheduleElements;

namespace WebSchedule.Models
{
    public class FinalScheduleModel
    {
        public DaySchedule Schedule { get; set; }

        public FinalScheduleModel(DaySchedule schedule)
        {
            Schedule = schedule;
        }
    }
}
