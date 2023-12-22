using System.ComponentModel.DataAnnotations;

namespace E_books.Data.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Product Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Product Image is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Product creation date")]
        [Required(ErrorMessage = "creation date is required")]
        public DateTime createdAt { get; set; }

        //Relationships


        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public int CategoryId { get; set; }

        [Display(Name = "Select a supplier")]
        [Required(ErrorMessage = "Product Supplier is required")]
        public int SupplierId { get; set; }
    }
}
