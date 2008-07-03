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
using System.Web.UI;

namespace Ra.Widgets
{
    public class ListItemCollection : ICollection, IList<ListItem>, IStateManager
    {
        private List<ListItem> _list = new List<ListItem>();
        private RaControl _control;

        public ListItemCollection(RaControl control)
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
            _control.SignalizeReRender();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _control.SignalizeReRender();
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
                _control.SignalizeReRender();
            }
        }

        #endregion



        #region ICollection<ListItem> Members

        public void Add(ListItem item)
        {
            _list.Add(item);
            _control.SignalizeReRender();
        }

        public void Clear()
        {
            _list.Clear();
            _control.SignalizeReRender();
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
            _control.SignalizeReRender();
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
                idxItem.Selected = (bool)listItemViewState[1];
                idxItem.Text = listItemViewState[2].ToString();
                idxItem.Value = listItemViewState[3].ToString();
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
                object[] listItemViewState = new object[4];
                listItemViewState[0] = idxItem.Enabled;
                listItemViewState[1] = idxItem.Selected;
                listItemViewState[2] = idxItem.Text;
                listItemViewState[3] = idxItem.Value;
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
