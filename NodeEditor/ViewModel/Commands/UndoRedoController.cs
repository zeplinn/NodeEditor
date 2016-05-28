using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace NodeEditor.ViewModel.Commands
{
    public class UndoRedoController
    {
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        private readonly Stack<IUndoRedoCommand> _undoStack;
        private readonly Stack<IUndoRedoCommand> _redoStack;
        private readonly Action<ICommand> _onUndoRedoStackChanged;

        protected UndoRedoController(Func<Action, Func<bool>, ICommand> initializeCommand, Action<ICommand> onStackChanged)
        {
            _undoStack = new Stack<IUndoRedoCommand>();
            _redoStack = new Stack<IUndoRedoCommand>();
            UndoCommand = initializeCommand(Undo, () => CanUndo);
            RedoCommand = initializeCommand(Redo, () => CanRedo);
            _onUndoRedoStackChanged = onStackChanged;
        }

       
        public static UndoRedoController CreateInstance<TResult>(
            Expression<Func<Action, Func<bool>, TResult>> initializeCommand
            , Expression<Action<TResult>> onUndoRedoStackChanged
            ) where TResult : class, ICommand
        {
            return new TypedUndoRedoController<TResult>(
                initializeCommand.Compile()
                , onUndoRedoStackChanged.Compile()
                );
        }

        public void HookUpOnCanExecuteChanged(ICommand undoCommand, ICommand redoCommand)
        {
        }



        public bool CanUndo => _undoStack.Any();
        public bool CanRedo => _redoStack.Any();


        public void AddAndExecuteCommand(IUndoRedoCommand command)
        {
            _redoStack.Clear();
            _undoStack.Push(command.ExecuteCommand());
            Update();

        }

        public void Undo() { _redoStack.Push(_undoStack.Pop().UnExcecuteCommand()); Update(); }

        public void Redo() { _undoStack.Push(_redoStack.Pop().ExecuteCommand()); Update(); }

        private void Update()
        {
            _onUndoRedoStackChanged(UndoCommand);
            _onUndoRedoStackChanged(RedoCommand);
        }


        private class TypedUndoRedoController<T> : UndoRedoController
        {
            public TypedUndoRedoController(Func<Action, Func<bool>, ICommand> initializeCommand, Action<T> onUndoRedoStackChanged)
                : base(initializeCommand, c => onUndoRedoStackChanged((T)c))
            {

            }
        }

    }
}
