using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Repository.Interfaces;
using Services;

namespace Services
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IList<Group> GetAll()
        {
            IList<Group> result = null;

            this.groupRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.groupRepository.GetAll();
            });

            return result;
        }

        public Group Get(int id)
        {
            Group result = null;

            this.groupRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.groupRepository.Get(id);
            });

            return result;
        }

       public void Create(Group group)
       {
           this.groupRepository.GetSessionFactory().TransactionalInterceptor(() =>
           {
               this.groupRepository.Add(group);
           });
       }

      //  void Update(int id, Realty realty, string addres, string details);

      //  void Delete(int id);
    }
}
