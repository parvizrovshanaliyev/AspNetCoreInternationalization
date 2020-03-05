using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace EnumerationsDemo
{
    public static class EnumExtensions
    {
        public static string ToLocalizedString(this Enum en)
        {
            Type type = en.GetType();


            TypeInfo typeInfo = type.GetTypeInfo();

            EnumResourceAttribute attribute =
                typeInfo.GetCustomAttribute<EnumResourceAttribute>();

            if (attribute != null)
            {
                ResourceManager manager =
                    new ResourceManager(attribute.ResourceType);
                return manager.GetString(en.ToString());
            }

            return en.ToString();
        }
    }
}
