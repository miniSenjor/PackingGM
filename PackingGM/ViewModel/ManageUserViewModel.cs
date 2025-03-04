using PackingGM.Data;
using PackingGM.Model;
using PackingGM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class ManageUserViewModel : BaseViewModel
    {
        public ManageUserViewModel()
        {
            try
            {
                _context = App.GetContext();
                Users = new ObservableCollection<User>(_context.Users.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Users = new ObservableCollection<User>();
                for (int i = 1; i < 5; i++)
                    Users.Add(new User(i));
            }
        }

        private AppDb _context;
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public string SearchText { get; set; }
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(Save);
                return _saveCommand;
            }
        }

        private void Save(object obj)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Успешно сохранено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private RelayCommand _createUserCommand;
        public RelayCommand CreateUserCommand
        {
            get
            {
                if (_createUserCommand == null)
                    _createUserCommand = new RelayCommand(CreateUser);
                return _createUserCommand;
            }
        }

        private void CreateUser(object obj)
        {
            ChangeUserWindow changeUserWindow = new ChangeUserWindow();
            changeUserWindow.ShowDialog();
        }

        private RelayCommand _searchUserCommand;
        public RelayCommand SearchUserCommand
        {
            get
            {
                if (_searchUserCommand == null)
                    _searchUserCommand = new RelayCommand(SearchUser);
                return _searchUserCommand;
            }
        }
        private void SearchUser(object obj)
        {
            Users = new ObservableCollection<User>(_context.Users.Where(u => u.Login.Contains(SearchText)).ToList());
        }

        private RelayCommand _backCommand;
        public RelayCommand BackCommand
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
            Navigation.Navigate(new MainView());
        }
    }
}
