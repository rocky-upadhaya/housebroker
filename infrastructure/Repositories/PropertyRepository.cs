using domain.Entities.Properties;
using domain.Enums;
using domain.Interfaces.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property> CreateAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property> UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
                return false;

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Properties.AnyAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<Property>> SearchAsync(
            string? location,
            decimal? minPrice,
            decimal? maxPrice,
            PropertyType? type)
        {
            var query = _context.Properties.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(p => p.Location.ToLower().Contains(location.ToLower()));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (type.HasValue)
            {
                query = query.Where(p => p.Type == type.Value);
            }

            // Return top 100 results ordered by Id
            return await query.OrderBy(p => p.Id).Take(100).ToListAsync();
        }
    }
}
