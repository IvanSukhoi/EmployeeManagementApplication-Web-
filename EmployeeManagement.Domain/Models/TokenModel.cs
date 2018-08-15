namespace EmployeeManagement.Domain.Models
{
    public class TokenModel
    {
        public int Id { get; set; }
        public string JsonWebRefreshToken { get; set; }
        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
