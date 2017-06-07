using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using SoccerApp.Models;

namespace SoccerApp.Models
{
    public class Result
    {

       
            public int PredictionId { get; set; }
            public int UserId { get; set; }
            public int MatchId { get; set; }
            public int LocalGoals { get; set; }
            public int VisitorGoals { get; set; }
            public int Points { get; set; }
            public Match Match { get; set; }

        //public class Match
        //{
        //    public int MatchId { get; set; }
        //    public int DateId { get; set; }
        //    public DateTime DateTime { get; set; }
        //    public int LocalId { get; set; }
        //    public int VisitorId { get; set; }
        //    public int LocalGoals { get; set; }
        //    public int VisitorGoals { get; set; }
        //    public int StatusId { get; set; }
        //    public int TournamentGroupId { get; set; }
        //    public bool WasPredicted { get; set; }
        //    public Local Local { get; set; }
        //    public Visitor Visitor { get; set; }
        //}

        //public class Local
        //{
        //    public int TeamId { get; set; }
        //    public string Name { get; set; }
        //    public string Logo { get; set; }
        //    public string Initials { get; set; }
        //    public int LeagueId { get; set; }
        //}

        //public class Visitor
        //{
        //    public int TeamId { get; set; }
        //    public string Name { get; set; }
        //    public string Logo { get; set; }
        //    public string Initials { get; set; }
        //    public int LeagueId { get; set; }
        //}

    }


}
