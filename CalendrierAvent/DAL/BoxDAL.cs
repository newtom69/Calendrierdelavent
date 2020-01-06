using System;
using System.Linq;

namespace HttpCalendrierAvent.DAL
{
    public class BoxDAL
    {
        public Box Details(int id)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Box box = (from c in db.Box
                           where c.Id == id
                           select c).FirstOrDefault();
                return box;
            }
        }

        public Box Details(string name)
        {
            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                Box box = (from c in db.Box
                           where c.Name == name
                           select c).FirstOrDefault();
                return box;
            }
        }

        public void Add(string name)
        {
            Box box = new Box()
            {
                Name = name,
                Path = Guid.NewGuid().ToString("n"),
            };

            using (AdventCalendarEntities db = new AdventCalendarEntities())
            {
                db.Box.Add(box);
                db.SaveChanges();
            }
        }
    }
}