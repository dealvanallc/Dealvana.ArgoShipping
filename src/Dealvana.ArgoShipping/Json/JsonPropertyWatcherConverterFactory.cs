using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Items;
using Dealvana.ArgoShipping.ReceiptsOfMerchandise;

namespace Dealvana.ArgoShipping.Json
{
    internal class JsonPropertyWatcherConverterFactory : JsonConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, JsonConverter> _converters = new ConcurrentDictionary<Type, JsonConverter>();

        private static readonly HashSet<Type> _typesToConvert = new HashSet<Type>
        {
            typeof(Item),
            typeof(ReceiptOfMerchandise),
            typeof(ReceiptOfMerchandiseItem)
        };

        public override bool CanConvert(Type typeToConvert)
            => _typesToConvert.Contains(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => _converters.GetOrAdd(typeToConvert, CreateConverter);

        private static JsonConverter CreateConverter(Type type)
        {
            var converterType = typeof(JsonPropertyWatcher<>.WatchedPropertyJsonConverter)
                .MakeGenericType(type);

            return Activator.CreateInstance(converterType) as JsonConverter;
        }
    }
}
