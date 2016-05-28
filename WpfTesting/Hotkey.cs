using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfTesting
{
    public class Hotkey : IEnumerable<Key>, IEqualityComparer<Hotkey>

    {
        private int _hashCode;
        public IEnumerable<Key> Keys { get; }

        public bool IsPressed(KeyEventArgs e) => Keys.All(key => e.KeyboardDevice.IsKeyDown(key));

        public Hotkey(IEnumerable<Key> hotkey)
        {
            Keys = hotkey;
            _hashCode = Keys.Sum(key => (int)key);
        }

        public IEnumerator<Key> GetEnumerator()
        {
            foreach (Key key in Keys)
            {
                yield return key;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Hotkey x, Hotkey y)
        {
            if (x.Count() == y.Count())
            {
                var nd = y.GetEnumerator();
                return x.All(key =>
                {
                    nd.MoveNext();
                    return key == nd.Current;
                });
            }
            else return false;
        }

        public int GetHashCode(Hotkey obj)
        {
            return _hashCode;
        }
    }
}
