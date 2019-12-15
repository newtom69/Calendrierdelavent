
namespace HttpCalendrierAvent.Models
{
    using OmniFW.Business;
    using System;
    using System.Collections.Generic;

    public partial class Calendar : Entite
    {
        [ID]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string DisplayName { get; set; }
        public string PublicName { get; set; }
        public string PrivateName { get; set; }
        
        [ParentId("Box", "Id")]
        public int BoxId { get; set; }
        
        [Affichage]
        public string Path { get; set; }
        
        [ChildId("CalendarId")]
        public CollectionEntite<Picture> CollPic { get; set; }

        public Calendar(int id) : base(id)
        {
        }

        public Calendar() : base()
        {
        }

        public Calendar(string name, string column) : base()
        {
            Id = OmniFW.Outils.Trans.NullToInt(GetIdByColonne(column, name));
        }
    }
}
