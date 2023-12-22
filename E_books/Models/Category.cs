using E_books.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace E_books.Models
{
    public class Category: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category Image")]
        [Required(ErrorMessage = "Category image is required")]
        public string ImageUrl { get; set; }
        [Display(Name = "Category Title")]
        [Required(ErrorMessage = "Category name is required")]

        public string Title { get; set; }
        [Display(Name = "Category Description")]
        [Required(ErrorMessage = "Category description is required")]

        public string Description { get; set; }

        //Relationships
        //Category contains a lot of products
        public List<Product> Products { get; set; }
    }
}
