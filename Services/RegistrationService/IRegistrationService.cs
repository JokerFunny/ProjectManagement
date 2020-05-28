using Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RegistrationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "create", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        string RegisterUser(NewUser newUser);

        [OperationContract]
        [WebInvoke(UriTemplate = "getByEmail", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool GetUser(string email);
    }
}
