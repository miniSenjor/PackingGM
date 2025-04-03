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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.RegisterPage(PageType.LoginView, new LoginView());
            Navigation.RegisterPage(PageType.MainView, new MainView());
            Navigation.RegisterPage(PageType.ManageRoleView, new ManageRoleView());
            Navigation.RegisterPage(PageType.ManageUserView, new ManageUserView());
            Navigation.RegisterPage(PageType.ManageGraphView, new ManageGraphView());
            Navigation.RegisterPage(PageType.TestView, new TestView());
            Navigation.RegisterPage(PageType.ManageGraphD3View, new ManageGraphD3View());
            Navigation.Initialize(MainFrame);
            //Открытие страницы с авторизацией
            Navigation.Navigate(PageType.LoginView);
            //Navigation.Navigate(PageType.ManageGraphD3View);
        }
    }
}
