using Model;
using System;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for User service
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Get user from DAL
        /// </summary>
        /// <param name="email">Target email</param>
        User GetUser(string email);

        /// <summary>
        /// Check if user with <paramref name="loginModel"/> can be logined
        /// </summary>
        /// <param name="loginModel">Target login model</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if login succesfull
        ///     False - if error happens
        /// </returns>
        bool Connect(LoginModel loginModel, out string errorMessage);

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user">Targer new user</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if login succesfull
        ///     False - if error happens
        /// </returns>
        bool RegisterNewUser(NewUser user, out string errorMessage);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="newUser">Target new propertioes</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateUser(NewUser newUser, out string errorMessage);

        /// <summary>
        /// Delete user by <paramref name="email"/>
        /// </summary>
        /// <param name="email">Target email</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if delete succesfull
        ///     False - if error happens
        /// </returns>
        bool DeleteUser(string email, out string errorMessage);

        /// <summary>
        /// Get user photo by <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">Target user Id</param>
        string GetUserPhoto(Guid userId);

        /// <summary>
        /// Get user name by <paramref name="email"/>
        /// </summary>
        /// <param name="email">Target id</param>
        string GetUserName(string email);
    }
}
