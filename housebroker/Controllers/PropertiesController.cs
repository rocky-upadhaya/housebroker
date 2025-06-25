using application.DTO;
using application.Interfaces.Properties;
using domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace housebroker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<ActionResult<PropertyDto>> GetById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound($"Property with ID {id} not found.");

            return Ok(property);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<ActionResult<PropertyDto>> Create([FromBody] CreatePropertyDto createPropertyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProperty = await _propertyService.CreateAsync(createPropertyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProperty.Id }, createdProperty);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<ActionResult<PropertyDto>> Update(int id, [FromBody] UpdatePropertyDto updatePropertyDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProperty = await _propertyService.UpdateAsync(updatePropertyDto);
            if (updatedProperty == null)
                return NotFound($"Property with ID {id} not found.");

            return Ok(updatedProperty);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Broker))]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _propertyService.DeleteAsync(id);
            if (!result)
                return NotFound($"Property with ID {id} not found.");

            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = $"{nameof(UserRole.Broker)},{nameof(UserRole.Customer)}")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> Search([FromQuery] PropertySearchDto searchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Custom validation
            if (!searchDto.IsValid(out var customErrors))
            {
                foreach (var error in customErrors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }

            var result = await _propertyService.SearchAsync(searchDto);
            return Ok(result);
        }
    }
}
