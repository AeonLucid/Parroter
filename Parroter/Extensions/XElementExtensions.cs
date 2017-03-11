using System;
using System.Xml.Linq;

namespace Parroter.Extensions
{
    internal static class XElementExtensions
    {
        public static string GetAttribute(this XElement element, string attributeName)
        {
            var attribute = element.Attribute(attributeName)?.Value;
            if (attribute == null)
                throw new NullReferenceException($"Attribute '{attributeName}' was not found in the element '{element.Name}'.");

            return attribute;
        }
    }
}
