namespace POOmall
{
    class Bien
    {
        public string Nombre;
        public int Id;
        public int Precio;

        public Bien(string nombre, int id, int precio)
        {
            this.Nombre = nombre;
            this.Id = id;
            this.Precio = precio;
        }
    }
}
