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
        void Edit(int TeamId,string Name, string Description, byte[] Image);
        void AddMember(string UserId, int TeamId,int RoleId);

        void AddRole(string Name, int TeamId, bool admin = false);
        string UserInRole(string userId,int teamId);
        int GetOwnerRoleId(int TeamId);
        int GetRoleId(string roleName, int teamId);
        IEnumerable<Team> GetTeamsByMember(string UserId);
        IEnumerable<ApplicationUser> GetTeamMembers(int teamId);
        IEnumerable<Team> SearchTeam(string Name);
        Team GetTeamByID(int Id);
        bool InviteUser(string InviterId, string InviteeId, int teamId, out string msg);
        IEnumerable<TeamInvite> GetInvites(string userId);
        bool DeclineInvite(int inviteId);
        bool AcceptInvite(int inviteId);
        int GetTeamIdFromInvite(int inviteId);
        bool SendJoinRequestToTeam(int teamId,string userId);
        bool DeclineRequest(int requestId);
        bool AcceptRequest(int requestId);
        ApplicationUser GetUserFromRequest(int requestId);
        IEnumerable<TeamJoinRequests> UserGetTeamJoinRequests(string userId);
        IEnumerable<TeamJoinRequests> TeamGetTeamJoinRequests(int teamId);
        bool IsUserAMember(string UserId, int teamId);
        bool IsRequestSent(string UserId, int teamId);
        Team GetTeamFromRequest(int requestId);
        bool isNameTaken(string Name);
        bool LeaveTeamMember(string MemberId, int teamId);
        bool TranseferOwnershipAndLeave(string OwnerId, int teamId, string MemberName,out string msg);
        bool DisabandonTeam(int teamId);
    }
}
