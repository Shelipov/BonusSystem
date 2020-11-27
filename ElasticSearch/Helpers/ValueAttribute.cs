using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueAttribute : Attribute
    {
        public string Value { get; }

        public ValueAttribute(string value)
        {
            Value = value;
        }
    }
}
