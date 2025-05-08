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

            if (!_pages.ContainsKey(pageType))
            {
                string typeName = pageType.ToString();
                var type = Type.GetType($"PackingGM.View.{typeName}");
                if (type == null)
                    throw new InvalidOperationException($"Класс {typeName} не найден");
                var instanse = Activator.CreateInstance(type) as Page;
                RegisterPage(pageType, instanse);
            }
            _mainFrame.Navigate(_pages[pageType]);
            //if(thisPage!=null && Enum.TryParse<PageType>(thisPage, out var page))
            //{
            //    _pages.Remove(page);
            //}
            //else
            //{
            //    //throw new InvalidOperationException($"Страница {thisPage} не найденa");
            //}

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
        ManageUserView,
        TestView,
        ManageGraphD3View
    }
}