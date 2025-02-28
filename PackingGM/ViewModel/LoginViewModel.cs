using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Diagnostics;
using PackingGM.Model;
using PackingGM.Data;
using PackingGM.View;

namespace PackingGM.ViewModel
{
    public class LoginViewModel : BaseModel
    {
        public LoginViewModel()
        {
            try
            {
                _context = App.GetContext();
                _isConnected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private bool _isConnected = false;
        public User LogUser { get; set; } = new User();
        private AppDb _context;
        private RelayCommand _enterCommand;
        public RelayCommand EnterCommand
        {
            get
            {
                if (_enterCommand == null)
                    _enterCommand = new RelayCommand(Enter);
                return _enterCommand;
            }
        }
        private void Enter(object obj)
        {
            //HashPassword("admin");
            try
            {
                if (!_isConnected)
                {
                    Navigation.Navigate(new MainView());
                    return;
                }


                if ("" == LogUser.Login || "" == LogUser.Password)
                {
                    MessageBox.Show("Не введен логин или пароль");
                    return;
                }
                string hash = HashPassword(LogUser.Password);
                Debug.Print(LogUser.Login + LogUser.Password);
                User currentUser = _context.Users.First(u => u.Login == LogUser.Login && u.Password == /*hash*//**/LogUser.Password/**/);
                if (currentUser != null)
                {
                    CurrentUser.User = currentUser;
                    Navigation.Navigate(new MainView());
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Время ответа сервера превышено. Попробуйте повторить операцию чуть позже");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Не правильный логин или пароль");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string HashPassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hash = sha256Hash.ComputeHash(data);

                // Преобразуем байтовый массив в шестнадцатеричную строку
                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                    hex.AppendFormat("{0:x2}", b);
                //Debug.Print(hex.ToString());
                return hex.ToString();
            }
        }
    }
}
