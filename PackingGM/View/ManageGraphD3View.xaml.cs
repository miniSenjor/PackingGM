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
    /// Логика взаимодействия для ManageGraphD3View.xaml
    /// </summary>
    public partial class ManageGraphD3View : Page
    {
        public ManageGraphD3View()
        {
            InitializeComponent();
            DataContext = new ManageGraphD3ViewModel();
        }
    }
}
