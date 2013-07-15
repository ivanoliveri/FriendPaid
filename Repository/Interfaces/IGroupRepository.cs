using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Repository.Interfaces
{
    public interface IGroupRepository:IRepository<Group>
    {
        Group GetByName(string groupName);
    }
}
