using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Json
{
    public abstract class JsonPropertyWatcher<T>
        where T : JsonPropertyWatcher<T>, new()
    {
        private bool _trackingPaused;

        private readonly HashSet<string> _changedProperties = new HashSet<string>();

        public JsonPropertyWatcher()
        {
        }

        public JsonPropertyWatcher(IEnumerable<string> alwaysChangedProperties)
        {
            foreach (var prop in alwaysChangedProperties)
            {
                _ = _changedProperties.Add(prop);
            }
        }

        protected TProp PropertyChanged<TProp>(TProp value, [CallerMemberName] string propertyName = "")
        {
            if (!_trackingPaused)
            {
                _ = _changedProperties.Add(propertyName);
            }

            return value;
        }

        private void ForcePropertyChanged(string propertyName)
        {
            _ = _changedProperties.Add(propertyName);
        }

        private void PauseChangeTracking()
        {
            _trackingPaused = true;
        }

        private void ResumeChangeTracking()
        {
            _trackingPaused = false;
        }

        internal sealed class WatchedPropertyJsonConverter : JsonConverter<T>
        {
            private static readonly Dictionary<string, PropertyInfo> _properties =
                typeof(T).GetProperties()
                .Select(p => (p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name, p))
                .Where(p => p.Name != null)
                .ToDictionary(p => p.Name, p => p.p);

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var obj = new T();
                obj.PauseChangeTracking();

                reader.Read();
                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    var propertyName = reader.GetString();
                    if (_properties.TryGetValue(propertyName, out var propertyInfo))
                    {
                        var propertyValue = JsonSerializer.Deserialize(ref reader, propertyInfo.PropertyType, options);
                        propertyInfo.SetValue(obj, propertyValue);

                        if (propertyValue != null)
                        {
                            obj.ForcePropertyChanged(propertyInfo.Name);
                        }
                    }
                    else
                    {
                        _ = JsonSerializer.Deserialize(ref reader, typeof(object), options);
                    }

                    reader.Read();
                }

                obj.ResumeChangeTracking();
                return obj;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach (var (propertyName, property) in _properties)
                {
                    var propertyValue = property.GetValue(value, null);
                    if (propertyValue == null && !value._changedProperties.Contains(property.Name))
                    {
                        continue;
                    }

                    writer.WritePropertyName(propertyName);
                    if (propertyValue == null)
                    {
                        writer.WriteNullValue();
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
}
