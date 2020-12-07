using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Edit(string Id, byte[] Image)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == Id);
            user.ProfilePicture = Image;
            context.SaveChanges();
        }

        public ApplicationUser GetUser(string Id)
        {
            ApplicationUser User = context.Users.FirstOrDefault(x => x.Id == Id);
            return User;   
        }
    }
}
