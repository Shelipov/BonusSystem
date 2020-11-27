using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Helpers
{
    public static class EnumExtensions
    {
        public static string GetValueAttribute(this IConvertible value)
        {
            return value.GetAttribute<ValueAttribute>()?.Value;
        }

        public static TAttribute GetAttribute<TAttribute>(this IConvertible value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}
