using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Dealvana.ArgoShipping
{
    public class PropertySetters<TSource>
        where TSource : class, new()
    {
        private readonly List<(LambdaExpression Property, ConstantExpression Value)> _setters = new List<(LambdaExpression Property, ConstantExpression Value)>();

        public PropertySetters<TSource> SetProperty<TProperty>(
            Expression<Func<TSource, TProperty>> propertyExpression,
            TProperty value)
        {
            ValidatePropertyExpression(propertyExpression);

            _setters.Add((propertyExpression, Expression.Constant(value, typeof(object))));

            return this;
        }

        public void ApplySetters(TSource obj)
        {
            foreach (var (propertyExpression, valueExpression) in _setters)
            {
                var propertyInfo = (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;
                propertyInfo.SetValue(obj, valueExpression.Value);
            }
        }

        public TSource CreateAndApplySetters()
        {
            var obj = new TSource();
            ApplySetters(obj);
            return obj;
        }

        private static void ValidatePropertyExpression(LambdaExpression propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException($"{nameof(propertyExpression)}.Body must be {nameof(MemberExpression)}", nameof(propertyExpression));
            }

            _ = memberExpression.Member as PropertyInfo
                ?? throw new ArgumentException($"{nameof(propertyExpression)}.Body is {nameof(MemberExpression)}, but is not property");
        }
    }
}
