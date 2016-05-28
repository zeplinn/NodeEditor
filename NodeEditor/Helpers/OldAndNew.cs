using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Helpers
{
    public class OldAndNew
    {
        public object OldValue { get; }
        public object NewValue { get; }
        public OldAndNew(object oldValue, object newValue) { OldValue = oldValue; NewValue = newValue; }

    }
    public class OldAndNew<T> : OldAndNew
    {
        public OldAndNew(T oldValue, T newValue) : base(oldValue, newValue) { }

        new public T OldValue { get { return (T)base.OldValue; } }
        new public T NewValue { get { return (T)base.NewValue; } }

    }
}
