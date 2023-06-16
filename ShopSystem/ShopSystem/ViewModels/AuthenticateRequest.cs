using System.ComponentModel.DataAnnotations;

namespace ShopSystem.ViewModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string UName { get; set; }
        [Required]
        public string UPassword { get; set; }
    }
}
