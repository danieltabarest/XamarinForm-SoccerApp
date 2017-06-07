using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace SoccerApp.Models
{
    public class Group
    {
        public int TournamentGroupId { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }
    }


}
