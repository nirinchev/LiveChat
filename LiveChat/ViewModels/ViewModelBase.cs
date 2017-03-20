using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiveChat
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                Set(ref _isBusy, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (field?.Equals(value) != true)
            {
                field = value;
                RaisePropertyChanged(property);
                return true;
            }

            return false;
        }

        protected void HandleError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }           
    }
}
