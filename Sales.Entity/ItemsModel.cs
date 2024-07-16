using System.ComponentModel.DataAnnotations;

namespace SalsesProject.Models
{
    public class ItemsModel
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Unit { get; set; }

        public Category Category { get; set; }
    }
    public enum Category
    {
        Fruits = 1,
        Vegetables,
        Drinks
    }
}
