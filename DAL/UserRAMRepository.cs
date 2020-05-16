using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="IUserRepository"/>
    /// </summary>
    public class UserRAMRepository : IUserRepository
    {
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
            Storage.AddUser(newUser);

            return true;
        }

        /// <summary>
        /// <see cref="IUserRepository.DeleteUser(string)"/>
        /// </summary>
        public bool DeleteUser(string email)
        {
            Storage.DeleteUser(email);

            return true;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUser(string)"/>
        /// </summary>
        public User GetUser(string email)
        {
            User user = Storage.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUserName(string)"/>
        /// </summary>
        public string GetUserName(string email)
        {
            User user = Storage.Users.FirstOrDefault(u => u.Email == email);

            return user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUserName(Guid)"/>
        /// </summary>
        public string GetUserName(Guid id)
        {
            User user = Storage.Users.FirstOrDefault(u => u.Id == id);

            return user.FirstName + " " + user.LastName;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetUserPhoto(Guid)"/>
        /// </summary>
        public string GetUserPhoto(Guid userId)
        {
            User user = Storage.Users.FirstOrDefault(u => u.Id == userId);

            return user.Photo;
        }

        /// <summary>
        /// <see cref="IUserRepository.UpdateUser(User)"/>
        /// </summary>
        public bool UpdateUser(User newUser)
        {
            Storage.UpdateUser(newUser);

            return true;
        }

        /// <summary>
        /// <see cref="IUserRepository.GetCurrentUser"/>
        /// </summary>
        public User GetCurrentUser()
            => Storage.Users.Where(u => u.Id == Storage.CurrentUser)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IUserRepository.ClearCurrentUser"/>
        /// </summary>
        public void ClearCurrentUser()
            => Storage.CurrentUser = Guid.Empty;
    }
}
