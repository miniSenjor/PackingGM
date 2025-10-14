using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class CurrentUser : BaseModel
    {
        private static User _user;
        public static User User
        {
            get => _user;
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnStaticPropertyChanged(nameof(User));
                }
            }
        }
        public static System.Windows.Visibility GetVisibility(string field)
        {
            bool visibility = false;
            try
            {
                switch (field)
                {
                    case "IsAlowedAdmining":
                        visibility = User.IsAlowedAdmining;
                        break;
                    case "IsAlowedWriting":
                        visibility = User.IsAlowedWriting;
                        break;
                    case "IsAlowedViewing":
                        visibility = User.IsAlowedViewing;
                        break;
                }
            }
            catch { }
            if (visibility)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }
    }
}
