using System.ComponentModel.DataAnnotations;

namespace THD.Domain.Models.AccountModels.Request
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
