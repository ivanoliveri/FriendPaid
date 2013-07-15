using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Services
{
    public interface IGroupService
    {
        IList<Group> GetAll();

        Group Get(int id);

        void Create(Group group);
    }
}
