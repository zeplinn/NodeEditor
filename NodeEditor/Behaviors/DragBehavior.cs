using NodeEditor.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace NodeEditor.Behaviors
{
    public class DragBehavior : Behavior<Control>, ICommandSource
    {
        private Point _before;
        private Canvas _canvas;

        //private UIElement _rootElement;
        private double X
        {
            get { return Canvas.GetLeft(this.AssociatedObject); }
            set { Canvas.SetLeft(this.AssociatedObject, value); }
        }

        private double Y
        {
            get { return Canvas.GetTop(this.AssociatedObject); }
            set { Canvas.SetTop(this.AssociatedObject, value); }
        }

        public Hotkey Hotkey
        {
            get { return (Hotkey)GetValue(HotkeyProperty); }
            set { SetValue(HotkeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hotkey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HotkeyProperty =
            DependencyProperty.Register("Hotkey", typeof(Hotkey), typeof(DragBehavior), new PropertyMetadata(new Hotkey()));



        public bool IsCommandEnabled { get; private set; } = true;

        public bool IsAutoCommandParameterEnabled
        {
            get { return (bool)GetValue(IsAutoCommandParameterEnabledProperty); }
            set { SetValue(IsAutoCommandParameterEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAutoCommandParameterEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoCommandParameterEnabledProperty =
            DependencyProperty.Register("IsAutoCommandParameterEnabled", typeof(bool), typeof(DragBehavior), new FrameworkPropertyMetadata(true));

        #region ICommandSource Implementation

        public static readonly DependencyProperty CommandProperty =
                    DependencyProperty.Register("Command", typeof(ICommand), typeof(DragBehavior), new PropertyMetadata((ICommand)null, new PropertyChangedCallback(CommandChanged)));

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
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(DragBehavior), new PropertyMetadata(null));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(DragBehavior), new PropertyMetadata(null));

        #region ICommand Hookup

        // Command dependency property change callback.
        private static void CommandChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            DragBehavior cs = (DragBehavior)d;
            cs.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        // Add a new command to the Command Property.
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            // If oldCommand is not null, then we need to remove the handlers.
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
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
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
            }
        }

        private EventHandler canExecuteChangedHandler;

        // CanExecuteChanged should dictate if the command can execute
        //(ex.  for a button could be:  button.IsEnabled=true/false)
        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                // If a RoutedCommand.
                if (command != null)
                {
                    if (command.CanExecute(CommandParameter, CommandTarget))
                    {
                        IsCommandEnabled = true;
                    }
                    else
                    {
                        IsCommandEnabled = false;
                    }
                }
                // If a not RoutedCommand.
                else
                {
                    if (Command.CanExecute(CommandParameter))
                    {
                        IsCommandEnabled = true;
                    }
                    else
                    {
                        IsCommandEnabled = false;
                    }
                }
            }
        }

        #endregion ICommand Hookup

        protected virtual void ExecuteCommand(object before, object after)
        {
            var param = IsAutoCommandParameterEnabled ? new OldAndNew(before, after) : CommandParameter;
            if (this.Command != null)
            {
                RoutedCommand command = Command as RoutedCommand;

                if (command != null)
                {
                    command.Execute(param, CommandTarget);
                }
                else
                {
                    ((ICommand)Command).Execute(CommandParameter);
                }
            }
        }

        #endregion ICommandSource Implementation

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            AssociatedObject.Loaded += AssociatedObject_Loaded;

            base.OnAttached();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            //_rootElement = findClossestChildOf<Canvas>(AssociatedObject);

            //X = CanvasLeft;
            //Y = CanvasTop;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsCommandEnabled && AssociatedObject.IsMouseCaptured)
            {
                AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
                e.Handled = true;
                //CanvasLeft = X;
                //CanvasTop = Y;
                var after = new Point()
                {
                    X = X,
                    Y = Y
                };
                ExecuteCommand(_before, after);
                AssociatedObject.ReleaseMouseCapture();
            }
        }

        private bool _canMove;

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsCommandEnabled && AssociatedObject.CaptureMouse())
            {
                _before = e.GetPosition(AssociatedObject);
                _canvas = ClossestParentOf<Canvas>(AssociatedObject);
                AssociatedObject.MouseMove += AssociatedObject_MouseMove;
                e.Handled = true;
            }
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canMove)
            {
                var gpos = e.GetPosition(_canvas);
                X = gpos.X - _before.X;
                Y = gpos.Y - _before.Y;
            }
            else
                AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        }

        //private T findClossestChildOf<T>(DependencyObject obj) where T : UIElement
        //{
        //        var parent = VisualTreeHelper.GetParent(obj);
        //        return parent is T ? obj as T : findClossestChildOf<T>(parent);
        //}
        private TP ClossestParentOf<TP>(DependencyObject obj) where TP : DependencyObject
        {
            return obj == null || obj is TP ? (TP)obj : ClossestParentOf<TP>(VisualTreeHelper.GetParent(obj));
        }
    }
}