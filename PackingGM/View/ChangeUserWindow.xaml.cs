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
using System.Windows.Shapes;

namespace PackingGM.View
{
    /// <summary>
    /// Логика взаимодействия для CreateUserWindow.xaml
    /// </summary>
    public partial class ChangeUserWindow : Window
    {
        public ChangeUserWindow()
        {
            InitializeComponent();
            DataContext = new ChangeUserWindowModel();
        }
    }
}
