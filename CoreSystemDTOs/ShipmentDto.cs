public class ShipmentDto
{
    public DateTime CreationDate { get; set; }
    public string ShipmentId { get; set; }

    public TrackingKeyDto TrackingKey { get; set; } = new();
    public List<ShipmentStateDto> States { get; set; } = new();
}

public class TrackingKeyDto
{
    public string Carrier { get; set; }
    public string TrackingNumber { get; set; }
}

public class ShipmentStateDto
{
    public string State { get; set; }
    public DateTime OccurredOn { get; set; }
}
