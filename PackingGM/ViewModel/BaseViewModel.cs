using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PackingGM.Data;

namespace PackingGM.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            _context = App.GetContext();
        }
        private AppDb _context;

        private protected virtual void SetField<T>(ref T field, T newValue, T publicField)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(nameof(publicField));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual  void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private protected RelayCommand _backCommand;
        public virtual RelayCommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new RelayCommand(Back);
                return _backCommand;
            }
        }

        private void Back(object obj)
        {
            Navigation.Navigate(PageType.MainView);
        }

        //private protected void Save(object obj)
        //{

        //}
    }
}
