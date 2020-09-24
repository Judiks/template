using System.Collections.Generic;

namespace THD.Domain.Models.AccountModels.Response
{
    public class GetAllUserResponse
    {
        public List<UserResponse> Users { get; set; }
        public GetAllUserResponse()
        {
            Users = new List<UserResponse>();
        }
    }
}
