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
                Users = new ObservableCollection<User>(_context.Users
                    .Include("Manufactory")
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Users = new ObservableCollection<User>();
                for (int i = 1; i < 5; i++)
                    Users.Add(new User());
            }
        }

        private AppDb _context;
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetField(ref _users, value, nameof(Users));
        }
        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => SetField(ref _selectedUser, value, nameof(SelectedUser));
        }
        private ObservableCollection<Manufactory> _manufactories;
        public ObservableCollection<Manufactory> Manufactories
        {
            get => _manufactories ?? (_manufactories = new ObservableCollection<Manufactory>(_context.Manufactories));
            set => SetField(ref _manufactories, value, nameof(Manufactories));
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
            OnPropertyChanged(nameof(Users));
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
            if (SearchText != "")
                Users = new ObservableCollection<User>(_context.Users.Where(u => u.Login.Contains(SearchText)).ToList());
            else
                Users = new ObservableCollection<User>(_context.Users);
        }

        private RelayCommand _deleteUserCommand;
        public RelayCommand DeleteUserCommand
        {
            get
            {
                if (_deleteUserCommand == null)
                    _deleteUserCommand = new RelayCommand(DeleteUser);
                return _deleteUserCommand;
            }
        }
        private void DeleteUser(object obj)
        {
            if (SelectedUser != null && _context.Users.Contains(SelectedUser))
            {
                _context.Users.Remove(SelectedUser);
                SelectedUser = null;
            }
            else
                StateApp.Instance.ChangeText("Пользователь не выбран или уже удален");
        }
    }
}
