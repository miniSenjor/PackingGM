using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;

namespace PackingGM.ViewModel
{
    /// <summary>
    /// Класс для навигации в приложении между страницами
    /// </summary>
    public class Navigation
    {
        private static Frame _mainFrame;
        private static readonly Dictionary<PageType, Page> _pages = new Dictionary<PageType, Page>();

        /// <summary>
        /// Иницилизировали методом страницу
        /// </summary>
        /// <param name="mainFrame"></param>
        public static void Initialize(Frame mainFrame)
        {
            _mainFrame = mainFrame;
        }

        /// <summary>
        /// Переход на новую страницу
        /// </summary>
        /// <param name="page">Новая страница</param>
        public static void Navigate(PageType pageType)
        {
            if (_mainFrame == null)
                throw new InvalidOperationException("Frame не инициализирован");
            //_mainFrame.Content = null;
            _mainFrame.Navigate(_pages[pageType]);
            //_mainFrame.Content = page;
            //Debug.Print(_mainFrame.CanGoBack.ToString());
            //_mainFrame.NavigationService.RemoveBackEntry();
            //Debug.Print(_mainFrame.CanGoBack.ToString());
        }

        public static void RegisterPage(PageType pageType, Page page)
        {
            _pages[pageType] = page;
        }
    }
    public enum PageType
    {
        LoginView,
        MainView,
        ManageGraphView,
        ManageRoleView,
        ManageUserView,
        TestView
    }
}