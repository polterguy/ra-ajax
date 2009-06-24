/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;

/**
 * Main namespace for Ra-Ajax and eveything within
 */
namespace Ra
{
    /**
     * Interface defining Ra-Ajax controls, implement on all of your Ra-Ajax extension controls which 
     * have their own JavaScript files.
     */
    public interface IRaControl
    {
        /**
         * Called internally by framework (Ra-Ajax) when event occurs from client.
         */
        void DispatchEvent(string name);
    }
}
