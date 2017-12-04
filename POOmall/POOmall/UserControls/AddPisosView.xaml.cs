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
        public ObservableCollection<Piso> Pisos;
        private int _cuenta;

        public AddPisosView()
        {
            TiendasCollection = new ObservableCollection<string>();
            Pisos = new ObservableCollection<Piso>();
            _cuenta = 0;

            InitializeComponent();
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
            int areaPiso = 0;
            try
            {
                areaPiso = Convert.ToUInt16(TextBoxAreaPiso.Text);
                Piso piso = new Piso(Pisos.Count() + 1, areaPiso, Acc.IsChecked, Est.IsChecked, Sub.IsChecked);
                if (areaPiso < 500 || areaPiso > 1500)
                {
                    MessageBox.Show("El area debe ser entre 500 y 1500", "Error");
                }
                else if (Pisos.Count() > 0 && areaPiso > Pisos.Last().Espacio)
                {
                    MessageBox.Show("El area debe ser menor o igual al piso anterior", "Error");
                }
                else
                {
                    TiendasCollection.Add("1");
                    TextBoxAreaPiso.IsEnabled = false;
                    Pisos.Add(piso);
                    TextBoxAreaPiso.Text = "";
                    TextBoxAreaPiso.IsEnabled = true;
                    TiendasCollection = new ObservableCollection<string>();
                    _cuenta += 1;
                    if(Sub.IsChecked == false)
                    {
                        Sub.IsEnabled = false;
                    }
                }
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
            
            if (Pisos.Count > 0)
            {
                BtnSiguiente.IsEnabled = true;
            }
        }

        private void BtnSiguiente_OnClick(object sender, RoutedEventArgs e)
        {
            Parche.dineroInicial = Convert.ToInt32(TextBoxDineroInicial.Text);
            Parche.precioArriendo = Convert.ToInt32(TextBoxArriendo.Text);
            Parche.Pisos = Pisos;
            POOmall.MainWindow.Instance.Load.IsEnabled = false;
            POOmall.MainWindow.Instance.BtnCorrer.IsEnabled = true;
        }
    }
}
