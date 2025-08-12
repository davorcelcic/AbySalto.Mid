using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}