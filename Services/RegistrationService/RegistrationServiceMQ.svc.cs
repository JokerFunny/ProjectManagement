using BLL;
using BLL.Interfaces;
using DAL;
using Model;

namespace RegistrationService
{
    public class RegistrationServiceMQ : IRegistrationServiceMQ
    {
        private readonly IUsersService _rUsersService;

        public RegistrationServiceMQ()
        {
            _rUsersService = new UserService(new UserDBRepository("ProjectManagementConnectionStr"));
        }

        public void RegisterUser(NewUser newUser)
            => _rUsersService.RegisterNewUser(newUser, out _);
    }
}
