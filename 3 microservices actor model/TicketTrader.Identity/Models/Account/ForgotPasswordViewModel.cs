using System.ComponentModel.DataAnnotations;

namespace TicketTrader.Identity.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
