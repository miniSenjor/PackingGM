using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class ChangeUserWindowModel : BaseViewModel
    {
        public ChangeUserWindowModel()
        {
            try
            {
                using (AppDb db = new AppDb())
                {
                    Manufactories = new ObservableCollection<Manufactory>(db.Manufactories);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            TextHeading = "Создание нового пользователя";
        }
        public ChangeUserWindowModel(User user) : this()
        {
            //Не реализовано
            User = user;
            TextHeading = "Редактирование пользователя " + User.Login;
        }
        private AppDb _context = App.GetContext();
        public string TextHeading { get; set; }
        public User User { get; set; } = new User();
        public ObservableCollection<Manufactory> Manufactories { get; set; }

        private RelayCommand _saveUserCommand;
        public RelayCommand SaveUserCommand
        {
            get
            {
                if (_saveUserCommand == null)
                    _saveUserCommand = new RelayCommand(SaveUser);
                return _saveUserCommand;
            }
        }
        private void SaveUser(object obj)
        {
            try
            {
                _context.Users.Add(User);
                _context.SaveChanges();
                MessageBox.Show("Успешно сохранено");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = entry.GetDatabaseValues();
                var clientValues = entry.Entity;

                if (databaseValues == null)
                {
                    MessageBox.Show("Редактируемая запись была удалена другим пользователем.");
                }
                else
                {
                    var updatedRole = (Role)databaseValues.ToObject();
                    MessageBox.Show($"Запись была изменена другим пользователем. Текущие данные:{updatedRole.Name} Просмотр{updatedRole.IsAlowedViewing} Редактирование{updatedRole.IsAlowedWriting} Администрирование{updatedRole.IsAlowedAdmining}");
                    // Обновите данные на клиенте
                    entry.OriginalValues.SetValues(databaseValues);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
