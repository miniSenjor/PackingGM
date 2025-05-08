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
            //List<string> l = new List<string>();
            //for (int i = 0; i < 100; i++)
            //    l.Add(i.ToString());
            //ItemsSource = l;
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable<object>),
                typeof(ComboTextBox),
                new PropertyMetadata(null, OnItemsSourceChanged));
        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                UpdateListBox();
            }
        }
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComboTextBox)d;
            control.UpdateListBox();
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ComboTextBox),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedItemChanged));
        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComboTextBox)d;
            control.UpdateSelectedItem(e.NewValue);
        }

        private void UpdateSelectedItem(object newValue)
        {
            // Обновляем TextBox только если значение действительно изменилось
            if (!Equals(lst.SelectedItem, newValue))
            {
                SelectedItem = newValue;

                if (newValue != null && !string.IsNullOrEmpty(DisplayMemberPath))
                {
                    var prop = newValue.GetType().GetProperty(DisplayMemberPath);
                    txt.Text = prop?.GetValue(newValue, null)?.ToString() ?? "";
                }
            }
        }
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
                //var prop = item.GetType().GetProperty(DisplayMemberPath);
                //var value = prop.GetValue(item, null)?.ToString() ?? "";
                //Debug.Print(SelectedItemProperty.ToString());
                //написать выбор поля по displayMemberPath
                if (!string.IsNullOrEmpty(DisplayMemberPath))
                {
                    var property = SelectedItem.GetType().GetProperty(DisplayMemberPath);
                    txt.Text = property?.GetValue(SelectedItem, null)?.ToString() ?? "";
                }
                ItemsPopup.IsOpen = false;
            }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(ComboTextBox),
                new PropertyMetadata(null));

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
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
            if (lst.SelectedItem != null)
            {
                SelectedItem = lst.SelectedItem;
                //if (!string.IsNullOrEmpty(DisplayMemberPath))
                //{
                //    var property = SelectedItem.GetType().GetProperty(DisplayMemberPath);
                //    txt.Text = property?.GetValue(SelectedItem, null)?.ToString() ?? "";
                //}//перенес в изменение выбранного элемента
                ItemsPopup.IsOpen = false;
            }
        }

        private void Txt_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txt.Text != "" && !lst.Items.Contains(txt.Text))
            {
                Debug.Print(txt.Text);
                foreach(var item in lst.Items)
                {
                    Debug.Print(item.ToString());
                }
                //Debug.Print("Фокус не потерян");
                //e.Handled = true;
            }
            else
            {
                    return;
                //доделать что бы при вводе и снятия фокуса выбирался элемент из списка
                foreach(var item in lst.ItemsSource)
                {
                    //SelectedItem = item;
                }
            }
        }

        private List<object> SearchInList(string searchText)
        {
            if (searchText == "")
                if (ItemsSource == null || string.IsNullOrEmpty(DisplayMemberPath))
                    return new List<object>();
                else
                    return ItemsSource.ToList();

            if (ItemsSource == null || string.IsNullOrEmpty(DisplayMemberPath))
                return new List<object>();

            //var propertyInfo = typeof(object).GetProperty(DisplayMemberPath);
            //if (propertyInfo == null)
            //    return new List<object>();

            return ItemsSource
                .Where(item =>
                {
                    var prop = item.GetType().GetProperty(DisplayMemberPath);
                    var value = prop.GetValue(item, null)?.ToString() ?? "";
                    return NormalizeText(value.ToLower()).Contains(NormalizeText(searchText));
                })
                .ToList();

            //return ItemsSource
            //    .Where(item => NormalizeText(item.ToLower()).Contains(NormalizeText(searchText)))
            //    .ToList();
        }

        private string NormalizeText(string text)
        {
            var replacementMap = new Dictionary<char, char>
            {
                { 'а', 'a' },  // Русская 'а' -> английская 'a'
                { 'е', 'e' },  // Русская 'е' -> английская 'e'
                { 'о', '0' },  // Русская 'о' -> Цифра '0'
                { 'с', 'c' },  // Русская 'с' -> английская 'c'
                { 'o', '0' },  // английская 'o' -> Цифра '0'
                { 'н', 'h' },  // Русская 'н' -> английская 'h'
                { 'т', 't' },  // Русская 'т' -> английская 't'
                { 'у', 'y' },   // Русская 'у' -> английская 'y'
                { 'р', 'p' },   // Русская 'р' -> английская 'p'
                { 'х', 'x' },   // Русская 'х' -> английская 'x'
                { 'в', 'b' },   // Русская 'в' -> английская 'b'
                { 'м', 'm' },   // Русская 'м' -> английская 'm'
                { 'к', 'k' },   // Русская 'к' -> английская 'k'
                { 'з', '3' }   // Русская 'з' -> цифра '3'
            };
            return new string(text.Trim(' ').Select(c => replacementMap.ContainsKey(c) ? replacementMap[c] : c).ToArray());
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectedItem(SelectedItem);
            ItemsPopup.IsOpen = false;
        }
    }
}
