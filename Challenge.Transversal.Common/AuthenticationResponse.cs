using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Transversal.Common
{
    public class AuthenticationResponse<T>
    {
        public T Data { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
