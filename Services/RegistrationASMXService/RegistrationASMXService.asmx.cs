using BLL;
using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Web.Services;

namespace RegistrationASMXService
{
    /// <summary>
    /// Summary description for RegistrationASMXService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class RegistrationASMXService : WebService
    {
        private readonly IUsersService _rUsersService;

        public RegistrationASMXService()
        {
            _rUsersService = new UserService(new UserDBRepository("ProjectManagementConnectionStr"));
        }

        [WebMethod]
        public string RegisterUser(NewUserDTO newUserDTO)
        {
            NewUser newUser = new NewUser()
            {
                FirstName = newUserDTO.FirstName,
                LastName = newUserDTO.LastName,
                Email = newUserDTO.Email,
                Password = newUserDTO.Password,
                CompanyId = newUserDTO.CompanyId
            };

            _rUsersService.RegisterNewUser(newUser, out string res);

            return res;
        }
    }

    public class NewUserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid CompanyId { get; set; }

        public string Photo { get; set; }
    }
}
