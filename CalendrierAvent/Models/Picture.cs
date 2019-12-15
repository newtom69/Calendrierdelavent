
using OmniFW.Business;

namespace HttpCalendrierAvent.Models
{
    public partial class Picture : Entite
    {
        public Picture() : base()
        {
        }

        public Picture(int id) : base(id)
        {
        }

        [ID]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name { get; set; }

        [ParentId("Calendar", "Id")]
        public int CalendarId { get; set; }
        public int DayNumber { get; set; }
    }
}
