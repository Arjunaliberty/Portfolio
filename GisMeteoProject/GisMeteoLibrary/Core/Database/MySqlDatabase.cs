using GisMeteoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Database
{
    public enum SelectTable
    {
        Informations,
        Weathers,
        All
    }
    
    /// <summary>
    /// Класс - образ БД
    /// </summary>
    public class MySqlDatabase
    {
        public Info Informations { get; set; }
        public Weather Weathers { get; set; }

        public MySqlDatabase()
        {
            this.Informations = new Info();
            this.Weathers = new Weather();
        }
    }
}
