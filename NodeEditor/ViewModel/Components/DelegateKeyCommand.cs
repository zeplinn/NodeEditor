using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace NodeEditor.ViewModel.Components
{
    public class DelegateKeyCommand:DelegateCommand
    {
        public IEnumerable<Key> Hotkey { get; set; }

        public DelegateKeyCommand(Action executeMethod) : this(executeMethod,()=>true)
        {
        }

        public DelegateKeyCommand(Action executeMethod
                                , Func<bool> canExecuteMethod
                                ,IEnumerable<Key> hotkey=null
                                ,bool isKeyReadOnly=false
                                ) : base(executeMethod, canExecuteMethod)
        {
            Hotkey = hotkey ?? new[] {Key.NoName, Key.NoName, Key.NoName};
        }




    }
    
}
