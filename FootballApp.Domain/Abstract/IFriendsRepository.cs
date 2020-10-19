using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballApp.Domain.Models;

namespace FootballApp.Domain.Abstract
{
    public interface IFriendsRepository
    {
        IEnumerable<ApplicationUser> GetFriends(string id);
    }
}
