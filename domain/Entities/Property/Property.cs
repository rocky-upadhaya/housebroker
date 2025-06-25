using domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities.Properties
{
    public class Property
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PropertyType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
