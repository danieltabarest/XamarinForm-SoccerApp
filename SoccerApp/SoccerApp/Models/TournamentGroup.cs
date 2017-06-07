using SoccerApp.Models;
using System.Collections.Generic;

namespace SoccerApp.Models
{
    public class TournamentGroup
    {
        public int TournamentGroupId { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual List <Match> Matches { get; set; }

        public virtual List<TournamentTeam> TournamentTeams { get; set; }


    }
}
