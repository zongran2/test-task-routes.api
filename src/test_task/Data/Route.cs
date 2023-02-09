namespace TestTask.Data;

public class Route
{
    // Mandatory
    // Identifier of the whole route
    public Guid Id { get; set; }

    // Mandatory
    // Start point of route
    public string Origin { get; set; }

    // Mandatory
    // End point of route
    public string Destination { get; set; }

    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }

    // Mandatory
    // End date of route
    public DateTime DestinationDateTime { get; set; }

    // Mandatory
    // Price of route
    public decimal Price { get; set; }

    // Mandatory
    // Timelimit. After it expires, route became not actual
    public DateTime TimeLimit { get; set; }

    public override int GetHashCode()
    {
        return (Origin, Destination, DestinationDateTime, OriginDateTime, Price, TimeLimit).GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Route route && Equals((Route)obj);
    }

    public bool Equals(Route other)
    {
       return (Origin, Destination, DestinationDateTime, OriginDateTime, Price, TimeLimit)
              .Equals(
       (other.Origin, other.Destination, other.DestinationDateTime, other.OriginDateTime, other.Price, other.TimeLimit));
    }
}