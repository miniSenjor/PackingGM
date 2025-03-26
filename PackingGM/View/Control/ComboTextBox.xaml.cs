using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PackingGM.View.Control
{
    /// <summary>
    /// Логика взаимодействия для ComboTextBox.xaml
    /// </summary>
    public partial class ComboTextBox : UserControl
    {
        public ComboTextBox()
        {
            InitializeComponent();
            List<string> l = new List<string>();
            for (int i = 0; i < 100; i++)
                l.Add(i.ToString());
            ItemsSource = l;
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable<string>),
                typeof(ComboTextBox),
                new PropertyMetadata(null, OnItemsSourceChanged));
        public IEnumerable<string> ItemsSource
        {
            get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(string),
                typeof(ComboTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private void UpdateListBox()
        {
            lst.ItemsSource = ItemsSource;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txt.Text.ToLower();
            lst.ItemsSource = SearchInList(searchText);
            ItemsPopup.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ItemsPopup.IsOpen = !ItemsPopup.IsOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lst.SelectedItem != null)
            {
                SelectedItem = lst.SelectedItem.ToString();
                txt.Text = SelectedItem;
                ItemsPopup.IsOpen = false;
            }
        }
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComboTextBox)d;
            control.UpdateListBox();
        }

        private void Txt_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txt.Text != "" && !lst.Items.Contains(txt.Text))
            {
                Debug.Print("Фокус не потерян");
                e.Handled = true;
            }

        }

        private List<string> SearchInList(string searchText)
        {
            return ItemsSource
                .Where(item => NormalizeText(item.ToLower()).Contains(NormalizeText(searchText)))
                .ToList();
        }

        private string NormalizeText(string text)
        {
            var replacementMap = new Dictionary<char, char>
            {
                { 'а', 'a' },  // Русская 'а' -> английская 'a'
                { 'е', 'e' },  // Русская 'е' -> английская 'e'
                { 'о', 'o' },  // Русская 'о' -> английская 'o'
                { 'с', 'c' },  // Русская 'с' -> английская 'c'
                { '0', 'o' },  // Цифра '0' -> английская 'o'
                { 'н', 'h' },  // Русская 'н' -> английская 'h'
                { 'т', 't' },  // Русская 'т' -> английская 't'
                { 'у', 'y' },   // Русская 'у' -> английская 'y'
                { 'р', 'p' },   // Русская 'р' -> английская 'p'
                { 'х', 'x' },   // Русская 'х' -> английская 'x'
                { 'в', 'b' },   // Русская 'в' -> английская 'b'
                { 'м', 'm' },   // Русская 'м' -> английская 'm'
            };
            return new string(text.Select(c => replacementMap.ContainsKey(c) ? replacementMap[c] : c).ToArray());
        }

    }
}
