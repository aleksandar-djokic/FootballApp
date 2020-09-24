using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface ITeamRepository
    {
        IEnumerable<Team> Teams { get; }
        void Create(string Name,string Description,byte[] Image,string user);
      
    }
}
