using System;
using System.Collections.Generic;
using System.Text;

namespace Ra
{
    /**
     * Mark your own page methods with this attribute to make it possible to call these methods
     * from your own JavaScript using the Ra-Ajax JavaScript API. This restriction is because of
     * security risks associated with letting JavaScript call any methods on page.
     * To use WebMethods in your own project you must call them from JavaScript with something
     * looking like this;
     * <pre>
     * Ra.Control.callServerMethod('foo', {
     *   onSuccess: function(retVal) {
     *     alert(retVal);
     *   },
     *   onError: function(status, fullTrace) {
     *     alert(fullTrace);
     *   }
     * }, ['parameter1', 'parameter2', 'etc...');
     * </pre>
     * The above syntax will expect to find a method on your page which is called "foo"
     * and this method must be market with the WebMethod attribute. See below for an
     * example of usage. Notice that unlike ASP.NET AJAX Ra-Ajax can call methods in both 
     * your MasterPage and in UserControls. To call methods in MasterPages you don't need to
     * do anything special. To call methods in UserControls you must prefix the
     * name of the method with the ID of your UserControl. So if your UserControl have
     * an ID of "MyUserControl1" and your method is called "foo" then the fully qualified
     * name to your method in your JavaScript will become "MyUserControl1.foo".
     */
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class WebMethod : Attribute
    {
    }
}
