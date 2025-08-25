using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Dealvana.ArgoShipping
{
    public abstract class BaseRequest
    {
        private static readonly ConcurrentDictionary<Type, List<Tuple<PropertyInfo, QueryStringParameterAttribute>>> _queryStringPropeties 
            = new ConcurrentDictionary<Type, List<Tuple<PropertyInfo, QueryStringParameterAttribute>>>();

        private static readonly ConcurrentDictionary<Type, List<Tuple<PropertyInfo, PathParameterAttribute>>> _pathProperties
            = new ConcurrentDictionary<Type, List<Tuple<PropertyInfo, PathParameterAttribute>>>();

        internal abstract string Endpoint { get; }

        internal string GetQueryString()
        {
            var queryStringProperties = _queryStringPropeties.GetOrAdd(GetType(), GetQueryStringProperties);
            var queryValues = new Dictionary<string, string>();

            foreach (var property in queryStringProperties)
            {
                var value = property.Item1.GetValue(this);
                if (value == null)
                {
                    continue;
                }

                var valueString = value is DateTime dateTime
                    ? dateTime.ToString("yyyy-MM-dd HH:mm:ss")
                    : value.ToString();

                queryValues[property.Item2.Parameter] = HttpUtility.UrlEncode(valueString);
            }

            return string.Join("&", queryValues.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        internal string GetEndpoint()
        {
            var endpointProperties = _pathProperties.GetOrAdd(GetType(), GetPathProperties);
            var endpoint = Endpoint;

            foreach (var (property, attribute) in endpointProperties)
            {
                var value = property.GetValue(this)?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException($"Path property {attribute.Parameter} requires non-null value");
                }

                endpoint = endpoint.Replace($"{{{attribute.Parameter}}}", value);
            }

            return endpoint;
        }

        internal virtual void Validate()
        {
        }

        private static List<Tuple<PropertyInfo, QueryStringParameterAttribute>> GetQueryStringProperties(Type type)
        {
            return type.GetProperties()
                .Select(p => new Tuple<PropertyInfo, QueryStringParameterAttribute>(p, p.GetCustomAttribute<QueryStringParameterAttribute>()))
                .Where(t => t.Item2 != null)
                .ToList();
        }

        private static List<Tuple<PropertyInfo, PathParameterAttribute>> GetPathProperties(Type type)
        {
            return type.GetProperties()
                .Select(p => new Tuple<PropertyInfo, PathParameterAttribute>(p, p.GetCustomAttribute<PathParameterAttribute>()))
                .Where(t => t.Item2 != null)
                .ToList();
        }
    }
}
