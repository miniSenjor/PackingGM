using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PackingGM.Data;
using PackingGM.Model;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            _context = App.GetContext();
            BaseModel.StaticPropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CurrentUser.User))
                {
                    OnPropertyChanged(nameof(IsAlowedViewing));
                    OnPropertyChanged(nameof(IsAlowedWriting));
                    OnPropertyChanged(nameof(IsAlowedAdmining));
                }
            };
        }
        private AppDb _context;

        private protected virtual void SetField<T>(ref T field, T newValue, string publicField)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(publicField);
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

        protected virtual void Back(object obj)
        {
            Navigation.Navigate(PageType.MainView);
        }
        
        public Visibility IsAlowedViewing
        {
            get
            {
                //OnPropertyChanged(nameof(IsAlowedViewing));
                return CurrentUser.GetVisibility("IsAlowedViewing");
            }
        }
        public Visibility IsAlowedWriting
        {
            get
            {
                //OnPropertyChanged(nameof(IsAlowedWriting));
                return CurrentUser.GetVisibility("IsAlowedWriting");
            }
        }
        public Visibility IsAlowedAdmining
        {
            get
            {
                //OnPropertyChanged(nameof(IsAlowedAdmining));
                return CurrentUser.GetVisibility("IsAlowedAdmining");
            }
        }
        //private protected void Save(object obj)
        //{

        //}
    }
}
