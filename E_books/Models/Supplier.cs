using E_books.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace E_books.Models
{
    public class Supplier: IEntityBase
    {
        [Key] 
        public int Id { get; set; }

        [Display(Name = "Supplier Image")]
        [Required(ErrorMessage = "Supplier image is required")]
        public string ImageUrl { get; set; }
        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 chars")]
        public string Name { get; set; }
        [Display(Name = "Supplier Description")]
        [Required(ErrorMessage = "Supplier description is required")]
        public string Description { get; set; }
         
        //RelationShips
        //Supplier can supply me a lot of products
        public List<Product> Products { get; set; }
    }
}
