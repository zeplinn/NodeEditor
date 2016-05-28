using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace NodeEditor.Behaviors
{
    public class ListReferenceBehavior:Behavior<ItemsControl>
    {
        private INotifyPropertyChanged _property;
        private INotifyCollectionChanged _collection;

        protected override void OnAttached()
        {
            _collection = AssociatedObject.ItemsSource as INotifyCollectionChanged;
            _property = AssociatedObject.ItemsSource as INotifyPropertyChanged;
            if (_property == null || _collection == null)
                throw new ArgumentNullException(
                    $"Item source do not implement {nameof(INotifyPropertyChanged)} or {nameof(INotifyCollectionChanged)}");
            _property.PropertyChanged += ListReferenceBehavior_PropertyChanged;
            _collection.CollectionChanged += ListReferenceBehavior_CollectionChanged;
            base.OnAttached();
        }
        protected override void OnDetaching()
        {
            _property.PropertyChanged -= ListReferenceBehavior_PropertyChanged;
            _collection.CollectionChanged -= ListReferenceBehavior_CollectionChanged;
            _property = null;
            _collection = null;
            base.OnDetaching();
        }

        private void ListReferenceBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAdd(e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnRemove(e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    OnReplace(e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    OnMove(e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    OnReset(e);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnReset(NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnMove(NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnReplace(NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void OnRemove(NotifyCollectionChangedEventArgs e)
        {
                
        }

        private void OnAdd(NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void ListReferenceBehavior_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
