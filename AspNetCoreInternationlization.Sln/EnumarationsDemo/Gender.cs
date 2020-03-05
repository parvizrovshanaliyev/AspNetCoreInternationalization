using System;
using System.Collections.Generic;
using System.Text;
using EnumerationsDemo.Resources;

namespace EnumerationsDemo
{
    [EnumResource(typeof(GenderEnumResource))]
    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}
