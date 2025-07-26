using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OttoMiddleware.Dto
{
    public class ReturnOrderDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isSelected;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [JsonPropertyName("returnId")]
        public string ReturnId { get; set; } = string.Empty;

        [JsonPropertyName("salesOrderId")]
        public string SalesOrderId { get; set; } = string.Empty;

        [JsonPropertyName("orderNumber")]
        public string OrderNumber { get; set; } = string.Empty;

        [JsonPropertyName("returnDate")]
        public DateTime? ReturnDate { get; set; }

        [JsonPropertyName("returnReason")]
        public string ReturnReason { get; set; } = string.Empty;

        [JsonPropertyName("customerComment")]
        public string CustomerComment { get; set; } = string.Empty;

        [JsonPropertyName("returnStatus")]
        public string ReturnStatus { get; set; } = string.Empty;

        [JsonPropertyName("positionItems")]
        public List<ReturnItemDto> Items { get; set; } = new();

        [JsonPropertyName("customer")]
        public ReturnCustomerDto Customer { get; set; } = new();

        // UI-specific properties
        public bool IsSelected 
        { 
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        public string DisplayStatus => ReturnStatus switch
        {
            "ANNOUNCED" => "📋 Announced",
            "RECEIVED" => "📦 Received", 
            "ACCEPTED" => "✅ Accepted",
            "REJECTED" => "❌ Rejected",
            "PROCESSED" => "✔️ Processed",
            _ => $"❓ {ReturnStatus}"
        };
    }

    public class ReturnItemDto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _isSelected;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [JsonPropertyName("positionItemId")]
        public string PositionItemId { get; set; } = string.Empty;

        [JsonPropertyName("returnQuantity")]
        public int ReturnQuantity { get; set; }

        [JsonPropertyName("returnReason")]
        public string ReturnReason { get; set; } = string.Empty;

        [JsonPropertyName("product")]
        public ReturnProductDto Product { get; set; } = new();

        [JsonPropertyName("expectedDeliveryDate")]
        public DateTime? ExpectedDeliveryDate { get; set; }

        public bool IsSelected 
        { 
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
    }

    public class ReturnProductDto
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; } = string.Empty;

        [JsonPropertyName("productTitle")]
        public string ProductTitle { get; set; } = string.Empty;

        [JsonPropertyName("articleNumber")]
        public string ArticleNumber { get; set; } = string.Empty;

        [JsonPropertyName("ean")]
        public string Ean { get; set; } = string.Empty;
    }

    public class ReturnCustomerDto
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();
    }

    public class ReturnAcceptanceRequestDto
    {
        [JsonPropertyName("returnId")]
        public string ReturnId { get; set; } = string.Empty;

        [JsonPropertyName("positionItems")]
        public List<ReturnItemActionDto> Items { get; set; } = new();
    }

    public class ReturnItemActionDto
    {
        [JsonPropertyName("positionItemId")]
        public string PositionItemId { get; set; } = string.Empty;

        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty; // "ACCEPT" or "REJECT"

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class ReturnResponseDto
    {
        [JsonPropertyName("returnId")]
        public string ReturnId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("processedAt")]
        public DateTime ProcessedAt { get; set; }
    }

    public class ReturnListResponseDto
    {
        [JsonPropertyName("resources")]
        public List<ReturnOrderDto> Resources { get; set; } = new();

        [JsonPropertyName("links")]
        public List<LinkDto> Links { get; set; } = new();
    }

    public class LinkDto
    {
        [JsonPropertyName("href")]
        public string Href { get; set; } = string.Empty;

        [JsonPropertyName("rel")]
        public string Rel { get; set; } = string.Empty;
    }
}