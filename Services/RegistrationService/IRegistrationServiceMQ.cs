using Model;
using System.ServiceModel;

namespace RegistrationService
{
    [ServiceContract]
    public interface IRegistrationServiceMQ
    {
        [OperationContract(IsOneWay = true)]
        void RegisterUser(NewUser newUser);
    }
}