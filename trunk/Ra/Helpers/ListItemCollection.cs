/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections;

namespace Ra.Widgets
{
    public class ListItemCollection : ICollection, IList<ListItem>
    {
        private List<ListItem> _list = new List<ListItem>();
        private RaWebControl _control;

        public ListItemCollection(RaWebControl control)
        {
            _control = control;
        }

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array as ListItem[], index);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IList<ListItem> Members

        public int IndexOf(ListItem item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, ListItem item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public ListItem this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        #endregion

        #region ICollection<ListItem> Members

        public void Add(ListItem item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(ListItem item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(ListItem[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ListItem item)
        {
            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<ListItem> Members

        IEnumerator<ListItem> IEnumerable<ListItem>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}
