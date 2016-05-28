using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Hotkey Hotkey => new Hotkey(new[] { Key.LeftCtrl, Key.A, Key.D });
        public MainWindow()
        {
            InitializeComponent();
            HotkeyManager.Inst.AttachKeyEvents(this);
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;


        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //text.Text = $"down  {text.Text} \n{HotkeyManager.Inst.IsPressed(Hotkey)}";
            //text2.Text = $"down  {text.Text} \n{HotkeyManager.Inst.IsPressed(Hotkey)}";
            h.Text = $"key:{e.Key}\n all Keys: {HotkeyManager.Inst.PressedKeys}";
        base.OnKeyDown(e);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //text.Text = $"up  {text.Text} \n{HotkeyManager.Inst.IsPressed(Hotkey)}";
            //text2.Text = $"up  {text.Text} \n{HotkeyManager.Inst.IsPressed(Hotkey)}";
            h2.Text = $"key:{e.Key}\n all Keys:{ showList(HotkeyManager.Inst.PressedKeys)}";
            base.OnKeyUp(e);
        }

        private string showList(IEnumerable<Key> l)
        {
            var b = new StringBuilder();
                b.EnsureCapacity(150);
            foreach (Key key in l)
            {
                b.Append(Enum.GetName(typeof(Key),key));
                b.Append(" ");
            }
            return b.ToString();
        }
    }
}
