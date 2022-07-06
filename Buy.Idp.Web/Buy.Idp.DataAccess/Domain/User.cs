using System;

namespace Buy.Idp.DataAccess.Domain
{
    public sealed class User {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}