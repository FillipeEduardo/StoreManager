using System.ComponentModel.DataAnnotations;

namespace StoreManager.DTOs.InputModels
{
    public class ProductInputModel
    {
        [Required(ErrorMessage = "\"name\" is required")]
        [MinLength(5, ErrorMessage = "\"name\" length must be at least 5 characters long")]
        public string? Name { get; set; }
    }
}
