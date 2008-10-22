using System;
using System.Collections.Generic;
using System.Text;

namespace Ra
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class MyAttribute : Attribute
    {
        
    }
}
