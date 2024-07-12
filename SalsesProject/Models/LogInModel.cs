using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalsesProject.Models
{
    public class LogInModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PassWord { get; set; }
    }
}
