namespace WiseShare.Api.DTO.Property;

public record UpdatePropertyRequest(
    string Id, // PropertyId
    string Address,
    string Location,
    double PropertyValue,
    double SharePrice,
    int AvailableShares
);
