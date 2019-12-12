﻿using System.Collections.Generic;

namespace AdventCalendar.ViewModels
{
    public class CalendarViewModel
    {
        public Calendar Calendar { get; }

        public Dictionary<int, string> PicturesNames { get; }
        public Dictionary<int, string> GenericsPicturesNames { get; }

        public CalendarViewModel(Calendar calendar , Dictionary<int, string> picturesNames, Dictionary<int, string> genericsPicturesNames)
        {
            PicturesNames = picturesNames;
            GenericsPicturesNames = genericsPicturesNames;
            Calendar = calendar;
        }
    }
}