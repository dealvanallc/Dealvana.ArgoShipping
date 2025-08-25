using System;

namespace Dealvana.ArgoShipping
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PathParameterAttribute : Attribute
    {
        public PathParameterAttribute(string parameterName)
        {
            Parameter = parameterName;
        }

        public string Parameter { get; }
    }
}
