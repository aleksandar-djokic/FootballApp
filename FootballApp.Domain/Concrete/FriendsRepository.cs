using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class FriendsRepository : IFriendsRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<ApplicationUser> GetFriends(string id)
        {
            List<ApplicationUser> friends = new List<ApplicationUser>();
            var friendships = context.Friendships.Where(x => x.User1Id == id || x.User2Id == id).ToList();
            foreach(var f in friendships)
            {
                if (f.User1Id == id)
                {
                    friends.Add(f.User2);
                }
                else if (f.User2Id == id)
                {
                    friends.Add(f.User1);
                }
            }
            return friends;
        }
    }
}
