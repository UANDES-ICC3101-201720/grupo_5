using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using POOmall.UserControls;

namespace POOmall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public int pisnum = new int();
        public List<Piso> pisos = new List<Piso>();
        public int precioArriendo = 3000;
        public int dineroInicial = 3000;

        static MainWindow()
        {
            Instance = new MainWindow();
        }
        private MainWindow()
        {
            Random rn = new Random();
            Categoria ropa = new Categoria("Ropa");
            Categoria hogar = new Categoria("Hogar");
            Categoria alimento = new Categoria("Alimento");
            Categoria ferre = new Categoria("Ferreteria");
            Categoria tec = new Categoria("Tecnologia");
            Categoria rapida = new Categoria("Comida Rapida");
            Categoria rest = new Categoria("Restaurant");
            Categoria cine = new Categoria("Cine");
            Categoria jue = new Categoria("Juegos");
            Categoria bow = new Categoria("Bowling");
            Categoria[] cats = new Categoria[] { ropa, hogar, alimento, ferre, tec, rapida, rest, cine, jue, bow };
            //int precioArriendo = 3;
            //int dineroInicial = 3000;

            pisnum = 1; // aqui debe ingresarlo el usuario

            // Comentado porque se genera en GetConstruccion
            ////pisnum = ContarPiso(); esto era antes, falla por pedir consola

            //Piso pis = new Piso(1, 1000, true, true, false); //esto era solo para probar
            //pisos.Add(pis); //esto tmbn

            //// Piso pis = CrearPisos(pisnum); esto era antes, falla por pedir consola


            //foreach (Piso p in pisos)
            //{
            //    if (p.Subterraneo == false)
            //    {
            //        CrearLocales(p, cats);
            //    }
            //}

            InitializeComponent();

            //runFinal(pisos, pisnum);

        }

        public void GetConstruccion()
        {
            precioArriendo = Parche.precioArriendo;
            dineroInicial = Parche.dineroInicial;

            Categoria ropa = new Categoria("Ropa");
            Categoria hogar = new Categoria("Hogar");
            Categoria alimento = new Categoria("Alimento");
            Categoria ferre = new Categoria("Ferreteria");
            Categoria tec = new Categoria("Tecnologia");
            Categoria rapida = new Categoria("Comida Rapida");
            Categoria rest = new Categoria("Restaurant");
            Categoria cine = new Categoria("Cine");
            Categoria jue = new Categoria("Juegos");
            Categoria bow = new Categoria("Bowling");
            Categoria[] cats = new Categoria[] { ropa, hogar, alimento, ferre, tec, rapida, rest, cine, jue, bow };

            for (var i = 0; i < Parche.Pisos.Count; i++)
            {
                var piso = Parche.Pisos[i];
                pisos.Add(new Piso(i+1, Convert.ToInt16(piso[1]), true, true, false));
            }
            foreach (Piso p in pisos)
            {
                if (p.Subterraneo == false)
                {
                    CrearLocales(p, cats);
                }
            }
        }
        public void runFinal(List<Piso> pisos, int precioArriendo)
        {
            for (int day = 0; day < 10; day++)
            {
                Random rn = new Random();
                int contG = 0;
                int contC = 0;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Console.WriteLine();
                Console.WriteLine("Dia " + day);
                int c = 0;
                int maxC = 0;
                int minC = 1000000000;
                int g = 0;
                int maxG = 0;
                int minG = 1000000000;
                Local max2 = new Local(null, 0, 0, 0, 0, null, null);
                Local min2 = new Local(null, 0, 0, 0, 0, null, null);
                foreach (Piso p in pisos)
                {
                    if (p.Subterraneo == false)
                    {
                        foreach (Local l in p.Tiendas)
                        {
                            int a = Math.Max((100 - (l.PrecioMax + l.PrecioMin) / 2) / 2, 0);
                            int cmax = l.CantClientes + (l.Area / 10) * a / 100 * l.CantEmpleados;
                            l.CantClientes = rn.Next(0, cmax);
                            c += l.CantClientes;
                            int ph = maxC;
                            maxC = Math.Max(maxC, l.CantClientes);
                            if (ph != maxC)
                            {
                                max2 = l;
                            }
                            int ph2 = minC;
                            minC = Math.Min(minC, l.CantClientes);
                            if (ph2 != minC)
                            {
                                min2 = l;
                            }
                        }
                    }
                }
                Local max = new Local(null, 0, 0, 0, 0, null, null);
                Local min = new Local(null, 0, 0, 0, 0, null, null);
                foreach (Piso p in pisos)
                {
                    if (p.Subterraneo == false)
                    {
                        foreach (Local l in p.Tiendas)
                        {
                            l.Ganancia = 0;
                            int v = rn.Next(l.PrecioMin, l.PrecioMax);
                            int costoArriendo = l.Area * precioArriendo;
                            l.Ganancia = v * l.CantClientes - (l.CantEmpleados + costoArriendo);
                            l.GananciaTotal += l.Ganancia;
                            g += l.Ganancia;
                            int ph = maxG;
                            maxG = Math.Max(maxG, l.Ganancia);
                            if (ph != maxG)
                            {
                                max = l;
                            }
                            int ph2 = minG;
                            minG = Math.Min(minG, l.Ganancia);
                            if (ph2 != minG)
                            {
                                min = l;
                            }
                        }

                    }
                }


                contC += c;
                contG += g;
                string s0 = "Dia: " + (day+1);
                string s1 = "Cantidad de clientes recepcionados: " + c;
                string s2 = "Cantidad de clientes promedio por dia: " + contC / (day + 1);
                string s3 = "Ganacia total: " + g;
                string s4 = "Ganacia promedio por dia: " + contG / (day + 1);
                string s5 = "Local con mayor ganancia: " + max.Nombre + " Ganancia: " + maxG;
                string s6 = "Local con menor ganancia: " + min.Nombre + " Ganancia: " + minG;
                string s7 = "Local con mayor clientes: " + max2.Nombre + " Atendidos: " + maxC;
                string s8 = "Local con menor clientes: " + min2.Nombre + " Atendidos: " + minC;
                string[] repFin = new string[] { s0, s1, s2, s3, s4, s5, s6, s7, s8 };
                SaveReport(day, repFin);
                while (sw.ElapsedMilliseconds < 100)
                {
                    //if (PauseCurrentOperation())
                    //{
                    //    break;
                    //}
                }
            }
        }

        public static void SaveReport(int day, string[] repFin)
        {
            File.WriteAllLines("reporte" + (day+1) + ".txt", repFin);
        }

        public static void RunSim(List<Piso> pisos, int precioArriendo, int contC, int contG)
        {

        }

        public static void CrearLocales(Piso p, Categoria[] cats)
        {
            Random rn = new Random();
            int numTien = rn.Next(5, 50);
            Local[] ph = new Local[numTien];
            int areaTot = 0;
            for (int nt = 0; nt < numTien; nt++)
            {
                ph[nt] = CrearRngLocal(cats, p, nt);
                areaTot += ph[nt].Area;
            }
            if (areaTot > p.Espacio)
            {
                CrearLocales(p, cats);
            }
            p.Tiendas = ph;
        }

        public static Local CrearRngLocal(Categoria[] cats, Piso p, int num)
        {
            Random rn = new Random();
            string nombre = "tienda" + num;
            Local loc = new Local(nombre, rn.Next(1, 15), rn.Next(10, 150), rn.Next(0, 50), rn.Next(50, 100), cats[rn.Next(0, 9)], p);

            return loc;
        }

        public static int ContarPiso()
        {
            Console.WriteLine("Numero de Pisos: (entre 1 y 10)");
            string p = Console.ReadLine();
            int pisnum = 0;
            try
            {
                pisnum = Int32.Parse(p);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error de Ingreso.");
                pisnum = ContarPiso();
            }
            if (pisnum < 1 || pisnum > 10)
            {
                Console.WriteLine("Error de Ingreso.");
                pisnum = ContarPiso();
            }
            return pisnum;
        }

        public static List<Piso> CrearPisos(int num)
        {
            int err = 0;
            List<Piso> pisos = new List<Piso>();
            for (int i = 0; i < num; i++)
            {
                pisos.Add(CrearPiso(i));
            }

            for (int i = 1; i < num; i++)
            {
                if (pisos.ElementAtOrDefault(i).Espacio > pisos.ElementAtOrDefault(i - 1).Espacio)
                {
                    err++;
                }
            }

            if (err != 0)
            {
                Console.WriteLine("El espacio de un piso no puede ser mayor que el piso debajo.");
                for (int i = 0; i < num; i++)
                {
                    pisos.Add(CrearPiso(i));
                }
            }

            err = 0;

            for (int i = 1; i < num; i++)
            {
                if (pisos.ElementAtOrDefault(i).Subterraneo == true && pisos.ElementAtOrDefault(i - 1).Subterraneo == false)
                {
                    err++;
                }
            }

            if (err != 0)
            {
                Console.WriteLine("No puede haber subterraneo sobre un no subterraneo");
                for (int i = 0; i < num; i++)
                {
                    pisos.Add(CrearPiso(i));
                }
            }

            return pisos;
        }

        public static Piso CrearPiso(int num)
        {

            Console.WriteLine();
            Console.WriteLine("Piso numero: " + (num + 1));
            int err = 0;
            Console.WriteLine("Espacio del Piso: (entre 500 y 1500)");
            string e = Console.ReadLine();
            int espacio = 0;
            try
            {
                espacio = Int32.Parse(e);
            }
            catch (Exception exception)
            {
                err++;
            }
            if (espacio < 500 || espacio > 1500)
            {
                err++;
            }

            Boolean acc = true;
            Console.WriteLine("Es Acceso? (si o no)");
            string y = Console.ReadLine();
            if (y.Equals("no"))
            {
                acc = false;
            }
            else if (y.Equals("si")) { }
            else
            {
                err++;
            }

            Boolean est = true;
            Console.WriteLine("Tiene Estacionamiento? (si o no)");
            string t = Console.ReadLine();
            if (t.Equals("no"))
            {
                est = false;
            }
            else if (t.Equals("si")) { }
            else
            {
                err++;
            }

            Boolean sub = true;
            Console.WriteLine("Es Subterraneo? (si o no)");
            string s = Console.ReadLine();
            if (s.Equals("no"))
            {
                sub = false;
            }
            else if (s.Equals("si")) { }
            else
            {
                err++;
            }

            Piso pis = new Piso(num, espacio, acc, est, sub);

            if (err != 0)
            {
                Console.WriteLine("Error de Ingreso.");
                pis = CrearPiso(num);
            }

            return pis;
        }

        public static bool PauseCurrentOperation()
        {
            if (Console.KeyAvailable)
            {
                var consoleKey = Console.ReadKey(true); // No mostrar tecla presionada en consola
                if (consoleKey.Key == ConsoleKey.P) // Si la tecla presionada es P...
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    string input = Console.ReadLine();
                    if (input == "c" || input == "C")
                    {
                        return false; // Continue 
                    }
                    else
                    {
                        // Despues de presionar Enter, se elimina el texto "Pesione Enter..." de la consola
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                    }
                }
            }
            return false;
        }

        // log.txt  http://www.mcrook.com/2014/11/quick-and-easy-console-logging-trace.html
        private static void InitiateTracer()
        {
            Trace.Listeners.Clear();
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var twtl = new TextWriterTraceListener("log.txt")
            {
                Name = "TextLogger",
                TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime
            };
            var ctl = new ConsoleTraceListener(false) { TraceOutputOptions = TraceOptions.DateTime };
            Trace.Listeners.Add(twtl);
            Trace.Listeners.Add(ctl);
            Trace.AutoFlush = true;
        }

        private void rep1_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte1.txt"));
        }

        private void rep2_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte2.txt"));
        }

        private void rep3_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte3.txt"));
        }

        private void rep4_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte4.txt"));
        }

        private void rep5_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte5.txt"));
        }

        private void rep6_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte6.txt"));
        }

        private void rep7_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte7.txt"));
        }

        private void rep8_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte8.txt"));
        }

        private void rep9_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte9.txt"));
        }

        private void rep10_Click(object sender, RoutedEventArgs e)
        {
            repoCons.Text = string.Join("\r\n", File.ReadAllLines("reporte10.txt"));
        }

        private void BtnEmpezar_OnClick(object sender, RoutedEventArgs e)
        {
            this.PisosView.Visibility = Visibility.Visible;
            this.BtnEmpezar.IsEnabled = false;
        }

        private void BtnCorrer_OnClick(object sender, RoutedEventArgs e)
        {
            BtnCorrer.IsEnabled = false;
            GetConstruccion();
            runFinal(pisos, precioArriendo);

        }
    }
}
