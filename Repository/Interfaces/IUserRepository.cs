using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Repository.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);

        User GetByUsername(string username);

        IList<User> GetUsersWhoseNamesBeginWith(string username);

        string GetHashPasswordFromUser(string username);
    }
}
