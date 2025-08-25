using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Dealvana.ArgoShipping.Categories;
using Dealvana.ArgoShipping.Items;
using Dealvana.ArgoShipping.Json;
using Dealvana.ArgoShipping.Orders;
using Dealvana.ArgoShipping.ReceiptsOfMerchandise;
using Dealvana.ArgoShipping.Shipments;
using Dealvana.ArgoShipping.ShippingMethods;

namespace Dealvana.ArgoShipping
{
    internal class ArgoShippingService : IArgoShippingService
    {
        #region Fields

        private readonly ArgoShippingSettings _settings;
        private readonly HttpClient _httpClient;

        #endregion

        #region Static Fields

        private static readonly JsonSerializerOptions _serializerOptions;

        #endregion

        #region Static Ctor

        static ArgoShippingService()
        {
            _serializerOptions = new JsonSerializerOptions
            {
#if DEBUG
                WriteIndented = true,
#endif
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            _serializerOptions.Converters.Add(new JsonPropertyWatcherConverterFactory());
        }

        #endregion

        #region Ctor

        public ArgoShippingService(ArgoShippingSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.Username))
            {
                throw new ArgumentException("settings.Username cannot be null or empty", nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.Password))
            {
                throw new ArgumentException("settings.Password cannot be null or empty", nameof(settings));
            }

