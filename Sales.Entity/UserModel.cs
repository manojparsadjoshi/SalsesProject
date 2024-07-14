using System.ComponentModel.DataAnnotations;

namespace SalsesProject.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        public string Username {  get; set; }

        public string Password { get; set; }
        public string ? Roless {  get; set; }
    }
}
