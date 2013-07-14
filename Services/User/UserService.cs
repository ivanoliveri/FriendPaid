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

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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

        public User GetByUsernameAndPassword(string username, string password)
        {
            User result = null;
            
            this.userRepository.GetSessionFactory().TransactionalInterceptor(() =>
            {
                result =this.userRepository.GetByUsernameAndPassword(username, password);
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

      //  void Update(int id, Realty realty, string addres, string details);

      //  void Delete(int id);
    }
}
