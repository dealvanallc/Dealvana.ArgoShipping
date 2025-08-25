using System.Linq;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Orders
{
    /// <summary>
    ///     Provides a response wrapper for orders queried by customer order ID.
    /// </summary>
    /// <remarks>
    ///     The <see cref="GetOrderByCustomerIdRequest"/> may return multiple orders for a single customer order ID,
    ///     this can happen when a single customer order id is used to create multiple orders.
    /// </remarks>
    public class CustomerIdOrderResponse : OrdersResponse
    {
        /// <summary>
        ///     Gets a value indicating whether multiple orders were found for the specified customer order ID.
        /// </summary>
        [JsonIgnore]
        public bool HasMultipleOrders 
            => Orders != null && Orders.Count > 1;

        /// <summary>
        ///     Gets a value indicating the first order associated with the specified customer order ID.
        /// </summary>
        [JsonIgnore]
        public OrderResponse Order
            => Orders?.First();
    }
}
