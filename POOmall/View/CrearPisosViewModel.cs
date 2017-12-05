using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using POOmall.Model;

namespace POOmall.View
{
    public class CrearPisosViewModel : INotifyPropertyChanged
    {
        public int CuentaPisos { get; set; }



        public CrearPisosViewModel()
        {
            AddTiendaCommand = new RelayCommand(OnAddTienda, CanAddTienda);
            AddPisoCommand = new RelayCommand(OnAddPiso, CanAddPiso);
            AreaLastPiso = 1500;
            CuentaPisos = 1;
            SliderAreaPisoValue = 1500;
            NombreTienda = "";
            CantidadEmpleados = 1;
            AreaTienda = 1;
            PrecioMin = 1;
            PrecioMax = 2;
        }


        public enum Categorias
        {
            Ropa,
            Hogar,
            Alimentos,
            Ferreteria,
            Tecnologia,
            ComidaRapida,
            Restaurant,
            Cine,
            Juegos,
            Bowling
        }
        // Para poblar ComboBox desde enum, sacado de:
        // ridgesolutions.ie/index.php/2014/11/05/wpf-xaml-binding-combobox-directly-to-enum-values
        public IList<Categorias> CategoriasEnum => Enum.GetValues(typeof(Categorias)).Cast<Categorias>().ToList<Categorias>();


        public ObservableCollection<Piso> Pisos
        {
            get => Settings.Pisos;
            set => Settings.Pisos = value;
        }

        public ObservableCollection<Tienda> Tiendas
        {
            get => Settings.Tiendas;
            set
            {
                Settings.Tiendas = value;
                NotifyPropertyChanged("Settings.Tiendas");
            }
        }

        private Piso _selectedPiso;
        public Piso SelectedPiso
        {
            get => _selectedPiso;
            set
            {
                _selectedPiso = value;
                AddTiendaCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged("SelectedPiso");
            }
        }

        private int _areaLastPiso;
        private int _sliderAreaPisoValue;
        private int _sliderAreaTiendaValue;

        public int SliderAreaPisoValue
        {
            get => _sliderAreaPisoValue;
            set
            {
                _sliderAreaPisoValue = value;
                NotifyPropertyChanged("SliderAreaPisoValue");
            }
        }
        public int AreaLastPiso
        {
            get => _areaLastPiso;
            set
            {
                _areaLastPiso = value;
                NotifyPropertyChanged("AreaLastPiso");
            }
        }
        public int SliderAreaTiendaValue
        {
            get => _sliderAreaTiendaValue;
            set
            {
                _sliderAreaTiendaValue = value;
                NotifyPropertyChanged("SliderAreaTiendaValue");
            }
        }


        public int SliderAreaTiendaMax
        {
            get { return _sliderAreaTiendaMax; }
            set { _sliderAreaTiendaMax = SelectedPiso?.Area ?? 0 - (Tiendas.Count > 0 ? Tiendas.Where(t => t.Piso == SelectedPiso.Numero).Sum(t => t.Area) : 0);  }
        }



        #region AddPiso
        public RelayCommand AddPisoCommand { get; }

        private bool CanAddPiso()
        {
            return true;
        }

        private void OnAddPiso()
        {
            Pisos.Add(new Piso(CuentaPisos++, SliderAreaPisoValue));
            AreaLastPiso = Pisos.Last().Area;

        }

        #endregion
        
        #region AddTienda

        private string _nombreTienda;
        public string NombreTienda
        {
            get => _nombreTienda;
            set
            {
                _nombreTienda = value;
                NotifyPropertyChanged("NombreTienda");
            }
        }
        private int _cantidadEmpleados;
        public int CantidadEmpleados
        {
            get => _cantidadEmpleados;
            set
            {
                _cantidadEmpleados = value;
                NotifyPropertyChanged("CantidadEmpleados");
            }
        }
        private int _areaTienda;
        public int AreaTienda
        {
            get => SliderAreaTiendaValue;
            set
            {
                _areaTienda = value;
                NotifyPropertyChanged("AreaTienda");
            }
        }
        private int _precioMin;
        public int PrecioMin
        {
            get => _precioMin;
            set
            {
                _precioMin = value;
                NotifyPropertyChanged("PrecioMin");
            }
        }
        private int _precioMax;
        private int _sliderAreaTiendaMax;

        public int PrecioMax
        {
            get => _precioMax;
            set
            {
                _precioMax = value;
                NotifyPropertyChanged("PrecioMax");
            }
        }
        public RelayCommand AddTiendaCommand { get; private set; }

        private bool CanAddTienda()
        {
            return SelectedPiso != null;
        }

        private void OnAddTienda()
        {
            if (PrecioMin>=PrecioMax)
            {
                // Estos messagebox rompen el patrón MVVM, pero no encontramos otra forma de hacerlo
                MessageBox.Show("Precio minimo no puede ser mayor a precio maximo");
                return;
            }
            if (Tiendas.Where(t => t.Piso == SelectedPiso.Numero).Sum(t => t.Area) + AreaTienda > SelectedPiso.Area)
            {
                // Estos messagebox rompen el patrón MVVM, pero no encontramos otra forma de hacerlo
                MessageBox.Show("El área sumada de las tiendas no puede ser superior a la del piso que las contiene\n" + 
                    $"Espacio restante: {SelectedPiso.Area - Tiendas.Where(t => t.Piso == SelectedPiso.Numero).Sum(t => t.Area)}");
                return;
            }
            Tiendas.Add(new Tienda(NombreTienda, SelectedPiso.Numero, CantidadEmpleados, AreaTienda, PrecioMin, PrecioMax, Settings.Categoria.Ferreteria));
            NombreTienda = "";
            CantidadEmpleados = 1;
            AreaTienda = 1;
            PrecioMin = 1;
            PrecioMax = 2;
        }
        #endregion

        #region Implementación INotify...
        // https://stackoverflow.com/a/8316100
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
