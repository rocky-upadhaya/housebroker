using application.DTO;
using application.Interfaces.Properties;
using application.Mapper;
using domain.Enums;
using domain.Interfaces.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Services.Properties
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyDto?> GetByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return property != null ? PropertyMapper.ToDto(property) : null;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllAsync()
        {
            var properties = await _propertyRepository.GetAllAsync();
            return PropertyMapper.ToDtoList(properties);
        }

        public async Task<PropertyDto> CreateAsync(CreatePropertyDto createPropertyDto)
        {
            var property = PropertyMapper.ToEntity(createPropertyDto);
            var createdProperty = await _propertyRepository.CreateAsync(property);
            return PropertyMapper.ToDto(createdProperty);
        }

        public async Task<PropertyDto?> UpdateAsync(UpdatePropertyDto updatePropertyDto)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(updatePropertyDto.Id);
            if (existingProperty == null)
                return null;

            var updatedProperty = PropertyMapper.ToEntity(updatePropertyDto, existingProperty);
            var result = await _propertyRepository.UpdateAsync(updatedProperty);
            return PropertyMapper.ToDto(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _propertyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PropertyDto>> SearchAsync(PropertySearchDto searchDto)
        {
            // Convert string type to enum if provided
            PropertyType? propertyType = null;
            if (!string.IsNullOrEmpty(searchDto.Type))
            {
                propertyType = Enum.Parse<PropertyType>(searchDto.Type);
            }

            var properties = await _propertyRepository.SearchAsync(
                searchDto.Location,
                searchDto.MinPrice,
                searchDto.MaxPrice,
                propertyType
            );

            return PropertyMapper.ToDtoList(properties);
        }
    }

}
