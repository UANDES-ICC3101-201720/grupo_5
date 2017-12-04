using System;

namespace POOmall
{
    public class Piso
    {
        public int Num;
        public int Espacio;
        public bool? Acceso;
        public bool? Estacionamiento;
        public bool? Subterraneo;
        public Local[] Tiendas;

        public Piso(int num, int esp, bool? acc, bool? est, bool? sub)
        {
            this.Num = num;
            this.Espacio = esp;
            this.Acceso = acc;
            this.Estacionamiento = est;
            this.Subterraneo = sub;
            Local[] tie = new Local[3];
            Tiendas = tie;
        }
        public override string ToString()
        {
            return "Piso " + Num + ",  Area: " + Espacio + ",  Est: " + Estacionamiento + ",  Acc: " + Acceso + ",  Sub: " + Subterraneo;
        }
    }
}