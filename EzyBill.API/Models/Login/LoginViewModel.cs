using System.ComponentModel.DataAnnotations;

namespace EzyBill.API.Models.Login
{
    public class LoginViewModel
    {

        [Display(Name = "User name"), Required]
        public string Username {  get; set; } = string.Empty;
        [DataType(DataType.Password), Display(Name = "Password"), Required]
        public string Password { get; set; } = string.Empty;

    }
}
