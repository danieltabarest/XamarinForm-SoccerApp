using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using SoccerApp.Models;

namespace SoccerApp.Models
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public List<GroupUser> GroupUsers { get; set; }


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