            _settings = settings;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.Sandbox
                    ? "https://fulfillment-staging.disk.com/api/"
                    : "https://ship.disk.com/api/")
            };
        }

        #endregion

        #region Properties

        public bool IsSandbox => _settings.Sandbox;

        #endregion

        #region Categories

        public Task<CategoriesResponse> ListCategoriesAsync(CancellationToken cancellationToken)
        {
            return GetAsync<ListCategoriesRequest, CategoriesResponse>(new ListCategoriesRequest(), cancellationToken);
        }

        #endregion

        #region Items

        public Task<ItemsResponse> ListItemsAsync(ListItemsRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<ListItemsRequest, ItemsResponse>(request, cancellationToken);
        }

        public Task<ItemsResponse> ListItemsAsync(int? after, int? categoryId, CancellationToken cancellationToken)
        {
            return ListItemsAsync(new ListItemsRequest
            {
                After = after,
                CategoryId = categoryId
            }, cancellationToken);
        }

        public async IAsyncEnumerable<Item> ListItemsEnumerableAsync(int? after, int? categoryId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var itemsResponse = await ListItemsAsync(after, categoryId, cancellationToken).ConfigureAwait(false);
            
            while(itemsResponse != null && itemsResponse.IsSuccess && itemsResponse.Items.Count > 0)
            {
                foreach (var item in itemsResponse.Items)
                {
                    yield return item;
                }

                itemsResponse = await ListItemsAsync(itemsResponse.Items.Last().Id, categoryId, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task<ItemResponse> GetItemByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetAsync<GetItemByIdRequest, ItemResponse>(new GetItemByIdRequest
            {
                Id = id
            }, cancellationToken);
        }

        public Task<ItemResponse> GetItemBySkuAsync(string sku, CancellationToken cancellationToken)
        {
            return GetAsync<GetItemBySkuRequest, ItemResponse>(new GetItemBySkuRequest
            {
                Sku = sku
            }, cancellationToken);
        }

        public Task<ItemResponse> UpdateItemAsync(
            int id,
            Action<PropertySetters<Item>> setProperties,
            CancellationToken cancellationToken)
        {
            var propertySetters = new PropertySetters<Item>();
            setProperties(propertySetters);

            return PostAsync<UpdateItemByIdRequest, ItemResponse>(
                new UpdateItemByIdRequest
                {
                    Id = id,
                    Item = propertySetters.CreateAndApplySetters()
                },
                cancellationToken);
        }

        public Task<ItemResponse> UpdateItemAsync(
            string sku,
            Action<PropertySetters<Item>> setProperties,
            CancellationToken cancellationToken)
        {
            var propertySetters = new PropertySetters<Item>();
            setProperties(propertySetters);

            return PostAsync<UpdateItemBySkuRequest, ItemResponse>(
                new UpdateItemBySkuRequest
                {
                    Sku = sku,
                    Item = propertySetters.CreateAndApplySetters()
                },
                cancellationToken);
        }

        public Task<ItemResponse> UpdateItemAsync<TProperty>(
            int id,
            Expression<Func<Item, TProperty>> propertyExpression,
            TProperty value,
            CancellationToken cancellationToken)
        {
            return UpdateItemAsync(
                id,
                setters => setters.SetProperty(propertyExpression, value),
                cancellationToken);
        }

        public Task<ItemResponse> UpdateItemAsync<TProperty>(
            string sku,
            Expression<Func<Item, TProperty>> propertyExpression,
            TProperty value,
            CancellationToken cancellationToken)
        {
            return UpdateItemAsync(
                sku,
                setters => setters.SetProperty(propertyExpression, value),
                cancellationToken);
        }

        public Task<ItemResponse> UpdateItemAsync(Item item, CancellationToken cancellationToken)
        {
            if (item.Id != null)
            {
                return PostAsync<UpdateItemByIdRequest, ItemResponse>(
                    new UpdateItemByIdRequest
                    {
                        Id = item.Id.Value,
                        Item = item
                    },
                    cancellationToken);
            }
            else if (!string.IsNullOrEmpty(item.Sku))
            {
                return PostAsync<UpdateItemBySkuRequest, ItemResponse>(
                    new UpdateItemBySkuRequest
                    {
                        Sku = item.Sku,
                        Item = item
                    },
                    cancellationToken);
            }
            
            throw new ArgumentException("Item must have either Id or Sku set to be updated", nameof(item));
        }

        public Task<ItemResponse> CreateItemAsync(CreateItemRequest request, CancellationToken cancellationToken)
        {
            return PostAsync<CreateItemRequest, ItemResponse>(request, cancellationToken);
        }

        public Task<ItemResponse> CreateItemAsync(Item item, CancellationToken cancellationToken)
        {
            return CreateItemAsync(
                new CreateItemRequest
                {
                    Item = item
                },
                cancellationToken);
        }

        public Task<ItemResponse> CreateItemAsync(string sku, string name, CancellationToken cancellationToken)
        {
            return CreateItemAsync(
                new CreateItemRequest
                {
                    Item = new Item
                    {
                        Sku = sku,
                        Name = name
                    }
                },
                cancellationToken);
        }

        #endregion

        #region Orders

        public Task<OrderResponse> GetOrderByIdAsync(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<GetOrderByIdRequest, OrderResponse>(request, cancellationToken);
        }

        public Task<OrderResponse> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetOrderByIdAsync(
                new GetOrderByIdRequest
                {
                    Id = id
                },
                cancellationToken);
        }

        public Task<CustomerIdOrderResponse> GetOrderByCustomerIdAsync(GetOrderByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<GetOrderByCustomerIdRequest, CustomerIdOrderResponse>(request, cancellationToken);
        }

        public Task<CustomerIdOrderResponse> GetOrderByCustomerIdAsync(string customerId, CancellationToken cancellationToken)
        {
            return GetOrderByCustomerIdAsync(
                new GetOrderByCustomerIdRequest
                {
                    CustomerOrderId = customerId
                },
                cancellationToken);
        }

        public Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} cannot be nuill", nameof(request));
            }

            if (request.Order == null)
            {
                throw new ArgumentException($"{nameof(request)}.{nameof(request.Order)} cannot be null", $"{nameof(request)}.{nameof(request.Order)}");
            }

            return PostAsync<CreateOrderRequest, OrderResponse>(request, cancellationToken);
        }

        public Task<OrderResponse> CreateOrderAsync(RequestOrder order, CancellationToken cancellationToken)
        {
            return CreateOrderAsync(
                new CreateOrderRequest
                {
                    Order = order
                },
                cancellationToken);
        }

        public Task<OrderResponse> DeleteOrderAsync(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            return DeleteAsync<DeleteOrderRequest, OrderResponse>(request, cancellationToken);
        }

        public Task<OrderResponse> DeleteOrderAsync(int id, CancellationToken cancellationToken)
        {
            return DeleteOrderAsync(
                new DeleteOrderRequest
                {
                    OrderId = id
                },
                cancellationToken);
        }

        #endregion

        #region Receipts of Merchandise

        public Task<ReceiptOfMerchandiseResponse> GetReceiptOfMerchandiseByIdAsync(
            GetReceiptOfMerchandiseByIdRequest request,
            CancellationToken cancellationToken)
        {
            return GetAsync<GetReceiptOfMerchandiseByIdRequest, ReceiptOfMerchandiseResponse>(request, cancellationToken);
        }

        public Task<ReceiptOfMerchandiseResponse> GetReceiptOfMerchandiseByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetReceiptOfMerchandiseByIdAsync(
                new GetReceiptOfMerchandiseByIdRequest
                {
                    ReceiptOfMerchandiseId = id
                },
                cancellationToken);
        }

        public Task<ReceiptsofMerchandiseResponse> ListReceiptsOfMerchandiseAsync(
            ListReceiptsOfMerchandiseRequest request,
            CancellationToken cancellationToken)
        {
            return GetAsync<ListReceiptsOfMerchandiseRequest, ReceiptsofMerchandiseResponse>(request, cancellationToken);
        }

        public Task<ReceiptsofMerchandiseResponse> ListReceiptsOfMerchandiseAsync(
            int page,
            int perPage,
            string status,
            string searchQuery,
            CancellationToken cancellationToken)
        {
            return ListReceiptsOfMerchandiseAsync(
                new ListReceiptsOfMerchandiseRequest
                {
                    Page = page,
                    PerPage = perPage,
                    Status = status,
                    SearchQuery = searchQuery
                },
                cancellationToken);
        }

        public async IAsyncEnumerable<ReceiptOfMerchandise> ListReceiptsOfMerchandiseEnumerableAsync(
            string status,
            string searchQuery,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var request = new ListReceiptsOfMerchandiseRequest
            {
                Page = 1,
                PerPage = 25,
                Status = status,
                SearchQuery = searchQuery
            };

            var receiptsOfMerchandiseResponse =
                await ListReceiptsOfMerchandiseAsync(request, cancellationToken).ConfigureAwait(false);

            while (true)
            {
                foreach (var receiptOfMerchandise in receiptsOfMerchandiseResponse.ReceiptsOfMerchandise)
                {
                    yield return receiptOfMerchandise;
                }

                if (++request.Page > receiptsOfMerchandiseResponse.TotalPages)
                {
                    yield break;
                }

                receiptsOfMerchandiseResponse = await ListReceiptsOfMerchandiseAsync(request, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task<ReceiptOfMerchandiseResponse> CreateReceiptOfMerchandiseAsync(
            CreateReceiptOfMerchandiseRequest request,
            CancellationToken cancellationToken)
        {
            return PostAsync<CreateReceiptOfMerchandiseRequest, ReceiptOfMerchandiseResponse>(request, cancellationToken);
        }

        public Task<ReceiptOfMerchandiseResponse> CreateReceiptOfMerchandiseAsync(
            ReceiptOfMerchandise receiptOfMerchandise,
            CancellationToken cancellationToken)
        {
            return CreateReceiptOfMerchandiseAsync(
                new CreateReceiptOfMerchandiseRequest
                {
                    ReceiptOfMerchandise = receiptOfMerchandise
                },
                cancellationToken);
        }

        #endregion

        #region Shipments

        public Task<ShipmentsResponse> ListShipmentsAsync(ListShipmentsRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<ListShipmentsRequest, ShipmentsResponse>(request, cancellationToken);
        }

        public Task<ShipmentsResponse> ListShipmentsAsync(
            int page,
            int perPage,
            string status,
            int? tenantId,
            string shippingMethod,
            string searchQuery,
            DateTime? startDate,
            DateTime? endDate,
            string dateField,
            CancellationToken cancellationToken)
        {
            return ListShipmentsAsync(new ListShipmentsRequest
            {
                Page = page,
                PerPage = perPage,
                Status = status,
                TenantId = tenantId,
                ShippingMethod = shippingMethod,
                SearchQuery = searchQuery,
                StartDate = startDate,
                EndDate = endDate,
                DateField = dateField
            }, cancellationToken);
        }

        public async IAsyncEnumerable<ShipmentResponse> ListShipmentsEnumerableAsync(
            string status,
            int? tenantId,
            string shippingMethod,
            string searchQuery,
            DateTime? startDate,
            DateTime? endDate,
            string dateField,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var request = new ListShipmentsRequest
            {
                Page = 1,
                PerPage = 100,
                Status = status,
                TenantId = tenantId,
                ShippingMethod = shippingMethod,
                SearchQuery = searchQuery,
                StartDate = startDate,
                EndDate = endDate,
                DateField = dateField
            };

            var shipmentsResponse =
                await ListShipmentsAsync(request, cancellationToken).ConfigureAwait(false);

            while (true)
            {
                foreach (var shipment in shipmentsResponse.Shipments)
                {
                    yield return shipment;
                }

                if (++request.Page > shipmentsResponse.TotalPages)
                {
                    yield break;
                }

                shipmentsResponse = await ListShipmentsAsync(request, cancellationToken).ConfigureAwait(false);
            }
        }

        public Task<ShipmentsResponse> GetShipmentsByCustomerOrderIdAsync(
            GetShipmentsByCustomerIdRequest request,
            CancellationToken cancellationToken)
        {
            return GetAsync<GetShipmentsByCustomerIdRequest, ShipmentsResponse>(request, cancellationToken);
        }

        public Task<ShipmentsResponse> GetShipmentsByCustomerOrderIdAsync(
            string customerOrderId,
            CancellationToken cancellationToken)
        {
            return GetShipmentsByCustomerOrderIdAsync(
                new GetShipmentsByCustomerIdRequest
                {
                    CustomerOrderId = customerOrderId
                }, cancellationToken);
        }

        public Task<ShipmentResponse> GetShipmentByIdAsync(GetShipmentByIdRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<GetShipmentByIdRequest, ShipmentResponse>(request, cancellationToken);
        }

        public Task<ShipmentResponse> GetShipmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            return GetShipmentByIdAsync(new GetShipmentByIdRequest
            {
                Id = id
            }, cancellationToken);
        }

        #endregion

        #region Shipping Methods

        public Task<ShippingMethodsResponse> ListShippingMethodsAsync(ListShippingMethodsRequest request, CancellationToken cancellationToken)
        {
            return GetAsync<ListShippingMethodsRequest, ShippingMethodsResponse>(request ?? new ListShippingMethodsRequest(), cancellationToken);
        }

        #endregion

        #region Utilities

        private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
            where TResponse : BaseResponse, new()
        {
            using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return new TResponse
                {
                    StatusCode = httpResponse.StatusCode,
                    Errors = new List<string>
                    {
                        $"Unsuccessful status code '{httpResponse.StatusCode}'.",
                        await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false)
                    }
                };
            }

            var stream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var response = await JsonSerializer.DeserializeAsync<TResponse>(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
            response.StatusCode = httpResponse.StatusCode;

            return response;
        }

        private HttpRequestMessage CreateRequestMessage<TRequest>(TRequest request, HttpMethod method)
            where TRequest : BaseRequest
        {
            request.Validate();

            var endpoint = request.GetEndpoint();
            var queryString = request.GetQueryString();

            if (!string.IsNullOrEmpty(queryString))
            {
                endpoint = $"{endpoint}?{queryString}";
            }

            var httpRequest = new HttpRequestMessage(method, endpoint);
            httpRequest.Headers.Authorization = GetAuthentication();

            return httpRequest;
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            using var httpRequest = CreateRequestMessage(request, HttpMethod.Post);
            httpRequest.Content = JsonContent.Create(request, options: _serializerOptions);

            return await SendAsync<TResponse>(httpRequest, cancellationToken).ConfigureAwait(false);
        }

        private async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            return await SendAsync<TResponse>(CreateRequestMessage(request, HttpMethod.Get), cancellationToken).ConfigureAwait(false);
        }

        private async Task<TResponse> DeleteAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            return await SendAsync<TResponse>(CreateRequestMessage(request, HttpMethod.Delete), cancellationToken).ConfigureAwait(false);
        }   

        private AuthenticationHeaderValue GetAuthentication()
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}")));
        }

        #endregion
    }
}
