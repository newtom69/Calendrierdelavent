
using OmniFW.Business;

namespace AdventCalendar
{

    public partial class Box : Entite
    {

        public Box(int boxId)
        {
            _id = boxId;
        }

        [ID]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name { get; set; }
        public string Path { get; set; }

        public Box() : base() { }
    }

}
