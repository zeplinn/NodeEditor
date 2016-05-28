using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NodeEditor.Behaviors.Helpers
{
    public class HotKeyBinding : Freezable, ICommandSource
    {
        protected override Freezable CreateInstanceCore()
        {
            return new HotKeyBinding();
        }

        #region ICommandSource Implementation

        public static readonly DependencyProperty CommandProperty =
                    DependencyProperty.Register("Command", typeof(ICommand), typeof(HotKeyBinding), new PropertyMetadata(null, CommandChanged));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(HotKeyBinding), new PropertyMetadata(null));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(HotKeyBinding), new PropertyMetadata(null));
        #region ICommand Hookup 

        // Command dependency property change callback.
        private static void CommandChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var cs = (HotKeyBinding)d;
            cs?.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        // Add a new command to the Command Property.
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            if (!(newCommand is IHotkeyAbleCommand)) throw new InvalidCastException($"Command do not implement {nameof(IHotkeyAbleCommand)}");
            // If oldCommand is not null, then we need to remove the handlers.
            if (oldCommand != null) RemoveCommand(oldCommand, newCommand);
            AddCommand(oldCommand, newCommand);
        }

        // Remove an old command from the Command Property.
        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        // Add the command.
        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            _canExecuteChangedHandler = handler;
            if (newCommand == null) return;
            newCommand.CanExecuteChanged += _canExecuteChangedHandler;
        }

        private EventHandler _canExecuteChangedHandler;
        private bool _isExecuteable;
        // CanExecuteChanged should dictate if the command can execute
        //(ex.  for a button could be:  button.IsEnabled=true/false)
        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (Command == null) return;
            _isExecuteable = (Command as RoutedCommand)?.CanExecute(CommandParameter, CommandTarget)
                           ?? Command.CanExecute(CommandParameter);
        }

        #endregion ICommand Hookup
        protected virtual void ExecuteCommand()
        {
            if (!_isExecuteable || Command == null) return;
            var command = Command as RoutedCommand;

            if (command != null) command.Execute(CommandParameter, CommandTarget);
            else Command.Execute(CommandParameter);
        }


        #endregion ICommandSource Implementation
    }
}
