using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Services
{
    public interface IUserService
    {
        IList<User> GetAll();

        User Get(int id);

        void Create(User user);

        void CreateGroup(User user, string groupname);

        void JoinGroup(User user,Group group);

        void LeaveGroup(User user, Group group);

        void RegisterPurchase(User user , Purchase purchase );

        User GetByUsernameAndPassword(string username, string password);

        IList<User> GetUsersWhoseNamesBeginWith(string username);

        User GetByUsername(string username);

        string GetHashPasswordFromUser(string username);

        //  void Update(int id, Realty realty, string addres, string details);

        //  void Delete(int id);

    }
}
