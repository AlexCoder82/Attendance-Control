
namespace AttendanceControl.API.Business.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Shift Shift { get; set; }       
    }
}
