using System.Collections.Generic;

namespace THD.Domain.Models.AccountModels.Response
{
    public class JwtAuthentificationResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
        public JwtAuthentificationResponse()
        {
            Roles = new List<string>();
        }
    }
}
