using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NodeEditor.Helpers
{
    public class Hotkey
    {
        public IEnumerable<Key> Keys { get; set; } = new[] {Key.NoName};

        public bool IsPressed(KeyEventArgs e) => Keys.All(key => e.KeyboardDevice.IsKeyDown(key));
        
    }
}
