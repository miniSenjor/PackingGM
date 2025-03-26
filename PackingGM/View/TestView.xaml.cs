using PackingGM.Data;
using PackingGM.Model;
using PackingGM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestView : Page
    {
        public TestView()
        {
            InitializeComponent();
            DataContext = new TestViewModel();
            //AddInDb("select nmk_note, nmk_name from nmk where nmk_classif_id=2722 and nmk_classif_type_id=19", 2);
        }
        
    }
}
