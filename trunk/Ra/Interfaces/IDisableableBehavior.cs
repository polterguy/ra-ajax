/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

using System;
namespace Ra.Widgets
{
    /**
     * Interface defining a Behavior that can be enabled or disabled through using the Enabled property.
     */
    interface IDisableableBehavior
    {
        /**
         * Determines whether a Behviour that implements this interface is enabled or disabled.
         */ 
        bool Enabled { get; set; }
    }
}
