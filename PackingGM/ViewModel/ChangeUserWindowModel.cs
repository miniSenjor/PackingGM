using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
                    Roles = new ObservableCollection<Role>(db.Roles.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            TextHeading = "Создание нового пользователя";
        }
        public ChangeUserWindowModel(User user) : base()
        {
            User = user;
            TextHeading = "Редактирование пользователя " + User.Login;
        }
        public User User { get; set; }
        public ObservableCollection<Role> Roles { get; set; }
        public string TextHeading { get; set; }
        //public Role Role { get; set; }
    }
}
