using HttpCalendrierAvent.Models;
using System;
using System.Linq;

namespace HttpCalendrierAvent.DAL
{
    public class BoxDAL
    {
        public Box Details(int id)
        {
            Box box = new Box(id);
            box.Lire();
            return box;
        }

        public Box Details(string name)
        {
            throw new NotImplementedException();
        }

        public void Add(string name)
        {
            Box box = new Box()
            {
                Name = name,
                Path = Guid.NewGuid().ToString("n"),
            };

            throw new NotImplementedException();
        }
    }
}