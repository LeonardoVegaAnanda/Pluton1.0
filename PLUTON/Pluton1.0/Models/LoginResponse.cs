using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluton1_0.Models
{
        public class LoginResponse : Response
        {
            public string SessionId { get; set; }
            public string Version { get; set; }
        }


        public class Message
        {
            public string lang { get; set; }
            public string value { get; set; }
        }
        public class Error
        {
            public int code { get; set; }
            public Message message { get; set; }
        }
        public class Response
        {
            public Error error { get; set; }
            public string DocEntry { get; set; }
        }
    }

