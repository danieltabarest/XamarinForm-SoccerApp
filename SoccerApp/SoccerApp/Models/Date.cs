using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace SoccerApp.Models
{
    public class Date
    {
        public int DateId { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }
    }

}
