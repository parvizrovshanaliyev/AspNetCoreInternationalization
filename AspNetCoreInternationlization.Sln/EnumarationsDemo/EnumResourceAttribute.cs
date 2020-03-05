using System;
using System.Collections.Generic;
using System.Text;

namespace EnumerationsDemo
{
    public class EnumResourceAttribute : Attribute
    {
        public Type ResourceType { get; }

        public EnumResourceAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }
    }
}
