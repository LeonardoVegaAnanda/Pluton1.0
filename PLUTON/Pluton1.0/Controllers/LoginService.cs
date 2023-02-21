using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Pluton1_0.Models;
using System.Net;

namespace Pluton1_0.Controllers
{
    public class LoginService
    {
        public string sessionId { get; set; }
        public LoginService(string session)
        {
            this.sessionId = session;
        }
        public LoginService()
        {

        }
        public string LoginSAP()
        {


            LoginRequest login = new LoginRequest("ANANDA_TEST","70221101" , "TECNO001", "https://199.89.53.35:50000/b1s/v1/Login");

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, SslPolicyErrors) => true;
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", "ApplicationException/json");
            var bodyParam = JsonConvert.SerializeObject(login);
            request.AddParameter("application/json", bodyParam, ParameterType.RequestBody);

            IRestResponse res = cliente.Execute(request);
            var respuesta = JsonConvert.DeserializeObject<LoginResponse>(res.Content);
            var resultado = respuesta.SessionId;
            sessionId = resultado;
            return sessionId;
        }
    }
}
