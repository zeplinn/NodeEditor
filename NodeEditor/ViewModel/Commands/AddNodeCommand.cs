using NodeEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.ViewModel.Commands
{
    public class AddNodeCommand : IUndoRedoCommand
    {
        private Node _node;
        private ICollection<Node> _nodes;
        public AddNodeCommand(Node node, ICollection<Node> nodes)
        {
            _node = node;
            _nodes = nodes;
        }
        public IUndoRedoCommand ExecuteCommand()
        {
            _nodes.Add(_node);
            return this;
        }

        public IUndoRedoCommand UnExcecuteCommand()
        {
            _nodes.Remove(_node);
            return this;
        }
    }
}
