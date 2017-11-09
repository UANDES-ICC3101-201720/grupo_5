using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POOmall.UserControls
{
    /// <summary>
    /// Interaction logic for AddPisosView.xaml
    /// </summary>
    public partial class AddPisosView : UserControl
    {
        public int[] TiendasArray;
        public ObservableCollection<int> TiendasCollection;

        public AddPisosView()
        {
            TiendasArray = new int[1];

            InitializeComponent();
            ListBoxAreas.ItemsSource = TiendasCollection;
            
        }

        //https://stackoverflow.com/a/1268648
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnAddTiendas_OnClick(object sender, RoutedEventArgs e)
        {
            TiendasCollection.Add(new int());
        }
    }
}
