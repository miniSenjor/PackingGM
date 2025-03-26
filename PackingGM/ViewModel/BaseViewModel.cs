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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual  void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private protected void Save(object obj)
        //{

        //}
    }
}
