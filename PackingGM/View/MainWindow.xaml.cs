using PackingGM.ViewModel;
using System.Diagnostics;
using System.Windows;

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
            //Navigation.RegisterPage(PageType.LoginView, new LoginView());
            //Navigation.RegisterPage(PageType.MainView, new MainView());
            //Navigation.RegisterPage(PageType.ManageRoleView, new ManageRoleView());
            //Navigation.RegisterPage(PageType.ManageUserView, new ManageUserView());
            //Navigation.RegisterPage(PageType.ManageGraphView, new ManageGraphView());
            //Navigation.RegisterPage(PageType.TestView, new TestView());
            Navigation.RegisterPage(PageType.ManageGraphD3View, new ManageGraphD3View());
            Navigation.Initialize(MainFrame);
            //Открытие страницы с авторизацией
            //Navigation.Navigate(PageType.LoginView);
            Navigation.Navigate(PageType.ManageGraphD3View);
            //Navigation.Navigate(PageType.TestView);
            //Testing t = new Testing();
        }
    }
}
