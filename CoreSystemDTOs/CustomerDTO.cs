
    public class CustomerDTO
    {
    public int CustomerId { get; set; }
    public string CustomerNumber { get; set; } = string.Empty;
    public int? kAccountMarketplaceId { get; set; }
    public int? kMarketplaceId { get; set; }
    public int CustomerGroupId { get; set; }
    public string MarketplaceAccountName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public int OdooId { get; set; }
    public string CustomerGroupName { get; set; } = string.Empty;

    // Optional Address Data
    public int? CustomerAddressId { get; set; } 
    public int AddressType { get; set; }
    public string? CompanyName { get; set; } = string.Empty;
    public string? CompanyAddition { get; set; } = string.Empty;
    public string? Salutation { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? Forename { get; set; } = string.Empty;
    public string? Surname { get; set; } = string.Empty;
    public string? StreetAndHousenumber { get; set; } = string.Empty;
    public string? AddressAddition { get; set; } = string.Empty;
    public string? ZipCode { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public string? EmailAddress { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? MobilePhoneNumber { get; set; } = string.Empty;
    public string? FaxNumber { get; set; } = string.Empty;
    public string? Website { get; set; } = string.Empty;
}

