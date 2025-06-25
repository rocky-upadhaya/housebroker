using domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.DTO
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public class CreatePropertyDto
    {
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [EnumDataType(typeof(PropertyType), ErrorMessage = "Type must be either 'Land' or 'Building'")]
        public string Type { get; set; } = string.Empty;
    }



    public class UpdatePropertyDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [EnumDataType(typeof(PropertyType), ErrorMessage = "Type must be either 'Land' or 'Building'")]
        public string Type { get; set; } = string.Empty;
    }


    public class PropertySearchDto
    {
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string? Location { get; set; }

        [Range(0, 9999999, ErrorMessage = "MinPrice must be greater than or equal to 0")]
        public decimal? MinPrice { get; set; }

        [Range(0, 10000000, ErrorMessage = "MaxPrice must be greater than or equal to 0")]
        public decimal? MaxPrice { get; set; }

        [EnumDataType(typeof(PropertyType), ErrorMessage = "Type must be either 'Land' or 'Building'")]
        public string? Type { get; set; }

        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (MinPrice.HasValue && MaxPrice.HasValue && MinPrice > MaxPrice)
            {
                errors.Add("MinPrice cannot be greater than MaxPrice");
            }

            return errors.Count == 0;
        }
    }

}
