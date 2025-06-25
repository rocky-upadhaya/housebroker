using application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Interfaces.Properties
{
    public interface IPropertyService
    {
        Task<PropertyDto?> GetByIdAsync(int id);
        Task<IEnumerable<PropertyDto>> GetAllAsync();
        Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto);
        Task<PropertyDto?> UpdateAsync(UpdatePropertyDto updatePropertyDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PropertyDto>> SearchAsync(PropertySearchDto searchDto);
    }
}
