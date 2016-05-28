using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeEditor.View.DocumentComponents
{
    /// <summary>
    /// Interaction logic for NodeContainer.xaml
    /// </summary>
    public partial class NodeContainer : ListBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeLBI();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NodeLBI;
        }
        
        public NodeContainer()
        {
            InitializeComponent();
        }
    }
}
