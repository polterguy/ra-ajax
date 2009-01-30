/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI;

namespace Ra.Widgets
{
    public class ListItemCollection : ICollection, IList<ListItem>, IStateManager
    {
        private List<ListItem> _list = new List<ListItem>();
        private SelectList _control;

        public ListItemCollection(SelectList control)
        {
            _control = control;
        }

        public ListItem Find(Predicate<ListItem> functor)
        {
            foreach (ListItem idx in _list)
            {
                if (functor(idx))
                    return idx;
            }
            return null;
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
            get { return _list; }
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
            item.SelectList = this._control as SelectList;
            _control.ReRender();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _control.ReRender();
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
                value.SelectList = this._control as SelectList;
                _control.ReRender();
            }
        }

        #endregion



        #region ICollection<ListItem> Members

        public void Add(ListItem item)
        {
            _list.Add(item);
            item.SelectList = this._control as SelectList;
            _control.ReRender();
        }

        public void Clear()
        {
            _list.Clear();
            _control.ReRender();
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
            _control.ReRender();
            return _list.Remove(item);
        }

        #endregion



        #region IEnumerable<ListItem> Members

        IEnumerator<ListItem> IEnumerable<ListItem>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IStateManager Members

        private bool _trackingViewState;
        public bool IsTrackingViewState
        {
            get { return _trackingViewState; }
        }

        public void LoadViewState(object state)
        {
            _list.Clear();
            object[] values = state as object[];
            int count = (int)values[0];
            for (int idx = 0; idx < count; idx++)
            {
                object[] listItemViewState = values[idx + 1] as object[];
                ListItem idxItem = new ListItem();
                idxItem.Enabled = (bool)listItemViewState[0];
                idxItem.Text = listItemViewState[1].ToString();
                idxItem.Value = listItemViewState[2].ToString();
                _list.Add(idxItem);
            }
        }

        public object SaveViewState()
        {
            object[] retVal = new object[Count + 1];
            retVal[0] = Count;
            int idxNo = 1;
            foreach (ListItem idxItem in this)
            {
                object[] listItemViewState = new object[3];
                listItemViewState[0] = idxItem.Enabled;
                listItemViewState[1] = idxItem.Text;
                listItemViewState[2] = idxItem.Value;
                retVal[idxNo++] = listItemViewState;
            }
            return retVal;
        }

        public void TrackViewState()
        {
            _trackingViewState = true;
        }

        #endregion
    }
}
