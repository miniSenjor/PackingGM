using PackingGM.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace PackingGM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static AppDb _context;

        public static AppDb GetContext()
        {
            if (_context == null)
                _context = new AppDb();
            return _context;
        }
    }
}
