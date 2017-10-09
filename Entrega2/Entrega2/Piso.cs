using System;

namespace Entrega2
{
    class Piso
    {
        public int Num;
        public int Espacio;
        public Boolean Acceso;
        public Boolean Estacionamiento;
        public Boolean Subterraneo;
        public Local[] Tiendas;

        public Piso(int num, int esp, Boolean acc, Boolean est, Boolean sub)
        {
            this.Num = num;
            this.Espacio = esp;
            this.Acceso = acc;
            this.Estacionamiento = est;
            this.Subterraneo = sub;
            Local[] tie = new Local[3];
            Tiendas = tie;
        }
    }
}