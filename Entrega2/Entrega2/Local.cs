using System.Collections.Generic;

namespace POOmall
{
    class Local
    {
        public string Nombre;
        public int CantEmpleados;
        public int Area;
        public int PrecioMin;
        public int PrecioMax;
        public Categoria Cat;
        public Piso Piso;
        public List<Producto> OfertaProd;
        public List<Servicio> OfertaServ;
        public List<Trabajador> Empleados;
        public List<Cliente> Clientes;
        public int CantClientes;
        public int Ganancia;
        public int GananciaTotal;

        public Local(string nombre, int cantEmp, int area, int preMin, int preMax, Categoria cat, Piso piso)
        {
            this.Nombre = nombre;
            this.CantEmpleados = cantEmp;
            this.Area = area;
            this.PrecioMin = preMin;
            this.PrecioMax = preMax;
            this.Cat = cat;
            List<Cliente> clnts = new List<Cliente>();
            this.Clientes = clnts;
            CantClientes = 0;
            Ganancia = 0;
            GananciaTotal = 0;
        }

        public void EntrarTienda(Cliente c)
        {
            this.Clientes.Add(c);
        }

        public void SalirTienda(Cliente c)
        {
            this.Clientes.Remove(this.Clientes.Find(x => x.Rut == c.Rut));
        }

        public void Venta(Cliente c, Trabajador t, Producto b)
        {
            if (c.Compras.Find(x => x.Id == b.Id) != null)
            {
                c.Saldo = c.Saldo - b.Precio;
                t.Saldo = t.Saldo + b.Precio * 0.05;
                c.ListaCompra.Remove(c.ListaCompra.Find(x => x.Id == b.Id));
                c.Compras.Add(b);
            }
        }
    }
}