using System.ComponentModel.DataAnnotations;

namespace SalsesProject.Models
{
    public class ItemsModel
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName {  get; set; }
        public string Unit {  get; set; }
        public string Category {  get; set; }      
    }
}
