﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace SoccerApp.Models
{
    public class Team
    {
        [PrimaryKey]
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Initials { get; set; }

        public int LeagueId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<User> Fans { get; set; }

        public string FullLogo
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    return "avatar_shield.png";
                }

                return string.Format("http://soccerbackend.azurewebsites.net{0}", Logo.Substring(1));
            }
        }

        public override int GetHashCode()
        {
            return TeamId;
        }
    }

}
