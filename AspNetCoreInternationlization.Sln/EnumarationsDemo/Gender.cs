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


    public enum StatusE
    {
        Pending = 1,
        Approve = 2,
        Reject = 3
    }
}
