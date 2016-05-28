using NodeEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Helpers
{
    public class SingleTon<T> where T: class, new()
    {
        private static readonly SingleTon<T> Self = new SingleTon<T>();
        private readonly T _object;
        public static T Obj { get; } = Self._object ;
        
        private SingleTon()
        {
            _object = Self._object ?? new T();
        }
    }
}
