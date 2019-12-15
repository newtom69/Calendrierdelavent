using AdventCalendar.DAL;
using AdventCalendar.Tools;
using AdventCalendar.ViewModels;
using OmniFW.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AdventCalendar.Controllers
{
    public class ParentController : Controller
    {
        public ParentController()
        {
            Data.CnxStringStatic = ConfigurationManager.AppSettings["ChaineCnx"];
            //-- Définition de la base de données par défaut
            Data.TypeCnxStatic = TypeConnexion.SqlServer;
        }
    }
}