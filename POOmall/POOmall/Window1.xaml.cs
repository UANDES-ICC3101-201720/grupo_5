using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POOmall
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void BtnListo_OnClick(object sender, RoutedEventArgs e)
        {
            Parche.dineroInicial = Convert.ToInt16(dinero.Text);
            Parche.precioArriendo = Convert.ToInt16(preciom2.Text);
            this.Close();
        }
    }
}
