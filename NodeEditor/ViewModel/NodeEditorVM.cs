using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using NodeEditor.Model;
using Microsoft.Practices.Prism.Commands;
using NodeEditor.DummyObjects;
using System.Windows.Input;
using NodeEditor.ViewModel.Commands;

namespace NodeEditor.ViewModel
{
    public class NodeEditorVM:BaseViewModel
    {
        public ObservableCollection<Node> Nodes => new ObservableCollection<Node>(Init.InitNodes(5));
        public ObservableCollection<Connector> Lines => new ObservableCollection<Connector>();

        public DelegateCommand AddNodeCommand => new DelegateCommand(AddNode);
        public ICommand UndoCommand => _undoController.UndoCommand;
        public ICommand RedoCommand => _undoController.RedoCommand;
        private readonly UndoRedoController _undoController;
        

        public NodeEditorVM()
        {
            _undoController = UndoRedoController.CreateInstance((a, b) => new DelegateCommand(a, b)
                                                                    ,d=> d.RaiseCanExecuteChanged());


        }
        

        public void AddNode()
        {
            _undoController.AddAndExecuteCommand(new AddNodeCommand(new Node() { X = 300, Y = 300 }, Nodes));
            
        }

    }
}
