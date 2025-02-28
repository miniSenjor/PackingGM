using PackingGM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PackingGM.View
{
    /// <summary>
    /// Логика взаимодействия для ManageRoleView.xaml
    /// </summary>
    public partial class ManageRoleView : Page
    {
        public ManageRoleView()
        {
            InitializeComponent();
            DataContext = new ManageRoleViewModel();
        }
    }
}
