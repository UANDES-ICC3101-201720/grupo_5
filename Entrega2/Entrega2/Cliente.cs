using System.Collections.Generic;

namespace Entrega2
{
    class Cliente : Persona
    {
        public List<Producto> ListaCompra;
        public List<Producto> Compras;

        public Cliente(string nombre, int rut, double saldo, List<Producto> listaCompra) : base(nombre, rut, saldo)
        {
            this.ListaCompra = listaCompra;
            List<Producto> cmp = new List<Producto>();
            this.Compras = cmp;
        }
    }
}
