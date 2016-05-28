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

namespace NodeEditor.View.DocumentComponents
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NodeLBI : ListBoxItem
    {
        private bool _isKeyPressed;

        public NodeLBI()
        {
            
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (_isKeyPressed && !IsSelected)
                base.OnMouseLeftButtonDown(e);
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _isKeyPressed = false;
            base.OnMouseLeftButtonUp(e);
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
        }
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _isKeyPressed = e.KeyboardDevice.Modifiers == ModifierKeys.Control;
                base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_isKeyPressed)
            {
                _isKeyPressed = false;
                base.OnKeyUp(e);
            } 
                
        }

    }
}
