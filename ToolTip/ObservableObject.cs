using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToolTip
{
    public class ObservableObject : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetPropertyValue<T>(ref T property, T value, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = "")
        {
            if (!(comparer ?? EqualityComparer<T>.Default).Equals(property, value))
            {
                RaisePropertyChanging(propertyName);

                property = value;

                RaisePropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        protected void RaisePropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
