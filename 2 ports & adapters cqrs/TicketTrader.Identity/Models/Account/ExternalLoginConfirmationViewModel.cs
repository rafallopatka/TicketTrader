using System.ComponentModel.DataAnnotations;

namespace TicketTrader.Identity.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
