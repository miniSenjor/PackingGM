using PackingGM.Data;
using PackingGM.Model;
using PackingGM.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Npgsql;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Data.Entity;

namespace PackingGM.ViewModel
{
    public class ManageRoleViewModel : BaseViewModel
    {
        public ManageRoleViewModel()
        {
            try
            {
                _context = App.GetContext();
                Roles = new ObservableCollection<Role>(_context.Roles.ToList());
                //using (AppDb db = new AppDb())
                //{
                //    Roles = new ObservableCollection<Role>(db.Roles.ToList());
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //сделать только если нет подключения к бд
                Roles = new ObservableCollection<Role>();
                for (int i = 1; i < 5; i++)
                    Roles.Add(new Role(i));
            }
        }

        private ObservableCollection<Role> _roles;
        public ObservableCollection<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        private AppDb _context;
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
                //using (AppDb db = new AppDb())
                //{
                //    db.Entry(Roles).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                _context.SaveChanges();
                MessageBox.Show("Успешно сохранено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private RelayCommand _createRoleCommand;
        public RelayCommand CreateRoleCommand
        {
            get
            {
                if (_createRoleCommand == null)
                    _createRoleCommand = new RelayCommand(CreateRole);
                return _createRoleCommand;
            }
        }

        private void CreateRole(object obj)
        {

        }
        //private ManageTheme mt = new ManageTheme();
        private RelayCommand _changeThemeCommand;
        public RelayCommand ChangeThemeCommand
        {
            get
            {
                if (_changeThemeCommand == null)
                    _changeThemeCommand = new RelayCommand(ChangeTheme);
                return _changeThemeCommand;
            }
        }

        private void ChangeTheme(object obj)
        {
            //if (mt.CurrentTheme == "Light")
            //    mt.CurrentTheme = "Dark";
            //else mt.CurrentTheme = "Light";
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
        private void con()
        {
            try
            {
                // добавление данных
                using (AppDb db = new AppDb())
                {
                    // создаем два объекта User
                    Role user1 = new Role { Name = "Tom" };
                    Role user2 = new Role { Name = "Alice" };

                    // добавляем их в бд
                    //db.Role.AddRange(user1, user2);
                    db.SaveChanges();
                }
                // получение данных
                using (AppDb db = new AppDb())
                {
                    // получаем объекты из бд и выводим на консоль
                    //var roles = db.Role.ToList();
                    //foreach (Role u in roles)
                    //{
                    //    MessageBox.Show(u.Name);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            string connString = "Host=localhost;Port=5433;Username=postgres;Password=38tn4WbR94;Database=Graph";

            //Создание подключения
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    // Открываем соединение
                    conn.Open();

                    // Выполнение SQL-запроса
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT version();", conn))
                    {
                        // Выполнение запроса и получение результата
                        string version = cmd.ExecuteScalar().ToString();
                        MessageBox.Show("PostgreSQL version: " + version);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}