

public class OrderInsertDto
    {
        public OrderDTO Order { get; set; }
        public List<OrderPositionDTO> Positions { get; set; } = new();
    }
