using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Dealvana.ArgoShipping.Categories;
using Dealvana.ArgoShipping.Items;
using Dealvana.ArgoShipping.Orders;
using Dealvana.ArgoShipping.ReceiptsOfMerchandise;
using Dealvana.ArgoShipping.Shipments;
using Dealvana.ArgoShipping.ShippingMethods;

namespace Dealvana.ArgoShipping
{
    public interface IArgoShippingService
    {
        #region Properties

        bool IsSandbox { get; }

        #endregion

        #region Categories

        Task<CategoriesResponse> ListCategoriesAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Items

        Task<ItemsResponse> ListItemsAsync(ListItemsRequest request, CancellationToken cancellationToken = default);

        Task<ItemsResponse> ListItemsAsync(int? after = null, int? categoryId = null, CancellationToken cancellationToken = default);

        IAsyncEnumerable<Item> ListItemsEnumerableAsync(int? after = null, int? categoryId = null, CancellationToken cancellationToken = default);

        Task<ItemResponse> GetItemByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<ItemResponse> GetItemBySkuAsync(string sku, CancellationToken cancellationToken = default);

        Task<ItemResponse> UpdateItemAsync(
            int id,
            Action<PropertySetters<Item>> setProperties,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> UpdateItemAsync(
            string sku,
            Action<PropertySetters<Item>> setProperties,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> UpdateItemAsync(Item item, CancellationToken cancellationToken = default);

        Task<ItemResponse> UpdateItemAsync<TProperty>(
            int id,
            Expression<Func<Item, TProperty>> property,
            TProperty value,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> UpdateItemAsync<TProperty>(
            string sku,
            Expression<Func<Item, TProperty>> property,
            TProperty value,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> CreateItemAsync(
            CreateItemRequest request,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> CreateItemAsync(
            Item item,
            CancellationToken cancellationToken = default);

        Task<ItemResponse> CreateItemAsync(
            string sku,
            string name,
            CancellationToken cancellationToken = default);

        #endregion

        #region Orders

        Task<OrderResponse> GetOrderByIdAsync(
            GetOrderByIdRequest request,
            CancellationToken cancellationToken = default);

        Task<OrderResponse> GetOrderByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<CustomerIdOrderResponse> GetOrderByCustomerIdAsync(
            GetOrderByCustomerIdRequest request,
            CancellationToken cancellationToken = default);

        Task<CustomerIdOrderResponse> GetOrderByCustomerIdAsync(
            string customerOrderId,
            CancellationToken cancellationToken = default);

        Task<OrderResponse> CreateOrderAsync(
            CreateOrderRequest request,
            CancellationToken cancellationToken = default);

        Task<OrderResponse> CreateOrderAsync(
            RequestOrder order,
            CancellationToken cancellationToken = default);

        Task<OrderResponse> DeleteOrderAsync(
            DeleteOrderRequest request,
            CancellationToken cancellationToken = default);

        Task<OrderResponse> DeleteOrderAsync(
            int id,
            CancellationToken cancellationToken = default);

        #endregion

        #region Receipts Of Merchandise

        Task<ReceiptOfMerchandiseResponse> GetReceiptOfMerchandiseByIdAsync(
            GetReceiptOfMerchandiseByIdRequest request,
            CancellationToken cancellationToken = default);

        Task<ReceiptOfMerchandiseResponse> GetReceiptOfMerchandiseByIdAsync(
            int receiptOfMerchandiseId,
            CancellationToken cancellationToken = default);

        Task<ReceiptsofMerchandiseResponse> ListReceiptsOfMerchandiseAsync(
            ListReceiptsOfMerchandiseRequest request,
            CancellationToken cancellationToken = default);

        Task<ReceiptsofMerchandiseResponse> ListReceiptsOfMerchandiseAsync(
            int page = 1,
            int perPage = 10,
            string status = null,
            string searchQuery = null,
            CancellationToken cancellationToken = default);

        IAsyncEnumerable<ReceiptOfMerchandise> ListReceiptsOfMerchandiseEnumerableAsync(
            string status = null,
            string searchQuery = null,
            CancellationToken cancellationToken = default);

        Task<ReceiptOfMerchandiseResponse> CreateReceiptOfMerchandiseAsync(
            CreateReceiptOfMerchandiseRequest request,
            CancellationToken cancellationToken = default);

        Task<ReceiptOfMerchandiseResponse> CreateReceiptOfMerchandiseAsync(
            ReceiptOfMerchandise receiptOfMerchandise,
            CancellationToken cancellationToken = default);

        #endregion

        #region Shipments

        Task<ShipmentsResponse> ListShipmentsAsync(
            ListShipmentsRequest request,
            CancellationToken cancellationToken = default);

        Task<ShipmentsResponse> ListShipmentsAsync(
            int page = 1,
            int perPage = 10,
            string status = null,
            int? tenantId = null,
            string shippingMethod = null,
            string searchQuery = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string dateField = null,
            CancellationToken cancellationToken = default);

        IAsyncEnumerable<ShipmentResponse> ListShipmentsEnumerableAsync(
            string status = null,
            int? tenantId = null,
            string shippingMethod = null,
            string searchQuery = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string dateField = null,
            CancellationToken cancellationToken = default);

        Task<ShipmentsResponse> GetShipmentsByCustomerOrderIdAsync(
            GetShipmentsByCustomerIdRequest request,
            CancellationToken cancellationToken = default);

        Task<ShipmentsResponse> GetShipmentsByCustomerOrderIdAsync(
            string customerOrderId,
            CancellationToken cancellationToken = default);

        Task<ShipmentResponse> GetShipmentByIdAsync(
            GetShipmentByIdRequest request,
            CancellationToken cancellationToken = default);

        Task<ShipmentResponse> GetShipmentByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        #endregion

        #region Shipping Methods

        Task<ShippingMethodsResponse> ListShippingMethodsAsync(ListShippingMethodsRequest request = null, CancellationToken cancellationToken = default);

        #endregion
    }
}
