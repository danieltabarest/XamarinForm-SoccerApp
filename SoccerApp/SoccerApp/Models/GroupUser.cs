

namespace SoccerApp.Models
{
    public class GroupUser
    {
        public int GroupUserId { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsBlocked { get; set; }

        public int Points { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }


    }
}
