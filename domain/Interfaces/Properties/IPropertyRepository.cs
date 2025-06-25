using domain.Entities.Properties;
using domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces.Properties
{
    public interface IPropertyRepository
    {
        Task<Property> GetByIdAsync(int id);
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> CreateAsync(Property property);
        Task<Property> UpdateAsync(Property property);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<Property>> SearchAsync(
            string? location,
            decimal? minPrice,
            decimal? maxPrice,
            PropertyType? type);
    }
}
