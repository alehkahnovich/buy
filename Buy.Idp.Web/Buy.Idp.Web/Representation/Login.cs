using System.ComponentModel.DataAnnotations;

namespace Buy.Idp.Web.Representation
{
    public sealed class Login {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
        public string Client { get; set; }
    }
}