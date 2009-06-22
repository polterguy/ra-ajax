/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ra.Extensions.Helpers
{
    // This is the "driver" for our Comet Control
    // This one just contains the logic to "lock" until an Event is raised and then fetch that
    // Event out of the queue and return back to the caller...
    internal class CometQueue
    {
        // This on is the "heart" of the CometQueue
        // This is the object that makes it possible to "lock" and wait
        private ManualResetEvent _resetEvent;
        private List<string> _events = new List<string>();

        public CometQueue()
        {
            _resetEvent = new ManualResetEvent(false);
        }

        public string WaitForNextMessage(string lastEvent, int timeout)
        {
            if (lastEvent != null && _events.Count > 0 && lastEvent != _events[_events.Count - 1])
            {
                // This bugger must be locked to ensure none inserts a new event while we're looking
                // for the previous one...
                lock (this)
                {
                    // Trick to find the index "one offset after" the one that matches...
                    // (if any)
                    bool found = false;
                    string retVal = _events.Find(
                        delegate(string idx)
                        {
                            if (found)
                                return true;
                            if (idx == lastEvent)
                                found = true;
                            return false;
                        });

                    // There was another event after the last one, therefor we can
                    // return that event immediately
                    // This can happen if there was an event raised while we established 
                    // a new connection from the server
                    // Without this logic we run the risk of "loosing" events that are
                    // raised while the client is "off line" and in that "off line"
                    // window a new event is raised...
                    if (retVal != null)
                        return retVal;
                }

                // No new event, just waiting for the "next one"...
                return WaitOne(timeout);
            }
            else if (lastEvent == null || _events.Count == 0 || lastEvent == _events[_events.Count - 1])
            {
                // Waiting for our next event...
                return WaitOne(timeout);
            }
            return null;
        }

        private string WaitOne(int timeout)
        {
            // TODO: Should we lock here...?
            // Go through entire logic later to verify correctness... :|
            _resetEvent.Reset();
            if (_resetEvent.WaitOne(new TimeSpan(0, 0, timeout), true))
            {
                lock (this)
                {
                    return _events[_events.Count - 1];
                }
            }
            return null;
        }

        // Note that this method expects a UNIQUE string as the message name
        // This can be obtained by e.g. using Guid.New().ToString() if you
        // have no other methods of obtaining a unique string...
        public void SignalizeNewEvent(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Can't post empty message");
            lock (this)
            {
                if (_events.Exists(
                    delegate(string idx)
                    {
                        return idx == message;
                    }))
                    throw new ArgumentException("Event name already exists in CometQueue");
                _events.Add(message);
            }
            _resetEvent.Set();
        }
    }
}
