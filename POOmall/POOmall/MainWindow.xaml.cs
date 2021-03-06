﻿using System;
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
        public int precioArriendo = 3;
        public int dineroInicial = 3000;
        public int tiempo = 0;

        static MainWindow()
        {
            Instance = new MainWindow();
        }
        private MainWindow()
        {
            InitializeComponent();

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
                pisos.Add(Parche.Pisos[i]);
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
            if(tiempo<10)
            {
                tiempo++;
                if (tiempo >= 1)
                {
                    rep1.IsEnabled = true;
                }

                if (tiempo >= 2)
                {
                    rep2.IsEnabled = true;
                }

                if (tiempo >= 3)
                {
                    rep3.IsEnabled = true;
                }

                if (tiempo >= 4)
                {
                    rep4.IsEnabled = true;
                }

                if (tiempo >= 5)
                {
                    rep5.IsEnabled = true;
                }

                if (tiempo >= 6)
                {
                    rep6.IsEnabled = true;
                }

                if (tiempo >= 7)
                {
                    rep7.IsEnabled = true;
                }

                if (tiempo >= 8)
                {
                    rep8.IsEnabled = true;
                }

                if (tiempo >= 9)
                {
                    rep9.IsEnabled = true;
                }

                if (tiempo >= 10)
                {
                    rep10.IsEnabled = true;
                    Avanzar.IsEnabled = false;
                }
                Random rn = new Random();
                int contG = 0;
                int contC = 0;
                Stopwatch sw = new Stopwatch();
                sw.Start();
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
                string s0 = "Dia: " + tiempo;
                string s1 = "Cantidad de clientes recepcionados: " + c;
                string s2 = "Cantidad de clientes promedio por dia: " + contC / tiempo;
                string s3 = "Ganacia total: " + g;
                string s4 = "Ganacia promedio por dia: " + contG / tiempo;
                string s5 = "Local con mayor ganancia: " + max.Nombre + " Ganancia: " + maxG;
                string s6 = "Local con menor ganancia: " + min.Nombre + " Ganancia: " + minG;
                string s7 = "Local con mayor clientes: " + max2.Nombre + " Atendidos: " + maxC;
                string s8 = "Local con menor clientes: " + min2.Nombre + " Atendidos: " + minC;
                string[] repFin = new string[] { s0, s1, s2, s3, s4, s5, s6, s7, s8 };
                SaveReport(tiempo, repFin);
                Time.Text = "" + tiempo;
            }
            else
            {

            }
        }

        public static void SaveReport(int day, string[] repFin)
        {
            File.WriteAllLines("reporte" + day + ".txt", repFin);
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
            string nombre = "Tienda" + num;
            Local loc = new Local(nombre, rn.Next(1, 15), rn.Next(10, 150), rn.Next(0, 50), rn.Next(50, 100), cats[rn.Next(0, 9)], p);

            return loc;
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

        private void BtnCorrer_OnClick(object sender, RoutedEventArgs e)
        {
            General.IsEnabled = true;
            Reporte.IsEnabled = true;
            BtnCorrer.IsEnabled = false;
            Panel.Visibility = Visibility.Hidden;
            General.IsSelected = true;
            GetConstruccion();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Load.IsEnabled = true;
            BtnCorrer.IsEnabled = true;
            PisosView.IsEnabled = false;
        }

        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            Load.IsEnabled = false;
            BtnCorrer.IsEnabled = false;
            PisosView.IsEnabled = true;
        }

        private void Avanzar_Click(object sender, RoutedEventArgs e)
        {
            runFinal(pisos, precioArriendo);
        }
    }
}
