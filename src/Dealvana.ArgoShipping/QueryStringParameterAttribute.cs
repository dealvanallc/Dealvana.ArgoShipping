using System;

namespace Dealvana.ArgoShipping
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryStringParameterAttribute : Attribute
    {
        public QueryStringParameterAttribute(string parameterName)
        {
            Parameter = parameterName;
        }

        public string Parameter { get; }
    }
}
