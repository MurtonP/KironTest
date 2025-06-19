using Microsoft.EntityFrameworkCore;

namespace KironTestWebAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
