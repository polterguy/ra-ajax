/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
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
