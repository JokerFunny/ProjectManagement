using Autofac;
using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="IUserRepository"/>
    /// </summary>
    public class UserDBRepository : IUserRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public UserDBRepository()
        {
            _rProjectManagementContext = IoC.Container.Resolve<ProjectManagementContext>();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public UserDBRepository(string connectionString)
        {
            _rProjectManagementContext = new ProjectManagementContext(connectionString);
        }

        /// <summary>
        /// <see cref="IUserRepository.AddCurrentUser(Guid)"/>
        /// </summary>
        public void AddCurrentUser(Guid id)
        {
            Storage.CurrentUser = id;
        }

        /// <summary>
        /// <see cref="IUserRepository.AddUser(User)"/>
        /// </summary>
        public bool AddUser(User newUser)
        {
            _rProjectManagementContext.Users.Add(newUser);

            _rProjectManagementContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// <see cref="IUserRepository.DeleteUser(string)"/>
        /// </summary>
        public bool DeleteUser(string email)
        {
            var user = _rProjectManagementContext.Users.Where(u => u.Email == email).FirstOrDefault();

            if (user != null)
            {
                _rProjectManagementContext.Users.Remove(user);
                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUser(string)"/>
        /// </summary>
        public User GetUser(string email)
            => _rProjectManagementContext.Users
            .Where(u => u.Email == email)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IUserRepository.GetUserName(string)"/>
        /// </summary>
        public string GetUserName(string email)
        {
            User user = _rProjectManagementContext.Users.FirstOrDefault(u => u.Email == email);

            return user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUserName(Guid)"/>
        /// </summary>
        public string GetUserName(Guid id)
        {
            User user = _rProjectManagementContext.Users.FirstOrDefault(u => u.Id == id);

            return user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUserPhoto(Guid)"/>
        /// </summary>
        public string GetUserPhoto(Guid userId)
            => _rProjectManagementContext.Users
            .Where(u => u.Id == userId)
            .FirstOrDefault()?
            .Photo;

        /// <summary>
        /// <see cref="IUserRepository.UpdateUser(User)"/>
        /// </summary>
        public bool UpdateUser(User newUser)
        {
            var userFromDB = _rProjectManagementContext.Users.Where(u => u.Id == newUser.Id).FirstOrDefault();

            if (userFromDB != null)
            {
                userFromDB.FirstName = newUser.FirstName;
                userFromDB.LastName = newUser.LastName;
                userFromDB.Email = newUser.Email;
                userFromDB.Photo = newUser.Photo;
                userFromDB.PasswordSalt = newUser.PasswordSalt;
                userFromDB.PasswordHash = newUser.PasswordHash;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetCurrentUser"/>
        /// </summary>
        public User GetCurrentUser()
            => _rProjectManagementContext.Users
            .Where(u => u.Id == Storage.CurrentUser)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IUserRepository.ClearCurrentUser"/>
        /// </summary>
        public void ClearCurrentUser()
            => Storage.CurrentUser = Guid.Empty;
    }
}
