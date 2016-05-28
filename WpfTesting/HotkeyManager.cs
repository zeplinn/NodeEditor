using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfTesting
{
    public class SingleTon<T> where T : class, new()
    {
        private static readonly T Self = new T();
        public static T Inst { get; } = Self;
        
        protected SingleTon()
        {
        }
    }
    public class HotkeyManager : SingleTon<HotkeyManager>
    {
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        private static extern bool NativeGetKeyboardState([Out] byte[] keyStates);
        private static bool GetKeyboardState(byte[] keyStates)
        {
            if (keyStates == null)
                throw new ArgumentNullException("keyState");
            if (keyStates.Length != 256)
                throw new ArgumentException("The buffer must be 256 bytes long.", "keyState");
            return NativeGetKeyboardState(keyStates);
        }
        private static byte[] GetKeyboardState()
        {
            byte[] keyStates = new byte[256];
            if (!GetKeyboardState(keyStates))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return keyStates;
        }

        private static bool AnyKeyPressed()
        {
            byte[] keyState = GetKeyboardState();
            // skip the mouse buttons
            return keyState.Skip(8).Any(state => (state & 0x80) != 0);
        }


        private readonly LinkedList<Key> _pressedKeys = new LinkedList<Key>();
        private HashSet<Key> _IsPressed => new HashSet<Key>();
        public ICollection<Key> PressedKeys => _pressedKeys;
        
        public void AttachKeyEvents(UIElement ele)
        {
            ele.KeyDown += OnKeyDown;
            ele.KeyUp += OnKeyUp;
        }
        
        //private Key _lastKeyLifted= Key.NoName;
        

        private bool IsModifier(Key key)
        {
            const bool t = true;
            switch (key)
            {
                case Key.LeftCtrl: return t;
                case Key.LeftShift: return t;
                case Key.LeftAlt: return t;
                case Key.RightCtrl: return t;
                case Key.RightShift: return t;
                case Key.RightAlt: return t;
                default:
                    return t;
            }
        }
        
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_IsPressed.Contains(e.Key)) return;
            _IsPressed.Add(e.Key);
            if (IsModifier(e.Key))
            {
                
            }
                
                && _pressedKeys.Last.Value) _pressedKeys.First() (IsModifier).
            _pressedKeys.AddLast(e.Key);

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            _IsPressed.Remove(e.Key);
            if (IsModifier(e.Key)) _IsPressed.RemoveWhere(key => !IsModifier(key));

        }

        public bool IsPressed(Hotkey hotkey)
        {
            return hotkey.SequenceEqual(_pressedKeys);
        }
        private void ExecuteCommand(FrameworkElement iSourceCommand)
        {
            if (!(iSourceCommand is ICommandSource)) return;
            var iCmd = iSourceCommand as ICommandSource;
            var command = iCmd.Command;
            if (command == null) return;
            if (command is RoutedCommand)
            {
                (command as RoutedCommand).Execute(iCmd.CommandParameter, iCmd.CommandTarget);
            }
            else
            {
                ((ICommand)command).Execute(iCmd.CommandParameter);
            }
        }
    }
}
