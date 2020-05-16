using BLL.Interfaces;
using DAL.Interfaces;
using Infrastructure;
using Model;
using System;

namespace BLL
{
    /// <summary>
    /// <see cref="IUsersService"/>
    /// </summary>
    public class UserService : IUsersService
    {
        private readonly IUserRepository _rUserRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="userRepository"><see cref="IUserRepository"/></param>
        public UserService(IUserRepository userRepository)
        {
            _rUserRepository = userRepository;
        }

        /// <summary>
        /// <see cref="IUsersService.Connect(LoginModel, out string)"/>
        /// </summary>
        public bool Connect(LoginModel loginModel, out string errorMessage)
        {
            errorMessage = string.Empty;

            var user = _rUserRepository.GetUser(loginModel.Email);

            if (user == null)
            {
                errorMessage = "No user with such email found";
                return false;
            }

            if (!PasswordHelper.ValidatePassword(loginModel.Password, user.PasswordSalt, user.PasswordHash))
            {
                errorMessage = "Incorrect password";
                return false;
            }

            _rUserRepository.AddCurrentUser(user.Id);
            return true;
        }

        /// <summary>
        /// <see cref="IUsersService.DeleteUser(string, out string)"/>
        /// </summary>
        public bool DeleteUser(string email, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(email))
            { 
                errorMessage = $"User email can`t be null or empty!";
                return false;
            }

            User user = _rUserRepository.GetUser(email);
            if (user == null)
            {
                errorMessage = $"{nameof(user)} don`t exist in Users";
                return false;
            }

            return _rUserRepository.DeleteUser(email);
        }

        /// <summary>
        /// <see cref="IUsersService.GetUser(string)"/>
        /// </summary>
        public User GetUser(string email)
            => _rUserRepository.GetUser(email);

        /// <summary>
        /// <see cref="IUsersService.GetUserName(string)"/>
        /// </summary>
        public string GetUserName(string email)
            => _rUserRepository.GetUserName(email);

        /// <summary>
        /// <see cref="IUsersService.GetUserName(Guid)"/>
        /// </summary>
        public string GetUserName(Guid id)
            => _rUserRepository.GetUserName(id);

        /// <summary>
        /// <see cref="IUsersService.GetUserPhoto(Guid)"/>
        /// </summary>
        public string GetUserPhoto(Guid userId)
            => _rUserRepository.GetUserPhoto(userId);

        /// <summary>
        /// <see cref="IUsersService.RegisterNewUser(NewUser, out string)"/>
        /// </summary>
        public bool RegisterNewUser(NewUser user, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (user == null)
            {
                errorMessage = $"{nameof(user)} can`t be null!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                errorMessage = $"Email can`t be null or empty!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                errorMessage = "Password can`t be null or empty";
                return false;
            }

            User userFromDAL = _rUserRepository.GetUser(user.Email);
            if (userFromDAL != null)
            {
                errorMessage = $"User with email `{user.Email}` already exist!";
                return false;
            }

            if (!PasswordHelper.IsPasswordSatisfied(user.Password, out errorMessage))
                return false;

            byte[] passwordSalt = PasswordHelper.GenerateSalt();
            string passwordHash = PasswordHelper.HashPassword(user.Password, passwordSalt);

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                PasswordHash = passwordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Photo = user.Photo
            };

            return _rUserRepository.AddUser(newUser);
        }

        /// <summary>
        /// <see cref="IUsersService.UpdateUser(NewUser, out string)"/>
        /// </summary>
        public bool UpdateUser(NewUser newUser, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(newUser.Email))
            {
                errorMessage = $"Email can`t be null or empty!";
                return false;
            }

            User userFromList = _rUserRepository.GetUser(newUser.Email);
            if (userFromList == null)
            {
                errorMessage = $"User witn email {newUser.Email} don`t exist";
                return false;
            }

            byte[] passwordSalt = null;
            string passwordHash = null;

            if (!string.IsNullOrWhiteSpace(newUser.Password))
            {
                if (!PasswordHelper.IsPasswordSatisfied(newUser.Password, out errorMessage))
                    return false;

                passwordSalt = PasswordHelper.GenerateSalt();
                passwordHash = PasswordHelper.HashPassword(newUser.Password, passwordSalt);
            }

            User user = new User()
            {
                Id = userFromList.Id,
                FirstName = newUser.FirstName ?? userFromList.FirstName,
                LastName = newUser.LastName ?? userFromList.LastName,
                Email = newUser.Email ?? userFromList.Email,
                Photo = newUser.Photo ?? userFromList.Photo,
                PasswordSalt = passwordSalt != null ? Convert.ToBase64String(passwordSalt) : userFromList.PasswordSalt,
                PasswordHash = passwordHash ?? userFromList.PasswordHash
            };

            return _rUserRepository.UpdateUser(user);
        }

        /// <summary>
        /// <see cref="IUsersService.GetCurrentUser"/>
        /// </summary>
        public User GetCurrentUser()
            => _rUserRepository.GetCurrentUser();

        /// <summary>
        /// <see cref="IUsersService.ClearCurrentUser"/>
        /// </summary>
        public void ClearCurrentUser()
            => _rUserRepository.ClearCurrentUser();
    }
}
