using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace SoccerApp.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public List<Group> Groups { get; set; }

        public List<Date> Dates { get; set; }

        public string FullLogo
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    //return "avatar_tournament.png";
                    return "SoccerLogo.png";
                    
                }

                return string.Format("http://soccerbackend.azurewebsites.net{0}", Logo.Substring(1));
            }
        }
    }

}
