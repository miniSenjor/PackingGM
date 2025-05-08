using PackingGM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingGM.ViewModel
{
    public class MainViewModel : BaseViewModel
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
            //Navigation.Navigate(PageType.ManageRoleView);
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
            Navigation.Navigate(PageType.ManageUserView);
        }
        private RelayCommand _openChangeGraphCommand;
        public RelayCommand OpenChangeGraphCommand
        {
            get
            {
                if (_openChangeGraphCommand == null)
                    _openChangeGraphCommand = new RelayCommand(OpenChangeGraph);
                return _openChangeGraphCommand;
            }
        }
        private void OpenChangeGraph(object obj)
        {
            Navigation.Navigate(PageType.ManageGraphView);
        }
        private RelayCommand _openChangeGraphD3Command;
        public RelayCommand OpenChangeGraphD3Command
        {
            get
            {
                if (_openChangeGraphD3Command == null)
                    _openChangeGraphD3Command = new RelayCommand(OpenChangeGraphD3);
                return _openChangeGraphD3Command;
            }
        }
        private void OpenChangeGraphD3(object obj)
        {
            Navigation.Navigate(PageType.ManageGraphD3View);
        }
        private RelayCommand _openTestCommand;
        public RelayCommand OpenTestCommand
        {
            get
            {
                if (_openTestCommand == null)
                    _openTestCommand = new RelayCommand(OpenTest);
                return _openTestCommand;
            }
        }
        private void OpenTest(object obj)
        {
            Navigation.Navigate(PageType.TestView);
        }
        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new RelayCommand(Exit);
                return _exitCommand;
            }
        }
        private void Exit(object obj)
        {
            Navigation.Navigate(PageType.LoginView);
            Model.CurrentUser.User = null;
        }
    }
}
