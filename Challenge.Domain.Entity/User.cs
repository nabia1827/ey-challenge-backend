using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserImage { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool UserActive { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
