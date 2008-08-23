using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;

namespace Entity
{
    [ActiveRecord(Table="Todos")]
    public class Todo : ActiveRecordBase<Todo>
    {
        private int _id;
        private string _header;
        private string _body;
        private DateTime _created;
        private string _typeOfTodo;
        private bool _finished;
        private Operator _operator;

        [BelongsTo]
        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        [Property]
        public bool Finished
        {
            get { return _finished; }
            set { _finished = value; }
        }

        [Property]
        public string TypeOfTodo
        {
            get { return _typeOfTodo; }
            set { _typeOfTodo = value; }
        }

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [Property]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        [Property]
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        [PrimaryKey]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}



























