namespace OneTwenty.Shared.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Signup_date { get; set; }
        public List<string> Interests { get; set; }
    }
}
