using System;
using System.Collections.Generic;
using System.Text;

namespace Ra
{
    /**
     * Mark your own page methods with this attribute to make it possible to call these methods
     * from your own JavaScript using the Ra-Ajax JavaScript API. This restriction is because of
     * security risks associated with letting JavaScript call any methods on page.
     */
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class WebMethod : Attribute
    {
    }
}
