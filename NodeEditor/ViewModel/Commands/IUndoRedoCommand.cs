using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.ViewModel.Commands
{
    public interface IUndoRedoCommand
    {
        IUndoRedoCommand ExecuteCommand();
        IUndoRedoCommand UnExcecuteCommand();
    }
}
