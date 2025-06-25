using application.DTO;
using domain.Entities.Properties;
using domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Mapper
{
    public static class PropertyMapper
    {
        public static PropertyDto ToDto(Property property)
        {
            return new PropertyDto
            {
                Id = property.Id,
                Location = property.Location,
                Price = property.Price,
                Type = property.Type.ToString(),
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt
            };
        }

        public static Property ToEntity(CreatePropertyDto createDto)
        {
            return new Property
            {
                Location = createDto.Location,
                Price = createDto.Price,
                Type = Enum.Parse<PropertyType>(createDto.Type),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static Property ToEntity(UpdatePropertyDto updateDto, Property existingProperty)
        {
            existingProperty.Location = updateDto.Location;
            existingProperty.Price = updateDto.Price;
            existingProperty.Type = Enum.Parse<PropertyType>(updateDto.Type);
            existingProperty.UpdatedAt = DateTime.UtcNow;

            return existingProperty;
        }

        public static IEnumerable<PropertyDto> ToDtoList(IEnumerable<Property> properties)
        {
            return properties.Select(ToDto);
        }
    }
}
