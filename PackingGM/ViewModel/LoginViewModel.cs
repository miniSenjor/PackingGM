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
using System.Windows.Media;
using System.Data.SqlClient;

namespace PackingGM.ViewModel
{
    public class LoginViewModel : BaseModel
    {
        public LoginViewModel()
        {
            //string connString = "Server=gt-srv30;Database=gt;User Id=1587;Password=YD30K2D5;";

            ////Создание подключения
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    try
            //    {
            //        // Открываем соединение
            //        conn.Open();
            //        string sql1 = "SELECT [NMK_NAME] FROM[NMK] where NMK.NMK_NOTE = R59800698";
            //        string sql2 = "SELECT @@VERSION AS Version";
            //        // Выполнение SQL-запроса
            //        using (SqlCommand cmd = new SqlCommand(sql1, conn))
            //        {
            //            // Выполнение запроса и получение результата
            //            //string version = cmd.ExecuteScalar().ToString();

            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    MessageBox.Show($"Версия {reader["Version"]}");
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            //return;
            try
            {
                _context = App.GetContext();
                _isConnected = true;
                _worker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                _worker.DoWork += Worker_DoWork;
                _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private AppDb _context;
        private readonly BackgroundWorker _worker;
        private bool _isConnected = false;
        public User LogUser { get; set; } = new User();

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(CanLogin)); // Обновляем состояние кнопки
            }
        }
        public bool CanLogin => !IsBusy;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string hash = HashPassword(LogUser.Password);
                User currentUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Login == LogUser.Login && u.Password == /*hash*//**/LogUser.Password/**/);
                if (currentUser != null)
                {
                    CurrentUser.User = currentUser;
                    e.Result = true;
                }
                else
                {
                    e.Result = new InvalidOperationException("Не правильно веден логин или пароль");
                }
            }
            catch (TimeoutException ex)
            {
                e.Result = new TimeoutException("Время ответа превышено. Попробуйте повторить операцию чуть позже");
            }
            catch (InvalidOperationException ex)
            {
                e.Result = ex;
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                StateApp.Instance.ChangeAll("Ошибка: " + e.Error.Message, "red");
            }
            else if (e.Result is Exception ex)
            {
                MessageBox.Show(ex.ToString());
                StateApp.Instance.ChangeAll("Ошибка: " + ex.Message, "red");

            }
            else if ((bool)e.Result)
            {
                StateApp.Instance.ChangeAll("Успешный вход!", "blue");
                Navigation.Navigate(PageType.MainView);
            }
        }

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
            if (!_isConnected)
            {
                Navigation.Navigate(PageType.MainView);
                return;
            }
            if (string.IsNullOrEmpty(LogUser.Login) || string.IsNullOrEmpty(LogUser.Password))
            {
                MessageBox.Show("Не введен логин или пароль");
                return;
            }
            IsBusy = true;
            StateApp.Instance.ChangeAll("Проверка пользователя...", "blue");
            _worker.RunWorkerAsync();
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
