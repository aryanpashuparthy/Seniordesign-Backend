namespace WiseShare.Api.DTO.Property;

public record CreatePropertyRequest(
    string Address,
    string Location,
    double PropertyValue,
    double SharePrice,
    int AvailableShares
);
