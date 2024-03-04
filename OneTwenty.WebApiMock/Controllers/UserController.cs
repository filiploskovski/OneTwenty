using Microsoft.AspNetCore.Mvc;
using OneTwenty.Shared.Models;

namespace OneTwenty.WebApiMock.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<UserModel> Get()
        {
            return Users();
        }

        private List<UserModel> Users()
        {
            return new List<UserModel>
            {
                new()
                {
                    Name = "Alex Johnson",
                    Email = "alex.johnson@example.com",
                    Signup_date = DateTime.Parse("2024-01-15"),
                    Interests = new List<string> { "coding", "crypto" }
                },
                new()
                {
                    Name = "Casey Smith",
                    Email = "casey.smith@example.com",
                    Signup_date = DateTime.Parse("2024-01-20"),
                    Interests = new List<string> { "music", "gardening" }
                },
                new()
                {
                    Name = "Jordan Lee",
                    Email = "jordan.lee@example.com",
                    Signup_date = DateTime.Parse("2024-02-05"),
                    Interests = new List<string> { "travel", "books" }
                },
                new()
                {
                    Name = "Taylor Brown",
                    Email = "taylor.brown@example.com",
                    Signup_date = DateTime.Parse("2024-02-10"),
                    Interests = new List<string> { "fitness", "fashion" }
                },
                new()
                {
                    Name = "Alex Johnson",
                    Email = "alex.johnson@example.com",
                    Signup_date = DateTime.Parse("2024-01-15"),
                    Interests = new List<string> { "coding", "blockchain" }
                }
            };
        }
    }
}
