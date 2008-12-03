/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RaSelector
{
    /**
     * Helper class for doing selector queries on Controls to retrieve specific 
     * Controls in Page hierarchy. Kind of like a CSS selector only for the server-side.
     * This class can be very handsome in scenarios where you have GridViews or Data Repeators
     * which are dynamically databound and you for some reason cannot use the ID directly or something
     * but still need to traverse server-side Control hierarchy to find a specific control...
     */
    public static class Selector
    {
        /**
         * Generic find delegate to use your own custom find clause
         */
        public delegate bool FindDelegate(Control idx);

        /**
         * Recursively search for Control which matches FindDelegate and returns it as T
         */
        public static T SelectFirst<T>(Control from, FindDelegate del) where T : Control
        {
            if (del(from))
                return from as T;
            foreach (Control idx in from.Controls)
            {
                T tmpRetVal = SelectFirst<T>(idx, del);
                if (tmpRetVal != null)
                    return tmpRetVal;
            }
            return null;
        }

        /**
         * Recursively search int Control hierarcy for the first control that 
         * matches the type of T and returns it as T
         */
        public static T SelectFirst<T>(Control from) where T : Control
        {
            if (from is T)
                return from as T;
            foreach (Control idx in from.Controls)
            {
                T tmpRetVal = SelectFirst<T>(idx);
                if (tmpRetVal != null)
                    return tmpRetVal;
            }
            return null;
        }
    }
}
