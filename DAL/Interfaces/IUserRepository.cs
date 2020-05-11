using Model;
using System;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for User repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user from Storage
        /// </summary>
        /// <param name="email">Target email</param>
        User GetUser(string email);

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="newUser">Targer new user</param>
        bool AddUser(User newUser);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="newUser">Target new propertioes</param>
        bool UpdateUser(User newUser);

        /// <summary>
        /// Delete user by <paramref name="email"/>
        /// </summary>
        /// <param name="email">Target email</param>
        bool DeleteUser(string email);

        /// <summary>
        /// Get user photo by <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">Target user Id</param>
        string GetUserPhoto(Guid userId);

        /// <summary>
        /// Get user name by <paramref name="email"/>
        /// </summary>
        /// <param name="email">Target email</param>
        string GetUserName(string email);

        /// <summary>
        /// Add current user id
        /// </summary>
        /// <param name="id">Target id</param>
        void AddCurrentUser(Guid id);
    }
}
