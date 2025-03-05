using PackingGM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingGM.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {

        }
        private RelayCommand _openChangeRoleCommand;
        public RelayCommand OpenChangeRoleCommand
        {
            get
            {
                if (_openChangeRoleCommand == null)
                    _openChangeRoleCommand = new RelayCommand(OpenChangeRole);
                return _openChangeRoleCommand;
            }
        }
        private void OpenChangeRole(object obj)
        {
            Navigation.Navigate(new ManageRoleView());
        }

        private RelayCommand _openChangeUserCommand;
        public RelayCommand OpenChangeUserCommand
        {
            get
            {
                if (_openChangeUserCommand == null)
                    _openChangeUserCommand = new RelayCommand(OpenChangeUser);
                return _openChangeUserCommand;
            }
        }
        private void OpenChangeUser(object obj)
        {
            MainWindow main = new MainWindow();
            //main.ShowDialog();
            Navigation.Navigate(new ManageUserView());
        }
    }
}
