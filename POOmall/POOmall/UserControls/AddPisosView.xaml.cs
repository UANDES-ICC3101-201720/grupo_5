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
        public ObservableCollection<string> TiendasCollection;
        public ObservableCollection<object[]> Pisos;
        private int _cuenta;

        public AddPisosView()
        {
            TiendasCollection = new ObservableCollection<string>();
            Pisos = new ObservableCollection<object[]>();
            _cuenta = 0;

            InitializeComponent();
            ListBoxAreas.ItemsSource = TiendasCollection;
            ListBox.ItemsSource = Pisos;

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
            //foreach (var item in TiendasCollection)
            //{
            //    MessageBox.Show($"{item}");
            //}
            Pisos.Add(new object[] {$"Tienda {_cuenta}", TextBoxAreaPiso.Text.ToString(), TiendasCollection});
            TextBoxAreaPiso.Text = "";
            TextBoxAreaPiso.IsEnabled = true;
            TiendasCollection = new ObservableCollection<string>();
            ListBoxAreas.ItemsSource = TiendasCollection;
            _cuenta += 1;
            if (Pisos.Count > 0)
            {
                BtnSiguiente.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddTiendas_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var areaPiso = Convert.ToUInt16(TextBoxAreaPiso.Text);
                TiendasCollection.Add("1");
                TextBoxAreaPiso.IsEnabled = false;

            }
            catch (FormatException)
            {
                MessageBox.Show("Primero debe especificar un area válida para el piso", "Error");
                TextBoxAreaPiso.Text = "";
            }
            catch (OverflowException)
            {
                MessageBox.Show("El área no puede ser negativa", "Error");
                TextBoxAreaPiso.Text = "";
            }
            
        }

        private void BtnSiguiente_OnClick(object sender, RoutedEventArgs e)
        {
            Parche.Pisos = Pisos;
        }
    }
}
