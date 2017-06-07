using SoccerApp.Models;
using System.Collections.Generic;

namespace SoccerApp.Models
{
    public class League
    {
        public int LeagueId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public virtual List<Team> Teams{ get; set; }

    }
}
