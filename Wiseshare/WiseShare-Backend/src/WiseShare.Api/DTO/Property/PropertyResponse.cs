namespace WiseShare.Api.DTO.Property;

public record PropertyResponse(
    string Id, // PropertyId
    string Address,
    string Location,
    double PropertyValue,
    double SharePrice,
    int AvailableShares
    //DateTime CreatedDateTime,
    //DateTime UpdatedDateTime
);
