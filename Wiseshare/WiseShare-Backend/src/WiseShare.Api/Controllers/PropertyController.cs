using Microsoft.AspNetCore.Mvc;
using FluentResults;
using WiseShare.Api.DTO.Property;
using Wiseshare.Application.Services;
using Wiseshare.Domain.PropertyAggregate;
using Wiseshare.Domain.PropertyAggregate.ValueObjects;

namespace WiseShare.Api.Controllers;


/// Controller for managing properties.
[Route("api/property")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }


    /// Create a new property.
    [HttpPost("create")]
    public IActionResult CreateProperty([FromBody] CreatePropertyRequest request)
    {
        var property = Property.Create(
            address: request.Address,
            location: request.Location,
            propertyValue: request.PropertyValue,
            sharePrice: request.SharePrice,
            availableShares: request.AvailableShares);

        var result = _propertyService.Insert(property);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Message));
        }

        return Ok(new { Message = "Property created successfully", PropertyId = property.Id.Value.ToString() });
    }


    /// Search property by ID.
    [HttpGet("search/id/{id}")]
    public IActionResult GetPropertyById(string id)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            return BadRequest("Invalid PropertyId format.");
        }

        var propertyId = PropertyId.Create(guid);
        var result = _propertyService.GetPropertyById(propertyId);

        if (result.IsFailed)
        {
            return NotFound(result.Errors.Select(e => e.Message));
        }

        return Ok(new PropertyResponse(
            Id: result.Value.Id.Value.ToString(),
            Address: result.Value.Address,
            Location: result.Value.Location,
            PropertyValue: result.Value.PropertyValue,
            SharePrice: result.Value.SharePrice,
            AvailableShares: result.Value.AvailableShares));
    }


    /// Search property by address.
    [HttpGet("search/address/{address}")]
    public IActionResult GetPropertyByAddress(string address)
    {
        var result = _propertyService.GetPropertyByAddress(address);

        if (result.IsFailed)
        {
            return NotFound(result.Errors.Select(e => e.Message));
        }

        return Ok(new PropertyResponse(
            Id: result.Value.Id.Value.ToString(),
            Address: result.Value.Address,
            Location: result.Value.Location,
            PropertyValue: result.Value.PropertyValue,
            SharePrice: result.Value.SharePrice,
            AvailableShares: result.Value.AvailableShares));
    }


    /// Search properties by location.
    [HttpGet("search/location/{location}")]
    public IActionResult GetPropertiesByLocation(string location)
    {
        var result = _propertyService.GetPropertyByLocation(location);

        if (result.IsFailed)
        {
            return NotFound(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value.Select(property => new PropertyResponse(
            Id: property.Id.Value.ToString(),
            Address: property.Address,
            Location: property.Location,
            PropertyValue: property.PropertyValue,
            SharePrice: property.SharePrice,
            AvailableShares: property.AvailableShares)));
    }
}
