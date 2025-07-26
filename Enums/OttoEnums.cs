using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



public static class OttoTokenScopes
{
	public const string Products = "products";
	public const string Orders = "orders";
	public const string Receipts = "receipts";
	public const string Returns = "returns";
	public const string PriceReduction = "price-reduction";
	public const string Shipments = "shipments";
	public const string ShippingProfiles = "shipping-profiles";
	public const string Availability = "availability";
	public const string ReturnsWarehouseRead = "returns-warehouse-read";
	public const string ReturnsWarehouseWrite = "returns-warehouse-write";
}

public enum OttoShippingProvider
{
	DHL,
	DHL_EXPRESS,
	DHL_FREIGHT,
	DHL_HOME_DELIVERY,
	GLS,
	HERMES,
	DPD,
	UPS,
	BTW,
	BURSPED,
	CARGOLINE,
	DACHSER,
	DB_SCHENKER,
	DSV,
	EMONS,
	EPH_DIREKT,
	IDS,
	GEIS,
	GEL,
	GUETTLER,
	HES,
	HELLMANN,
	HEUEL,
	KOCH,
	KUEHNE_NAGEL,
	LOGWIN,
	MEYER_JUMBO,
	RABEN_LOGISTIK,
	RHENUS,
	REIMER,
	REITHMEIER,
	SCHOCKEMOEHLE,
	SCHMIDT_GEVELSBERG,
	ZUFALL,
	OTHER_FORWARDER
}
public enum OttoDeliveryType
{
	PARCEL,
	FORWARDER_PREFERREDLOCATION,
	FORWARDER_CURBSIDE,
	FORWARDER_HEAVYDUTY
}
public enum OttoPositionItemCondition
{
	A, B, C, D, E
}
public enum OttoReturnAcceptanceReason
{
	RETURN_RECEIVED,
	STATUS_SENT_BY_ACCIDENT,
	CANCEL_REQUEST_BY_CUSTOMER,
	REFUND_AFTER_AFFIDAVIT,
	COMPLAINT,
	SHIPMENT_LOST,
	RETURN_LOST,
	UNDELIVERABLE
}
public enum OttoReturnRejectionReason
{
	THIRD_PARTY_ITEM,
	WRONG_ITEM,
	EXCHANGE,
	DAMAGE_TO_THE_HYGIENE_SEAL,
	ITEM_DAMAGED,
	RETURN_PERIOD_EXCEEDED,
	ITEM_NOT_IN_THE_PARCEL
}
public enum OttoMediaAssetType
{
	IMAGE,
	DIMENSIONAL_DRAWING,
	COLOR_VARIANT,
	ENERGY_EFFICIENCY_LABEL,
	MATERIAL_SAMPLE,
	PRODUCT_DATASHEET,
	USER_MANUAL,
	MANUFACTURER_WARRANTY,
	SAFETY_DATASHEET,
	ASSEMBLY_INSTRUCTIONS,
	WARNING_LABEL
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OttoFulfillmentStatus
{
	ANNOUNCED,
	PROCESSABLE,
	SENT,
	RETURNED,
	CANCELLED_BY_PARTNER,
	CANCELLED_BY_MARKETPLACE
}


public enum PaginationLinkRelation
{
	self,
	prev,
	next
}