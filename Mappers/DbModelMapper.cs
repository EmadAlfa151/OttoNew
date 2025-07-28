using CoreSystem.Shared.DTOs;
using OttoNew.ApiClients.ApiModels.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OttoNew.Mappers
{
    public static class DbModelMapper
    {        
        public static List<ProductDTO> MapToProductDto(List<OttoProductVariation> ottoProdVars)
        {
            if (ottoProdVars is not { Count: > 0}) return new List<ProductDTO>();
            var productsVariationsDictoinary = ottoProdVars.GroupBy(p => p.ProductReference)
                .ToDictionary(g => g.Key, g => g.ToList()); ;


            var productDtos = new List<ProductDTO>();
            foreach ( var productVariationGroup in productsVariationsDictoinary)
            {
                var productReference = productVariationGroup.Key;
                var variations = productVariationGroup.Value;
                if (variations == null || variations.Count == 0) continue;

                var productDto = new ProductDTO
                {
                    Sku = variations.First().Sku, //not usable
                    Name = variations.First().ProductDescription?.Category ?? variations.First().ProductReference,
                    Description = variations.First().ProductDescription?.Description,
                    Variants = MapToVariantsDtoList(variations),
                };
                productDtos.Add(productDto);
            }


            return productDtos;
        }               
        public static List<VariantDto> MapToVariantsDtoList(List<OttoProductVariation> ottoProdVars)
        {
            if (ottoProdVars == null || ottoProdVars.Count == 0) return new List<VariantDto>();

            var variantsDto = new List<VariantDto>();
            foreach (var ottoProdVar in ottoProdVars)
            {
                var variantDto = MapToVariantDto(ottoProdVar);
                if (variantDto != null)
                {
                    variantsDto.Add(variantDto);
                }
            }

            return variantsDto;
        }        
        public static VariantDto MapToVariantDto(OttoProductVariation ottoProdVar)
        {
            if (ottoProdVar == null) return null;


            return new VariantDto
            {
                Sku = ottoProdVar.Sku,
                Barcode = ottoProdVar.Ean,
                NetPrice = ottoProdVar.Pricing?.StandardPrice?.Amount ?? 0,
                Quantity = 0, // Otto doesn't provide quantity in productVariationGroup variations
                Name = ottoProdVar.ProductDescription.Category,

                Attributes = MapProductAttributes(ottoProdVar.ProductDescription?.Attributes)
            };
        }
        private static Dictionary<string, string> MapProductAttributes(List<ProductAttribute>? attributes)
        {
            var attributesDictionary = new Dictionary<string, string>();
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    attributesDictionary[attribute.Name] = string.Join(", ", attribute.Values);
                }
            }
            return attributesDictionary;
        }

        

        public static List<OrderDTO> MapToOrderDtoList(List<OttoOrderResource> ottoOrders)
        {
            if (ottoOrders == null || ottoOrders.Count == 0) return new List<OrderDTO>();
            var orderDtos = new List<OrderDTO>();
            foreach (var ottoOrder in ottoOrders)
            {
                var orderDto = MapToOrderDto(ottoOrder);
                if (orderDto != null)
                {
                    orderDtos.Add(orderDto);
                }
            }
            return orderDtos;
        }

        public static OrderDTO MapToOrderDto(OttoOrderResource ottoOrder)
        {
            if (ottoOrder == null) return null;

            return new OrderDTO
            {
                OrderNumber = ottoOrder.OrderNumber,
                MarketplaceOrderNumber = ottoOrder.SalesOrderId, // This is the Otto DB order ID
                CreationTime = ottoOrder.OrderDate,
                Customer = MapCustomerDto(ottoOrder),
                OrderPositions = MapToOrderPositionsDto(ottoOrder.PositionItems),
            };
        }

        private static List<OrderPositionDTO> MapToOrderPositionsDto(List<OttoPositionItem> positionItems)
        {
            var items = new List<OrderPositionDTO>();
            if (positionItems == null || positionItems.Count == 0) return items;
            foreach (var item in positionItems) 
            {
                var orderPositionDto = new OrderPositionDTO
                {
                    Name = item.Product?.ProductTitle,
                    NetPrice = (double)item.ItemValueReducedGrossPrice.Amount,
                    Vat = (double)item.Product?.VatRate,
                };
                items.Add(orderPositionDto);
            }

            return items;
        }

        public static CustomerDTO MapCustomerDto(OttoOrderResource ottoOrder)
        {
            if (ottoOrder is { DeliveryAddress: null, InvoiceAddress: null}) return null;
            var address = ottoOrder.InvoiceAddress?? ottoOrder.DeliveryAddress;
            var customerDto = new CustomerDTO
            {
                Forename = address?.FirstName,
                Surname = address?.LastName,
                Salutation = address?.Salutation,
                PhoneNumber = address?.PhoneNumber,
                AddressAddition = address?.HouseNumber,
                StreetAndHousenumber = $"{address?.Street} - {address?.HouseNumber}",
                ZipCode = address?.ZipCode,
                City = address?.City,
                Country = address?.CountryCode,
            };

            return customerDto;
        }
    }
}
