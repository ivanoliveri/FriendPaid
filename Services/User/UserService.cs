using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Repository.Interfaces;

namespace Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;

        private readonly IGroupRepository groupRepository;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
        }

        public IList<User> GetAll()
        {
            IList<User> result = null;

            this.userRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.userRepository.GetAll();
            });

            return result;
        }

        public User Get(int id)
        {
            User result = null;

            this.userRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.userRepository.Get(id);
            });

            return result;
        }

        public void Create(User user)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
               this.userRepository.Add(user);
            });
        }

        public void CreateGroup(User user,string groupname)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                var group = user.createGroup(groupname);
                this.groupRepository.Add(group);
                this.userRepository.Update(user);
            });
        }

        public void JoinGroup(User user,Group group){
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                user.joinGroup(group);
                this.userRepository.Update(user);
            }); 
        }

        public void LeaveGroup(User user, Group group)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                user.leaveGroup(group);
                this.userRepository.Update(user);
                this.groupRepository.Update(group);
            });
        }

        public void RegisterPurchase(User user, Purchase purchase)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                user.registerPurchase(purchase);

                //Para que actualice las notificaciones
                purchase.debtors.ToList().ForEach(oneUser => this.userRepository.Update(oneUser));
            });
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            User result = null;
            
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                result =this.userRepository.GetByUsernameAndPassword(username, password);
            });

            return result;
        }

        public IList<User> GetUsersWhoseNamesBeginWith(string username)
        {
            IList<User> result = null;

            this.userRepository.GetSessionFactory().SessionInterceptor(() =>
            {
                result = this.userRepository.GetUsersWhoseNamesBeginWith(username);
            });

            return result; 
        }

        public User GetByUsername(string username)
        {
            User result = null;

            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                result = this.userRepository.GetByUsername(username);
            });

            return result;
        }

        public void Invite(User user, User userToInvite)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                user.addContact(userToInvite);
                this.userRepository.Update(user);
                this.userRepository.Update(userToInvite);
            });
        }

        public void Delete(User user, User userToDelete)
        {
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                user.removeContact(userToDelete);
                this.userRepository.Update(user);
                this.userRepository.Update(userToDelete);
            });
        }

        public string GetHashPasswordFromUser(string username)
        {
            string result = null;

            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                result = this.userRepository.GetHashPasswordFromUser(username);
            });

            return result;
        }

      //  void Update(int id, Realty realty, string addres, string details);

      //  void Delete(int id);
    }
}
