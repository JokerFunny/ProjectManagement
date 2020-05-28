using BLL;
using BLL.Interfaces;
using DAL;
using Model;

namespace RegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RegistrationService : IRegistrationService
    {
        private readonly IUsersService _rUsersService;

        public RegistrationService()
        {
            _rUsersService = new UserService(new UserDBRepository("ProjectManagementConnectionStr"));
        }

        public bool GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _rUsersService.GetUser(email) != null
                ? true
                : false;
        }

        public string RegisterUser(NewUser newUser)
        {
            _rUsersService.RegisterNewUser(newUser, out string res);

            return res;
        }
    }
}
