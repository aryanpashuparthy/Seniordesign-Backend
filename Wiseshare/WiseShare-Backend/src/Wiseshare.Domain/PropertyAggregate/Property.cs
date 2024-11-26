using System.Dynamic;
using Wiseshare.Domain.Common.Models;
using Wiseshare.Domain.PropertyAggregate.ValueObjects;



namespace Wiseshare.Domain.PropertyAggregate;

public sealed class Property : AggregateRoot<PropertyId, Guid>{
    public string Address {get; private set;}
    public string Location {get; private set;}
    public double PropertyValue {get; private set;}
    public double SharePrice {get; private set;}
    public int AvailableShares {get; private set;}

    public DateTime CreatedDateTime {get; private set;}
    public DateTime UpdatedDateTime {get; private set;}

    private Property(string address, string location, double propertyValue,double sharePrice,
                     int availableShares, PropertyId? propertyId = null)
            : base(propertyId ?? PropertyId.CreateUnique())
    {
        Address = address;
        Location = location;
        PropertyValue = propertyValue;
        SharePrice = sharePrice;
        AvailableShares = availableShares;

    }
    public static Property Create(string address,string location,double propertyValue,double sharePrice, int availableShares)
    {
        return new Property(address,location,propertyValue,sharePrice,availableShares);

    }
}